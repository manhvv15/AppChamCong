using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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
using static QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.KindOfDon.ĐonDoiCa;

namespace QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Popup.LichLamViecPopups
{
    /// <summary>
    /// Interaction logic for ucTaoChuKy.xaml
    /// </summary>
    public partial class ucTaoChuKy : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public class lichlamviec : INotifyPropertyChanged
        {


            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public int id;
            public int ngay { get; set; }

            public int _ca;
            public int ca
            {
                get { return _ca; }
                set
                {
                    _ca = value;
                    OnPropertyChanged();
                }
            }

            public int _status;
            public int status
            {
                get { return _status; }
                set
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }

            public bool _isCheck;
            public bool isCheck
            {
                get { return _isCheck; }
                set
                {
                    _isCheck = value;
                    OnPropertyChanged();
                }
            }
            public string listShiftIdString { get { return string.Join(",", dsca); } }
            public string ngayString { get; set; } = "";
            public List<int> dsca { get; set; }
        }

        private List<lichlamviec> _listLich;

        public List<lichlamviec> listLich
        {
            get { return _listLich; }
            set
            {
                _listLich = value;
                OnPropertyChanged("listLich");
            }
        }

        frmMain Main;
        string ten;
        string thang;
        int select;
        string date;
        int month;
        int year;
        int start;
        List<ListShiftEntities.Item> listSelectedShift = new List<ListShiftEntities.Item>();
        List<int> dsca = new List<int>();
        public ucTaoChuKy(frmMain Main, string ten, string thang, int select, string date, List<ListShiftEntities.Item> listSelectedShift)
        {
            InitializeComponent();
            this.Main = Main;
            this.ten = ten;
            this.thang = thang;
            this.select = select;
            this.date = date;
            this.listSelectedShift = listSelectedShift;
            lsvShift.ItemsSource = listSelectedShift;
            this.dsca = listSelectedShift.Select(x => (int)x.shift_id).ToList();

            this.DataContext = this;
            //lsvShift.ItemsSource = listSelectedShift;
            LoadShiftDetail();
        }
        public ucTaoChuKy()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        //khởi tạo lịch
        private void LoadShiftDetail()
        {
            month = int.Parse(DateTime.Parse(thang).ToString("MM"));
            year = int.Parse(DateTime.Parse(thang).ToString("yyyy"));
            start = (int)new DateTime(year, month, 1).DayOfWeek;
            listLich = new List<lichlamviec>();
            if (month - 1 > 0)
            {
                for (int i = 0; i < start; i++)
                {
                    var x = DateTime.DaysInMonth(year, month - 1);
                    listLich.Add(
                        new lichlamviec() { id = listLich.Count, ngay = x - i, ca = 0, status = 0 });
                }

                listLich.Reverse();
            }

            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                List<int> dsc = new List<int>();
                var d = new lichlamviec()
                { id = listLich.Count, ngay = i, ca = dsc.Count, status = 0, dsca = dsc };
                listLich.Add(d);
            }

            int n = 42 - listLich.Count;
            for (int i = 1; i <= n; i++)
            {
                var d = new lichlamviec() { id = listLich.Count, ngay = i, ca = 0, status = 0 };
                listLich.Add(d);
            }

            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                string ngayString = "";
                List<int> dsc = new List<int>();
                int x = (int)new DateTime(year, month, listLich[i + start - 1].ngay).DayOfWeek;
                ngayString = year + "-" + month + "-" + listLich[i + start - 1].ngay;
                if (DateTime.Parse(date).Day <= listLich[i + start - 1].ngay)
                {

                    bool isCheck = false;
                    if (select == 0)
                    {
                        if (x >= 1 && x < 6)
                        {
                            isCheck = true;
                            dsc = new List<int>(dsca);
                        }
                    }

                    if (select == 1)
                    {

                        if (x >= 1 && x < 7)
                        {
                            isCheck = true;

                            dsc = new List<int>(dsca);
                        }
                    }

                    if (select == 2)
                    {

                        isCheck = true;

                        dsc = new List<int>(dsca);
                    }
                    var d = new lichlamviec()
                    { id = i + start - 1, ngay = i, ca = dsc.Count, status = 1, dsca = dsc, isCheck = isCheck, ngayString = ngayString };
                    listLich[i + start - 1] = d;
                }
                else
                {
                    var d = new lichlamviec()
                    { id = i + start - 1, ngay = i, ca = dsc.Count, status = 1, dsca = new List<int>(), ngayString = ngayString };
                    listLich[i + start - 1] = d;
                }


            }

            listLich = listLich.ToList();
        }
        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodContinue_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CreateCyCle();
        }

        private void ExitCreateCalendarWork_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void selectNgay(object sender, MouseButtonEventArgs e)
        {
            Border b = sender as Border;
            lichlamviec data = (lichlamviec)b.DataContext;
            if (data.status == 1)
            {
                foreach (var item in listLich)
                {
                    if (item.status == 2)
                        item.status = 1;
                    if (item.id == data.id)
                        item.status = 2;
                }
            }
        }
        private void OpenCalander_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var img = sender as Image;
                var grid = img.Parent as Grid;
                var border = grid.Parent as Border;
                var stackPanel = border.Parent as StackPanel;
                var calander = stackPanel.Children[1];
                if (calander.Visibility == Visibility.Collapsed)
                {
                    calander.Visibility = Visibility.Visible;
                    img.LayoutTransform = new RotateTransform(0);
                    img.UpdateLayout();
                }
                else
                {
                    calander.Visibility = Visibility.Collapsed;
                    img.LayoutTransform = new RotateTransform(-90);
                    img.UpdateLayout();

                }
            }
            catch { }
        }


        // Hàm giúp tìm cha của một đối tượng trong VisualTree
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

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the CheckBox that triggered the event
                CheckBox checkBox = (CheckBox)sender;

                // Find the parent ListViewItem
                ListViewItem listViewItem = FindParent<ListViewItem>(checkBox);

                // Find the parent StackPanel
                StackPanel stackPanel = FindParent<StackPanel>(listViewItem);

                var shift_id = (stackPanel.DataContext as ListShiftEntities.Item).shift_id;

                if (shift_id != null)
                {
                    var selectedDay = ((CheckBox)sender).DataContext as lichlamviec;
                    if (selectedDay != null) selectedDay.dsca.Add((int)shift_id);
                }
            }
            catch { }

        }

        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            // Traverse up the visual tree to find the specified type of parent
            while ((child != null) && !(child is T))
            {
                child = VisualTreeHelper.GetParent(child);
            }

            return child as T;
        }

        private static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            // Traverse down the visual tree to find the child with the specified name and type
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is T && ((FrameworkElement)child).Name == childName)
                {
                    return (T)child;
                }

                T childOfChild = FindChild<T>(child, childName);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }

            return null;
        }


        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the CheckBox that triggered the event
                CheckBox checkBox = (CheckBox)sender;

                // Find the parent ListViewItem
                ListViewItem listViewItem = FindParent<ListViewItem>(checkBox);

                // Find the parent StackPanel
                StackPanel stackPanel = FindParent<StackPanel>(listViewItem);

                var shift_id = (stackPanel.DataContext as ListShiftEntities.Item).shift_id;

                if (shift_id != null)
                {
                    var selectedDay = ((CheckBox)sender).DataContext as lichlamviec;
                    if (selectedDay != null) selectedDay.dsca.Remove((int)shift_id);
                }
            }
            catch { }
        }

        public class ListBodyJson
        {
            public string date { get; set; }
            public string shift_id { get; set; }
        }

        public async void CreateCyCle()
        {
            try
            {
                List<ListBodyJson> listBodyJsons = new List<ListBodyJson>();
                foreach (var item in listLich)
                {
                    if (item.ngayString != null && item.ngayString != "")
                    {

                        listBodyJsons.Add(new ListBodyJson() { date = item.ngayString, shift_id = item.listShiftIdString });
                    }
                }
                var json = JsonConvert.SerializeObject(listBodyJsons);

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/cycle/create");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(ten), "cy_name");
                content.Add(new StringContent(DateTime.Parse(date).ToString("yyyy-MM-dd")), "apply_month");
                content.Add(new StringContent(json), "cy_detail");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                this.Visibility = Visibility.Collapsed;


                //var client = new HttpClient();
                //var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/cycle/create");
                //request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                //var content = new StringContent("{\"cy_name\":\"ssds\",\"apply_month\":\"2023-11-18\",\"cy_detail\":\"[{\\\"date\\\":\\\"2023-11-18\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"},{\\\"date\\\":\\\"2023-11-20\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456,2025475,2025467\\\"},{\\\"date\\\":\\\"2023-11-21\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"},{\\\"date\\\":\\\"2023-11-22\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"},{\\\"date\\\":\\\"2023-11-23\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"},{\\\"date\\\":\\\"2023-11-24\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"},{\\\"date\\\":\\\"2023-11-25\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"},{\\\"date\\\":\\\"2023-11-27\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"},{\\\"date\\\":\\\"2023-11-28\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"},{\\\"date\\\":\\\"2023-11-29\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"},{\\\"date\\\":\\\"2023-11-30\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"}]\"}", null, "application/json");
                //request.Content = content;
                //var response = await client.SendAsync(request);
                //var responseContent = await response.Content.ReadAsStringAsync();
                ////response.EnsureSuccessStatusCode();
                ////Console.WriteLine();


            }
            catch { }
        }
    }
}
