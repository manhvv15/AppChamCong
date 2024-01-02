using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;


namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.PopupTimeKeeping
{

    /// <summary>
    /// Interaction logic for ucListSaff.xaml
    /// </summary>
    public partial class ucListSaff : UserControl
    {
        MainWindow Main;
        //List<Saff> itemsSaff = new List<Saff>();
        public ucListSaff(MainWindow main)
        {

            InitializeComponent();
            Main = main;
            
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
