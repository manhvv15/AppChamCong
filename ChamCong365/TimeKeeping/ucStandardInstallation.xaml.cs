using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChamCong365.TimeKeeping;

namespace ChamCong365.TimeKeeping
{
    /// <summary>
    /// Interaction logic for ucStandardInstallation.xaml
    /// </summary>
    ///   

    public partial class ucStandardInstallation : UserControl
    {
        List<string> listSearch = new List<string>();
        List<string> listYear = new List<string>() { "Năm 2023", "Năm 2024", "Năm 2025", "Năm 2026", "Năm 2027" };
        List<string> listMonth = new List<string>() { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
        public ucStandardInstallation()
        {
            InitializeComponent();
            txbNumberStandard.Focus();
            txbSelectMonth.Text = listMonth[0];
            txbSelectYear.Text = listYear[0];
            lsvListMonth.ItemsSource = listMonth;
            lsvListYear.ItemsSource = listYear;
        }
        private void lsvListMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectMonth.Text = lsvListMonth.SelectedItem.ToString();
            bodMonth.Visibility = Visibility.Collapsed;

        }
        private void bodMonth_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodMonth.Visibility == Visibility.Collapsed)
            {
                bodMonth.Visibility = Visibility.Visible;
                txtSearchMonth.Focus();
                bodYear.Visibility = Visibility.Collapsed;
            }
            else
            {
                bodMonth.Visibility -= Visibility.Collapsed;
            }
        }
        private void txtSearchMonth_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (var itemMoth in listMonth)
            {
                if (itemMoth.ToLower().Contains(txtSearchMonth.Text.ToString()))
                {
                    listSearch.Add(itemMoth);
                }

            }
            lsvListMonth.ItemsSource = listSearch;
        }
        private void txtSearchYear_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (var itemYear in listYear)
            {
                if (itemYear.ToLower().Contains(txtSearchYear.Text.ToString()))
                {
                    listSearch.Add(itemYear);
                }

            }
            lsvListYear.ItemsSource = listSearch;
        }
        private void lsvListYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectYear.Text = lsvListYear.SelectedItem.ToString();
            bodYear.Visibility = Visibility.Collapsed;
        }
        private void bodYear_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodYear.Visibility == Visibility.Collapsed)
            {
                bodYear.Visibility = Visibility.Visible;
                txtSearchYear.Focus();
                bodMonth.Visibility = Visibility.Collapsed;
            }
            else
            {
                bodYear.Visibility -= Visibility.Collapsed;
            }
        }

        private void bodSaveStandanrd_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodMessageBox.Visibility == Visibility.Collapsed)
            {
                bodMessageBox.Visibility = Visibility.Visible;
                txbUpdateNumberStandard.Text = txbNumberStandard.Text.ToString();


            }
            else
            {
                bodMessageBox.Visibility = Visibility.Collapsed;
            }
        }

        private void bodOkMessageBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodUpdateStandard.Visibility == Visibility.Collapsed)
            {
                bodMessageBox.Visibility = Visibility.Collapsed;
                bodUpdateStandard.Visibility = Visibility.Visible;
               

            }
            else
            {
                bodMessageBox.Visibility = Visibility.Collapsed;
            }
        }

        private void txbNumberStandard_GotFocus(object sender, RoutedEventArgs e)
        {
            
            
        }
    }
}
