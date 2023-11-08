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
    /// <summary>
    /// Interaction logic for ucFunctionCompanyManager.xaml
    /// </summary>
    public partial class ucFunctionCompanyManager : UserControl
    {
        MainWindow Main;

        public ucFunctionCompanyManager(MainWindow main)
        {
            InitializeComponent();
            this.Main = main;

        }

        private void wapbuttonWorkShiftManager_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucShiftWorkManager uc = new ucShiftWorkManager(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void wapChildCompanyManager_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucChildCompanyManagerment uc = new ucChildCompanyManagerment();
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void wapDepartmentManager_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucDepartmentManager uc = new ucDepartmentManager(this.Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }


    }
}
