using System.Diagnostics;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.ChamCongBangQRRR.Function;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.ChamCongBangTaiKhoanCongTy.Function;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.ChamCongKhuonMat.Function;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.LichSu.Function;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.Propose;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
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
//using NPOI.SS.Formula.Functions;
using QuanLyChung365TruocDangNhap.ChamCongNew.Login;
//using DocumentFormat.OpenXml.Spreadsheet;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.TinhLuong;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien
{
    /// <summary>
    /// Interaction logic for ChamCongBangQR.xaml
    /// </summary>
    public partial class ChamCongBangQR : Window
    {
        MainChamCong Main;
        BrushConverter bcBody = new BrushConverter();
        public ChamCongBangQR(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
           // Main.Back = 1;
        }

        private void grChamCong_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 1;
            listChamCong uc = new listChamCong(Main);
            grLoadFunctionQR.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            grLoadFunctionQR.Children.Add(Content as UIElement);
            txt2.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt3.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt4.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt5.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt6.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt1.Foreground = (Brush)bcBody.ConvertFrom("#4c5bd4 ");
            txt7.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = "Chấm công bằng QR";

        }
        private void Grid_MouseUp_2(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 2;
            listKhuonMat uc = new listKhuonMat(Main);
            grLoadFunctionQR.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            grLoadFunctionQR.Children.Add(Content as UIElement);
            txt1.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt3.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt4.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt5.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt6.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt2.Foreground = (Brush)bcBody.ConvertFrom("#4c5bd4 ");
            txt7.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = "Chấm công bằng nhận diện khuôn mặt";

        }
        private void Grid_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 3;
            listCompany uc = new listCompany(Main);
            grLoadFunctionQR.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            grLoadFunctionQR.Children.Add(Content as UIElement);
            txt3.Foreground = (Brush)bcBody.ConvertFrom("#4c5bd4 ");
            txt1.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt2.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt4.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt5.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt6.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt7.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = "Chấm công bằng tài khoản công ty";
        }
        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 4;
           // Test1 uc = new Test1(Main);
            listPropose uc = new listPropose(Main);
            grLoadFunctionQR.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            grLoadFunctionQR.Children.Add(Content as UIElement);
            txt4.Foreground = (Brush)bcBody.ConvertFrom("#4c5bd4 ");
            txt1.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt3.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt2.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt5.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt6.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt7.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = "Tạo đề xuất";

        }

        private void grChamCong5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 6;
            listHistory uc = new listHistory(Main,null);
            grLoadFunctionQR.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            grLoadFunctionQR.Children.Add(Content as UIElement);
           
            txt2.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt3.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt4.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt5.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt1.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt6.Foreground = (Brush)bcBody.ConvertFrom("#4c5bd4 ");
            txt7.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = "Lịch sử";
        }

        private void txt1_MouseLeave(object sender, MouseEventArgs e)
        {
            //txt1.Foreground = (Brush)bcBody.ConvertFrom("red ");

        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                /*ucChooseLoginOptions uc = new ucChooseLoginOptions();
           //     ucChoo
                string token = uc.acc_token;
                string link = $"https://hungha365.com/phan-mem-cham-cong/cap-nhat-du-lieu-khuon-mat?refresh_token={Properties.Settings.Default.Token}";
                Process.Start(link);
                //     Process.Start("https://hungha365.com/phan-mem-cham-cong/cap-nhat-du-lieu-khuon-mat");*/
                popup.Children.Add(new NotificationDownLoadChat365(this));
                popup.Visibility = Visibility.Visible;
            }catch(Exception ex)
            {

            }


        }

        private void grChamCong6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 7;
            ucListTinhLuong uc = new ucListTinhLuong(Main);
            grLoadFunctionQR.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            grLoadFunctionQR.Children.Add(Content as UIElement);

            txt2.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt3.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt4.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt5.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt1.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt6.Foreground = (Brush)bcBody.ConvertFrom("#474747 ");
            txt7.Foreground = (Brush)bcBody.ConvertFrom("#4c5bd4 ");
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = "Lịch sử";
        }
    }
}
