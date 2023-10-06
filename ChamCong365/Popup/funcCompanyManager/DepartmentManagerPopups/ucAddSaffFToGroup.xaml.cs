using ChamCong365.TimeKeeping;
using MaterialDesignThemes.Wpf;
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

namespace ChamCong365.Popup.PopupSalarySettings
{
    /// <summary>
    /// Interaction logic for ucAddSaffFToGroup.xaml
    /// </summary>
    /// 

    public partial class ucAddSaffFToGroup : UserControl
    {
        //List<Saff> itemsSaff = new List<Saff>();
        List<string> itemGround = new List<string>() {"Nhóm 1", "Nhóm 2", "Nhóm 3", "Nhóm 4", "Nhóm 5", "Nhóm 6", "Nhóm 7" };
        BrushConverter br = new BrushConverter();
        MainWindow Main;
       
        public ucAddSaffFToGroup(MainWindow main)
        {
            InitializeComponent();
            Main = main;
           
            
            bodSelectSaff.BorderThickness = new Thickness(0, 0, 0, 3);
            bodSelectSaff.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            txbSaff.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void ExitCreateSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodSelectSaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodSelectSaff.BorderThickness = new Thickness(0,0,0,3);
            bodSelectSaff.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            txbSaff.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
            stpLoadListSaff.Visibility = Visibility.Visible;

        }


        private void bodNextSaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

    }
}
