using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Comons;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Entities;
using Newtonsoft.Json;
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
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.funcQuanLyCongTy.ThietLapCongTy.Comons
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
            public string Key { get; set; } //Key = "" Là tất cả  Key = "0" là tất cả đã lưu
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
        public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register("ItemsSource", typeof(List<ItemCbx>), typeof(ucComboboxMuiltiSelect));

        private List<ItemCbx> _SelectList = new List<ItemCbx>();
        public List<ItemCbx> SelectList
        {
            get
            {
                return _SelectList;
            }
            set
            {

                _SelectList = value;
                OnPropertyChanged(nameof(SelectList));
                listXetDuyt.ItemsSource = value;
                listXetDuyt.Items.Refresh();
            }
        }

        public List<ItemCbx> SelectedList
        {
            get
            {
                foreach (var item in _SelectList)
                {
                    if (item.Key == "") return new List<ItemCbx>();
                }
                return _SelectList;
            }
        }
        public SelectionChangedEventHandler SelectionChange
        {
            get { return (SelectionChangedEventHandler)GetValue(SelectionChangeProperty); }
            set { SetValue(SelectionChangeProperty, value); }
        }
        public static readonly DependencyProperty SelectionChangeProperty =
        DependencyProperty.Register("SelectionChange", typeof(SelectionChangedEventHandler), typeof(ucComboboxMuiltiSelect));

        public DependencyPropertyChangedEventHandler NumSelectChanged
        {
            get { return (DependencyPropertyChangedEventHandler)GetValue(NumSelectChangedProperty); }
            set { SetValue(NumSelectChangedProperty, value); }
        }
        public static readonly DependencyProperty NumSelectChangedProperty =
        DependencyProperty.Register("NumSelectChanged", typeof(DependencyPropertyChangedEventHandler), typeof(ucComboboxMuiltiSelect));

        public ucComboboxMuiltiSelect()
        {
            InitializeComponent();
            this.DataContext = this;
            //getNgDuyet();
        }

        //public async void getNgDuyet()
        //{
        //    try
        //    {

        //        var searchObject = new
        //        {
        //            ep_status = "Active",
        //            pageSize = 10000


        //        };
        //        string searchJson = JsonConvert.SerializeObject(searchObject, Formatting.Indented);

        //        var client = new HttpClient();
        //        var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.Api_DanhSachNhanVien);
        //        request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

        //        var content = new StringContent(searchJson, null, "application/json");
        //        request.Content = content;
        //        var response = await client.SendAsync(request);
        //        var resSaff = await response.Content.ReadAsStringAsync();

        //        if (response.IsSuccessStatusCode)
        //        {
        //            Root_NhanVien resultSaff = JsonConvert.DeserializeObject<Root_NhanVien>(resSaff);

        //            // Xử lý phản hồi ở đây
        //            if (resultSaff.data.data != null)
        //            {
        //                foreach (var item in resultSaff.data.data)
        //                {
        //                    MainList.Add(new ItemCbx() { Key = item.ep_id.ToString(), Value = item.userName });
        //                }

        //                lsvNguoiXtDuyet.ItemsSource = MainList;
        //                //listUsersTheoDoi = lsvNguoiTheoDoi;
        //            }
        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Đã xảy ra lỗi lấy danh sách ng duyệt " + ex.Message);
        //    }
        //}
        private void grNgD1(object sender, MouseButtonEventArgs e)
        {
            if (borNgD.Visibility == Visibility.Collapsed)
            {

            }
            else
            {

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

                listXetDuyt.Visibility = Visibility.Collapsed;
                //textChonNgD.Text = "";

            }
            if (SelectionChange != null) SelectionChange(this, null);
        }
        private void xoaAnh_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.DarkGray);
            ((Border)sender).Background = redBrush;
        }
        Brush brush = new SolidColorBrush();
        private void xoaAnh_MouseLeave(object sender, MouseEventArgs e)
        {

            textChonNgD.Focus();
            //xoaAnh.bac
            SolidColorBrush grayBrush = new SolidColorBrush(Color.FromRgb(220, 220, 220)); // Màu #dcdcdc
            ((Border)sender).Background = grayBrush;
            if (SelectList.Count == 0)
            {

                textChonNgD.Focus();
            }
            if (SelectList.Count == 0)
            {
                textDuye.Text = "Chọn ";
            }
        }
        private void lsvNguoiXtDctionChanged(object sender, SelectionChangedEventArgs e)
        {
            toggleButton.IsChecked = false;
            if (lsvNguoiXtDuyet.SelectedItem != null)
            {

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

            if (SelectionChange != null) SelectionChange(this, null);
            lsvNguoiXtDuyet.SelectedIndex = -1;
        }

        private void textChonNgD_TextChanged(object sender, TextChangedEventArgs e)
        {
            toggleButton.IsChecked = false;
            toggleButton.IsChecked = true;
            List<ItemCbx> ItemCbxTimKiem = new List<ItemCbx>();
            string searchText = textChonNgD.Text.ToString().ToLower().RemoveUnicode();
            foreach (var str in ItemsSource)
            {
                if (str.Value.ToLower().RemoveUnicode().Contains(searchText))
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

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the Popup state
            toggleButton.IsChecked = true;

        }

        private void toggleButton_Checked(object sender, RoutedEventArgs e)
        {
            textChonNgD.Focus();
        }

        private void listXetDuyt_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }


        // Hàm giúp tìm kiếm đối tượng con trong VisualTree

        //// Hàm giúp tìm cha của một đối tượng trong VisualTree
    }

}
