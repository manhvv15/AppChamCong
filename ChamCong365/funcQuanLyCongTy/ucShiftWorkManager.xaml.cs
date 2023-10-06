using ChamCong365.Popup;
using ChamCong365.Popup.DatePicker;
using ChamCong365.Popup.funcCompanyManager;
using ChamCong365.Popup.funcCompanyManager.ShiftWorkPopups;
using ChamCong365.TimeKeeping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ChamCong365.APIs;
using ChamCong365.OOP.funcQuanLyCongTy;
using Newtonsoft.Json;
using ChamCong365.OOP.Login;

namespace ChamCong365
{


    /// <summary>
    /// Interaction logic for ucShiftWorkManager.xaml
    /// </summary>
    /// 

    public partial class ucShiftWorkManager : UserControl
    {
        MainWindow Main;
        string com_id = "";
        List<Shift> listShift = new List<Shift>();
        public ucShiftWorkManager(MainWindow main)
        {
            this.Main = main;
            InitializeComponent();
            LoadCompanySelectBox();
            GetListShift();

        }

        //get danh sách ca làm việc
        public async void GetListShift()
        {
            try
            {

                var httpClient = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Get;
                string api = API.list_shift_api;

                httpRequestMessage.RequestUri = new Uri(api);
                httpRequestMessage.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var response = await httpClient.SendAsync(httpRequestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();
                ShiftRoot result = JsonConvert.DeserializeObject<ShiftRoot>(responseContent);

                listShift = result.data.items;
                // Thêm một phần tử trống
                listShift.Add(new Shift());

                ListCaLamViec.ItemsSource = listShift;

                // thêm last item  là button thêm ca làm việc

                if (ListCaLamViec.Items.Count > 0)
                {
                    // Get the last item
                    var lastItem = ListCaLamViec.Items[ListCaLamViec.Items.Count - 1];

                    // Apply the custom template to the last item
                    var lastItemContainer = ListCaLamViec.ItemContainerGenerator.ContainerFromItem(lastItem) as ListViewItem;
                    if (lastItemContainer != null)
                    {
                        lastItemContainer.ContentTemplate = ListCaLamViec.Resources["LastItemTemplate"] as DataTemplate;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại!" + e.Message);
            }
        }
        // Ham load select box company
        public void LoadCompanySelectBox()
        {
            Company ParentCompany = new Company() { com_id = Main.IdAcount, com_name = Main.txbNameAccount.Text };
            //GetListChildCompany();
            List<Company> listDataDropBox = new List<Company>();
            listDataDropBox.Add(ParentCompany);
            lsvCompany.ItemsSource = listDataDropBox;
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

        //Áp dụng template cho item cuối là button thêm
        private void ListCaLamViec_Loaded(object sender, RoutedEventArgs e)
        {
            if (ListCaLamViec.Items.Count > 0)
            {
                // Get the last item
                var lastItem = ListCaLamViec.Items[ListCaLamViec.Items.Count - 1];

                // Apply the custom template to the last item
                var lastItemContainer = ListCaLamViec.ItemContainerGenerator.ContainerFromItem(lastItem) as ListViewItem;
                if (lastItemContainer != null)
                {
                    lastItemContainer.ContentTemplate = ListCaLamViec.Resources["LastItemTemplate"] as DataTemplate;
                }
            }
        }

        //Xử lý nút ấn lịch làm việc
        private void btnWorkCalendar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucCaiDatLichLamViec uc = new ucCaiDatLichLamViec(Main, Main.IdAcount);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }
        //Xử lý drop box chọn công ty
        private void Image_MouseLeftButtonUp_SelectCompany(object sender, MouseButtonEventArgs e)
        {
            if (bodCompany.Visibility == Visibility.Collapsed)
            {
                bodCompany.Visibility = Visibility.Visible;
            }
            else
            {
                bodCompany.Visibility -= Visibility.Collapsed;

            }
        }
        //Xử lý khi ấn chọn công ty
        private void lsvCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectedItem = lsvCompany.SelectedItem as Company;
            txbSelectCompany.Text = selectedItem.com_name;
            bodCompany.Visibility = Visibility.Collapsed;
            com_id = selectedItem.com_id.ToString();
            var itemSource = listShift.Where(x => x.com_id.ToString() == com_id).ToList();
            itemSource.Add(new Shift());
            ListCaLamViec.ItemsSource = itemSource;

            // thêm last item  là button thêm ca làm việc

            if (ListCaLamViec.Items.Count > 0)
            {
                // Get the last item
                var lastItem = ListCaLamViec.Items[ListCaLamViec.Items.Count - 1];

                // Apply the custom template to the last item
                var lastItemContainer = ListCaLamViec.ItemContainerGenerator.ContainerFromItem(lastItem) as ListViewItem;
                if (lastItemContainer != null)
                {
                    lastItemContainer.ContentTemplate = ListCaLamViec.Resources["LastItemTemplate"] as DataTemplate;
                }
            }

        }
        //xử lý khi hover vào các item trong items ca làm việc
        private void LsvItem_CaLamViec_MouseEnter(object sender, MouseEventArgs e)
        {
            var gird = (System.Windows.Controls.Grid)sender;
            gird.Children[3].Visibility = Visibility.Visible;

        }

        private void LsvItem_CaLamViec_MouseLeave(object sender, MouseEventArgs e)
        {
            var gird = (System.Windows.Controls.Grid)sender;
            gird.Children[3].Visibility = Visibility.Collapsed;
        }
        //Sửa ca làm việc
        private void docpSuaCaLamViec_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var selectedShift = ListCaLamViec.SelectedItem as Shift;
            Main.grShowPopup.Children.Add(new ucUpdateShiftWork(this, selectedShift));
        }
        //Xóa ca làm việc
        private void docpXoaCaLamViec_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var selectedShift = ListCaLamViec.SelectedItem as Shift;
            var shift_id = selectedShift.shift_id;
            Main.grShowPopup.Children.Add(new ucDeleteShiftWork(this, shift_id.ToString()));

        }

        //Xử lý button thêm ca làm việc
        private void bodBtnAdd_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucCreateShiftWork(this, com_id));
        }

        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bodCompany.Visibility = Visibility.Collapsed;
        }
    }
}
