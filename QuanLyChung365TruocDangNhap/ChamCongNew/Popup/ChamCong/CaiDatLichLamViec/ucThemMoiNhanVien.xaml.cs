using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Comons;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi.API_List_Detail;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.CaiDatLichLamViec
{
    /// <summary>
    /// Interaction logic for ucCreateSaff.xaml
    /// </summary>
    /// 
    public partial class ucThemMoiNhanVien : UserControl, INotifyPropertyChanged
    {
        MainWindow Main;
        BrushConverter bc = new BrushConverter();
        public string month;
        public string year;
        public int Cy_Id;
        public string AppLy_Month;
        public int pageSize = 10000;
        ucCaiDatLichLamViec ucCaiDatLichLamViec;
        public ucDanhSachNhanVien ucNv;
        private List<EmployeeNotInCycleEntites.Item> _lstAllSaff;
        public List<EmployeeNotInCycleEntites.Item> lstAllSaff
        {
            get { return _lstAllSaff; }
            set { _lstAllSaff = value; }
        }

        private List<EmployeeNotInCycleEntites.Item> _lstAllSaff1;
        public List<EmployeeNotInCycleEntites.Item> lstAllSaff1
        {
            get { return _lstAllSaff1; }
            set { _lstAllSaff1 = value; }
        }
        ucCaiDatLichLamViec ucSetting;
        public ucThemMoiNhanVien(MainWindow main, int id, ucCaiDatLichLamViec ucSetting, string apply_Month)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            Cy_Id = id;
            AppLy_Month = apply_Month;
            this.ucSetting = ucSetting;
            LoadListSaff();
            GetListOrganize();
            //LoadDep();
        }
        List<Item_PhongBan> listDep = new List<Item_PhongBan>();
        Item_PhongBan newItem = new Item_PhongBan
        {
            dep_name = "Tất cả phòng ban"
        };

        #region Call Api AddSaffinCalendarWork
        public async void LoadDep()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/department/list");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    Root_PhongBan api = JsonConvert.DeserializeObject<Root_PhongBan>(responseContent);
                    if (api != null)
                    {

                        listDep = api.data.items;
                        listDep.Insert(0, newItem);
                        lsvDep.ItemsSource = listDep;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("loi load department" + ex.Message);
            }
        }
        public async Task LoadListSaffInCalendarWork()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.list_saff_in_Calendar_Work_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Cy_Id.ToString()), "cy_id");
                request.Content = content;
                var response = await client.SendAsync(request);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    RootSaff result = JsonConvert.DeserializeObject<RootSaff>(responseContent);
                    if (result != null)
                    {
                        listAdd = result.data.list;
                    }
                };

            }
            catch (System.Exception)
            {
            }
        }
        List<ItemAllSaff> listStaff = new List<ItemAllSaff>();
        public async void LoadListSaff()
        {
            //await LoadListSaffInCalendarWork();
            try
            {
                List<ListOrganizeEntities.ListOrganizeDetailId> listOrganizeDetailIds = null;
                if (cboTenToChuc.SelectedIndex != -1)
                {
                    listOrganizeDetailIds = (cboTenToChuc.SelectedItem as ListOrganizeEntities.OrganizeData).listOrganizeDetailId;
                }
                // LoadListSaffInCalendarWork();

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.List_Ep_not_in_Calendar_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                string applyMonth = DateTime.Parse(AppLy_Month).ToString("yyyy-MM");
                var  jsonObject =new { apply_month = applyMonth, organizeDetailId = listOrganizeDetailIds };
                string json = JsonConvert.SerializeObject(jsonObject);
                var content = new StringContent(json, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var resContent = await response.Content.ReadAsStringAsync();
                EmployeeNotInCycleEntites.Root result = JsonConvert.DeserializeObject<EmployeeNotInCycleEntites.Root>(resContent);


                lstAllSaff = result.data.items;
                lsvListSaff.ItemsSource = lstAllSaff;



                //if (listAdd.Any(add => add.ep_id == item.ep_id))
                //{
                //    //lstAllSaff = lstAllSaff1 = result.data.items;
                //    lstAllSaff1.RemoveAll(item1 => item1.ep_id == item.ep_id);
                //    lstAllSaff = lstAllSaff1;
                //}
                //else
                //{
                //    lstAllSaff = lstAllSaff1 ;

                //}
                if (lstAllSaff != null)
                {
                    foreach (var item in lstAllSaff)
                    {
                        //if (item.ep_image == "")
                        //{
                        //    item.ep_image = "https://tinhluong.timviec365.vn/img/add.png";
                        //}
                        //else
                        //{
                        //    item.ep_image = "https://chamcong.24hpay.vn/upload/employee/" + item.ep_image;
                        //}
                    }
                }

                //foreach(ItemAllSaff item in listAdd)
                //{
                //    if(item)
                //}
            }
            catch (System.Exception)
            {
            }


        }
        private List<ListOrganizeEntities.OrganizeData> _lstOrganizeData;
        public List<ListOrganizeEntities.OrganizeData> lstOrganizeData
        {
            get { return _lstOrganizeData; }
            set { _lstOrganizeData = value; }
        }
        public async void GetListOrganize()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.listAll_organize);

                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ListOrganizeEntities.Root result = JsonConvert.DeserializeObject<ListOrganizeEntities.Root>(responseContent);

                    if (result.data.data != null)
                    {
                        lstOrganizeData = result.data.data.Prepend(new ListOrganizeEntities.OrganizeData() { id = 0, organizeDetailName = "Tất cả" }).ToList();
                        cboTenToChuc.ItemsSource = lstOrganizeData;
                    }
                }
            }
            catch
            {

            }
        }

        private List<string> saff = new List<string>();
        private void ChonNhanVien_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {

            CheckBox cb = sender as CheckBox;
            ItemAllSaff data = (ItemAllSaff)cb.DataContext;
            saff.Remove(data.ep_id.ToString());
            bodThemNhanVienVaoLich.Background = (Brush)bc.ConvertFrom("#a9a9a9");
            // lstAllSaff1.Add(data);
            }
            catch { }
        }

        //List<ListSaff> listAdd = new List<ListSaff>();
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        List<ListSaff> listAdd = new List<ListSaff>();
        //public List<ListSaff> listAdd
        //{
        //    get { return _listAdd; }
        //    set { _listAdd = value; OnPropertyChanged(); }
        //}
        private void ChonNhanVien_Checked(object sender, RoutedEventArgs e)
        {
            try
            {

            CheckBox cb = sender as CheckBox;
                EmployeeNotInCycleEntites.Item data = (EmployeeNotInCycleEntites.Item)cb.DataContext;
            saff.Add(data.ep_id.ToString());
            bodThemNhanVienVaoLich.Background = (Brush)bc.ConvertFrom("#4C5BD4");

            }
            catch { }
            //lsvListSaff.ItemsSource = lstAllSaff1;
        }

        private void tbSearchSaff_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                lstAllSaff1 = new List<EmployeeNotInCycleEntites.Item>();
                foreach (var str in lstAllSaff)
                {
                    if (str.ep_name.ToLower().RemoveUnicode().Contains(tbSearchSaff.Text.ToLower().RemoveUnicode()))
                    {
                        lstAllSaff1.Add(str);

                    }
                }
                lsvListSaff.ItemsSource = lstAllSaff1;
                if (tbSearchSaff.Text == "")
                {
                    lsvListSaff.ItemsSource = lstAllSaff;
                }
            }
            catch
            {

            }

        }
        #endregion
        private void ExitCreateSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            ucCaiDatLichLamViec ucC = new ucCaiDatLichLamViec(Main, 0);
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            ucCaiDatLichLamViec ucC = new ucCaiDatLichLamViec(Main, 0);

        }

        private void bodButonAddFileSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemFileNhanVien());
            this.Visibility = Visibility.Collapsed;
        }

        private void bodExitCreateSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        public string Arr_Id_Ep;
        string formatMonth;
        private void RemoveStaff()
        {
            //foreach (var item in listAdd)
            //{
            //    lstAllSaff1.Remove(item);
            //}
            //lsvListSaff.ItemsSource = lstAllSaff1;
        }
        private async void ThemNhanVienVaoLich(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.Add_Saff_In_CalendarWork_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                int demSaff = 0;
                foreach (var item in saff)
                {
                    if (demSaff == saff.Count - 1)
                    {
                        Arr_Id_Ep += item.ToString();
                    }
                    else
                    {
                        Arr_Id_Ep += item.ToString() + ",";
                    }

                    demSaff++;
                }
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Arr_Id_Ep), "list_id");
                content.Add(new StringContent(Cy_Id.ToString()), "cy_id");
                string[] past = AppLy_Month.Split('/');

                if (past != null && past.Length == 2)
                {
                    month = past[0];
                    year = past[1];
                    formatMonth = $"{year}-{month}";
                }
                content.Add(new StringContent(formatMonth), "curMonth");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responsContent = await response.Content.ReadAsStringAsync();

                RootAddSaffInCalendarWork api = JsonConvert.DeserializeObject<RootAddSaffInCalendarWork>(responsContent);

                if (responsContent != null)
                {
                    // RemoveStaff();
                    this.Visibility = Visibility.Collapsed;
                    month = ucSetting.txbSelectMonth.Text.Split()[1];
                    year = ucSetting.txbSelectYear.Text.Split()[1];
                    ucSetting.LoadCalendarWorkStart(month, year);
                }
            }
            catch (Exception)
            { }
        }

        private void chonphongban(object sender, MouseButtonEventArgs e)
        {
            if (borDep.Visibility == Visibility.Collapsed)
            {
                borDep.Visibility = Visibility.Visible;
            }
            else
            {
                borDep.Visibility = Visibility.Collapsed;
            }
        }

        private void selected_ban(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Item_PhongBan d = (sender as Border).DataContext as Item_PhongBan;
                if (d != null)
                {
                    txtPhongBan.Text = d.dep_name;
                    borDep.Visibility = Visibility.Collapsed;

                    lstAllSaff1 = new List<EmployeeNotInCycleEntites.Item>();
                    foreach (var str in lstAllSaff)
                    {
                        if (str.dep_name.ToLower().RemoveUnicode() == (txtPhongBan.Text.ToLower().RemoveUnicode()))
                        {
                            lstAllSaff1.Add(str);

                        }
                    }

                    lsvListSaff.ItemsSource = lstAllSaff1;

                    if (txtPhongBan.Text == "Tất cả phòng ban")
                    {
                        lsvListSaff.ItemsSource = lstAllSaff;
                        //lsvListSaff.ItemsSource = lstAllSaff1;
                    }
                    lsvListSaff.Items.Refresh();
                }
            }
            catch { }


        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borDep.Visibility = Visibility.Collapsed;
        }
        bool isChecked = true;
        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T typedChild && (child as FrameworkElement)?.Name == childName)
                {
                    return typedChild;
                }
                else
                {
                    T childOfChild = FindChild<T>(child, childName);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        private List<T> FindVisualChildren<T>(DependencyObject parent, string name) where T : FrameworkElement
        {
            List<T> children = new List<T>();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T frameworkElement && (frameworkElement.Name == name || string.IsNullOrEmpty(name)))
                {
                    children.Add(frameworkElement);
                }

                children.AddRange(FindVisualChildren<T>(child, name));
            }

            return children;
        }
        private void ChonTatCaNV(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var item in lstAllSaff)
                {
                    item.ischecked = true;
                    saff.Add(item.ep_id.ToString());
                    bodThemNhanVienVaoLich.Background = (Brush)bc.ConvertFrom("#4C5BD4");
                }
                // lstAllSaff = null;
                lsvListSaff.Items.Refresh();
            }
            catch { }
            //var borders = FindVisualChildren<Border>(lsvListSaff, "bodChonNhanVien");

            //foreach (var border in borders)
            //{
            //    foreach (var child in FindVisualChildren<CheckBox>(border, ""))
            //    {
            //        child.IsChecked = true;
            //    }
            //}

        }

        private void HuyChonTatCa(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var item in lstAllSaff)
                {
                    item.ischecked = false;
                    saff.Remove(item.ep_id.ToString());
                    bodThemNhanVienVaoLich.Background = (Brush)bc.ConvertFrom("#a9a9a9");
                }
                lsvListSaff.Items.Refresh();
            }
            catch { }
            
        }

        private void cboTenToChuc_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {


            if (e.Key == Key.Back)
            {
                cboTenToChuc.SelectedIndex = -1;
                string textSearch = cboTenToChuc.Text;
                cboTenToChuc.Items.Refresh();
                cboTenToChuc.ItemsSource = lstOrganizeData.Where(t => t.organizeDetailName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
            } catch { } 
        }

        private void cboTenToChuc_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {

            cboTenToChuc.SelectedIndex = -1;
            string textSearch = cboTenToChuc.Text + e.Text;
            cboTenToChuc.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cboTenToChuc.Text = "";
                cboTenToChuc.Items.Refresh();
                cboTenToChuc.ItemsSource = lstOrganizeData;
                cboTenToChuc.SelectedIndex = -1;
            }
            else
            {
                cboTenToChuc.ItemsSource = "";
                cboTenToChuc.Items.Refresh();
                cboTenToChuc.ItemsSource = lstOrganizeData.Where(t => t.organizeDetailName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }

            }
            catch { }
        }

        private void cboTenToChuc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadListSaff();
        }
    }
}
