using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.Tools
{
    /// <summary>
    /// Interaction logic for TimePicker.xaml
    /// </summary>
    public partial class TimePicker : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _text = "";
        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged("Text"); }
        }
        private string _hour = "00";
        string Hour = "00";
        string Minute = "00";
        public TimePicker()
        {
            InitializeComponent();

            List<string> listHours = new List<string>() { "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", " ", "  ", "   ", "    ", "     ", "       " };
            List<string> listMinutes = new List<string>() { "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", " ", "  ", "   ", "    ", "     ", "      " };

            lsvHours.ItemsSource = listHours;
            lsvMinutes.ItemsSource = listMinutes;
        }
        private void lsvHours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lsvHours.SelectedItem != null)
            {
                int index = lsvHours.Items.IndexOf(lsvHours.SelectedItem);
                if (index <= 23)
                {
                    int index1 = index + 6;
                    lsvHours.ScrollIntoView(lsvHours.Items[index1]);
                    Hour = lsvHours.SelectedItem.ToString();
                }
            }
            UpdateText();
        }
        private void lsvMinutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsvMinutes.SelectedItem != null)
            {
                int index = lsvMinutes.Items.IndexOf(lsvMinutes.SelectedItem);
                if (index <= 59)
                {
                    lsvMinutes.ScrollIntoView(lsvMinutes.Items[index + 6]);
                    Minute = lsvMinutes.SelectedItem.ToString();
                }

            }
            UpdateText();
        }
        private void UpdateText()
        {
            Text = Hour + ":" + Minute;

        }

        private void TimePickerNow_MouseUP(object sender, MouseButtonEventArgs e)
        {
            Text = DateTime.Now.ToLocalTime().ToString("HH:mm");
            this.Visibility = Visibility.Collapsed;
        }

        private void Ok_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

    }
}
