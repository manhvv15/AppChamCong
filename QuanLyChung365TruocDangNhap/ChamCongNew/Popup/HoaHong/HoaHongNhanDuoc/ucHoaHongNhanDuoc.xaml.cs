using QuanLyChung365TruocDangNhap.ChamCongNew.Core;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.HoaHong;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.HoaHong;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.HoaHong.ThemSuaHoaHong;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.clsNhanVienThuocCongTy;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.HoaHong.HoaHongNhanDuoc
{
    /// <summary>
    /// Interaction logic for ucHoaHongNhanDuoc.xaml
    /// </summary>
    public partial class ucHoaHongNhanDuoc : UserControl
    {
        MainWindow Main;
        int Month; int Year;int Day ;string Rose_Id_User;
        public List<string> Montlist { get; set; }
        public List<string> YearList { get; set; }
        public ucHoaHongNhanDuoc(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            LoadNhanhVien();
            LoadListMonth();
            LoadListYear();
            LoadListRoseSaff();
            dgvHoaHongCaNhanTien.Visibility = Visibility.Collapsed;
            dgvHoaHongNhanDuoc.Visibility = Visibility.Visible;
        }
        int Back;
        public ucHoaHongNhanDuoc(MainWindow main, int back)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Main = main;
            Back = back;
            LoadNhanhVien();
            LoadListMonth();
            LoadListYear();
            LoadListRoseSaff();
            textTieuDe.Text = "Danh sách nhân viên & hoa hồng";
            textTieuDeMo.Text = "Quản lý theo dõi nhân viên được gán hoa hồng";
            btn_ThemMoiCacLoai.Visibility = Visibility.Visible;
            dgvHoaHongCaNhanTien.Visibility = Visibility.Visible;
            dgvHoaHongNhanDuoc.Visibility = Visibility.Collapsed;
        }

        string Next1;
        public ucHoaHongNhanDuoc(MainWindow main, string next1)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Main = main;
            Next1 = next1;
            LoadNhanhVien();
            LoadListMonth();
            LoadListYear();
            LoadListRoseSaff();
            textTieuDe.Text = "Tổng hoa hồng tiền";
            textTieuDeMo.Text = "Quản lý theo dõi tổng hoa hồng tiền";
            btn_ThemMoiCacLoai.Visibility = Visibility.Collapsed;
            dgvHoaHongCaNhanTien.Visibility = Visibility.Collapsed;
            dgvHoaHongNhanDuoc.Visibility = Visibility.Collapsed;
            dgvTongHoaHongTien.Visibility = Visibility.Visible;
          
        }

        string Next2;
        int CN;
        public List<ListThietLap> lstRose2 = new List<ListThietLap>();
        public ucHoaHongNhanDuoc(MainWindow main,int back, string next2, List<ListThietLap> lstrose2, int cn)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Main = main;
            Next2 = next2;
            Back = back;
            lstRose2 = lstrose2;
            CN = cn;
            LoadNhanhVien();
            LoadListMonth();
            LoadListYear();
            LoadHoaHongDoanhThuCaNhan();
            textTieuDe.Text = "Danh sách nhân viên hưởng hoa hồng doanh thu";
            textTieuDeMo.Text = "Quản lý theo dõi nhân viên được gán hoa hồng";
            btn_ThemMoiCacLoai.Visibility = Visibility.Visible;
            dgvHoaHongCaNhanTien.Visibility = Visibility.Collapsed;
            dgvHoaHongNhanDuoc.Visibility = Visibility.Collapsed;
            dgvTongHoaHongTien.Visibility = Visibility.Collapsed;
            dgvHoaHongDoanhThu.Visibility = Visibility.Visible;
        }

        long HoaHongDoanhThu;
        public ucHoaHongNhanDuoc(MainWindow main, long hhdt, int cn)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Main = main;
            HoaHongDoanhThu = hhdt;
            CN = cn;
            LoadNhanhVien();
            LoadListMonth();
            LoadListYear();
            LoadHoaHongDoanhThuCaNhan();
            textTieuDe.Text = "Tổng hoa hồng doanh thu";
            textTieuDeMo.Text = "Quản lý theo dõi tổng hoa hồng doanh thu";
            btn_ThemMoiCacLoai.Visibility = Visibility.Collapsed;
            dgvHoaHongCaNhanTien.Visibility = Visibility.Collapsed;
            dgvHoaHongNhanDuoc.Visibility = Visibility.Collapsed;
            dgvTongHoaHongTien.Visibility = Visibility.Collapsed;
            dgvHoaHongDoanhThu.Visibility = Visibility.Collapsed;
            dgvTongHoaHongDoanhThu.Visibility = Visibility.Visible;
        }

        double LoiNhuan;
        public ucHoaHongNhanDuoc(MainWindow main, double loinhuan, List<ListThietLap> lstrose2)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Main = main;
            this.LoiNhuan = loinhuan;
            this.lstRose2 = lstrose2;
            LoadNhanhVien();
            LoadListMonth();
            LoadListYear();
            LoadHoaHongLoiNhuan();
            textTieuDe.Text = "Danh sách nhân viên hưởng hoa hồng lợi nhuận";
            textTieuDeMo.Text = "Quản lý theo dõi nhân viên hưởng hoa hồng lợi nhuận";
            btn_ThemMoiCacLoai.Visibility = Visibility.Visible;
            dgvHoaHongCaNhanTien.Visibility = Visibility.Collapsed;
            dgvHoaHongNhanDuoc.Visibility = Visibility.Collapsed;
            dgvTongHoaHongTien.Visibility = Visibility.Collapsed;
            dgvHoaHongDoanhThu.Visibility = Visibility.Collapsed;
            dgvTongHoaHongDoanhThu.Visibility = Visibility.Collapsed;
            dgvHoaHongLoiNhuan.Visibility = Visibility.Visible;
        }

        bool NhomLoiNhuan;
        public ucHoaHongNhanDuoc(MainWindow main, bool nhomloinhuan)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Main = main;
            this.NhomLoiNhuan = nhomloinhuan;
            LoadNhanhVien();
            LoadListMonth();
            LoadListYear();
            textTieuDe.Text = "Danh sách nhóm hưởng hoa hồng lợi nhuận";
            textTieuDeMo.Text = "Quản lý theo dõi nhóm nhân viên hoa hồng lợi nhuận";
            btn_ThemMoiCacLoai.Visibility = Visibility.Visible;
            dgvHoaHongCaNhanTien.Visibility = Visibility.Collapsed;
            dgvHoaHongNhanDuoc.Visibility = Visibility.Collapsed;
            dgvTongHoaHongTien.Visibility = Visibility.Collapsed;
            dgvHoaHongDoanhThu.Visibility = Visibility.Collapsed;
            dgvTongHoaHongDoanhThu.Visibility = Visibility.Collapsed;
            dgvHoaHongLoiNhuan.Visibility = Visibility.Collapsed;
            dgvNhomHoaHongLoiNhuan.Visibility = Visibility.Visible;
        }

        float HoaHongLoiNhuan;
        public ucHoaHongNhanDuoc(MainWindow main, float hoahongloinhuan)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Main = main;
            HoaHongLoiNhuan = hoahongloinhuan;
            LoadNhanhVien();
            LoadListMonth();
            LoadListYear();
            LoadHoaHongLoiNhuan();
            textTieuDe.Text = "Tổng hoa hồng lợi nhuận";
            textTieuDeMo.Text = "Quản lý theo dõi tổng hoa hồng lợi nhuận";
            btn_ThemMoiCacLoai.Visibility = Visibility.Collapsed;
            dgvHoaHongCaNhanTien.Visibility = Visibility.Collapsed;
            dgvHoaHongNhanDuoc.Visibility = Visibility.Collapsed;
            dgvTongHoaHongTien.Visibility = Visibility.Collapsed;
            dgvHoaHongDoanhThu.Visibility = Visibility.Collapsed;
            dgvTongHoaHongDoanhThu.Visibility = Visibility.Collapsed;
            dgvHoaHongLoiNhuan.Visibility = Visibility.Collapsed;
            dgvNhomHoaHongLoiNhuan.Visibility = Visibility.Collapsed;
            dgvTongHoaHongLoiNhuan.Visibility = Visibility.Visible;

        }

        decimal ViTri;
        public ucHoaHongNhanDuoc(MainWindow main, decimal vitri, List<ListThietLap> lstrose2)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Main = main;
            this.ViTri = vitri;
            this.lstRose2 = lstrose2;
            LoadNhanhVien();
            LoadListMonth();
            LoadListYear();
            LoadHoaHongViTri();
            textTieuDe.Text = "Danh sách nhân viên hưởng hoa hồng lệ phí vị trí";
            textTieuDeMo.Text = "Quản lý theo dõi nhân viên hưởng hoa hồng lệ phí vị trí";
            btn_ThemMoiCacLoai.Visibility = Visibility.Visible;
            dgvHoaHongCaNhanTien.Visibility = Visibility.Collapsed;
            dgvHoaHongNhanDuoc.Visibility = Visibility.Collapsed;
            dgvTongHoaHongTien.Visibility = Visibility.Collapsed;
            dgvHoaHongDoanhThu.Visibility = Visibility.Collapsed;
            dgvTongHoaHongDoanhThu.Visibility = Visibility.Collapsed;
            dgvHoaHongLoiNhuan.Visibility = Visibility.Collapsed;
            dgvNhomHoaHongLoiNhuan.Visibility = Visibility.Collapsed;
            dgvTongHoaHongLoiNhuan.Visibility = Visibility.Collapsed;
            dgvHoaHongLePhiViTri.Visibility = Visibility.Visible;

        }

        short HoaHongViTri;
        public ucHoaHongNhanDuoc(MainWindow main, short hoaHongViTri)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Main = main;
            this.HoaHongViTri = hoaHongViTri;
            LoadNhanhVien();
            LoadListMonth();
            LoadListYear();
            LoadHoaHongViTri();
            textTieuDe.Text = "Tổng hoa hồng tiền lệ phí vị trí";
            textTieuDeMo.Text = "Quản lý theo dõi tổng hoa hồng lệ phí vị trí";
            btn_ThemMoiCacLoai.Visibility = Visibility.Collapsed;
            dgvHoaHongCaNhanTien.Visibility = Visibility.Collapsed;
            dgvHoaHongNhanDuoc.Visibility = Visibility.Collapsed;
            dgvTongHoaHongTien.Visibility = Visibility.Collapsed;
            dgvHoaHongDoanhThu.Visibility = Visibility.Collapsed;
            dgvTongHoaHongDoanhThu.Visibility = Visibility.Collapsed;
            dgvHoaHongLoiNhuan.Visibility = Visibility.Collapsed;
            dgvNhomHoaHongLoiNhuan.Visibility = Visibility.Collapsed;
            dgvTongHoaHongLoiNhuan.Visibility = Visibility.Collapsed;
            dgvHoaHongLePhiViTri.Visibility = Visibility.Collapsed;
            dgvTongHoaHongViTri.Visibility= Visibility.Visible;
        }

        uint KeHoach;
        string NextKH;
        List<ListThietLap> lstRose5 = new List<ListThietLap>();
        public ucHoaHongNhanDuoc(MainWindow main, uint kehoach, List<ListThietLap> lstrose5)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Main = main;
            this.KeHoach = kehoach;
            this.lstRose5 = lstrose5;
            LoadNhanhVien();
            LoadListMonth();
            LoadListYear();
            LoadHoaHongKeHoach();
            textTieuDe.Text = "Danh sách nhân viên hưởng hoa hồng kế hoạch";
            textTieuDeMo.Text = "Quản lý theo dõi nhân viên hưởng hoa hồng kế hoạch";
            btn_ThemMoiCacLoai.Visibility = Visibility.Visible;
            dgvHoaHongCaNhanTien.Visibility = Visibility.Collapsed;
            dgvHoaHongNhanDuoc.Visibility = Visibility.Collapsed;
            dgvTongHoaHongTien.Visibility = Visibility.Collapsed;
            dgvHoaHongDoanhThu.Visibility = Visibility.Collapsed;
            dgvTongHoaHongDoanhThu.Visibility = Visibility.Collapsed;
            dgvHoaHongLoiNhuan.Visibility = Visibility.Collapsed;
            dgvNhomHoaHongLoiNhuan.Visibility = Visibility.Collapsed;
            dgvTongHoaHongLoiNhuan.Visibility = Visibility.Collapsed;
            dgvHoaHongLePhiViTri.Visibility = Visibility.Collapsed;
            dgvTongHoaHongViTri.Visibility = Visibility.Collapsed;
            dgvHoaHongKeHoach.Visibility = Visibility.Visible;
        }

        int HoaHongKeHoach;
        public ucHoaHongNhanDuoc(MainWindow main, uint kehoach, int hhkh)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Main = main;
            this.KeHoach = kehoach;
            this.HoaHongKeHoach = hhkh;
            LoadNhanhVien();
            LoadListMonth();
            LoadListYear();
            LoadHoaHongKeHoach();
            textTieuDe.Text = "Tổng hoa hồng kế hoạch";
            textTieuDeMo.Text = "Quản lý theo dõi tổng hoa hồng kế hoạch";
            btn_ThemMoiCacLoai.Visibility = Visibility.Collapsed;
            dgvHoaHongCaNhanTien.Visibility = Visibility.Collapsed;
            dgvHoaHongNhanDuoc.Visibility = Visibility.Collapsed;
            dgvTongHoaHongTien.Visibility = Visibility.Collapsed;
            dgvHoaHongDoanhThu.Visibility = Visibility.Collapsed;
            dgvTongHoaHongDoanhThu.Visibility = Visibility.Collapsed;
            dgvHoaHongLoiNhuan.Visibility = Visibility.Collapsed;
            dgvNhomHoaHongLoiNhuan.Visibility = Visibility.Collapsed;
            dgvTongHoaHongLoiNhuan.Visibility = Visibility.Collapsed;
            dgvHoaHongLePhiViTri.Visibility = Visibility.Collapsed;
            dgvTongHoaHongViTri.Visibility = Visibility.Collapsed;
            dgvHoaHongKeHoach.Visibility = Visibility.Collapsed;
            dgvTongHoaHongKeHoach.Visibility = Visibility.Visible;

        }
        List<RoseUser> lstRoseUser = new List<RoseUser>();
        List<RoseUser> lstRoseUserObject = new List<RoseUser>();
        List<RoseUser> lstRoseUserCPT = new List<RoseUser>();
        List<RoseUser> lstSearchRoseUser = new List<RoseUser>();
        List<TinhluongThietLap> lstTinhLuong = new List<TinhluongThietLap>();
        List<Detail> lstDetail = new List<Detail>();
        //List<listHoaHongNhanDuoc> lstHHNhanDuoc = new List<listHoaHongNhanDuoc>();
        private int TongSoTrang = 0;
        private int PageNumberCurrent = 1;
        private int SoDu = 0;
        BrushConverter brus = new BrushConverter();
        public async void LoadListRoseSaff()
        {
            try
            {
                loading.Visibility = Visibility.Visible;
                lstRoseUser.Clear();
                lstRoseUserCPT.Clear();
                lstRoseUserObject.Clear();
                Month = int.Parse(cbo_ChonThang.Text.Split(' ')[1]);
                Year = int.Parse(cbo_ChonNam.Text.Split(' ')[1]);
                Day = int.Parse(DateTime.Now.Day.ToString());
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/lay_tien_ca_nhan");
                var content = new MultipartFormDataContent();
                DateTime firstDayOfMonth = new DateTime(Year, Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                if (Month < 10)
                {
                    content.Add(new StringContent($"{Year}-0{Month}-0{firstDayOfMonth.Day}"), "start_date");
                    content.Add(new StringContent($"{Year}-0{Month}-{lastDayOfMonth.Day}"), "end_date"); 
                }
                else if (Year >= 10)
                {
                    content.Add(new StringContent($"{Year}-{Month}-0{firstDayOfMonth.Day}"), "start_date");
                    content.Add(new StringContent($"{Year}-{Month}-{lastDayOfMonth.Day}"), "end_date");
                }
                if (searchBarNhanVien.SelectedItem != null && ((ListUser)searchBarNhanVien.SelectedItem)._id > 0)
                {
                    content.Add(new StringContent(Rose_Id_User), "ro_id_user");
                }
                content.Add(new StringContent(Main.IdAcount.ToString()), "ro_id_com");
                content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var resConten = await response.Content.ReadAsStringAsync();
                    Root_Rose lstus = JsonConvert.DeserializeObject<Root_Rose>(resConten);
                    if (lstus.rose_user != null)
                    {
                        if (Back == 21 || Next1 == "RoseMoney1")
                        {
                            if (lstus.rose_user.Count != 0)
                            {
                                foreach (var item in lstus.rose_user)
                                {
                                    if (item.ro_id_lr == 1)
                                    {
                                        foreach (var it in item.detail)
                                        {
                                            if (item.ro_id_user == it.idQLC)
                                            {
                                                item.usName = it.userName;
                                            }
                                        }
                                        item.ro_time_format = $"{item.ro_time.Day}-{item.ro_time.Month}-{item.ro_time.Year}";
                                        lstRoseUser.Add(item);
                                    }
                                    else
                                    {
                                        lstRoseUser.Remove(item);
                                    }
                                }
                            }
                            if (lstRoseUser.Count <= 10) DpPhanTRang.Visibility = Visibility.Collapsed;
                            else DpPhanTRang.Visibility = Visibility.Visible;
                            TongSoTrang = lstRoseUser.Count / 10;
                            SoDu = 10 - (lstRoseUser.Count % 10);
                            if (lstRoseUser.Count % 10 > 0)
                            {
                                TongSoTrang++;
                            }
                            for (int i = 0; i < 10 && i < lstRoseUser.Count; i++)
                            {
                                lstRoseUserCPT.Add(lstRoseUser[i]);
                            }

                            dgvHoaHongCaNhanTien.ItemsSource = lstRoseUserCPT;
                            dgvHoaHongCaNhanTien.Items.Refresh();
                            dgvTongHoaHongTien.ItemsSource = lstRoseUserCPT;
                            dgvTongHoaHongTien.Items.Refresh();

                            if (TongSoTrang < 3)
                            {
                                if (TongSoTrang == 2)
                                {
                                    borPage3.Visibility = Visibility.Collapsed;
                                    borPage2.Visibility = Visibility.Visible;
                                    borLen1.Visibility = Visibility.Visible;
                                    borPageCuoi.Visibility = Visibility.Visible;
                                }
                                else if (TongSoTrang == 1)
                                {
                                    borPage2.Visibility = Visibility.Collapsed;
                                    borPage3.Visibility = Visibility.Collapsed;
                                    borLen1.Visibility = Visibility.Collapsed;
                                    borPageCuoi.Visibility = Visibility.Collapsed;
                                }
                            }
                            else
                            {
                                borLui1.Visibility = Visibility.Collapsed;
                                borPageDau.Visibility = Visibility.Collapsed;
                                borPage2.Visibility = Visibility.Visible;
                                borPage3.Visibility = Visibility.Visible;
                                borLen1.Visibility = Visibility.Visible;
                                borPageCuoi.Visibility = Visibility.Visible;
                            }
                            loading.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            if (lstus.rose_user.Count != 0)
                            {
                                foreach (var item in lstus.rose_user)
                                {
                                    item.usID = item.ro_id_user.ToString();
                                    foreach (var it in item.detail)
                                    {
                                        if (item.ro_id_user == it.idQLC)
                                        {
                                            item.usName = it.userName;
                                        }
                                    }
                                    if (item.ro_id_lr == 1)
                                    {
                                        item.RoseTien = item.ro_price;
                                    }
                                    else if (item.ro_id_lr == 2)
                                    {
                                        item.RoseDoanhThu = item.ro_price;
                                    }
                                    else if (item.ro_id_lr == 3)
                                    {
                                        item.RoseLoiNhuan = item.ro_price;
                                    }
                                    else if (item.ro_id_lr == 4)
                                    {
                                        item.RoseViTri = item.ro_price;
                                    }
                                    else
                                    {
                                        item.RoseKeHoach = item.ro_price;
                                    }
                                    item.RoseTong = item.RoseKeHoach + item.RoseViTri + item.RoseLoiNhuan + item.RoseDoanhThu + item.RoseTien;
                                    lstRoseUser.Add(item);
                                }
                                if (lstRoseUser.Any())
                                {
                                    var uniqueRecords = lstRoseUser.GroupBy(r => new { r.usID, r.usName }).Select(g => new RoseUser
                                    {
                                        usID = g.Key.usID,
                                        usName = g.First().usName,
                                        RoseTien = g.Sum(x => x.RoseTien),
                                        RoseDoanhThu = g.Sum(x => x.RoseDoanhThu),
                                        RoseLoiNhuan = g.Sum(x => x.RoseLoiNhuan),
                                        RoseViTri = g.Sum(x => x.RoseViTri),
                                        RoseKeHoach = g.Sum(x => x.RoseKeHoach),
                                        RoseTong = g.Sum(x => x.RoseTong)
                                    }).ToList();
                                    lstRoseUserObject = uniqueRecords.ToList();
                                }
                            }
                            if (lstRoseUserObject.Count <= 10) DpPhanTRang.Visibility = Visibility.Collapsed;
                            else DpPhanTRang.Visibility = Visibility.Visible;
                            TongSoTrang = lstRoseUserObject.Count / 10;
                            SoDu = 10 - (lstRoseUserObject.Count % 10);
                            if (lstRoseUserObject.Count % 10 > 0)
                            {
                                TongSoTrang++;
                            }
                            for (int i = 0; i < 10 && i < lstRoseUserObject.Count; i++)
                            {
                                lstRoseUserCPT.Add(lstRoseUserObject[i]);
                            }
                            dgvHoaHongNhanDuoc.ItemsSource = lstRoseUserCPT;
                            dgvHoaHongNhanDuoc.Items.Refresh();
                            if (TongSoTrang < 3)
                            {
                                if (TongSoTrang == 2)
                                {
                                    borPage3.Visibility = Visibility.Collapsed;
                                    borPage2.Visibility = Visibility.Visible;
                                    borLen1.Visibility = Visibility.Visible;
                                    borPageCuoi.Visibility = Visibility.Visible;
                                }
                                else if (TongSoTrang == 1)
                                {
                                    borPage2.Visibility = Visibility.Collapsed;
                                    borPage3.Visibility = Visibility.Collapsed;
                                    borLen1.Visibility = Visibility.Collapsed;
                                    borPageCuoi.Visibility = Visibility.Collapsed;
                                }
                            }
                            else
                            {
                                borLui1.Visibility = Visibility.Collapsed;
                                borPageDau.Visibility = Visibility.Collapsed;
                                borPage2.Visibility = Visibility.Visible;
                                borPage3.Visibility = Visibility.Visible;
                                borLen1.Visibility = Visibility.Visible;
                                borPageCuoi.Visibility = Visibility.Visible;
                            }
                        }
                        loading.Visibility = Visibility.Collapsed;
                    }
                } 
                loading.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            { loading.Visibility = Visibility.Collapsed; }
        }

        List<List_Rose_DoanhThu> lstDoanhThuCaNhan = new List<List_Rose_DoanhThu>();
        List<List_Rose_DoanhThu> lstDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
        List<List_Rose_DoanhThu> lstSearchDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
        List<List_Rose_DoanhThu> lstSearchDoanhThuCaNhan = new List<List_Rose_DoanhThu>();
        List<List_Rose_DoanhThu> lstTongDoanhThuCaNhan = new List<List_Rose_DoanhThu>();
        List<List_Rose_DoanhThu> lstTongDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
      
        public async void LoadHoaHongDoanhThuCaNhan()
        {
            try
            {
                loading.Visibility = Visibility.Visible;
                lstDoanhThuCaNhan.Clear();
                lstDoanhThuCaNhanCPT.Clear();
                lstTongDoanhThuCaNhan.Clear();
                lstTongDoanhThuCaNhanCPT.Clear();
                Month = int.Parse(cbo_ChonThang.Text.Split(' ')[1]);
                Year = int.Parse(cbo_ChonNam.Text.Split(' ')[1]);
                Day = int.Parse(DateTime.Now.Day.ToString());
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/take_hoa_hong_dt_ca_nhan");
                var content = new MultipartFormDataContent();
                DateTime firstDayOfMonth = new DateTime(Year, Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                if (Month < 10)
                {
                    content.Add(new StringContent($"{Year}-0{Month}-0{firstDayOfMonth.Day}"), "start_date");
                    content.Add(new StringContent($"{Year}-0{Month}-{lastDayOfMonth.Day}"), "end_date");
                }
                else if (Month >= 10)
                {
                    content.Add(new StringContent($"{Year}-{Month}-0{firstDayOfMonth.Day}"), "start_date");
                    content.Add(new StringContent($"{Year}-{Month}-{lastDayOfMonth.Day}"), "end_date");
                }

                if (searchBarNhanVien.SelectedItem != null && ((ListUser)searchBarNhanVien.SelectedItem)._id > 0)
                {
                    content.Add(new StringContent(Rose_Id_User), "ro_id_user");
                } 
                
                content.Add(new StringContent(Main.IdAcount.ToString()), "ro_id_com");
                content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var resContent = await response.Content.ReadAsStringAsync();
                    Root_DoanhThu rose_DoanhThu_CaNhan = JsonConvert.DeserializeObject<Root_DoanhThu>(resContent);
                    if (rose_DoanhThu_CaNhan.data != null)
                    {
                        foreach (var item in rose_DoanhThu_CaNhan.data)
                        {
                            item.ro_time_format = $"{item.ro_time.Month}-{item.ro_time.Year}";
                            item.userName = item.detail.userName;
                            double phantram = (double.Parse(item.TinhluongThietLap.tl_phan_tram.numberDecimal) / 100);
                            item.ro_Rose_dt = (item.ro_price * phantram).ToString("0");
                            item.ro_Rose_dt_long = Math.Round((item.ro_price * phantram), 0);
                            lstDoanhThuCaNhan.Add(item);
                        }
                        var uniqueRecords = lstDoanhThuCaNhan.GroupBy(r => new { r.ID, r.userName }).Select(g => new List_Rose_DoanhThu
                        {
                            ID = g.First().ro_id_user,
                            Name = g.First().userName,
                            TongTien = g.Sum(x => x.ro_Rose_dt_long).ToString("0"),
                        }).ToList();
                        lstTongDoanhThuCaNhan = uniqueRecords.ToList();

                        if (CN == 1)
                        {
                            lstTongDoanhThuCaNhan.Clear();
                            if (lstDoanhThuCaNhan.Count <= 10) DpPhanTRang.Visibility = Visibility.Collapsed;
                            else DpPhanTRang.Visibility = Visibility.Visible;
                            TongSoTrang = lstDoanhThuCaNhan.Count / 10;
                            SoDu = 10 - (lstDoanhThuCaNhan.Count % 10);
                            if (lstDoanhThuCaNhan.Count % 10 > 0)
                            {
                                TongSoTrang++;
                            }
                            for (int i = 0; i < 10 && i < lstDoanhThuCaNhan.Count; i++)
                            {
                                lstDoanhThuCaNhanCPT.Add(lstDoanhThuCaNhan[i]);
                            }
                            dgvHoaHongDoanhThu.ItemsSource = lstDoanhThuCaNhanCPT;
                            dgvHoaHongDoanhThu.Items.Refresh();
                        }
                        else if (CN == 2) 
                        {
                            lstDoanhThuCaNhan.Clear();
                            if (lstTongDoanhThuCaNhan.Count <= 10) DpPhanTRang.Visibility = Visibility.Collapsed;
                            else DpPhanTRang.Visibility = Visibility.Visible;
                            TongSoTrang = lstTongDoanhThuCaNhan.Count / 10;
                            SoDu = 10 - (lstTongDoanhThuCaNhan.Count % 10);
                            if (lstTongDoanhThuCaNhan.Count % 10 > 0)
                            {
                                TongSoTrang++;
                            }
                            for (int i = 0; i < 10 && i < lstTongDoanhThuCaNhan.Count; i++)
                            {
                                lstTongDoanhThuCaNhanCPT.Add(lstTongDoanhThuCaNhan[i]);
                            }
                            dgvTongHoaHongDoanhThu.ItemsSource = lstTongDoanhThuCaNhanCPT;
                            dgvTongHoaHongDoanhThu.Items.Refresh();
                        }
                        if (TongSoTrang < 3)
                        {
                            if (TongSoTrang == 2)
                            {
                                borPage1.Background =  (Brush)brus.ConvertFrom("#4c5bd4");
                                borPage3.Visibility = Visibility.Collapsed;
                                borPage2.Visibility = Visibility.Visible;
                                borLen1.Visibility = Visibility.Visible;
                                borPageCuoi.Visibility = Visibility.Visible;
                            }
                            else if (TongSoTrang == 1)
                            {
                                borPage2.Visibility = Visibility.Collapsed;
                                borPage3.Visibility = Visibility.Collapsed;
                                borLen1.Visibility = Visibility.Collapsed;
                                borPageCuoi.Visibility = Visibility.Collapsed;
                            }
                        }
                        else
                        {
                            borLui1.Visibility = Visibility.Collapsed;
                            borPageDau.Visibility = Visibility.Collapsed;
                            borPage2.Visibility = Visibility.Visible;
                            borPage3.Visibility = Visibility.Visible;
                            borLen1.Visibility = Visibility.Visible;
                            borPageCuoi.Visibility = Visibility.Visible;
                        }
                    }
                }
                loading.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
            }
        }

        List<List_Rose_LoiNhuan> lstLoiNhuanCaNhan = new List<List_Rose_LoiNhuan>();
        List<List_Rose_LoiNhuan> lstLoiNhuanCaNhanCPT = new List<List_Rose_LoiNhuan>();
        List<List_Rose_LoiNhuan> lstSearchLoiNhuanCaNhan = new List<List_Rose_LoiNhuan>();
        public void LoadHoaHongLoiNhuan()
        {
            try
            {
                using (WebClient request = new WebClient())
                {
                    loading.Visibility = Visibility.Visible;
                    lstLoiNhuanCaNhan.Clear();
                    lstLoiNhuanCaNhanCPT.Clear();
                    Month = int.Parse(cbo_ChonThang.Text.Split(' ')[1]);
                    Year = int.Parse(cbo_ChonNam.Text.Split(' ')[1]);
                    Day = int.Parse(DateTime.Now.Day.ToString());
                    DateTime firstDayOfMonth = new DateTime(Year, Month, 1);
                    DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                    if (Month < 10)
                    {
                        request.QueryString.Add("start_date", $"new StringContent({Year}-0{Month}-0{firstDayOfMonth.Day}");
                        request.QueryString.Add("end_date", $"{Year}-0{Month}-{lastDayOfMonth.Day}");
                    }
                    else if (Month >= 10)
                    {
                       request.QueryString.Add("start_date", $"new StringContent({Year}-0{Month}-0{firstDayOfMonth.Day}");
                        request.QueryString.Add("end_date", $"{Year}-0{Month}-{lastDayOfMonth.Day}");
                    }
                    if (searchBarNhanVien.SelectedItem != null && ((ListUser)searchBarNhanVien.SelectedItem)._id > 0)
                    {
                        request.QueryString.Add("ro_id_user", Rose_Id_User);
                    }
                    request.QueryString.Add("ro_id_com", Main.IdAcount.ToString());
                    request.QueryString.Add("token", Properties.Settings.Default.Token);
                    request.UploadValuesCompleted += (s, e) =>
                    {
                        try
                        {
                            Root_LoiNhuan lstLoiNhuan = JsonConvert.DeserializeObject<Root_LoiNhuan>(UnicodeEncoding.UTF8.GetString(e.Result));
                            if (lstLoiNhuan.data != null)
                            {
                                foreach (var item in lstLoiNhuan.data)
                                {
                                    item.userName = item.detail.userName;
                                    item.ro_tl_name = item.TinhluongThietLap.tl_name;
                                    item.ro_rose_ln = (item.ro_price * (double.Parse(item.TinhluongThietLap.tl_phan_tram.numberDecimal) / 100)).ToString("0.00");
                                    item.ro_time_created_format = $"{item.ro_time_created.Day}-{item.ro_time_created.Month}-{item.ro_time_created.Year}";
                                    lstLoiNhuanCaNhan.Add(item);
                                }
                                if (lstLoiNhuan.data.Count <= 10) DpPhanTRang.Visibility = Visibility.Collapsed;
                                else DpPhanTRang.Visibility = Visibility.Visible;
                                TongSoTrang = lstLoiNhuan.data.Count / 10;
                                SoDu = 10 - (lstLoiNhuan.data.Count % 10);
                                if (lstLoiNhuan.data.Count % 10 > 0)
                                {
                                    TongSoTrang++;
                                }
                                for (int i = 0; i < 10 && i < lstLoiNhuan.data.Count; i++)
                                {
                                    lstLoiNhuanCaNhanCPT.Add(lstLoiNhuan.data[i]);
                                }
                                dgvHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                                dgvTongHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                                dgvHoaHongLoiNhuan.Items.Refresh();
                                dgvTongHoaHongLoiNhuan.Items.Refresh();
                                if (TongSoTrang < 3)
                                {
                                    if (TongSoTrang == 2)
                                    {
                                        borPage3.Visibility = Visibility.Collapsed;
                                        borPage2.Visibility = Visibility.Visible;
                                        borLen1.Visibility = Visibility.Visible;
                                        borPageCuoi.Visibility = Visibility.Visible;
                                    }
                                    else if (TongSoTrang == 1)
                                    {
                                        borPage2.Visibility = Visibility.Collapsed;
                                        borPage3.Visibility = Visibility.Collapsed;
                                        borLen1.Visibility = Visibility.Collapsed;
                                        borPageCuoi.Visibility = Visibility.Collapsed;
                                    }
                                }
                                else
                                {
                                    borLui1.Visibility = Visibility.Collapsed;
                                    borPageDau.Visibility = Visibility.Collapsed;
                                    borPage2.Visibility = Visibility.Visible;
                                    borPage3.Visibility = Visibility.Visible;
                                    borLen1.Visibility = Visibility.Visible;
                                    borPageCuoi.Visibility = Visibility.Visible;
                                }
                            }
                            loading.Visibility = Visibility.Collapsed;
                        }
                        catch { }
                    };
                    request.UploadValuesTaskAsync("https://api.timviec365.vn/api/tinhluong/congty/take_hoa_hong_ln_ca_nhan",
                        request.QueryString);
                }
            }
            catch (Exception)
            {
            }
        }

        List<List_Rose_ViTri> lstHoaHongViTri = new List<List_Rose_ViTri>();
        List<List_Rose_ViTri> lstHoaHongViTriCPT = new List<List_Rose_ViTri>();
        List<List_Rose_ViTri> lstSearchHoaHongViTri = new List<List_Rose_ViTri>();
        public async void LoadHoaHongViTri()
        {
            try 
            {
                loading.Visibility = Visibility.Visible;
                lstHoaHongViTri.Clear();
                lstHoaHongViTriCPT.Clear();
                Month = int.Parse(cbo_ChonThang.Text.Split(' ')[1]);
                Year = int.Parse(cbo_ChonNam.Text.Split(' ')[1]);
                Day = int.Parse(DateTime.Now.Day.ToString());
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/take_hoa_hong_vt_ca_nhan");
                var content = new MultipartFormDataContent();
                DateTime firstDayOfMonth = new DateTime(Year, Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                if (Month < 10)
                {

                    content.Add(new StringContent($"{Year}-0{Month}-0{firstDayOfMonth.Day}"), "start_date");
                    content.Add(new StringContent($"{Year}-0{Month}-{lastDayOfMonth.Day}"), "end_date");

                }
                else if (Year >= 10)
                {
                    content.Add(new StringContent($"{Year}-{Month}-0{firstDayOfMonth.Day}"), "start_date");
                    content.Add(new StringContent($"{Year}-{Month}-{lastDayOfMonth.Day}"), "end_date");
                }

                if (searchBarNhanVien.SelectedItem != null && ((ListUser)searchBarNhanVien.SelectedItem)._id > 0)
                {
                    content.Add(new StringContent(Rose_Id_User), "ro_id_user");
                }
                content.Add(new StringContent(Main.IdAcount.ToString()), "ro_id_com");
                content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                { 
                    var resContent = await response.Content.ReadAsStringAsync();
                    Root_Vitri rose_ViTri_CaNhan = JsonConvert.DeserializeObject<Root_Vitri>(resContent);
                    if (rose_ViTri_CaNhan.data != null)
                    {
                        foreach (var item in rose_ViTri_CaNhan.data)
                        {
                            item.ro_time_format = $"{item.ro_time.Day}-{item.ro_time.Month}-{item.ro_time.Year}";
                            item.userName = item.detail.userName;
                            item.ro_rose_vt = (item.ro_price * (double.Parse(item.TinhluongThietLap.tl_phan_tram.numberDecimal) / 100)).ToString("0.00");
                            item.productName = item.TinhluongThietLap.tl_name;
                            lstHoaHongViTri.Add(item);
                        }
                        //lstHoaHongViTri = rose_ViTri_CaNhan.data;
                        if (rose_ViTri_CaNhan.data.Count <= 10) DpPhanTRang.Visibility = Visibility.Collapsed;
                        else DpPhanTRang.Visibility = Visibility.Visible;
                        TongSoTrang = rose_ViTri_CaNhan.data.Count / 10;
                        SoDu = 10 - (rose_ViTri_CaNhan.data.Count % 10);
                        if (rose_ViTri_CaNhan.data.Count % 10 > 0)
                        {
                            TongSoTrang++;
                        }
                        for (int i = 0; i < 10 && i < rose_ViTri_CaNhan.data.Count; i++)
                        {
                            lstHoaHongViTriCPT.Add(rose_ViTri_CaNhan.data[i]);
                        }
                        dgvHoaHongLePhiViTri.ItemsSource = lstHoaHongViTriCPT;
                        dgvHoaHongLePhiViTri.Items.Refresh();
                        dgvTongHoaHongViTri.ItemsSource= lstHoaHongViTriCPT;
                        dgvTongHoaHongViTri.Items.Refresh();
                        if (TongSoTrang < 3)
                        {
                            if (TongSoTrang == 2)
                            {
                                borPage3.Visibility = Visibility.Collapsed;
                                borPage2.Visibility = Visibility.Visible;
                                borLen1.Visibility = Visibility.Visible;
                                borPageCuoi.Visibility = Visibility.Visible;
                            }
                            else if (TongSoTrang == 1)
                            {
                                borPage2.Visibility = Visibility.Collapsed;
                                borPage3.Visibility = Visibility.Collapsed;
                                borLen1.Visibility = Visibility.Collapsed;
                                borPageCuoi.Visibility = Visibility.Collapsed;
                            }
                        }
                        else
                        {
                            borLui1.Visibility = Visibility.Collapsed;
                            borPageDau.Visibility = Visibility.Collapsed;
                            borPage2.Visibility = Visibility.Visible;
                            borPage3.Visibility = Visibility.Visible;
                            borLen1.Visibility = Visibility.Visible;
                            borPageCuoi.Visibility = Visibility.Visible;
                        }
                    }
                }
                loading.Visibility = Visibility.Collapsed;
            }
            catch { }
        }

        List<Datum_kh> lstHoaHongKeHoach = new List<Datum_kh>();
        List<Datum_kh> lstHoaHongKeHoachCPT = new List<Datum_kh>();
        List<Datum_kh> lstSearchHoaHongKeHoach = new List<Datum_kh>();
        public async void LoadHoaHongKeHoach()
        {
            try
            {
                loading.Visibility = Visibility.Visible;
                lstHoaHongKeHoach.Clear();
                lstHoaHongKeHoachCPT.Clear();
                Month = int.Parse(cbo_ChonThang.Text.Split(' ')[1]);
                Year = int.Parse(cbo_ChonNam.Text.Split(' ')[1]);
                Day = int.Parse(DateTime.Now.Day.ToString());
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/take_hoa_hong_kh_ca_nhan");
                var content = new MultipartFormDataContent();
                DateTime firstDayOfMonth = new DateTime(Year, Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                if (Month < 10)
                {
                    content.Add(new StringContent($"{Year}-0{Month}-0{firstDayOfMonth.Day}"), "start_date");
                    content.Add(new StringContent($"{Year}-0{Month}-{lastDayOfMonth.Day}"), "end_date");
                }
                else if (Year >= 10)
                {
                    content.Add(new StringContent($"{Year}-{Month}-0{firstDayOfMonth.Day}"), "start_date");
                    content.Add(new StringContent($"{Year}-{Month}-{lastDayOfMonth.Day}"), "end_date");
                }
                if (searchBarNhanVien.SelectedItem != null && ((ListUser)searchBarNhanVien.SelectedItem)._id > 0)
                {
                    content.Add(new StringContent(Rose_Id_User), "ro_id_user");
                }
                content.Add(new StringContent(Main.IdAcount.ToString()), "ro_id_com");
                content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                content.Add(new StringContent("5"), "tl_id_rose");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var resContent = await response.Content.ReadAsStringAsync();
                    Root_kh lstHHKeHoach = JsonConvert.DeserializeObject<Root_kh>(resContent);
                    if (lstHHKeHoach.data != null)
                    {
                        foreach (var item in lstHHKeHoach.data)
                        { 
                            item.ro_name_user = item.detail.userName;
                            item.ro_time_format = $"{item.ro_time.Day}-{item.ro_time.Month}-{item.ro_time.Year}";
                            item.ro_name_tl = item.TinhluongThietLap.tl_name;
                             //item.tl_HoaHong = it.tl_hoahong;
                             //item.tl_Name = it.tl_name;
                             //item.ro_time_format = $"{item.ro_time.Day}-{item.ro_time.Month}-{item.ro_time.Year}";
                             //foreach (var item3 in item.TinhluongThietLap)
                             //{
                             //    item.ro_rose_kh = (item.ro_price * (double.Parse(item3.tl_phan_tram.numberDecimal) / 100)).ToString("0.00");
                             //}
                             lstHoaHongKeHoach.Add(item);    
                        }
                        if (lstHoaHongKeHoach.Count <= 10) DpPhanTRang.Visibility = Visibility.Collapsed;
                        else DpPhanTRang.Visibility = Visibility.Visible;
                        TongSoTrang = lstHoaHongKeHoach.Count / 10;
                        SoDu = 10 - (lstHoaHongKeHoach.Count % 10);
                        if (lstHoaHongKeHoach.Count % 10 > 0)
                        {
                            TongSoTrang++;
                        }
                        for (int i = 0; i < 10 && i < lstHoaHongKeHoach.Count; i++)
                        {
                            lstHoaHongKeHoachCPT.Add(lstHoaHongKeHoach[i]);
                        }
                        dgvHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                        dgvHoaHongKeHoach.Items.Refresh();
                        dgvTongHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                        dgvTongHoaHongKeHoach.Items.Refresh();
                        if (TongSoTrang < 3)
                        {
                            if (TongSoTrang == 2)
                            {
                                borPage3.Visibility = Visibility.Collapsed;
                                borPage2.Visibility = Visibility.Visible;
                                borLen1.Visibility = Visibility.Visible;
                                borPageCuoi.Visibility = Visibility.Visible;
                            }
                            else if (TongSoTrang == 1)
                            {
                                borPage2.Visibility = Visibility.Collapsed;
                                borPage3.Visibility = Visibility.Collapsed;
                                borLen1.Visibility = Visibility.Collapsed;
                                borPageCuoi.Visibility = Visibility.Collapsed;
                            }
                        }
                        else
                        {
                            borLui1.Visibility = Visibility.Collapsed;
                            borPageDau.Visibility = Visibility.Collapsed;
                            borPage2.Visibility = Visibility.Visible;
                            borPage3.Visibility = Visibility.Visible;
                            borLen1.Visibility = Visibility.Visible;
                            borPageCuoi.Visibility = Visibility.Visible;
                        }
                    }
                }
                loading.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {}
        }
        private void btnThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
            if (Back == 21 || Next1 != null)
            {
                try
                {
                   lstSearchRoseUser.Clear();
                    if (searchBarNhanVien.SelectedItem != null && ((ListUser)searchBarNhanVien.SelectedItem)._id > 0)
                    {
                        foreach (var item in lstRoseUser)
                        {
                            if (Rose_Id_User == item.ro_id_user.ToString())
                            {
                                lstSearchRoseUser.Add(item);
                            }
                        }
                        dgvHoaHongCaNhanTien.ItemsSource = lstSearchRoseUser;
                        dgvHoaHongCaNhanTien.Items.Refresh();
                        dgvTongHoaHongTien.ItemsSource = lstSearchRoseUser;
                        dgvTongHoaHongTien.Items.Refresh();
                    }
                    else if (searchBarNhanVien.SelectedIndex == 0)
                    {
                        LoadListRoseSaff();
                    }
                    else
                    {
                        LoadListRoseSaff();
                    }
                }
                catch (Exception)
                {
                }
                
            }
            else if (Next2 == "RoseMoney2" || HoaHongDoanhThu == 22)
            {
                try
                {
                    lstDoanhThuCaNhanCPT.Clear();
                    lstTongDoanhThuCaNhanCPT.Clear();
                    lstSearchDoanhThuCaNhan.Clear();
                    lstSearchDoanhThuCaNhanCPT.Clear();
                    if (searchBarNhanVien.SelectedItem != null && ((ListUser)searchBarNhanVien.SelectedItem)._id > 0)
                    {
                        if (CN == 1)
                        {
                            foreach (var item in lstDoanhThuCaNhan)
                            {
                                if (Rose_Id_User == item.ro_id_user.ToString())
                                {
                                    lstSearchDoanhThuCaNhan.Add(item);
                                }
                            }
                        }
                        else if (CN == 2)
                        {
                            foreach (var itemTong in lstTongDoanhThuCaNhan)
                            {
                                if (Rose_Id_User == itemTong.ID.ToString())
                                {
                                    lstSearchDoanhThuCaNhan.Add(itemTong);
                                }
                            }
                        }
                        if (lstSearchDoanhThuCaNhan.Count <= 10) DpPhanTRang.Visibility = Visibility.Collapsed;
                        else DpPhanTRang.Visibility = Visibility.Visible;
                        TongSoTrang = lstSearchDoanhThuCaNhan.Count / 10;
                        SoDu = 10 - (lstSearchDoanhThuCaNhan.Count % 10);
                        if (lstSearchDoanhThuCaNhan.Count % 10 > 0)
                        {
                            TongSoTrang++;
                        }
                        for (int i = 0; i < 10 && i < lstSearchDoanhThuCaNhan.Count; i++)
                        {
                            lstSearchDoanhThuCaNhanCPT.Add(lstSearchDoanhThuCaNhan[i]);
                        }
                        if (CN == 1)
                        {
                            dgvHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                            dgvHoaHongDoanhThu.Items.Refresh();
                        }
                        else
                        {
                            dgvTongHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                            dgvTongHoaHongDoanhThu.Items.Refresh();
                        }
                        if (TongSoTrang < 3)
                        {
                            if (TongSoTrang == 2)
                            {
                                borPage1.Background = (Brush)brus.ConvertFrom("#4C5BD4");
                                borPage3.Visibility = Visibility.Collapsed;
                                borPage2.Visibility = Visibility.Visible;
                                borLen1.Visibility = Visibility.Visible;
                                borPageCuoi.Visibility = Visibility.Visible;
                            }
                            else if (TongSoTrang == 1)
                            {
                                borPage2.Visibility = Visibility.Collapsed;
                                borPage3.Visibility = Visibility.Collapsed;
                                borLen1.Visibility = Visibility.Collapsed;
                                borPageCuoi.Visibility = Visibility.Collapsed;
                            }
                        }
                        else
                        {
                            borLui1.Visibility = Visibility.Collapsed;
                            borPageDau.Visibility = Visibility.Collapsed;
                            borPage2.Visibility = Visibility.Visible;
                            borPage3.Visibility = Visibility.Visible;
                            borLen1.Visibility = Visibility.Visible;
                            borPageCuoi.Visibility = Visibility.Visible;
                        }
                    }
                    else if (searchBarNhanVien.SelectedIndex == 0)
                    {
                        LoadHoaHongDoanhThuCaNhan();
                    }
                    else
                    {
                        LoadHoaHongDoanhThuCaNhan();
                    }
                }
                catch (Exception)
                { }
            }
            else if (LoiNhuan == 23 || HoaHongLoiNhuan == 23)
            {
                try
                {
                    lstSearchLoiNhuanCaNhan.Clear();
                    if (searchBarNhanVien.SelectedItem != null && ((ListUser)searchBarNhanVien.SelectedItem)._id > 0)
                    {
                        foreach (var item in lstLoiNhuanCaNhan)
                        {
                            if (Rose_Id_User == item.ro_id_user.ToString())
                            {
                                lstSearchLoiNhuanCaNhan.Add(item);
                            }
                            dgvHoaHongLoiNhuan.ItemsSource = lstSearchLoiNhuanCaNhan;
                            dgvTongHoaHongLoiNhuan.ItemsSource = lstSearchLoiNhuanCaNhan;
                            dgvHoaHongLoiNhuan.Items.Refresh();
                            dgvTongHoaHongLoiNhuan.Items.Refresh();
                        }
                    }
                    else if (searchBarNhanVien.SelectedIndex == 0)
                    {
                        LoadHoaHongLoiNhuan();
                    }
                    else
                    {
                        LoadHoaHongLoiNhuan();
                    }
                }
                catch (Exception)
                {
                }
            }
            else if (KeHoach == 25 || HoaHongKeHoach == 25)
            {
                try
                {
                    lstSearchHoaHongKeHoach.Clear();
                    if (searchBarNhanVien.SelectedItem != null && ((ListUser)searchBarNhanVien.SelectedItem)._id > 0)
                    {
                        foreach (var item in lstHoaHongKeHoach)
                        {
                            if (Rose_Id_User == item.ro_id_user.ToString())
                            {
                                lstSearchHoaHongKeHoach.Add(item);
                            }
                            dgvHoaHongKeHoach.ItemsSource = lstSearchHoaHongKeHoach;
                            dgvHoaHongKeHoach.Items.Refresh();
                            dgvTongHoaHongKeHoach.ItemsSource = lstSearchHoaHongKeHoach;
                            dgvTongHoaHongKeHoach.Items.Refresh();
                        }
                    }
                    else if (searchBarNhanVien.SelectedIndex == 0)
                    {
                        LoadHoaHongKeHoach();
                    }
                    else
                    {
                        LoadHoaHongKeHoach();
                    }
                }
                catch (Exception)
                {} 
            }
            else if (ViTri == 24 || HoaHongViTri == 24)
            {
                try
                {
                    lstSearchHoaHongViTri.Clear();
                    if (searchBarNhanVien.SelectedItem != null && ((ListUser)searchBarNhanVien.SelectedItem)._id > 0)
                    {
                        foreach (var item in lstHoaHongViTri)
                        {
                            if (Rose_Id_User == item.ro_id_user.ToString())
                            {
                                lstSearchHoaHongViTri.Add(item);
                            }
                            dgvHoaHongLePhiViTri.ItemsSource = lstSearchHoaHongViTri;
                            dgvHoaHongLePhiViTri.Items.Refresh();
                            dgvTongHoaHongViTri.ItemsSource = lstSearchHoaHongViTri;
                            dgvTongHoaHongViTri.Items.Refresh();
                        }
                    }
                    else if (searchBarNhanVien.SelectedIndex == 0)
                    {
                        LoadHoaHongViTri();
                    }
                    else
                    {
                        LoadHoaHongViTri();
                    }
                }
                catch (Exception)
                {}
            }
            else
            {
                LoadListRoseSaff();
            }
        }
        List<ListUser> listSaftSearch = new List<ListUser>();
        private void ChonNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (searchBarNhanVien.SelectedItem != null)
                {
                    searchBarNhanVien.PlaceHolderForground = "#474747";
                    listSaftSearch = new List<ListUser>();
                    var chonca = ((ListUser)searchBarNhanVien.SelectedItem).idQLC.ToString();
                    Rose_Id_User = chonca;
                    if (!Main.lstNhanVienThuocCongTy.Any(item => item.idQLC.ToString() == chonca))
                    {
                        listSaftSearch = Main.lstNhanVienThuocCongTy.ToList();
                        foreach (var item in listSaftSearch)
                        {
                            Main.NhanVien = item.userName;
                            Rose_Id_User = item.idQLC.ToString();
                        }
                    }
                }
                else
                {
                    searchBarNhanVien.PlaceHolderForground = "#ACACAC";
                }
            }
            catch (Exception)
            {
            }
        }
        private void cboChonThang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbo_ChonThang.SelectedItem != null)
                {
                    cbo_ChonThang.PlaceHolderForground = "#474747";
                    LoadHoaHongDoanhThuCaNhan();
                }
                else
                {
                    cbo_ChonThang.PlaceHolderForground = "#ACACAC";
                }
            }
            catch (Exception)
            { }
        }
        private void cboChonNam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbo_ChonNam.SelectedItem != null)
                {
                    LoadHoaHongDoanhThuCaNhan();
                    cbo_ChonNam.PlaceHolderForground = "#474747";
                }
                else
                {
                    cbo_ChonNam.PlaceHolderForground = "#ACACAC";
                }
            }
            catch (Exception)
            { }
        }
        public void LoadListMonth()
        {
            Montlist = new List<string>();
            cbo_ChonThang.Text = "Tháng "+ DateTime.Now.Month.ToString();
            for (var i = 1; i <= 12; i++)
            {
                Montlist.Add($"Tháng {i}");
            }
            cbo_ChonThang.ItemsSource = Montlist;
        }
        public void LoadListYear()
        {
            YearList = new List<string>();
            var c = DateTime.Now.Year;
            cbo_ChonNam.Text = "Năm " + c;
            if (c.ToString() != null)
            {
                YearList.Add($"Năm {c - 5}");
                YearList.Add($"Năm {c - 4}");
                YearList.Add($"Năm {c - 3}");
                YearList.Add($"Năm {c - 2}");
                YearList.Add($"Năm {c - 1}");
                YearList.Add($"Năm {c}");
                YearList.Add($"Năm {c + 1}");
                YearList.Add($"Năm {c + 2}");
                YearList.Add($"Năm {c + 3}");
                YearList.Add($"Năm {c + 4}");
                YearList.Add($"Năm {c + 5}");
            }
            cbo_ChonNam.ItemsSource = YearList;
        }
        public List<OOP.clsNhanVienThuocCongTy.ListUser> lstNhanVienThuocCongTy = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        public void LoadNhanhVien()
        {
           try { searchBarNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy; /*searchBarNhanVien.SelectedIndex = 0; */} catch (Exception) { }  
        }
        
        
        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        #region Phan trang
        private void borPageDau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BrushConverter brus = new BrushConverter();
                borPageDau.Visibility = Visibility.Collapsed;
                borLui1.Visibility = Visibility.Collapsed;
                borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                textPage1.Text = "1";
                textPage2.Text = "2";
                textPage3.Text = "3";
                borLen1.Visibility = Visibility.Visible;
                if (TongSoTrang > 2)
                {
                    borPage2.Visibility = Visibility.Visible;
                    borPage3.Visibility = Visibility.Visible;
                }
                else if (TongSoTrang > 1)
                {
                    borPage2.Visibility = Visibility.Visible;
                    borPage3.Visibility = Visibility.Collapsed;
                }
                else
                {
                    borPage2.Visibility = Visibility.Collapsed;
                    borPage3.Visibility = Visibility.Collapsed;
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Collapsed;
                }
                if (TongSoTrang > 1)
                {
                    borPageCuoi.Visibility = Visibility.Visible;
                    borLen1.Visibility = Visibility.Visible;
                }
                if (lstRoseUser.Count != 0)
                {
                    if (lstRoseUserObject.Count != 0)
                    {
                        lstRoseUserCPT = new List<RoseUser>();
                        for (int i = 0; i < 10; i++)
                        {
                            lstRoseUserCPT.Add(lstRoseUserObject[i]);
                        }
                        dgvHoaHongNhanDuoc.ItemsSource = lstRoseUserCPT;
                        PageNumberCurrent = 1;
                    }
                    else
                    {
                        lstRoseUserCPT = new List<RoseUser>();
                        for (int i = 0; i < 10; i++)
                        {
                            lstRoseUserCPT.Add(lstRoseUser[i]);
                        }
                        dgvHoaHongCaNhanTien.ItemsSource = lstRoseUserCPT;
                        dgvTongHoaHongTien.ItemsSource = lstRoseUserCPT;
                        PageNumberCurrent = 1;
                    }
                    
                }
                else if(lstDoanhThuCaNhan.Count != 0 || lstTongDoanhThuCaNhan.Count != 0)
                {
                    lstDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                    lstTongDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                    lstSearchDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                    for (int i = 0; i < 10; i++)
                    {
                        if (lstSearchDoanhThuCaNhan.Count != 0)
                        {
                            lstSearchDoanhThuCaNhanCPT.Add(lstSearchDoanhThuCaNhan[i]);
                        }
                        else if (CN == 2)
                        {
                            lstTongDoanhThuCaNhanCPT.Add(lstTongDoanhThuCaNhan[i]);
                        }
                        else if (CN == 1)
                        {
                            lstDoanhThuCaNhanCPT.Add(lstDoanhThuCaNhan[i]);
                        }
                    }
                    if (lstSearchDoanhThuCaNhan.Count != 0)
                    {
                        if (CN == 1)
                        {
                            dgvHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                        }
                        else
                        {
                            dgvTongHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                        }
                    }
                    else
                    {
                        if (CN == 1)
                        {
                            dgvHoaHongDoanhThu.ItemsSource = lstDoanhThuCaNhanCPT;
                        }
                        else if (CN == 2)
                        {
                            dgvTongHoaHongDoanhThu.ItemsSource = lstTongDoanhThuCaNhanCPT;
                        }
                    }
                    PageNumberCurrent = 1;
                }
                else if (lstLoiNhuanCaNhan.Count != 0)
                {
                    lstLoiNhuanCaNhanCPT = new List<List_Rose_LoiNhuan>();
                    for (int i = 0; i < 10; i++)
                    {
                        lstLoiNhuanCaNhanCPT.Add(lstLoiNhuanCaNhan[i]);
                    }
                    dgvHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                    dgvTongHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                    PageNumberCurrent = 1;
                }
                else if (lstHoaHongViTri.Count != 0)
                {
                    lstHoaHongViTriCPT = new List<List_Rose_ViTri>();
                    for (int i = 0; i < 10; i++)
                    {
                        lstHoaHongViTriCPT.Add(lstHoaHongViTri[i]);
                    }
                    dgvHoaHongLePhiViTri.ItemsSource = lstHoaHongViTriCPT;
                    dgvTongHoaHongViTri.ItemsSource = lstHoaHongViTriCPT;
                    PageNumberCurrent = 1;
                }
                else if (lstHoaHongKeHoach.Count != 0)
                {
                    lstHoaHongKeHoachCPT = new List<Datum_kh>();
                    for (int i = 0; i < 10; i++)
                    {
                        lstHoaHongKeHoachCPT.Add(lstHoaHongKeHoach[i]);
                    }
                    dgvHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                    dgvTongHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                    PageNumberCurrent = 1;
                }
            }
            catch (Exception)
            {
            }
        }
        private void borLui1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (PageNumberCurrent > 2)
                {
                    textPage1.Text = (PageNumberCurrent - 2).ToString();
                    textPage2.Text = (PageNumberCurrent - 1).ToString();
                    textPage3.Text = PageNumberCurrent.ToString();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                }
                else if (TongSoTrang > 2)
                {
                    textPage1.Text = (PageNumberCurrent - 1).ToString();
                    textPage2.Text = (PageNumberCurrent).ToString();
                    textPage3.Text = (PageNumberCurrent + 1).ToString();
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageDau.Visibility = Visibility.Collapsed;
                    borLui1.Visibility = Visibility.Collapsed;
                }
                else
                {
                    textPage1.Text = "1";
                    textPage2.Text = "2";
                    textPage3.Text = (PageNumberCurrent + 1).ToString();
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageDau.Visibility = Visibility.Collapsed;
                    borLui1.Visibility = Visibility.Collapsed;
                    borPage3.Visibility = Visibility.Collapsed;
                }
                borPageCuoi.Visibility = Visibility.Visible;
                borLen1.Visibility = Visibility.Visible;
                PageNumberCurrent--;
                if (lstRoseUser.Count != 0)
                {
                    if (lstRoseUserObject.Count != 0)
                    {
                        lstRoseUserCPT = new List<RoseUser>();
                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstRoseUserObject.Count; i++)
                        {
                            lstRoseUserCPT.Add(lstRoseUserObject[i]);
                        }
                        dgvHoaHongNhanDuoc.ItemsSource = lstRoseUserCPT;
                    }
                    else
                    {
                        lstRoseUserCPT = new List<RoseUser>();
                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstRoseUser.Count; i++)
                        {
                            lstRoseUserCPT.Add(lstRoseUser[i]);
                        }
                        dgvHoaHongCaNhanTien.ItemsSource = lstRoseUserCPT;
                        dgvTongHoaHongTien.ItemsSource = lstRoseUserCPT;
                    }
                }
                else if (lstDoanhThuCaNhan.Count != 0 || lstTongDoanhThuCaNhan.Count != 0)
                {
                    lstDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                    lstTongDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                    lstSearchDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                    if (lstSearchDoanhThuCaNhan.Count != 0)
                    {
                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstSearchDoanhThuCaNhan.Count; i++)
                        {
                            lstSearchDoanhThuCaNhanCPT.Add(lstSearchDoanhThuCaNhan[i]);
                        }
                    }
                    else
                    {
                        if (CN == 1)
                        {
                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstDoanhThuCaNhan.Count; i++)
                            {
                                lstDoanhThuCaNhanCPT.Add(lstDoanhThuCaNhan[i]);
                            }
                        }
                        else if (CN == 2)
                        {
                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstTongDoanhThuCaNhan.Count; i++)
                            {
                                lstTongDoanhThuCaNhanCPT.Add(lstTongDoanhThuCaNhan[i]);
                            }
                        }
                    }
                    if (lstSearchDoanhThuCaNhan.Count != 0)
                    {
                        if (CN == 1)
                        {
                            dgvHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                        }
                        else
                        {
                            dgvTongHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                        }
                    }
                    else
                    {
                        if (CN == 1)
                        {
                            dgvHoaHongDoanhThu.ItemsSource = lstDoanhThuCaNhanCPT;
                        }
                        else if (CN == 2)
                        {
                            dgvTongHoaHongDoanhThu.ItemsSource = lstTongDoanhThuCaNhanCPT;
                        }
                    } 
                }
                else if (lstLoiNhuanCaNhan.Count != 0)
                {
                    lstLoiNhuanCaNhanCPT = new List<List_Rose_LoiNhuan>();
                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoiNhuanCaNhan.Count; i++)
                    {
                        lstLoiNhuanCaNhanCPT.Add(lstLoiNhuanCaNhan[i]);
                    }
                    dgvHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                    dgvTongHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                }
                else if (lstHoaHongViTri.Count != 0)
                {
                    lstHoaHongViTriCPT = new List<List_Rose_ViTri>();
                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstHoaHongViTri.Count; i++)
                    {
                        lstHoaHongViTriCPT.Add(lstHoaHongViTri[i]);
                    }
                    dgvHoaHongLePhiViTri.ItemsSource = lstHoaHongViTriCPT;
                    dgvTongHoaHongViTri.ItemsSource = lstHoaHongViTriCPT;
                }
                else if (lstHoaHongKeHoach.Count != 0)
                {
                    lstHoaHongKeHoachCPT = new List<Datum_kh>();
                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstHoaHongKeHoach.Count; i++)
                    {
                        lstHoaHongKeHoachCPT.Add(lstHoaHongKeHoach[i]);
                    }
                    dgvHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                    dgvTongHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                }
               
            }
            catch (Exception)
            {
            }
        }
        private void borPage1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (textPage1.Text != PageNumberCurrent.ToString() && int.Parse(textPage1.Text) == PageNumberCurrent - 1)
                {
                    if (PageNumberCurrent > 2)
                    {
                        textPage1.Text = (PageNumberCurrent - 2).ToString();
                        textPage2.Text = (PageNumberCurrent - 1).ToString();
                        textPage3.Text = PageNumberCurrent.ToString();
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    }
                    else if (TongSoTrang > 2)
                    {
                        textPage1.Text = (PageNumberCurrent - 1).ToString();
                        textPage2.Text = (PageNumberCurrent).ToString();
                        textPage3.Text = (PageNumberCurrent + 1).ToString();
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPageDau.Visibility = Visibility.Collapsed;
                        borLui1.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        textPage1.Text = "1";
                        textPage2.Text = "2";
                        textPage3.Text = (PageNumberCurrent + 1).ToString();
                        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPageDau.Visibility = Visibility.Collapsed;
                        borLui1.Visibility = Visibility.Collapsed;
                        borPage3.Visibility = Visibility.Collapsed;
                    }
                    borPageCuoi.Visibility = Visibility.Visible;
                    borLen1.Visibility = Visibility.Visible;
                    PageNumberCurrent--;
                    if (lstRoseUser.Count != 0)
                    {
                        if (lstRoseUserObject.Count != 0)
                        {
                            lstRoseUserCPT = new List<RoseUser>();
                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstRoseUserObject.Count; i++)
                            {
                                lstRoseUserCPT.Add(lstRoseUserObject[i]);
                            }
                            dgvHoaHongNhanDuoc.ItemsSource = lstRoseUserCPT;
                        }
                        else
                        {
                            lstRoseUserCPT = new List<RoseUser>();
                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstRoseUser.Count; i++)
                            {
                                lstRoseUserCPT.Add(lstRoseUser[i]);
                            }
                            dgvHoaHongCaNhanTien.ItemsSource = lstRoseUserCPT;
                            dgvTongHoaHongTien.ItemsSource = lstRoseUserCPT;
                        }
                    }
                    else if (lstDoanhThuCaNhan.Count != 0 || lstTongDoanhThuCaNhan.Count != 0)
                    {
                        lstDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                        lstTongDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                        lstSearchDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                        if (lstDoanhThuCaNhan.Count > 10 || lstTongDoanhThuCaNhan.Count != 0)
                        {
                            if (lstSearchDoanhThuCaNhan.Count != 0)
                            {
                                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstSearchDoanhThuCaNhan.Count; i++)
                                {
                                    lstSearchDoanhThuCaNhanCPT.Add(lstSearchDoanhThuCaNhan[i]);
                                }
                            }
                            else
                            {
                                if (CN == 1)
                                {
                                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstDoanhThuCaNhan.Count; i++)
                                    {
                                        lstDoanhThuCaNhanCPT.Add(lstDoanhThuCaNhan[i]);
                                    }
                                }
                                else if (CN == 2)
                                {
                                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstTongDoanhThuCaNhan.Count; i++)
                                    {
                                        lstTongDoanhThuCaNhanCPT.Add(lstTongDoanhThuCaNhan[i]);
                                    }
                                }
                            }
                            if (lstSearchDoanhThuCaNhan.Count != 0)
                            {
                                if (CN == 1)
                                {
                                    dgvHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                                }
                                else
                                {
                                    dgvTongHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                                }
                            }
                            else
                            {
                                if (CN == 1)
                                {
                                    dgvHoaHongDoanhThu.ItemsSource = lstDoanhThuCaNhanCPT;
                                }
                                else if (CN == 2)
                                {
                                    dgvTongHoaHongDoanhThu.ItemsSource = lstTongDoanhThuCaNhanCPT;
                                }
                            }
                        }
                    }
                    else if (lstLoiNhuanCaNhan.Count != 0)
                    {
                        lstLoiNhuanCaNhanCPT = new List<List_Rose_LoiNhuan>();
                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoiNhuanCaNhan.Count; i++)
                        {
                            lstLoiNhuanCaNhanCPT.Add(lstLoiNhuanCaNhan[i]);
                        }
                        dgvHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                        dgvTongHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                    }
                    else if (lstHoaHongViTri.Count != 0)
                    {
                        lstHoaHongViTriCPT = new List<List_Rose_ViTri>();
                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstHoaHongViTri.Count; i++)
                        {
                            lstHoaHongViTriCPT.Add(lstHoaHongViTri[i]);
                        }
                        dgvHoaHongLePhiViTri.ItemsSource = lstHoaHongViTriCPT;
                        dgvTongHoaHongViTri.ItemsSource = lstHoaHongViTriCPT;
                    }
                    else if (lstHoaHongKeHoach.Count != 0)
                    {
                        lstHoaHongKeHoachCPT = new List<Datum_kh>();
                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstHoaHongKeHoach.Count; i++)
                        {
                            lstHoaHongKeHoachCPT.Add(lstHoaHongKeHoach[i]);
                        }
                        dgvHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                        dgvTongHoaHongKeHoach.ItemsSource = lstHoaHongKeHoach;
                    }
                }
                else
                {
                    if (textPage1.Text != PageNumberCurrent.ToString())
                    {
                        if (PageNumberCurrent == 3)
                        {
                            borPageDau.Visibility = Visibility.Collapsed;
                            borLui1.Visibility = Visibility.Collapsed;
                            borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                            textPage1.Text = "1";
                            textPage2.Text = "2";
                            textPage3.Text = "3";
                            borLen1.Visibility = Visibility.Visible;
                            if (TongSoTrang > 2)
                            {
                                borPage2.Visibility = Visibility.Visible;
                                borPage3.Visibility = Visibility.Visible;
                            }
                            else if (TongSoTrang > 1)
                            {
                                borPage2.Visibility = Visibility.Visible;
                                borPage3.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                borPage2.Visibility = Visibility.Collapsed;
                                borPage3.Visibility = Visibility.Collapsed;
                                borPageCuoi.Visibility = Visibility.Collapsed;
                                borLen1.Visibility = Visibility.Collapsed;
                            }
                            if (TongSoTrang > 1)
                            {
                                borPageCuoi.Visibility = Visibility.Visible;
                                borLen1.Visibility = Visibility.Visible;
                            }
                            if (lstRoseUser.Count != 0)
                            {
                                if (lstRoseUserObject.Count != 0)
                                {
                                    lstRoseUserCPT = new List<RoseUser>();
                                    for (int i = 0; i < 10; i++)
                                    {
                                        lstRoseUserCPT.Add(lstRoseUserObject[i]);
                                    }
                                    dgvHoaHongNhanDuoc.ItemsSource = lstRoseUserCPT;
                                }
                                else
                                {
                                    lstRoseUserCPT = new List<RoseUser>();
                                    for (int i = 0; i < 10; i++)
                                    {
                                        lstRoseUserCPT.Add(lstRoseUser[i]);
                                    }
                                    dgvHoaHongCaNhanTien.ItemsSource = lstRoseUserCPT;
                                    dgvTongHoaHongTien.ItemsSource = lstRoseUserCPT;
                                }
                            }
                            else if (lstDoanhThuCaNhan.Count != 0 || lstTongDoanhThuCaNhan.Count != 0)
                            {
                                lstDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                                lstTongDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                                lstSearchDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                                for (int i = 0; i < 10; i++)
                                {
                                    if (lstSearchDoanhThuCaNhan.Count != 0)
                                    {
                                        lstSearchDoanhThuCaNhanCPT.Add(lstSearchDoanhThuCaNhan[i]);
                                    }
                                    else if(CN == 1)
                                    {
                                        lstDoanhThuCaNhanCPT.Add(lstDoanhThuCaNhan[i]);
                                    }
                                    else if(CN == 2)
                                    {
                                        lstTongDoanhThuCaNhanCPT.Add(lstTongDoanhThuCaNhan[i]);
                                    }
                                }
                                if (lstSearchDoanhThuCaNhan.Count != 0)
                                {
                                    if (CN == 1)
                                    {
                                        dgvHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                                    }
                                    else
                                    {
                                        dgvTongHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                                    }
                                }
                                else
                                {
                                    if (CN == 1)
                                    {
                                        dgvHoaHongDoanhThu.ItemsSource = lstDoanhThuCaNhanCPT;
                                    }
                                    else if (CN == 2)
                                    {
                                        dgvTongHoaHongDoanhThu.ItemsSource = lstTongDoanhThuCaNhanCPT;
                                    }
                                }
                            }
                            else if (lstLoiNhuanCaNhan.Count != 0)
                            {
                                lstLoiNhuanCaNhanCPT = new List<List_Rose_LoiNhuan>();
                                for (int i = 0; i < 10; i++)
                                {
                                    lstLoiNhuanCaNhanCPT.Add(lstLoiNhuanCaNhan[i]);
                                }
                                dgvHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                                dgvTongHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                            }
                            else if (lstHoaHongViTri.Count != 0)
                            {
                                lstHoaHongViTriCPT = new List<List_Rose_ViTri>();
                                for (int i = 0; i < 10; i++)
                                {
                                    lstHoaHongViTriCPT.Add(lstHoaHongViTri[i]);
                                }
                                dgvHoaHongLePhiViTri.ItemsSource = lstHoaHongViTriCPT;
                                dgvTongHoaHongViTri.ItemsSource = lstHoaHongViTriCPT;
                            }
                            else if (lstHoaHongKeHoach.Count != 0)
                            {
                                lstHoaHongKeHoachCPT = new List<Datum_kh>();
                                for (int i = 0; i < 10; i++)
                                {
                                    lstHoaHongKeHoachCPT.Add(lstHoaHongKeHoach[i]);
                                }
                                dgvHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                                dgvTongHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                            }
                            PageNumberCurrent = 1;
                        }
                        else
                        {
                            textPage1.Text = (PageNumberCurrent - 3).ToString();
                            textPage2.Text = (PageNumberCurrent - 2).ToString();
                            textPage3.Text = (PageNumberCurrent - 1).ToString();
                            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPageCuoi.Visibility = Visibility.Visible;
                            borLen1.Visibility = Visibility.Visible;
                            borPageDau.Visibility = Visibility.Visible;
                            borLui1.Visibility = Visibility.Visible;
                            PageNumberCurrent -= 2;
                            if (lstRoseUser.Count != 0)
                            {
                                if (lstRoseUserObject.Count != 0)
                                {
                                    lstRoseUserCPT = new List<RoseUser>();
                                    if (lstRoseUserObject.Count > 10)
                                    {
                                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstRoseUserObject.Count; i++)
                                        {
                                            lstRoseUserCPT.Add(lstRoseUserObject[i]);
                                        }
                                        dgvHoaHongNhanDuoc.ItemsSource = lstRoseUserCPT;
                                    }
                                }
                                else
                                {
                                    lstRoseUserCPT = new List<RoseUser>();
                                    if (lstRoseUser.Count > 10)
                                    {
                                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstRoseUser.Count; i++)
                                        {
                                            lstRoseUserCPT.Add(lstRoseUser[i]);
                                        }
                                        dgvHoaHongCaNhanTien.ItemsSource = lstRoseUserCPT;
                                        dgvTongHoaHongTien.ItemsSource = lstRoseUserCPT;
                                    }
                                }
                            }
                            else if (lstDoanhThuCaNhan.Count != 0 || lstTongDoanhThuCaNhan.Count != 0)
                            {
                                lstDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                                lstTongDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                                lstSearchDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                                if (lstDoanhThuCaNhan.Count > 10)
                                {
                                    if (lstSearchDoanhThuCaNhan.Count != 0)
                                    {
                                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstSearchDoanhThuCaNhan.Count; i++)
                                        {
                                            lstSearchDoanhThuCaNhanCPT.Add(lstSearchDoanhThuCaNhan[i]);
                                        }
                                    }
                                    else
                                    {
                                        if (CN == 1)
                                        {
                                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstDoanhThuCaNhan.Count; i++)
                                            {
                                                lstDoanhThuCaNhanCPT.Add(lstDoanhThuCaNhan[i]);
                                            }
                                        }
                                        else if (CN == 2)
                                        {
                                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstTongDoanhThuCaNhan.Count; i++)
                                            {
                                                lstTongDoanhThuCaNhanCPT.Add(lstTongDoanhThuCaNhan[i]);
                                            }
                                        }
                                    }
                                    if (lstSearchDoanhThuCaNhan.Count != 0)
                                    {
                                        if (CN == 1)
                                        {
                                            dgvHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                                        }
                                        else
                                        {
                                            dgvTongHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                                        }
                                    }
                                    else
                                    {
                                        if (CN == 1)
                                        {
                                            dgvHoaHongDoanhThu.ItemsSource = lstDoanhThuCaNhanCPT;
                                        }
                                        else if (CN == 2)
                                        {
                                            dgvTongHoaHongDoanhThu.ItemsSource = lstTongDoanhThuCaNhanCPT;
                                        }
                                    }
                                }
                            }
                            else if (lstLoiNhuanCaNhan.Count != 0)
                            {
                                lstLoiNhuanCaNhanCPT = new List<List_Rose_LoiNhuan>();
                                if (lstLoiNhuanCaNhan.Count > 10)
                                {
                                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoiNhuanCaNhan.Count; i++)
                                    {
                                        lstLoiNhuanCaNhanCPT.Add(lstLoiNhuanCaNhan[i]);
                                    }
                                    dgvHoaHongLoiNhuan.ItemsSource = lstDoanhThuCaNhanCPT;
                                    dgvTongHoaHongLoiNhuan.ItemsSource = lstDoanhThuCaNhanCPT;
                                }
                            }
                            else if (lstHoaHongViTri.Count != 0)
                            {
                                lstHoaHongViTriCPT = new List<List_Rose_ViTri>();
                                if (lstHoaHongViTri.Count > 10)
                                {
                                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstHoaHongViTri.Count; i++)
                                    {
                                        lstHoaHongViTriCPT.Add(lstHoaHongViTri[i]);
                                    }
                                    dgvHoaHongLePhiViTri.ItemsSource = lstHoaHongViTriCPT;
                                    dgvTongHoaHongViTri.ItemsSource = lstHoaHongViTriCPT;
                                }
                            }
                            else if (lstHoaHongKeHoach.Count != 0)
                            {
                                lstHoaHongKeHoachCPT = new List<Datum_kh>();
                                if (lstHoaHongKeHoach.Count > 10)
                                {
                                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstHoaHongKeHoach.Count; i++)
                                    {
                                        lstHoaHongKeHoachCPT.Add(lstHoaHongKeHoach[i]);
                                    }
                                    dgvHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                                    dgvTongHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void borPage2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BrushConverter brus = new BrushConverter();
                if (PageNumberCurrent.ToString() != textPage2.Text)
                {
                    PageNumberCurrent = int.Parse(textPage2.Text);
                    if (TongSoTrang >= 3)
                    {
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPageDau.Visibility = Visibility.Visible;
                        borLen1.Visibility = Visibility.Visible;
                        borPageCuoi.Visibility = Visibility.Visible;
                        borLui1.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Visibility = Visibility.Collapsed;
                        borPageCuoi.Visibility = Visibility.Collapsed;
                        borLen1.Visibility = Visibility.Collapsed;
                        borPageDau.Visibility = Visibility.Visible;
                        borLui1.Visibility = Visibility.Visible;
                    }
                }
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                if (lstRoseUser.Count != 0)
                {
                    if (lstRoseUserObject.Count != 0)
                    {
                        lstRoseUserCPT = new List<RoseUser>();
                        if (lstRoseUserObject.Count > 10)
                        {
                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstRoseUserObject.Count; i++)
                            {
                                lstRoseUserCPT.Add(lstRoseUserObject[i]);
                            }
                            dgvHoaHongNhanDuoc.ItemsSource = lstRoseUserCPT;
                        }
                    }
                    else
                    {
                        lstRoseUserCPT = new List<RoseUser>();
                        if (lstRoseUser.Count > 10)
                        {
                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstRoseUser.Count; i++)
                            {
                                lstRoseUserCPT.Add(lstRoseUser[i]);
                            }
                            dgvHoaHongCaNhanTien.ItemsSource = lstRoseUserCPT;
                            dgvTongHoaHongTien.ItemsSource = lstRoseUserCPT;
                        }
                    }
                }
                else if (lstDoanhThuCaNhan.Count != 0 || lstTongDoanhThuCaNhan.Count != 0)
                {
                    lstDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                    lstTongDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                    lstSearchDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                    if (lstDoanhThuCaNhan.Count > 10 || lstTongDoanhThuCaNhan.Count != 0)
                    {
                        if (lstSearchDoanhThuCaNhan.Count != 0)
                        {
                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstSearchDoanhThuCaNhan.Count; i++)
                            {
                                lstSearchDoanhThuCaNhanCPT.Add(lstSearchDoanhThuCaNhan[i]);
                            }
                        }
                        else
                        {
                            if (CN == 1)
                            {
                                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstDoanhThuCaNhan.Count; i++)
                                {
                                    lstDoanhThuCaNhanCPT.Add(lstDoanhThuCaNhan[i]);
                                }
                            }
                            else if (CN == 2)
                            {
                                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstTongDoanhThuCaNhan.Count; i++)
                                {
                                    lstTongDoanhThuCaNhanCPT.Add(lstTongDoanhThuCaNhan[i]);
                                }
                            }
                        }
                        if (lstSearchDoanhThuCaNhan.Count != 0)
                        {
                            if (CN == 1)
                            {
                                dgvHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                            }
                            else
                            {
                                dgvTongHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                            }
                        }
                        else
                        {
                            if (CN == 1)
                            {
                                dgvHoaHongDoanhThu.ItemsSource = lstDoanhThuCaNhanCPT;
                            }
                            else if (true)
                            {
                                dgvTongHoaHongDoanhThu.ItemsSource = lstTongDoanhThuCaNhanCPT;
                            }
                        } 
                    }
                }
                else if (lstLoiNhuanCaNhan.Count != 0)
                {
                    lstLoiNhuanCaNhanCPT = new List<List_Rose_LoiNhuan>();
                    if (lstLoiNhuanCaNhan.Count > 10)
                    {
                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoiNhuanCaNhan.Count; i++)
                        {
                            lstLoiNhuanCaNhanCPT.Add(lstLoiNhuanCaNhan[i]);
                        }
                        dgvHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                        dgvTongHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                    }
                }
                else if (lstHoaHongViTri.Count != 0)
                {
                    lstHoaHongViTriCPT = new List<List_Rose_ViTri>();
                    if (lstHoaHongViTri.Count > 10)
                    {
                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstHoaHongViTri.Count; i++)
                        {
                            lstHoaHongViTriCPT.Add(lstHoaHongViTri[i]);
                        }
                        dgvHoaHongLePhiViTri.ItemsSource = lstHoaHongViTriCPT;
                        dgvTongHoaHongViTri.ItemsSource = lstHoaHongViTriCPT;
                    }
                }
                else if (lstHoaHongKeHoach.Count != 0)
                {
                    lstHoaHongKeHoachCPT = new List<Datum_kh>();
                    if (lstHoaHongKeHoach.Count > 10)
                    {
                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstHoaHongKeHoach.Count; i++)
                        {
                            lstHoaHongKeHoachCPT.Add(lstHoaHongKeHoach[i]);
                        }
                        dgvHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                        dgvTongHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                    }
                }
            }
            catch
            {

            }
        }
        private void borPage3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (PageNumberCurrent != TongSoTrang)
                {
                    if (PageNumberCurrent.ToString() != textPage3.Text && PageNumberCurrent > int.Parse(textPage3.Text) - 2)
                    {
                        if (PageNumberCurrent < TongSoTrang - 1)
                        {
                            textPage1.Text = PageNumberCurrent.ToString();
                            textPage2.Text = (PageNumberCurrent + 1).ToString();
                            textPage3.Text = (PageNumberCurrent + 2).ToString();
                            BrushConverter brus = new BrushConverter();
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        }
                        else if (TongSoTrang >= 3)
                        {
                            textPage1.Text = (PageNumberCurrent - 1).ToString();
                            textPage2.Text = (PageNumberCurrent).ToString();
                            textPage3.Text = (PageNumberCurrent + 1).ToString();
                            BrushConverter brus = new BrushConverter();
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPageCuoi.Visibility = Visibility.Collapsed;
                            borLen1.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            textPage1.Text = "1";
                            textPage2.Text = "2";
                            textPage3.Text = (PageNumberCurrent + 1).ToString();
                            BrushConverter brus = new BrushConverter();
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPageCuoi.Visibility = Visibility.Collapsed;
                            borLen1.Visibility = Visibility.Collapsed;
                            borPage3.Visibility = Visibility.Collapsed;
                        }
                        borPageDau.Visibility = Visibility.Visible;
                        borLui1.Visibility = Visibility.Visible;
                        PageNumberCurrent++;
                        if (lstRoseUser.Count != 0)
                        {
                            if (lstRoseUserObject.Count != 0)
                            {
                                lstRoseUserCPT = new List<RoseUser>();
                                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstRoseUserObject.Count; i++)
                                {
                                    lstRoseUserCPT.Add(lstRoseUserObject[i]);
                                }
                                dgvHoaHongNhanDuoc.ItemsSource = lstRoseUserCPT;
                            }
                            else
                            {
                                lstRoseUserCPT = new List<RoseUser>();
                                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstRoseUser.Count; i++)
                                {
                                    lstRoseUserCPT.Add(lstRoseUser[i]);
                                }
                                dgvHoaHongCaNhanTien.ItemsSource = lstRoseUserCPT;
                                dgvTongHoaHongTien.ItemsSource = lstRoseUserCPT;
                            }
                        }
                        else if (lstDoanhThuCaNhan.Count != 0 || lstTongDoanhThuCaNhan.Count != 0)
                        {
                            lstDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                            lstTongDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                            lstSearchDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                            if (lstSearchDoanhThuCaNhan.Count != 0)
                            {
                                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstSearchDoanhThuCaNhan.Count; i++)
                                {
                                    lstSearchDoanhThuCaNhanCPT.Add(lstSearchDoanhThuCaNhan[i]);
                                }
                            }
                            else
                            {
                                if (CN == 1)
                                {
                                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstDoanhThuCaNhan.Count; i++)
                                    {
                                        lstDoanhThuCaNhanCPT.Add(lstDoanhThuCaNhan[i]);
                                    }
                                }
                                else if (CN == 2)
                                {
                                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstTongDoanhThuCaNhan.Count; i++)
                                    {
                                        lstTongDoanhThuCaNhanCPT.Add(lstTongDoanhThuCaNhan[i]);
                                    }
                                }  
                            }
                            if (lstSearchDoanhThuCaNhan.Count != 0)
                            {
                                if (CN == 1)
                                {
                                    dgvHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                                }
                                else
                                {
                                    dgvTongHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                                }
                            }
                            else
                            {
                                if (CN == 1)
                                {
                                    dgvHoaHongDoanhThu.ItemsSource = lstDoanhThuCaNhanCPT;
                                }
                                else if (true)
                                {
                                    dgvTongHoaHongDoanhThu.ItemsSource = lstTongDoanhThuCaNhanCPT;
                                }
                            }
                        }
                        else if (lstLoiNhuanCaNhan.Count != 0)
                        {
                            lstLoiNhuanCaNhanCPT = new List<List_Rose_LoiNhuan>();
                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoiNhuanCaNhan.Count; i++)
                            {
                                lstLoiNhuanCaNhanCPT.Add(lstLoiNhuanCaNhan[i]);
                            }
                            dgvHoaHongLoiNhuan.ItemsSource = lstDoanhThuCaNhanCPT;
                            dgvTongHoaHongLoiNhuan.ItemsSource = lstDoanhThuCaNhanCPT;
                        }
                        else if (lstHoaHongViTri.Count != 0)
                        {
                            lstHoaHongViTriCPT = new List<List_Rose_ViTri>();
                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstHoaHongViTri.Count; i++)
                            {
                                lstHoaHongViTriCPT.Add(lstHoaHongViTri[i]);
                            }
                            dgvHoaHongLePhiViTri.ItemsSource = lstHoaHongViTriCPT;
                            dgvTongHoaHongViTri.ItemsSource = lstHoaHongViTriCPT;
                        }
                        else if (lstHoaHongKeHoach.Count != 0)
                        {
                            lstHoaHongKeHoachCPT = new List<Datum_kh>();
                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstHoaHongKeHoach.Count; i++)
                            {
                                lstHoaHongKeHoachCPT.Add(lstHoaHongKeHoach[i]);
                            }
                            dgvHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                            dgvTongHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                        }
                    }
                    else
                    {
                        if (TongSoTrang == 3)
                        {
                            textPage3.Text = TongSoTrang.ToString();
                            textPage2.Text = (TongSoTrang - 1).ToString();
                            textPage1.Text = (TongSoTrang - 2).ToString();
                            borPageDau.Visibility = Visibility.Visible;
                            borLui1.Visibility = Visibility.Visible;
                            BrushConverter brus = new BrushConverter();
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPageCuoi.Visibility = Visibility.Collapsed;
                            borLen1.Visibility = Visibility.Collapsed;
                            PageNumberCurrent = TongSoTrang;
                            if (lstRoseUser.Count != 0)
                            {
                                if (lstRoseUserObject.Count != 0)
                                {
                                    lstRoseUserCPT = new List<RoseUser>();
                                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                                    {
                                        lstRoseUserCPT.Add(lstRoseUserObject[i]);
                                    }
                                    dgvHoaHongNhanDuoc.ItemsSource = lstRoseUserCPT;
                                }
                                else
                                {
                                    lstRoseUserCPT = new List<RoseUser>();
                                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                                    {
                                        lstRoseUserCPT.Add(lstRoseUser[i]);
                                    }
                                    dgvHoaHongCaNhanTien.ItemsSource = lstRoseUserCPT;
                                    dgvTongHoaHongTien.ItemsSource = lstRoseUserCPT;
                                }
                            }
                            else if (lstDoanhThuCaNhan.Count != 0 || lstTongDoanhThuCaNhan.Count != 0)
                            {
                                lstDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                                lstTongDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                                lstSearchDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                                {
                                    if (lstSearchDoanhThuCaNhan.Count != 0)
                                    {
                                        lstSearchDoanhThuCaNhanCPT.Add(lstSearchDoanhThuCaNhan[i]);
                                    }
                                    else if (CN == 1)
                                    {
                                        lstDoanhThuCaNhanCPT.Add(lstDoanhThuCaNhan[i]);
                                    }
                                    else if (true)
                                    {
                                        lstTongDoanhThuCaNhanCPT.Add(lstTongDoanhThuCaNhan[i]);
                                    }
                                }
                                if (lstSearchDoanhThuCaNhan.Count != 0)
                                {
                                    if (CN == 1)
                                    {
                                        dgvHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                                    }
                                    else
                                    {
                                        dgvTongHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                                    }
                                }
                                else
                                {
                                    if (CN == 1)
                                    {
                                        dgvHoaHongDoanhThu.ItemsSource = lstDoanhThuCaNhanCPT;
                                    }
                                    else if (true)
                                    {
                                        dgvTongHoaHongDoanhThu.ItemsSource = lstTongDoanhThuCaNhanCPT;
                                    }
                                }
                            }
                            else if (lstLoiNhuanCaNhan.Count != 0)
                            {
                                lstLoiNhuanCaNhanCPT = new List<List_Rose_LoiNhuan>();
                                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                                {
                                    lstLoiNhuanCaNhanCPT.Add(lstLoiNhuanCaNhan[i]);
                                }
                                dgvHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                                dgvTongHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                            }
                            else if (lstHoaHongViTri.Count != 0)
                            {
                                lstHoaHongViTriCPT = new List<List_Rose_ViTri>();
                                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                                {
                                    lstHoaHongViTriCPT.Add(lstHoaHongViTri[i]);
                                }
                                dgvHoaHongLePhiViTri.ItemsSource = lstHoaHongViTriCPT;
                                dgvTongHoaHongViTri.ItemsSource = lstHoaHongViTriCPT;
                            }
                            else if (lstHoaHongKeHoach.Count != 0)
                            {
                                lstHoaHongKeHoachCPT = new List<Datum_kh>();
                                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                                {
                                    lstHoaHongKeHoachCPT.Add(lstHoaHongKeHoach[i]);
                                }
                                dgvHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                                dgvTongHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                            }
                        }
                        else
                        {
                            BrushConverter brus = new BrushConverter();
                            textPage1.Text = (PageNumberCurrent + 1).ToString();
                            textPage2.Text = (PageNumberCurrent + 2).ToString();
                            textPage3.Text = (PageNumberCurrent + 3).ToString();
                            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPageCuoi.Visibility = Visibility.Visible;
                            borLen1.Visibility = Visibility.Visible;
                            borPageDau.Visibility = Visibility.Visible;
                            borLui1.Visibility = Visibility.Visible;
                            PageNumberCurrent += 2;
                            if (lstRoseUser.Count != 0)
                            {
                                if (lstRoseUserObject.Count != 0)
                                {
                                    lstRoseUserCPT = new List<RoseUser>();
                                    if (lstRoseUserObject.Count > 10)
                                    {
                                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstRoseUserObject.Count; i++)
                                        {
                                            lstRoseUserCPT.Add(lstRoseUserObject[i]);
                                        }
                                        dgvHoaHongNhanDuoc.ItemsSource = lstRoseUserCPT;
                                    }
                                }
                                lstRoseUserCPT = new List<RoseUser>();
                                if (lstRoseUser.Count > 10)
                                {
                                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstRoseUser.Count; i++)
                                    {
                                        lstRoseUserCPT.Add(lstRoseUser[i]);
                                    }
                                    dgvHoaHongCaNhanTien.ItemsSource = lstRoseUserCPT;
                                    dgvTongHoaHongTien.ItemsSource = lstRoseUserCPT;
                                }
                            }
                            else if (lstDoanhThuCaNhan.Count != 0 || lstTongDoanhThuCaNhan.Count != 0)
                            {
                                lstDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                                lstTongDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                                lstSearchDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                                if (lstDoanhThuCaNhan.Count > 10)
                                {
                                    if (lstSearchDoanhThuCaNhan.Count != 0)
                                    {
                                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstSearchDoanhThuCaNhan.Count; i++)
                                        {
                                            lstSearchDoanhThuCaNhanCPT.Add(lstSearchDoanhThuCaNhan[i]);
                                        }
                                    }
                                    else
                                    {
                                        if (CN == 1)
                                        {
                                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstDoanhThuCaNhan.Count; i++)
                                            {
                                                lstDoanhThuCaNhanCPT.Add(lstDoanhThuCaNhan[i]);
                                            }
                                        }
                                        else if (true)
                                        {
                                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstTongDoanhThuCaNhan.Count; i++)
                                            {
                                                lstTongDoanhThuCaNhanCPT.Add(lstTongDoanhThuCaNhan[i]);
                                            }
                                        }
                                    }
                                    if (lstSearchDoanhThuCaNhan.Count != 0)
                                    {
                                        if (CN == 1)
                                        {
                                            dgvHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                                        }
                                        else
                                        {
                                            dgvTongHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                                        }
                                    }
                                    else
                                    {
                                        if (CN == 1)
                                        {
                                            dgvHoaHongDoanhThu.ItemsSource = lstDoanhThuCaNhanCPT;
                                        }
                                        else if (true)
                                        {
                                            dgvTongHoaHongDoanhThu.ItemsSource = lstTongDoanhThuCaNhanCPT;
                                        }
                                    } 
                                }
                            }
                            else if (lstLoiNhuanCaNhan.Count != 0)
                            {
                                lstLoiNhuanCaNhanCPT = new List<List_Rose_LoiNhuan>();
                                if (lstLoiNhuanCaNhan.Count > 10)
                                {
                                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoiNhuanCaNhan.Count; i++)
                                    {
                                        lstLoiNhuanCaNhanCPT.Add(lstLoiNhuanCaNhan[i]);
                                    }
                                    dgvHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                                    dgvTongHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                                }
                            }
                            else if (lstHoaHongViTri.Count != 0)
                            {
                                lstHoaHongViTriCPT = new List<List_Rose_ViTri>();
                                if (lstHoaHongViTri.Count > 10)
                                {
                                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstHoaHongViTri.Count; i++)
                                    {
                                        lstHoaHongViTriCPT.Add(lstHoaHongViTri[i]);
                                    }
                                    dgvHoaHongLePhiViTri.ItemsSource = lstHoaHongViTriCPT;
                                    dgvTongHoaHongViTri.ItemsSource = lstHoaHongViTriCPT;
                                }
                            }
                            else if (lstHoaHongKeHoach.Count != 0)
                            {
                                lstHoaHongKeHoachCPT = new List<Datum_kh>();
                                if (lstHoaHongKeHoach.Count > 10)
                                {
                                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstHoaHongKeHoach.Count; i++)
                                    {
                                        lstHoaHongKeHoachCPT.Add(lstHoaHongKeHoach[i]);
                                    }
                                    dgvHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                                    dgvTongHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void borLen1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (PageNumberCurrent < TongSoTrang - 1)
                {
                    textPage1.Text = PageNumberCurrent.ToString();
                    textPage2.Text = (PageNumberCurrent + 1).ToString();
                    textPage3.Text = (PageNumberCurrent + 2).ToString();
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                }
                else if (TongSoTrang >= 3)
                {
                    textPage1.Text = (PageNumberCurrent - 1).ToString();
                    textPage2.Text = (PageNumberCurrent).ToString();
                    textPage3.Text = (PageNumberCurrent + 1).ToString();
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Collapsed;
                }
                else
                {
                    textPage1.Text = "1";
                    textPage2.Text = "2";
                    textPage3.Text = (PageNumberCurrent + 1).ToString();
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Collapsed;
                    borPage3.Visibility = Visibility.Collapsed;
                }
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                PageNumberCurrent++;
                if (lstRoseUser.Count != 0)
                {
                    if (lstRoseUserObject.Count != 0)
                    {
                        lstRoseUserCPT = new List<RoseUser>();
                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstRoseUserObject.Count; i++)
                        {
                            lstRoseUserCPT.Add(lstRoseUserObject[i]);
                        }
                        dgvHoaHongNhanDuoc.ItemsSource = lstRoseUserCPT;
                    }
                    else
                    {
                        lstRoseUserCPT = new List<RoseUser>();
                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstRoseUser.Count; i++)
                        {
                            lstRoseUserCPT.Add(lstRoseUser[i]);
                        }
                        dgvHoaHongCaNhanTien.ItemsSource = lstRoseUserCPT;
                        dgvTongHoaHongTien.ItemsSource = lstRoseUserCPT;
                    } 
                }
                else if (lstDoanhThuCaNhan.Count != 0 || lstTongDoanhThuCaNhan.Count != 0)
                {
                    lstDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                    lstTongDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                    lstSearchDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                    if (lstSearchDoanhThuCaNhan.Count != 0)
                    {
                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstSearchDoanhThuCaNhan.Count; i++)
                        {
                            lstSearchDoanhThuCaNhanCPT.Add(lstSearchDoanhThuCaNhan[i]);
                        }
                    }
                    else
                    {
                        if (CN == 1)
                        {
                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstDoanhThuCaNhan.Count; i++)
                            {
                                lstDoanhThuCaNhanCPT.Add(lstDoanhThuCaNhan[i]);
                            }
                        }
                        else if (CN == 2)
                        {
                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstTongDoanhThuCaNhan.Count; i++)
                            {
                                lstTongDoanhThuCaNhanCPT.Add(lstTongDoanhThuCaNhan[i]);
                            }
                        }
                    }
                    if (lstSearchDoanhThuCaNhan.Count != 0)
                    {
                        if (CN == 1)
                        {
                            dgvHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                        }
                        else
                        {
                            dgvTongHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                        }
                    }
                    else
                    {
                         if (CN == 1)
                        {
                            dgvHoaHongDoanhThu.ItemsSource = lstDoanhThuCaNhanCPT;
                        }
                        else if (true)
                        {
                            dgvTongHoaHongDoanhThu.ItemsSource = lstTongDoanhThuCaNhanCPT;
                        }
                    }
                }
                else if (lstLoiNhuanCaNhan.Count != 0)
                {
                    lstLoiNhuanCaNhanCPT = new List<List_Rose_LoiNhuan>();
                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoiNhuanCaNhan.Count; i++)
                    {
                        lstLoiNhuanCaNhanCPT.Add(lstLoiNhuanCaNhan[i]);
                    }
                    dgvHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                    dgvTongHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                }
                else if (lstHoaHongViTri.Count != 0)
                {
                    lstHoaHongViTriCPT = new List<List_Rose_ViTri>();
                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstHoaHongViTri.Count; i++)
                    {
                        lstHoaHongViTriCPT.Add(lstHoaHongViTri[i]);
                    }
                    dgvHoaHongLePhiViTri.ItemsSource = lstHoaHongViTriCPT;
                    dgvTongHoaHongViTri.ItemsSource = lstHoaHongViTriCPT;
                }
                else if (lstHoaHongKeHoach.Count != 0)
                {
                    lstHoaHongKeHoachCPT = new List<Datum_kh>();
                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstHoaHongKeHoach.Count; i++)
                    {
                        lstHoaHongKeHoachCPT.Add(lstHoaHongKeHoach[i]);
                    }
                    dgvHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                    dgvTongHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                }
            }
            catch (Exception)
            {
            }
        }
        private void borPageCuoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (TongSoTrang >= 3)
                {
                    textPage3.Text = TongSoTrang.ToString();
                    textPage2.Text = (TongSoTrang - 1).ToString();
                    textPage1.Text = (TongSoTrang - 2).ToString();
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                }
                else if (TongSoTrang == 2)
                {
                    textPage3.Text = TongSoTrang.ToString();
                    textPage2.Text = "2";
                    textPage1.Text = "1";
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Visibility = Visibility.Collapsed;
                }
                borPageCuoi.Visibility = Visibility.Collapsed;
                borLen1.Visibility = Visibility.Collapsed;
                PageNumberCurrent = TongSoTrang;
                if (lstRoseUser.Count != 0)
                {
                    if (lstRoseUserObject.Count != 0)
                    {
                        lstRoseUserCPT = new List<RoseUser>();
                        for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                        {
                            lstRoseUserCPT.Add(lstRoseUserObject[i]);
                        }
                        dgvHoaHongNhanDuoc.ItemsSource = lstRoseUserCPT;
                    }
                    else
                    {
                        lstRoseUserCPT = new List<RoseUser>();
                        for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                        {
                            lstRoseUserCPT.Add(lstRoseUser[i]);
                        }
                        dgvHoaHongCaNhanTien.ItemsSource = lstRoseUserCPT;
                        dgvTongHoaHongTien.ItemsSource = lstRoseUserCPT;
                    }
                }
                else if (lstDoanhThuCaNhan.Count != 0 || lstTongDoanhThuCaNhan.Count != 0)
                {
                    lstDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                    lstTongDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                    lstSearchDoanhThuCaNhanCPT = new List<List_Rose_DoanhThu>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        if (lstSearchDoanhThuCaNhan.Count != 0)
                        {
                            lstSearchDoanhThuCaNhanCPT.Add(lstSearchDoanhThuCaNhan[i]);
                        }
                        else if (CN == 1)
                        {
                            lstDoanhThuCaNhanCPT.Add(lstDoanhThuCaNhan[i]);
                        }
                        else if (CN == 2)
                        {
                            lstTongDoanhThuCaNhanCPT.Add(lstTongDoanhThuCaNhan[i]);
                        }
                    }
                    if (lstSearchDoanhThuCaNhan.Count != 0)
                    {
                        if (CN == 1)
                        {
                            dgvHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                        }
                        else
                        {
                            dgvTongHoaHongDoanhThu.ItemsSource = lstSearchDoanhThuCaNhanCPT;
                        }
                    }
                    else
                    {
                        if (CN == 1)
                        {
                            dgvHoaHongDoanhThu.ItemsSource = lstDoanhThuCaNhanCPT;
                        }
                        else if (true)
                        {
                            dgvTongHoaHongDoanhThu.ItemsSource = lstTongDoanhThuCaNhanCPT;
                        }
                    }
                    
                }
                else if (lstLoiNhuanCaNhan.Count != 0)
                {
                    lstLoiNhuanCaNhanCPT = new List<List_Rose_LoiNhuan>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstLoiNhuanCaNhanCPT.Add(lstLoiNhuanCaNhan[i]);
                    }
                    dgvHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                    dgvTongHoaHongLoiNhuan.ItemsSource = lstLoiNhuanCaNhanCPT;
                }
                else if (lstHoaHongViTri.Count != 0)
                {
                    lstHoaHongViTriCPT = new List<List_Rose_ViTri>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstHoaHongViTriCPT.Add(lstHoaHongViTri[i]);
                    }
                    dgvHoaHongLePhiViTri.ItemsSource = lstHoaHongViTriCPT;
                    dgvTongHoaHongViTri.ItemsSource = lstHoaHongViTriCPT;
                }
                else if (lstHoaHongKeHoach.Count != 0)
                {
                    lstHoaHongKeHoachCPT = new List<Datum_kh>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstHoaHongKeHoachCPT.Add(lstHoaHongKeHoach[i]);
                    }
                    dgvHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                    dgvTongHoaHongKeHoach.ItemsSource = lstHoaHongKeHoachCPT;
                }
              
            }
            catch (Exception)
            {}
        }
        #endregion

        
        private void btnXoa_HoaHongDoanhThu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            List_Rose_DoanhThu lstDT = (sender as Border).DataContext as List_Rose_DoanhThu;
            if (lstDT != null)
            {
                Main.grShowPopup.Children.Add(new ucXacNhanXoa(Main, lstDT, this));
            }
        }

        private void btnXoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void btn_XoaHoaHongViTri_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            List_Rose_ViTri lstVT = (sender as Border).DataContext as List_Rose_ViTri;
            if (lstVT != null)
            {
                Main.grShowPopup.Children.Add(new ucXacNhanXoa(Main, lstVT, this));
            }
        }

        private void btnChinhSuaHoaHongKeHoach_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnXoaHoaHongKeHoach_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RoseUser roseUser = (sender as Border).DataContext as RoseUser;
            if (roseUser != null)
            {
                Main.grShowPopup.Children.Add(new ucXacNhanXoa(Main, roseUser, this));
            }
        }
        private void btnXoa_HoaHongTien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RoseUser roseUser = (sender as Border).DataContext as RoseUser;
            if (roseUser != null)
            {
                Main.grShowPopup.Children.Add(new ucXacNhanXoa(Main, roseUser, this, Back));
            }
        }
        private void btnXoa_LoiNhuan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            List_Rose_LoiNhuan ln = (sender as Border).DataContext as List_Rose_LoiNhuan;
            if (ln != null)
            {
                Main.grShowPopup.Children.Add(new ucXacNhanXoa(Main, ln, this, LoiNhuan));
            }
        }
        private void btn_ThemMoiCacLoai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (KeHoach == 25)
            {
                Main.grShowPopup.Children.Add(new ucThemHoaHong_Tien_KeHoach(Main, KeHoach, lstHoaHongKeHoach, this, lstRose5));
            }
            else if (Next2 == "RoseMoney2")
            {
                Main.grShowPopup.Children.Add(new ucThemMoi_HoaHong_DT_LN_VT(Main, Next2, lstDoanhThuCaNhan, lstRose2, this));
            }
            else if (LoiNhuan == 23)
            {
                Main.grShowPopup.Children.Add(new ucThemMoi_HoaHong_DT_LN_VT(Main, LoiNhuan, lstRose2, this));
            }
            else if(ViTri == 24)
            {
                Main.grShowPopup.Children.Add(new ucThemMoi_HoaHong_DT_LN_VT(Main, ViTri, lstRose2, this));
            }
            else
            {
                Main.grShowPopup.Children.Add(new ucThemHoaHong_Tien_KeHoach(Main, this));
            }
        }

        private void btnChinhSua_HoaHongKeHoach_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Datum_kh roseUser = (sender as Border).DataContext as Datum_kh;
            if (roseUser != null)
            {
                Main.grShowPopup.Children.Add(new ucThemHoaHong_Tien_KeHoach(Main, roseUser, lstHoaHongKeHoach, this));
            }
        }

        private void btnChinhSua_HoaHongTien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RoseUser roseUser = (sender as Border).DataContext as RoseUser;
            if (roseUser != null)
            {
                Main.grShowPopup.Children.Add(new ucThemHoaHong_Tien_KeHoach(Main, roseUser, lstRoseUser, this, Back));
            }
        }

        private void btnChinhSua_LoiNhuan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void dgvList_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }
    }
}
