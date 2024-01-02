using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatThue
{
    /// <summary>
    /// Interaction logic for PopUpAddStaffInTax.xaml
    /// </summary>
    public partial class PopUpAddStaffInTax : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private List<OOP.clsNhanVienThuocCongTy.ListUser> _lstNhanVienThuocCongTy = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        public List<OOP.clsNhanVienThuocCongTy.ListUser> lstNhanVien
        {
            get { return _lstNhanVienThuocCongTy; }
            set { _lstNhanVienThuocCongTy = value; OnPropertyChanged(); }
        }
        BrushConverter br = new BrushConverter();
        MainWindow Main;
        private string IdNV = "";
        private string TenNV = "";
        private string AnhNV = "";
        private int IdThue;
        private List<OOP.clsNhanVienThuocCongTy.ListUser> lstNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        private List<OOP.clsNhanVienThuocCongTy.ListUser> lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        List<OOP.clsNhanVienThuocCongTy.ListUser> listUsersNext = new List<OOP.clsNhanVienThuocCongTy.ListUser>();

        public PopUpAddStaffInTax(MainWindow main, int IdT)
        {
            InitializeComponent();
            Main = main;
            IdThue = IdT;
            //foreach (var item in main.lstNhanVienThuocCongTy)
            //{
            //    item.isSelected = "False";
            //}
            bodSelectSaff.BorderThickness = new Thickness(0, 0, 0, 3);
            bodSelectSaff.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            txbSaff.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
            foreach (var item in main.lstNhanVienThuocCongTy)
            {
                if (item._id != 0)
                {
                    if (string.IsNullOrEmpty(item.AvatarUser))
                        item.AvatarUser = "https://hungha365.com/_next/image?url=%2Favt_365.png&w=48&q=75";
                    item.isSelected = "False";
                }
            }
            Main.lstNhanVienThuocCongTy.RemoveAt(0);
            lstNhanVien = main.lstNhanVienThuocCongTy.ToList();
            //
            //foreach (var item in main.lstNhanVienThuocCongTy)
            //{
            //    if (item._id != 0)
            //    {
            //        //= Main.lstNhanVienThuocCongTy.ToList();
            //        lstNhanVien.Add(item);
            //    }
            //}
            //lstNhanVien = lstNV;

        }
        public void loadNV()
        {
           
            lsvListSaff.ItemsSource = Main.lstNhanVienThuocCongTy.ToList();
            
        }
        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OOP.clsNhanVienThuocCongTy.ListUser us = (sender as DockPanel).DataContext as OOP.clsNhanVienThuocCongTy.ListUser;
                us.isSelected = us.isSelected == "True" ? "False" : "True";
                if (us.idQLC == 0)
                {
                    foreach (var item in lstNhanVien)
                    {
                        item.isSelected = us.isSelected;
                    }
                }
                else
                {
                    //if (us.isSelected == "False" && lstNhanVien.Find(x => x.idQLC == 0).isSelected == "True")
                    //    lstNhanVien.Find(x => x.idQLC == 0).isSelected = "False";
                    if (us.isSelected == "True" && lstNhanVien.Find(x => x.isSelected == "False" && x.idQLC != 0) == null)
                        lstNhanVien.Find(x => x.idQLC == 0).isSelected = "True";
                }
            }
            catch (Exception)
            {
            }
            
        }
        private void bodNextSaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool allow = true;
                foreach (var item in Main.lstNhanVienThuocCongTy)
                {
                    if (item.isSelected == "True")
                        listUsersNext.Add(item);
                }
                if (listUsersNext.Count == 0)
                {
                    tb_ValidateChonNhanVien.Visibility = Visibility.Visible;
                    tb_ValidateChonNhanVien.Text = "Bạn vui lòng chọn nhân viên!";
                    allow = false;
                }
                else
                {
                    Main.grShowPopup.Children.Add(new PopUpTGApDung(Main, this, IdNV, IdThue, TenNV, AnhNV, listUsersNext));
                    tb_ValidateChonNhanVien.Visibility = Visibility.Collapsed;
                    this.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception)
            {
            }
        }
        private void ChonNhanvien(object sender, RoutedEventArgs e)
        {
            //CheckBox cb = sender as CheckBox;
            //OOP.clsNhanVienThuocCongTy.ListUser data = (OOP.clsNhanVienThuocCongTy.ListUser)cb.DataContext;
            //data.isChecked = true;
        }

        private void HuyCHon(object sender, RoutedEventArgs e)
        {
            //CheckBox cb = sender as CheckBox;
            //OOP.clsNhanVienThuocCongTy.ListUser data = (OOP.clsNhanVienThuocCongTy.ListUser)cb.DataContext;
            //data.isChecked = false;
        }
        private void textTuCanTim_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
            foreach (var str in Main.lstNhanVienThuocCongTy)
            {
                if (str.userName.ToLower().RemoveUnicode().Contains(textTuCanTim.Text.ToLower().RemoveUnicode()) || str.idQLC.ToString().Contains(textTuCanTim.Text.ToString()))
                {
                    lstSearchNV.Add(str);

                }
            }
            lsvListSaff.ItemsSource = lstSearchNV;
            if (textTuCanTim.Text == "")
            {
                lsvListSaff.ItemsSource = Main.lstNhanVienThuocCongTy;
            }
        }


        private void bod_Closed_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void ExitCreateSaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }
    }
}
