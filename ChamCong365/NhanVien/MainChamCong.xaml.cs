
using ChamCong365.Login;
using ChamCong365.NhanVien.ChamCongBangQRRR.Function;
using ChamCong365.NhanVien.ChamCongBangTaiKhoanCongTy.Function;
using ChamCong365.NhanVien.ChamCongKhuonMat.Function;
using ChamCong365.NhanVien.LichSu.Function;
using ChamCong365.NhanVien.Propose;
using ChamCong365.TimeKeeping;
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

namespace ChamCong365.NhanVien
{
    /// <summary>
    /// Interaction logic for MainChamCong.xaml
    /// </summary>
    public partial class MainChamCong : Window
    {
        MainChamCong Main;
        List<Saff> saffs = new List<Saff>();
        BrushConverter bcMain = new BrushConverter();
        public int Test = 0;
        public int Back = 0;
        public int IdEp = 0;
        public MainChamCong(OOP.Login.LoginCom.Data data, ucChooseLoginOptions login)
        {
            InitializeComponent();
            try
            {
                IdEp = data.data._id;
                ChamCongBangQR ucbodyhome = new ChamCongBangQR(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);

                listChamCong uc = new listChamCong(this);
                ucbodyhome.grLoadFunctionQR.Children.Clear();
                object Content1 = uc.Content;
                uc.Content = null;
                ucbodyhome.grLoadFunctionQR.Children.Add(Content1 as UIElement);
                LoadImgEp(data);
            }
            catch
            {

            }

        }

        private void LoadImgEp(OOP.Login.LoginCom.Data dt)
        {
            var img = new Uri("https://chamcong.24hpay.vn/upload/employee/" + dt.data.user_info.com_logo);
            BitmapImage bm = new BitmapImage(img);
            ImgAvatar.ImageSource = bm;
        }

        private int i = 0;
        private void Window_SizeChangedChamCong(object sender, SizeChangedEventArgs e)
        {
            //ChamCongBangQR uc = new ChamCongBangQR(this);
            if (i == 0)
            {
                if (MainWindowChamCong.Width <= 1024)
                {

                    HearderColesspa.Visibility = Visibility.Visible;
                    HearderVisibility.Visibility = Visibility.Collapsed;

                }
                else
                {

                    HearderColesspa.Visibility = Visibility.Collapsed;
                    HearderVisibility.Visibility = Visibility.Visible;
                    MenuCollapsed.Visibility = Visibility.Collapsed;

                }
            }
           
        }

        private void MainWindows_StateChangedChamcong(object sender, EventArgs e)
        {

        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //ChamCongBangQR ucbodyhome = new ChamCongBangQR(this);
            //dopBody.Children.Clear();
            //object Content = ucbodyhome.Content;
            //ucbodyhome.Content = null;
            //dopBody.Children.Add(Content as UIElement);
            if (Back == 1)
            {
                ChamCongBangQR ucbodyhome = new ChamCongBangQR(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);

                listChamCong uc = new listChamCong(this);
                ucbodyhome.grLoadFunctionQR.Children.Clear();
                object Content1 = uc.Content;
                uc.Content = null;
                ucbodyhome.grLoadFunctionQR.Children.Add(Content1 as UIElement);
            }else if(Back == 2)
            {
                ChamCongBangQR ucbodyhome = new ChamCongBangQR(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);

                listKhuonMat uc = new listKhuonMat(this);
                ucbodyhome.grLoadFunctionQR.Children.Clear();
                object Content1 = uc.Content;
                uc.Content = null;
                ucbodyhome.grLoadFunctionQR.Children.Add(Content1 as UIElement);
            }else if(Back == 3)
            {
                ChamCongBangQR ucbodyhome = new ChamCongBangQR(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);

                listCompany uc = new listCompany(this);
                ucbodyhome.grLoadFunctionQR.Children.Clear();
                object Content1 = uc.Content;
                uc.Content = null;
                ucbodyhome.grLoadFunctionQR.Children.Add(Content1 as UIElement);
            }else if(Back == 4)
            {
                ChamCongBangQR ucbodyhome = new ChamCongBangQR(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);

                listPropose uc = new listPropose(this);
                ucbodyhome.grLoadFunctionQR.Children.Clear();
                object Content1 = uc.Content;
                uc.Content = null;
                ucbodyhome.grLoadFunctionQR.Children.Add(Content1 as UIElement);
            }else if(Back == 5)
            {
                ChamCongBangQR ucbodyhome = new ChamCongBangQR(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);
            }else if (Back == 6)
            {
                ChamCongBangQR ucbodyhome = new ChamCongBangQR(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);

                listHistory uc = new listHistory(this);
                ucbodyhome.grLoadFunctionQR.Children.Clear();
                object Content1 = uc.Content;
                uc.Content = null;
                ucbodyhome.grLoadFunctionQR.Children.Add(Content1 as UIElement);
            }
        }

        private void MainWindowChamCong_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
