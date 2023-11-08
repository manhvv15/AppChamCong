using ChamCong365.SalarySettings.DiMuonVeSom;
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

namespace ChamCong365.SalarySettings
{
    /// <summary>
    /// Interaction logic for frmCaiDatThietLapPhatDiMuonVeSom.xaml
    /// </summary>
    public partial class frmCaiDatThietLapPhatDiMuonVeSom : Page
    {
        public class Test
        {
            public string LoaiPhat { get; set; }
            public string CaLVApDung { get; set; }
            public string TuThang { get; set; }
            public string DenThang { get; set; }
            public string ThoiGianTinhPhat { get; set; }
            public string MucPhat { get; set; }
            
        }
        private MainWindow Main;
        public frmCaiDatThietLapPhatDiMuonVeSom(MainWindow main)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            BrushConverter bc = new BrushConverter();
            if (main.i == 0)
            {
                textDiMuonVeSom.Foreground = (Brush)bc.ConvertFrom("#666666");
                lineDiMuonVeSom.Visibility = Visibility.Collapsed;
                textCaiDatDiMuonVeSom.Foreground = (Brush)bc.ConvertFrom("#4C5BD4");
                lineCaiDatDiMuonVeSom.Visibility = Visibility.Visible;
                textNghiSaiQuyDinh.Foreground = (Brush)bc.ConvertFrom("#666666");
                lineNghiSaiQuyDinh.Visibility = Visibility.Collapsed;
                textDanhSachNghiSaiQD.Foreground = (Brush)bc.ConvertFrom("#666666");
                lineDanhSachNghiSaiQD.Visibility = Visibility.Collapsed;
                frmCaiDatDiMuonVeSom frm = new frmCaiDatDiMuonVeSom(Main);
                pnlHienThi.Children.Clear();
                object content = frm.Content;
                frm.Content = null;
                pnlHienThi.Children.Add(content as UIElement);
            }
            if (Main.j == 1)
            {
                if (main.i == 3)
                {
                    BrushConverter bc1 = new BrushConverter();
                    textDiMuonVeSom.Foreground = (Brush)bc1.ConvertFrom("#666666");
                    lineDiMuonVeSom.Visibility = Visibility.Collapsed;
                    textCaiDatDiMuonVeSom.Foreground = (Brush)bc1.ConvertFrom("#666666");
                    lineCaiDatDiMuonVeSom.Visibility = Visibility.Collapsed;
                    textNghiSaiQuyDinh.Foreground = (Brush)bc1.ConvertFrom("#666666");
                    lineNghiSaiQuyDinh.Visibility = Visibility.Collapsed;
                    textDanhSachNghiSaiQD.Foreground = (Brush)bc1.ConvertFrom("#4C5BD4");
                    lineDanhSachNghiSaiQD.Visibility = Visibility.Visible;
                    frmDanhSachNghiSaiQuyDinh frm1 = new frmDanhSachNghiSaiQuyDinh(Main);
                    frm1.borThang.HorizontalAlignment = HorizontalAlignment.Left;
                    frm1.borThang.Margin = new Thickness(546, 63, 0, 0);
                    frm1.borNam.HorizontalAlignment = HorizontalAlignment.Left;
                    frm1.borNam.Margin = new Thickness(10, 108, 0, 0);
                    pnlHienThi.Children.Clear();
                    object content1 = frm1.Content;
                    frm1.Content = null;
                    pnlHienThi.Children.Add(content1 as UIElement);
                    if (Main.Nam != "")
                    {
                        frm1.textHienThiNam.Text = Main.Nam;

                    }
                    if (Main.Thang != "")
                    {
                        frm1.textHienThiThang.Text = Main.Thang;
                    }
                    if (Main.PhongBan != "")
                    {
                        frm1.textHienThiPhongBan.Text = Main.PhongBan;
                    }
                    if (Main.NhanVien != "")
                    {
                        frm1.textHienThiNhanVien.Text = Main.NhanVien;
                    }
                }
                else if (main.i == 2)
                {
                    BrushConverter bc1 = new BrushConverter();
                    textDiMuonVeSom.Foreground = (Brush)bc1.ConvertFrom("#4C5BD4");
                    lineDiMuonVeSom.Visibility = Visibility.Visible;
                    textCaiDatDiMuonVeSom.Foreground = (Brush)bc1.ConvertFrom("#666666");
                    lineCaiDatDiMuonVeSom.Visibility = Visibility.Collapsed;
                    textNghiSaiQuyDinh.Foreground = (Brush)bc1.ConvertFrom("#666666");
                    lineNghiSaiQuyDinh.Visibility = Visibility.Collapsed;
                    textDanhSachNghiSaiQD.Foreground = (Brush)bc1.ConvertFrom("#666666");
                    lineDanhSachNghiSaiQD.Visibility = Visibility.Collapsed;
                    frmDSDiMuonVeSom frm1 = new frmDSDiMuonVeSom(Main);
                    frm1.borThang.HorizontalAlignment = HorizontalAlignment.Left;
                    frm1.borThang.Margin = new Thickness(546, 63, 0, 0);
                    frm1.borNam.HorizontalAlignment = HorizontalAlignment.Left;
                    frm1.borNam.Margin = new Thickness(10, 108, 0, 0);
                    pnlHienThi.Children.Clear();
                    object content1 = frm1.Content;
                    frm1.Content = null;
                    pnlHienThi.Children.Add(content1 as UIElement);
                    if (Main.Nam != "")
                    {
                        frm1.textHienThiNam.Text = Main.Nam;

                    }
                    if (Main.Thang != "")
                    {
                        frm1.textHienThiThang.Text = Main.Thang;
                    }
                    if (Main.PhongBan != "")
                    {
                        frm1.textHienThiPhongBan.Text = Main.PhongBan;
                    }
                    if (Main.NhanVien != "")
                    {
                        frm1.textHienThiNhanVien.Text = Main.NhanVien;
                    }
                }
            }
            else if (Main.j == 2)
            {
                if (main.i == 3)
                {
                    BrushConverter bc1 = new BrushConverter();
                    textDiMuonVeSom.Foreground = (Brush)bc1.ConvertFrom("#666666");
                    lineDiMuonVeSom.Visibility = Visibility.Collapsed;
                    textCaiDatDiMuonVeSom.Foreground = (Brush)bc1.ConvertFrom("#666666");
                    lineCaiDatDiMuonVeSom.Visibility = Visibility.Collapsed;
                    textNghiSaiQuyDinh.Foreground = (Brush)bc1.ConvertFrom("#666666");
                    lineNghiSaiQuyDinh.Visibility = Visibility.Collapsed;
                    textDanhSachNghiSaiQD.Foreground = (Brush)bc1.ConvertFrom("#4C5BD4");
                    lineDanhSachNghiSaiQD.Visibility = Visibility.Visible;
                    frmDanhSachNghiSaiQuyDinh frm1 = new frmDanhSachNghiSaiQuyDinh(Main);
                    frm1.borNam.HorizontalAlignment = HorizontalAlignment.Left;
                    frm1.borNam.VerticalAlignment = VerticalAlignment.Top;
                    frm1.borNam.Margin = new Thickness(814, 64, 0, 0);
                    pnlHienThi.Children.Clear();
                    object content1 = frm1.Content;
                    frm1.Content = null;
                    pnlHienThi.Children.Add(content1 as UIElement);
                    if (Main.Nam != "")
                    {
                        frm1.textHienThiNam.Text = Main.Nam;

                    }
                    if (Main.Thang != "")
                    {
                        frm1.textHienThiThang.Text = Main.Thang;
                    }
                    if (Main.PhongBan != "")
                    {
                        frm1.textHienThiPhongBan.Text = Main.PhongBan;
                    }
                    if (Main.NhanVien != "")
                    {
                        frm1.textHienThiNhanVien.Text = Main.NhanVien;
                    }
                }
                else if (main.i == 2)
                {
                    BrushConverter bc1 = new BrushConverter();
                    textDiMuonVeSom.Foreground = (Brush)bc1.ConvertFrom("#4C5BD4");
                    lineDiMuonVeSom.Visibility = Visibility.Visible;
                    textCaiDatDiMuonVeSom.Foreground = (Brush)bc1.ConvertFrom("#666666");
                    lineCaiDatDiMuonVeSom.Visibility = Visibility.Collapsed;
                    textNghiSaiQuyDinh.Foreground = (Brush)bc1.ConvertFrom("#666666");
                    lineNghiSaiQuyDinh.Visibility = Visibility.Collapsed;
                    textDanhSachNghiSaiQD.Foreground = (Brush)bc1.ConvertFrom("#666666");
                    lineDanhSachNghiSaiQD.Visibility = Visibility.Collapsed;
                    frmDSDiMuonVeSom frm1 = new frmDSDiMuonVeSom(Main);
                    frm1.borNam.HorizontalAlignment = HorizontalAlignment.Left;
                    frm1.borNam.VerticalAlignment = VerticalAlignment.Top;
                    frm1.borNam.Margin = new Thickness(814, 64, 0, 0);
                    pnlHienThi.Children.Clear();
                    object content1 = frm1.Content;
                    frm1.Content = null;
                    pnlHienThi.Children.Add(content1 as UIElement);
                    if (Main.Nam != "")
                    {
                        frm1.textHienThiNam.Text = Main.Nam;

                    }
                    if (Main.Thang != "")
                    {
                        frm1.textHienThiThang.Text = Main.Thang;
                    }
                    if (Main.PhongBan != "")
                    {
                        frm1.textHienThiPhongBan.Text = Main.PhongBan;
                    }
                    if (Main.NhanVien != "")
                    {
                        frm1.textHienThiNhanVien.Text = Main.NhanVien;
                    }
                }
            }
            else if (Main.j == 3)
            {
                if (main.i == 3)
                {
                    BrushConverter bc1 = new BrushConverter();
                    textDiMuonVeSom.Foreground = (Brush)bc1.ConvertFrom("#4C5BD4");
                    lineDiMuonVeSom.Visibility = Visibility.Visible;
                    textCaiDatDiMuonVeSom.Foreground = (Brush)bc1.ConvertFrom("#666666");
                    lineCaiDatDiMuonVeSom.Visibility = Visibility.Collapsed;
                    textNghiSaiQuyDinh.Foreground = (Brush)bc1.ConvertFrom("#666666");
                    lineNghiSaiQuyDinh.Visibility = Visibility.Collapsed;
                    textDanhSachNghiSaiQD.Foreground = (Brush)bc1.ConvertFrom("#666666");
                    lineDanhSachNghiSaiQD.Visibility = Visibility.Collapsed;
                    frmDanhSachNghiSaiQuyDinh frm1 = new frmDanhSachNghiSaiQuyDinh(Main);
                    frm1.borNam.HorizontalAlignment = HorizontalAlignment.Left;
                    frm1.borNam.VerticalAlignment = VerticalAlignment.Top;
                    frm1.borNam.Margin = new Thickness(278, 108, 0, 0);
                    frm1.borThang.HorizontalAlignment = HorizontalAlignment.Left;
                    frm1.borThang.VerticalAlignment = VerticalAlignment.Top;
                    frm1.borThang.Margin = new Thickness(10, 108, 0, 0);
                    pnlHienThi.Children.Clear();
                    object content1 = frm1.Content;
                    frm1.Content = null;
                    pnlHienThi.Children.Add(content1 as UIElement);
                    if (Main.Nam != "")
                    {
                        frm1.textHienThiNam.Text = Main.Nam;

                    }
                    if (Main.Thang != "")
                    {
                        frm1.textHienThiThang.Text = Main.Thang;
                    }
                    if (Main.PhongBan != "")
                    {
                        frm1.textHienThiPhongBan.Text = Main.PhongBan;
                    }
                    if (Main.NhanVien != "")
                    {
                        frm1.textHienThiNhanVien.Text = Main.NhanVien;
                    }
                }
                else if (main.i == 2)
                {
                    BrushConverter bc1 = new BrushConverter();
                    textDiMuonVeSom.Foreground = (Brush)bc1.ConvertFrom("#4C5BD4");
                    lineDiMuonVeSom.Visibility = Visibility.Visible;
                    textCaiDatDiMuonVeSom.Foreground = (Brush)bc1.ConvertFrom("#666666");
                    lineCaiDatDiMuonVeSom.Visibility = Visibility.Collapsed;
                    textNghiSaiQuyDinh.Foreground = (Brush)bc1.ConvertFrom("#666666");
                    lineNghiSaiQuyDinh.Visibility = Visibility.Collapsed;
                    textDanhSachNghiSaiQD.Foreground = (Brush)bc1.ConvertFrom("#666666");
                    lineDanhSachNghiSaiQD.Visibility = Visibility.Collapsed;
                    frmDSDiMuonVeSom frm1 = new frmDSDiMuonVeSom(Main);
                    frm1.borNam.HorizontalAlignment = HorizontalAlignment.Left;
                    frm1.borNam.VerticalAlignment = VerticalAlignment.Top;
                    frm1.borNam.Margin = new Thickness(278, 108, 0, 0);
                    frm1.borThang.HorizontalAlignment = HorizontalAlignment.Left;
                    frm1.borThang.VerticalAlignment = VerticalAlignment.Top;
                    frm1.borThang.Margin = new Thickness(10, 108, 0, 0);
                    pnlHienThi.Children.Clear();
                    object content1 = frm1.Content;
                    frm1.Content = null;
                    pnlHienThi.Children.Add(content1 as UIElement);
                    if (Main.Nam != "")
                    {
                        frm1.textHienThiNam.Text = Main.Nam;

                    }
                    if (Main.Thang != "")
                    {
                        frm1.textHienThiThang.Text = Main.Thang;
                    }
                    if (Main.PhongBan != "")
                    {
                        frm1.textHienThiPhongBan.Text = Main.PhongBan;
                    }
                    if (Main.NhanVien != "")
                    {
                        frm1.textHienThiNhanVien.Text = Main.NhanVien;
                    }
                }
            }
            else if (Main.j == 4)
            {
                BrushConverter bc1 = new BrushConverter();
                textDiMuonVeSom.Foreground = (Brush)bc1.ConvertFrom("#666666");
                lineDiMuonVeSom.Visibility = Visibility.Visible;
                textCaiDatDiMuonVeSom.Foreground = (Brush)bc1.ConvertFrom("#666666");
                lineCaiDatDiMuonVeSom.Visibility = Visibility.Collapsed;
                textNghiSaiQuyDinh.Foreground = (Brush)bc1.ConvertFrom("#4c5bd4");
                lineNghiSaiQuyDinh.Visibility = Visibility.Collapsed;
                textDanhSachNghiSaiQD.Foreground = (Brush)bc1.ConvertFrom("#666666");
                lineDanhSachNghiSaiQD.Visibility = Visibility.Collapsed;
                frmNghiSaiQuyDinh frm1 = new frmNghiSaiQuyDinh(Main);
                frm1.borNam.HorizontalAlignment = HorizontalAlignment.Left;
                frm1.borNam.VerticalAlignment = VerticalAlignment.Top;
                frm1.borNam.Margin = new Thickness(10, 108, 0, 0);
                
                pnlHienThi.Children.Clear();
                object content1 = frm1.Content;
                frm1.Content = null;
                pnlHienThi.Children.Add(content1 as UIElement);
                
                
            }
            else if (Main.j == 5)
            {
                BrushConverter bc1 = new BrushConverter();
                textDiMuonVeSom.Foreground = (Brush)bc1.ConvertFrom("#666666");
                lineDiMuonVeSom.Visibility = Visibility.Collapsed;
                textCaiDatDiMuonVeSom.Foreground = (Brush)bc1.ConvertFrom("#666666");
                lineCaiDatDiMuonVeSom.Visibility = Visibility.Collapsed;
                textNghiSaiQuyDinh.Foreground = (Brush)bc1.ConvertFrom("#666666");
                lineNghiSaiQuyDinh.Visibility = Visibility.Collapsed;
                textDanhSachNghiSaiQD.Foreground = (Brush)bc1.ConvertFrom("#4c5bd4");
                lineDanhSachNghiSaiQD.Visibility = Visibility.Visible;
                frmDanhSachNghiSaiQuyDinh frm1 = new frmDanhSachNghiSaiQuyDinh(Main);
                frm1.borNam.HorizontalAlignment = HorizontalAlignment.Left;
                frm1.borNam.VerticalAlignment = VerticalAlignment.Top;
                frm1.borNam.Margin = new Thickness(814, 64, 0, 0);

                pnlHienThi.Children.Clear();
                object content1 = frm1.Content;
                frm1.Content = null;
                pnlHienThi.Children.Add(content1 as UIElement);
                if (Main.Nam != "")
                {
                    frm1.textHienThiNam.Text = Main.Nam;

                }
                if (Main.Thang != "")
                {
                    frm1.textHienThiThang.Text = Main.Thang;
                }
                if (Main.PhongBan != "")
                {
                    frm1.textHienThiPhongBan.Text = Main.PhongBan;
                }
                if (Main.NhanVien != "")
                {
                    frm1.textHienThiNhanVien.Text = Main.NhanVien;
                }

            }
        }

        private void tabDiMuonVeSom_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            textDiMuonVeSom.Foreground = (Brush)bc.ConvertFrom("#4C5BD4");
            lineDiMuonVeSom.Visibility = Visibility.Visible;
            textCaiDatDiMuonVeSom.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineCaiDatDiMuonVeSom.Visibility = Visibility.Collapsed;
            textNghiSaiQuyDinh.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineNghiSaiQuyDinh.Visibility = Visibility.Collapsed;
            textDanhSachNghiSaiQD.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineDanhSachNghiSaiQD.Visibility = Visibility.Collapsed;
            frmDSDiMuonVeSom frm = new frmDSDiMuonVeSom(Main);
            pnlHienThi.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            pnlHienThi.Children.Add(content as UIElement);
            Main.i = 2;
        }

        private void tabCaiDatDiMuonVeSom_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            textDiMuonVeSom.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineDiMuonVeSom.Visibility = Visibility.Collapsed;
            textCaiDatDiMuonVeSom.Foreground = (Brush)bc.ConvertFrom("#4C5BD4");
            lineCaiDatDiMuonVeSom.Visibility = Visibility.Visible;
            textNghiSaiQuyDinh.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineNghiSaiQuyDinh.Visibility = Visibility.Collapsed;
            textDanhSachNghiSaiQD.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineDanhSachNghiSaiQD.Visibility = Visibility.Collapsed;
            frmCaiDatDiMuonVeSom frm = new frmCaiDatDiMuonVeSom(Main);
            pnlHienThi.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            pnlHienThi.Children.Add(content as UIElement);
        }

        private void tabNghiSaiQuyDinh_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            textDiMuonVeSom.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineDiMuonVeSom.Visibility = Visibility.Collapsed;
            textCaiDatDiMuonVeSom.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineCaiDatDiMuonVeSom.Visibility = Visibility.Collapsed;
            textNghiSaiQuyDinh.Foreground = (Brush)bc.ConvertFrom("#4C5BD4");
            lineNghiSaiQuyDinh.Visibility = Visibility.Visible;
            textDanhSachNghiSaiQD.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineDanhSachNghiSaiQD.Visibility = Visibility.Collapsed;
            frmNghiSaiQuyDinh frm = new frmNghiSaiQuyDinh(Main);
            pnlHienThi.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            pnlHienThi.Children.Add(content as UIElement);
        }

        private void tabDanhSachNghiSaiQD_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            textDiMuonVeSom.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineDiMuonVeSom.Visibility = Visibility.Collapsed;
            textCaiDatDiMuonVeSom.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineCaiDatDiMuonVeSom.Visibility = Visibility.Collapsed;
            textNghiSaiQuyDinh.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineNghiSaiQuyDinh.Visibility = Visibility.Collapsed;
            textDanhSachNghiSaiQD.Foreground = (Brush)bc.ConvertFrom("#4C5BD4");
            lineDanhSachNghiSaiQD.Visibility = Visibility.Visible;
            frmDanhSachNghiSaiQuyDinh frm = new frmDanhSachNghiSaiQuyDinh(Main);
            pnlHienThi.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            pnlHienThi.Children.Add(content as UIElement);
        }
    }
}
