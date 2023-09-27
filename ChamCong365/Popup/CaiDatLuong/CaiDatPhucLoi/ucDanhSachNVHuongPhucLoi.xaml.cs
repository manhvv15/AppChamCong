using ChamCong365.OOP.CaiDatLuong.CaiDatLuongCB;
using ChamCong365.Popup.ChamCong;
using ChamCong365.Popup.PopupSalarySettings;
using ChamCong365.SalarySettings;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChamCong365.Popup.CaiDatLuong.CaiDatPhucLoi
{
    /// <summary>
    /// Interaction logic for ucDanhSachNVHuongPhucLoi.xaml
    /// </summary>
    public partial class ucDanhSachNVHuongPhucLoi : UserControl
    {
        
        MainWindow Main;
        public List<OOP.CaiDatLuong.PhucLoi.clsNVTrongPL.ListUserFinal> lstFinal = new List<OOP.CaiDatLuong.PhucLoi.clsNVTrongPL.ListUserFinal>();
        private OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf PhucLoi = new OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf();
        private OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa PhuCap = new OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa();
        public ucDanhSachNVHuongPhucLoi(MainWindow main, OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf pl)
        {
            InitializeComponent();
            Main = main;
            PhucLoi = pl;
            textTd.Text = pl.cl_name;
            LoadDLNhanVienPL();
            dgvListSaffInsurance.Visibility = Visibility.Visible;
            dgvPC.Visibility = Visibility.Collapsed;
        }
        public ucDanhSachNVHuongPhucLoi(MainWindow main,frmDanhSachPhuCap frmpc, OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa pc)
        {
            InitializeComponent();
            Main = main;
            PhuCap = pc;
            textTd.Text = pc.cl_name;
            dgvListSaffInsurance.Visibility = Visibility.Collapsed;
            dgvPC.Visibility = Visibility.Visible;
            LoadDLNhanVienPC();
        }

        private void LoadDLNhanVienPC()
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/take_list_nv_nhom")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("cls_id_cl", PhuCap.cl_id);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.PhucLoi.clsNVTrongPL.Root nvpl = JsonConvert.DeserializeObject<OOP.CaiDatLuong.PhucLoi.clsNVTrongPL.Root>(b);
                    if (nvpl.listUserFinal != null)
                    {
                        foreach (var item in nvpl.listUserFinal)
                        {
                            item.cl_salary = PhucLoi.cl_salary.ToString();
                            item.cl_day = PhucLoi.cl_day.ToString();
                            item.cl_day_end = PhucLoi.cl_day_end.ToString();
                            foreach (var dep in item.department)
                            {
                                item.dep_id = dep.dep_id.ToString();
                                item.dep_name = dep.dep_name;
                            }
                            WebClient httpClient2 = new WebClient();
                            httpClient2.QueryString.Clear();
                            httpClient2.QueryString.Add("ID", item._id.ToString());
                            var response = httpClient2.UploadValues(new Uri("http://43.239.223.142:9000/api/users/GetInfoUser"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                            APIUser receiveInfoAPI = JsonConvert.DeserializeObject<APIUser>(UnicodeEncoding.UTF8.GetString(response));
                            if (receiveInfoAPI.data != null)
                            {
                                item.avatarUser = receiveInfoAPI.data.user_info.AvatarUser;
                            }
                            lstFinal.Add(item);

                        }
                        dgvPC.ItemsSource = lstFinal;
                    }
                }
            }
            catch
            {

            }
        }

        private void LoadDLNhanVienPL()
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/take_list_nv_nhom")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("cls_id_cl", PhucLoi.cl_id);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.PhucLoi.clsNVTrongPL.Root nvpl = JsonConvert.DeserializeObject<OOP.CaiDatLuong.PhucLoi.clsNVTrongPL.Root>(b);
                    if (nvpl.listUserFinal != null)
                    {
                        foreach (var item in nvpl.listUserFinal)
                        {
                            item.cl_salary = PhucLoi.cl_salary.ToString();
                            item.cl_day = PhucLoi.cl_day.ToString();
                            item.cl_day_end = PhucLoi.cl_day_end.ToString();
                            foreach (var dep in item.department)
                            {
                                item.dep_id = dep.dep_id.ToString();
                                item.dep_name = dep.dep_name;
                            }
                            WebClient httpClient2 = new WebClient();
                            httpClient2.QueryString.Clear();
                            httpClient2.QueryString.Add("ID", item._id.ToString());
                            var response = httpClient2.UploadValues(new Uri("http://43.239.223.142:9000/api/users/GetInfoUser"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                            APIUser receiveInfoAPI = JsonConvert.DeserializeObject<APIUser>(UnicodeEncoding.UTF8.GetString(response));
                            if (receiveInfoAPI.data != null)
                            {
                                item.avatarUser = receiveInfoAPI.data.user_info.AvatarUser;
                            }
                            lstFinal.Add(item);

                        }
                        dgvListSaffInsurance.ItemsSource = lstFinal;
                    }
                }

            }
            catch
            {

            }
        }

        private void bodSuaNhanVienPhucLoi_MouseUp(object sender, MouseButtonEventArgs e)
        {
            grLoadNhanVienPhucLoi.Children.Add(new ucChinhSuaNVPhucLoi(Main));
        }

        private void bodXoaNVPhucLoi_MouseUp(object sender, MouseButtonEventArgs e)
        {
            OOP.CaiDatLuong.PhucLoi.clsNVTrongPL.ListUserFinal xoa=(sender as Border).DataContext as OOP.CaiDatLuong.PhucLoi.clsNVTrongPL.ListUserFinal;
            if (xoa != null)
            {
                grLoadNhanVienPhucLoi.Children.Add(new ucXoaPhucLoiCuaNhanVien(Main, this, xoa, PhucLoi.cl_id));

            }
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void dgvListSaffInsurance_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }

        private void dgvPC_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }
    }
}
