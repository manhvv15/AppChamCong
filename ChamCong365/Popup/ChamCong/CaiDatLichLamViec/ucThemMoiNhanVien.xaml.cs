using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChamCong365.APIs;
using ChamCong365.OOP.ChamCong.CaiDatLichLamViec;
using ChamCong365.TimeKeeping;
using Newtonsoft.Json;

namespace ChamCong365.Popup.ChamCong.CaiDatLichLamViec
{
    /// <summary>
    /// Interaction logic for ucCreateSaff.xaml
    /// </summary>
    /// 
    public partial class ucThemMoiNhanVien : UserControl
    {
        MainWindow Main;
        public string month;
        public string year;
        public int Cy_Id;
        public string AppLy_Month;
        public int pageSize = 10000;
        ucCaiDatLichLamViec ucCaiDatLichLamViec;
        public ucDanhSachNhanVien ucNv;
        private List<ItemAllSaff> _lstAllSaff;
        public List<ItemAllSaff> lstAllSaff
        {
            get { return _lstAllSaff; }
            set { _lstAllSaff = value;}
        }

        private List<ItemAllSaff> _lstAllSaff1;
        public List<ItemAllSaff> lstAllSaff1
        {
            get { return _lstAllSaff1; }
            set { _lstAllSaff1 = value; }
        }
        ucCaiDatLichLamViec ucSetting;
        public ucThemMoiNhanVien(MainWindow main, int id, ucCaiDatLichLamViec ucSetting, string apply_Month)
        {
            InitializeComponent();
           this.DataContext = this;
            Main = main;
            Cy_Id = id;
            AppLy_Month = apply_Month;
            this.ucSetting = ucSetting;
            LoadListSaff(); 
        }

        #region Call Api AddSaffinCalendarWork
         public async void LoadListSaff()
         {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.List_Saff_Api);
                request.Headers.Add("Authorization", "Bearer "+ Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                content.Add(new StringContent(pageSize.ToString()), "pageSize");
                content.Add(new StringContent("1"), "pageNumber");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resContent = await response.Content.ReadAsStringAsync();
                RootAllSaff result = JsonConvert.DeserializeObject<RootAllSaff>(resContent);
               if (result.data.items != null) 
               {

                    lstAllSaff = lstAllSaff1 = result.data.items;
                    lsvListSaff.ItemsSource = lstAllSaff;

                }
                foreach (ItemAllSaff item in lstAllSaff)
                {
                    if (item.ep_image == "")
                    {
                        item.ep_image = "https://tinhluong.timviec365.vn/img/add.png";
                    }
                    else
                    {
                        item.ep_image = "https://chamcong.24hpay.vn/upload/employee/" + item.ep_image;
                    }
                }
            }
            catch (Exception)
            {

               
            }
        }

         private List<string> saff = new List<string>();
        private void ChonNhanVien_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            ItemAllSaff data = (ItemAllSaff)cb.DataContext;
            saff.Remove(data.ep_id.ToString());
        }

        private void ChonNhanVien_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            ItemAllSaff data = (ItemAllSaff)cb.DataContext;
            saff.Add(data.ep_id.ToString());

           
        }
            
        private void tbSearchSaff_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstAllSaff1 = new List<ItemAllSaff>();
            foreach (var str in lstAllSaff)
            {
                if (str.ep_name.ToLower().RemoveUnicode().Contains(tbSearchSaff.Text.ToLower().RemoveUnicode()))
                {
                    lstAllSaff1.Add(str);

                }
            }
            lsvListSaff.ItemsSource = lstAllSaff1;
            if (tbSearchSaff.Text == "")
            {
                lsvListSaff.ItemsSource = lstAllSaff;
            }
        }
        #endregion
        private void ExitCreateSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            ucCaiDatLichLamViec ucC = new ucCaiDatLichLamViec(Main,0);
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            ucCaiDatLichLamViec ucC = new ucCaiDatLichLamViec(Main,0);
            
        }

        private void bodButonAddFileSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemFileNhanVien());
            this.Visibility = Visibility.Collapsed;
        }

        private void bodExitCreateSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }

        public string Arr_Id_Ep;
        string formatMonth;
        private async void ThemNhanVienVaoLich(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.Add_Saff_In_CalendarWork_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                int demSaff = 0;
                foreach (var item in saff)
                {
                    if (demSaff == saff.Count - 1)
                    {
                        Arr_Id_Ep += item.ToString();
                    }
                    else
                    {
                        Arr_Id_Ep += item.ToString() + ",";
                    }

                    demSaff++;
                }
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Arr_Id_Ep), "list_id");
                content.Add(new StringContent(Cy_Id.ToString()), "cy_id");
                string[] past = AppLy_Month.Split('/');
               
                if (past != null && past.Length == 2)
                {
                    month = past[0];
                    year = past[1];
                    formatMonth = $"{year}-{month}";
                }
                content.Add(new StringContent(formatMonth), "curMonth");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responsContent = await response.Content.ReadAsStringAsync();

                RootAddSaffInCalendarWork api = JsonConvert.DeserializeObject<RootAddSaffInCalendarWork>(responsContent);

                if (responsContent != null)
                {
                    this.Visibility = Visibility.Collapsed;
                    month = ucSetting.txbSelectMonth.Text.Split()[1];
                    year = ucSetting.txbSelectYear.Text.Split()[1];
                    ucSetting.LoadCalendarWorkStart(month,year);
                }
            }
            catch (Exception)
            {}
        }
    }
}
