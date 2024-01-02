using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CapNhatKhuonMat;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager;
//using DocumentFormat.OpenXml.Wordprocessing;
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
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping
{
    /// <summary>
    /// Interaction logic for ucCapNhatKhuonMatMoi.xaml
    /// </summary>
    public partial class ucCapNhatKhuonMatMoi : UserControl
    {
        MainWindow Main;
        BrushConverter bc = new BrushConverter();
        public ucCapNhatKhuonMatMoi(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            //getListUpdateFace();
            getListPhongBanMoi();
            getListName();
            GetListUpdateFace();
            //getListUpdated();
            // getDsListLoc();
        }
        List<API_ListAll.ListUpdateNV> listAll = new List<API_ListAll.ListUpdateNV>();
        List<API_ListLoc.NV> listAllUp = new List<API_ListLoc.NV>();
        List<API_Ten.List> ListNameStaff = new List<API_Ten.List>();
        int number_List;
        private async void getListUpdated()
        {
            //try
            //{
            //    var client = new HttpClient();
            //    var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/face/list");
            //    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

            //    var content = new MultipartFormDataContent();
            //    content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
            //    request.Content = content;
            //    var response = await client.SendAsync(request);
            //    response.EnsureSuccessStatusCode();
            //    var responseContent = await response.Content.ReadAsStringAsync();
            //    if (response.IsSuccessStatusCode)
            //    {
            //        API_ListLoc.ListLoc api = JsonConvert.DeserializeObject<API_ListLoc.ListLoc>(responseContent);
            //        if (api.data.data != null)
            //        {
            //            listAllUp = api.data.data;
            //            number_List = listAllUp.Count();
            //            txtCountSaff.Text = number_List.ToString();
            //            dgvCapNhapKhuonMat1.ItemsSource = listAllUp;
            //            foreach (var item in api.data.data)
            //            {
            //                if (item.email == null)
            //                {
            //                    item.email = "Chưa cập nhật";
            //                }
            //                else
            //                {
            //                    item.email = item;
            //                }
            //            }
            //            foreach (var item in api.data.data)
            //            {
            //                if (item.detail == null)
            //                {
            //                    item.detail = new API_ListLoc.Detail
            //                    {
            //                        organizeDetailName = "Chưa cập nhật"
            //                    };
            //                }
            //                else
            //                {
            //                    item.detail.organizeDetailName = item.detail.organizeDetailName;
            //                }
            //            }
            //            foreach (var item in api.data.data)
            //            {
            //                if (item.avatarUser == null)
            //                {
            //                    item.avatarUser = "/Resource/image/CompanyLogo.png";
            //                }
            //                else
            //                {
            //                    item.avatarUser = item;
            //                }
            //            }
            //            //  listAll = api.data.data;

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}


        }

        #region CallApi
        async void GetListUpdateFace(int pageNumber = 1)
        {
            try
            {

                int? idQLC = null;
                if (lsvTen.SelectedItem != null)
                {
                    idQLC = (lsvTen.SelectedItem as API_Ten.List).idQLC;
                }
                List<API_PhongBanMoi.ListOrganizeDetailId> dep_id = null;
                if (lsvPhongBan.SelectedItem != null)
                {
                    dep_id = (lsvPhongBan.SelectedItem as API_PhongBanMoi.PhongBan).listOrganizeDetailId;
                }

                var bodyObject = new
                {
                    pageNumber = pageNumber,
                    is_update = false,
                    dep_id = dep_id,
                    idQLC = idQLC

                };
                string json = JsonConvert.SerializeObject(bodyObject);

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/face/list");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent(json, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                MessageEntities.Root resultMessage = JsonConvert.DeserializeObject<MessageEntities.Root>(responseContent);
                if (response.IsSuccessStatusCode)
                {
                    API_ListAll.ListUpdateNV result = JsonConvert.DeserializeObject<API_ListAll.ListUpdateNV>(responseContent);
                    listNV_All = result.data.data;
                    if (paginNV.SelectedPage == 0) paginNV.TotalRecords = (int)result.data.count;
                    txtCountSaff.Text = result.data.count.ToString();
                    listNV_All = (from item in listNV_All
                                  select new API_ListAll.NV
                                  {
                                      _id = item._id,
                                      userName = (item.userName == null || item.userName == "") ? "Chưa cập nhật" : item.userName,
                                      positionName = (item.positionName == null || item.positionName == "") ? "Chưa cập nhật" : item.positionName,
                                      phoneTK = (item.phoneTK == null || item.phoneTK == "") ? "Chưa cập nhật" : item.phoneTK,
                                      avatarUser = (item.avatarUser == null || item.avatarUser == "") ? "https://tinhluong.timviec365.vn/img/add.png" : "https://chamcong.24hpay.vn/upload/employee/" + item.avatarUser,
                                      email = (item.email == null || item.email == "") ? "Chưa cập nhật" : item.email,

                                      dep_id = item.dep_id,
                                      com_id = item.com_id,
                                      allow_update_face = item.allow_update_face,
                                      position_id = item.position_id,
                                      idQLC = item.idQLC,
                                      detail = item.detail,
                                  }).ToList();


                    dgvCapNhapKhuonMat.ItemsSource = listNV_All;
                    dgvCapNhapKhuonMat.Items.Refresh();

                }
                else
                {
                    Main.grShowPopup.Children.Add(new ucPopupError(resultMessage.error.message));
                }

            }
            catch (Exception ex) { Main.grShowPopup.Children.Add(new ucPopupError("Có lỗi xảy ra" + ex.Message)); }

        }
        #endregion

        async void GetListUpdatedFace(int pageNumber = 1)
        {
            try
            {

                int? idQLC = null;
                if (lsvTen.SelectedItem != null)
                {
                    idQLC = (lsvTen.SelectedItem as API_Ten.List).idQLC;
                }
                List<API_PhongBanMoi.ListOrganizeDetailId> dep_id = null;
                if (lsvPhongBan.SelectedItem != null)
                {
                    dep_id = (lsvPhongBan.SelectedItem as API_PhongBanMoi.PhongBan).listOrganizeDetailId;
                }

                var bodyObject = new
                {
                    pageNumber = pageNumber,
                    is_update = true,
                    dep_id = dep_id,
                    idQLC = idQLC

                };
                string json = JsonConvert.SerializeObject(bodyObject);

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/face/list");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent(json, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                MessageEntities.Root resultMessage = JsonConvert.DeserializeObject<MessageEntities.Root>(responseContent);
                if (response.IsSuccessStatusCode)
                {
                    API_ListAll.ListUpdateNV result = JsonConvert.DeserializeObject<API_ListAll.ListUpdateNV>(responseContent);
                    listNV_All = result.data.data;
                    if (paginNVUpdated.SelectedPage == 0) paginNVUpdated.TotalRecords = (int)result.data.count;
                    txtCountSaff.Text = result.data.count.ToString();
                    listNV_All = (from item in listNV_All
                                  select new API_ListAll.NV
                                  {
                                      _id = item._id,
                                      userName = (item.userName == null || item.userName == "") ? "Chưa cập nhật" : item.userName,
                                      positionName = (item.positionName == null || item.positionName == "") ? "Chưa cập nhật" : item.positionName,
                                      phoneTK = (item.phoneTK == null || item.phoneTK == "") ? "Chưa cập nhật" : item.phoneTK,
                                      avatarUser = (item.avatarUser == null || item.avatarUser == "") ? "https://tinhluong.timviec365.vn/img/add.png" : "https://chamcong.24hpay.vn/upload/employee/" + item.avatarUser,
                                      email = (item.email == null || item.email == "") ? "Chưa cập nhật" : item.email,

                                      dep_id = item.dep_id,
                                      com_id = item.com_id,
                                      allow_update_face = item.allow_update_face,
                                      position_id = item.position_id,
                                      idQLC = item.idQLC,
                                      detail = item.detail,
                                  }).ToList();


                    dgvCapNhapKhuonMat1.ItemsSource = listNV_All;
                    dgvCapNhapKhuonMat1.Items.Refresh();

                }
                else
                {
                    Main.grShowPopup.Children.Add(new ucPopupError(resultMessage.error.message));
                }

            }
            catch (Exception ex) { Main.grShowPopup.Children.Add(new ucPopupError("Có lỗi xảy ra" + ex.Message)); }

        }

        #region PhanTrang
        private void paginNV_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            //    //getDsNhanVien("", paginNV.SelectedPage);

            GetListUpdateFace(paginNV.SelectedPage);
        }
        private void paginNVUpdated_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            //    //getDsNhanVien("", paginNV.SelectedPage);

            GetListUpdatedFace(paginNVUpdated.SelectedPage);
        }
        #endregion

        public async void getListUpdateFace()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/face/list");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_ListAll.ListUpdateNV api = JsonConvert.DeserializeObject<API_ListAll.ListUpdateNV>(responseContent);
                    if (api.data.data != null)
                    {
                        listNV_All = api.data.data;
                        number_List = listNV_All.Count();
                        txtCountSaff.Text = number_List.ToString();
                        dgvCapNhapKhuonMat.ItemsSource = listNV_All;
                        foreach (var item in api.data.data)
                        {
                            if (item.email == null)
                            {
                                item.email = "Chưa cập nhật";
                            }
                            else
                            {
                                item.email = item.email;
                            }
                        }
                        foreach (var item in api.data.data)
                        {
                            if (item.detail == null)
                            {
                                item.detail = new API_ListAll.Detail
                                {
                                    organizeDetailName = "Chưa cập nhật"
                                };
                            }
                            else
                            {
                                item.detail.organizeDetailName = item.detail.organizeDetailName;
                            }
                        }
                        foreach (var item in api.data.data)
                        {
                            if (item.avatarUser == null)
                            {
                                item.avatarUser = "/Resource/image/CompanyLogo.png";
                            }
                            else
                            {
                                item.avatarUser = item.avatarUser;
                            }
                        }
                        //  listAll = api.data.data;

                    }
                }
            }
            catch (Exception ex)
            {

            }


        }
        // List<API_PhongBanMoi.PhongBanMoi> listAll = new List<API_PhongBanMoi.PhongBanMoi>();
        private async void getListPhongBanMoi()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/organizeDetail/listAll");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_PhongBanMoi.PhongBanMoi api = JsonConvert.DeserializeObject<API_PhongBanMoi.PhongBanMoi>(responseContent);
                    if (api.data.data != null)
                    {
                        //listAll = api.data.data;
                        lsvPhongBan.ItemsSource = api.data.data;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private async void getListName()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/employee/listEmpSimpleNoToken");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_Ten.Name api = JsonConvert.DeserializeObject<API_Ten.Name>(responseContent);
                    if (api.data.list != null)
                    {

                        ListNameStaff = api.data.list.Prepend(new API_Ten.List() { idQLC = 0, userName = "Tất cả nhân viên" }).ToList();
                        lsvTen.ItemsSource = ListNameStaff;

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void borPhongBan1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borPhongBan.Visibility == Visibility.Collapsed)
            {
                borPhongBan.Visibility = Visibility.Visible;
            }
            else
            {
                borPhongBan.Visibility = Visibility.Collapsed;
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        List<API_ListAll.NV> filteredList = new List<API_ListAll.NV>();
        List<API_ListAll.NV> filteredListName = new List<API_ListAll.NV>();
        List<API_ListAll.NV> SearchList = new List<API_ListAll.NV>();
        List<API_ListAll.NV> listNV_All = new List<API_ListAll.NV>();
        List<API_ListLoc.NV> filteredList1 = new List<API_ListLoc.NV>();
        List<API_ListLoc.NV> filteredListName1 = new List<API_ListLoc.NV>();
        List<API_Ten.List> SearchList1 = new List<API_Ten.List>();
        List<API_ListLoc.NV> listNV_All1 = new List<API_ListLoc.NV>();

        //private void lsvPhongBan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //    // filteredList
        //    if (check)
        //    {
        //        filteredListName1.Clear();
        //        if (lsvPhongBan.SelectedItem != null)
        //        {
        //            string selectedPo = ((API_PhongBanMoi.PhongBan)lsvPhongBan.SelectedItem).organizeDetailName;
        //            txtPhongBan.Text = selectedPo;
        //            borPhongBan.Visibility = Visibility.Collapsed;

        //            foreach (var item in listNV_All1)
        //            {
        //                if (item is API_ListLoc.NV name) // Kiểm tra kiểu dữ liệu
        //                {
        //                    if (name.detail.organizeDetailName == selectedPo)
        //                    {
        //                        filteredListName1.Add(name);

        //                    }
        //                    else
        //                    {

        //                    }
        //                }

        //            }
        //        }
        //        number_List = filteredListName1.Count();
        //        txtCountSaff.Text = number_List.ToString();
        //        dgvCapNhapKhuonMat1.ItemsSource = filteredListName1;
        //        dgvCapNhapKhuonMat1.Items.Refresh();
        //    }
        //    else
        //    {
        //        filteredList.Clear();
        //        if (lsvPhongBan.SelectedItem != null)
        //        {
        //            string selectedPo = ((API_PhongBanMoi.PhongBan)lsvPhongBan.SelectedItem).organizeDetailName;
        //            txtPhongBan.Text = selectedPo;
        //            borPhongBan.Visibility = Visibility.Collapsed;

        //            foreach (var item in listNV_All)
        //            {
        //                if (item is API_ListAll.NV phongBan) // Kiểm tra kiểu dữ liệu
        //                {
        //                    if (phongBan.detail.organizeDetailName == selectedPo)
        //                    {
        //                        filteredList.Add(phongBan);

        //                    }
        //                    else
        //                    {

        //                    }
        //                }

        //            }
        //        }
        //        number_List = filteredList.Count();
        //        txtCountSaff.Text = number_List.ToString();
        //        dgvCapNhapKhuonMat.ItemsSource = filteredList;
        //        dgvCapNhapKhuonMat.Items.Refresh();
        //    }
        //}

        private void lsvPhongBan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lsvPhongBan.SelectedItem != null)
            {
                string selectedPo = ((API_PhongBanMoi.PhongBan)lsvPhongBan.SelectedItem).organizeDetailName;
                txtPhongBan.Text = selectedPo;
                txtPhongBan.Foreground = (SolidColorBrush)bc.ConvertFromString("#474747");
                borPhongBan.Visibility = Visibility.Collapsed;


            }
        }
        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borPhongBan.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            borTen1.Visibility = Visibility.Collapsed;
        }

        private void borTen_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textSearchTen.Text = "";
            textSearchTen.Focus();
            if (borTen1.Visibility == Visibility.Collapsed)
            {
                borTen1.Visibility = Visibility.Visible;
            }
            else
            {
                borTen1.Visibility = Visibility.Collapsed;
            }
        }

        private void textSearchTen_TextChanged(object sender, TextChangedEventArgs e)
        {

            SearchList1.Clear();
            string searchText1 = textSearchTen.Text.ToString().ToLower().RemoveUnicode();
            foreach (var str in ListNameStaff)
            {
                if (str.userName.ToLower().RemoveUnicode().Contains(searchText1))
                {
                    SearchList1.Add(str);

                }
            }
            lsvTen.ItemsSource = SearchList1;
            if (searchText1 == "")
            {

                lsvTen.ItemsSource = ListNameStaff;
            }
            lsvTen.Items.Refresh();
            borTen1.Visibility = Visibility.Visible;
        }

        //private void lsvTen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (check)
        //    {
        //        filteredListName1.Clear();
        //        if (lsvTen.SelectedItem != null)
        //        {
        //            string selectedPo = ((API_Ten.List)lsvTen.SelectedItem).userName;
        //            textSearchTen.Text = selectedPo;
        //            borTen1.Visibility = Visibility.Collapsed;

        //            foreach (var item in listNV_All1)
        //            {
        //                if (item is API_ListLoc.NV name) // Kiểm tra kiểu dữ liệu
        //                {
        //                    if (name.userName == selectedPo)
        //                    {
        //                        filteredListName1.Add(name);

        //                    }
        //                    else
        //                    {

        //                    }
        //                }

        //            }
        //        }
        //        number_List = filteredListName1.Count();
        //        txtCountSaff.Text = number_List.ToString();
        //        dgvCapNhapKhuonMat1.ItemsSource = filteredListName1;
        //        dgvCapNhapKhuonMat1.Items.Refresh();
        //    }
        //    else
        //    {
        //        filteredListName.Clear();
        //        if (lsvTen.SelectedItem != null)
        //        {
        //            string selectedPo = ((API_Ten.List)lsvTen.SelectedItem).userName;
        //            textSearchTen.Text = selectedPo;
        //            borTen1.Visibility = Visibility.Collapsed;

        //            foreach (var item in listNV_All)
        //            {
        //                if (item is API_ListAll.NV name) // Kiểm tra kiểu dữ liệu
        //                {
        //                    if (name.userName == selectedPo)
        //                    {
        //                        filteredListName.Add(name);

        //                    }
        //                    else
        //                    {

        //                    }
        //                }

        //            }
        //        }
        //        number_List = filteredListName.Count();
        //        txtCountSaff.Text = number_List.ToString();
        //        dgvCapNhapKhuonMat.ItemsSource = filteredListName;
        //        dgvCapNhapKhuonMat.Items.Refresh();
        //    }
        //}

        private void lsvTen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsvTen.SelectedItem != null)
            {
                string selectedPo = ((API_Ten.List)lsvTen.SelectedItem).userName;
                textSearchTen.Text = selectedPo;
                borTen1.Visibility = Visibility.Collapsed;
            }
        }
        List<API_ListLoc.NV> listAllLoc = new List<API_ListLoc.NV>();
        int totalRecords = 0;
        private async void getDsListLoc(int pagenumber = 1)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/face/list");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                //  request.Headers.Add("content-type", "application/json");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent("true"), "is_update");
                content.Add(new StringContent(pagenumber.ToString()), "pageNumber");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    API_ListLoc.ListLoc api = JsonConvert.DeserializeObject<API_ListLoc.ListLoc>(responseContent);
                    if (api.data.data != null)
                    {
                        totalRecords = (int)api.data.count;
                        listNV_All1 = api.data.data;
                        number_List = listNV_All1.Count();
                        txtCountSaff.Text = api.data.count.ToString();

                        foreach (var item in api.data.data)
                        {
                            if (item.email == null)
                            {
                                item.email = "Chưa cập nhật";
                            }
                            else
                            {
                                item.email = item.email;
                            }
                        }
                        foreach (var item in api.data.data)
                        {
                            if (item.detail == null)
                            {
                                item.detail = new API_ListLoc.Detail
                                {
                                    organizeDetailName = "Chưa cập nhật"
                                };
                            }
                            else
                            {
                                item.detail.organizeDetailName = item.detail.organizeDetailName;
                            }
                        }
                        foreach (var item in api.data.data)
                        {
                            if (item.avatarUser == null)
                            {
                                item.avatarUser = "/Resource/image/CompanyLogo.png";
                            }
                            else
                            {
                                item.avatarUser = item.avatarUser;
                            }
                        }
                        //  listAll = api.data.data;
                        //if (pageKH.SelectedPage == 0)
                        //    pageKH.TotalRecords = (int)api.data.count;
                        // pagenumber = 
                        // if (pageKH.SelectedPage == 0) pageKH.TotalRecords = (int)api.data.count;
                        dgvCapNhapKhuonMat1.ItemsSource = listNV_All1;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        int pageNumber = 1;
        private void btnLoc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (check)
            {

                UnUpdate.Visibility = Visibility.Collapsed;
                Updated.Visibility = Visibility.Visible;
                GetListUpdatedFace(1);

            }
            else
            {
                UnUpdate.Visibility = Visibility.Visible;
                Updated.Visibility = Visibility.Collapsed;
                GetListUpdateFace(1);
            }

            //pageNumber = pageKH.SelectedPage;
        }
        private void pageKH_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            //    //getDsNhanVien("", paginNV.SelectedPage);
            // getDsListLoc(pageKH.SelectedPage);
        }
        bool check = false;
        private void Check(object sender, RoutedEventArgs e)
        {
            check = true;
        }

        private void uncheck(object sender, RoutedEventArgs e)
        {
            check = false;
        }

        private void XacNhanKhuonMatTatCa_Checked(object sender, RoutedEventArgs e)
        {
            try
            {


                //ThongBao.Visibility = Visibility.Visible;
                listId.Clear();

                foreach (var item in listNV_All)
                {
                    item.isCheck = true;
                    int id = (int)item.idQLC;
                    string idString = Convert.ToString(id);

                    listId.Add(idString + ",");

                }
                for (int i = 0; i < listId.Count; i++)
                {
                    if (listId[i].EndsWith(",") && i == listId.Count - 1)
                    {
                        listId[i] = listId[i].Substring(0, listId[i].Length - 1);
                    }
                    //listString = Convert.ToString(listString);
                }
                listid = string.Join("", listId);
                //dgvCapNhapKhuonMat.ItemsSource = null;
                //dgvCapNhapKhuonMat.ItemsSource = listNV_All;
                dgvCapNhapKhuonMat.Items.Refresh();
            }
            catch
            {

            }
        }

        private void XacNhanKhuonMatTatCa_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var item in listNV_All)
            {
                item.isCheck = false;
            }
            dgvCapNhapKhuonMat.Items.Refresh();
        }
        List<string> listId = new List<string>();
        string listid;
        private void XacNhanKhuonMat_Checked(object sender, RoutedEventArgs e)
        {
            //XacNhanKhuonMat.isc
            int id = (int)((API_ListAll.NV)dgvCapNhapKhuonMat.SelectedItem).idQLC;
            //listId.Clear();
            if (listId.Count == 0)
            {
                listId.Add(id.ToString());
                listid = string.Join("", listId);
            }
            else if (listId.Count > 0)
            {
                listId.Add("," + id.ToString());
                //for (int i = 0; i < listId.Count; i++)
                //{
                //    if (listId[i].EndsWith(",") && i == listId.Count - 1)
                //    {
                //        listId[i] = listId[i].Substring(0, listId[i].Length - 1);
                //    }
                //    //listString = Convert.ToString(listString);
                //}
                listid = string.Join("", listId);
            }





        }

        private void XacNhanKhuonMat_Unchecked(object sender, RoutedEventArgs e)
        {
            int id = (int)((API_ListAll.NV)dgvCapNhapKhuonMat.SelectedItem).idQLC;
            listId.Clear();
            listId.Remove(id.ToString());
            listid = string.Join("", listId);
        }

        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ThongBao.Visibility = Visibility.Collapsed;
        }

        private void btnDuyet_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucDuyetNoti uc = new ucDuyetNoti(Main, listid, this);
            // Main.grShowPopup.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.grShowPopup.Children.Add(Content as UIElement);
        }

        private void dgvCapNhapKhuonMat1_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {

                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    var scrollViewer = FindVisualChild<ScrollViewer>(dgvCapNhapKhuonMat1);
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

        private void dgvCapNhapKhuonMat_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {

                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    var scrollViewer = FindVisualChild<ScrollViewer>(dgvCapNhapKhuonMat);
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




        //phan trang

    }
}
