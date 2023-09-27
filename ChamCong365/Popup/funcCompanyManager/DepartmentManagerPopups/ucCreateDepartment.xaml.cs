using ChamCong365.APIs;
using ChamCong365.OOP.funcQuanLyCongTy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace ChamCong365.Popup.funcCompanyManager
{
    /// <summary>
    /// Interaction logic for ucCreateDepartment.xaml
    /// </summary>
    public partial class ucCreateDepartment : UserControl
    {
        private ucListDpartment ucListDpartment;
        BrushConverter bc = new BrushConverter();
        private string com_id = "";
        public ucCreateDepartment(ucListDpartment ucListDpartment, string com_id)
        {
            InitializeComponent();
            this.ucListDpartment = ucListDpartment;
            lsvCompany.ItemsSource = ucListDpartment.dataCompanySelectBox;
            //GetListChildCompany();
            this.com_id = com_id;
        }

        private async void CreateDepartment()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.create_department_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id), "com_id");
                content.Add(new StringContent(txtDepartmentName.Text), "dep_name");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    bodCreateDepartmentCollapsed.Visibility = Visibility.Collapsed;
                    bodThemThanhCong.Visibility = Visibility.Visible;
                    ucListDpartment.LoadListDepartment();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi khi thêm phòng ban");
            }
        }

        //get danh sách tên công ty con đổ vào dropdown box chọn công ty
        private async void GetListChildCompany()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.list_ChildCompany_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                CompanyRoot result = JsonConvert.DeserializeObject<CompanyRoot>(responseContent);

                lsvCompany.ItemsSource = result.data.items;

            }
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại!" + e.Message);
            }
        }

        private void bodExitPopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        private void bodSelectCompany_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListCompany.Visibility == Visibility.Collapsed)
            {
                bodListCompany.Visibility = Visibility.Visible;
            }
            else
            {
                bodListCompany.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = lsvCompany.SelectedItem as Company;
            if (selectedItem != null)
            {
                com_id = selectedItem.com_id.ToString();
                txbSelectCompany.Text = selectedItem.com_name;
                txbSelectCompany.Foreground = (Brush)bc.ConvertFromString("#474747");
                bodListCompany.Visibility = Visibility.Collapsed;
            }

        }

        private void bodOK_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodAdd_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CreateDepartment();
        }
    }
}
