
using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.Comons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
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


namespace QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Tools
{
    /// <summary>
    /// Interaction logic for ucComboboxMuiltiSelect.xaml
    /// </summary>
    public partial class ucComboboxMuiltiSelect : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public class ItemCbx
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }


        public List<ItemCbx> ItemsSource
        {
            get { return (List<ItemCbx>)GetValue(ItemsSourceProperty); }
            set
            {

                SetValue(ItemsSourceProperty, value);
                lsvNguoiXtDuyet.ItemsSource = value;
            }
        }
        private bool islsvNguoiXtDuyetHasData = true;
        public bool IslsvNguoiXtDuyetHasData
        {
            get { return islsvNguoiXtDuyetHasData; }
            set
            {
                islsvNguoiXtDuyetHasData = value;
                OnPropertyChanged();
            }
        }
        public static readonly DependencyProperty IslsvNguoiXtDuyetHasDataProperty =
           DependencyProperty.Register("IslsvNguoiXtDuyetHasData", typeof(bool), typeof(ucComboboxMuiltiSelect));
        public Visibility PopupVisible
        {
            get { return (Visibility)GetValue(PopupVisibleProperty); }
            set { SetValue(PopupVisibleProperty, value); }
        }
        public static readonly DependencyProperty PopupVisibleProperty =
            DependencyProperty.Register("PopupVisible", typeof(Visibility), typeof(ucComboboxMuiltiSelect));

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(List<ItemCbx>), typeof(ucComboboxMuiltiSelect));

        public List<ItemCbx> SelectList = new List<ItemCbx>();

        public ucComboboxMuiltiSelect()
        {
            InitializeComponent();
            this.DataContext = this;

        }


        private void grNgD1(object sender, MouseButtonEventArgs e)
        {
            if (borNgD.Visibility == Visibility.Collapsed)
            {
                borNgD.Visibility = Visibility.Visible;
            }
            else
            {
                borNgD.Visibility = Visibility.Collapsed;
            }
        }
        bool shouldProcessEvent = true;
        private void xoaAnh_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ItemCbx index = (ItemCbx)listXetDuyt.SelectedItem;
            if (index != null)
            {
                SelectList.Remove(index);
                listXetDuyt.ClearValue(ItemsControl.ItemsSourceProperty);
                listXetDuyt.ItemsSource = SelectList;
                shouldProcessEvent = false;
            }
            shouldProcessEvent = true;
            if (SelectList.Count == 0)
            {
                borNgD.Visibility = Visibility.Visible;
                listXetDuyt.Visibility = Visibility.Collapsed;
                //textChonNgD.Text = "";

            }
        }
        private void xoaAnh_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.DarkGray);
            ((Border)sender).Background = redBrush;
        }
        Brush brush = new SolidColorBrush();
        private void xoaAnh_MouseLeave(object sender, MouseEventArgs e)
        {
            borNgD.Visibility = Visibility.Collapsed;
            textChonNgD.Focus();
            //xoaAnh.bac
            SolidColorBrush grayBrush = new SolidColorBrush(Color.FromRgb(220, 220, 220)); // Màu #dcdcdc
            ((Border)sender).Background = grayBrush;
            if (SelectList.Count == 0)
            {
                borNgD.Visibility = Visibility.Collapsed;
                textChonNgD.Focus();
            }
            if (SelectList.Count == 0)
            {
                textDuye.Text = "Chọn người xét duyệt";
            }
        }
        private void lsvNguoiXtDctionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lsvNguoiXtDuyet.SelectedItem != null)
            {
                borNgD.Visibility = Visibility.Collapsed;
                string selectedValue = ((ItemCbx)lsvNguoiXtDuyet.SelectedItem).Value;
                if (!SelectList.Any(item => item.Value == selectedValue))
                {
                    ItemCbx infor = new ItemCbx()
                    {
                        Value = ((ItemCbx)lsvNguoiXtDuyet.SelectedItem).Value,
                        Key = ((ItemCbx)lsvNguoiXtDuyet.SelectedItem).Key

                    };

                    SelectList.Add(infor);
                    SelectList = SelectList.ToList();
                    if (SelectList.Count > 0)
                    {
                        textDuye.Text = "";
                        //  grNgD.Height = 45;
                    }

                    listXetDuyt.ItemsSource = SelectList;
                    listXetDuyt.Visibility = Visibility.Visible;


                }

            }
            lsvNguoiXtDuyet.Items.Refresh();
            if (lsvNguoiXtDuyet.Items.Count > 0)
            {
                textChonNgD.Text = "";
                textChonNgD.IsReadOnly = false;
                textChonNgD.Focus();
            }
            borNgD.Visibility = Visibility.Collapsed;
        }

        private void textChonNgD_TextChanged(object sender, TextChangedEventArgs e)
        {
            borNgD.Visibility = Visibility.Visible;
            List<ItemCbx> ItemCbxTimKiem = new List<ItemCbx>();
            string searchText = textChonNgD.Text.ToString().ToLower().RemoveUnicode1();
            foreach (var str in ItemsSource)
            {
                if (str.Value.ToLower().RemoveUnicode1().Contains(searchText))
                {
                    ItemCbxTimKiem.Add(str);

                }
            }
            lsvNguoiXtDuyet.ItemsSource = ItemCbxTimKiem;

            if (textChonNgD.Text == "")
            {
                lsvNguoiXtDuyet.ItemsSource = ItemsSource;
            }
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void grNgD_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Check if the mouse click is outside the Popup
            //if (!IsMouseOverPopup(e.GetPosition(this)))
            //{
            //    // Close the Popup
            //    borNgD.Visibility = Visibility.Visible;
            //}
        }

        private void ClearSelectedList(object sender, MouseButtonEventArgs e)
        {
            SelectList.Clear();
            listXetDuyt.Items.Refresh();
            var db = ((Path)sender).DataContext;

        }

        //private bool IsMouseOverPopup(Point mousePosition)
        //{
        //    // Check if the mouse click is inside the Popup
        //    var commonAncestor = FindCommonAncestor(grNgD, borNgD);
        //    if (commonAncestor != null)
        //    {
        //        var grNgDPosition = grNgD.TransformToVisual((Visual)commonAncestor).TransformBounds(new Rect(0, 0, grNgD.ActualWidth, grNgD.ActualHeight));
        //        var borNgDPosition = borNgD.TransformToVisual((Visual)commonAncestor).TransformBounds(new Rect(0, 0, borNgD.ActualWidth, borNgD.ActualHeight));

        //        return grNgDPosition.Contains(mousePosition) || borNgDPosition.Contains(mousePosition);
        //    }

        //    return false;
        //}

        //private DependencyObject FindCommonAncestor(DependencyObject visual1, DependencyObject visual2)
        //{
        //    var ancestors1 = new HashSet<DependencyObject>();

        //    while (visual1 != null)
        //    {
        //        ancestors1.Add(visual1);
        //        visual1 = VisualTreeHelper.GetParent(visual1);
        //    }

        //    while (visual2 != null)
        //    {
        //        if (ancestors1.Contains(visual2))
        //        {
        //            return visual2;
        //        }
        //        visual2 = VisualTreeHelper.GetParent(visual2);
        //    }

        //    return null;
        //}

        // Hàm giúp tìm kiếm đối tượng con trong VisualTree

        //// Hàm giúp tìm cha của một đối tượng trong VisualTree
        ///

    }

}
