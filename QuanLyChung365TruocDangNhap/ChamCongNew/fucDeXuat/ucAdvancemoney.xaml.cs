using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.fucDeXuat;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew
{

    /// logic for ucAdvancemoney

    public partial class ucAdvancemoney : UserControl
    {

        private MainWindow Main;
        BrushConverter brus = new BrushConverter();
        List<Employee> AllEmployeeList = new List<Employee>();
        int id_user = -1;
        int year = -1;
        int month = -1;
        int pageNumber = 1;
        int pageSize = 10;
        int TongSoTrang = 0;
        public ucAdvancemoney(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            LoadSelectBox();
            LoadSearchNameStaff();
            GetDSTamUng();


        }
        public void LoadSelectBox()
        {
            List<Item> listYear   = new List<Item>();
            for (int i = 1970; i <= DateTime.Now.Year; i++)
            {
                listYear.Add(new Item() { ID = i.ToString(), value = i.ToString() });
            }

            lsvSelectYear.ItemsSource = listYear.Prepend(new Item() { ID = "-1", value = "Chọn năm" });


            lsvSelectMonth.ItemsSource = ListItemCombobox.listMonth;
            




        }

        public async void GetDSTamUng()
        {
            try {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post,API.TamUng_api );
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(pageNumber.ToString()), "page");
                if(id_user!=-1) content.Add(new StringContent(id_user.ToString()),"id_user");
                request.Content = content;
                var response = await client.SendAsync(request);
                if(response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    TamUngTien.Root result = JsonConvert.DeserializeObject<TamUngTien.Root>(responseContent);
                    List<TamUngTien.DeXuat> listDeXuat = result.data.data.listDeXuat;
                    List<TamUngTien.User> listUser = result.data.data.listUser;
                    TongSoTrang = result.data.totalPages;
                    if(pageNumber==1) LoadTableDataPagging();
                    var listTamUngTien = (from dx in listDeXuat
                                                 select  new TamUngTien.TamUngTienEntities
                                                 {
                                                     _id = dx._id,
                                                     ep_id = dx.id_user,
                                                     ep_name = dx.name_user,
                                                     ep_avatar =listUser.Where(x => x.idQLC == dx.id_user).FirstOrDefault()?.avatarUser?? "https://tinhluong.timviec365.vn/img/add.png",
                                          
                                              
                                                     sotien_tam_ung = dx?.noi_dung?.tam_ung?.sotien_tam_ung,
                                                
                                                     trang_thai = (dx.type_duyet==5)?"Đã duyệt":"Chưa duyệt",
                                                     isCheck = (dx.type_duyet == 5) ?true:false
                                                 }).ToList();
                    foreach (var item in listTamUngTien)
                    {
                        item.ngay_tam_ung = await GetNgayTamUng(item._id);
                    }
                    if(month!=-1) listTamUngTien=listTamUngTien.Where(x=>x.ngay_tam_ung.Month==month).ToList();
                    if(year!=-1) listTamUngTien=listTamUngTien.Where(x=>x.ngay_tam_ung.Year==year).ToList();
                    lsvTamUngTien.ItemsSource = listTamUngTien;
                }

            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadTableDataPagging()
        {
            borPageDau.Visibility = Visibility.Collapsed;
            borLui1.Visibility = Visibility.Collapsed;
            borPage1.Visibility = Visibility.Collapsed;
            borPage2.Visibility = Visibility.Collapsed;
            borPage3.Visibility = Visibility.Collapsed;
            borLen1.Visibility = Visibility.Collapsed;
            borPageCuoi.Visibility = Visibility.Collapsed;

            if (TongSoTrang == 1)
            {
                borPage1.Visibility = Visibility.Visible;
            }
            if (TongSoTrang == 2)
            {
                borPage1.Visibility = Visibility.Visible;
                borPage2.Visibility = Visibility.Visible;
            }
            if (TongSoTrang == 3)
            {
                borPage1.Visibility = Visibility.Visible;
                borPage2.Visibility = Visibility.Visible;
                borPage3.Visibility = Visibility.Visible;
            }
            if (TongSoTrang > 3)
            {
                borPage1.Visibility = Visibility.Visible;
                borPage2.Visibility = Visibility.Visible;
                borPage3.Visibility = Visibility.Visible;
                borLen1.Visibility = Visibility.Visible;
                borPageCuoi.Visibility = Visibility.Visible;
            }




        }
        public async Task<DateTime> GetNgayTamUng(int dxId)
        {

            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.ChiTietDeXuat);
                request.Headers.Add("Authorization", "Bearer "+Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(dxId.ToString()), "_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();   
                    ChiTietDeXuat.Root result = JsonConvert.DeserializeObject<ChiTietDeXuat.Root>(responseContent);
                    long timeSpan  = (long)result.data.detailDeXuat[0].thong_tin_chung.tam_ung.ngay_tam_ung;
                    return ConvertToVST(timeSpan);
                }

            }
            catch(Exception ex)
            {
            }
            return DateTime.MinValue;
        }
        DateTime ConvertToVST(long unixTimestamp)
        {
            // Define the Vietnam Standard Time (VST) offset in seconds
            int vstOffsetSeconds = 25200; // UTC+7

            // Create a DateTime representing the Unix epoch (January 1, 1970, 00:00:00 UTC)
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Add the Unix timestamp and VST offset to the epoch
            DateTime vstDateTime = epoch.AddSeconds(unixTimestamp + vstOffsetSeconds);

            return vstDateTime;
        }

        public async void LoadSearchNameStaff()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.managerUser_all);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    EmployeeRoot result = JsonConvert.DeserializeObject<EmployeeRoot>(responseContent);
                    AllEmployeeList = result.data.items;
                    lsvListNameSaff.ItemsSource = AllEmployeeList.Prepend(new Employee() { ep_id = -1, ep_name = "Tất cả" });
                }
            }
            catch
            {

            }

        }
        private void bodSelectStaffName_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListStaffNameCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListStaffNameCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListStaffNameCollapsed.Visibility -= Visibility.Collapsed;

            }
        }
        private void lsvStaffName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectedItem = (Employee)lsvListNameSaff.SelectedItem;
            if (selectedItem != null)
            {
                this.id_user = selectedItem.ep_id;
                txbSelectStaffName.Text = selectedItem.ep_name;
                txbSelectStaffName.Foreground = (Brush)brus.ConvertFromString("#474747");
                bodListStaffNameCollapsed.Visibility = Visibility.Collapsed;
            }
        }

        private void bodSelectYear_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodSelectYearCollapsed.Visibility == Visibility.Collapsed)
            {
                bodSelectYearCollapsed.Visibility = Visibility.Visible;
                bodSelectMonthCollapsed.Visibility = Visibility.Collapsed;
                bodListStaffNameCollapsed.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Visible;
                txtSelectYear.Focus();
            }
            else
            {
                bodSelectYearCollapsed.Visibility = Visibility.Collapsed;
            }
        }
        private void txtSearchYear_TextChanged(object sender, TextChangedEventArgs e)
        {
            //foreach (var itemYear in listYear)
            //{
            //    if (itemYear.ToLower().Contains(txtSearchYear.Text.ToString()))
            //    {
            //        listSearch.Add(itemYear);
            //    }

            //}
            //lsvListYear.ItemsSource = listSearch;
        }
        private void bodSelectMonth_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodSelectMonthCollapsed.Visibility == Visibility.Collapsed)
            {
                bodSelectMonthCollapsed.Visibility = Visibility.Visible;
                bodListStaffNameCollapsed.Visibility = Visibility.Collapsed;
                bodSelectYearCollapsed.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Visible;
                txtSelectMonth.Focus();
            }
            else
            {
                bodSelectMonthCollapsed.Visibility = Visibility.Collapsed;
            }
        }

        private void lsvList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            bodSelectYearCollapsed.Visibility = Visibility.Collapsed;
        }

        private void txtSearchNameSaff_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textName = txtSearchNameSaff.Text;
            lsvListNameSaff.ItemsSource = AllEmployeeList.Where(x => x.ep_name.ToUpper().Contains(textName.ToUpper())).Prepend(new Employee() { ep_id = -1, ep_name = "Tất cả" }); ;
        }

        private void lsvSelectMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem  =lsvSelectMonth.SelectedItem as Item;
            if(selectedItem != null)
            {
                month = int.Parse(selectedItem.ID);
                txtSelectMonth.Text = selectedItem.value;
                bodSelectMonthCollapsed.Visibility = Visibility.Collapsed;
            }

        }



        private void checkManager_Checked(object sender, RoutedEventArgs e)
        {
            bodMessageboxYesAll.Visibility = Visibility.Visible;
            bodMessageboxNoAll.Visibility = Visibility.Collapsed;
        }

        private void checkManager_Unchecked(object sender, RoutedEventArgs e)
        {
            bodMessageboxNoAll.Visibility = Visibility.Visible;
            bodMessageboxYesAll.Visibility = Visibility.Collapsed;
        }

        private void bodOkMessageYesAll_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bodMessageboxYesAll.Visibility = Visibility.Collapsed;
        }
        private void bodOkMessageNoAll_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bodMessageboxNoAll.Visibility = Visibility.Collapsed;
        }

        private void bodOkMessageYesSelected_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bodMessageboxYesSelected.Visibility = Visibility.Collapsed;
        }

        private void bodOkMessageNoSelected_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bodMessageboxNoSelected.Visibility = Visibility.Collapsed;
        }

        private void checkManagerSelected_Checked(object sender, RoutedEventArgs e)
        {
            bodMessageboxYesSelected.Visibility = Visibility.Visible;
            bodMessageboxNoSelected.Visibility = Visibility.Collapsed;
        }

        private void checkManagerSelected_Unchecked(object sender, RoutedEventArgs e)
        {
            bodMessageboxNoSelected.Visibility = Visibility.Visible;
            bodMessageboxYesSelected.Visibility = Visibility.Collapsed;
        }

        private void bodYear_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodYear.Visibility == Visibility.Collapsed)
            {
                bodYear.Visibility = Visibility.Visible;
                txtSearchYear.Focus();

            }
            else
            {
                bodYear.Visibility -= Visibility.Collapsed;
            }
        }

        private void lsvListYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = lsvSelectYear.SelectedItem as Item;
            if (selectedItem != null)
            {
                year = int.Parse(selectedItem.ID);
                txtSelectYear.Text = selectedItem.value;
                bodSelectYearCollapsed.Visibility = Visibility.Collapsed;
            }

        }

        private void txtSearchMonth_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtSearchNV_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void bodAddMoney_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (bodAddMoney.Visibility == Visibility.Collapsed)
            //{
            //    bodAddMoney.Visibility = Visibility.Visible;

            //    bodAddInfo.Visibility = Visibility.Visible;




            //}
            //else
            //{
            //    bodAddMoney.Visibility = Visibility.Collapsed;

            //    bodAddInfo.Visibility = Visibility.Collapsed;
            //}
        }

        private void bodCheckBoxAll_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (checkManagerAll.Background == Brushes.White)
            //{
            //    checkManagerAll.Background = Brushes.Black;
            //    bodMessageboxYesAll.Visibility = Visibility.Collapsed;

            //    bodMessageboxYesAll.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    if (bodMessageboxNoAll.Visibility == Visibility.Collapsed)
            //    {

            //        bodMessageboxNoAll.Visibility = Visibility.Visible;

            //        checkManagerAll.Background = Brushes.White;




            //    }
            //}
        }

        private void lsvTamUngTien_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void popup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodSelectMonthCollapsed.Visibility = Visibility.Collapsed;
            bodListStaffNameCollapsed.Visibility = Visibility.Collapsed;
            bodSelectYearCollapsed.Visibility = Visibility.Collapsed;
            popup.Visibility = Visibility.Collapsed;
        }

        private void checkDuyet_Checked(object sender, RoutedEventArgs e)
        {
            Main.grShowPopup.Children.Add(new Popup.DeXuat.TamUngTien.PopUpHoiTruocKhiDuyet(Main, "Việc duyệt sẽ ảnh hưởng đến mức lương của nhân viên bạn vẫn muốn tiếp tục ?"));
        }

        private void checkDuyet_Unchecked(object sender, RoutedEventArgs e)
        {
            Main.grShowPopup.Children.Add(new Popup.DeXuat.TamUngTien.PopUpHoiTruocKhiDuyet(Main, "Bạn có chắc chắn muốn bỏ duyệt không ?"));

        }

        #region phantrang
        private void borPageDau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            borPageDau.Visibility = Visibility.Collapsed;
            borLui1.Visibility = Visibility.Collapsed;
            borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
            textPage1.Text = "1";
            textPage2.Text = "2";
            textPage3.Text = "3";
            borLen1.Visibility = Visibility.Visible;
            borPageCuoi.Visibility = Visibility.Visible;

            pageNumber = 1;
            GetDSTamUng();
        }

        private void borLui1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            if (int.Parse(textPage1.Text) >= 1)
            {
                if (textPage3.Text == TongSoTrang.ToString() && borPageCuoi.Visibility == Visibility.Collapsed)
                {
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPageCuoi.Visibility = Visibility.Visible;
                    borLen1.Visibility = Visibility.Visible;

                }
                else
                {
                    if (textPage1.Text == "1")
                    {
                        borPageDau.Visibility = Visibility.Collapsed;
                        borLui1.Visibility = Visibility.Collapsed;
                        borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borLen1.Visibility = Visibility.Visible;
                        borPageCuoi.Visibility = Visibility.Visible;

                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();

                    }


                }
            }
            pageNumber--;
            GetDSTamUng();

        }

        private void borPage1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (int.Parse(textPage1.Text) >= 1)
            {
                if (textPage1.Text == (TongSoTrang - 2).ToString() && borPageCuoi.Visibility == Visibility.Collapsed && TongSoTrang > 3)
                {

                    textPage1.Text = (TongSoTrang - 3).ToString();
                    textPage2.Text = (TongSoTrang - 2).ToString();
                    textPage3.Text = (TongSoTrang - 1).ToString();


                    BrushConverter brus = new BrushConverter();

                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    if (TongSoTrang > 2)
                    {
                        borLen1.Visibility = Visibility.Visible;
                        borPageCuoi.Visibility = Visibility.Visible;
                    }



                }
                else
                {

                    if (textPage1.Text == "1")
                    {
                        BrushConverter brus = new BrushConverter();
                        borPageDau.Visibility = Visibility.Collapsed;
                        borLui1.Visibility = Visibility.Collapsed;
                        borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        if (TongSoTrang > 3)
                        {
                            borLen1.Visibility = Visibility.Visible;
                            borPageCuoi.Visibility = Visibility.Visible;
                        }


                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();

                    }
                }
            }
            if (int.Parse(textPage1.Text) > 1)
            {
                pageNumber = int.Parse(textPage2.Text);
                GetDSTamUng();
            }
            if (int.Parse(textPage1.Text) == 1)
            {
                pageNumber = 1;
                GetDSTamUng();
            }

        }

        private void borPage2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");

            if (TongSoTrang > 3)
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;

                if (textPage2.Text == (TongSoTrang - 1).ToString())
                {
                    borPageCuoi.Visibility = Visibility.Visible;
                    borLen1.Visibility = Visibility.Visible;
                }
            }

            pageNumber = int.Parse(textPage2.Text);
            GetDSTamUng();


            //listDepartment = result.data.items;

        }

        private void borPage3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {


            if (TongSoTrang == 3)
            {
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");

            }
            else if (TongSoTrang > 3)
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                if (textPage3.Text == TongSoTrang.ToString())
                {

                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Collapsed;

                }
                else if (textPage3.Text == "3")
                {
                    textPage1.Text = "2";
                    textPage2.Text = "3";
                    textPage3.Text = "4";
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");

                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();


                }
            }

            if (pageNumber == TongSoTrang - 1) { pageNumber = int.Parse(textPage3.Text); }
            else pageNumber = int.Parse(textPage2.Text);
            GetDSTamUng();


        }
        private void borLen1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (TongSoTrang == 3)
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");

            }
            else if (TongSoTrang > 3)
            {
                if (textPage3.Text == TongSoTrang.ToString())
                {
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Collapsed;


                }
                else if (textPage3.Text == "3")
                {

                    if (borPageDau.Visibility == Visibility.Collapsed && borPageCuoi.Visibility == Visibility.Visible)
                    {
                        BrushConverter brus = new BrushConverter();
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPageDau.Visibility = Visibility.Visible;
                        borLui1.Visibility = Visibility.Visible;


                    }
                    else if (borPageDau.Visibility == Visibility.Visible && borPageCuoi.Visibility == Visibility.Visible)
                    {
                        textPage1.Text = "2";
                        textPage2.Text = "3";
                        textPage3.Text = "4";
                        BrushConverter brus = new BrushConverter();
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");

                    }


                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();

                }

            }
            pageNumber++;
            GetDSTamUng();
        }

        private void borPageCuoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textPage3.Text = TongSoTrang.ToString();
            textPage2.Text = (TongSoTrang - 1).ToString();
            textPage1.Text = (TongSoTrang - 2).ToString();
            borPageDau.Visibility = Visibility.Visible;
            borLui1.Visibility = Visibility.Visible;
            BrushConverter brus = new BrushConverter();
            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            borPageCuoi.Visibility = Visibility.Collapsed;
            borLen1.Visibility = Visibility.Collapsed;
            pageNumber = (int)TongSoTrang;
            GetDSTamUng();
        }
        #endregion

        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = ((Rectangle)sender).Parent as Grid;
            var bodPopUp = grid.Parent as Border;
            bodPopUp.Visibility = Visibility.Collapsed;
        }

        private void btnThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            pageNumber = 1;
            GetDSTamUng();
        }

        private void Detail_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var  selectedItem = lsvTamUngTien.SelectedItem as TamUngTien.TamUngTienEntities;
            if(selectedItem!= null)
            {
                int dx_id = selectedItem._id;
                ucDetailAdvancemoney frm = new ucDetailAdvancemoney(Main,selectedItem._id);
                Main.dopBody.Children.Clear();
                object content = frm.Content;
                frm.Content = null;
                Main.dopBody.Children.Add(content as UIElement);

            }
            
        }
    }

}












