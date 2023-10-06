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

namespace ChamCong365.Login
{
    /// <summary>
    /// Interaction logic for ucChooseSignUpOptions.xaml
    /// </summary>
    public partial class ucChooseSignUpOptions : Window
    {
        MainWindow Main;
        public ucChooseSignUpOptions(MainWindow main)
        {
            InitializeComponent();
            Main = main;    
        }

        private void LoginEp(object sender, MouseButtonEventArgs e)
        {

        }

        private void LoginCom(object sender, MouseButtonEventArgs e)
        {
            InitializeComponent();
            LoginCompany ucbodyhome = new LoginCompany(this.Main);
            Main.dopBody.Children.Clear();
            object Content = ucbodyhome.Content;
            ucbodyhome.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }
    }
}
