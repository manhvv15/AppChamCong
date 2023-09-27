using ChamCong365.TimeKeeping;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.CaiDatLuong.ChinhSachBaoHiem
{

    /// <summary>
    /// Interaction logic for ucAddSaffToGroundInsurance.xaml
    /// </summary>
    public partial class ucThemNhanVienVaoNhomBH : UserControl
    {
        List<Saff> saffs = new List<Saff>();
        BrushConverter br = new BrushConverter();
        public ucThemNhanVienVaoNhomBH()
        {
            InitializeComponent();
            saffs.Add(new Saff() { IdSaff = 10001, ImageSaff = "./Resource/image/KyTo.jpg", NameSaff = "KyTo01"});
            saffs.Add(new Saff() { IdSaff = 10002, ImageSaff = "./Resource/image/KyTo.jpg", NameSaff = "KyTo01" });
            saffs.Add(new Saff() { IdSaff = 10003, ImageSaff = "./Resource/image/KyTo.jpg", NameSaff = "KyTo01" });
            saffs.Add(new Saff() { IdSaff = 10004, ImageSaff = "./Resource/image/KyTo.jpg", NameSaff = "KyTo01" });
            saffs.Add(new Saff() { IdSaff = 10005, ImageSaff = "./Resource/image/KyTo.jpg", NameSaff = "KyTo01" });
            saffs.Add(new Saff() { IdSaff = 10006, ImageSaff = "./Resource/image/KyTo.jpg", NameSaff = "KyTo01" });
            saffs.Add(new Saff() { IdSaff = 10001, ImageSaff = "./Resource/image/KyTo.jpg", NameSaff = "KyTo01" });
            saffs.Add(new Saff() { IdSaff = 10001, ImageSaff = "./Resource/image/KyTo.jpg", NameSaff = "KyTo01" });
            saffs.Add(new Saff() { IdSaff = 10001, ImageSaff = "./Resource/image/KyTo.jpg", NameSaff = "KyTo01" });
            saffs.Add(new Saff() { IdSaff = 10001, ImageSaff = "./Resource/image/KyTo.jpg", NameSaff = "KyTo01" });
            saffs.Add(new Saff() { IdSaff = 10001, ImageSaff = "./Resource/image/KyTo.jpg", NameSaff = "KyTo01" });
            lsvListSaff.ItemsSource = saffs;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ExitCreateSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility=System.Windows.Visibility.Collapsed;    
        }
    }
}
