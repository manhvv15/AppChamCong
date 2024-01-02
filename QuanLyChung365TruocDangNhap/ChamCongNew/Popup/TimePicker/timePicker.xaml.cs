using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.TimePicker
{
    /// <summary>
    /// Interaction logic for customTimePicker.xaml
    /// </summary>
    public partial class timePicker : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _text = "--:-- --";
        BrushConverter br = new BrushConverter();
        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged("Text"); }
        }
        public string tempText { get; set; }
        public List<string> Hours { get; set; }
        public List<string> Minutes { get; set; }
        public List<string> MidDay { get; set; }

        private int _demHours;

        public int demHours
        {
            get { return _demHours; }
            set { _demHours = value; }
        }
        private int _demMinutes;

        public int demMinutes
        {
            get { return _demMinutes; }
            set { _demMinutes = value; }
        }
        public string hour = "--";
        public string minute = "--";
        public string midday = "--";
        public timePicker()
        {
            InitializeComponent();
            Hours = new List<string>() { "01", "02", "03", "04", "05", "06", "07" };
            Minutes = new List<string>() { "00", "01", "02", "03", "04", "05", "06" };
            MidDay = new List<string>() { "AM", "PM" };
            lsvTimePicker_MidDay.ItemsSource = MidDay;
            lsvTimePicker_Hour.ItemsSource = Hours;
            lsvTimePicker_Minute.ItemsSource = Minutes;
        }

        private void ListViewHour_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0)
            {
                demHours++;
                if (demHours > 12)
                {
                    demHours = 1;
                }
            }
            if (e.Delta > 0)
            {
                demHours--;
                if (demHours < 1)
                {
                    demHours = 12;
                }
            }
            Hours.Clear();
            for (int i = 0; i < 7; i++)
            {
                if (i + demHours < 10)
                    Hours.Add("0" + (i + demHours));
                else
                {
                    if (i + demHours <= 12)
                        Hours.Add((i + demHours).ToString());
                    else
                    {
                        Hours.Add("0" + (i + demHours - 12));
                    }
                }
            }
            Hours = Hours.ToList();
            lsvTimePicker_Hour.ItemsSource = Hours;
            this.Text = Hours[0] + ":" + minute + " " + midday;
            hour = Hours[0];
            var firstItem = lsvTimePicker_Hour.Items[0];

            // Apply the custom template to the first item
            var firstItemContainer = lsvTimePicker_Hour.ItemContainerGenerator.ContainerFromItem(firstItem) as ListViewItem;
            if (firstItemContainer != null)
            {
                firstItemContainer.ContentTemplate = lsvTimePicker_Hour.Resources["FirstItemTemplate"] as DataTemplate;
            }


        }
        private void ListViewMinute_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

            if (e.Delta < 0)
            {
                demMinutes++;
                if (demMinutes > 59)
                {
                    demMinutes = 0;
                }
            }
            if (e.Delta > 0)
            {
                demMinutes--;
                if (demMinutes < 0)
                {
                    demMinutes = 59;
                }
            }
            Minutes.Clear();
            for (int i = 0; i < 7; i++)
            {
                if (i + demMinutes < 10)
                    Minutes.Add("0" + (i + demMinutes));
                else
                {
                    if (i + demMinutes <= 59)
                        Minutes.Add((i + demMinutes).ToString());
                    else
                    {
                        Minutes.Add("0" + (i + demMinutes - 60));
                    }
                }
            }
            Minutes = Minutes.ToList();
            lsvTimePicker_Minute.ItemsSource = Minutes;
            this.Text = hour + ":" + Minutes[0] + " " + midday;
            minute = Minutes[0];
            var firstItem = lsvTimePicker_Minute.Items[0];

            // Apply the custom template to the first item
            var firstItemContainer = lsvTimePicker_Minute.ItemContainerGenerator.ContainerFromItem(firstItem) as ListViewItem;
            if (firstItemContainer != null)
            {
                firstItemContainer.ContentTemplate = lsvTimePicker_Hour.Resources["FirstItemTemplate"] as DataTemplate;
            }
        }

        private void lsvTimePicker_Hour_Loaded(object sender, RoutedEventArgs e)
        {
            if (lsvTimePicker_Hour.Items.Count > 0)
            {
                // Get the first item
                var firstItem = lsvTimePicker_Hour.Items[0];

                // Apply the custom template to the first item
                var firstItemContainer = lsvTimePicker_Hour.ItemContainerGenerator.ContainerFromItem(firstItem) as ListViewItem;
                if (firstItemContainer != null)
                {
                    firstItemContainer.ContentTemplate = lsvTimePicker_Hour.Resources["FirstItemTemplate"] as DataTemplate;
                }
            }
        }

        private void lsvTimePicker_Minute_Loaded(object sender, RoutedEventArgs e)
        {
            if (lsvTimePicker_Minute.Items.Count > 0)
            {
                // Get the first item
                var firstItem = lsvTimePicker_Minute.Items[0];

                // Apply the custom template to the first item
                var firstItemContainer = lsvTimePicker_Minute.ItemContainerGenerator.ContainerFromItem(firstItem) as ListViewItem;
                if (firstItemContainer != null)
                {
                    firstItemContainer.ContentTemplate = lsvTimePicker_Hour.Resources["FirstItemTemplate"] as DataTemplate;
                }
            }
        }
        private void lsvTimePicker_MidDay_Loaded(object sender, RoutedEventArgs e)
        {
            if (lsvTimePicker_MidDay.Items.Count > 0)
            {
                // Get the first item
                var firstItem = lsvTimePicker_MidDay.Items[0];

                // Apply the custom template to the first item
                var firstItemContainer = lsvTimePicker_MidDay.ItemContainerGenerator.ContainerFromItem(firstItem) as ListViewItem;
                if (firstItemContainer != null)
                {
                    firstItemContainer.ContentTemplate = lsvTimePicker_Hour.Resources["FirstItemTemplate"] as DataTemplate;
                }
            }
        }

        private void lsvTimePicker_MidDay_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var firstItem = lsvTimePicker_MidDay.Items[0];
            if (firstItem.ToString() == "AM")
            {
                MidDay = new List<string> { "PM", "AM" };
                lsvTimePicker_MidDay.ItemsSource = MidDay;

                this.Text = hour + ":" + minute + " " + MidDay[0];
                midday = MidDay[0];
                firstItem = lsvTimePicker_MidDay.Items[0];
                var firstItemContainer = lsvTimePicker_MidDay.ItemContainerGenerator.ContainerFromItem(firstItem) as ListViewItem;
                if (firstItemContainer != null)
                {
                    firstItemContainer.ContentTemplate = lsvTimePicker_Hour.Resources["FirstItemTemplate"] as DataTemplate;
                }
            }
            else
            {
                MidDay = new List<string> { "AM", "PM" };
                lsvTimePicker_MidDay.ItemsSource = MidDay;
                lsvTimePicker_MidDay.ItemsSource = MidDay;

                this.Text = hour + ":" + minute + " " + MidDay[0];
                midday = MidDay[0];

                firstItem = lsvTimePicker_MidDay.Items[0];
                var firstItemContainer = lsvTimePicker_MidDay.ItemContainerGenerator.ContainerFromItem(firstItem) as ListViewItem;
                if (firstItemContainer != null)
                {
                    firstItemContainer.ContentTemplate = lsvTimePicker_Hour.Resources["FirstItemTemplate"] as DataTemplate;
                }
            }

        }

        private void lsvTimePicker_Hour_MouseEnter(object sender, MouseEventArgs e)
        {
            var lisview = (ListView)sender;
            var firstItem = lisview.Items[0];
            var firstItemContainer = lsvTimePicker_MidDay.ItemContainerGenerator.ContainerFromItem(firstItem) as Border;
            var a = 1;
        }

        private void lsvTimePicker_Hour_MouseLeave(object sender, MouseEventArgs e)
        {

        }
        #region Hover event
        private void bod_MidDay_MouseEnter(object sender, MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border bod_MidDay = FindChild<Border>(row, "bod_MidDay");
                TextBlock tb_MidDay = FindChild<TextBlock>(row, "tb_MidDay");
                if (bod_MidDay != null && tb_MidDay != null)
                {
                    bod_MidDay.Background = (Brush)br.ConvertFrom("#4C5BD4");
                    tb_MidDay.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
                }
            }
        }

        private void bod_MidDay_MouseLeave(object sender, MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border bod_MidDay = FindChild<Border>(row, "bod_MidDay");
                TextBlock tb_MidDay = FindChild<TextBlock>(row, "tb_MidDay");
                if (bod_MidDay != null && tb_MidDay != null)
                {
                    bod_MidDay.Background = (Brush)br.ConvertFrom("#FFFFFF");
                    tb_MidDay.Foreground = (Brush)br.ConvertFrom("#474747");
                }
            }
        }
        private void bod_Minute_MouseEnter(object sender, MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border bod_Minute = FindChild<Border>(row, "bod_Minute");
                TextBlock tb_Minute = FindChild<TextBlock>(row, "tb_Minute");
                if (bod_Minute != null && tb_Minute != null)
                {
                    bod_Minute.Background = (Brush)br.ConvertFrom("#4C5BD4");
                    tb_Minute.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
                }
            }
        }

        private void bod_Minute_MouseLeave(object sender, MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border bod_Minute = FindChild<Border>(row, "bod_Minute");
                TextBlock tb_Minute = FindChild<TextBlock>(row, "tb_Minute");
                if (bod_Minute != null && tb_Minute != null)
                {
                    bod_Minute.Background = (Brush)br.ConvertFrom("#FFFFFF");
                    tb_Minute.Foreground = (Brush)br.ConvertFrom("#474747");
                }
            }
        }
        private void bod_Hour_MouseEnter(object sender, MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border bod_Hour = FindChild<Border>(row, "bod_Hour");
                TextBlock tb_Hour = FindChild<TextBlock>(row, "tb_Hour");
                if (bod_Hour != null && tb_Hour != null)
                {
                    bod_Hour.Background = (Brush)br.ConvertFrom("#4C5BD4");
                    tb_Hour.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
                }
            }
        }
        private void bod_Hour_MouseLeave(object sender, MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border bod_Hour = FindChild<Border>(row, "bod_Hour");
                TextBlock tb_Hour = FindChild<TextBlock>(row, "tb_Hour");
                if (bod_Hour != null && tb_Hour != null)
                {
                    bod_Hour.Background = (Brush)br.ConvertFrom("#FFFFFF");
                    tb_Hour.Foreground = (Brush)br.ConvertFrom("#474747");
                }
            }
        }

        // Hàm giúp tìm kiếm đối tượng con trong VisualTree
        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T typedChild && (child as FrameworkElement)?.Name == childName)
                {
                    return typedChild;
                }
                else
                {
                    T childOfChild = FindChild<T>(child, childName);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        //// Hàm giúp tìm cha của một đối tượng trong VisualTree
        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }



        #endregion

        public void lsvTimePicker_Hour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = lsvTimePicker_Hour.SelectedItem;
            if (selectedItem != null)
            {
                hour = selectedItem.ToString();
                this.Text = hour + ":" + minute + " " + midday;
            }
        }

        public void lsvTimePicker_Minute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = lsvTimePicker_Minute.SelectedItem;
            if (selectedItem != null)
            {

                minute = selectedItem.ToString();
                this.Text = hour + ":" + minute + " " + midday;
            }
        }

        private void lsvTimePicker_MidDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = lsvTimePicker_MidDay.SelectedItem;
            if (selectedItem != null)
            {

                midday = selectedItem.ToString();
                this.Text = hour + ":" + minute + " " + midday;

            }
        }
    }
}
