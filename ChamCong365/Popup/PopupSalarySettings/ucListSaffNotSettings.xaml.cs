using ChamCong365.TimeKeeping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChamCong365.Popup.ListTabInsurance
{
    /// <summary>
    /// Interaction logic for ucListSaffNotSettings.xaml
    /// </summary>
    /// 
    public class InsuranceSafff
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string PhongBan { get; set; }
        public string Anh { get; set; }
    }
    public partial class ucListSaffNotSettings : System.Windows.Controls.UserControl
    {
        MainWindow Main;
        int months, years;
        List<InsuranceSafff> itemsis = new List<InsuranceSafff>();
        List<string> year = new List<string>() { "Năm 2023", "Năm 2024", "Năm 2025", "Năm 2026" };
        List<string> month = new List<string>() { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
        List<string> ListRoom = new List<string>() { " Kỹ Thuật", "Kinh Doanh & Bán Hàng", "Đề Án", "Sale", "Đào tạo", "Sáng Tạo" };
        List<Saff> itemsSaff = new List<Saff>();
        public ucListSaffNotSettings(MainWindow main)
        {
            InitializeComponent();
            Main = main;

            itemsSaff.Add(new Saff() { IdSaff = 50001, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto001@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50002, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto002@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50003, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto003@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50004, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto004@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50004, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto005@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50005, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto006@gmail.com", NumPhoneSaff = "0393039393" });
            itemsis.Add(new InsuranceSafff() { Name = "Nguyễn Văn A", PhongBan = "Phòng 13", ID = 100001, Anh = "./Resource/image/KyTo.jpg" });
            itemsis.Add(new InsuranceSafff() { Name = "Nguyễn Văn B", PhongBan = "Phòng 13", ID = 200002, Anh = "./Resource/image/KyTo.jpg" });
            itemsis.Add(new InsuranceSafff() { Name = "Nguyễn Văn C", PhongBan = "Phòng 13", ID = 300003, Anh = "./Resource/image/KyTo.jpg" });
            itemsis.Add(new InsuranceSafff() { Name = "Nguyễn Văn D", PhongBan = "Phòng 13", ID = 400004, Anh = "./Resource/image/KyTo.jpg" });
            itemsis.Add(new InsuranceSafff() { Name = "Nguyễn Văn E", PhongBan = "Phòng 13", ID = 500005, Anh = "./Resource/image/KyTo.jpg" });
            itemsis.Add(new InsuranceSafff() { Name = "Nguyễn Văn F", PhongBan = "Phòng 13", ID = 600006, Anh = "./Resource/image/KyTo.jpg" });
            itemsis.Add(new InsuranceSafff() { Name = "Nguyễn Văn I", PhongBan = "Phòng 13", ID = 700007, Anh = "./Resource/image/KyTo.jpg" });
            itemsis.Add(new InsuranceSafff() { Name = "Nguyễn Văn K", PhongBan = "Phòng 13", ID = 800008, Anh = "./Resource/image/KyTo.jpg" });
            lsvLoadSaffs.ItemsSource = itemsis;
            lsvLoadMont.ItemsSource = month;
            lsvLoadYear.ItemsSource = year;
            lsvLoadListRoom.ItemsSource = ListRoom;
            var namesaff = from saff in itemsSaff select saff.NameSaff;
            lsvLoadNameSaffInsurance.ItemsSource = namesaff;
            DateTime now = DateTime.Now;
            months = now.Month;
            years = now.Year;
            txbSelectYear.Text = "Năm " + years;
            txbSelectMonth.Text = "Tháng " + months;
            

        }
       

        private void bodSelectMonth_MouseUp(object sender, MouseButtonEventArgs e)
        {

            if (bodSelectMontlv.Visibility == Visibility.Collapsed)
            {
                bodSelectMontlv.Visibility = Visibility.Visible;
                bodSelectYearlv.Visibility = Visibility.Collapsed;
                bodListNameSaffInsurance.Visibility = Visibility.Collapsed;
                bodSelectRooms.Visibility = Visibility.Collapsed;
            }
            else
            {
                bodSelectMontlv.Visibility = Visibility.Collapsed;
            }  
        }

        private void lsvLoadMont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectMonth.Text = lsvLoadMont.SelectedItem.ToString();
            bodSelectMontlv.Visibility=Visibility.Collapsed;
        }

        private void bodSelectYear_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodSelectYearlv.Visibility == Visibility.Collapsed)
            {
                bodSelectYearlv.Visibility = Visibility.Visible;
                bodSelectMontlv.Visibility = Visibility.Collapsed;
                bodListNameSaffInsurance.Visibility = Visibility.Collapsed;
                bodSelectRooms.Visibility = Visibility.Collapsed;
            }
            else
            {
                bodSelectYearlv.Visibility = Visibility.Collapsed;
            }
        }

        private void lsvLoadListRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtSelectRooms.Text = lsvLoadListRoom.SelectedItem.ToString();
            bodSelectRooms.Visibility=Visibility.Collapsed;
        }

        private void bodSelectRoom_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodSelectRooms.Visibility == Visibility.Collapsed)
            {
                bodSelectRooms.Visibility = Visibility.Visible;
                bodListNameSaffInsurance.Visibility = Visibility.Collapsed;
                bodSelectYearlv.Visibility = Visibility.Collapsed;
                bodSelectMontlv.Visibility = Visibility.Collapsed;
            }
            else
            {
                bodSelectRooms.Visibility = Visibility.Collapsed;
            }
        }

        private void lsvLoadNameSaffInsurance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbNameSaffInsurance.Text = lsvLoadNameSaffInsurance.SelectedItem.ToString();
            bodListNameSaffInsurance.Visibility = Visibility.Collapsed;
        }

        private void bodSelectNameSaffInsurance_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListNameSaffInsurance.Visibility == Visibility.Collapsed)
            {
                bodListNameSaffInsurance.Visibility = Visibility.Visible;
                bodSelectYearlv.Visibility = Visibility.Collapsed;
                bodSelectMontlv.Visibility = Visibility.Collapsed;
                bodSelectRooms.Visibility = Visibility.Collapsed;
            }
            else
            {
                bodListNameSaffInsurance.Visibility = Visibility.Collapsed;
            }
        }

        private void bodSetupInsurance_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucSetupInsurance ucs = new ucSetupInsurance();
           //Main.grShowPopup.Children.Add(new ucSetupInsurance());
          
        }

        private void lsvLoadSaffs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ucSetupInsurance ucs = new ucSetupInsurance();
            Main.grShowPopup.Children.Add(new ucSetupInsurance());

          
        }

        private void bodSetupInsurance_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            
        }

        private void bodSetupInsurance_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void lsvLoadYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectYear.Text = lsvLoadYear.SelectedItem.ToString();
            bodSelectYearlv.Visibility=Visibility.Collapsed;
        }
    }
}
