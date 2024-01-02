using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.DatePicker;
using System.Globalization;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP;
using System.Collections.Generic;
using QuanLyChung365TruocDangNhap.ChamCongNew.funcQuanLyCongTy;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using Newtonsoft.Json;
using System.Net.Http;
//using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec.QuanLyChung365TruocDangNhap.ChamCongNew.Entities.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec;
using static QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.CaiDatLichLamViec.ucChinhSuaLichLamViec;
using System.Linq;
using System.Net;
using System.Text;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.DetailOfDon;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi;
using System.Threading.Tasks;
//using DocumentFormat.OpenXml.Spreadsheet;
//using DocumentFormat.OpenXml.Wordprocessing;
using System.Windows.Media;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec.ChamCong365.Entities.funcQuanLyCongTy;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.CaiDatLichLamViec
{
    /// <summary>
    /// Interaction logic for ucNextCreateCalendarWork.xaml
    /// </summary>
    public partial class ucXemChiTietLichLamViec : UserControl
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        string thang;

        string date;
        int month;
        int year;
        int start;
        ucCaiDatLichLamViec ucSetting;
        public delegate void ShowPopup(object obj);
        public static event ShowPopup onShowPopup;
        List<XemChiTietLichLichLamViecEntites.Data> ListDay;
        public ucXemChiTietLichLamViec(string thang, string startDate, List<XemChiTietLichLichLamViecEntites.Data> ListDay)
        {
            InitializeComponent();
            this.DataContext = this;
            this.thang = thang;

            this.date = startDate;
            this.ListDay = ListDay;


            LoadShiftDetail();
        }
        #region# ListCa
        private List<Item_CaLamViec> _listCa;

        public List<Item_CaLamViec> listCa
        {
            get { return _listCa; }
            set
            {
                _listCa = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadShiftInChuKy()
        {
            try
            {
                var httpClient = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Get;
                string api = API.list_shifts_api;

                httpRequestMessage.RequestUri = new Uri(api);
                httpRequestMessage.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var response = await httpClient.SendAsync(httpRequestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();
                Root_CaLamViec result = JsonConvert.DeserializeObject<Root_CaLamViec>(responseContent);
                if (result.data.items != null)
                {
                    listCa = result.data.items;

                }
            }
            catch (Exception) { }
        }


        List<Item_CaLamViec> dsca;

        public class lichlamviec : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public int id;
            public int ngay { get; set; }
            public List<Item_CaLamViec> dsca { get; set; }
        }

        private List<lichlamviec> _listLich;

        public List<lichlamviec> listLich
        {
            get { return _listLich; }
            set
            {
                _listLich = value;
                OnPropertyChanged();
            }
        }
        private async void LoadShiftDetail()
        {
            await LoadShiftInChuKy();
            try
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
                            new lichlamviec() { id = listLich.Count, ngay = x - i, });
                    }

                    listLich.Reverse();
                }

                for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
                {
                    List<Item_CaLamViec> dsc = new List<Item_CaLamViec>();

                    var day = ListDay.FirstOrDefault(x => x.day == i);
                    if (day != null)
                    {
                        foreach (var shiftId in day.listShift_id)
                        {
                            if (shiftId != null && shiftId != "")
                            {
                                var ca = listCa.FirstOrDefault(x => x.shift_id == int.Parse(shiftId));
                                if (ca != null)
                                {
                                    dsc.Add(ca);
                                }
                            }

                        }
                    }
                    var d = new lichlamviec() { id = listLich.Count, ngay = i, dsca = dsc };
                    listLich.Add(d);
                }

                int n = 42 - listLich.Count;
                for (int i = 1; i <= n; i++)
                {
                    var d = new lichlamviec() { id = listLich.Count, ngay = i };
                    listLich.Add(d);
                }


                listLich = listLich.ToList();
                lsvListLich.ItemsSource = listLich;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }







        #endregion

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void lsvListLich_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {

                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    var scrollViewer = FindVisualChild<ScrollViewer>(lsvListLich);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - e.Delta);
                        e.Handled = true;
                    }

                }
                else
                {
                    tablescroll.ScrollToVerticalOffset(tablescroll.VerticalOffset - e.Delta);
                }
            }
            catch { }
        }

        private T FindVisualChild<T>(DependencyObject visual) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(visual, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null)
                        return childItem;
                }
            }
            return null;
        }

    }
}
