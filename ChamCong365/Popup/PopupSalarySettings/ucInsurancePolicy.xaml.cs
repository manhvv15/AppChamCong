using ChamCong365.Popup.PopupSalarySettings;
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

namespace ChamCong365.Popup.ListTabInsurance
{
    /// <summary>
    /// Interaction logic for ucInsurancePolicy.xaml
    /// </summary>
    public partial class ucInsurancePolicy : UserControl
    {
        MainWindow Main;
       
        public ucInsurancePolicy(MainWindow main)
        {
            InitializeComponent();
            Main = main;
        }

        private void bodMethondSalaryInput_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListMethondSalaryInput.Visibility == Visibility.Collapsed)
            {
                bodListMethondSalaryInput.Visibility = Visibility.Visible;
                bodControlSalaryInput.Visibility = Visibility.Visible;
                bodListMethondSalaryBasic.Visibility = Visibility.Collapsed;
                bodControlSalaryBasic.Visibility = Visibility.Collapsed;
                bodListMethond.Visibility = Visibility.Collapsed;
                bodControl.Visibility = Visibility.Collapsed;
            }
            else
            {
                bodListMethondSalaryInput.Visibility = Visibility.Collapsed;
                bodControlSalaryBasic.Visibility = Visibility.Visible;
            }
        }

        private void bodMethondSalaryBasic_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListMethondSalaryBasic.Visibility == Visibility.Collapsed)
            {
                bodListMethondSalaryBasic.Visibility = Visibility.Visible;
                bodControlSalaryBasic.Visibility = Visibility.Visible;
                bodListMethondSalaryInput.Visibility = Visibility.Collapsed;
                bodControlSalaryInput.Visibility = Visibility.Collapsed;
                bodListMethond.Visibility = Visibility.Collapsed;
                bodControl.Visibility = Visibility.Collapsed;
            }
            else
            {
                bodListMethondSalaryBasic.Visibility = Visibility.Collapsed;
                bodControlSalaryBasic.Visibility = Visibility.Visible;
                
            }
        }

        private void bodMethond_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListMethond.Visibility == Visibility.Collapsed)
            {
                bodListMethond.Visibility = Visibility.Visible;
                bodControl.Visibility = Visibility.Visible;
                bodListMethondSalaryInput.Visibility = Visibility.Collapsed;
                bodControlSalaryInput.Visibility = Visibility.Collapsed;
                bodListMethondSalaryBasic.Visibility = Visibility.Collapsed;
                bodControlSalaryBasic.Visibility = Visibility.Collapsed;
            }
            else
            {
                bodListMethond.Visibility = Visibility.Collapsed;
                bodControl.Visibility = Visibility.Collapsed;
                
            }
        }

        private void dopAddSaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucAddSaffForInsurance(Main));
        }

        private void dopListSaffInsurance_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucListSaffInsurance());
        }
    }
}
