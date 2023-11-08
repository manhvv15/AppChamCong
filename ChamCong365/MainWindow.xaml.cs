using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ChamCong365.SalarySettings;
using ChamCong365.SalarySettings.DiMuonVeSom;
using ChamCong365.TimeKeeping;
namespace ChamCong365
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       List<Saff> saffs = new List<Saff>();
        BrushConverter bcMain = new BrushConverter();
        private frmDSDiMuonVeSom frm;
        public string Nam = "";
        public string Thang = "";
        public string PhongBan = "";
        public string NhanVien = "";
        public MainWindow()
        {
            InitializeComponent();
            ucBodyHome ucbodyhome = new ucBodyHome(this);
            dopBody.Children.Clear();
            object Content = ucbodyhome.Content;
            ucbodyhome.Content = null;
            dopBody.Children.Add(Content as UIElement);
            //txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text;
            
            ucListTimeKeeping uc = new ucListTimeKeeping(this);
            ucbodyhome.grLoadFunction.Children.Clear();
            object Content1 = uc.Content;
            uc.Content = null;
            ucbodyhome.grLoadFunction.Children.Add(Content1 as UIElement);
            ucbodyhome.txbChamCong.Foreground = (Brush)bcMain.ConvertFrom("#4C5BD4");
            

        }


        private void bodBackto_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucBodyHome ucbodyhome = new ucBodyHome(this);
            dopBody.Children.Clear();
            object Content = ucbodyhome.Content;
            ucbodyhome.Content = null;
            dopBody.Children.Add(Content as UIElement);
            //txbLoadChamCong.Text = "";
            i = 0;
            LableFunction.Visibility = Visibility.Collapsed;
        }
        
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ucBasicSalary ucb = new ucBasicSalary(this);
            if (k == 0)
            {
                if (i == 0)
                {
                    if (MainWindows.Width <= 1024)
                    {

                        HearderColesspa.Visibility = Visibility.Visible;
                        HearderVisibility.Visibility = Visibility.Collapsed;
                        ucb.bodSelectRoomCollapsed.Visibility = Visibility.Visible;
                        //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;

                    }

                    else
                    {

                        HearderColesspa.Visibility = Visibility.Collapsed;
                        HearderVisibility.Visibility = Visibility.Visible;
                        MenuCollapsed.Visibility = Visibility.Collapsed;
                        //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;
                        ucb.bodSelectRoomCollapsed.Visibility = Visibility.Visible;

                    }

                }
                else if (i == 2 || i == 3)
                {
                    if (MainWindows.Width <= 865)
                    {
                        j = 3;
                        frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
                        dopBody.Children.Clear();
                        object Content = frm.Content;
                        frm.Content = null;
                        dopBody.Children.Add(Content as UIElement);
                        HearderColesspa.Visibility = Visibility.Visible;
                        HearderVisibility.Visibility = Visibility.Collapsed;
                        ucb.bodSelectRoomCollapsed.Visibility = Visibility.Visible;
                        //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;

                    }
                    else if (MainWindows.Width <= 872)
                    {
                        j = 1;
                        frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
                        dopBody.Children.Clear();
                        object Content = frm.Content;
                        frm.Content = null;
                        dopBody.Children.Add(Content as UIElement);
                        HearderColesspa.Visibility = Visibility.Visible;
                        HearderVisibility.Visibility = Visibility.Collapsed;
                        ucb.bodSelectRoomCollapsed.Visibility = Visibility.Visible;
                        //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;
                    }
                    else if (MainWindows.Width <= 1024)
                    {
                        HearderColesspa.Visibility = Visibility.Visible;
                        HearderVisibility.Visibility = Visibility.Collapsed;
                        ucb.bodSelectRoomCollapsed.Visibility = Visibility.Visible;
                        //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;

                    }
                    //else if (MainWindows.Width <= 1064)
                    //{

                    //    j = 4;
                    //    frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
                    //    dopBody.Children.Clear();
                    //    object Content = frm.Content;
                    //    frm.Content = null;
                    //    dopBody.Children.Add(Content as UIElement);
                    //    HearderColesspa.Visibility = Visibility.Visible;
                    //    HearderVisibility.Visibility = Visibility.Collapsed;
                    //    ucb.bodSelectRoomCollapsed.Visibility = Visibility.Visible;
                    //    ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;


                    //}
                    else if (MainWindows.Width <= 1138)
                    {

                        j = 1;
                        frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
                        dopBody.Children.Clear();
                        object Content = frm.Content;
                        frm.Content = null;
                        dopBody.Children.Add(Content as UIElement);
                        HearderColesspa.Visibility = Visibility.Visible;
                        HearderVisibility.Visibility = Visibility.Collapsed;
                        ucb.bodSelectRoomCollapsed.Visibility = Visibility.Visible;
                        //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;


                    }
                    else
                    {
                        j = 2;
                        frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
                        dopBody.Children.Clear();
                        object Content = frm.Content;
                        frm.Content = null;
                        dopBody.Children.Add(Content as UIElement);
                        
                        HearderColesspa.Visibility = Visibility.Collapsed;
                        HearderVisibility.Visibility = Visibility.Visible;
                        MenuCollapsed.Visibility = Visibility.Collapsed;
                        //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;
                        ucb.bodSelectRoomCollapsed.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                HearderColesspa.Visibility = Visibility.Collapsed;
                HearderVisibility.Visibility = Visibility.Visible;
                MenuCollapsed.Visibility = Visibility.Collapsed;
                //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;
                ucb.bodSelectRoomCollapsed.Visibility = Visibility.Visible;
            }
        }

        private void SlineBar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (MenuCollapsed.Visibility == Visibility.Collapsed)
            {
                MenuCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                MenuCollapsed.Visibility = Visibility.Collapsed;
            }
        }

        private void bodBackto_MouseEnter(object sender, MouseEventArgs e)
        {
            txbBackToBack.Foreground = (Brush)bcMain.ConvertFrom("#4C5BD4");
        }

        private void bodBackto_MouseLeave(object sender, MouseEventArgs e)
        {
            txbBackToBack.Foreground = (Brush)bcMain.ConvertFrom("#474747");
        }
        public int k = 0;
        public int i = 0;
        public int j = 0;
        private void MainWindows_StateChanged(object sender, EventArgs e)
        {
            MainWindow Main = new MainWindow();
            if (k == 0)
            {
                Main.WindowState = WindowState.Maximized;
                k = 1;
                if (i == 2)
                {
                    ucBasicSalary ucb = new ucBasicSalary(this);
                    if (Main.WindowState == WindowState.Maximized)
                    {
                        
                        j = 2;
                        frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
                        dopBody.Children.Clear();
                        object Content = frm.Content;
                        frm.Content = null;
                        dopBody.Children.Add(Content as UIElement);
                        HearderColesspa.Visibility = Visibility.Visible;
                        HearderVisibility.Visibility = Visibility.Collapsed;
                        ucb.bodSelectRoomCollapsed.Visibility = Visibility.Visible;
                        //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;
                    }

                    
                }
                else if (i == 3)
                {
                    ucBasicSalary ucb = new ucBasicSalary(this);
                    if (Main.WindowState == WindowState.Maximized)
                    {
                        j = 5;
                        frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
                        dopBody.Children.Clear();
                        object Content = frm.Content;
                        frm.Content = null;
                        dopBody.Children.Add(Content as UIElement);
                        HearderColesspa.Visibility = Visibility.Visible;
                        HearderVisibility.Visibility = Visibility.Collapsed;
                        ucb.bodSelectRoomCollapsed.Visibility = Visibility.Visible;
                        //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;
                    }
                }
            }
            
            else
            {
                Main.WindowState = WindowState.Normal;
                k = 0;
                //if (i == 2 || i == 3)
                //{
                //    ucBasicSalary ucb = new ucBasicSalary(this);
                //    if (MainWindows.Width <= 1008)
                //    {
                //        j = 1;
                //        frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
                //        dopBody.Children.Clear();
                //        object Content = frm.Content;
                //        frm.Content = null;
                //        dopBody.Children.Add(Content as UIElement);
                //        HearderColesspa.Visibility = Visibility.Visible;
                //        HearderVisibility.Visibility = Visibility.Collapsed;
                //        ucb.bodSelectRoomCollapsed.Visibility = Visibility.Visible;
                //        ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;

                //    }
                //    else if (MainWindows.Width <= 1024)
                //    {
                //        j = 2;
                //        HearderColesspa.Visibility = Visibility.Visible;
                //        HearderVisibility.Visibility = Visibility.Collapsed;
                //        ucb.bodSelectRoomCollapsed.Visibility = Visibility.Visible;
                //        ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;
                        

                //    }
                //    else
                //    {
                //        j = 2;
                //        frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
                //        dopBody.Children.Clear();
                //        object Content = frm.Content;
                //        frm.Content = null;
                //        dopBody.Children.Add(Content as UIElement);
                        
                //    }
                //}
                
                //i = 2;

            }
        }

        private void clearPopUp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
