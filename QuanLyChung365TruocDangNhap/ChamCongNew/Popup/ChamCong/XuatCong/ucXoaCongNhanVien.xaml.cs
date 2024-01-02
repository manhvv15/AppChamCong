
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.XuatCong;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.XuatCong.XuatCongEntities;
//using static ClosedXML.Excel.XLPredefinedFormat;


namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.XuatCong
{
    /// <summary>
    /// Interaction logic for ucThietLapNhanVien.xaml
    /// </summary>
    public class RootShiftInDay
    {
        public List<Shift> list { get; set; }
    }
    public class Shift
    {
        public int shift_id { get; set; }
        public string shift_name { get; set; }
    }
    public partial class ucXoaCongNhanVien : UserControl
    {
        BrushConverter br = new BrushConverter();
        //List<XuatCongEntities.Shift> ListOrganize;

        ucXuatCongMoi ucXuatCongMoi;
        int ep_id = 0;
        string ep_name;
        public ucXoaCongNhanVien()
        {
            InitializeComponent();
        }
        string at_time;
        public ucXoaCongNhanVien(ucXuatCongMoi ucXuatCongMoi, XuatCongEntities.DataXuatCong data)
        {
            InitializeComponent();

            this.ucXuatCongMoi = ucXuatCongMoi;

            this.ep_id = data.ep_id.Value;
            this.txbEmployeeName.Text = ep_name;
            at_time = data.ts_date.ToString();
            KeepTimeDate.SelectedDate = data.ts_date;
            KeepTimeDate.Part_TextBox.Text = data.ts_date?.ToString("dd/MM/yyyy");
            List<DataXuatCong> list = new List<DataXuatCong>();
            list.Add(data);
            cbxOrganize.ItemsSource = list;
            //GetListShiftInDay();


        }

        //private async void GetListShiftInDay()
        //{
        //    var client = new HttpClient();
        //    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/vanthu/dexuat/empShiftInDay");
        //    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
        //    var content = new MultipartFormDataContent();
        //    content.Add(new StringContent("10019859"), "ep_id");
        //    content.Add(new StringContent(KeepTimeDate.SelectedDate.Value.ToString("yyyy-MM-dd")), "day");
        //    request.Content = content;
        //    var response = await client.SendAsync(request);
        //    var responseContent = await response.Content.ReadAsStringAsync();
        //    if (response.IsSuccessStatusCode)
        //    {
        //        RootShiftInDay result = JsonConvert.DeserializeObject<RootShiftInDay>(responseContent);
        //        cbxOrganize.ItemsSource = result.list;
        //    }

        //}
        private async void XoaCong()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/delete_cong");

                var content = new StringContent("{\"ep_id\":" + ep_id + ",\"at_time\":\"" + KeepTimeDate.SelectedDate?.ToString("yyyy-MM-dd") + "\",\"shift_id\":" + int.Parse(cbxOrganize.SelectedValue.ToString()) + ",\"token\": \"" + Properties.Settings.Default.Token + "\"}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();

                this.Visibility = Visibility.Collapsed;
                ucXuatCongMoi.GetListDataChamCong();
                CustomMessageBox.Show("Thành công");


            }
            catch { }
        }
        private void cbxOrganize_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cbxOrganize.SelectedIndex = -1;
                string textSearch = cbxOrganize.Text;
                cbxOrganize.Items.Refresh();
                //cbxOrganize.ItemsSource = ListOrganize.Where(t => t.shift_name.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }
        private void cbxOrganize_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxOrganize.SelectedIndex = -1;
            string textSearch = cbxOrganize.Text + e.Text;
            cbxOrganize.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cbxOrganize.Text = "";
                cbxOrganize.Items.Refresh();
                //cbxOrganize.ItemsSource = ListOrganize;
                cbxOrganize.SelectedIndex = -1;
            }
            else
            {
                cbxOrganize.ItemsSource = "";
                cbxOrganize.Items.Refresh();
                //cbxOrganize.ItemsSource = ListOrganize.Where(t => t.shift_name.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }
        private void Rectangel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodExitCreateWifi_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        bool Validate()
        {

            return true;
        }

        private void Luu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Validate())
            {
                XoaCong();
            }
        }
    }
}
