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

namespace ChamCong365.NhanVien.LichSu.Function
{
    /// <summary>
    /// Interaction logic for KhuonMatError.xaml
    /// </summary>
    public partial class KhuonMatError : Page
    {

        MainChamCong Main;
        public KhuonMatError(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
        }
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        public static readonly DependencyProperty MessageProperty =
           DependencyProperty.Register("Message", typeof(string), typeof(KhuonMatError));
        public string Message2
        {
            get { return (string)GetValue(Message2Property); }
            set { SetValue(Message2Property, value); }
        }
        public static readonly DependencyProperty Message2Property =
            DependencyProperty.Register("Message2", typeof(string), typeof(KhuonMatError));
        public UpdateKhuonMat UpdateKhuonMat { get; set; }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            chamCong uc = new chamCong(Main);
            // CapNhatKhuonMat uc = new CapNhatKhuonMat(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void Border_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            //CapNhatKhuonMat uc = new CapNhatKhuonMat(Main);
            //Main.dopBody.Children.Clear();
            //object Content = uc.Content;
            //uc.Content = null;
            //Main.dopBody.Children.Add(Content as UIElement);
        }
    }
}
