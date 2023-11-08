using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChamCong365.Popup.funcCompanyManager
{
    /// <summary>
    /// Interaction logic for ucCreateDepartment.xaml
    /// </summary>
    public partial class ucCreateDepartment : UserControl
    {
        private ucListDpartment _ucListDpartment;
        public ucCreateDepartment(ucListDpartment ucListDpartment)
        {
            InitializeComponent();
            List<string> dsTenCongTy = new List<string>() { "Hà hung 2", "PT shop", "PT Shop 2", "PT Shop 3", "aaa" };
            CreateArea_lsvCompany.ItemsSource = dsTenCongTy;
        }

        private void bodExitPopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void CreateArea_bodSelectCompany_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CreateArea_bodListCompany.Visibility == Visibility.Collapsed)
            {
                CreateArea_bodListCompany.Visibility = Visibility.Visible;
            }
            else
            {
                CreateArea_bodListCompany.Visibility -= Visibility.Collapsed;

            }
        }

        private void CreateArea_lsvCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CreateArea_txbSelectCompany.Text = CreateArea_lsvCompany.SelectedItem.ToString();
            CreateArea_bodListCompany.Visibility = Visibility.Collapsed;
        }
    }
}
