using ChamCong365.Salarysettings;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ChamCong365.TimeKeeping;

namespace ChamCong365
{
    /// <summary>
    /// Interaction logic for ucBodyHome.xaml
    /// </summary>
    public partial class ucBodyHome : UserControl
    {
        MainWindow Main;
        BrushConverter bcBody = new BrushConverter();
        
        public ucBodyHome(MainWindow main)
        {
            InitializeComponent();
            Main = main;
           
        }     
        private void bodSalarySettings_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucListSalarySettings uc = new ucListSalarySettings(Main);
            grLoadFunction.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            grLoadFunction.Children.Add(Content as UIElement);
            txbChamCong.Foreground = (Brush)bcBody.ConvertFrom("#474747");
            txbSalarySettings.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");

        }

        private void bodFunctionTimeKeeping_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucListTimeKeeping uc = new ucListTimeKeeping(Main);
            grLoadFunction.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            grLoadFunction .Children.Add(Content as UIElement);
            txbChamCong.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
            txbSalarySettings.Foreground = (Brush)bcBody.ConvertFrom("#474747");
          
        }
    } 
}
