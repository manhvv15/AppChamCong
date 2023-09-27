using ChamCong365.funcQuanLyCongTy.AddNewStaffTabList;
using ChamCong365.Popup.funcCompanyManager;
using ChamCong365.Popup.funcCompanyManager.AddNewStaffPopups;
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

namespace ChamCong365.funcQuanLyCongTy
{
    /// <summary>
    /// Interaction logic for ucInstallAddNewStaff.xaml
    /// </summary>
    public partial class ucInstallAddNewStaff : UserControl
    {
        MainWindow Main;
        public ucInstallAddNewStaff(MainWindow main)
        {
            InitializeComponent();
            Main = main;
        }

        private void bodToanBo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodAddStaff.Visibility = Visibility.Visible;

            SetDefaultMenuColor();
            ChangeBorderColor((Border)sender);
            ucListAllStaff uc = new ucListAllStaff(this.Main);
            stackTabBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            stackTabBody.Children.Add(Content as UIElement);
        }

        private void bodChoDuyet_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodAddStaff.Visibility = Visibility.Collapsed;
            SetDefaultMenuColor();
            ChangeBorderColor((Border)sender);
            ucListWaitingStaff uc = new ucListWaitingStaff(this.Main);
            stackTabBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            stackTabBody.Children.Add(Content as UIElement);
        }


        private void stackTabBody_Loaded(object sender, RoutedEventArgs e)
        {
            ucListAllStaff uc = new ucListAllStaff(this.Main);
            stackTabBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            stackTabBody.Children.Add(Content as UIElement);
        }
        ///<summary>
        /// Change color menu
        /// </summary>
        public void ChangeBorderColor(Border border)
        {
            border.BorderThickness = new Thickness(0, 0, 0, 5);
            border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
            ((TextBlock)border.Child).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
        }

        ///<summary>
        /// set default color menu
        /// </summary>
        public void SetDefaultMenuColor()
        {
            foreach (var child in GridMenu.Children)
            {
                if (child is Border)
                {
                    var border = (Border)child;
                    border.BorderThickness = new Thickness(0, 0, 0, 1);
                    border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#474747"));
                    ((TextBlock)border.Child).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#474747"));

                }
            }
        }

        private void bodAddStaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucAddNewStaff(Main, this));
        }
    }
}
