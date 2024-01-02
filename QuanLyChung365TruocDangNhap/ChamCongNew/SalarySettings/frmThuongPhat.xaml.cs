using QuanLyChung365TruocDangNhap.ChamCongNew.Common;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.ThuongPhat;
//using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Win32;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using static QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.Entities.ListOrganizeEntities;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.clsNhanVienThuocCongTy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings
{
    /// <summary>
    /// Interaction logic for frmThuongPhat.xaml
    /// </summary>
    public partial class frmThuongPhat : System.Windows.Controls.Page
    { 
        public class PhongBan
        {
            public string TenPB { get; set; }
        }
        public class Thang
        {
            public string thang { get; set; }
        }
        public class Nam
        {
            public string nam { get; set; }
        }
        
        List<Nam> lstNam = new List<Nam>();
        List<Nam> lstSearchNam = new List<Nam>();
        List<Thang> lstThang = new List<Thang>();
        List<Thang> lstSearchThang = new List<Thang>();
        List<OOP.clsPhongBanThuocCongTy.Item> lstPhongBan = new List<OOP.clsPhongBanThuocCongTy.Item>();
        List<OOP.clsNhanVienThuocCongTy.ListUser> lstNhanVien = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        List<OOP.clsNhanVienThuocCongTy.ListUser> lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        private double TongTT;
        private double TongTP;
        private string IdNV = "0";
        private string IdPB = "0";
        private int TongSoTrang = 0;
        private int SoDu = 0;
        private int countdownValue = 2;
        public event EventHandler CountdownFinished;
        private DispatcherTimer countdownTimer;
        private List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal> lstChuaTL = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
        private List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal> lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
        private List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal> lstChuaTLFilter = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
        private List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal> lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
        private MainWindow Main;
        private string DuongDanEx = Environment.CurrentDirectory + "\\TempExcel\\FileThuongPhat.xltx";
        public static DataTable tb_ThuongPhat = new DataTable();

        public frmThuongPhat(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            tb_ThuongPhat = Function.clsExPortExcel.NewTables("tb_ThuongPhat", new string[] { "colIDNhanVien", "colTenNhanVien", "colTienThuong", "colTienPhat"}, new int[] { 100, 250, 150, 100 });
            LoadDLNam();
            LoadDLThang();
            LoadDLPhongBan();
            LoadDLNhanVien();
            LoadDLThuongPhat();
            main.scrollMain.ScrollToVerticalOffset(0);
        }

        private List<ListUser> lstSearchNVTheoPhongBan2 = new List<ListUser>();
        private List<ListUser> lstSearchNVTheoPhongBan = new List<ListUser>();
        private List<ListUser> lstLuongCBFilter = new List<ListUser>();
        public OOP.OrganizeData SelectedOrganize = new OOP.OrganizeData();
        private List<ListUser> lstThuongPhat = new List<ListUser>();
        List<clsLuongCoBan.OrganizeDetail> lstORD = new List<clsLuongCoBan.OrganizeDetail>();
        private void Organize_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (searchBarToChuc.SelectedItem != null)
                {
                    searchBarToChuc.PlaceHolderForground = "#474747";
                }
                else
                {
                    searchBarToChuc.PlaceHolderForground = "#ACACAC";
                }
                lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
                OOP.OrganizeData pb = (OOP.OrganizeData)searchBarToChuc.SelectedItem;
                if (pb != null)
                {
                    searchBarToChuc.Text = pb.organizeDetailName;
                    Main.PhongBan = pb.organizeDetailName;
                    IdPB = pb.id.ToString();
                    if (IdPB != "0")
                    {
                        foreach (var nv in Main.lstNhanVienThuocCongTy)
                        {
                            if (nv.organizeDetail != null)
                            {
                                lstORD.Add(nv.organizeDetail);
                                foreach (var item in lstORD)
                                {
                                    if (IdPB == item.id.ToString())
                                    {
                                        lstSearchNV.Add(nv);
                                    }
                                }
                            }

                        }
                        OOP.clsNhanVienThuocCongTy.ListUser Staff = new OOP.clsNhanVienThuocCongTy.ListUser();
                        Staff.idQLC = 0;
                        Staff.userName = "Tất cả nhân viên";
                        lstSearchNV.Insert(0, Staff);
                        searchBarNhanVien.ItemsSource = lstSearchNV;
                    }
                    else
                    {
                        searchBarNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy;
                    }

                }
            }
            catch (Exception)
            {}
        }

        List<ListUser> listSaftSearch = new List<ListUser>();
        List<ListUser> lstSelectSaft = new List<ListUser>();
        private string SearchNV = "";
        private string SearchPB = "";
        private void Staff_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (searchBarNhanVien.SelectedItem != null)
                {
                    searchBarNhanVien.PlaceHolderForground = "#474747";
                    listSaftSearch = new List<ListUser>();
                    var chonca = ((ListUser)searchBarNhanVien.SelectedItem).idQLC.ToString();
                    IdNV = chonca;
                    SearchNV = ((ListUser)searchBarNhanVien.SelectedItem).idQLC.ToString();
                    if (!Main.lstNhanVienThuocCongTy.Any(item => item.idQLC.ToString() == chonca))
                    {
                        listSaftSearch = Main.lstNhanVienThuocCongTy.ToList();
                        foreach (var item in listSaftSearch)
                        {
                            Main.NhanVien = item.userName;
                            IdNV = item.idQLC.ToString();
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
        private void ChonThang_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbo_ChonThang.SelectedItem != null)
                {
                    cbo_ChonThang.PlaceHolderForground = "#474747";
                    popup.Visibility = Visibility.Collapsed;

                }
                else
                {
                    cbo_ChonThang.PlaceHolderForground = "#ACACAC";
                }
            }
            catch (Exception)
            {}
            
        }
        private void ChonNam_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbo_ChonNam.SelectedItem != null)
                {
                    cbo_ChonNam.PlaceHolderForground = "#474747";
                    popup.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cbo_ChonNam.PlaceHolderForground = "#ACACAC";
                }
            }
            catch (Exception)
            {}
        }
       
        public void LoadDLThuongPhat()
        {
            try
            {
                lstChuaTL = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                using (RestClient restclient = new RestClient(new Uri("https://api.timviec365.vn/api/tinhluong/congty/take_thuong_phat")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("month", cbo_ChonThang.Text.Split(' ')[1].ToString());
                    request.AddParameter("year", cbo_ChonNam.Text.Split(' ')[1].ToString());
                    request.AddParameter("id_com", Main.IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.Root chuaTL = JsonConvert.DeserializeObject<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.Root>(b);
                    if (chuaTL.data.data_final != null)
                    {
                        foreach (var item in chuaTL.data.data_final)
                        {
                            lstChuaTL.Add(item);
                        }
                        if (lstChuaTL.Count > 10)
                        {
                            TongSoTrang = chuaTL.data.data_final.Count / 10;
                            SoDu = 10 - (chuaTL.data.data_final.Count % 10);
                            if (chuaTL.data.data_final.Count % 10 > 0)
                            {
                                TongSoTrang++;
                            }
                            if (TongSoTrang < 3)
                            {
                                borPage3.Visibility = Visibility.Collapsed;
                            }
                            for (int i = 0; i < 10; i++)
                            {
                                lstChuaTLPT.Add(chuaTL.data.data_final[i]);
                            }
                            lsvThuongPhat.ItemsSource = lstChuaTLPT;
                            docPhanTrang.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            lsvThuongPhat.ItemsSource = lstChuaTL;
                            docPhanTrang.Visibility = Visibility.Collapsed;
                        }
                        LoadDataInDataTable(lstChuaTL);
                    }
                    loading.Visibility = Visibility.Collapsed;
                }
            }
            catch
            {
            }
        }
        private void LoadDataInDataTable(List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal> lstChuaTL)
        {
            try
            {
                foreach (var item in lstChuaTL)
                {
                    DataRow dr = tb_ThuongPhat.NewRow();

                    dr["colIDNhanVien"] = item.inforUser.idQLC;
                    dr["colTenNhanVien"] = item.inforUser.userName;
                    dr["colTienThuong"] = item.tt_thuong.tong_thuong;
                    dr["colTienPhat"] = item.tt_phat.tong_phat;
                    tb_ThuongPhat.Rows.Add(dr);
                }
            }
            catch (Exception)
            {
            }
        }
        private void LoadDLNhanVien()
        {
            try{ searchBarNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy;}catch (Exception){}
        }
        private void LoadDLPhongBan()
        {
            try
            {
                searchBarToChuc.ItemsSource = Main.lstOrganizeData;
            }
            catch (Exception){}
        }
        private void LoadDLThang()
        {
            #region Tháng
            cbo_ChonThang.PlaceHolder = "Tháng " + DateTime.Now.Month.ToString();
            cbo_ChonThang.Text = "Tháng " + DateTime.Now.Month.ToString();
            lstThang.Add(new Thang { thang = "Tháng 1" });
            lstThang.Add(new Thang { thang = "Tháng 2" });
            lstThang.Add(new Thang { thang = "Tháng 3" });
            lstThang.Add(new Thang { thang = "Tháng 4" });
            lstThang.Add(new Thang { thang = "Tháng 5" });
            lstThang.Add(new Thang { thang = "Tháng 6" });
            lstThang.Add(new Thang { thang = "Tháng 7" });
            lstThang.Add(new Thang { thang = "Tháng 8" });
            lstThang.Add(new Thang { thang = "Tháng 9" });
            lstThang.Add(new Thang { thang = "Tháng 10" });
            lstThang.Add(new Thang { thang = "Tháng 11" });
            lstThang.Add(new Thang { thang = "Tháng 12" });
            cbo_ChonThang.ItemsSource = lstThang;
            #endregion
        }
        private void LoadDLNam()
        {
            #region Năm
            cbo_ChonNam.PlaceHolder = "Năm " + DateTime.Now.Year.ToString();
            cbo_ChonNam.Text = "Năm " + DateTime.Now.Year.ToString();
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 5).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 4).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 3).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 2).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + DateTime.Now.Year });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 2).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 3).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 4).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 5).ToString() });
            cbo_ChonNam.ItemsSource = lstNam;
            #endregion
        }
        private void lsvPhongBan_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is ListView && !e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((System.Windows.Controls.Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }
        private void dgv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }
        private void lsvDSNSChuaTL_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }
        public OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal Tp;
        private void borThuong_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal tp = (sender as System.Windows.Controls.Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal;
                if (tp != null)
                {
                    Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset);
                    System.Windows.Controls.Border b = sender as System.Windows.Controls.Border;
                    Tp = tp;
                   
                        DateTime aDateTimet;
                        foreach (var item in tp.tt_thuong.ds_thuong)
                        {
                            DateTime.TryParse(item.pay_day, out aDateTimet);
                            item.pay_day = aDateTimet.ToString("dd-MM-yyyy");
                        }
                    
                    dgvThuongNV.ItemsSource = tp.tt_thuong.ds_thuong;
                    borChiTietThuongNV.Margin = new Thickness(Mouse.GetPosition(popupTP).X - 250, (Mouse.GetPosition(popupTP).Y + 20), 0, 0);
                    borChiTietThuongNV.VerticalAlignment = VerticalAlignment.Top;
                    borChiTietThuongNV.HorizontalAlignment = HorizontalAlignment.Left;
                    popupTP.Visibility = Visibility.Visible;
                    borChiTietThuongNV.Visibility = Visibility.Visible;
                }
            }
            catch (Exception)
            {
            }
            
        }
        private void popupTP_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            popupTP.Children.Clear();
            popupTP.Visibility = Visibility.Hidden;
            borChiTietThuongNV.Visibility = Visibility.Collapsed;
            borChiTietPhatNV.Visibility = Visibility.Collapsed;
            borChiTietPhatCong.Visibility = Visibility.Collapsed;
        }
        private void btnThemTPTien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsThuong Thuong = (sender as System.Windows.Controls.Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsThuong;
                OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat Phat = (sender as System.Windows.Controls.Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat;
                OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal tp = (sender as System.Windows.Controls.Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal;
                if (tp != null)
                {
                    Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.ThuongPhat.PopUpThemMoiThuongPhat(Main, tp, Thuong, Phat, this));
                }
            }
            catch (Exception)
            {}
        }

        private void btnThemTPCong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                
                OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhatCong PhatCong = (sender as System.Windows.Controls.Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhatCong;
                OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal tp = (sender as System.Windows.Controls.Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal;
                if (tp != null)
                {
                    Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.ThuongPhat.PopUpThemMoiThuongPhat(Main, tp, PhatCong, this));
                }
            }
            catch (Exception)
            { }
        }

        private void bod_ChinhSuaThuong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsThuong Thuong = (sender as System.Windows.Controls.Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsThuong;
                if (Thuong != null)
                {
                    Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.ThuongPhat.PopUpThemMoiThuongPhat(Main, Tp, Thuong, null, this));
                }
            }
            catch (Exception)
            { } 
        }
        private void bod_ChinhSuaPhat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat phat = (sender as System.Windows.Controls.Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat;
                if (phat != null)
                {
                    Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.ThuongPhat.PopUpThemMoiThuongPhat(Main, Tp, null, phat, this));
                }
            }
            catch (Exception)
            {}
        }

        private void bod_ChinhSuaPhatCong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhatCong phatcong = (sender as System.Windows.Controls.Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhatCong;
                if (phatcong != null)
                {
                    Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.ThuongPhat.PopUpThemMoiThuongPhat(Main, Tp, phatcong, this));
                    this.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception)
            { }
        }
        private void btnXoaPhat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat phat = (sender as System.Windows.Controls.Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat;
                if (phat != null)
                {
                    Main.grShowPopup.Children.Add(new PopUpHoiTruocKhiXoaThuongPhat(Main, phat, "Bạn có chắc chăn muốn xoá mức phạt này không?", null, this));
                }
            }
            catch (Exception)
            {}
        }
        private void btnXoaThuong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsThuong thuong = (sender as System.Windows.Controls.Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsThuong;
                if (thuong != null)
                {
                    Main.grShowPopup.Children.Add(new PopUpHoiTruocKhiXoaThuongPhat(Main, thuong, "Bạn có chắc chăn muốn xoá mức thưởng này không?", null, this));
                }
            }
            catch (Exception)
            {
            }
        }
        private void btnXoaPhatCong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhatCong phatcong = (sender as System.Windows.Controls.Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhatCong;
                if (phatcong != null)
                {
                    Main.grShowPopup.Children.Add(new PopUpHoiTruocKhiXoaThuongPhat(Main, phatcong, "Bạn có chắc chăn muốn xoá phạt công này không?", null, this));
                }
            }
            catch (Exception)
            {
            }
        }
        private void borPhat_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal tp = (sender as System.Windows.Controls.Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal;
                if (tp != null)
                {
                    Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset);
                    Tp = tp;
                    DateTime aDateTimep;
                    foreach (var item in tp.tt_phat.ds_phat)
                    {
                        DateTime.TryParse(item.pay_day, out aDateTimep);
                        item.pay_day = aDateTimep.ToString("dd-MM-yyyy");
                    }
                    dgvPhatNV.ItemsSource = tp.tt_phat.ds_phat;
                    borChiTietPhatNV.VerticalAlignment = VerticalAlignment.Top;
                    borChiTietPhatNV.HorizontalAlignment = HorizontalAlignment.Left;
                    borChiTietPhatNV.Margin = new Thickness(Mouse.GetPosition(popupTP).X - 350, Mouse.GetPosition(popupTP).Y + 20, 0, 0);
                    popupTP.Visibility = Visibility.Visible;
                    borChiTietPhatNV.Visibility = Visibility.Visible;
                }
            }
            catch (Exception)
            {}
        }

        private void borPhatCong_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal tp = (sender as System.Windows.Controls.Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal;
                if (tp != null)
                {
                    Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset);
                    Tp = tp;
                    DateTime aDateTimepc;
                    foreach (var item in tp.tt_phat_cong.ds_phat_cong)
                    {
                        DateTime.TryParse(item.phatcong_time, out aDateTimepc);
                        item.phatcong_time = aDateTimepc.ToString("dd-MM-yyyy");
                    }
                    dgvPhatCongNV.ItemsSource = tp.tt_phat_cong.ds_phat_cong;
                    borChiTietPhatCong.VerticalAlignment = VerticalAlignment.Top;
                    borChiTietPhatCong.HorizontalAlignment = HorizontalAlignment.Left;
                    borChiTietPhatCong.Margin = new Thickness(Mouse.GetPosition(popupTP).X - 300, Mouse.GetPosition(popupTP).Y + 20, 0, 0);
                    popupTP.Visibility = Visibility.Visible;
                    borChiTietPhatCong.Visibility = Visibility.Visible;
                }
            }
            catch (Exception)
            { }
        }
        public void LoadLaiThemThuongPhat()
        {
            try
            {
                lstChuaTLFilter.Clear();
                LoadDLThuongPhat();
                if (searchBarNhanVien.SelectedItem != null && ((ListUser)searchBarNhanVien.SelectedItem)._id > 0)
                {
                    foreach (var item in lstChuaTL)
                    {
                        if (IdNV == item.inforUser.idQLC.ToString())
                        {
                            lstChuaTLFilter.Add(item);
                        }
                        if (lstChuaTLFilter.Count < 10)
                        {
                            docPhanTrang.Visibility = Visibility.Collapsed;
                        }
                        lsvThuongPhat.ItemsSource = lstChuaTLFilter;
                        lsvThuongPhat.Items.Refresh();

                    }
                }
                else if (searchBarToChuc.SelectedItem != null && ((OOP.OrganizeData)searchBarToChuc.SelectedItem).id > 0)
                {
                    foreach (var item in lstChuaTL)
                    {
                        if (item.inforUser.idQLC.ToString() == IdNV && IdPB == "0")
                        {
                            lstChuaTLFilter.Add(item);
                        }
                        else if (IdNV == "0" && IdPB == item.inforUser.inForPerson.employee.organizeDetailId.ToString())
                        {
                            lstChuaTLFilter.Add(item);
                        }
                        else if (IdNV == item.inforUser.idQLC.ToString() && IdPB == item.inforUser.inForPerson.employee.organizeDetailId.ToString())
                        {
                            lstChuaTLFilter.Add(item);
                        }
                        lsvThuongPhat.ItemsSource = lstChuaTLFilter;
                        lsvThuongPhat.Items.Refresh();
                    }

                }
                else if (searchBarNhanVien.SelectedIndex == 0)
                {
                    LoadDLThuongPhat();
                }
                else
                {
                    LoadDLThuongPhat();
                }
            }
            catch (Exception)
            {
            }
        }
        public void btnThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                lstChuaTLFilter.Clear();
                LoadDLThuongPhat();
                if (searchBarNhanVien.SelectedItem != null && ((ListUser)searchBarNhanVien.SelectedItem)._id > 0)
                {
                    foreach (var item in lstChuaTL)
                    {
                        if (IdNV == item.inforUser.idQLC.ToString())
                        {
                            lstChuaTLFilter.Add(item);
                        }
                        if (lstChuaTLFilter.Count < 10)
                        {
                            docPhanTrang.Visibility = Visibility.Collapsed;
                        }
                        lsvThuongPhat.ItemsSource = lstChuaTLFilter;
                        lsvThuongPhat.Items.Refresh();
                       
                    }
                }
                else if (searchBarToChuc.SelectedItem != null && ((OOP.OrganizeData)searchBarToChuc.SelectedItem).id > 0)
                {
                    foreach (var item in lstChuaTL)
                    {
                        if (item.inforUser.idQLC.ToString() == IdNV && IdPB == "0")
                        {
                            lstChuaTLFilter.Add(item);
                        }
                        else if (IdNV == "0" && IdPB == item.inforUser.inForPerson.employee.organizeDetailId.ToString())
                        {
                            lstChuaTLFilter.Add(item);
                        }
                        else if (IdNV == item.inforUser.idQLC.ToString() && IdPB == item.inforUser.inForPerson.employee.organizeDetailId.ToString())
                        {
                            lstChuaTLFilter.Add(item);
                        }
                        lsvThuongPhat.ItemsSource = lstChuaTLFilter;
                        lsvThuongPhat.Items.Refresh();
                    }
               
                }
                else if (searchBarNhanVien.SelectedIndex == 0)
                {
                    LoadDLThuongPhat();
                }
                else
                {
                    LoadDLThuongPhat();
                }
            }
            catch (Exception)
            {
            } 
        }
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
                borPageCuoi.Visibility = Visibility.Visible;
                lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                for (int i = 0; i < 10; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                lsvThuongPhat.ItemsSource = lstChuaTLPT;
            }
            catch (Exception)
            {}
        }
        private void borLui1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BrushConverter brus = new BrushConverter();
                if (int.Parse(textPage1.Text) >= 1)
                {
                    if (textPage3.Text == TongSoTrang.ToString() && borPageCuoi.Visibility == Visibility.Collapsed)
                    {
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPageCuoi.Visibility = Visibility.Visible;
                        borLen1.Visibility = Visibility.Visible;
                        lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                        for (int i = TongSoTrang * 10 - 20; i < TongSoTrang * 10 - 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        lsvThuongPhat.ItemsSource = lstChuaTLPT;
                    }
                    else
                    {
                        if (textPage1.Text == "2")
                        {
                            borLui1.Visibility = Visibility.Collapsed;
                        }
                        if (textPage1.Text == "1")
                        {
                            borPageDau.Visibility = Visibility.Collapsed;
                            borLui1.Visibility = Visibility.Collapsed;
                            borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borLen1.Visibility = Visibility.Visible;
                            borPageCuoi.Visibility = Visibility.Visible;
                            lstChuaTL = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                            for (int i = 0; i < 10; i++)
                            {
                                lstChuaTLPT.Add(lstChuaTL[i]);
                            }
                            lsvThuongPhat.ItemsSource = lstChuaTLPT;
                        }
                        else
                        {
                            textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                            textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                            textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                            lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                            for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                            {
                                lstChuaTLPT.Add(lstChuaTL[i]);
                            }
                            lsvThuongPhat.ItemsSource = lstChuaTLPT;
                        }
                    }
                }
            }
            catch (Exception)
            {}
        }
        private void borPage1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (int.Parse(textPage1.Text) > 1)
                {
                    if (textPage1.Text == (TongSoTrang - 2).ToString() && borPageCuoi.Visibility == Visibility.Collapsed)
                    {
                        textPage1.Text = (TongSoTrang - 3).ToString();
                        textPage2.Text = (TongSoTrang - 2).ToString();
                        textPage3.Text = (TongSoTrang - 1).ToString();
                        BrushConverter brus = new BrushConverter();

                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borLen1.Visibility = Visibility.Visible;
                        borPageCuoi.Visibility = Visibility.Visible;
                        lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        lsvThuongPhat.ItemsSource = lstChuaTLPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                        lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        lsvThuongPhat.ItemsSource = lstChuaTLPT;
                    }
                }
                else
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
                    borLen1.Visibility = Visibility.Visible;
                    borPageCuoi.Visibility = Visibility.Visible;
                    lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                    for (int i = 0; i < 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvThuongPhat.ItemsSource = lstChuaTLPT;
                }
            }
            catch (Exception)
            {}
        }
        private void borPage2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (TongSoTrang == 2)
                {
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    BrushConverter brus = new BrushConverter();
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10 - 10 + (10 - SoDu); i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    lsvThuongPhat.ItemsSource = lstChuaTLPT;
                }
                else if (TongSoTrang > 2)
                {
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    borPageCuoi.Visibility = Visibility.Visible;
                    borLen1.Visibility = Visibility.Visible;
                    BrushConverter brus = new BrushConverter();
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    lsvThuongPhat.ItemsSource = lstChuaTLPT;
                }
            }
            catch (Exception)
            {}
        }
        private void borPage3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (textPage3.Text == TongSoTrang.ToString())
                {
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Collapsed;
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();

                    for (int i = int.Parse(textPage3.Text) * 10 - 10; i < int.Parse(textPage3.Text) * 10 - 10 + (10 - SoDu); i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    lsvThuongPhat.ItemsSource = lstChuaTLPT;
                }
                else
                {
                    if (TongSoTrang == 3)
                    {
                        borPageDau.Visibility = Visibility.Visible;
                        borLui1.Visibility = Visibility.Visible;
                        BrushConverter brus = new BrushConverter();
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                        for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        lsvThuongPhat.ItemsSource = lstChuaTLPT;
                    }
                    else if (TongSoTrang > 3)
                    {
                        if (textPage3.Text == TongSoTrang.ToString())
                        {
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
                            lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                            for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                            {
                                lstChuaTLPT.Add(lstChuaTL[i]);
                            }
                            lsvThuongPhat.ItemsSource = lstChuaTLPT;
                        }
                        else if (textPage3.Text == "3")
                        {
                            textPage1.Text = "2";
                            textPage2.Text = "3";
                            textPage3.Text = "4";
                            BrushConverter brus = new BrushConverter();
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                            lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                            for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                            {
                                lstChuaTLPT.Add(lstChuaTL[i]);
                            }
                            lsvThuongPhat.ItemsSource = lstChuaTLPT;
                        }
                        else
                        {
                            textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                            textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                            textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                            lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                            for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                            {
                                lstChuaTLPT.Add(lstChuaTL[i]);
                            }
                            lsvThuongPhat.ItemsSource = lstChuaTLPT;
                        }

                    }
                }
            }
            catch (Exception)
            {}
            
        }
        private void borLen1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (TongSoTrang == 3)
                {
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    lsvThuongPhat.ItemsSource = lstChuaTLPT;
                }
                else if (TongSoTrang > 3)
                {
                    if (textPage3.Text == TongSoTrang.ToString())
                    {
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
                        lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                        for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        lsvThuongPhat.ItemsSource = lstChuaTLPT;

                    }
                    else if (textPage3.Text == "3")
                    {

                        if (borPageDau.Visibility == Visibility.Collapsed && borPageCuoi.Visibility == Visibility.Visible)
                        {
                            BrushConverter brus = new BrushConverter();
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPageDau.Visibility = Visibility.Visible;
                            borLui1.Visibility = Visibility.Visible;
                            lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                            for (int i = 10; i < 20; i++)
                            {
                                lstChuaTLPT.Add(lstChuaTL[i]);
                            }
                            lsvThuongPhat.ItemsSource = lstChuaTLPT;

                        }
                        else if (borPageDau.Visibility == Visibility.Visible && borPageCuoi.Visibility == Visibility.Visible)
                        {
                            textPage1.Text = "2";
                            textPage2.Text = "3";
                            textPage3.Text = "4";
                            BrushConverter brus = new BrushConverter();
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                            lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                            for (int i = 20; i < 30; i++)
                            {
                                lstChuaTLPT.Add(lstChuaTL[i]);
                            }
                            lsvThuongPhat.ItemsSource = lstChuaTLPT;
                        }


                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                        lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        lsvThuongPhat.ItemsSource = lstChuaTLPT;
                    }

                }
            }
            catch (Exception)
            {}
        }
        private void borPageCuoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                borPageCuoi.Visibility = Visibility.Collapsed;
                borLen1.Visibility = Visibility.Collapsed;
                if (TongSoTrang >= 3)
                {
                    textPage1.Text = (TongSoTrang - 2).ToString();
                    textPage2.Text = (TongSoTrang - 1).ToString();
                    textPage3.Text = TongSoTrang.ToString();
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - 10 + (10 - SoDu); i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    lsvThuongPhat.ItemsSource = lstChuaTLPT;
                }

                else
                {
                    textPage1.Text = (TongSoTrang - 1).ToString();
                    textPage2.Text = TongSoTrang.ToString();
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    lsvThuongPhat.ItemsSource = lstChuaTLPT;
                }
            }
            catch (Exception)
            {}
            
        }
        private void borPageDauS_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //try
            //{
            //    BrushConverter brus = new BrushConverter();
            //    borPageDauS.Visibility = Visibility.Collapsed;
            //    borLui1S.Visibility = Visibility.Collapsed;
            //    borPage1S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            //    textPage1S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            //    borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //    textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //    borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //    textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //    textPage1S.Text = "1";
            //    textPage2S.Text = "2";
            //    textPage3S.Text = "3";
            //    borLen1S.Visibility = Visibility.Visible;
            //    borPageCuoiS.Visibility = Visibility.Visible;
            //    lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
            //    for (int i = 0; i < 10; i++)
            //    {
            //        lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
            //    }
            //    lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
            //}
            //catch (Exception)
            //{}
        }
        private void borLui1S_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //try
            //{
            //    BrushConverter brus = new BrushConverter();
            //    if (int.Parse(textPage1S.Text) >= 1)
            //    {
            //        if (textPage3S.Text == TongSoTrang.ToString() && borPageCuoiS.Visibility == Visibility.Collapsed)
            //        {
            //            borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //            textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //            borPage2S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            //            textPage2S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            //            borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //            textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //            borPageCuoiS.Visibility = Visibility.Visible;
            //            borLen1S.Visibility = Visibility.Visible;
            //            lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
            //            for (int i = TongSoTrang * 10 - 20; i < TongSoTrang * 10 - 10; i++)
            //            {
            //                lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
            //            }
            //            lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
            //        }
            //        else
            //        {
            //            if (textPage1S.Text == "1")
            //            {
            //                borPageDauS.Visibility = Visibility.Collapsed;
            //                borLui1S.Visibility = Visibility.Collapsed;
            //                borPage1S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            //                textPage1S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            //                borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //                textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //                borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //                textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //                borLen1S.Visibility = Visibility.Visible;
            //                borPageCuoiS.Visibility = Visibility.Visible;
            //                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
            //                for (int i = 0; i < 10; i++)
            //                {
            //                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
            //                }
            //                lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
            //            }
            //            else
            //            {
            //                textPage1S.Text = (int.Parse(textPage1S.Text) - 1).ToString();
            //                textPage2S.Text = (int.Parse(textPage2S.Text) - 1).ToString();
            //                textPage3S.Text = (int.Parse(textPage3S.Text) - 1).ToString();
            //                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
            //                for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
            //                {
            //                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
            //                }
            //                lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
            //            }
            //        }
            //    }
            //}
            //catch (Exception)
            //{}
           
        }
        private void borPage1S_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //try
            //{
            //    if (int.Parse(textPage1S.Text) > 1)
            //    {
            //        if (textPage1S.Text == (TongSoTrang - 2).ToString() && borPageCuoiS.Visibility == Visibility.Collapsed)
            //        {
            //            textPage1S.Text = (TongSoTrang - 3).ToString();
            //            textPage2S.Text = (TongSoTrang - 2).ToString();
            //            textPage3S.Text = (TongSoTrang - 1).ToString();
            //            BrushConverter brus = new BrushConverter();

            //            borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //            textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //            borPage2S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            //            textPage2S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            //            borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //            textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //            borLen1S.Visibility = Visibility.Visible;
            //            borPageCuoiS.Visibility = Visibility.Visible;
            //            lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
            //            for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
            //            {
            //                lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
            //            }
            //            lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
            //        }
            //        else
            //        {
            //            textPage1S.Text = (int.Parse(textPage1S.Text) - 1).ToString();
            //            textPage2S.Text = (int.Parse(textPage2S.Text) - 1).ToString();
            //            textPage3S.Text = (int.Parse(textPage3S.Text) - 1).ToString();
            //            lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
            //            for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
            //            {
            //                lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
            //            }
            //            lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
            //        }
            //    }
            //    else
            //    {
            //        BrushConverter brus = new BrushConverter();
            //        borPageDauS.Visibility = Visibility.Collapsed;
            //        borLui1S.Visibility = Visibility.Collapsed;
            //        borPage1S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            //        textPage1S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            //        borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //        textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //        borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //        textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //        borLen1S.Visibility = Visibility.Visible;
            //        borPageCuoiS.Visibility = Visibility.Visible;
            //        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
            //        for (int i = 0; i < 10; i++)
            //        {
            //            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
            //        }
            //        lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
            //    }
            //}
            //catch (Exception)
            //{}
        }
        private void borPage2S_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //try
            //{
            //    if (TongSoTrang == 2)
            //    {
            //        borPageDauS.Visibility = Visibility.Visible;
            //        borLui1S.Visibility = Visibility.Visible;
            //        BrushConverter brus = new BrushConverter();
            //        borPage2S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            //        textPage2S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            //        borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //        textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //        borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //        textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
            //        for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10 - 10 + (10 - SoDu); i++)
            //        {
            //            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
            //        }
            //        lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
            //    }
            //    else if (TongSoTrang > 2)
            //    {
            //        borPageDauS.Visibility = Visibility.Visible;
            //        borLui1S.Visibility = Visibility.Visible;
            //        borPageCuoiS.Visibility = Visibility.Visible;
            //        borLen1S.Visibility = Visibility.Visible;
            //        BrushConverter brus = new BrushConverter();
            //        borPage2S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            //        textPage2S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            //        borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //        textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //        borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //        textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
            //        for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
            //        {
            //            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
            //        }
            //        lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
            //    }
            //}
            //catch (Exception)
            //{}
           
        }
        private void borPage3S_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //try
            //{
            //    if (textPage3S.Text == TongSoTrang.ToString())
            //    {
            //        BrushConverter brus = new BrushConverter();
            //        borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //        textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //        borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //        textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //        borPage3S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            //        textPage3S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            //        borPageCuoiS.Visibility = Visibility.Collapsed;
            //        borLen1S.Visibility = Visibility.Collapsed;
            //        borPageDauS.Visibility = Visibility.Visible;
            //        borLui1S.Visibility = Visibility.Visible;
            //        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();

            //        for (int i = int.Parse(textPage3S.Text) * 10 - 10; i < int.Parse(textPage3S.Text) * 10 - 10 + (10 - SoDu); i++)
            //        {
            //            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
            //        }
            //        //lstLuongCB = luongCB.listResult;
            //        lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
            //    }
            //    else
            //    {
            //        if (TongSoTrang == 3)
            //        {
            //            borPageDauS.Visibility = Visibility.Visible;
            //            borLui1S.Visibility = Visibility.Visible;
            //            BrushConverter brus = new BrushConverter();
            //            borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //            textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //            borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //            textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //            borPage3S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            //            textPage3S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            //            lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
            //            for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
            //            {
            //                lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
            //            }
            //            //lstLuongCB = luongCB.listResult;
            //            lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
            //        }
            //        else if (TongSoTrang > 3)
            //        {
            //            if (textPage3S.Text == TongSoTrang.ToString())
            //            {
            //                borPageDauS.Visibility = Visibility.Visible;
            //                borLui1S.Visibility = Visibility.Visible;
            //                BrushConverter brus = new BrushConverter();
            //                borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //                textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //                borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //                textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //                borPage3S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            //                textPage3S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            //                borPageCuoiS.Visibility = Visibility.Collapsed;
            //                borLen1S.Visibility = Visibility.Collapsed;
            //                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
            //                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
            //                {
            //                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
            //                }
            //                //lstLuongCB = luongCB.listResult;
            //                lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
            //            }
            //            else if (textPage3S.Text == "3")
            //            {
            //                textPage1S.Text = "2";
            //                textPage2S.Text = "3";
            //                textPage3S.Text = "4";
            //                BrushConverter brus = new BrushConverter();
            //                borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //                textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //                borPage2S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            //                textPage2S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            //                borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //                textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
            //                for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
            //                {
            //                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
            //                }
            //                //lstLuongCB = luongCB.listResult;
            //                lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
            //            }
            //            else
            //            {
            //                textPage1S.Text = (int.Parse(textPage1S.Text) + 1).ToString();
            //                textPage2S.Text = (int.Parse(textPage2S.Text) + 1).ToString();
            //                textPage3S.Text = (int.Parse(textPage3S.Text) + 1).ToString();
            //                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
            //                for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
            //                {
            //                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
            //                }
            //                //lstLuongCB = luongCB.listResult;
            //                lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
            //            }

            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //}
        }
        private void borLen1S_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {}
        private void borPageCuoiS_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //try
            //{
            //    borPageDauS.Visibility = Visibility.Visible;
            //    borLui1S.Visibility = Visibility.Visible;
            //    borPageCuoiS.Visibility = Visibility.Collapsed;
            //    borLen1S.Visibility = Visibility.Collapsed;
            //    if (TongSoTrang >= 3)
            //    {
            //        textPage1S.Text = (TongSoTrang - 2).ToString();
            //        textPage2S.Text = (TongSoTrang - 1).ToString();
            //        textPage3S.Text = TongSoTrang.ToString();
            //        BrushConverter brus = new BrushConverter();
            //        borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //        textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //        borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //        textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //        borPage3S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            //        textPage3S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            //        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
            //        for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - 10 + (10 - SoDu); i++)
            //        {
            //            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
            //        }
            //        //lstLuongCB = luongCB.listResult;
            //        lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
            //    }

            //    else
            //    {
            //        textPage1S.Text = (TongSoTrang - 1).ToString();
            //        textPage2S.Text = TongSoTrang.ToString();
            //        //textPage3S.Text = TongSoTrang.ToString();
            //        BrushConverter brus = new BrushConverter();
            //        borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
            //        textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
            //        borPage2S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            //        textPage2S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            //        //borPage3S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            //        //textPage3S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            //        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
            //        for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
            //        {
            //            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
            //        }
            //        //lstLuongCB = luongCB.listResult;
            //        lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
            //    }
            //}
            //catch (Exception)
            //{
            //}
            
        }
        private void btnExPortExcel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string filePath = "";
                //SaveFileDiaLog lưu file Excel
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                //Đọc ra các file có định dạng excel
                saveFileDialog.Filter = "Excel | *.xlsx | Excel 2016 | *.xls";
                //Lưu đường dẫn file 
                if (saveFileDialog.ShowDialog() == true){filePath = saveFileDialog.FileName;}
                //File rỗng 
                if (string.IsNullOrEmpty(filePath)){ MessageBox.Show("Đường dẫn không hợp lệ");return;}
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                try
                {
                    using (ExcelPackage p = new ExcelPackage())
                    {
                        // đặt tên người tạo file
                        p.Workbook.Properties.Author = "QuanLyChung365TruocDangNhap.ChamCongNew";
                        // đặt tiêu đề cho file
                        p.Workbook.Properties.Title = "Danh sách thưởng phạt";
                        //Tạo một sheet để làm việc trên đó
                        p.Workbook.Worksheets.Add("sheet ExportWork");
                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet ws = p.Workbook.Worksheets[0];
                        // đặt tên cho sheet
                        ws.Name = $"Danh sách 1";
                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = 12;
                        // font family mặc định cho cả sheet
                        ws.Cells.Style.Font.Name = "Calibri";
                        // Tạo danh sách các column header
                        string[] arrColumnHeader = {
                            "Mã Nv", "Họ tên","Thưởng", "Phạt"
                        };
                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();
                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge 
                        ws.Cells[1, 1].Value = $"Danh sách thưởng phạt tháng {cbo_ChonThang.Text.Split(' ')[1]}-{cbo_ChonNam.Text.Split(' ')[1]}";
                        ws.Cells[1, 1, 1, countColHeader].Merge = true;
                        // in đậm
                        ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                        // căn giữa
                        ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        int colIndex = 1;
                        int rowIndex = 2;
                        //tạo các header từ column header đã tạo từ bên trên
                        foreach (var item in arrColumnHeader)
                        {
                            var cell = ws.Cells[rowIndex, colIndex];
                            //set màu thành gray
                            var fill = cell.Style.Fill; fill.PatternType = ExcelFillStyle.Solid;
                            fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                            //căn chỉnh các border
                            var border = cell.Style.Border;
                            border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                            //gán giá trị
                            cell.Value = item; colIndex++;
                        }
                        //lấy ra danh sách UserInfo từ ItemSource của ListView
                        List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal> userList = lstChuaTL.Cast<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>().ToList();
                        //với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in userList)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;
                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;
                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item.inforUser.idQLC;
                            ws.Cells[rowIndex, colIndex++].Value = item.inforUser.userName;
                            ws.Cells[rowIndex, colIndex++].Value = "Thưởng: " + item.tt_thuong.tong_thuong + " VNĐ";
                            ws.Cells[rowIndex, colIndex++].Value = "Phạt: " + item.tt_phat.tong_phat + " VNĐ";    
                        }
                        //Lưu file lại
                        Byte[] bin = p.GetAsByteArray();
                        File.WriteAllBytes(filePath, bin);
                    }
                    Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, this));
                }
                catch (Exception)
                {
                    ErrorSytem = "Error";
                    Main.grShowPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
                }

                if (lstChuaTL == null)
                {
                    Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, this, lstChuaTL));
                    return;
                }
            }
            catch (Exception)
            {
                ErrorSytem = "Error";
                Main.grShowPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
            }
        }
        string ErrorSytem;
        private void bod_ThoatChiTietPhatNV_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borChiTietPhatNV.Visibility = Visibility.Collapsed;
        }
        private void bod_ThoatChiTietThuongNV_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borChiTietThuongNV.Visibility = Visibility.Collapsed;
        }

        private void bod_ThoatChiTietPhatCongNV_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            borChiTietPhatCong.Visibility = Visibility.Collapsed;
        }

        private void btnThemMoiLoaiTP_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void btn_ThemMoiTP_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try { Main.grShowPopup.Children.Add(new PopUpAddTP(Main, this, lstChuaTL)); } catch (Exception) { }
        }

  
        private void btnThemMoiTP_MouseEnter(object sender, MouseEventArgs e)
        {
           
        }

        private void btnThemMoiTP_MouseLeave(object sender, MouseEventArgs e)
        {
           
        }

        private void btnThemMoiTP_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (btnThemMoiLoaiTP.Visibility == Visibility.Collapsed)
            {
                btnThemMoiLoaiTP.Visibility = Visibility.Visible;
            }
            else
            {
                btnThemMoiLoaiTP.Visibility = Visibility.Collapsed;
            }
        }
        private void btnThemMoiLoaiTP_MouseEnter(object sender, MouseEventArgs e)
        {
           btnThemMoiLoaiTP.Visibility = Visibility.Visible;
        }

        private void btnThemMoiLoaiTP_MouseLeave(object sender, MouseEventArgs e)
        {
            btnThemMoiLoaiTP.Visibility = Visibility.Collapsed;
        }
        private void StartCountdown()
        {
            countdownTimer = new DispatcherTimer();
            countdownTimer.Interval = TimeSpan.FromSeconds(1);
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownTimer.Start();
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            countdownValue--;
            if (countdownValue < 0)
            {
                countdownTimer.Stop();
                if (allowButton == false)
                {
                    btnThemMoiLoaiTP.Visibility = Visibility.Collapsed;
                }
                CountdownFinished?.Invoke(this, EventArgs.Empty);
            }

        }
        BrushConverter br = new BrushConverter();
        bool allowButton = false;
        private void btn_ThemMoiTP_MouseEnter(object sender, MouseEventArgs e)
        {
           
            tb_ThemThuongPhat.Foreground = (Brush)br.ConvertFrom("#4c5bd4");
        }

        private void btn_ThemMoiTP_MouseLeave(object sender, MouseEventArgs e)
        {
            
            tb_ThemThuongPhat.Foreground = (Brush)br.ConvertFrom("#474747");
        }

        private void btn_ThemPhatCong_MouseEnter(object sender, MouseEventArgs e)
        {
            tb_ThemphatCong.Foreground = (Brush)br.ConvertFrom("#4c5bd4");
        }

        private void btn_ThemPhatCong_MouseLeave(object sender, MouseEventArgs e)
        {
            tb_ThemphatCong.Foreground = (Brush)br.ConvertFrom("#474747");
        }

        private void btnThemMoiTP_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {

        }

       
    }
}
