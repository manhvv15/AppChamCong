using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.Tax;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatThue
{
    /// <summary>
    /// Interaction logic for PopUpDanhSachNVTrongThue.xaml
    /// </summary>
    public partial class PopUpDanhSachNVTrongThue : UserControl
    {
        private MainWindow Main;
        private string Id;
        public List<OOP.CaiDatLuong.Tax.clsStaffInTax.ListUserFinal> lstStaff = new List<OOP.CaiDatLuong.Tax.clsStaffInTax.ListUserFinal>();
        public List<OOP.CaiDatLuong.Tax.clsStaffInTax.ListUserClass> lstStaffClass = new List<OOP.CaiDatLuong.Tax.clsStaffInTax.ListUserClass>();
        public List<OOP.CaiDatLuong.Tax.clsStaffInTax.ListUserInTax> lstNhanVienTrongThue = new List<OOP.CaiDatLuong.Tax.clsStaffInTax.ListUserInTax>();
        public List<OOP.CaiDatLuong.Tax.clsStaffInTax.ListUserInTax> query = new List<OOP.CaiDatLuong.Tax.clsStaffInTax.ListUserInTax>();

        private string cls_Day = "";
        private string cls_Day_End = "";
        public PopUpDanhSachNVTrongThue(MainWindow main, string id, string name,string day,string dayend)
        {
            InitializeComponent();
            Main = main;
            Id = id;
            cls_Day = day;
            cls_Day_End = dayend;
            textTieuDe.Text = name;
            LoadDataStaffInTax();
            txbCountSaff.Text = lstStaff.Count.ToString();
        }
        public void LoadDataStaffInTax()
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("https://api.timviec365.vn/api/tinhluong/congty/take_list_nv_tax")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("cls_id_cl", Id);
                    request.AddParameter("com_id", Main.IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.Tax.clsStaffInTax.Root staffintax = JsonConvert.DeserializeObject<OOP.CaiDatLuong.Tax.clsStaffInTax.Root>(b);
                    if (staffintax != null)
                    {
                        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                        lstStaff = staffintax.listUserFinal;
                        lstStaffClass = staffintax.listUserClass;
                        var query = from e in lstStaff
                                    join p in lstStaffClass on e.idQLC equals p.cls_id_user
                                    select new OOP.CaiDatLuong.Tax.clsStaffInTax.ListUserInTax
                                    {
                                        idQLC = e.idQLC,
                                        
                                        cls_id = p.cls_id,
                                        avatarUser = e.avatarUser_format.ToString(),
                                        userName = e.userName,
                                        cls_salary = p.cls_salary,
                                        cls_day = TimeZoneInfo.ConvertTimeFromUtc(p.cls_day, vietnamTimeZone),
                                        cls_day_end = TimeZoneInfo.ConvertTimeFromUtc(p.cls_day_end, vietnamTimeZone),
                                        cls_day_format = $"{TimeZoneInfo.ConvertTimeFromUtc(p.cls_day, vietnamTimeZone).Month}-{TimeZoneInfo.ConvertTimeFromUtc(p.cls_day, vietnamTimeZone).Year}",
                                        cls_day_end_format = $"{TimeZoneInfo.ConvertTimeFromUtc(p.cls_day_end, vietnamTimeZone).Month}-{TimeZoneInfo.ConvertTimeFromUtc(p.cls_day_end, vietnamTimeZone).Year}"
                                    };
                        if (query != null)
                        {
                            lstNhanVienTrongThue = query.ToList();
                            foreach (var item in lstNhanVienTrongThue)
                            {
                                if (item.cls_day_end_format == "1-1970")
                                {
                                    item.cls_day_end_format = "Chưa cập nhật";
                                }
                            }
                        }
                        dgvStaffInTax.ItemsSource = lstNhanVienTrongThue;
                    }
                }
            }
            catch (Exception)
            {
            }   
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodEditInsuranceSaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            clsStaffInTax.ListUserInTax nv = (sender as Border).DataContext as clsStaffInTax.ListUserInTax;
            if (nv != null)
            {
                Main.grShowPopup.Children.Add(new PopUpTGApDung(Main, this, nv));
            }
            
        }

        private void bodDeletes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            clsStaffInTax.ListUserInTax nv = (sender as Border).DataContext as clsStaffInTax.ListUserInTax;
            if (nv != null)
            {
                Main.grShowPopup.Children.Add(new PopUpHoiTruocKhiXoaCaiDatThue(Main, nv, Id, this));
            }
        }
    }
}
