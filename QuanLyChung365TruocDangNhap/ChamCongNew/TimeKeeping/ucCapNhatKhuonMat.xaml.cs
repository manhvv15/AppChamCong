using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
//using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.funcQuanLyCongTy.AddNewStaffTabList;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatCongChuan;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CapNhatKhuonMat;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.DuyetThietBiMoi;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.CapNhapKhuonMat;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.Login.InfoEp;

namespace QuanLyChung365TruocDangNhap.ChamCongNew
{
    /// <summary>
    /// Interaction logic for ucUpdateFace.xaml
    /// </summary>
    /// 
    public partial class ucCapNhatKhuonMat : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        MainWindow Main;
        int com_id = 0;
        int dep_id = 0;
        int ep_id = 0;
        int TotalItem = 0;
        double TongSoTrang = 0;
        public int pageNumber = 1;
        int pageSize = 10;
        public ucCapNhatKhuonMat(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            com_id = Main.IdAcount;
            LoadListChillCompany();
            LoadListRoom(com_id);
            LoadListSaff(dep_id);   
        }
     

        #region FuncComon
        public void LoadDataNull()
        {
            foreach (Item_NhanVien item in listSaff)
            {
                if (item.ep_image == "" || item.ep_image == null)
                {
                    item.ep_image = "https://chamcong.timviec365.vn/images/ep_logo.png";
                }
                else
                {
                    item.ep_image = "https://chamcong.24hpay.vn/upload/employee/" + item.ep_image;
                }
            }
            foreach (Item_NhanVien item in listSaff)
            {
                if (item.dep_name == "" || item.dep_name == null)
                {
                    item.dep_name = "Chưa cập nhật!";

                }
                else
                {
                    item.dep_name = item.dep_name;
                }
            }
            foreach (Item_NhanVien item in listSaff)
            {
                if (item.ep_phone_tk == "" || item.ep_phone_tk == null)
                {
                    item.ep_phone_tk = "Chưa cập nhật!";
                }
                else
                {
                    item.ep_phone_tk = item.ep_phone_tk;
                }
            }
            foreach (Item_NhanVien item in listSaff)
            {
                if (item.ep_email == "" || item.ep_email == null)
                {
                    item.ep_email = "Chưa cập nhật!";
                }
                else
                {
                    item.ep_email = item.ep_email;
                }
            }
        }
        #endregion

        #region Call API
        private List<List_Position> _listPosition;
        public List<List_Position> listPosition
        {
            get { return _listPosition; }
            set { _listPosition = value; OnPropertyChanged(); }
        }

        private List<Item_NhanVien> _listSaff;
        public List<Item_NhanVien> listSaff
        {
            get { return _listSaff; }
            set { _listSaff = value; OnPropertyChanged(); }
        }
        public async void LoadListSaff(int dep_id)
        {  
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.List_ManagerSaff_Api);             
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                if (this.com_id !=0)
                {
                    content.Add(new StringContent(this.com_id.ToString()), "com_id");
                } 
                if (this.dep_id !=0)
                {
                    content.Add(new StringContent(dep_id.ToString()), "dep_id");
                }
                content.Add(new StringContent(this.pageNumber.ToString()), "pageNumber");
                content.Add(new StringContent(this.pageSize.ToString()), "pageSize");
                request.Content = content;
                var response = await client.SendAsync(request);
                var resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    Root_NhanVien result = JsonConvert.DeserializeObject<Root_NhanVien>(resContent);

                    if (result.data != null)
                    {
                        TotalItem = result.data.totalItems;
                        listSaff = result.data.items;
                        txtCountSaff.Text = TotalItem.ToString();

                    }
                }
                TongSoTrang = (int)Math.Ceiling((double)TotalItem / pageSize);
                if (pageNumber == 1)
                {
                    LoadTableDataPagging(listSaff);
                }
                lsvNhanVien.ItemsSource = listSaff;
                LoadDataNull();
            }
            catch (System.Exception)
            {
            }
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.List_Position_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resContent = await response.Content.ReadAsStringAsync();

                Root_Position result = JsonConvert.DeserializeObject<Root_Position>(resContent);

                if (result.data.data != null)
                {
                    listPosition = result.data.data;
                }

            }
            catch (Exception)
            {
            }
            var query = from e in listSaff
                        join p in listPosition on e.position_id equals p.positionId
                        select new Item_NhanVien
                        {
                            _id = e._id,
                            ep_id = e.ep_id,
                            ep_email = e.ep_email,
                            ep_email_lh = e.ep_email_lh,
                            ep_phone_tk = e.ep_phone_tk,
                            ep_name = e.ep_name,
                            ep_education = e.ep_education,
                            ep_exp = e.ep_exp,
                            ep_phone = e.ep_phone,
                            ep_image = e.ep_image,
                            ep_address = e.ep_address,
                            ep_gender = e.ep_gender,
                            ep_married = e.ep_married,
                            ep_birth_day = e.ep_birth_day,
                            ep_description = e.ep_description,
                            create_time = e.create_time,
                            role_id = e.role_id,
                            group_id = e.group_id,
                            start_working_time = e.start_working_time,
                            position_id = e.position_id,
                            ep_status = e.ep_status,
                            allow_update_face = e.allow_update_face,
                            com_id = e.com_id,
                            dep_id = e.dep_id,
                            dep_name = e.dep_name,
                            gr_name = e.gr_name,
                            positionName = p.positionName,
                            ep_married_status = e.ep_married_status,
                        };
            if (query != null)
            {
                Console.WriteLine("query là " + query.ToString());
                listSaff = query.ToList();
            }
            else
            {
                listSaff = listSaff.ToList();
            }

            dgvCapNhapKhuonMat.ItemsSource = listSaff;
        }
        #endregion

        #region Api Công Ty
        private List<Item_CtyCon> _ctyCon;
        public List<Item_CtyCon> ctyCon
        {
            get { return _ctyCon; }
            set { _ctyCon = value; OnPropertyChanged(); }
        }
        public async void LoadListChillCompany()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.List_ChillCompany_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resContent = await response.Content.ReadAsStringAsync();

                Root_CongTyCon result = JsonConvert.DeserializeObject<Root_CongTyCon>(resContent);

                if (result.data.items != null)
                {
                    ctyCon = result.data.items;
                    Item_CtyCon chillcom = new Item_CtyCon();
                    chillcom.com_id = Main.IdAcount;
                    chillcom.com_name = "Chọn công ty";
                    ctyCon.Insert(0, chillcom);
                    cboDSCongTy.ItemsSource = ctyCon;
                    var congty = ctyCon
                        .Where(Item_CtyCon => Item_CtyCon.com_id == Main.IdAcount)
                        .Select(Item_CtyCon => Item_CtyCon.com_name)
                        .FirstOrDefault();
                   cboDSCongTy.Text = congty.ToString();
                   cboDSCongTy.SelectedIndex = 0;

                }  

            }
            catch (Exception)
            {
            }

        }

        private void cboDSCongTy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cty = cboDSCongTy.SelectedItem as Item_CtyCon;
            if (cty != null)
            {     
                LoadListRoom(cty.com_id);
            } 
        }
        private void bodSelectCompany_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodPhongBan.Visibility == Visibility.Collapsed)
            {
                bodPhongBan.Visibility = Visibility.Visible;
                borNhanVien.Visibility = Visibility.Collapsed;
            }
            else
            {
                bodPhongBan.Visibility = Visibility.Collapsed;
            }
        }
       
        private void popup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodPhongBan.Visibility == Visibility.Visible)
            {
                bodPhongBan.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;

            }
            else if (borNhanVien.Visibility == Visibility.Visible)
            {
                borNhanVien.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;

            }
        }
        #endregion

        #region API PhongBan
        public List<Item_PhongBan> _lstPhongBan;
        public List<Item_PhongBan> lstPhongBan
        {
            get { return _lstPhongBan;}
            set { _lstPhongBan = value; OnPropertyChanged(); }
        }

        private List<ListFace> _lstSearchNVPhongBan;
        public List<ListFace> lstSearchNVPhongBan
        {
            get { return _lstSearchNVPhongBan; }
            set { _lstSearchNVPhongBan = value; }
        }
        public async void LoadListRoom(int com_id)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.List_Room_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resContent = await response.Content.ReadAsStringAsync();

                Root_PhongBan result = JsonConvert.DeserializeObject<Root_PhongBan>(resContent);

                if (result.data.items != null)
                {
                    lstPhongBan = result.data.items;
                    lsvPhongBan.ItemsSource = lstPhongBan;
                    Item_PhongBan dep = new Item_PhongBan();
                    dep.dep_id = 0;
                    dep.dep_name = "Phòng ban (tất cả)";
                    lstPhongBan.Insert(0, dep);
                    var phongban = lstPhongBan
                        .Where(Item_PhongBan => Item_PhongBan.dep_id == 0)
                        .Select(Item_PhongBan => Item_PhongBan.dep_name)
                        .FirstOrDefault();
                    txtHienThiPhongBan.Text = phongban;
                }
            }
            catch (Exception)
            {
            }
        }
        private void bodTenPhongBan_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Item_PhongBan phongban = (sender as Border).DataContext as Item_PhongBan;
            if (phongban != null)
            {
                txtHienThiPhongBan.Text = phongban.dep_name;
                dep_id = phongban.dep_id;
                bodPhongBan.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                Main.PhongBan = phongban.dep_name.ToString();

            }
            LoadListSaff(dep_id);


        }
        private void lsvCongTy_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is System.Windows.Controls.ListView && !e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((System.Windows.Controls.Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }
        #endregion

        #region Api Nhân Viên
        private List<Item_NhanVien> _listSaff1;
        public List<Item_NhanVien> listSaff1
        {
            get { return _listSaff1; }
            set { _listSaff1 = value; }
        }

        //đag làm chỗ này
        public async void loadListSaff()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.List_ManagerSaff_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());

            }
            catch (Exception)
            {
            }
        }

        private void borHienThiNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borNhanVien.Visibility == Visibility.Collapsed)
            {
                borNhanVien.Visibility = Visibility.Visible;
                bodPhongBan.Visibility = Visibility.Collapsed;  
            }
            else
            {
                borNhanVien.Visibility = Visibility.Collapsed;
            }
        }
        private void borTenNhanVien_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Item_NhanVien nv = (sender as Border).DataContext as Item_NhanVien;
            if (nv != null)
            {
                textHienThiNhanVien.Text = nv.ep_name;
                ep_id = nv.ep_id;
                borNhanVien.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                Main.NhanVien = nv.ep_name.ToString();
            }
            //var searchNameSaff = listSaff.Where(x => x.ep_id.ToString().Contains(nv.ep_id.ToString())).ToList();
            //TongSoTrang = Math.Ceiling((double)searchNameSaff.Count / pageSize);
            //pageNumber = 1;
            //dgvCapNhapKhuonMat.ItemsSource = searchNameSaff;
            //LoadTableDataPagging(listSaff);

        }
        private void lsvNhanVien_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollNhanVien.ScrollToVerticalOffset(scrollNhanVien.VerticalOffset - e.Delta);
        }
        private void textSearchNhanVien_TextChanged(object sender, TextChangedEventArgs e)
        {
            listSaff1 = new List<Item_NhanVien>();
            foreach (var str in listSaff)
            {
                if (str.ep_name.ToLower().RemoveUnicode().Contains(textSearchNhanVien.Text.ToLower().RemoveUnicode()))
                {
                    listSaff1.Add(str);
                }
            }
            lsvNhanVien.ItemsSource = listSaff1;
            if (textSearchNhanVien.Text == "")
            {
                lsvNhanVien.ItemsSource = listSaff;
            }
        }

        private void InputNameSearch_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textHienThiNhanVien.Text = "Nhập tên cần tìm";
        }
        #endregion

        #region Checkecd

        private List<string> listSaffID = new List<string>();
        
        private bool _IsCheckAll = false;

        public bool IsCheckAll
        {
            get { return _IsCheckAll; }
            set { _IsCheckAll = value; OnPropertyChanged(); }
        }

        private async void XacNhanKhuonMatTatCa(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsCheckAll == true)
                {
                    listSaffID.Clear();
                    foreach (var items in listSaff)
                    {
                        if (items.allow_update_face == 1)
                        {
                            items.allow_update_face = 0;
                            listSaffID.Add(items.ep_id.ToString());
                        }
                    }
                    IsCheckAll = false;
                    Main.grShowPopup.Children.Add(new ucThongBaoBoDuyetAll(Main));
                }
                else
                {
                    listSaffID.Clear();
                    foreach (var items in listSaff)
                    {
                        if (items.allow_update_face == 0)
                        {
                            items.allow_update_face = 1;
                            listSaffID.Add(items.ep_id.ToString());
                        }
                    }
                    IsCheckAll = true;
                    Main.grShowPopup.Children.Add(new ucThongBaoDuyetAll(Main));
                }
                string resultID = string.Join(",", listSaffID);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.Duyet_KhuonMat_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(resultID.ToString()), "list_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resContent = await response.Content.ReadAsStringAsync();
                Root_Duyet resurl = JsonConvert.DeserializeObject<Root_Duyet>(resContent);
                if (resurl.data != null && resurl.data.result == true)
                {
                    listSaff = listSaff.ToList();
                }
            }
            catch (Exception)
            { }
        }
       
        private async void XacNhanKhuonMat(object sender, MouseButtonEventArgs e)
        {
            try
            {
                for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                    if (vis is DataGridRow)
                    {
                        var row = (DataGridRow)vis;
                        var z = row.Item as Item_NhanVien;

                        if (z != null)
                        {
                            HttpClient http = new HttpClient();
                            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
                      
                            
                            Dictionary<string, string> form = new Dictionary<string, string>();
                            form.Add("list_id", z.ep_id.ToString());
                            HttpResponseMessage respon = await http.PostAsync(APIs.API.Duyet_KhuonMat_Api, new FormUrlEncodedContent(form));
                            Root_Duyet api = JsonConvert.DeserializeObject<Root_Duyet>(respon.Content.ReadAsStringAsync().Result);
                            if (z.allow_update_face == 0)
                            {
                                if (api.data != null && api.data.result == true)
                                {
                                   
                                    var ep = listSaff.Find(i => i.ep_id == z.ep_id);
                                    ep.allow_update_face = 1;
                                    listSaff = listSaff.ToList();
                                    Main.grShowPopup.Children.Add(new ucThongBaoDuyetKhuonMat(Main));

                                }
                            }
                            else
                            {
                                if (api.data != null && api.data.result == true)
                                {
                                   
                                    var ep = listSaff.Find(i => i.ep_id == z.ep_id);
                                    ep.allow_update_face = 0;
                                    listSaff = listSaff.ToList();
                                    Main.grShowPopup.Children.Add(new ucThongBaoBoDuyetKhuonMat(Main));
                                }
                            }
                        }
                        break;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void btnLoc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var ListSaffFilter = new List<Item_NhanVien>();
            if (com_id != 0 && dep_id != 0 || ep_id != 0)
            {
                ListSaffFilter = listSaff.Where(x =>x.com_id == com_id && x.dep_id == dep_id && x.ep_id == ep_id).ToList();
                TongSoTrang = Math.Ceiling((double)ListSaffFilter.Count / pageSize);
                dgvCapNhapKhuonMat.ItemsSource = ListSaffFilter;
                LoadTableDataPagging(ListSaffFilter);
            }
            LoadTableDataPagging(ListSaffFilter);

        }

        #region Phân Trang
        private List<ListFace> _lstPTFace;
        public List<ListFace> lstPTFace
        {
            get { return _lstPTFace; }
            set { _lstPTFace = value; }
        }
        public void LoadTableDataPagging(List<Item_NhanVien> listSaff)
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
        public void LoadTableData(List<ListFace> listFaces)
        {
            //lstPTFace = new List<ListFace>();
            //// ẩn phân trang khi không có dũ liệu
            //if (listFaces.Count > 0)
            //{
            //    TotalListItem = listFaces.Count;
            //    TongSoTrang = (int)Math.Ceiling(TotalListItem / 10.0);
            //    SoDu = 10 - (TotalListItem % 10);
            //    var numberOfItem = (TongSoTrang == 1) ? TotalListItem : 10;
            //    for (int i = 0; i < numberOfItem; i++)
            //    {
            //        lstPTFace.Add(listFaces[i]);
            //    }
            //    dgvCapNhapKhuonMat.ItemsSource = lstPTFace;


            //    textPage1.Text = "1";
            //    textPage2.Text = "2";
            //    textPage3.Text = "3";
            //    borLui1.Visibility = Visibility.Collapsed;
            //    borPageDau.Visibility = Visibility.Collapsed;
            //    borLen1.Visibility = Visibility.Collapsed;
            //    borPageCuoi.Visibility = Visibility.Collapsed;

            //    if (TongSoTrang <= 3)
            //    {
            //        borPage3.Visibility = Visibility.Visible;

            //        borLen1.Visibility = Visibility.Collapsed;
            //        borPageCuoi.Visibility = Visibility.Collapsed;
            //        if (TongSoTrang == 2)
            //        {
            //            borPage3.Visibility = Visibility.Collapsed;
            //            borPage2.Visibility = Visibility.Visible;

            //        }
            //        if (TongSoTrang == 1)
            //        {
            //            borPage3.Visibility = Visibility.Collapsed;
            //            borPage2.Visibility = Visibility.Collapsed;

            //        }
            //    }
            //    else
            //    {
            //        borLui1.Visibility = Visibility.Collapsed;
            //        borPageDau.Visibility = Visibility.Collapsed;
            //        borLen1.Visibility = Visibility.Visible;
            //        borPageCuoi.Visibility = Visibility.Visible;
            //    }
            //}
            //else
            //{
            //    lstPTFace = new List<ListFace>();
            //    borLui1.Visibility = Visibility.Collapsed;
            //    borPageDau.Visibility = Visibility.Collapsed;
            //    borLen1.Visibility = Visibility.Collapsed;
            //    borPageCuoi.Visibility = Visibility.Collapsed;
            //    borPage1.Visibility = Visibility.Visible;
            //    borPage2.Visibility = Visibility.Collapsed;
            //    borPage3.Visibility = Visibility.Collapsed;
            //}
        }
        //Deg Phan trang
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
            LoadListSaff(dep_id);
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
            LoadListSaff(dep_id);

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
                LoadListSaff(dep_id);
            }
            if (int.Parse(textPage1.Text) == 1)
            {
                pageNumber = 1;
                LoadListSaff(dep_id);
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
            //pageNumber = int.Parse(textPage2.Text);
            //if (int.Parse(textPage3.Text) == TongSoTrang) pageNumber = int.Parse(textPage3.Text);
            pageNumber = int.Parse(textPage2.Text);
            LoadListSaff(dep_id);

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

            //pageNumber = int.Parse(textPage3.Text);
            LoadListSaff(dep_id);


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
            LoadListSaff(dep_id);
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
            LoadListSaff(dep_id);
        }

        private void Send_CurrentPageMumber(object sender, int e)
        {
            pageNumber = e;
        }

        #endregion

        private void btnTatCaNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            ucListAllStaff uc = new ucListAllStaff(this.Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = string.Empty;
            Main.txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text + " / " + "Toàn bộ nhân viên";
        }

        private void btnNhanVienChoDuyet_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            ucListWaitingStaff uc = new ucListWaitingStaff(this.Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = string.Empty;
            Main.txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text + " / " + "Nhân viên chờ duyệt";
        }
    }
}
