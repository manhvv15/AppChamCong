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

namespace ChamCong365.NhanVien.ChamCongBangTaiKhoanCongTy.Function
{
    /// <summary>
    /// Interaction logic for FunctionCompany.xaml
    /// </summary>
    public partial class FunctionCompany : Window
    {
        MainChamCong Main;
        public FunctionCompany(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }

      


        private void Buoc31Hien_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DienThoai31.Visibility = Visibility.Visible;
            DienThoai32.Visibility = Visibility.Collapsed;
            DienThoai33.Visibility = Visibility.Collapsed;
            DienThoai34.Visibility = Visibility.Collapsed;
            DienThoai35.Visibility = Visibility.Collapsed;
            DienThoai36.Visibility = Visibility.Collapsed;

            Buoc31Hien.Visibility = Visibility.Visible;
            Buoc32Hien.Visibility = Visibility.Visible;
            Buoc33Hien.Visibility = Visibility.Visible;
            Buoc34Hien.Visibility = Visibility.Visible;
            Buoc35Hien.Visibility = Visibility.Visible;
            Buoc36Hien.Visibility = Visibility.Visible;
           
            Buoc32An.Visibility = Visibility.Collapsed;
            Buoc33An.Visibility = Visibility.Collapsed;
            Buoc34An.Visibility = Visibility.Collapsed;
            Buoc35An.Visibility = Visibility.Collapsed;
            Buoc36An.Visibility = Visibility.Collapsed;
        }

        private void Buoc32Hien_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            DienThoai32.Visibility = Visibility.Visible;
            DienThoai31.Visibility = Visibility.Collapsed;
            DienThoai33.Visibility = Visibility.Collapsed;
            DienThoai34.Visibility = Visibility.Collapsed;
            DienThoai35.Visibility = Visibility.Collapsed;
            DienThoai36.Visibility = Visibility.Collapsed;

            Buoc31Hien.Visibility = Visibility.Visible;
            Buoc32An.Visibility = Visibility.Visible;
            Buoc33Hien.Visibility = Visibility.Visible;
            Buoc34Hien.Visibility = Visibility.Visible;
            Buoc35Hien.Visibility = Visibility.Visible;
            Buoc36Hien.Visibility = Visibility.Visible;

            Buoc32Hien.Visibility = Visibility.Collapsed;
            Buoc33An.Visibility = Visibility.Collapsed;
            Buoc34An.Visibility = Visibility.Collapsed;
            Buoc35An.Visibility = Visibility.Collapsed;
            Buoc36An.Visibility = Visibility.Collapsed;
        }

        private void Buoc33Hien_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DienThoai32.Visibility = Visibility.Collapsed;
            DienThoai33.Visibility = Visibility.Visible;           
            //DienThoai31.Visibility = Visibility.Collapsed;
            //DienThoai34.Visibility = Visibility.Collapsed;
            //DienThoai35.Visibility = Visibility.Collapsed;
            //DienThoai36.Visibility = Visibility.Collapsed;

            Buoc31Hien.Visibility = Visibility.Visible;
            Buoc32An.Visibility = Visibility.Visible;
            Buoc33An.Visibility = Visibility.Visible;
            Buoc34Hien.Visibility = Visibility.Visible;
            Buoc35Hien.Visibility = Visibility.Visible;
            Buoc36Hien.Visibility = Visibility.Visible;

            Buoc32Hien.Visibility = Visibility.Collapsed;
            Buoc33Hien.Visibility = Visibility.Collapsed;
            Buoc34An.Visibility = Visibility.Collapsed;
            Buoc35An.Visibility = Visibility.Collapsed;
            Buoc36An.Visibility = Visibility.Collapsed;
        }

        private void Buoc34Hien_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
            DienThoai33.Visibility = Visibility.Collapsed;
            DienThoai34.Visibility = Visibility.Visible;
            //DienThoai31.Visibility = Visibility.Collapsed;
            //DienThoai32.Visibility = Visibility.Collapsed;
            //DienThoai35.Visibility = Visibility.Collapsed;
            //DienThoai36.Visibility = Visibility.Collapsed;

           

            Buoc31Hien.Visibility = Visibility.Visible;
            Buoc32An.Visibility = Visibility.Visible;
            Buoc32Hien.Visibility = Visibility.Collapsed;
            Buoc33An.Visibility = Visibility.Visible;
            Buoc33Hien.Visibility = Visibility.Collapsed;
            Buoc34An.Visibility = Visibility.Visible;
            Buoc34Hien.Visibility = Visibility.Collapsed;
            Buoc35Hien.Visibility = Visibility.Visible;
            Buoc36Hien.Visibility = Visibility.Visible;          
          
            Buoc35An.Visibility = Visibility.Collapsed;
            Buoc36An.Visibility = Visibility.Collapsed;
        }

        private void Buoc35Hien_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DienThoai34.Visibility = Visibility.Collapsed;
            DienThoai35.Visibility = Visibility.Visible;
            //DienThoai33.Visibility = Visibility.Collapsed;
            //DienThoai31.Visibility = Visibility.Collapsed;
            //DienThoai35.Visibility = Visibility.Collapsed;
            //DienThoai36.Visibility = Visibility.Collapsed;

            Buoc31Hien.Visibility = Visibility.Visible;
            Buoc32An.Visibility = Visibility.Visible;
            Buoc33An.Visibility = Visibility.Visible;
            Buoc34An.Visibility = Visibility.Visible;
            Buoc35An.Visibility = Visibility.Visible;
            Buoc36Hien.Visibility = Visibility.Visible;

            Buoc32Hien.Visibility = Visibility.Collapsed;
            Buoc33Hien.Visibility = Visibility.Collapsed;
            Buoc34Hien.Visibility = Visibility.Collapsed;
            Buoc35Hien.Visibility = Visibility.Collapsed;
            Buoc36An.Visibility = Visibility.Collapsed;
        }

        private void Buoc36Hien_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DienThoai35.Visibility = Visibility.Collapsed;
            DienThoai36.Visibility = Visibility.Visible;
            //DienThoai33.Visibility = Visibility.Collapsed;
            //DienThoai34.Visibility = Visibility.Collapsed;
            //DienThoai35.Visibility = Visibility.Collapsed;
            //DienThoai36.Visibility = Visibility.Collapsed;

            Buoc31Hien.Visibility = Visibility.Visible;
            Buoc32An.Visibility = Visibility.Visible;
            Buoc33An.Visibility = Visibility.Visible;
            Buoc34An.Visibility = Visibility.Visible;
            Buoc35An.Visibility = Visibility.Visible;
            Buoc36An.Visibility = Visibility.Visible;

            Buoc32Hien.Visibility = Visibility.Collapsed;
            Buoc33Hien.Visibility = Visibility.Collapsed;
            Buoc34Hien.Visibility = Visibility.Collapsed;
            Buoc35Hien.Visibility = Visibility.Collapsed;
            Buoc36Hien.Visibility = Visibility.Collapsed;
        }
    }
}
