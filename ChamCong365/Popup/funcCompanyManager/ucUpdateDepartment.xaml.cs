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

namespace ChamCong365.Popup.funcCompanyManager
{
    /// <summary>
    /// Interaction logic for ucUpdateDepartment.xaml
    /// </summary>
    public partial class ucUpdateDepartment : UserControl
    {
        private ucListDpartment _ucListDpartment;
        public ucUpdateDepartment(ucListDpartment ucListDpartment)
        {
            InitializeComponent();
            List<string> dsTenCongTy = new List<string>() { "Hà hung 2", "PT shop", "PT Shop 2", "PT Shop 3" };
            editArea_lsvCompany.ItemsSource = dsTenCongTy;  
        }

        private void bodExitPopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void EditArea_bodSelectCompany_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (editArea_bodListCompany.Visibility == Visibility.Collapsed)
            {
                editArea_bodListCompany.Visibility = Visibility.Visible;
            }
            else
            {
                editArea_bodListCompany.Visibility -= Visibility.Collapsed;

            }
        }

        private void EditArea_lsvCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            editArea_txbSelectCompany.Text = editArea_lsvCompany.SelectedItem.ToString();
            editArea_bodListCompany.Visibility = Visibility.Collapsed;
        }
    }
}

