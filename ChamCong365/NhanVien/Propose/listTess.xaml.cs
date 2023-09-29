using ChamCong365.NhanVien.KindOfDon;
using System;
using System.Collections.Generic;
using System.Linq;
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



namespace ChamCong365.NhanVien.Propose
{
    /// <summary>
    /// Interaction logic for listJob.xaml
    /// </summary>
    public partial class listTess : UserControl
    {
        MainChamCong Main;
        ChamCong365.NhanVien.KindOfDon.LichLamViec.NguoiXetDuyet ListNg;
        
        public class DL
        {
            public string TenDL { get; set; }
            public string Anh { get; set; }
        }
        private List<DL> lst = new List<DL>();
        public listTess(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
            LoadDL();
        }

        private void LoadDL()
        {
            lst.Add(new DL { TenDL = "Nghỉ phép", Anh = "/NhanVien/KindOfDon/Image/NghiPhep.png" });
            lst.Add(new DL { TenDL = "Đổi ca", Anh = "/NhanVien/KindOfDon/Image/ĐoiCa.png" });
            lst.Add(new DL { TenDL = "Tạm ứng tiền", Anh = "/NhanVien/KindOfDon/Image/UngTien.png" });
            lst.Add(new DL { TenDL = "Cấp phát tài sản", Anh = "/NhanVien/KindOfDon/Image/TaiSan.png" });
            lst.Add(new DL { TenDL = "Thôi việc", Anh = "/NhanVien/KindOfDon/Image/ThoiViec.png" });
            lst.Add(new DL { TenDL = "Tăng lương", Anh = "/NhanVien/KindOfDon/Image/TangLuong.png" });
            lst.Add(new DL { TenDL = "Bổ nhiệm", Anh = "/NhanVien/KindOfDon/Image/BoNhiem.png" });
            lst.Add(new DL { TenDL = "Luân chuyển công tác", Anh = "/NhanVien/KindOfDon/Image/CongTac.png" });
            lst.Add(new DL { TenDL = "Tham gia dự án", Anh = "/NhanVien/KindOfDon/Image/DuAn.png" });
            lst.Add(new DL { TenDL = "Xin tăng ca", Anh = "/NhanVien/KindOfDon/Image/TangCa.png" });
            lst.Add(new DL { TenDL = "Xin chế độ thai sản", Anh = "/NhanVien/KindOfDon/Image/ThaiSan.png" });
            lst.Add(new DL { TenDL = "Đăng kí sử dụng phòng họp", Anh = "/NhanVien/KindOfDon/Image/PhongHop.png" });
            lst.Add(new DL { TenDL = "Đăng kí sử dụng xe công", Anh = "/NhanVien/KindOfDon/Image/XeCong.png" });
            lst.Add(new DL { TenDL = "Sửa chữa cơ sở vật chất", Anh = "/NhanVien/KindOfDon/Image/VatChat.png" });
            lst.Add(new DL { TenDL = "Thanh toán", Anh = "/NhanVien/KindOfDon/Image/ThanhToan.png" });
            lst.Add(new DL { TenDL = "Khiếu nại", Anh = "/NhanVien/KindOfDon/Image/KhieuNai.png" });
            lst.Add(new DL { TenDL = "Cộng công", Anh = "/NhanVien/KindOfDon/Image/CongCong.png" });
            lst.Add(new DL { TenDL = "Cộng tiền/trừ tiền", Anh = "/NhanVien/KindOfDon/Image/Tien.png" });
            lst.Add(new DL { TenDL = "Hoa hồng doanh thu", Anh = "/NhanVien/KindOfDon/Image/HoaHong.png" });
            lst.Add(new DL { TenDL = "Lịch làm việc", Anh = "/NhanVien/KindOfDon/Image/LamViec.png" });
            lst.Add(new DL { TenDL = "Nghỉ phép ra ngoài", Anh = "/NhanVien/KindOfDon/Image/RaNgoai.png" });
            lst.Add(new DL { TenDL = "Xin đi muộn về sớm", Anh = "/NhanVien/KindOfDon/Image/VeSom.png" });
            lst.Add(new DL { TenDL = "Xin tài liệu", Anh = "/NhanVien/KindOfDon/Image/TaiLieu.png" });
            lst.Add(new DL { TenDL = "Nhập ngày nhận lương", Anh = "/NhanVien/KindOfDon/Image/NhanLuong.png" });
            lsvDeXuat.ItemsSource = lst;
        }

        private void Don1Hien_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            //listJob uc = new listJob(Main);
            //object Content = uc.Content;
            //uc.Content = null;
            //uc.Don1Hien.Visibility = Visibility.Collapsed;
            //uc.Don1An.Visibility = Visibility.Visible;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            //listJob uc = new listJob(Main);
            //Main.dopBody.Children.Clear();
            //object Content = uc.Content;
            //uc.Content = null;
            //Main.dopBody.Children.Add(Content as UIElement);
            //uc.Don1Hien.Visibility = Visibility.Collapsed;
            //uc.Don1An.Visibility = Visibility.Visible;
        }


        private void Don1Hien_MouseLeave(object sender, MouseEventArgs e)
        {
            //listJob uc = new listJob(Main);
            //Main.dopBody.Children.Clear();
            //object Content = uc.Content;
            //uc.Content = null;
            //Main.dopBody.Children.Add(Content as UIElement);
            //uc.Don1Hien.Visibility = Visibility.Visible;
            //uc.Don1An.Visibility = Visibility.Collapsed;
        }



        private void Grid_MouseEnter_1(object sender, MouseEventArgs e)
        {
            Flash11.Visibility = Visibility.Visible;
            DongKe11.Visibility = Visibility.Visible;
            TaoDeXuat11.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Flash11.Visibility = Visibility.Collapsed;
            DongKe11.Visibility = Visibility.Collapsed;
            TaoDeXuat11.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_2(object sender, MouseEventArgs e)
        {
            Flash12.Visibility = Visibility.Visible;
            DongKe12.Visibility = Visibility.Visible;
            TaoDeXuat12.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_1(object sender, MouseEventArgs e)
        {
            Flash12.Visibility = Visibility.Collapsed;
            DongKe12.Visibility = Visibility.Collapsed;
            TaoDeXuat12.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_3(object sender, MouseEventArgs e)
        {
            Flash13.Visibility = Visibility.Visible;
            DongKe13.Visibility = Visibility.Visible;
            TaoDeXuat13.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_2(object sender, MouseEventArgs e)
        {
            Flash13.Visibility = Visibility.Collapsed;
            DongKe13.Visibility = Visibility.Collapsed;
            TaoDeXuat13.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_4(object sender, MouseEventArgs e)
        {
            Flash21.Visibility = Visibility.Visible;
            DongKe21.Visibility = Visibility.Visible;
            TaoDeXuat21.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_3(object sender, MouseEventArgs e)
        {
            Flash21.Visibility = Visibility.Collapsed;
            DongKe21.Visibility = Visibility.Collapsed;
            TaoDeXuat21.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_5(object sender, MouseEventArgs e)
        {
            Flash22.Visibility = Visibility.Visible;
            DongKe22.Visibility = Visibility.Visible;
            TaoDeXuat22.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_4(object sender, MouseEventArgs e)
        {
            Flash22.Visibility = Visibility.Collapsed;
            DongKe22.Visibility = Visibility.Collapsed;
            TaoDeXuat22.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_6(object sender, MouseEventArgs e)
        {
            Flash23.Visibility = Visibility.Visible;
            DongKe23.Visibility = Visibility.Visible;
            TaoDeXuat23.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_5(object sender, MouseEventArgs e)
        {
            Flash23.Visibility = Visibility.Collapsed;
            DongKe23.Visibility = Visibility.Collapsed;
            TaoDeXuat23.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_7(object sender, MouseEventArgs e)
        {
            Flash31.Visibility = Visibility.Visible;
            DongKe31.Visibility = Visibility.Visible;
            TaoDeXuat31.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_6(object sender, MouseEventArgs e)
        {
            Flash31.Visibility = Visibility.Collapsed;
            DongKe31.Visibility = Visibility.Collapsed;
            TaoDeXuat31.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_8(object sender, MouseEventArgs e)
        {
            Flash32.Visibility = Visibility.Visible;
            DongKe32.Visibility = Visibility.Visible;
            TaoDeXuat32.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_7(object sender, MouseEventArgs e)
        {
            Flash32.Visibility = Visibility.Collapsed;
            DongKe32.Visibility = Visibility.Collapsed;
            TaoDeXuat32.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_9(object sender, MouseEventArgs e)
        {
            Flash33.Visibility = Visibility.Visible;
            DongKe33.Visibility = Visibility.Visible;
            TaoDeXuat33.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_8(object sender, MouseEventArgs e)
        {
            Flash33.Visibility = Visibility.Collapsed;
            DongKe33.Visibility = Visibility.Collapsed;
            TaoDeXuat33.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_10(object sender, MouseEventArgs e)
        {
            Flash41.Visibility = Visibility.Visible;
            DongKe41.Visibility = Visibility.Visible;
            TaoDeXuat41.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_9(object sender, MouseEventArgs e)
        {
            Flash41.Visibility = Visibility.Collapsed;
            DongKe41.Visibility = Visibility.Collapsed;
            TaoDeXuat41.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_11(object sender, MouseEventArgs e)
        {
            Flash42.Visibility = Visibility.Visible;
            DongKe42.Visibility = Visibility.Visible;
            TaoDeXuat42.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_10(object sender, MouseEventArgs e)
        {
            Flash42.Visibility = Visibility.Collapsed;
            DongKe42.Visibility = Visibility.Collapsed;
            TaoDeXuat42.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_12(object sender, MouseEventArgs e)
        {
            Flash43.Visibility = Visibility.Visible;
            DongKe43.Visibility = Visibility.Visible;
            TaoDeXuat43.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_11(object sender, MouseEventArgs e)
        {
            Flash43.Visibility = Visibility.Collapsed;
            DongKe43.Visibility = Visibility.Collapsed;
            TaoDeXuat43.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_13(object sender, MouseEventArgs e)
        {
            Flash51.Visibility = Visibility.Visible;
            DongKe51.Visibility = Visibility.Visible;
            TaoDeXuat51.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_12(object sender, MouseEventArgs e)
        {
            Flash51.Visibility = Visibility.Collapsed;
            DongKe51.Visibility = Visibility.Collapsed;
            TaoDeXuat51.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_14(object sender, MouseEventArgs e)
        {
            Flash52.Visibility = Visibility.Visible;
            DongKe52.Visibility = Visibility.Visible;
            TaoDeXuat52.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_13(object sender, MouseEventArgs e)
        {
            Flash52.Visibility = Visibility.Collapsed;
            DongKe52.Visibility = Visibility.Collapsed;
            TaoDeXuat52.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_15(object sender, MouseEventArgs e)
        {
            Flash53.Visibility = Visibility.Visible;
            DongKe53.Visibility = Visibility.Visible;
            TaoDeXuat53.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_14(object sender, MouseEventArgs e)
        {
            Flash53.Visibility = Visibility.Collapsed;
            DongKe53.Visibility = Visibility.Collapsed;
            TaoDeXuat53.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_16(object sender, MouseEventArgs e)
        {
            Flash61.Visibility = Visibility.Visible;
            DongKe61.Visibility = Visibility.Visible;
            TaoDeXuat61.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_15(object sender, MouseEventArgs e)
        {
            Flash61.Visibility = Visibility.Collapsed;
            DongKe61.Visibility = Visibility.Collapsed;
            TaoDeXuat61.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_17(object sender, MouseEventArgs e)
        {
            Flash62.Visibility = Visibility.Visible;
            DongKe62.Visibility = Visibility.Visible;
            TaoDeXuat62.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_16(object sender, MouseEventArgs e)
        {
            Flash62.Visibility = Visibility.Collapsed;
            DongKe62.Visibility = Visibility.Collapsed;
            TaoDeXuat62.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_18(object sender, MouseEventArgs e)
        {
            Flash63.Visibility = Visibility.Visible;
            DongKe63.Visibility = Visibility.Visible;
            TaoDeXuat63.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_17(object sender, MouseEventArgs e)
        {
            Flash63.Visibility = Visibility.Collapsed;
            DongKe63.Visibility = Visibility.Collapsed;
            TaoDeXuat63.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_19(object sender, MouseEventArgs e)
        {
            Flash71.Visibility = Visibility.Visible;
            DongKe71.Visibility = Visibility.Visible;
            TaoDeXuat71.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_18(object sender, MouseEventArgs e)
        {
            Flash71.Visibility = Visibility.Collapsed;
            DongKe71.Visibility = Visibility.Collapsed;
            TaoDeXuat71.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_20(object sender, MouseEventArgs e)
        {
            Flash72.Visibility = Visibility.Visible;
            DongKe72.Visibility = Visibility.Visible;
            TaoDeXuat72.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_19(object sender, MouseEventArgs e)
        {
            Flash72.Visibility = Visibility.Collapsed;
            DongKe72.Visibility = Visibility.Collapsed;
            TaoDeXuat72.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_21(object sender, MouseEventArgs e)
        {
            Flash73.Visibility = Visibility.Visible;
            DongKe73.Visibility = Visibility.Visible;
            TaoDeXuat73.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_20(object sender, MouseEventArgs e)
        {
            Flash73.Visibility = Visibility.Collapsed;
            DongKe73.Visibility = Visibility.Collapsed;
            TaoDeXuat73.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_22(object sender, MouseEventArgs e)
        {
            Flash81.Visibility = Visibility.Visible;
            DongKe81.Visibility = Visibility.Visible;
            TaoDeXuat81.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_21(object sender, MouseEventArgs e)
        {
            Flash81.Visibility = Visibility.Collapsed;
            DongKe81.Visibility = Visibility.Collapsed;
            TaoDeXuat81.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_23(object sender, MouseEventArgs e)
        {
            Flash82.Visibility = Visibility.Visible;
            DongKe82.Visibility = Visibility.Visible;
            TaoDeXuat82.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_22(object sender, MouseEventArgs e)
        {
            Flash82.Visibility = Visibility.Collapsed;
            DongKe82.Visibility = Visibility.Collapsed;
            TaoDeXuat82.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter_24(object sender, MouseEventArgs e)
        {
            Flash83.Visibility = Visibility.Visible;
            DongKe83.Visibility = Visibility.Visible;
            TaoDeXuat83.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave_23(object sender, MouseEventArgs e)
        {
            Flash83.Visibility = Visibility.Collapsed;
            DongKe83.Visibility = Visibility.Collapsed;
            TaoDeXuat83.Visibility = Visibility.Collapsed;
        }



        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (timKiemDeXuat.Visibility == Visibility.Collapsed)
            {

                timKiemDeXuat.Visibility = Visibility.Visible;

            }
            else
            {
                timKiemDeXuat.Visibility = Visibility.Collapsed;

            }
        }

        private List<DL> lstSearchDL = new List<DL>();
        private void textSearchDeXuat_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstSearchDL = new List<DL>();
            foreach (var str in lst)
            {
                if (str.TenDL.Contains(textSearchDeXuat.Text.ToString()))
                {
                    lstSearchDL.Add(str);

                }
            }

            if (textSearchDeXuat.Text == "")
            {
                lsvDeXuat.ItemsSource = lst;
            }

        }


        private void TaoDeXuat11_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            NghiPhep uc = new NghiPhep(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);

        }

        private void borDeXuat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DL d = (sender as Border).DataContext as DL;
            if (d != null)
            {
                textHienThiDL.Text = d.TenDL;

            }
        }

        private void lsvDeXuat_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollDeXuat.ScrollToVerticalOffset(scrollDeXuat.VerticalOffset - e.Delta);
        }

        private void TaoDeXuat12_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ĐonDoiCa uc = new ĐonDoiCa(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);

        }

        private void TaoDeXuat13_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DonTamUng uc = new DonTamUng(Main);
            Main.dopBody.Children.Clear();

            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = "hello";
            // ucbodyhome.txbSalarySettings.Text + " / " + txbFuncSalary01.Text;
        }

        private void TaoDeXuat21_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DonXinCapPhatTaiSan uc = new DonXinCapPhatTaiSan(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat22_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DonXinThoiViec uc = new DonXinThoiViec(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat32_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DeXuatLuanChuyenCongTac uc = new DeXuatLuanChuyenCongTac(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat33_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DeXuatThamGiaDuAn uc = new DeXuatThamGiaDuAn(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat41_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DeXuatXinTangCa uc = new DeXuatXinTangCa(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat42_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DeXuatXinCheDoThaiSan uc = new DeXuatXinCheDoThaiSan(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat43_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeXuatDangKiPhongHop uc = new DeXuatDangKiPhongHop(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat51_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeXuatDangKiSuDungXeCong uc = new DeXuatDangKiSuDungXeCong(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat52_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeXuatSuaChuaCoSoVatChat uc = new DeXuatSuaChuaCoSoVatChat(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat53_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeXuatThanhToan uc = new DeXuatThanhToan(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat61_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeXuatKhieuNai uc = new DeXuatKhieuNai(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat23_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DonXinTangLuong uc = new DonXinTangLuong(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat31_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeXuatBoNhiem uc = new DeXuatBoNhiem(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat62_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeXuatCongCong uc = new DeXuatCongCong(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat63_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DonXinTangLuong uc = new DonXinTangLuong(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat71_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeXuatHoaHongDoanhThu uc = new DeXuatHoaHongDoanhThu(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat72_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeXuatLichLamViec uc = new DeXuatLichLamViec(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat73_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DonXinPhepRaNgoai uc = new DonXinPhepRaNgoai(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat81_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeXuatXinDiMuonVeSom uc = new DeXuatXinDiMuonVeSom(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat82_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeXuatXinTaiTaiLieu uc = new DeXuatXinTaiTaiLieu(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void TaoDeXuat83_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeXuatNhapNgayNhanLuong uc = new DeXuatNhapNgayNhanLuong(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void borDeXuat_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DL d = (sender as Border).DataContext as DL;
            if (d != null)
            {
                textHienThiDL.Text = d.TenDL;

            }
        }

        private void textSearchDeXuat_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            lstSearchDL = new List<DL>();
            foreach (var str in lst)
            {
                if (str.TenDL.Contains(textSearchDeXuat.Text.ToString()))
                {
                    lstSearchDL.Add(str);

                }
            }
            lsvDeXuat.ItemsSource = lstSearchDL;

            if (textSearchDeXuat.Text == "")
            {
                lsvDeXuat.ItemsSource = lst;
            }
        }
    }
}
