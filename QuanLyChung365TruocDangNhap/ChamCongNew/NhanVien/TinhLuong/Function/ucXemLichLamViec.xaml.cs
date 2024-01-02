using QuanLyChung365TruocDangNhap.ChamCongNew.Core;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.KindOfDon;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB;
//using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec.QuanLyChung365TruocDangNhap.ChamCongNew.Entities.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.TinhLuong;
//using DocumentFormat.OpenXml.Bibliography;
//using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.TinhLuong.Function
{
    /// <summary>
    /// Interaction logic for ucXemLichLamViec.xaml
    /// </summary>
    public partial class ucXemLichLamViec : UserControl
    {
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
        public ucXemLichLamViec(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
            tb_IDNV.Text = main.Ep_Id.ToString();
            tb_TenNhanVien.Text = main.Name_Nv;
            start_date = $"{DateTime.Now.Year}-{DateTime.Now.Month}-01";
            end_date = $"{DateTime.Now.Year + 1}-01-01";
            month = DateTime.Now.Month;
            year = DateTime.Now.Year;
            LoadDLNam();
            loadDLThang();
            LoadLichLamViec();
        }

        private void LoadShiftDetail()
        {
            try
            {
                if (lsvThang.SelectedItem != null && lsvNam.SelectedItem == null)
                {
                    month = int.Parse(lsvThang.Text.Split(' ')[1]);
                    if (month < 12)
                    {
                        start_date = $"{year}-{month}-01";
                        end_date = $"{year}-{month + 1}-01";
                    }
                    else if (month == 12)
                    {
                        start_date = $"{year}-{month}-01";
                        end_date = $"{year + 1}-01-01";
                    }
                }
                else if (lsvThang.SelectedItem == null && lsvNam.SelectedItem != null)
                {
                    year = int.Parse(lsvNam.Text.Split(' ')[1]);
                    if (month < 12)
                    {
                        start_date = $"{year}-{month}-01";
                        end_date = $"{year}-{month + 1}-01";
                    }
                    else if (month == 12)
                    {
                        start_date = $"{year}-{month}-01";
                        end_date = $"{year + 1}-01-01";
                    }
                }
                else if (lsvThang.SelectedItem != null && lsvNam.SelectedItem != null)
                {
                    month = int.Parse(lsvThang.Text.Split(' ')[1]);
                    year = int.Parse(lsvNam.Text.Split(' ')[1]);
                    if (month < 12)
                    {
                        start_date = $"{year}-{month}-01";
                        end_date = $"{year}-{month + 1}-01";
                    }
                    else if (month == 12)
                    {
                        start_date = $"{year}-{month}-01";
                        end_date = $"{year + 1}-01-01";
                    }
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        List<ListCycle> dsca;
        public class lichlamviec : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public int id;
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
            public int ngay { get; set; }
            public List<ListCycle> dsca { get; set; }
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
        
        //List<ListCycle> lstCycles = new List<ListCycle>();
        private List<ListCycle> _lstCycles;

        public List<ListCycle> lstCycles
        {
            get { return _lstCycles; }
            set { _lstCycles = value; OnPropertyChanged(); }
        }
        public async void LoadLichLamViec()
        {
            try
            {
                loading.Visibility = Visibility.Visible;
                if (lsvThang.SelectedItem != null && lsvNam.SelectedItem == null)
                {
                    month = int.Parse(lsvThang.Text.Split(' ')[1]);
                    if (month < 12)
                    {
                        start_date = $"{year}-{month}-01";
                        end_date = $"{year}-{month + 1}-01";
                    }
                    else if (month == 12)
                    {
                        start_date = $"{year}-{month}-01";
                        end_date = $"{year + 1}-01-01";
                    }
                }
                else if (lsvThang.SelectedItem == null && lsvNam.SelectedItem != null)
                {
                    year = int.Parse(lsvNam.Text.Split(' ')[1]);
                    if (month < 12)
                    {
                        start_date = $"{year}-{month}-01";
                        end_date = $"{year}-{month + 1}-01";
                    }
                    else if (month == 12)
                    {
                        start_date = $"{year}-{month}-01";
                        end_date = $"{year + 1}-01-01";
                    }
                }
                else if (lsvThang.SelectedItem != null && lsvNam.SelectedItem != null)
                {
                    month = int.Parse(lsvThang.Text.Split(' ')[1]);
                    year = int.Parse(lsvNam.Text.Split(' ')[1]);
                    if (month < 12)
                    {
                        start_date = $"{year}-{month}-01";
                        end_date = $"{year}-{month + 1}-01";
                    }
                    else if (month == 12)
                    {
                        start_date = $"{year}-{month}-01";
                        end_date = $"{year + 1}-01-01";
                    }
                }

                int ep_id = Main.Ep_Id;
                int cb = Main.ComdID;
                tb_LichThang.Text = month.ToString();
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
                        lstCycles = lichnv.data.list_cycle;

                        start = (int)new DateTime(year, month, 1).DayOfWeek;
                        listLich = new List<lichlamviec>();
                        if (month - 1 > 0)
                        {
                            for (int i = 0; i < start; i++)
                            {
                                var x = DateTime.DaysInMonth(year, month - 1);
                                listLich.Add(
                                    new lichlamviec() { id = listLich.Count, ngay = x - i, status = 0});
                            }
                            listLich.Reverse();
                        }

                        for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
                        {
                            List<ListCycle> dsc = new List<ListCycle>();
                            var lstca = lstCycles.Where(x => x.date.Day == i);
                            if (lstca !=  null)
                            {
                               dsc = lstca.ToList();
                            }
                            var d = new lichlamviec() { id = listLich.Count, ngay = i, dsca = dsc, status = 1 };
                            listLich.Add(d);
                        }
                       

                        int n = 42 - listLich.Count;
                        for (int i = 1; i <= n; i++)
                        {
                            var d = new lichlamviec() { id = listLich.Count, ngay = i , status = 0};
                            listLich.Add(d);
                        }


                        listLich = listLich.ToList();
                        lsvListLich.ItemsSource = listLich;
                    }
                       
                }
                loading.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
            }
        }
        private void LoadDLNam()
        {
            List<string> listNam = new List<string>();
            listNam.Add("Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString());
            listNam.Add("Năm " + DateTime.Now.Year);
            listNam.Add("Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString());
            lsvNam.ItemsSource = listNam;
            lsvNam.SelectedIndex = 1;
            //lsvNam.SelectedItem = listNam[1];
            lsvNam.PlaceHolder = listNam[1];
        }
        private void loadDLThang()
        {
            List<string> listThang = new List<string>();
            listThang.Add("Tháng 1");
            listThang.Add("Tháng 2");
            listThang.Add("Tháng 3");
            listThang.Add("Tháng 4");
            listThang.Add("Tháng 5");
            listThang.Add("Tháng 6");
            listThang.Add("Tháng 7");
            listThang.Add("Tháng 8");
            listThang.Add("Tháng 9");
            listThang.Add("Tháng 10");
            listThang.Add("Tháng 11");
            listThang.Add("Tháng 12");
            lsvThang.ItemsSource = listThang;
            lsvThang.SelectedIndex = DateTime.Now.Month - 1;
            //lsvThang.SelectedItem = listThang[DateTime.Now.Month - 1];
            lsvThang.PlaceHolder = listThang[DateTime.Now.Month - 1];
        }
        private void selectNgay(object sender, MouseButtonEventArgs e)
        {

        }

        private void lsvListLich_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
           Main.scrollMainChamCong.ScrollToVerticalOffset(Main.scrollMainChamCong.VerticalOffset - e.Delta);
        }
        public Key keyDown { get; set; }
        private void ucSalary_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                keyDown = e.Key;
            }
        }

        private void ucSalary_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == keyDown)
            {
                keyDown = Key.Cancel;
            }
        }
        private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void btn_ThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LoadLichLamViec();
        }

        private void btn_DeXuatLich_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            DeXuatLichLamViec uc = new DeXuatLichLamViec(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void lsvListLich_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
        }

        private void lsvListLich_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void ListView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void ListView_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
        }

        private void ListView_PreviewDragEnter(object sender, DragEventArgs e)
        {
            e.Handled= true;
        }
    }
}
