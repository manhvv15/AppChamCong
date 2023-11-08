using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChamCong365.TimeKeeping;


namespace ChamCong365.Popup.PopupTimeKeeping
{

    /// <summary>
    /// Interaction logic for ucListSaff.xaml
    /// </summary>
    public partial class ucListSaff : UserControl
    {
        MainWindow Main;
        List<Saff> itemsSaff = new List<Saff>();
        public ucListSaff(MainWindow main)
        {

            InitializeComponent();
            Main = main;
            itemsSaff.Add(new Saff() { IdSaff = 50001, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto001@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50002, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto002@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50003, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto003@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50004, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto004@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50004, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto005@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50005, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto006@gmail.com", NumPhoneSaff = "0393039393" });
            lsvListSaffSmall.ItemsSource = itemsSaff;
            ucInstallCalendarWork ucI = new ucInstallCalendarWork(Main);
            txbHeaderListSaff.Text = ucI.txbTextCalendarMonth.Text + ucI.txbCalendarNumMonth.Text;
            txbCountSaff.Text = "(" + itemsSaff.Count + ")";
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodDeleteSaffOnList_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucDeleteSaff());
        }
    }
}
