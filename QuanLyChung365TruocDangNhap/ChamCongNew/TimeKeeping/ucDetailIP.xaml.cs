using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.CaiDatDeX.OOP;
using QuanLyChung365TruocDangNhap.ChamCongNew.CaiDatDeX.ThongBao;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.KindOfDon.LichLamViec;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.XuatCong;
//using DocumentFormat.OpenXml.Drawing;
//using DocumentFormat.OpenXml.Drawing.Charts;
//using DocumentFormat.OpenXml.Office2010.Excel;
//using MathNet.Numerics.LinearAlgebra.Factorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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

using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi.API_List_Detail;


namespace QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping
{
    /// <summary>
    /// Interaction logic for ucDetailIP.xaml
    /// </summary>
    public partial class ucDetailIP : UserControl, INotifyPropertyChanged
    {
        MainWindow Main;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private List<API_FilterComp.ListPo> _listAddPo = new List<API_FilterComp.ListPo>();
        public List<API_FilterComp.ListPo> listAddPo
        {
            get { return _listAddPo; }
            set { _listAddPo = value; OnPropertyChanged(); }
        }
        private List<API_FilterComp.ListOrg> _listAddOrg = new List<API_FilterComp.ListOrg>();
        public List<API_FilterComp.ListOrg> listAddOrg
        {
            get { return _listAddOrg; }
            set { _listAddOrg = value; OnPropertyChanged(); }
        }

        private List<API_FilterComp.ListUser> _listAddUser = new List<API_FilterComp.ListUser>();
        public List<API_FilterComp.ListUser> listAddUser
        {
            get { return _listAddUser; }
            set { _listAddUser = value; OnPropertyChanged(); }
        }
        private List<API_FilterComp.ListShift> _listAddShift = new List<API_FilterComp.ListShift>();
        public List<API_FilterComp.ListShift> listAddShift
        {
            get { return _listAddShift; }
            set { _listAddShift = value; OnPropertyChanged(); }
        }

        private List<API_FilterComp.ListLoc> _listAddLoc = new List<API_FilterComp.ListLoc>();
        public List<API_FilterComp.ListLoc> listAddLoc
        {
            get { return _listAddLoc; }
            set { _listAddLoc = value; OnPropertyChanged(); }
        }

        private List<API_FilterComp.ListWifi> _listAddWifi = new List<API_FilterComp.ListWifi>();
        public List<API_FilterComp.ListWifi> listAddWifi
        {
            get { return _listAddWifi; }
            set { _listAddWifi = value; OnPropertyChanged(); }
        }
        private List<API_FilterComp.ListLoc> _listAddThietBi = new List<API_FilterComp.ListLoc>();
        public List<API_FilterComp.ListLoc> listAddThietBi
        {
            get { return _listAddThietBi; }
            set { _listAddThietBi = value; OnPropertyChanged(); }
        }
        public class ListShift
        {
            public int id { get; set; }
            public int type_shift { get; set; }
        }
        //List<ListPo> listAddPo = new List<ListPo>();
        //List<ListUser> listAddUser = new List<ListUser>();
        ///List<ListShift> listAddShift = new List<ListShift>();
        //List<ListWifi> listAddWifi = new List<ListWifi>();
        public ucDetailIP(MainWindow main)
        {

            InitializeComponent();
            this.DataContext = this;
            Main = main;
            getList();
            getFilterComp();
        }

        private async void getList()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/settingTImesheet/list");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    int stt = 1;
                    API_List_Detail.List_Detail api = JsonConvert.DeserializeObject<API_List_Detail.List_Detail>(responseContent);
                    if (api.data.data != null)
                    {
                        var list = api.data.data;
                        var fullList = (from item in list
                                        select new ListAll()
                                        {
                                            //detail = item.detail,
                                            stt = stt++,
                                            list_org = item.list_org,
                                            displayDp = ((item.list_org.Count == 0) ? "Tất cả" : "Xem thêm"),
                                            list_pos = item.list_pos,
                                            displayDp1 = ((item.list_pos.Count == 0) ? "Tất cả" : "Xem thêm"),
                                            detail = item.detail,
                                            start = item.detail.start_time?.ToString("dd/MM/yyyy"),
                                            end = item.detail.end_time?.ToString("dd/MM/yyyy"),

                                            list_emps = item.list_emps,
                                            displayDp2 = ((item.list_emps.Count == 0) ? "Tất cả" : "Xem thêm"),
                                            list_loc = item.list_loc,
                                            displayDp3 = ((item.list_loc.Count == 0) ? "Tất cả" : "Xem thêm"),
                                            list_shifts = item.list_shifts,
                                            displayDp6 = ((item.list_shifts.Count == 0) ? "Tất cả" : "Xem thêm"),
                                            list_wifi = item.list_wifi,
                                            displayDp7 = ((item.list_wifi.Count == 0) ? "Tất cả" : "Xem thêm"),
                                            //displayDp1 = ((item.list_pos.Count == 0) ? "Tất cả" : "Xem thêm"),
                                        }).ToList();
                        dgv.ItemsSource = fullList;
                    }
                }

            }
            catch (Exception ex)
            {
                // MessageBox.Show("loi load list " + ex.Message);
            }
        }

        List<API_FilterComp.ListOrg> listAllOrg = new List<API_FilterComp.ListOrg> { };
        List<API_FilterComp.ListPo> listAllPo = new List<API_FilterComp.ListPo> { };
        List<API_FilterComp.ListShift> listAllShift = new List<API_FilterComp.ListShift> { };
        List<API_FilterComp.ListWifi> listAllWifi = new List<API_FilterComp.ListWifi> { };
        List<API_FilterComp.ListUser> listAllUser = new List<API_FilterComp.ListUser> { };
        List<API_FilterComp.ListLoc> listAllLoc = new List<API_FilterComp.ListLoc> { };
        private async void getFilterComp()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/timekeeping/filterComp");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_FilterComp.Filter_Comp api = JsonConvert.DeserializeObject<API_FilterComp.Filter_Comp>(responseContent);
                    listAllOrg = api.data.listOrg;
                    lsvToChuc.ItemsSource = listAllOrg;
                    lsvToChuc21.ItemsSource = listAllOrg;
                    listAllPo = api.data.listPos;
                    lsvChucVu.ItemsSource = listAllPo;
                    lsvChucVu1.ItemsSource = listAllPo;
                    listAllUser = api.data.listUsers;
                    lsvTen.ItemsSource = listAllUser;
                    lsvTen21.ItemsSource = listAllUser;

                    listAllShift = api.data.listShifts;
                    foreach (var item in listAllShift)
                    {
                        if (item.type == 1)
                        {
                            item.type_name = "Ca vào";
                        }
                        else if (item.type == 2)
                        {
                            item.type_name = "Ca ra";
                        }
                    }
                    lsvCa.ItemsSource = listAllShift;
                    lsvCa21.ItemsSource = listAllShift;
                    listAllWifi = api.data.listWifi;
                    lsvWifi.ItemsSource = listAllWifi;
                    lsvWifi21.ItemsSource = listAllWifi;

                    listAllLoc = api.data.listLoc;
                    lsvLoc.ItemsSource = listAllLoc;
                    //lsvLoc21.ItemsSource = listAllLoc;

                    listAllLoc = api.data.listLoc;
                    lsvThietBi.ItemsSource = listAllLoc;
                }

            }
            catch (Exception ex)
            {
                // MessageBox.Show("loi load list " + ex.Message);
            }
        }

        private void borToChuc1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            double width = (sender as System.Windows.Controls.Border).ActualWidth;
            double margi = 20 + width;
            borToChuc.Margin = new System.Windows.Thickness(margi, 62, 0, 0);
            if (borToChuc.Visibility == Visibility.Collapsed)
            {
                borToChuc.Visibility = Visibility.Visible;
            }
            else
            {
                borToChuc.Visibility = Visibility.Collapsed;
            }
        }

        private void lsvToChuc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        string id_ToChuc = "";
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            API_FilterComp.ListOrg d = (sender as Border).DataContext as API_FilterComp.ListOrg;
            if (d != null)
            {
                textNhapTenToChuc1.Text = d.organizeDetailName;
                id_ToChuc = d.id.ToString();
                textNhapTenToChuc.Text = "";
            }
            borToChuc.Visibility = Visibility.Collapsed;
        }
        double heightChucVu;
        double widthChucVu;
        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            widthChucVu = (sender as System.Windows.Controls.Grid).ActualWidth;
            double margi = 10 + widthChucVu;
            if (!double.IsNaN(heightChucVu) || !double.IsInfinity(heightChucVu))
            {
                heightChucVu = (sender as System.Windows.Controls.Grid).ActualHeight;


            }
            else
            {
                heightChucVu = 38;

            }

            double margi1 = 25 + heightChucVu;
            borChucVu.Margin = new System.Windows.Thickness(0, margi1, margi, 0);
            if (borChucVu.Visibility == Visibility.Collapsed)
            {
                borChucVu.Visibility = Visibility.Visible;
            }
            else
            {
                borChucVu.Visibility = Visibility.Collapsed;
            }
            textNhapViTri.Focus();
        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            API_FilterComp.ListPo d = (sender as Border).DataContext as API_FilterComp.ListPo;
            if (d != null)
            {
                textNhapViTri.Text = d.positionName;
                borChucVu.Visibility = Visibility.Collapsed;
            }
        }
        List<API_FilterComp.ListPo> listAddpo = new List<API_FilterComp.ListPo>();
        private void lsvChucVua1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lsvChucVu.SelectedItem != null)
                {
                    //string selectedPo = ((ListPo)lsvChucVu.SelectedItem).positionName;

                    string selected = ((API_FilterComp.ListPo)lsvChucVu.SelectedItem).positionName;
                    if (!listAddPo.Any(item => item.positionName == selected))
                    {
                        API_FilterComp.ListPo listPo = new API_FilterComp.ListPo()
                        {
                            positionName = ((API_FilterComp.ListPo)lsvChucVu.SelectedItem).positionName,
                            id = (int)((API_FilterComp.ListPo)lsvChucVu.SelectedItem).id,

                        };
                        listAddPo.Add(listPo);
                        listAddPo = listAddPo.ToList();
                    }
                    lsvChucVuAdd.Visibility = Visibility.Visible;
                    textNhapViTri.Focus();
                    textNhapViTri1.Text = "";
                    textNhapViTri.Text = "";
                    lsvChucVuAdd.ItemsSource = listAddPo;
                    lsvChucVuAdd.Items.Refresh();
                    borChucVu.Visibility = Visibility.Collapsed;


                }
            }
            catch (Exception ex)
            {

            }

        }
        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borChucVu.Visibility = Visibility.Collapsed;
        }

        private void PhongBan_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                var textBlock = sender as Grid;
                lsvShowDetaiPhongBan.ItemsSource = textBlock.DataContext as List<API_List_Detail.ListOrg>;

                popUpViewDetailPhongBan.PlacementTarget = textBlock;
                popUpViewDetailPhongBan.Placement = System.Windows.Controls.Primitives.PlacementMode.Center;
                popUpViewDetailPhongBan.VerticalOffset = -(textBlock.ActualHeight + popUpViewDetailPhongBan.Child.DesiredSize.Height) / 2;
                popUpViewDetailPhongBan.IsOpen = true;
            }
            catch { }

        }

        private void PhongBan_MouseLeave(object sender, MouseEventArgs e)
        {
            popUpViewDetailPhongBan.IsOpen = false;
        }

        private void CaLv_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                var textBlock = sender as Grid;
                lsvShowDetaiCaLV.ItemsSource = textBlock.DataContext as List<API_List_Detail.ListShift>;
                foreach (var item in lsvShowDetaiCaLV.Items)
                {
                    if (item is API_List_Detail.ListShift dx)
                    {

                        if (dx.type_shift == 1)
                        {
                            dx.type_name = "Ca vào";
                        }
                        else if (dx.type_shift == 2)
                        {
                            dx.type_name = "Ca ra";
                        }
                    }
                }
                popUpViewDetailCaLv.PlacementTarget = textBlock;
                popUpViewDetailCaLv.Placement = System.Windows.Controls.Primitives.PlacementMode.Center;
                popUpViewDetailCaLv.VerticalOffset = -(textBlock.ActualHeight + popUpViewDetailCaLv.Child.DesiredSize.Height) / 2;
                popUpViewDetailCaLv.IsOpen = true;
            }
            catch { }
        }

        private void CaLv_Mouselef(object sender, MouseEventArgs e)
        {
            popUpViewDetailCaLv.IsOpen = false;
        }

        private void Location_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                var textBlock = sender as Grid;
                lsvShowDetaiLocation.ItemsSource = textBlock.DataContext as List<API_List_Detail.ListLoc>;
                popUpViewDetailLocation.PlacementTarget = textBlock;
                popUpViewDetailLocation.Placement = System.Windows.Controls.Primitives.PlacementMode.Center;
                popUpViewDetailLocation.VerticalOffset = -(textBlock.ActualHeight + popUpViewDetailLocation.Child.DesiredSize.Height) / 2;
                popUpViewDetailLocation.IsOpen = true;
            }
            catch { }
        }

        private void Location_Mouselef(object sender, MouseEventArgs e)
        {
            popUpViewDetailLocation.IsOpen = false;
        }
        private void ChucVu_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                var textBlock = sender as Grid;
                lsvShowDetaiChucVu.ItemsSource = textBlock.DataContext as List<API_List_Detail.ListPo>;

                popUpViewDetailChucVu.PlacementTarget = textBlock;
                popUpViewDetailChucVu.Placement = System.Windows.Controls.Primitives.PlacementMode.Center;
                popUpViewDetailChucVu.VerticalOffset = -(textBlock.ActualHeight + popUpViewDetailChucVu.Child.DesiredSize.Height) / 2;
                popUpViewDetailChucVu.IsOpen = true;
            }
            catch { }
        }

        private void ChucVu_mouseLeft(object sender, MouseEventArgs e)
        {
            popUpViewDetailChucVu.IsOpen = false;
        }

        private void Name_enter(object sender, MouseEventArgs e)
        {
            try
            {
                var textBlock = sender as Grid;
                lsvShowDetaiName.ItemsSource = textBlock.DataContext as List<API_List_Detail.ListEmp>;

                popUpViewDetailName.PlacementTarget = textBlock;
                popUpViewDetailName.Placement = System.Windows.Controls.Primitives.PlacementMode.Center;
                popUpViewDetailName.VerticalOffset = -(textBlock.ActualHeight + popUpViewDetailName.Child.DesiredSize.Height) / 2;
                popUpViewDetailName.IsOpen = true;
            }
            catch { }
        }

        private void Name_left(object sender, MouseEventArgs e)
        {
            popUpViewDetailName.IsOpen = false;
        }

        private void wifi_enter(object sender, MouseEventArgs e)
        {
            try
            {
                var textBlock = sender as Grid;
                lsvShowDetaiWifi.ItemsSource = textBlock.DataContext as List<API_List_Detail.ListWifi>;

                popUpViewDetailWifi.PlacementTarget = textBlock;
                popUpViewDetailWifi.Placement = System.Windows.Controls.Primitives.PlacementMode.Center;
                popUpViewDetailWifi.VerticalOffset = -(textBlock.ActualHeight + popUpViewDetailWifi.Child.DesiredSize.Height) / 2;
                popUpViewDetailWifi.IsOpen = true;
            }
            catch { }
        }

        private void wifi_left(object sender, MouseEventArgs e)
        {
            popUpViewDetailWifi.IsOpen = false;
        }

        private void Rectangle_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            borTen.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            borToChuc.Visibility = Visibility.Collapsed;
        }
        // Hàm giúp tìm kiếm đối tượng con trong VisualTree
        #region
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
        //// Hàm giúp tìm cha của một đối tượng trong VisualTree
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
        #endregion
        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int id_xoa = 0;
            API_List_Detail.ListAll d = ((API_List_Detail.ListAll)dgv.SelectedItem);
            if (d != null)
            {
                id_xoa = (int)d.detail.setting_id;
            }
            //Xoa uc = new Xoa(Main, id_xoa);
            //Main.dopBody.Children.Clear();
            //object Content = uc.Content;
            //uc.Content = null;
            //Main.grShowPopup.Children.Add(Content as UIElement);
        }

        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            double width = (sender as System.Windows.Controls.Grid).ActualWidth;
            double margi = 10 + width;
            double height = (sender as System.Windows.Controls.Grid).ActualHeight;
            double margi1 = 25 + height;
            borTen.Margin = new System.Windows.Thickness(0, margi1, 0, 0);
            if (borTen.Visibility == Visibility.Visible)
            {
                borTen.Visibility = Visibility.Collapsed;
            }
            else
            {
                borTen.Visibility = Visibility.Visible;
            }
            textNhapViTri.Focus();

        }



        private void textNhapTenToChuc_TextChanged(object sender, TextChangedEventArgs e)
        {
            //textNhapTenToChuc.Text = "";
            borToChuc.Visibility = Visibility.Visible;
            List<API_FilterComp.ListOrg> listOrgTimKiem = new List<API_FilterComp.ListOrg>();
            string searchText = textNhapTenToChuc.Text.ToString().ToLower().RemoveUnicode();
            foreach (var str in listAllOrg)
            {
                if (str.organizeDetailName.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listOrgTimKiem.Any(item => item.organizeDetailName.Equals(str.organizeDetailName, StringComparison.OrdinalIgnoreCase)))
                    {
                        listOrgTimKiem.Add(str);
                    }

                }
            }
            lsvToChuc.ItemsSource = null;
            lsvToChuc.ItemsSource = listOrgTimKiem;

            //listDeXTimKiem.Clear();
            if (textNhapTenToChuc.Text == "")
            {
                lsvToChuc.ItemsSource = listAllOrg;
            }
        }

        private void textNhapViTri_TextChanged(object sender, TextChangedEventArgs e)
        {
            borChucVu.Visibility = Visibility.Visible;
            List<API_FilterComp.ListPo> listPoTimKiem = new List<API_FilterComp.ListPo>();
            string searchText = textNhapViTri.Text.ToString().ToLower().RemoveUnicode();
            foreach (var str in listAllPo)
            {
                if (str.positionName.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listPoTimKiem.Any(item => item.positionName.Equals(str.positionName, StringComparison.OrdinalIgnoreCase)))

                    {
                        listPoTimKiem.Add(str);
                    }

                }
            }
            lsvChucVu.ItemsSource = null;
            lsvChucVu.ItemsSource = listPoTimKiem;

            //listDeXTimKiem.Clear();
            if (textNhapViTri.Text == "")
            {
                lsvChucVu.ItemsSource = listAllPo;
            }
        }
        private void xoaChucVu_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.DarkGray);
            ((Border)sender).Background = redBrush;
        }
        Brush brush = new SolidColorBrush();
        private void xoaChucVu_MouseLeave(object sender, MouseEventArgs e)
        {
            borChucVu.Visibility = Visibility.Collapsed;
            textNhapViTri.Focus();
            //xoaAnh.bac
            SolidColorBrush grayBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 220, 220)); // Màu #dcdcdc
            ((Border)sender).Background = grayBrush;
            if (listAddPo.Count == 0)
            {
                lsvChucVuAdd.Visibility = Visibility.Collapsed;
                textNhapViTri.Focus();
                textNhapViTri1.Text = "Tìm theo vị trí";
            }

        }
        bool shouldProcessEvent = true;
        private void xoaChucVu_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            API_FilterComp.ListPo index = (API_FilterComp.ListPo)lsvChucVuAdd.SelectedItem;
            if (index != null)
            {
                listAddPo.Remove(index);
                lsvChucVuAdd.ClearValue(ItemsControl.ItemsSourceProperty);
                lsvChucVuAdd.ItemsSource = listAddPo;
                shouldProcessEvent = false;
            }
            shouldProcessEvent = true;
            if (listAddPo.Count == 0)
            {
                TimchucVu.Visibility = Visibility.Visible;
                lsvChucVuAdd.Visibility = Visibility.Collapsed;
                //textChonNgD.Text = "";

            }
        }
        private void lsvTen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lsvTen.SelectedItem != null)
                {
                    //string selectedPo = ((ListPo)lsvChucVu.SelectedItem).positionName;

                    string selected = ((API_FilterComp.ListUser)lsvTen.SelectedItem).userName;
                    int selected_id = (int)((API_FilterComp.ListUser)lsvTen.SelectedItem).idQLC;
                    if (!listAddUser.Any(item => item.userName == selected && item.idQLC == selected_id))
                    {
                        API_FilterComp.ListUser listPo = new API_FilterComp.ListUser()
                        {
                            userName = ((API_FilterComp.ListUser)lsvTen.SelectedItem).userName,
                            idQLC = ((API_FilterComp.ListUser)lsvTen.SelectedItem).idQLC
                        };
                        listAddUser.Add(listPo);
                        listAddUser = listAddUser.ToList();
                    }
                    lsvNameAdd.Visibility = Visibility.Visible;
                    textNhapHoVaTen.Focus();
                    textNhapHoVaTen1.Text = "";
                    textNhapHoVaTen.Text = "";
                    lsvNameAdd.ItemsSource = listAddUser;
                    lsvNameAdd.Items.Refresh();
                    borTen.Visibility = Visibility.Collapsed;


                }
            }
            catch (Exception ex)
            {

            }
        }

        private void textNhapHoVaTen_TextChanged(object sender, TextChangedEventArgs e)
        {
            borTen.Visibility = Visibility.Visible;
            List<API_FilterComp.ListUser> listNameTimKiem = new List<API_FilterComp.ListUser>();
            string searchText = textNhapHoVaTen.Text.ToString().ToLower().RemoveUnicode();
            foreach (var str in listAllUser)
            {
                if (str.userName.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listNameTimKiem.Any(item => item.userName.Equals(str.userName, StringComparison.OrdinalIgnoreCase)))

                    {
                        listNameTimKiem.Add(str);
                    }

                }
            }
            lsvTen.ItemsSource = null;
            lsvTen.ItemsSource = listNameTimKiem;

            //listDeXTimKiem.Clear();
            if (textNhapHoVaTen.Text == "")
            {
                lsvTen.ItemsSource = listAllUser;
            }
        }

        private void xoaName_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.DarkGray);
            ((Border)sender).Background = redBrush;
        }

        private void xoaName_MouseLeave(object sender, MouseEventArgs e)
        {
            borTen.Visibility = Visibility.Collapsed;
            textNhapHoVaTen.Focus();
            //xoaAnh.bac
            SolidColorBrush grayBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 220, 220)); // Màu #dcdcdc
            ((Border)sender).Background = grayBrush;
            if (listAddUser.Count == 0)
            {
                lsvNameAdd.Visibility = Visibility.Collapsed;
                textNhapHoVaTen.Focus();
                textNhapHoVaTen1.Text = "Tìm theo id nhân viên";
            }
        }
        bool shouldProcessEvent1 = true;
        private void xoaName_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            API_FilterComp.ListUser index = (API_FilterComp.ListUser)lsvNameAdd.SelectedItem;
            if (index != null)
            {
                listAddUser.Remove(index);
                lsvNameAdd.ClearValue(ItemsControl.ItemsSourceProperty);
                lsvNameAdd.ItemsSource = listAddUser;
                shouldProcessEvent1 = false;
            }
            shouldProcessEvent1 = true;
            if (listAddUser.Count == 0)
            {
                gridName.Visibility = Visibility.Visible;
                lsvNameAdd.Visibility = Visibility.Collapsed;
                //textChonNgD.Text = "";

            }
        }

        private void Rectangle_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            borCa.Visibility = Visibility.Collapsed;
        }

        private void lsvCa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lsvCa.SelectedItem != null)
                {
                    //string selectedPo = ((ListPo)lsvChucVu.SelectedItem).positionName;

                    string selected = ((API_FilterComp.ListShift)lsvCa.SelectedItem).shift_name;
                    int selected_id = (int)((API_FilterComp.ListShift)lsvCa.SelectedItem).type;
                    if (!listAddShift.Any(item => item.shift_name == selected && item.type == selected_id))
                    {
                        API_FilterComp.ListShift listPo = new API_FilterComp.ListShift()
                        {
                            shift_id = ((API_FilterComp.ListShift)lsvCa.SelectedItem).shift_id,
                            shift_name = ((API_FilterComp.ListShift)lsvCa.SelectedItem).shift_name,
                            type = ((API_FilterComp.ListShift)lsvCa.SelectedItem).type
                        };
                        listAddShift.Add(listPo);
                        listAddShift = listAddShift.ToList();
                        foreach (var item in listAddShift)
                        {
                            if (item.type == 1)
                            {
                                item.type_name = "Ca vào";
                            }
                            else if (item.type == 2)
                            {
                                item.type_name = "Ca ra";
                            }
                        }
                    }
                    lsvCaAdd.Visibility = Visibility.Visible;
                    textNhapCa.Focus();
                    textNhapCa1.Text = "";
                    textNhapCa.Text = "";
                    lsvCaAdd.ItemsSource = listAddShift;
                    lsvCaAdd.Items.Refresh();
                    borCa.Visibility = Visibility.Collapsed;


                }
            }
            catch (Exception ex)
            {

            }
        }

        private void textNhapCa_TextChanged(object sender, TextChangedEventArgs e)
        {

            borCa.Visibility = Visibility.Visible;
            List<API_FilterComp.ListShift> listPoTimKiem = new List<API_FilterComp.ListShift>();
            string searchText = textNhapCa.Text.ToString().ToLower().RemoveUnicode();

            foreach (var str in listAllShift)
            {
                if (str.shift_name.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listPoTimKiem.Any(item => item.shift_name.Equals(str.shift_name, StringComparison.OrdinalIgnoreCase)))

                    {
                        listPoTimKiem.Add(str);
                    }

                }
            }
            lsvCa.ItemsSource = null;
            lsvCa.ItemsSource = listPoTimKiem;

            //listDeXTimKiem.Clear();
            if (textNhapCa.Text == "")
            {
                lsvCa.ItemsSource = listAllShift;
            }
        }

        private void Grid_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            double width = (sender as System.Windows.Controls.Grid).ActualWidth;
            double marginRight = 10 + width;

            double gridApDungRow0Height = gridApDungRow0.ActualHeight;
            double heightCa = (sender as System.Windows.Controls.Grid).ActualHeight;
            double margiTop = gridApDungRow0Height + 42 + heightCa;
            borCa.Margin = new System.Windows.Thickness(0, margiTop, marginRight, 0);
            if (borCa.Visibility == Visibility.Collapsed)
            {
                borCa.Visibility = Visibility.Visible;
            }
            else
            {
                borCa.Visibility = Visibility.Collapsed;
            }
            textNhapCa.Focus();
        }

        private void Ca_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.DarkGray);
            ((Border)sender).Background = redBrush;
        }

        private void Ca_MouseLeave(object sender, MouseEventArgs e)
        {
            borCa.Visibility = Visibility.Collapsed;
            textNhapCa.Focus();
            //xoaAnh.bac
            SolidColorBrush grayBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 220, 220)); // Màu #dcdcdc
            ((Border)sender).Background = grayBrush;
            if (listAddShift.Count == 0)
            {
                lsvCaAdd.Visibility = Visibility.Collapsed;
                textNhapCa.Focus();
                textNhapCa1.Text = "Tìm theo ca làm việc";
            }
        }
        bool shouldProcessEvent2 = true;
        private void Ca_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            API_FilterComp.ListShift index = (API_FilterComp.ListShift)lsvCaAdd.SelectedItem;
            if (index != null)
            {
                listAddShift.Remove(index);
                lsvCaAdd.ClearValue(ItemsControl.ItemsSourceProperty);
                lsvCaAdd.ItemsSource = listAddShift;
                shouldProcessEvent2 = false;
            }
            shouldProcessEvent2 = true;
            if (listAddShift.Count == 0)
            {
                gridca.Visibility = Visibility.Visible;
                lsvCaAdd.Visibility = Visibility.Collapsed;
                //textChonNgD.Text = "";

            }
        }

        private void Rectangle_ClosePopupWifi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borWifi.Visibility = Visibility.Collapsed;
        }

        private void Grid_OpenPopupWifi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            double width = (sender as System.Windows.Controls.Grid).ActualWidth;
            double marginRight = 30 + width * 3;

            double gridApDungRow0Height = gridApDungRow0.ActualHeight;
            double gridApDungRow1Height = gridApDungRow1.ActualHeight;
            double heightCa = (sender as System.Windows.Controls.Grid).ActualHeight;
            double margiTop = gridApDungRow0Height + gridApDungRow1Height + 62 + heightCa;
            borWifi.Margin = new System.Windows.Thickness(0, margiTop, marginRight, 0);
            if (borWifi.Visibility == Visibility.Collapsed)
            {
                borWifi.Visibility = Visibility.Visible;
            }
            else
            {
                borWifi.Visibility = Visibility.Collapsed;
            }
        }

        private void Wifi_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.DarkGray);
            ((Border)sender).Background = redBrush;
        }

        private void Wifi_MouseLeave(object sender, MouseEventArgs e)
        {
            borWifi.Visibility = Visibility.Collapsed;
            textNhapWifi.Focus();
            //xoaAnh.bac
            SolidColorBrush grayBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 220, 220)); // Màu #dcdcdc
            ((Border)sender).Background = grayBrush;
            if (listAddWifi.Count == 0)
            {
                lsvWifiAdd.Visibility = Visibility.Collapsed;
                textNhapWifi.Focus();
                textNhapWifi1.Text = "Tìm theo wifi";
            }
        }
        bool shouldProcessEvent3 = true;
        private void Wifi_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            API_FilterComp.ListWifi index = (API_FilterComp.ListWifi)lsvWifiAdd.SelectedItem;
            if (index != null)
            {
                listAddWifi.Remove(index);
                lsvWifiAdd.ClearValue(ItemsControl.ItemsSourceProperty);
                lsvWifiAdd.ItemsSource = listAddWifi;
                shouldProcessEvent3 = false;
            }
            shouldProcessEvent3 = true;
            if (listAddWifi.Count == 0)
            {
                gridWifi.Visibility = Visibility.Visible;
                lsvWifiAdd.Visibility = Visibility.Collapsed;
                //textChonNgD.Text = "";

            }
        }

        private void textNhapWifi_TextChanged(object sender, TextChangedEventArgs e)
        {
            borWifi.Visibility = Visibility.Visible;
            List<API_FilterComp.ListWifi> listPoTimKiem = new List<API_FilterComp.ListWifi>();
            string searchText = textNhapWifi.Text.ToString().ToLower().RemoveUnicode();
            foreach (var str in listAllWifi)
            {
                if (str.name_wifi.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listPoTimKiem.Any(item => item.name_wifi.Equals(str.name_wifi, StringComparison.OrdinalIgnoreCase)))

                    {
                        listPoTimKiem.Add(str);
                    }

                }
            }
            lsvWifi.ItemsSource = null;
            lsvWifi.ItemsSource = listPoTimKiem;

            //listDeXTimKiem.Clear();
            if (textNhapWifi.Text == "")
            {
                lsvWifi.ItemsSource = listAllWifi;
            }
        }

        private void lsvWifi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lsvWifi.SelectedItem != null)
                {
                    //string selectedPo = ((ListPo)lsvChucVu.SelectedItem).positionName;

                    string selected = ((API_FilterComp.ListWifi)lsvWifi.SelectedItem).name_wifi;
                    // int selected_id = (int)((API_FilterComp.ListShift)lsvCa.SelectedItem).type;
                    if (!listAddWifi.Any(item => item.name_wifi == selected))
                    {
                        API_FilterComp.ListWifi listPo = new API_FilterComp.ListWifi()
                        {
                            name_wifi = ((API_FilterComp.ListWifi)lsvWifi.SelectedItem).name_wifi,
                            id = (int)((API_FilterComp.ListWifi)lsvWifi.SelectedItem).id
                        };
                        listAddWifi.Add(listPo);
                        listAddWifi = listAddWifi.ToList();

                    }
                    lsvWifiAdd.Visibility = Visibility.Visible;
                    textNhapWifi.Focus();
                    textNhapWifi1.Text = "";
                    textNhapWifi.Text = "";
                    lsvWifiAdd.ItemsSource = listAddWifi;
                    lsvWifiAdd.Items.Refresh();
                    borWifi.Visibility = Visibility.Collapsed;


                }
            }
            catch (Exception ex)
            {

            }
        }





        private void Rectangle_ClosePopupLoc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borLoc.Visibility = Visibility.Collapsed;
        }

        private void Grid_OpenPopupLoc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            double gridApDungRow0Height = gridApDungRow0.ActualHeight;
            double heightCa = (sender as System.Windows.Controls.Grid).ActualHeight;
            double margiTop = gridApDungRow0Height + 42 + heightCa;
            borLoc.Margin = new System.Windows.Thickness(0, margiTop, 0, 0);
            if (borLoc.Visibility == Visibility.Collapsed)
            {
                borLoc.Visibility = Visibility.Visible;
            }
            else
            {
                borLoc.Visibility = Visibility.Collapsed;
            }
        }

        private void Loc_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.DarkGray);
            ((Border)sender).Background = redBrush;
        }

        private void Loc_MouseLeave(object sender, MouseEventArgs e)
        {
            borLoc.Visibility = Visibility.Collapsed;
            textNhapLoc.Focus();
            //xoaAnh.bac
            SolidColorBrush grayBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 220, 220)); // Màu #dcdcdc
            ((Border)sender).Background = grayBrush;
            if (listAddLoc.Count == 0)
            {
                lsvLocAdd.Visibility = Visibility.Collapsed;
                textNhapLoc.Focus();
                textNhapLoc1.Text = "Tìm theo Loc";
            }
        }
        bool shouldProcessEvent15 = true;
        private void Loc_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            API_FilterComp.ListLoc index = (API_FilterComp.ListLoc)lsvLocAdd.SelectedItem;
            if (index != null)
            {
                listAddLoc.Remove(index);
                lsvLocAdd.ClearValue(ItemsControl.ItemsSourceProperty);
                lsvLocAdd.ItemsSource = listAddLoc;
                shouldProcessEvent15 = false;
            }
            shouldProcessEvent15 = true;
            if (listAddLoc.Count == 0)
            {
                gridLoc.Visibility = Visibility.Visible;
                lsvLocAdd.Visibility = Visibility.Collapsed;
                //textChonNgD.Text = "";

            }
        }

        private void textNhapLoc_TextChanged(object sender, TextChangedEventArgs e)
        {
            borLoc.Visibility = Visibility.Visible;
            List<API_FilterComp.ListLoc> listPoTimKiem = new List<API_FilterComp.ListLoc>();
            string searchText = textNhapLoc.Text.ToString().ToLower().RemoveUnicode();
            foreach (var str in listAllLoc)
            {
                if (str.cor_location_name.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listPoTimKiem.Any(item => item.cor_location_name.Equals(str.cor_location_name, StringComparison.OrdinalIgnoreCase)))

                    {
                        listPoTimKiem.Add(str);
                    }

                }
            }
            lsvLoc.ItemsSource = null;
            lsvLoc.ItemsSource = listPoTimKiem;

            //listDeXTimKiem.Clear();
            if (textNhapLoc.Text == "")
            {
                lsvLoc.ItemsSource = listAllLoc;
            }
        }

        private void lsvLoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lsvLoc.SelectedItem != null)
                {
                    //string selectedPo = ((ListPo)lsvChucVu.SelectedItem).positionName;

                    string selected = ((API_FilterComp.ListLoc)lsvLoc.SelectedItem).cor_location_name;
                    // int selected_id = (int)((API_FilterComp.ListShift)lsvCa.SelectedItem).type;
                    if (!listAddLoc.Any(item => item.cor_location_name == selected))
                    {
                        API_FilterComp.ListLoc listPo = new API_FilterComp.ListLoc()
                        {
                            cor_location_name = ((API_FilterComp.ListLoc)lsvLoc.SelectedItem).cor_location_name,
                            cor_id = (int)((API_FilterComp.ListLoc)lsvLoc.SelectedItem).cor_id
                        };
                        listAddLoc.Add(listPo);
                        listAddLoc = listAddLoc.ToList();

                    }
                    lsvLocAdd.Visibility = Visibility.Visible;
                    textNhapLoc.Focus();
                    textNhapLoc1.Text = "";
                    textNhapLoc.Text = "";
                    lsvLocAdd.ItemsSource = listAddLoc;
                    lsvLocAdd.Items.Refresh();
                    borLoc.Visibility = Visibility.Collapsed;


                }
            }
            catch (Exception ex)
            {

            }
        }






        private void Grid_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            if (borThietBi.Visibility == Visibility.Collapsed)
            {
                borThietBi.Visibility = Visibility.Visible;
            }
            else
            {
                borThietBi.Visibility = Visibility.Collapsed;
            }
        }

        private void ThietBi_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.DarkGray);
            ((Border)sender).Background = redBrush;
        }

        private void ThietBi_MouseLeave(object sender, MouseEventArgs e)
        {
            borThietBi.Visibility = Visibility.Collapsed;
            textNhapThietBi.Focus();
            //xoaAnh.bac
            SolidColorBrush grayBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 220, 220)); // Màu #dcdcdc
            ((Border)sender).Background = grayBrush;
            if (listAddThietBi.Count == 0)
            {

                lsvThietBiAdd.Visibility = Visibility.Collapsed;
                textNhapThietBi.Focus();
                textNhapThietBi1.Text = "Tìm theo thiết bị";
            }
        }
        bool shouldProcessEvent4 = true;
        private void ThietBi_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            API_FilterComp.ListLoc index = (API_FilterComp.ListLoc)lsvThietBiAdd.SelectedItem;
            if (index != null)
            {
                listAddThietBi.Remove(index);
                lsvThietBiAdd.ClearValue(ItemsControl.ItemsSourceProperty);
                lsvThietBiAdd.ItemsSource = listAddThietBi;
                shouldProcessEvent4 = false;
            }
            shouldProcessEvent4 = true;
            if (listAddThietBi.Count == 0)
            {
                gridThietBi.Visibility = Visibility.Visible;
                lsvThietBiAdd.Visibility = Visibility.Collapsed;
                //textChonNgD.Text = "";

            }
        }

        private void Rectangle_MouseLeftButtonUp_5(object sender, MouseButtonEventArgs e)
        {
            borThietBi.Visibility = Visibility.Collapsed;
        }

        private void lsvThietBi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lsvThietBi.SelectedItem != null)
                {
                    //string selectedPo = ((ListPo)lsvChucVu.SelectedItem).positionName;

                    string selected = ((API_FilterComp.ListLoc)lsvThietBi.SelectedItem).cor_location_name;
                    // int selected_id = (int)((API_FilterComp.ListShift)lsvCa.SelectedItem).type;
                    if (!listAddThietBi.Any(item => item.cor_location_name == selected))
                    {
                        API_FilterComp.ListLoc listPo = new API_FilterComp.ListLoc()
                        {
                            cor_location_name = ((API_FilterComp.ListLoc)lsvThietBi.SelectedItem).cor_location_name
                        };
                        listAddThietBi.Add(listPo);
                        listAddThietBi = listAddThietBi.ToList();

                    }
                    lsvThietBiAdd.Visibility = Visibility.Visible;
                    textNhapThietBi.Focus();
                    textNhapThietBi1.Text = "";
                    textNhapThietBi.Text = "";
                    lsvThietBiAdd.ItemsSource = listAddWifi;
                    lsvThietBiAdd.Items.Refresh();
                    borThietBi.Visibility = Visibility.Collapsed;


                }
            }
            catch (Exception ex)
            {

            }
        }

        private void textNhapThietBi_TextChanged(object sender, TextChangedEventArgs e)
        {
            borThietBi.Visibility = Visibility.Visible;
            List<API_FilterComp.ListLoc> listPoTimKiem = new List<API_FilterComp.ListLoc>();
            string searchText = textNhapThietBi.Text.ToString().ToLower().RemoveUnicode();
            foreach (var str in listAllLoc)
            {
                if (str.cor_location_name.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listPoTimKiem.Any(item => item.cor_location_name.Equals(str.cor_location_name, StringComparison.OrdinalIgnoreCase)))

                    {
                        listPoTimKiem.Add(str);
                    }

                }
            }
            lsvThietBi.ItemsSource = null;
            lsvThietBi.ItemsSource = listPoTimKiem;

            //listDeXTimKiem.Clear();
            if (textNhapThietBi.Text == "")
            {
                lsvThietBi.ItemsSource = listAllLoc;
            }
        }

        private async void Border_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/settingTimesheet/add");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                List<int> listStringPo = new List<int>();
                foreach (var item in listAddPo)
                {
                    listStringPo.Add((int)item.id);
                }


                List<int> listStringWifi = new List<int>();
                foreach (var item in listAddWifi)
                {
                    listStringWifi.Add((int)item.id);
                }


                List<int> listStringLoc = new List<int>();
                foreach (var item in listAddLoc)
                {
                    listStringLoc.Add((int)item.cor_id);
                }

                List<int> listStringEmp = new List<int>();
                foreach (var item in listAddUser)
                {
                    listStringEmp.Add((int)item.idQLC);
                }


                DateTime ngayBatDauDate;
                DateTime ngayKetThucDate;
                string ngayBatDauFormat = "";
                string ngayKetThucFormat = "";
                if (NgayBatDau.Text != "")
                {
                    ngayBatDauDate = DateTime.Parse(NgayBatDau.Text);
                    ngayBatDauFormat = ngayBatDauDate.ToString("yyyy-MM-ddTHH:mm:ss.fff+00:00");

                }

                if (NgayKetThuc.Text != "")
                {
                    ngayKetThucDate = DateTime.Parse(NgayKetThuc.Text);
                    ngayKetThucFormat = ngayKetThucDate.ToString("yyyy-MM-ddTHH:mm:ss.fff+00:00");
                }
                else
                {

                }
                string device = ComboBox.SelectedItem.ToString();

                List<ListShift> listStringShift = (from shift in listAddShift select new ListShift { id = (int)shift.shift_id, type_shift = (int)shift.type }).ToList();
                var bodyObject = new
                {
                    setting_name = textNhapTenCaiDat.Text,
                    type_loc = "",
                    type_wifi = "",
                    list_org = id_ToChuc,
                    list_pos = listStringPo,
                    list_emps = listStringEmp,
                    start_time = ngayBatDauFormat,

                    end_time = ngayKetThucFormat,
                    list_loc = listStringLoc,
                    list_wifi = listStringWifi,
                    list_shifts = listStringShift,
                    list_device = device

                };
                string json = JsonConvert.SerializeObject(bodyObject);
                var content = new StringContent(json, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    ThemThanhCong uc = new ThemThanhCong(Main);
                    Main.grShowPopup.Children.Add(uc);
                    object Content = uc.Content;
                    uc.Content = null;
                    Main.grShowPopup.Children.Add(Content as UIElement);
                    getList();
                    dgv.Items.Refresh();
                }
            }
            catch (Exception ex)
            {

            }
        }
        int id_edit;
        private void Grid_MouseLeftButtonUp_4(object sender, MouseButtonEventArgs e)
        {
            //grChinhSua.Visibility = Visibility.Visible;

            // int id_xoa = 0;
            API_List_Detail.ListAll d = ((API_List_Detail.ListAll)dgv.SelectedItem);
            if (d != null)
            {
                id_edit = (int)d.detail.setting_id;
            }
        }
        //chuc vu
        private void ChucVu_MouseEnter1(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.DarkGray);
            ((Border)sender).Background = redBrush;
        }

        private void x11oaChucVu_MouseLeave(object sender, MouseEventArgs e)
        {
            borChucVu51.Visibility = Visibility.Collapsed;
            textNhapViTri51.Focus();
            //xoaAnh.bac
            SolidColorBrush grayBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 220, 220)); // Màu #dcdcdc
            ((Border)sender).Background = grayBrush;
            if (listAddPo.Count == 0)
            {
                lsvChucVuAdd51.Visibility = Visibility.Collapsed;
                textNhapViTri51.Focus();
                textNhapViTri151.Text = "Tìm theo vị trí";
            }
        }
        bool shouldProcessEvent6 = true;
        private void xoaC11hucVu_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            API_FilterComp.ListPo index = (API_FilterComp.ListPo)lsvChucVuAdd51.SelectedItem;
            if (index != null)
            {
                listAddPo.Remove(index);
                lsvChucVuAdd51.ClearValue(ItemsControl.ItemsSourceProperty);
                lsvChucVuAdd51.ItemsSource = listAddPo;
                shouldProcessEvent6 = false;
            }
            shouldProcessEvent6 = true;
            if (listAddPo.Count == 0)
            {
                TimchucVu51.Visibility = Visibility.Visible;
                lsvChucVuAdd51.Visibility = Visibility.Collapsed;
                //textChonNgD.Text = "";

            }
        }

        private void Grid_Mouse11LeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borChucVu51.Visibility == Visibility.Collapsed)
            {
                borChucVu51.Visibility = Visibility.Visible;
            }
            else
            {
                borChucVu51.Visibility = Visibility.Collapsed;
            }
        }
        //vi tri
        private void textNhapViT11_TextChanged(object sender, TextChangedEventArgs e)
        {
            borChucVu51.Visibility = Visibility.Visible;
            List<API_FilterComp.ListPo> listPoTimKiem = new List<API_FilterComp.ListPo>();
            string searchText = textNhapViTri.Text.ToString().ToLower().RemoveUnicode();
            foreach (var str in listAllPo)
            {
                if (str.positionName.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listPoTimKiem.Any(item => item.positionName.Equals(str.positionName, StringComparison.OrdinalIgnoreCase)))

                    {
                        listPoTimKiem.Add(str);
                    }

                }
            }
            lsvChucVu1.ItemsSource = null;
            lsvChucVu1.ItemsSource = listPoTimKiem;

            //listDeXTimKiem.Clear();
            if (textNhapViTri51.Text == "")
            {
                lsvChucVu1.ItemsSource = listAllPo;
            }
        }

        private void Grid_Mouse11LeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (borca21.Visibility == Visibility.Collapsed)
            {
                borca21.Visibility = Visibility.Visible;
            }
            else
            {
                borca21.Visibility = Visibility.Collapsed;
            }
        }

        private void Ca_MouseEqnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.DarkGray);
            ((Border)sender).Background = redBrush;
        }

        private void Ca_MdouseLeave(object sender, MouseEventArgs e)
        {
            borca21.Visibility = Visibility.Collapsed;
            textNhapCa51.Focus();
            //xoaAnh.bac
            SolidColorBrush grayBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 220, 220)); // Màu #dcdcdc
            ((Border)sender).Background = grayBrush;
            if (listAddShift.Count == 0)
            {
                lsvCaAdd51.Visibility = Visibility.Collapsed;
                textNhapCa51.Focus();
                textNhapCa151.Text = "Tìm theo ca làm việc";
            }
        }
        bool shouldProcessEven9 = true;
        private void Ca_PreviewMou11seLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            API_FilterComp.ListShift index = (API_FilterComp.ListShift)lsvCaAdd51.SelectedItem;
            if (index != null)
            {
                listAddShift.Remove(index);
                lsvCaAdd51.ClearValue(ItemsControl.ItemsSourceProperty);
                lsvCaAdd51.ItemsSource = listAddShift;
                shouldProcessEven9 = false;
            }
            shouldProcessEven9 = true;
            if (listAddShift.Count == 0)
            {
                gridca51.Visibility = Visibility.Visible;
                lsvCaAdd51.Visibility = Visibility.Collapsed;
                //textChonNgD.Text = "";

            }
        }

        private void textNhapCaq_TextChanged(object sender, TextChangedEventArgs e)
        {
            borca21.Visibility = Visibility.Visible;
            List<API_FilterComp.ListShift> listPoTimKiem = new List<API_FilterComp.ListShift>();
            string searchText = textNhapCa51.Text.ToString().ToLower().RemoveUnicode();

            foreach (var str in listAllShift)
            {
                if (str.shift_name.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listPoTimKiem.Any(item => item.shift_name.Equals(str.shift_name, StringComparison.OrdinalIgnoreCase)))

                    {
                        listPoTimKiem.Add(str);
                    }

                }
            }
            lsvCa21.ItemsSource = null;
            lsvCa21.ItemsSource = listPoTimKiem;

            //listDeXTimKiem.Clear();
            if (textNhapCa51.Text == "")
            {
                lsvCa21.ItemsSource = listAllShift;
            }
        }
        //thiet bi
        private void Grid_MouseeLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {

        }

        private void ThietBi_1MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void ThietBi_dMouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void ThietBi_Previe11wMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void textNhapThietqBi_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        //to chuc
        private void borToChuwc1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borToChuc51.Visibility == Visibility.Collapsed)
            {
                borToChuc51.Visibility = Visibility.Visible;
            }
            else
            {
                borToChuc51.Visibility = Visibility.Collapsed;

            }
        }

        private void textNhapTenToChauc_TextChanged(object sender, TextChangedEventArgs e)
        {
            borToChuc51.Visibility = Visibility.Visible;
            List<API_FilterComp.ListOrg> listOrgTimKiem = new List<API_FilterComp.ListOrg>();
            string searchText = textNhapTenToChuc51.Text.ToString().ToLower().RemoveUnicode();
            foreach (var str in listAllOrg)
            {
                if (str.organizeDetailName.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listOrgTimKiem.Any(item => item.organizeDetailName.Equals(str.organizeDetailName, StringComparison.OrdinalIgnoreCase)))
                    {
                        listOrgTimKiem.Add(str);
                    }

                }
            }
            lsvToChuc21.ItemsSource = null;
            lsvToChuc21.ItemsSource = listOrgTimKiem;

            //listDeXTimKiem.Clear();
            if (textNhapTenToChuc51.Text == "")
            {
                lsvToChuc21.ItemsSource = listAllOrg;
            }
        }
        //ho va ten
        private void Border_21MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (borTen21.Visibility == Visibility.Collapsed)
            {
                borTen21.Visibility = Visibility.Visible;
            }
            else
            {
                borTen21.Visibility = Visibility.Collapsed;
            }
        }

        private void xoaName12_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.DarkGray);
            ((Border)sender).Background = redBrush;
        }

        private void xoaNa11me_MouseLeave(object sender, MouseEventArgs e)
        {
            borTen21.Visibility = Visibility.Collapsed;
            textNhapHoVaTen51.Focus();
            //xoaAnh.bac
            SolidColorBrush grayBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 220, 220)); // Màu #dcdcdc
            ((Border)sender).Background = grayBrush;
            if (listAddUser.Count == 0)
            {
                lsvNameAdd51.Visibility = Visibility.Collapsed;
                textNhapHoVaTen51.Focus();
                textNhapHoVaTen151.Text = "Tìm theo id nhân viên";
            }
        }
        bool shouldProcessEvent7 = true;
        private void xoaName_PrqeviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            API_FilterComp.ListUser index = (API_FilterComp.ListUser)lsvNameAdd51.SelectedItem;
            if (index != null)
            {
                listAddUser.Remove(index);
                lsvNameAdd51.ClearValue(ItemsControl.ItemsSourceProperty);
                lsvNameAdd51.ItemsSource = listAddUser;
                shouldProcessEvent7 = false;
            }
            shouldProcessEvent7 = true;
            if (listAddUser.Count == 0)
            {
                gridName51.Visibility = Visibility.Visible;
                lsvNameAdd51.Visibility = Visibility.Collapsed;
                //textChonNgD.Text = "";

            }
        }

        private void textNhaadpHoVaTen_TextChanged(object sender, TextChangedEventArgs e)
        {
            borTen21.Visibility = Visibility.Visible;
            List<API_FilterComp.ListUser> listNameTimKiem = new List<API_FilterComp.ListUser>();
            string searchText = textNhapHoVaTen51.Text.ToString().ToLower().RemoveUnicode();
            foreach (var str in listAllUser)
            {
                if (str.userName.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listNameTimKiem.Any(item => item.userName.Equals(str.userName, StringComparison.OrdinalIgnoreCase)))

                    {
                        listNameTimKiem.Add(str);
                    }

                }
            }
            lsvTen21.ItemsSource = null;
            lsvTen21.ItemsSource = listNameTimKiem;

            //listDeXTimKiem.Clear();
            if (textNhapHoVaTen51.Text == "")
            {
                lsvTen21.ItemsSource = listAllUser;
            }
        }
        //wifi
        private void Grid_MouseLweftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            if (borWifi21.Visibility == Visibility.Collapsed)
            {
                borWifi21.Visibility = Visibility.Visible;
            }
            else
            {
                borWifi21.Visibility = Visibility.Collapsed;

            }
        }

        private void Wifi_Mo2useEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.DarkGray);
            ((Border)sender).Background = redBrush;
        }

        private void Wifi_MaouseLeave(object sender, MouseEventArgs e)
        {
            borWifi21.Visibility = Visibility.Collapsed;
            textNhapWifi51.Focus();
            //xoaAnh.bac
            SolidColorBrush grayBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 220, 220)); // Màu #dcdcdc
            ((Border)sender).Background = grayBrush;
            if (listAddWifi.Count == 0)
            {
                lsvWifiAdd51.Visibility = Visibility.Collapsed;
                textNhapWifi51.Focus();
                textNhapWifi151.Text = "Tìm theo wifi";
            }
        }
        bool shouldProcessEvent10 = true;
        private void Wifi_PrevieawMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            API_FilterComp.ListWifi index = (API_FilterComp.ListWifi)lsvWifiAdd51.SelectedItem;
            if (index != null)
            {
                listAddWifi.Remove(index);
                lsvWifiAdd51.ClearValue(ItemsControl.ItemsSourceProperty);
                lsvWifiAdd51.ItemsSource = listAddWifi;
                shouldProcessEvent10 = false;
            }
            shouldProcessEvent10 = true;
            if (listAddWifi.Count == 0)
            {
                gridWifi51.Visibility = Visibility.Visible;
                lsvWifiAdd51.Visibility = Visibility.Collapsed;
                //textChonNgD.Text = "";

            }
        }

        private void Border_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            grChinhSua.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseLeftButtonUp_6(object sender, MouseButtonEventArgs e)
        {
            grChinhSua.Visibility = Visibility.Collapsed;
        }

        private void lsvC1hucVu1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lsvChucVu1.SelectedItem != null)
                {
                    //string selectedPo = ((ListPo)lsvChucVu.SelectedItem).positionName;

                    string selected = ((API_FilterComp.ListPo)lsvChucVu1.SelectedItem).positionName;
                    if (!listAddPo.Any(item => item.positionName == selected))
                    {
                        API_FilterComp.ListPo listPo = new API_FilterComp.ListPo()
                        {
                            positionName = ((API_FilterComp.ListPo)lsvChucVu1.SelectedItem).positionName,
                            id = (int)((API_FilterComp.ListPo)lsvChucVu1.SelectedItem).id,

                        };
                        listAddPo.Add(listPo);
                        listAddPo = listAddPo.ToList();
                    }
                    lsvChucVuAdd51.Visibility = Visibility.Visible;
                    textNhapViTri151.Focus();
                    textNhapViTri151.Text = "";
                    textNhapViTri151.Text = "";
                    lsvChucVuAdd51.ItemsSource = listAddPo;
                    lsvChucVuAdd51.Items.Refresh();
                    borChucVu51.Visibility = Visibility.Collapsed;


                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Border_MouseLeftB11uttonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Border_MouseLdeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            API_FilterComp.ListOrg d = (sender as Border).DataContext as API_FilterComp.ListOrg;
            if (d != null)
            {
                textNhapTenToChuc151.Text = d.organizeDetailName;
                id_ToChuc = d.id.ToString();
                textNhapTenToChuc51.Text = "";
            }
            borToChuc51.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseLeftButtonUp_121(object sender, MouseButtonEventArgs e)
        {
            borTen21.Visibility = Visibility.Collapsed;
        }

        private void lsvTen21_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lsvTen21.SelectedItem != null)
                {
                    //string selectedPo = ((ListPo)lsvChucVu.SelectedItem).positionName;

                    string selected = ((API_FilterComp.ListUser)lsvTen21.SelectedItem).userName;
                    int selected_id = (int)((API_FilterComp.ListUser)lsvTen21.SelectedItem).idQLC;
                    if (!listAddUser.Any(item => item.userName == selected && item.idQLC == selected_id))
                    {
                        API_FilterComp.ListUser listPo = new API_FilterComp.ListUser()
                        {
                            userName = ((API_FilterComp.ListUser)lsvTen21.SelectedItem).userName,
                            idQLC = ((API_FilterComp.ListUser)lsvTen21.SelectedItem).idQLC
                        };
                        listAddUser.Add(listPo);
                        listAddUser = listAddUser.ToList();
                    }
                    lsvNameAdd51.Visibility = Visibility.Visible;
                    textNhapHoVaTen51.Focus();
                    textNhapHoVaTen151.Text = "";
                    textNhapHoVaTen51.Text = "";
                    lsvNameAdd51.ItemsSource = listAddUser;
                    lsvNameAdd51.Items.Refresh();
                    borTen21.Visibility = Visibility.Collapsed;


                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Rectangle_MouseLeftButtonUp_7(object sender, MouseButtonEventArgs e)
        {
            borTen21.Visibility = Visibility.Collapsed;
        }

        private void lsvCa21_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lsvCa21.SelectedItem != null)
                {
                    //string selectedPo = ((ListPo)lsvChucVu.SelectedItem).positionName;

                    string selected = ((API_FilterComp.ListShift)lsvCa21.SelectedItem).shift_name;
                    int selected_id = (int)((API_FilterComp.ListShift)lsvCa21.SelectedItem).type;
                    if (!listAddShift.Any(item => item.shift_name == selected && item.type == selected_id))
                    {
                        API_FilterComp.ListShift listPo = new API_FilterComp.ListShift()
                        {
                            shift_name = ((API_FilterComp.ListShift)lsvCa21.SelectedItem).shift_name,
                            type = ((API_FilterComp.ListShift)lsvCa21.SelectedItem).type
                        };
                        listAddShift.Add(listPo);
                        listAddShift = listAddShift.ToList();
                        foreach (var item in listAddShift)
                        {
                            if (item.type == 1)
                            {
                                item.type_name = "Ca vào";
                            }
                            else if (item.type == 2)
                            {
                                item.type_name = "Ca ra";
                            }
                        }
                    }
                    lsvCaAdd51.Visibility = Visibility.Visible;
                    textNhapCa51.Focus();
                    textNhapCa151.Text = "";
                    textNhapCa51.Text = "";
                    lsvCaAdd51.ItemsSource = listAddShift;
                    lsvCaAdd51.Items.Refresh();
                    borca21.Visibility = Visibility.Collapsed;


                }
            }
            catch (Exception ex)
            {

            }

        }

        private void lsvWifi21_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lsvWifi21.SelectedItem != null)
                {
                    //string selectedPo = ((ListPo)lsvChucVu.SelectedItem).positionName;

                    string selected = ((API_FilterComp.ListWifi)lsvWifi21.SelectedItem).name_wifi;
                    // int selected_id = (int)((API_FilterComp.ListShift)lsvCa.SelectedItem).type;
                    if (!listAddWifi.Any(item => item.name_wifi == selected))
                    {
                        API_FilterComp.ListWifi listPo = new API_FilterComp.ListWifi()
                        {
                            name_wifi = ((API_FilterComp.ListWifi)lsvWifi21.SelectedItem).name_wifi,
                            id = (int)((API_FilterComp.ListWifi)lsvWifi21.SelectedItem).id
                        };
                        listAddWifi.Add(listPo);
                        listAddWifi = listAddWifi.ToList();

                    }
                    lsvWifiAdd51.Visibility = Visibility.Visible;
                    textNhapWifi51.Focus();
                    textNhapWifi151.Text = "";
                    textNhapWifi51.Text = "";
                    lsvWifiAdd51.ItemsSource = listAddWifi;
                    lsvWifiAdd51.Items.Refresh();
                    borWifi21.Visibility = Visibility.Collapsed;


                }
            }
            catch (Exception ex)
            {

            }
        }

        private void textNhapWifi51_TextChanged(object sender, TextChangedEventArgs e)
        {
            borWifi21.Visibility = Visibility.Visible;
            List<API_FilterComp.ListWifi> listPoTimKiem = new List<API_FilterComp.ListWifi>();
            string searchText = textNhapWifi51.Text.ToString().ToLower().RemoveUnicode();
            foreach (var str in listAllWifi)
            {
                if (str.name_wifi.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listPoTimKiem.Any(item => item.name_wifi.Equals(str.name_wifi, StringComparison.OrdinalIgnoreCase)))

                    {
                        listPoTimKiem.Add(str);
                    }

                }
            }
            lsvWifi21.ItemsSource = null;
            lsvWifi21.ItemsSource = listPoTimKiem;

            //listDeXTimKiem.Clear();
            if (textNhapWifi51.Text == "")
            {
                lsvWifi21.ItemsSource = listAllWifi;
            }
        }

        private void Border_MouseLeftButtonUp_4(object sender, MouseButtonEventArgs e)
        {

        }

        private async void Border_MouseLeftBuattonUp_2(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/settingTimesheet/edit");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                List<string> listStringPo = new List<string>();
                foreach (var item in listAddPo)
                {
                    listStringPo.Add(item.id.ToString() + ",");
                }
                for (int i = 0; i < listStringPo.Count; i++)
                {
                    if (listStringPo[i].EndsWith(",") && i == listStringPo.Count - 1)
                    {
                        listStringPo[i] = listStringPo[i].Substring(0, listStringPo[i].Length - 1);
                    }
                }
                string id_Po = "";
                id_Po = "[" + string.Join("", listStringPo) + "]";
                //
                List<string> listStringWifi = new List<string>();
                foreach (var item in listAddWifi)
                {
                    listStringWifi.Add(item.id.ToString() + ",");
                }
                for (int i = 0; i < listStringWifi.Count; i++)
                {
                    if (listStringWifi[i].EndsWith(",") && i == listStringWifi.Count - 1)
                    {
                        listStringWifi[i] = listStringWifi[i].Substring(0, listStringWifi[i].Length - 1);
                    }
                }
                string id_Wifi = "";
                id_Wifi = "[" + string.Join("", listStringWifi) + "]";

                List<string> listStringLoc = new List<string>();
                foreach (var item in listAddLoc)
                {
                    listStringLoc.Add(item.cor_id.ToString() + ",");
                }
                for (int i = 0; i < listStringLoc.Count; i++)
                {
                    if (listStringLoc[i].EndsWith(",") && i == listStringLoc.Count - 1)
                    {
                        listStringLoc[i] = listStringLoc[i].Substring(0, listStringLoc[i].Length - 1);
                    }
                }
                string id_Loc = "";
                id_Loc = "[" + string.Join("", listStringLoc) + "]";

                //
                List<string> listStringEmp = new List<string>();
                foreach (var item in listAddUser)
                {
                    listStringEmp.Add(item.idQLC.ToString() + ",");
                }
                for (int i = 0; i < listStringEmp.Count; i++)
                {
                    if (listStringEmp[i].EndsWith(",") && i == listStringEmp.Count - 1)
                    {
                        listStringEmp[i] = listStringEmp[i].Substring(0, listStringEmp[i].Length - 1);
                    }
                }
                string id_Emp = "";
                id_Emp = "[" + string.Join("", listStringEmp) + "]";


                DateTime ngayBatDauDate;
                DateTime ngayKetThucDate;
                string ngayBatDauFormat = "";
                string ngayKetThucFormat = "";
                if (NgayBatDau51.Text != "")
                {
                    ngayBatDauDate = DateTime.Parse(NgayBatDau51.Text);
                    ngayBatDauFormat = ngayBatDauDate.ToString("yyyy-MM-ddTHH:mm:ss.fff+00:00");

                }

                if (NgayKetThuc51.Text != "")
                {
                    ngayKetThucDate = DateTime.Parse(NgayKetThuc51.Text);
                    ngayKetThucFormat = ngayKetThucDate.ToString("yyyy-MM-ddTHH:mm:ss.fff+00:00");
                }
                else
                {

                }

                if (id_ToChuc == "")
                {
                    var content1 = new StringContent("{\"setting_name\":" + textNhapTenCaiDat.Text + ",\"list_org\":\"\",\"list_pos\":" + id_Po + ",\"list_shifts\":[{\"id\":2025319,\"type_shift\":2}],\"list_wifi\":" + id_Wifi + "}", null, "application/json");
                    var content = new StringContent("{\"setting_id\":" + id_edit.ToString() + ",\"setting_name\":\"" + textNhapTenCaiDat51.Text + "\",\"list_org\":315,\"list_pos\":" + id_Po + ",\"list_emps\":" + id_Emp + ",\"start_time\":\"" + ngayBatDauFormat + "\",\"end_time\":\"" + ngayKetThucFormat + "\",\"list_shifts\":[{\"id\":2025319,\"type_shift\":1},{\"id\":2025319,\"type_shift\":2}],\"list_wifi\":" + id_Wifi + ",\"list_device\":[\"web\",\"app\"]}", null, "application/json");
                    request.Content = content;
                }
                else
                {
                    var content1 = new StringContent("{\"setting_name\":" + textNhapTenCaiDat.Text + ",\"list_org\":" + id_ToChuc + ",\"list_pos\":" + id_Po + ",\"list_shifts\":[{\"id\":2025319,\"type_shift\":2}],\"list_wifi\":" + id_Wifi + "}", null, "application/json");
                    var content = new StringContent("{\"setting_id\":" + id_edit.ToString() + ",\"setting_name\":\"" + textNhapTenCaiDat51.Text + "\",\"list_org\":" + id_ToChuc + ",\"list_pos\":" + id_Po + ",\"list_emps\":" + id_Emp + ",\"start_time\":\"" + ngayBatDauFormat + "\",\"end_time\":\"" + ngayKetThucFormat + "\",\"list_shifts\":[{\"id\":2025319,\"type_shift\":1},{\"id\":2025319,\"type_shift\":2}],\"list_wifi\":" + id_Wifi + ",\"list_device\":[\"web\",\"app\"]}", null, "application/json");
                    request.Content = content;
                }

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    ThemThanhCong uc = new ThemThanhCong(Main);
                    Main.grShowPopup.Children.Add(uc);
                    object Content = uc.Content;
                    uc.Content = null;
                    Main.grShowPopup.Children.Add(Content as UIElement);
                    getList();
                    grChinhSua.Visibility = Visibility.Collapsed;
                    dgv.Items.Refresh();
                }
            }
            catch (Exception ex)
            {

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
                    Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
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