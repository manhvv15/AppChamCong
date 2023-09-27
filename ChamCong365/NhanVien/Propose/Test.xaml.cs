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
using System.Windows.Shapes;
using static ChamCong365.OOP.NhanVien.ToiGuiDi.DeXuatToiGuiDi;

namespace ChamCong365.NhanVien.Propose
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        List<string> data;
        public Test()
        {
            InitializeComponent();
           
           // data = new List<string>() { "HowKteam.com", "Free education", "Share tobe better", "K9" };
            //lsvList.ItemsSource = data;
        }
       
    }
}
