using ChamCong365.OOP.CaiDatLuong.CaiDatLuongCB;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace ChamCong365.Popup.CaiDatLuong.CaiDatThue
{
    /// <summary>
    /// Interaction logic for PopUpDanhSachNVTrongThue.xaml
    /// </summary>
    public partial class PopUpDanhSachNVTrongThue : UserControl
    {
        private MainWindow Main;
        private string Id;
        public List<OOP.CaiDatLuong.Tax.clsStaffInTax.ListUserFinal> lstStaff = new List<OOP.CaiDatLuong.Tax.clsStaffInTax.ListUserFinal>();
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
        }

        private void LoadDataStaffInTax()
        {
            using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/take_list_nv_tax")))
            {
                RestRequest request = new RestRequest();
                request.Method = Method.Post;
                request.AlwaysMultipartFormData = true;
                
                request.AddParameter("cls_id_cl", Id);
                request.AddParameter("token", Properties.Settings.Default.Token);
                RestResponse resAlbum = restclient.Execute(request);
                var b = resAlbum.Content;
                OOP.CaiDatLuong.Tax.clsStaffInTax.Root staffintax = JsonConvert.DeserializeObject<OOP.CaiDatLuong.Tax.clsStaffInTax.Root>(b);
                if (staffintax.listUserFinal != null)
                {
                    foreach (var item in staffintax.listUserFinal)
                    {
                        item.cl_day = cls_Day;
                        item.cl_day_end = cls_Day_End;
                        WebClient httpClient2 = new WebClient();
                        httpClient2.QueryString.Clear();
                        httpClient2.QueryString.Add("ID", item._id.ToString());
                        var response = httpClient2.UploadValues(new Uri("http://43.239.223.142:9000/api/users/GetInfoUser"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                        APIUser receiveInfoAPI = JsonConvert.DeserializeObject<APIUser>(UnicodeEncoding.UTF8.GetString(response));
                        if (receiveInfoAPI.data != null)
                        {
                            if (receiveInfoAPI.data.user_info.AvatarUser == null)
                            {
                                item.avatarUser = "/Resource/image/llll.jpg";
                            }
                            else
                            {
                                item.avatarUser = receiveInfoAPI.data.user_info.AvatarUser;
                            }
                        }
                        
                        lstStaff.Add(item);

                    }
                    dgvStaffInTax.ItemsSource = lstStaff;

                }
            }
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        private void dgvStaffInTax_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }

        private void bodEditInsuranceSaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void bodDeletes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.CaiDatLuong.Tax.clsStaffInTax.ListUserFinal staff = (sender as Border).DataContext as OOP.CaiDatLuong.Tax.clsStaffInTax.ListUserFinal;
            if (staff != null)
            {
                Main.grShowPopup.Children.Add(new PopUpHoiTruocKhiXoaCaiDatThue(Main, staff, Id, this));
            }
        }
    }
}
