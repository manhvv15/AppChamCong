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

namespace ChamCong365.Popup.DeXuat.LoaiHinhDuyetPhep
{
    /// <summary>
    /// Interaction logic for PopUpChonNhanVien.xaml
    /// </summary>
    public partial class PopUpChonNhanVien : UserControl
    {
        public class QNC
        {
            public int ID { get; set; }
            public string HoVaTen { get; set; }
            public string PhongBan { get; set; }
            public string Chucvu { get; set; }
        }
        private List<QNC> lstNC = new List<QNC>();
        private MainWindow Main;
        public PopUpChonNhanVien(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            LoadDLPB();
            LoadDLNhanVien();
        }

        private void LoadDLNhanVien()
        {
            
        }

        private void LoadDLPB()
        {
            foreach(var item in Main.lstPhongBanThuocCongTy)
            {
                cboPhongBan.Items.Add(item.dep_id + "-" + item.dep_name);
            }
            //lstNC.Add(new QNC { ID = 11, HoVaTen = "Đỗ Văn Hoàng", PhongBan = "Kỹ Thuật", Chucvu = "Giám đốc" });
            //lstNC.Add(new QNC { ID = 22, HoVaTen = "Đỗ Văn Hoàng", PhongBan = "Kỹ Thuật", Chucvu = "Giám đốc" });

            //lstNC.Add(new QNC { ID = 33, HoVaTen = "Đỗ Văn Hoàng", PhongBan = "Kỹ Thuật", Chucvu = "Giám đốc" });

            //lstNC.Add(new QNC { ID = 44, HoVaTen = "Đỗ Văn Hoàng", PhongBan = "Kỹ Thuật", Chucvu = "Giám đốc" });
            //lstNC.Add(new QNC { ID = 55, HoVaTen = "Đỗ Văn Hoàng", PhongBan = "Kỹ Thuật", Chucvu = "Giám đốc" });
            //lsvThuongPhat.ItemsSource = lstNC;

        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void lsvThuongPhat_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }

        private void borHienThiNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borNhanVien.Visibility == Visibility.Collapsed)
            {
                borNhanVien.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borNhanVien.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }

        private void popup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borNhanVien.Visibility = Visibility.Collapsed;
            popup.Visibility = Visibility.Collapsed;
        }
    }
}
