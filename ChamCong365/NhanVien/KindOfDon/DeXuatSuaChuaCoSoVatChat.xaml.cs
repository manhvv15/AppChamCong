using ChamCong365.OOP.Login;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static ChamCong365.NhanVien.KindOfDon.NghiPhep;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace ChamCong365.NhanVien.KindOfDon
{
    /// <summary>
    /// Interaction logic for DeXuatSuaChuaCoSoVatChat.xaml
    /// </summary>
    public partial class DeXuatSuaChuaCoSoVatChat : UserControl
    {
        MainChamCong Main;
        private class VatChat
        {
            public int stt { get; set; }
        
        }
       // private ObservableCollection<VatChat> listInfor = new ObservableCollection<VatChat>();

        public DeXuatSuaChuaCoSoVatChat(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
        }
        ObservableCollection<VatChat> list = new ObservableCollection<VatChat>();
        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            VatChat infor = new VatChat();
            list.Add(infor);
            // infor.stt = list.Count + 1;
            infor.stt = list.IndexOf(infor) + 1;
            
            dgv.ItemsSource = list;
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            VatChat index = (VatChat)dgv.SelectedItem;
            if (index != null)
            {
                list.Remove(index);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].stt = i + 1;
                };
                dgv.ClearValue(ItemsControl.ItemsSourceProperty);
                dgv.ItemsSource = list;
            }
        }
    }
}
