using ChamCong365.OOP.CaiDatLuong.CaiDatLuongCB;
using Newtonsoft.Json;
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
    /// Interaction logic for PopUpAddStaffInTax.xaml
    /// </summary>
    public partial class PopUpAddStaffInTax : UserControl
    {
        BrushConverter br = new BrushConverter();
        MainWindow Main;
        private string IdNV = "";
        private int IdThue;
        private List<OOP.clsNhanVienThuocCongTy.ListUser> lstNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        public PopUpAddStaffInTax(MainWindow main, int IdT)
        {
            InitializeComponent();
            Main = main;
            IdThue = IdT;
            //var countsaff = itemsSaff.Count();
            //lsvListSaff.ItemsSource = itemsSaff;
            //lsvListGround.ItemsSource = itemGround;
            bodSelectSaff.BorderThickness = new Thickness(0, 0, 0, 3);
            bodSelectGround.BorderThickness = new Thickness(0);
            bodSelectSaff.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            txbSaff.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
            foreach (var item in main.lstNhanVienThuocCongTy)
            {
                if (item._id != 0)
                {
                    WebClient httpClient2 = new WebClient();
                    httpClient2.QueryString.Clear();
                    httpClient2.QueryString.Add("ID", item._id.ToString());
                    var response = httpClient2.UploadValues(new Uri("http://43.239.223.142:9000/api/users/GetInfoUser"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                    APIUser receiveInfoAPI = JsonConvert.DeserializeObject<APIUser>(UnicodeEncoding.UTF8.GetString(response));
                    if (receiveInfoAPI.data != null)
                    {
                        item.avatarUser = receiveInfoAPI.data.user_info.AvatarUser;
                        lstNV.Add(item);
                    }
                    else
                    {
                        item.avatarUser = "Resource/image/llll.jpg";
                        lstNV.Add(item);
                    }

                }
            }
            lsvListSaff.ItemsSource = lstNV;
        }
        private void bor_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void lsvListSaff_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }

        private void borNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.clsNhanVienThuocCongTy.ListUser us = (sender as Border).DataContext as OOP.clsNhanVienThuocCongTy.ListUser;
            if (us != null)
            {
                IdNV = us.idQLC.ToString();
            }
        }

        private void bodNextSaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bor.Fill = Brushes.Transparent;
            Main.grShowPopup.Children.Add(new PopUpTGApDung(Main, this, IdNV, IdThue));
        }
    }
}
