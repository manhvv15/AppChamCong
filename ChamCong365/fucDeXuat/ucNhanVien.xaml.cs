using ChamCong365.fucDeXuat;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChamCong365
{
    /// ucNhanVien
    public partial class ucNhanVien : UserControl
    {
        
        List<String> itemsNhanVien = new List<String>() { "Tất cả nhân viên", "Hồ Mạnh Hùng ", "Hồ Mạnh Hùng", "Hồ Mạnh Hùng", "Hồ Mạnh Hùng" };
        List<String> itemsPhongBan = new List<String>() { "Tất cả", "Kỹ Thuật", "Biên Tập", "Kinh doanh", "Đề Án", "Phòng Seo", "Phòng Đào Tạo", "Phòng Sáng Tạo" };

        
      
        public class QNC
        {
            public int STT { get; set; }
            public int ID { get; set; }
            public string HoVaTen { get; set; }
            public string PhongBan { get; set; }
            public string Chucvu { get; set; }
        }


        public class QMC
        {
            public int STT { get; set; }
            public int ID { get; set; }
            public string HoVaTen { get; set; }
            public string PhongBan { get; set; }
            public string Chucvu { get; set; }
        }
        private MainWindow Main;
        public ucNhanVien(MainWindow main)
        {

            InitializeComponent();
            Main = main;
            frmDeXuatNghiPhepQuaNhieuCap frm = new frmDeXuatNghiPhepQuaNhieuCap(main);
            pnlHienThi.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            pnlHienThi.Children.Add(content as UIElement);

            BrushConverter brus = new BrushConverter();
            textNhieuCap.Foreground = (Brush)brus.ConvertFrom("#4c5bd4");
            borNhieuCap.BorderBrush = (Brush)brus.ConvertFrom("#4c5bd4");
            #region FakeInfor
            List<QNC> listPhongBan = new List<QNC>();


            List<QMC> listNV = new List<QMC>();

            #endregion

            listPhongBan.Add(new QNC() { STT = 1, ID = 441444, HoVaTen = "Lê Nhật Minh", PhongBan = "KỸ THUẬT", Chucvu = "Tổ Trưởng" });
            listPhongBan.Add(new QNC() { STT = 1, ID = 441444, HoVaTen = "Lê Nhật Minh", PhongBan = "KỸ THUẬT", Chucvu = "Tổ Trưởng" });
            listPhongBan.Add(new QNC() { STT = 1, ID = 441444, HoVaTen = "Lê Nhật Minh", PhongBan = "KỸ THUẬT", Chucvu = "Tổ Trưởng" });
            listPhongBan.Add(new QNC() { STT = 1, ID = 441444, HoVaTen = "Lê Nhật Minh", PhongBan = "KỸ THUẬT", Chucvu = "Tổ Trưởng" });
          

            listNV.Add(new QMC() { STT = 1, ID = 441444, HoVaTen = "Lê Nhật Minh", PhongBan = "KỸ THUẬT", Chucvu = "Tổ Trưởng" });
            listNV.Add(new QMC() { STT = 1, ID = 441444, HoVaTen = "Lê Nhật Minh", PhongBan = "KỸ THUẬT", Chucvu = "Nhân Viên Chính Thức" });
            listNV.Add(new QMC() { STT = 1, ID = 441444, HoVaTen = "Lê Nhật Minh", PhongBan = "KỸ THUẬT", Chucvu = "Tổ Trưởng" });
            listNV.Add(new QMC() { STT = 1, ID = 441444, HoVaTen = "Lê Nhật Minh", PhongBan = "KỸ THUẬT", Chucvu = "Tổ Trưởng" });
            listNV.Add(new QMC() { STT = 1, ID = 441444, HoVaTen = "Lê Nhật Minh", PhongBan = "KỸ THUẬT", Chucvu = "Tổ Phó" });


            

        }

        private void bodDXNPQNC_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetDefaultMenuColor();
            ChangeBorderColor((Border)sender);
            SetAllTableCollapsed();

            frmDeXuatNghiPhepQuaNhieuCap frm = new frmDeXuatNghiPhepQuaNhieuCap(Main);
            pnlHienThi.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            pnlHienThi.Children.Add(content as UIElement);


        }

        private void bodDuyetDeXuatNP1C_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetDefaultMenuColor();
            ChangeBorderColor((Border)sender);
            SetAllTableCollapsed();
            frmDeXuatNghiPhepQuaMotCap frm = new frmDeXuatNghiPhepQuaMotCap(Main);
            pnlHienThi.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            pnlHienThi.Children.Add(content as UIElement);



        }


        public void ChangeBorderColor(Border border)
        {
            border.BorderThickness = new Thickness(0, 0, 0, 5);
            border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
            ((TextBlock)border.Child).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
        }

        ///<summary>
        /// set default color menu
        /// </summary>
        public void SetDefaultMenuColor()
        {
            foreach (var child in GridMenu.Children)
            {
                if (child is Border)
                {
                    var border = (Border)child;
                    border.BorderThickness = new Thickness(0, 0, 0, 1);
                    border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#474747"));
                    ((TextBlock)border.Child).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#474747"));

                }
            }
        }


        public void SetAllTableCollapsed()
        {
            
        }

        private void Border_MouseLeftButtonUp_DeleteCollapsed(object sender, MouseButtonEventArgs e)
        {
        }

        private void checkManager_Checked(object sender, RoutedEventArgs e)
        {
            
        }






        private void bodSelectNV_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }



        private void bodAddNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }


        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }


        private void Image_MouseLeftButtonUp_SelectChonNhanVien(object sender, MouseButtonEventArgs e)
        {
            
        }



        private void lsvNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Border_MouseLeftButtonUp_Exit(object sender, MouseButtonEventArgs e)
        {

            
        }




        private void Image_MouseLeftButtonUp_SelectPhongBan(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void lsvPhongBan1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void bodCancel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void bodDeleteListDecvice_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void checkManager_Unchecked(object sender, RoutedEventArgs e)
        {
            
        }

        private void bodOkMessageNoAll_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void bodOkMessageYesAll_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

		private void Border_MouseLeftButtonUp_Huy(object sender, MouseButtonEventArgs e)
		{

        }

		private void Border_MouseLeftButtonUp_DongY(object sender, MouseButtonEventArgs e)
		{
        }

		private void bodCheckBoxAll_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
            
        }

		private void checkManagerSelected_Unchecked(object sender, RoutedEventArgs e)
		{
           
        }

		private void checkManagerSelected_Checked(object sender, RoutedEventArgs e)
		{
            
        }
	}
}

	
        

    

	

      

		

       

       
		


