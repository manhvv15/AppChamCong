using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using static QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.MainTestxaml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien
{
    /// <summary>
    /// Interaction logic for MainTestxaml.xaml
    /// </summary>
    public partial class MainTestxaml : Window
    {
        public MainTestxaml()
        {
            InitializeComponent();
            //getInfor();
        }

        private class VatChat
        {
            public int stt;
            public string hu;
        }
        private ObservableCollection<VatChat> listInfor = new ObservableCollection<VatChat>();
        int stt = 1; // Số thứ tự ban đầu
        public class ItemData
        {
            public int STT { get; set; }
            public string Ten { get; set; }
            public int SoLuong { get; set; }
        }
        ObservableCollection<ItemData> itemList = new ObservableCollection<ItemData>();
        private void getInfor()
        {
            int stt = 1;
            VatChat infor = new VatChat()
            {
                hu = "he"
            };
            infor.stt = stt + 1;
            listInfor.Add(infor);
            //dgv.ItemsSource = listInfor;

        }
        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            getInfor();
        }
    }
}
