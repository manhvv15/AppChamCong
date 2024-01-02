using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.DetailOfDon;
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
using System.Windows.Shapes;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.ToiGuiDi.DeXuatToiGuiDi;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.Propose
{
    /// <summary>
    /// Interaction logic for ToiGuiDi.xaml
    /// </summary>
    public partial class ToiGuiDi : Window
    {

        BrushConverter brus = new BrushConverter();
        Dictionary<string, string> listAllEmployee = new Dictionary<string, string>();
        List<Employee> listAllEmployee1 = new List<Employee>();
        List<ListCateDxEntites.Showcatedx> listCateDx = new List<ListCateDxEntites.Showcatedx>();
        MainChamCong Main;
        int type = -1;
        int pageNumber = 1;
        int pageSize = 10;
        int TongSoTrang = 0;
        int selectedTab = 1;

        public ToiGuiDi(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
            TabDisableAll();
            gridSearchArea.ColumnDefinitions[1].Width = new GridLength(0);
            gridSearchArea.ColumnDefinitions[2].Width = new GridLength(0);
            GetListCateDx();
            GetAll();
            Main.Back = 41;


        }
        public ToiGuiDi(MainChamCong main, int type)
        {
            InitializeComponent();
            this.type = type;
            Main = main;
            TabDisableAll();
            if (type == 2)
            {
                txbChoDuyet.Foreground = (Brush)brus.ConvertFromString("#4AA7FF");
                gridChoDuyet.Visibility = Visibility.Visible;
            }
            if (type == 4)
            {
                txbDaDuyet.Foreground = (Brush)brus.ConvertFromString("#4AA7FF");
                gridDaDuyet.Visibility = Visibility.Visible;
            }
            gridSearchArea.ColumnDefinitions[1].Width = new GridLength(0);
            gridSearchArea.ColumnDefinitions[2].Width = new GridLength(0);
            GetListCateDx();
            GetAll();
            Main.Back = 41;


        }
        public ToiGuiDi(MainChamCong main, int type, bool isGuiDenToi)
        {
            InitializeComponent();
            this.type = type;
            Main = main;
            TabDisableAll();
            NavigateGuiDenToi();
            gridSearchArea.ColumnDefinitions[1].Width = new GridLength(0);
            gridSearchArea.ColumnDefinitions[2].Width = new GridLength(0);
            txbChoDuyet.Foreground = (Brush)brus.ConvertFromString("#4AA7FF");
            gridChoDuyet.Visibility = Visibility.Visible;
            GetListCateDx();

            GetAll();
            Main.Back = 41;


        }

        public async void GetAll()
        {
            bool isLoadedEmpList = await GetAllEmployee();
            if (isLoadedEmpList)
            {
                getData();
            }
        }
        private async void getData()
        {

            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage();
                if (selectedTab == 1) request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/DeXuat/user_send_deXuat_All");
                if (selectedTab == 2) request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/DeXuat/deXuat_send_user");
                if (selectedTab == 3) request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/DeXuat/deXuat_follow");


                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();

                if (selectedTab == 2 || selectedTab == 3)
                {
                    if (cbxUserSend.SelectedIndex != -1) content.Add(new StringContent(cbxUserSend.SelectedValue.ToString()), "id_user");
                }
                if (selectedTab == 1) { if (cbxUserHiring.SelectedIndex != -1) content.Add(new StringContent(cbxUserHiring.SelectedValue.ToString()), "id_user_duyet"); };
                content.Add(new StringContent(txtNameDeXuat.Text), "name_dx");
                if (cbxType_dx.SelectedIndex != -1) content.Add(new StringContent(cbxType_dx.SelectedValue.ToString()), "type_dx");
                if (dpTimeStart.SelectedDate != null) content.Add(new StringContent(dpTimeStart?.SelectedDate?.ToString("yyyy-MM-dd")), "time_s");
                if (dpTimeEnd.SelectedDate != null) content.Add(new StringContent(dpTimeEnd?.SelectedDate?.ToString("yyyy-MM-dd")), "time_e");
                if (type != -1) content.Add(new StringContent(type.ToString()), "type");
                content.Add(new StringContent(pageNumber.ToString()), "page");
                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    // Xử lý phản hồi ở đây
                    DeXuatCuaToi_Entities.Root result = JsonConvert.DeserializeObject<DeXuatCuaToi_Entities.Root>(responseContent);
                    TongSoTrang = (int)result.data.totalPages;
                    if (pageNumber == 1) LoadTableDataPagging();

                    if (result.data.data != null)
                    {


                        foreach (var item in result.data.data)
                        {
                            string[] listIdUserDuyet = item.id_user_duyet.Split(',');
                            foreach (string idUser in listIdUserDuyet)
                            {

                                item.name_user_duyet += listAllEmployee1.Where(x => x.ep_id == int.Parse(idUser)).FirstOrDefault()?.ep_name + "| ";

                            }
                            item.name_user_duyet = CatBoKiTuCuoiKhongPhaiChu(item.name_user_duyet);


                        }

                        dgv.ItemsSource = result.data.data;
                        //listDataToiGuiDi = result.data.data;
                    }
                }
                else
                {
                    // Xử lý khi có lỗi trong phản hồi (ví dụ: response.StatusCode)
                    // Ví dụ: throw new Exception("Có lỗi trong yêu cầu HTTP.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại! " + ex.Message);
            }

        }
        private async void GetListCateDx()
        {

            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.showlistcate_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ListCateDxEntites.Root result = JsonConvert.DeserializeObject<ListCateDxEntites.Root>(responseContent);

                    listCateDx = result.data.showcatedx.Prepend(new ListCateDxEntites.Showcatedx() { _id = 0, name_cate_dx = "Chọn loại đề xuất" }).ToList();
                    cbxType_dx.ItemsSource = listCateDx;

                }

            }
            catch { MessageBox.Show("Co loi"); }
        }
        static string CatBoKiTuCuoiKhongPhaiChu(string str)
        {
            while (!string.IsNullOrEmpty(str) && !char.IsLetter(str[str.Length - 1]))
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str;
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
        private void cbxUserHiring_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Back)
                {
                    cbxUserHiring.SelectedIndex = -1;
                    string textSearch = cbxUserHiring.Text;
                    cbxUserHiring.Items.Refresh();
                    cbxUserHiring.ItemsSource = listAllEmployee.Where(t => t.Value.RemoveUnicode().ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
                }
            }
            catch { }

        }

        private void cbxUserHiring_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
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
                    cbxUserHiring.ItemsSource = listAllEmployee.Where(t => t.Value.RemoveUnicode().ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
                }
            }
            catch { }
        }
        private void cbxUserSend_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Back)
                {
                    cbxUserSend.SelectedIndex = -1;
                    string textSearch = cbxUserSend.Text;
                    cbxUserSend.Items.Refresh();
                    cbxUserSend.ItemsSource = listAllEmployee.Where(t => t.Value.RemoveUnicode().ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
                }
            }
            catch { }

        }

        private void cbxUserSend_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {

                cbxUserSend.SelectedIndex = -1;
                string textSearch = cbxUserSend.Text + e.Text;
                cbxUserSend.IsDropDownOpen = true;
                if (textSearch == "")
                {
                    cbxUserSend.Text = "";
                    cbxUserSend.Items.Refresh();
                    cbxUserSend.ItemsSource = listAllEmployee;
                    cbxUserSend.SelectedIndex = -1;
                }
                else
                {
                    cbxUserSend.ItemsSource = "";
                    cbxUserSend.Items.Refresh();
                    cbxUserSend.ItemsSource = listAllEmployee.Where(t => t.Value.RemoveUnicode().ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
                }
            }
            catch { }
        }


        private void cbxType_dx_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cbxType_dx.SelectedIndex = -1;
                string textSearch = cbxType_dx.Text;
                cbxType_dx.Items.Refresh();
                cbxType_dx.ItemsSource = listCateDx.Where(t => t.name_cate_dx.RemoveUnicode().ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }

        private void cbxType_dx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxType_dx.SelectedIndex = -1;
            string textSearch = cbxType_dx.Text + e.Text;
            cbxType_dx.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cbxType_dx.Text = "";
                cbxType_dx.Items.Refresh();
                cbxType_dx.ItemsSource = listCateDx;
                cbxType_dx.SelectedIndex = -1;
            }
            else
            {
                cbxType_dx.ItemsSource = "";
                cbxType_dx.Items.Refresh();
                cbxType_dx.ItemsSource = listCateDx.Where(t => t.name_cate_dx.RemoveUnicode().ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
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
                    listAllEmployee1 = result.data.items;
                    listAllEmployee.Add("", "Tất cả");
                    foreach (var item in result.data.items)
                    {
                        listAllEmployee.Add(item.ep_id.ToString(), item.ep_name + "(" + item.ep_id + ")");
                        cbxUserHiring.ItemsSource = listAllEmployee;
                        cbxUserSend.ItemsSource = listAllEmployee;

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
        public void TabDisableAll()
        {
            txbTatCa.Foreground = (Brush)brus.ConvertFromString("#474747");
            txbChoDuyet.Foreground = (Brush)brus.ConvertFromString("#474747");
            txbDaDuyet.Foreground = (Brush)brus.ConvertFromString("#474747");
            txbDaTuChoi.Foreground = (Brush)brus.ConvertFromString("#474747");
            gridTatCa.Visibility = Visibility.Collapsed;
            gridChoDuyet.Visibility = Visibility.Collapsed;
            gridDaDuyet.Visibility = Visibility.Collapsed;
            gridDaTuChoi.Visibility = Visibility.Collapsed;
        }

        private void tatCa_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TabDisableAll();
            txbTatCa.Foreground = (Brush)brus.ConvertFromString("#4AA7FF");
            gridTatCa.Visibility = Visibility.Visible;
            type = 1;
            getData();
        }

        private void ChoDuyet_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TabDisableAll();
            txbChoDuyet.Foreground = (Brush)brus.ConvertFromString("#4AA7FF");
            gridChoDuyet.Visibility = Visibility.Visible;
            type = 2;
            getData();
        }
        private void DaDuyet_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TabDisableAll();
            txbDaDuyet.Foreground = (Brush)brus.ConvertFromString("#4AA7FF");
            gridDaDuyet.Visibility = Visibility.Visible;
            type = 4;
            getData();
        }

        private void DaTuChoi_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TabDisableAll();
            txbDaTuChoi.Foreground = (Brush)brus.ConvertFromString("#4AA7FF");
            gridDaTuChoi.Visibility = Visibility.Visible;
            type = 5;
            getData();
        }

        private void borSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            getData();
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
            getData();
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
            getData();

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
                getData();
            }
            if (int.Parse(textPage1.Text) == 1)
            {
                pageNumber = 1;
                getData();
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
            getData();


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
            getData();


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
            getData();
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
            getData();
        }
        #endregion



        public void TabUnActive()
        {
            borGuiDenToiActive.Visibility = Visibility.Collapsed;
            borToiGuiDiActive.Visibility = Visibility.Collapsed;
            borDangTheoDoiActive.Visibility = Visibility.Collapsed;
        }
        private void NavigateToGuiDenToi(object sender, MouseButtonEventArgs e)
        {
            NavigateGuiDenToi();
        }
        private void NavigateGuiDenToi()
        {
            TabUnActive();
            gridSearchBox.Visibility = Visibility.Visible;
            borGuiDenToiActive.Visibility = Visibility.Visible;
            selectedTab = 2;
            gridSearchArea.ColumnDefinitions[1].Width = gridSearchArea.ColumnDefinitions[0].Width;
            gridSearchArea.ColumnDefinitions[2].Width = gridSearchArea.ColumnDefinitions[0].Width;
            cbxUserSend.Visibility = Visibility.Visible;
            cbxUserHiring.Visibility = Visibility.Collapsed;
            getData();
        }
        private void NavigateToDangTheoDoi(object sender, MouseButtonEventArgs e)
        {
            TabUnActive();
            gridSearchBox.Visibility = Visibility.Collapsed;

            borDangTheoDoiActive.Visibility = Visibility.Visible;
            selectedTab = 3;
            gridSearchArea.ColumnDefinitions[1].Width = gridSearchArea.ColumnDefinitions[0].Width;
            gridSearchArea.ColumnDefinitions[2].Width = gridSearchArea.ColumnDefinitions[0].Width;
            cbxUserSend.Visibility = Visibility.Visible;
            cbxUserHiring.Visibility = Visibility.Collapsed;
            getData();
        }

        private void NavigateToToiGuiDi(object sender, MouseButtonEventArgs e)
        {
            TabUnActive();
            gridSearchBox.Visibility = Visibility.Visible;

            borToiGuiDiActive.Visibility = Visibility.Visible;
            selectedTab = 1;
            gridSearchArea.ColumnDefinitions[1].Width = new GridLength(0);
            gridSearchArea.ColumnDefinitions[2].Width = new GridLength(0);
            cbxUserSend.Visibility = Visibility.Collapsed;
            cbxUserHiring.Visibility = Visibility.Visible;
            getData();
        }
        private void DetailDeXuat_MouseUp(object sender, MouseButtonEventArgs e)
        {

            var dx = dgv.SelectedItem as DeXuatCuaToi_Entities.InforDx;
            if (dx != null)
            {
                int dx_id = dx._id;
                ucChiTietDeXuat uc = new ucChiTietDeXuat(Main, dx_id, true);
                this.Visibility = Visibility.Collapsed;
                object Content = uc.Content;
                uc.Content = null;
                Main.dopBody.Children.Add(Content as UIElement);
            }

        }

        private void dgv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {

                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    var scrollViewer = FindVisualChild<ScrollViewer>(dgv);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - e.Delta);
                        e.Handled = true;
                    }

                }
                else
                {
                    Main.scrollMainChamCong.ScrollToVerticalOffset(Main.scrollMainChamCong.VerticalOffset - e.Delta);
                }
            }
            catch { }
        }

        private T FindVisualChild<T>(DependencyObject visual) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(visual, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null)
                        return childItem;
                }
            }
            return null;
        }
    }
}
