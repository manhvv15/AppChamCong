using ChamCong365.TimeKeeping;
using System.Net.Http;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ChamCong365.OOP.ChamCong.CaiDatLichLamViec;
using System.Collections.Generic;

namespace ChamCong365.Popup.ChamCong.CaiDatLichLamViec
{
    /// <summary>
    /// Interaction logic for ucDeleteCalendarWork.xaml
    /// </summary>
    public partial class ucXoaLichLamViec : UserControl
    {
        BrushConverter bc = new BrushConverter();
        MainWindow Main;
        public int cy_id;
        ucCaiDatLichLamViec ucSetting;
        public ucXoaLichLamViec(MainWindow main, ucCaiDatLichLamViec ucSetting,int ID)
        {
            InitializeComponent();
            Main = main;
            this.DataContext = this;
            this.cy_id = ID;
            this.ucSetting = ucSetting;
            
        }
        public async void DeleteCalendar()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Delete, APIs.API.delete_Calendar_Work);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(cy_id.ToString()), "cy_id");
                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    this.Visibility = Visibility.Collapsed;
                    string month = ucSetting.txbSelectMonth.Text.ToString().Split(' ')[1];
                    string year = ucSetting.txbSelectYear.Text.ToString().Split(' ')[1];
                    ucSetting.LoadCalendarWorkEnd(month, year);
                }

            }
            catch (System.Exception)
            {

            }
        }

        private void XacNhanXoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeleteCalendar();
        }


        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodCancel_MouseEnter(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            txbCancel.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
        }

        private void bodCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            txbCancel.Foreground = (Brush)bc.ConvertFrom("#4C5BD4");
        }



        private void XacNhanXoa_MouseEnter(object sender, MouseEventArgs e)
        {
            XacNhanXoa.BorderThickness = new Thickness(1);
            
        }

        private void XacNhanXoa_MouseLeave(object sender, MouseEventArgs e)
        {
            XacNhanXoa.BorderThickness = new Thickness(0);
        }

        private void bodCancel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
