
using ChamCong365.NhanVien.ChamCongBangQRRR.Function;
using ChamCong365.NhanVien.ChamCongBangTaiKhoanCongTy.Function;
using ChamCong365.NhanVien.ChamCongKhuonMat.Function;
using ChamCong365.NhanVien.LichSu.Function;
using ChamCong365.NhanVien.Propose;
using ChamCong365.NhanVien.Tool;
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

namespace ChamCong365.NhanVien
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
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = "Chấm công";

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
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = "Chấm công";
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
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = "Chấm công";

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
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = "Chấm công";

        }

        private void grChamCong5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 6;
            listHistory uc = new listHistory(Main);
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
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = "Chấm công";
        }

        private void txt1_MouseLeave(object sender, MouseEventArgs e)
        {
            //txt1.Foreground = (Brush)bcBody.ConvertFrom("red ");

        }

    }
}
