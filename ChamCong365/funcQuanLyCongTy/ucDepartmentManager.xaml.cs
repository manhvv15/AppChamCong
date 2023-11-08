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
    /// Interaction logic for ucDepartmentManager.xaml
    /// </summary>
    public partial class ucDepartmentManager : UserControl
    {

        MainWindow _main;
        public ucDepartmentManager(MainWindow main)
        {
            InitializeComponent();
            this._main = main;
            
        }
        //Load thì chon tab phòng ban
        private void stackTabBody_Loaded(object sender, RoutedEventArgs e)
        {
            ucListDpartment uc = new ucListDpartment(this._main);
            stackTabBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            stackTabBody.Children.Add(Content as UIElement);
        }
        //Xử lý kích menu ngang
        private void bodDSPhongBan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetDefaultMenuColor();
            ChangeBorderColor((Border)sender);
            ucListDpartment uc = new ucListDpartment(this._main);
            stackTabBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            stackTabBody.Children.Add(Content as UIElement);

        }

        private void bodDSTo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetDefaultMenuColor();
            ChangeBorderColor((Border)sender);
            ucListTo uc = new ucListTo(this._main);
            stackTabBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            stackTabBody.Children.Add(Content as UIElement);

        }

        private void bodDSNhom_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetDefaultMenuColor();
            ChangeBorderColor((Border)sender);
            ucListGroup uc = new ucListGroup(this._main);
            stackTabBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            stackTabBody.Children.Add(Content as UIElement);

        }

        private void bodDSLuanChuyen_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetDefaultMenuColor();
            ChangeBorderColor((Border)sender);

            ucListLuanChuyen uc = new ucListLuanChuyen(this._main);
            stackTabBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            stackTabBody.Children.Add(Content as UIElement);

        }

        private void bodDSNghiViec_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetDefaultMenuColor();
            ChangeBorderColor((Border)sender);

            ucListNghiViec uc = new ucListNghiViec(this._main);
            stackTabBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            stackTabBody.Children.Add(Content as UIElement);

        }

        private void bodDSGiamBienChe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetDefaultMenuColor();
            ChangeBorderColor((Border)sender);

            ucListGiamBienChe uc = new ucListGiamBienChe(this._main);
            stackTabBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            stackTabBody.Children.Add(Content as UIElement);

        }

        ///<summary>
        /// Change color menu
        /// </summary>
        public void ChangeBorderColor(Border border)
        {
            border.BorderThickness = new Thickness(0, 0, 0, 5);
            border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
            ((TextBlock)border.Child).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
        }

        ///<summary>
        /// set default color menu
        /// </summary>
        public void SetDefaultMenuColor()
        {
            foreach(var child in GridMenu.Children)
            {
                if(child is Border)
                {
                    var border = (Border)child; 
                        border.BorderThickness = new Thickness(0, 0, 0, 1);
                       border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#474747"));
                       ((TextBlock)border.Child).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#474747"));

                }
            }
        }



    } 
}
