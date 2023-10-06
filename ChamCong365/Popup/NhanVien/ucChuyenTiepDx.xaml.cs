using ChamCong365.APIs;
using ChamCong365.NhanVien;
using ChamCong365.NhanVien.DetailOfDon;
using ChamCong365.OOP.funcQuanLyCongTy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChamCong365.Popup.NhanVien
{
    /// <summary>
    /// Interaction logic for ucCreateStage.xaml
    /// </summary>
    /// 

    public partial class ucChuyenTiepDx : UserControl
    {
        MainChamCong Main;
        Dictionary<string,string> listAllEmployee = new Dictionary<string,string>(); 
        BrushConverter bc = new BrushConverter();
        int dx_id= 0;
        public ucChuyenTiepDx(MainChamCong main, int id)
        {
            InitializeComponent();
            this.Main= main;
            this.dx_id = id;
            GetAllEmployee();


        }
        private void cbxUserHiring_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cbxUserHiring.SelectedIndex = -1;
                string textSearch = cbxUserHiring.Text;
                cbxUserHiring.Items.Refresh();
                cbxUserHiring.ItemsSource = listAllEmployee.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
            }
        }

        private void cbxUserHiring_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxUserHiring.SelectedIndex = -1;
            string textSearch = cbxUserHiring.Text + e.Text;
            cbxUserHiring.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cbxUserHiring.Text = "";
                cbxUserHiring.Items.Refresh();
                cbxUserHiring.ItemsSource = listAllEmployee;
                cbxUserHiring.SelectedIndex = -1;
            }
            else
            {
                cbxUserHiring.ItemsSource = "";
                cbxUserHiring.Items.Refresh();
                cbxUserHiring.ItemsSource = listAllEmployee.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
            }
        }
        private async Task<bool> GetAllEmployee()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.managerUser_all);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent("10000"), "pageSize");
                request.Content = content;

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    EmployeeRoot result = JsonConvert.DeserializeObject<EmployeeRoot>(responseContent);
                 

                    foreach (var item in result.data.items)
                    {
                        listAllEmployee.Add(item.ep_id.ToString(), item.ep_name + "(" + item.ep_id + ")");
                        cbxUserHiring.ItemsSource = listAllEmployee;

                    }
                }
                return true;
            }
            catch
            {
                MessageBox.Show("lỗi lấy tất cả nhân viên");
            }
            return false;
        }

        private void bodExitPopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        private void bodSelectStage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListStageCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListStageCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListStageCollapsed.Visibility -= Visibility.Collapsed;

            }
        }



        private void lsvStage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var selectedItem = (ListProcess.Process)lsvStage.SelectedItem;
            //if (selectedItem != null)
            //{
            //    this.process_id = selectedItem.id;
            //    txbSelectStage.Text = selectedItem.name;
            //    txbSelectStage.Foreground = (Brush)bc.ConvertFromString("#474747");
            //    bodListStageCollapsed.Visibility = Visibility.Collapsed;
            //}

        }

        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bodListStageCollapsed.Visibility = Visibility.Collapsed;
        }
        public async void ChuyenTiepDX()
        {
            try
            {
                if (cbxUserHiring.SelectedIndex != -1)
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/editdx/edit_active");
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(dx_id.ToString()), "_id");
                    content.Add(new StringContent("4"), "type");
                    content.Add(new StringContent(cbxUserHiring.SelectedValue.ToString()), "id_uct");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        this.Visibility = Visibility.Collapsed;
                        ucChiTietDeXuat uc = new ucChiTietDeXuat(Main, dx_id);
                        Main.dopBody.Children.Clear();
                        object Content = uc.Content;
                        uc.Content = null;
                        Main.dopBody.Children.Add(Content as UIElement);
                    }
                }
                   

            }
            catch { }
        }
        private void OK_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChuyenTiepDX();
        }
    }
}
