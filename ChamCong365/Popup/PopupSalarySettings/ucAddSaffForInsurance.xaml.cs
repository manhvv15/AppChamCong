using ChamCong365.TimeKeeping;
using MaterialDesignThemes.Wpf;
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

namespace ChamCong365.Popup.PopupSalarySettings
{
    /// <summary>
    /// Interaction logic for ucAddSaffForInsurance.xaml
    /// </summary>
    /// 
  
    public partial class ucAddSaffForInsurance : UserControl
    {
        List<Saff> itemsSaff = new List<Saff>();
        List<string> itemGround = new List<string>() {"Nhóm 1", "Nhóm 2", "Nhóm 3", "Nhóm 4", "Nhóm 5", "Nhóm 6", "Nhóm 7" };
        BrushConverter br = new BrushConverter();
        MainWindow Main;
       
        public ucAddSaffForInsurance(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            #region FakeSaff
            itemsSaff.Add(new Saff() { IdSaff = 50001, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto001@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50002, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto002@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50003, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto003@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50004, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto004@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50004, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto005@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50005, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto006@gmail.com", NumPhoneSaff = "0393039393" });
            #endregion
            var countsaff =  itemsSaff.Count();
            lsvListSaff.ItemsSource = itemsSaff;
            lsvListGround.ItemsSource = itemGround;
            bodSelectSaff.BorderThickness = new Thickness(0, 0, 0, 3);
            bodSelectGround.BorderThickness = new Thickness(0);
            bodSelectSaff.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            txbSaff.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void ExitCreateSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodSelectSaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodSelectSaff.BorderThickness = new Thickness(0,0,0,3);
            bodSelectGround.BorderThickness = new Thickness(0);
            bodSelectSaff.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            txbSaff.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
            txbGround.Foreground = (Brush)br.ConvertFrom("#474747");
            stpLoadListSaff.Visibility = Visibility.Visible;
            stpLoadListGround.Visibility = Visibility.Collapsed;

        }

        private void bodSelectGround_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodSelectGround.BorderThickness = new Thickness(0, 0, 0, 3);
            bodSelectSaff.BorderThickness = new Thickness(0);
            bodSelectGround.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            txbGround.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
            txbSaff.Foreground = (Brush)br.ConvertFrom("#474747");
            stpLoadListSaff.Visibility = Visibility.Collapsed;
            stpLoadListGround.Visibility = Visibility.Visible;
        }

        private void bodNextSaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           Main.grShowPopup.Children.Add(new ucNextSaffInsurance(Main));
            
        }

        private void bodNextGroundInsurance_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucNextGroundInsurance(Main));
        }
    }
}
