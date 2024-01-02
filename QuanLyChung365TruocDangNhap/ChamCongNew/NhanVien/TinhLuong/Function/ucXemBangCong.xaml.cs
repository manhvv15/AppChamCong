using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.TinhLuong;
//using DocumentFormat.OpenXml.Bibliography;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.TinhLuong.Function
{
    /// <summary>
    /// Interaction logic for ucXemBangCong.xaml
    /// </summary>
    public partial class ucXemBangCong : UserControl, INotifyPropertyChanged
    {
        private int _IsSmallSize;
        public int IsSmallSize
        {
            get { return _IsSmallSize; }
            set { _IsSmallSize = value; OnPropertyChanged("IsSmallSize"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        MainChamCong Main;
        string start_date;
        string end_date;
        int month;
        int year;
        int start;
        public ucXemBangCong(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
            tb_ChuKyCong.Text = DateTime.Now.Month.ToString();
            tb_NameAcount.Text = main.Name_Nv;
            start_date = $"{DateTime.Now.Year}-{DateTime.Now.Month}-01";
            end_date = $"{DateTime.Now.Year + 1}-01-01";
            month = DateTime.Now.Month;
            year = DateTime.Now.Year;
            
            LoadLichLamViec();
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
            public List<DataFinal_LuongNV> ctca { get; set; }
            public int _status;
            public int status
            {
                get { return _status; }
                set { _status = value; OnPropertyChanged(); }
            }
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

        private List<DataFinal_LuongNV> _lstCong;

        public List<DataFinal_LuongNV> lstCong
        {
            get { return _lstCong; }
            set { _lstCong = value; OnPropertyChanged(); }
        }
        List<DataFinal_LuongNV> lstCongMS = new List<DataFinal_LuongNV>();
        public async void LoadLichLamViec()
        {
            try
            {
                loading.Visibility = Visibility.Visible;
                int ep_id = Main.Ep_Id;
                int cb = Main.ComdID;
                string token = Properties.Settings.Default.Token;
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/nhanvien/qly_ttnv");
                var DataObject = new
                {
                    token = Properties.Settings.Default.Token,
                    start_date = start_date,
                    end_date = end_date,
                    month = month,
                    year = year,
                    cp = cb,
                    ep_id = ep_id,
                };
                string json = JsonConvert.SerializeObject(DataObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var resConten = await response.Content.ReadAsStringAsync();
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    Root_LichNhanVien lichnv = JsonConvert.DeserializeObject<Root_LichNhanVien>(resConten);
                    if (lichnv.data != null)
                    {
                        lstCong = lichnv.data.data_final;
                        tb_CongChuan.Text = lichnv.data.count_standard_works.ToString();
                        tb_TongCong.Text = lichnv.data.cong_thuc.ToString();
                        tb_CongThem.Text = lichnv.data.cong_xn_them.ToString();
                        tb_CountCongTheoLich.Text = lichnv.data.cong_thuc.ToString();
                        foreach (var item in lstCong)
                        {
                            if (item.content == "Ca đi muộn về sớm")
                            {
                                lstCongMS.Add(item);
                            }
                        }
                        tb_DiMuon.Text = lstCongMS.Count.ToString();
                        tb_CountDMVS.Text = lstCongMS.Count.ToString();
                        start = (int)new DateTime(year, month, 1).DayOfWeek;
                        listLich = new List<lichlamviec>();
                        if (month - 1 > 0)
                        {
                            for (int i = 0; i < start; i++)
                            {
                                var x = DateTime.DaysInMonth(year, month - 1);
                                listLich.Add(
                                    new lichlamviec() { id = listLich.Count, ngay = x - i, status = 0 });
                            }
                            listLich.Reverse();
                        }
                        for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
                        {
                                    List<DataFinal_LuongNV> dsc = new List<DataFinal_LuongNV>();
                                    var lstca1 = lstCong.Where(x => x.detail_cycle.date.Day == i);
                                    if (lstca1 != null)
                                    {
                                        dsc = lstca1.ToList();
                                    }

                            var d = new lichlamviec() { id = listLich.Count, ngay = i, ctca = dsc, status = 1 };
                            listLich.Add(d);
                        }

                        int n = 42 - listLich.Count;
                        for (int i = 1; i <= n; i++)
                        {
                            var d = new lichlamviec() { id = listLich.Count, ngay = i, status = 0 };
                            listLich.Add(d);
                        }

                        listLich = listLich.ToList();
                        lsvListLich.ItemsSource = listLich;
                        lsvListLich.Items.Refresh();
                    }
                }
                loading.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
            }
        }
        private void st_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMainChamCong.ScrollToVerticalOffset(Main.scrollMainChamCong.VerticalOffset - e.Delta);
        }

        private void lv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //ListView lv = sender as ListView;
            //if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) { scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - e.Delta); } else Main.scrollMainChamCong.ScrollToVerticalOffset(Main.scrollMainChamCong.VerticalOffset - e.Delta);
        }

        private void wap_XemBangLuong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucListTinhLuong ucl = new ucListTinhLuong(Main);
            ucXemBangLuong uc = new ucXemBangLuong(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.txbLoadChamCong.Text = ucl.txbLoadNameFuction.Text + " / " + "Xem bảng lương";
        }

        private void lsvListLich_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMainChamCong.ScrollToVerticalOffset(Main.scrollMainChamCong.VerticalOffset - e.Delta);
        }

        private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void ListView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void lsvListLich_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void lsvListLich_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void lsvListLich_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            e.Handled= true;
        }

        private void ListView_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
        }
    }
}
