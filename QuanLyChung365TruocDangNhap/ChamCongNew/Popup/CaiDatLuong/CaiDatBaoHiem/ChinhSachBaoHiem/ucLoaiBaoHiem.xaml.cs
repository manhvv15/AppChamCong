using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.PopupSalarySettings;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB.clsLuongCoBan;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.ChinhSachBaoHiem
{
    /// <summary>
    /// Interaction logic for ucInsurancePolicy.xaml
    /// </summary>
    /// 
    
   
    public partial class ucLoaiBaoHiem : Page
    {
        MainWindow Main;
        //List<Insurance> Insurances = new List<Insurance>();
        public List<OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList> lstBH = new List<OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList>();
        public ucLoaiBaoHiem(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            LoadDataBH();
            //lsvloadInsurancePolicy.ItemsSource = Insurances;
            
        }

        private void LoadDataBH()
        {
            

            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/takeinfo_insrc")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("cl_com", Main.IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.Root Bh = JsonConvert.DeserializeObject<OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.Root>(b);
                    if (Bh.tax_list != null)
                    {
                        foreach (var item in Bh.tax_list)
                        {
                            if (item.TinhluongFormSalary != null)
                            {
                                foreach (var ct in item.TinhluongFormSalary)
                                {
                                    item.calculation_formula = item.calculation_formula + " " + ct.fs_repica;
                                }
                            }
                            lstBH.Add(item);

                        }
                        lstBH.Insert(0, new OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList() { cl_id = 5 });
                        lstBH.Insert(0, new OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList() { cl_id = 4 });
                        lstBH.Insert(0, new OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList() { cl_id = 3 });
                        lsvDSBaoHiem.ItemsSource = lstBH;


                    }
                }
            }
            catch
            {

            }
        }

        private void bodMethondSalaryInput_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (bodListMethondSalaryInput.Visibility == Visibility.Collapsed)
            //{
            //    bodListMethondSalaryInput.Visibility = Visibility.Visible;
            //    bodControlSalaryInput.Visibility = Visibility.Visible;
            //    bodListMethondSalaryBasic.Visibility = Visibility.Collapsed;
            //    bodControlSalaryBasic.Visibility = Visibility.Collapsed;
            //    bodListMethond.Visibility = Visibility.Collapsed;
            //    bodControl.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    bodListMethondSalaryInput.Visibility = Visibility.Collapsed;
            //    bodControlSalaryBasic.Visibility = Visibility.Visible;
            //}
        }

        private void bodMethondSalaryBasic_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (bodListMethondSalaryBasic.Visibility == Visibility.Collapsed)
            //{
            //    bodListMethondSalaryBasic.Visibility = Visibility.Visible;
            //    bodControlSalaryBasic.Visibility = Visibility.Visible;
            //    bodListMethondSalaryInput.Visibility = Visibility.Collapsed;
            //    bodControlSalaryInput.Visibility = Visibility.Collapsed;
            //    bodListMethond.Visibility = Visibility.Collapsed;
            //    bodControl.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    bodListMethondSalaryBasic.Visibility = Visibility.Collapsed;
            //    bodControlSalaryBasic.Visibility = Visibility.Visible;
                
            //}
        }

        private void bodMethond_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (bodListMethond.Visibility == Visibility.Collapsed)
            //{
            //    bodListMethond.Visibility = Visibility.Visible;
            //    bodControl.Visibility = Visibility.Visible;
            //    bodListMethondSalaryInput.Visibility = Visibility.Collapsed;
            //    bodControlSalaryInput.Visibility = Visibility.Collapsed;
            //    bodListMethondSalaryBasic.Visibility = Visibility.Collapsed;
            //    bodControlSalaryBasic.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    bodListMethond.Visibility = Visibility.Collapsed;
            //    bodControl.Visibility = Visibility.Collapsed;
                
            //}
        }

        private void dopAddSaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemNhanVienBaoHiem(Main));
        }

        private void dopListSaffInsurance_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Main.grShowPopup.Children.Add(new ucDanhSachNhanVienBH(Main));
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList Bh = (sender as MenuItem).DataContext as OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList;
            if (Bh != null)
            {
                Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.CaiDatBaoHiem.ChinhSachBaoHiem.PopUpHoiTruocKhiXoaBaoHiem(Main, Bh, this));
            }
        }

        private void lsvChinhSachThue_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList Bh = (sender as MenuItem).DataContext as OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList;
            if (Bh != null)
            {
                Main.grShowPopup.Children.Add(new ucThemMoiChinhSachBH(Main, Bh, "Chỉnh sửa chính sách bảo hiểm", this));
            }
        }

        private void btnThemChinhSachBaoHiem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemMoiChinhSachBH(Main, "Thêm mới chính sách bảo hiểm", this));
            
        }

        private void btnThemNhanVien_Click(object sender, RoutedEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemNhanVienBaoHiem(Main));
        }

        private void btnDanhSachNV_Click(object sender, RoutedEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucDanhSachNhanVienBH(Main, 3, "Nhập tiền bảo hiểm"));
        }

        private void btnThemNVBHXHLuongCB_Click(object sender, RoutedEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemNhanVienBaoHiem(Main));
        }

        private void btnDanhSachNVBHXHLuongCB_Click(object sender, RoutedEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucDanhSachNhanVienBH(Main, 4, "BHXH Tính theo lương cơ bản"));
        }

        private void btnDanhSachNVBHXHTheoLuongNhap_Click(object sender, RoutedEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucDanhSachNhanVienBH(Main, 5, "BHXH Tính theo lương nhập vào"));
        }

        private void btnThemNVBHXHLuongNhap_Click(object sender, RoutedEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemNhanVienBaoHiem(Main));
        }

        private void btnDanhSachNV_Click_1(object sender, RoutedEventArgs e)
        {
            OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList Bh = (sender as MenuItem).DataContext as OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList;
            if (Bh != null)
            {
                Main.grShowPopup.Children.Add(new ucDanhSachNhanVienBH(Main, Bh.cl_id, "BHXH Tính theo lương nhập vào"));
            }
        }

        private void btnThemNV_Click(object sender, RoutedEventArgs e)
        {
            OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList Bh = (sender as MenuItem).DataContext as OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList;
            if (Bh != null)
            {
                Main.grShowPopup.Children.Add(new ucThemNhanVienBaoHiem(Main, Bh.cl_id));
            }
        }

        private void popup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            popTuyChonBaoHiem.Visibility = Visibility.Hidden;
            popup.Visibility = Visibility.Hidden;
        }
        OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList BhSelected = new OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList();
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BhSelected = (sender as Border).DataContext as OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList;
            var z = Mouse.GetPosition(Main.dopBody);
            popTuyChonBaoHiem.Margin = new Thickness(z.X - 165, z.Y - 30, 0, 0);
            popTuyChonBaoHiem.Visibility = Visibility.Visible;
            popup.Visibility = Visibility.Visible;
            if(BhSelected.cl_id == 3 || BhSelected.cl_id == 4 || BhSelected.cl_id == 5)
            {
                btChinhSua.Visibility = Visibility.Collapsed;
                btXoa.Visibility = Visibility.Collapsed;
            }
            else
            {
                btChinhSua.Visibility = Visibility.Visible;
                btXoa.Visibility = Visibility.Visible;
            }
        }

        private void themNhanVien_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (BhSelected != null)
            {
                Main.grShowPopup.Children.Add(new ucThemNhanVienBaoHiem(Main, BhSelected.cl_id));
                popTuyChonBaoHiem.Visibility = Visibility.Hidden;
                popup.Visibility = Visibility.Hidden;
            }
        }

        private void DsNhanVien_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (BhSelected != null)
            {
                Main.grShowPopup.Children.Add(new ucDanhSachNhanVienBH(Main, BhSelected.cl_id, "BHXH Tính theo lương nhập vào"));
                popTuyChonBaoHiem.Visibility = Visibility.Hidden;
                popup.Visibility = Visibility.Hidden;
            }
        }

        private void ChinhSua_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (BhSelected != null)
            {
                Main.grShowPopup.Children.Add(new ucThemMoiChinhSachBH(Main, BhSelected, "Chỉnh sửa chính sách bảo hiểm", this));
                popTuyChonBaoHiem.Visibility = Visibility.Hidden;
                popup.Visibility = Visibility.Hidden;
            }
        }

        private void Xoa_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (BhSelected != null)
            {
                Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.CaiDatBaoHiem.ChinhSachBaoHiem.PopUpHoiTruocKhiXoaBaoHiem(Main, BhSelected, this));
                popTuyChonBaoHiem.Visibility = Visibility.Hidden;
                popup.Visibility = Visibility.Hidden;
            }
        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemNhanVienBaoHiem(Main, 3));
        }
    }
}
