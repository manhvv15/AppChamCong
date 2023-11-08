using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using ChamCong365.Popup.ListTabInsurance;

namespace ChamCong365.SalarySettings
{
    /// <summary>
    /// Interaction logic for ucInsuranceSettings.xaml
    /// </summary>
    public partial class ucInsuranceSettings : UserControl
    {
        BrushConverter bru = new BrushConverter();
        MainWindow Main;
        public ucInsuranceSettings( MainWindow main)
        {
            InitializeComponent();
            Main = main;
            ucInsurancePolicy ucI = new ucInsurancePolicy(Main);
            grLoadFormInsurance.Children.Clear();
            object Content = ucI.Content;
            ucI.Content = null;
            grLoadFormInsurance.Children.Add(Content as UIElement);
            bodInsurancePolicy.BorderThickness = new Thickness(0, 0, 0, 5);
            bodInsurancePolicy.BorderBrush = (Brush)bru.ConvertFrom("#4C5BD4");
            txbTextInsurancePolicy.Foreground = (Brush)bru.ConvertFrom("#4C5BD4");
            txbListSaffYesSettings.Foreground = (Brush)bru.ConvertFrom("#474747");
            txbListSaffNotSettings.Foreground = (Brush)bru.ConvertFrom("#474747");
            bodAddInsurance.Visibility = Visibility.Visible;
        }

        private void bodInsurancePolicy_MouseUp(object sender, MouseButtonEventArgs e)
        {
           ucInsurancePolicy ucI = new ucInsurancePolicy(Main);
            grLoadFormInsurance.Children.Clear();
            object Content = ucI.Content;
            ucI.Content = null;
            grLoadFormInsurance.Children.Add(Content as UIElement);
            bodInsurancePolicy.BorderThickness = new Thickness(0, 0, 0, 5);
            bodInsurancePolicy.BorderBrush = (Brush)bru.ConvertFrom("#4C5BD4");
            txbTextInsurancePolicy.Foreground = (Brush)bru.ConvertFrom("#4C5BD4");
            bodAddInsurance.Visibility = Visibility.Visible;

            bodListSaffNotSettings.BorderThickness = new Thickness(0, 0, 0, 1);
            bodListSaffNotSettings.BorderBrush = (Brush)bru.ConvertFrom("#DDDDDD");
            txbListSaffNotSettings.Foreground = (Brush)bru.ConvertFrom("#474747");
             
            bodListSaffYesSettings.BorderThickness = new Thickness(0, 0, 0, 1);
            bodListSaffYesSettings.BorderBrush = (Brush)bru.ConvertFrom("#DDDDDD");
            txbListSaffYesSettings.Foreground = (Brush)bru.ConvertFrom("#474747");
        }
    

        private void bodListSaffNotSettings_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucListSaffNotSettings ucI = new ucListSaffNotSettings(Main);
            grLoadFormInsurance.Children.Clear();
            object Content = ucI.Content;
            ucI.Content = null;
            grLoadFormInsurance.Children.Add(Content as UIElement);
            bodListSaffNotSettings.BorderThickness = new Thickness(0, 0, 0, 5);
            bodListSaffNotSettings.BorderBrush = (Brush)bru.ConvertFrom("#4C5BD4");
            txbListSaffNotSettings.Foreground = (Brush)bru.ConvertFrom("#4C5BD4");
            bodAddInsurance.Visibility = Visibility.Collapsed;

            bodListSaffYesSettings.BorderThickness = new Thickness(0, 0, 0, 1);
            bodListSaffYesSettings.BorderBrush = (Brush)bru.ConvertFrom("#DDDDDD");
            txbListSaffYesSettings.Foreground = (Brush)bru.ConvertFrom("#474747");

            bodInsurancePolicy.BorderThickness = new Thickness(0, 0, 0, 1);
            bodInsurancePolicy.BorderBrush = (Brush)bru.ConvertFrom("#DDDDDD");
            txbTextInsurancePolicy.Foreground = (Brush)bru.ConvertFrom("#474747");
        }

        private void bodListSaffYesSettings_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucListSaffYesSettings ucI = new ucListSaffYesSettings();
            grLoadFormInsurance.Children.Clear();
            object Content = ucI.Content;
            ucI.Content = null;
            grLoadFormInsurance.Children.Add(Content as UIElement);
            bodListSaffYesSettings.BorderThickness = new Thickness(0, 0, 0, 5);
            bodListSaffYesSettings.BorderBrush = (Brush)bru.ConvertFrom("#4C5BD4");
            txbListSaffYesSettings.Foreground = (Brush)bru.ConvertFrom("#4C5BD4");

            bodListSaffNotSettings.BorderThickness = new Thickness(0, 0, 0, 1);
            bodListSaffNotSettings.BorderBrush = (Brush)bru.ConvertFrom("#DDDDDD");
            txbListSaffNotSettings.Foreground = (Brush)bru.ConvertFrom("#474747");

            bodInsurancePolicy.BorderThickness = new Thickness(0, 0, 0, 1);
            bodInsurancePolicy.BorderBrush = (Brush)bru.ConvertFrom("#DDDDDD");
            txbTextInsurancePolicy.Foreground = (Brush)bru.ConvertFrom("#474747");

        }

        private void bodAddInsurance_MouseEnter(object sender, MouseEventArgs e)
        {
            bodAddInsurance.BorderThickness = new Thickness(1);
        }

        private void bodAddInsurance_MouseLeave(object sender, MouseEventArgs e)
        {
            bodAddInsurance.BorderThickness = new Thickness(0);
        }
    }
}
