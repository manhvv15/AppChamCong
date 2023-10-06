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
    /// Interaction logic for Body.xaml
    /// </summary>
    public partial class Body : UserControl
    {
        public Body()
        {
            InitializeComponent();
        }
        private void wapbuttonucNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucNhanVien uc = new ucNhanVien();
            dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            dopBody.Children.Add(Content as UIElement);
        }

 

        private void wapInstallTime_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucInstallTime uc = new ucInstallTime();
            dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            dopBody.Children.Add(Content as UIElement);
        }

        private void wapAdvancemoney_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucAdvancemoney uc = new ucAdvancemoney();
            dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            dopBody.Children.Add(Content as UIElement);
        }

	 private void wapUpdateFace_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucUpdateFace uc = new ucUpdateFace();
            dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            dopBody.Children.Add(Content as UIElement);
        }
    }
}
