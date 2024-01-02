using System.Net;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Text;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ListTabInsurance;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.PopupSalarySettings
{
    /// <summary>
    /// Interaction logic for ucDeleteInsuranceSaffYestSettings.xaml
    /// </summary>
    public partial class ucXoaBaoHiemNhanSu : UserControl
    {
       BrushConverter br = new BrushConverter();
        public int cls_id;
        public ucDanhSachNhanVienDaThietLapBH DataPop { get; set; }
        public ucXoaBaoHiemNhanSu(int cls_id, ucDanhSachNhanVienDaThietLapBH DataPop)
        {
            InitializeComponent();
            this.cls_id = cls_id;
            this.DataPop = DataPop;
        }

        private void bodCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txbTextCancel.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void bodCancel_MouseEnter(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)br.ConvertFrom("#4C5BD4");
            txbTextCancel.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
          
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodCancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void txbTextCancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility=Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            using(WebClient web = new WebClient())
            {
                try
                {
                    web.QueryString.Add("cl_id", cls_id.ToString());
                    web.QueryString.Add("token", Properties.Settings.Default.Token);
                    web.UploadValuesAsync(new Uri("https://api.timviec365.vn/api/tinhluong/congty/delete_nv_insrc"), "POST", web.QueryString);
                    web.UploadValuesCompleted += (s1, e1) =>
                    {
                        var check = UTF8Encoding.UTF8.GetString(e1.Result);
                        if (check.Contains("success"))
                        {
                            /*popup.Visibility = Visibility.Hidden;
                            lstUsLs.Remove(UserSelected);
                            dgvListSaffInsuranceLst.ItemsSource = lstUsLs;
                            dgvListSaffInsuranceLst.Items.Refresh();*/
                            DataPop.LoadDLNSuDaTL();
                            DataPop.Main.grShowPopup.Children.Remove(this);
                        }
                    };
                }
                catch { }
            }
        }
    }
}
