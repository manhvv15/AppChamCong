
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace ChamCong365.Login
{
    /// <summary>
    /// Interaction logic for LoginCompany.xaml
    /// </summary>
    public partial class LoginCompany : UserControl, INotifyPropertyChanged
    {
        MainWindow Main;
        BrushConverter bc = new BrushConverter();
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _TypeLogin = 1;

        public int TypeLogin
        {
            get { return _TypeLogin; }
            set { _TypeLogin = value; OnPropertyChanged(); }
        }
        public LoginCompany(MainWindow main)
        {
            InitializeComponent();
            this.Main = main;  
        }


        private void bodBackto_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void bodBackto_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void bodBackto_MouseLeave(object sender, MouseEventArgs e)
        {

        }


        private void GuideQrCode_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           // Main.grShowPopup.Children.Add(new ucGuideQRCode());
        }

        private void SelectedACCLogin(object sender, MouseButtonEventArgs e)
        {
      
            TypeLogin = 0;
                txbQRLoginTab.Foreground = (Brush)bc.ConvertFromString("#474747");
                txbACCLoginTab.Foreground = (Brush)bc.ConvertFromString("#5A68D8");
                Login_Account.Visibility = Visibility.Visible;
                spQRCode.Visibility = Visibility.Collapsed;

        }

        private void SelectedQRLogin(object sender, MouseButtonEventArgs e)
        {

            TypeLogin = 1;
 
                txbQRLoginTab.Foreground = (Brush)bc.ConvertFromString("#5A68D8");
                txbACCLoginTab.Foreground = (Brush)bc.ConvertFromString("#474747");
                spQRCode.Visibility = Visibility.Visible;
                Login_Account.Visibility = Visibility.Collapsed;

        }
    }
}
