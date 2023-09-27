
using ChamCong365.funcQuanLyCongTy;
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

namespace ChamCong365
{
    /// <summary>
    /// Interaction logic for ucFunctionCompanyManager.xaml
    /// </summary>
    public partial class ucFunctionCompanyManager : UserControl
    {
        MainWindow Main;
        ucBodyHome UcBodyHome;

        public ucFunctionCompanyManager(MainWindow main,ucBodyHome ucBodyHome)
        {
            InitializeComponent();
            this.Main = main;
            this.UcBodyHome = ucBodyHome;
            Main.LableFunction.Visibility = Visibility.Visible;
            //Main.txbLoadNameFunction.Text = UcBodyHome.txbQuanLyCongTy.Text;
        }

        private void wapbuttonWorkShiftManager_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucShiftWorkManager uc = new ucShiftWorkManager(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            //Main.txbLoadNameFunction.Text = UcBodyHome.txbQuanLyCongTy.Text + " / " + txbFunction1.Text;
        }

        private void wapChildCompanyManager_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucChildCompanyManagerment uc = new ucChildCompanyManagerment(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            //Main.txbLoadNameFunction.Text = UcBodyHome.txbQuanLyCongTy.Text + " / " + txbFunction2.Text;

        }

        private void wapDepartmentManager_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucDepartmentManager uc = new ucDepartmentManager(this.Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            //Main.txbLoadNameFunction.Text = UcBodyHome.txbQuanLyCongTy.Text + " / " + txbFunction3.Text;

        }


        private void wapCandidatesList_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucCandidatesList uc = new ucCandidatesList(this.Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            //Main.txbLoadNameFunction.Text = UcBodyHome.txbQuanLyCongTy.Text + " / " + txbFunction5.Text;

        }

        private void wapInstallNewStaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucInstallAddNewStaff uc = new ucInstallAddNewStaff(this.Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            //Main.txbLoadNameFunction.Text = UcBodyHome.txbQuanLyCongTy.Text + " / " + txbFunction4.Text;
        }
    }
}
