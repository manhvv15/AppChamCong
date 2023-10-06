using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChamCong365.Popup.PopupTimeKeeping;
using ChamCong365.TimeKeeping;

namespace ChamCong365.Popup.PopupTimeKeeping
{
    /// <summary>
    /// Interaction logic for ucCreateSaff.xaml
    /// </summary>
    public partial class ucCreateSaff : UserControl
    {
        MainWindow Main;
        //List<Saff> itemsSaff = new List<Saff>();
        public ucCreateSaff(MainWindow main)
        {
            InitializeComponent();
            Main = main;

             
        }

        private void ExitCreateSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodButonAddFileSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucCreateFileSaff());
        }

        private void bodExitCreateSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }
    }
}
