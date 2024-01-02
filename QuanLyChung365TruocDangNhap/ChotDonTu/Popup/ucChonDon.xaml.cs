using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static QRCoder.PayloadGenerator;
using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChotDonTu.OOP;

namespace QuanLyChung365TruocDangNhap.ChotDonTu.Popup
{
    /// <summary>
    /// Interaction logic for ucChonDon.xaml
    /// </summary>
    public partial class ucChonDon : UserControl
    {
        frmMain Main;
        ucChotDonTu ucCDT;
        public ucChonDon(frmMain main, ucChotDonTu ucCDT)
        {
            InitializeComponent();
            Main = main;
            this.ucCDT = ucCDT;

        }

        private void Border_MouseLeftButtonUp_4(object sender, MouseButtonEventArgs e)
        {
            ChotDon.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ChotDon.Visibility = Visibility.Collapsed;
        }

        private void huy(object sender, MouseButtonEventArgs e)
        {
            ChotDon.Visibility = Visibility.Collapsed;
        }

        private async void hoanThanh(object sender, MouseButtonEventArgs e)
        {
            if (cbTuDong.IsChecked == true)
            {
                borChotDon.Visibility = Visibility.Collapsed;
                ChonNgay.Visibility = Visibility.Visible;
            }
            else if (cbThuCong.IsChecked == true)
            {
                try
                {
                    if (ucCDT.listChot.Count == 0)
                    {
                        var client = new HttpClient();
                        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/chotDonTu/create");
                        request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                        var content = new MultipartFormDataContent();
                        //     content.Add(new StringContent(ucCDT.idLich.ToString()), "id");
                        content.Add(new StringContent("true"), "is_auto");
                        dateUpdate = ucCDT.year + "-" + ucCDT.month + "-" + ucCDT.ngayUpdate;
                        content.Add(new StringContent(Convert(dateUpdate).ToString()), "date_chot");
                        content.Add(new StringContent(ucCDT.month.ToString()), "thang_ap_dung");
                        content.Add(new StringContent(ucCDT.year.ToString()), "nam_ap_dung");
                        int ngayAdd = Int32.Parse(txtSo.Text);
                        string date_autoChot = ucCDT.year + "-" + ucCDT.month + "-" + (ucCDT.ngayUpdate + ngayAdd);
                        content.Add(new StringContent(Convert(dateUpdate).ToString()), "date_auto_chot");
                        content.Add(new StringContent(Main.IdAcount.ToString()), "comId");
                        request.Content = content;
                        var response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                        var responseContent = await response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            UpdateThanhCong uc = new UpdateThanhCong(Main);
                            object Content = uc.Content;
                            uc.Content = null;
                            Main.pnlShowPopUp.Children.Add(Content as UIElement);
                            ChotDon.Visibility = Visibility.Collapsed;
                            ucCDT.getList();
                        }
                    }
                    else
                    {

                        if (ucCDT.statusLich == 3)
                        {
                            var client = new HttpClient();
                            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/chotDonTu/update");
                            request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                            var content = new MultipartFormDataContent();
                            content.Add(new StringContent(ucCDT.idLich.ToString()), "id");
                            content.Add(new StringContent("true"), "is_auto");
                            dateUpdate = ucCDT.year + "-" + ucCDT.month + "-" + ucCDT.ngayUpdate;
                            content.Add(new StringContent(Convert(dateUpdate).ToString()), "date_chot");
                            content.Add(new StringContent(ucCDT.month.ToString()), "thang_ap_dung");
                            content.Add(new StringContent(ucCDT.year.ToString()), "nam_ap_dung");
                            int ngayAdd = Int32.Parse(txtSo.Text);
                            string date_autoChot = ucCDT.year + "-" + ucCDT.month + "-" + (ucCDT.ngayUpdate + ngayAdd);
                            content.Add(new StringContent(Convert(dateUpdate).ToString()), "date_auto_chot");
                            content.Add(new StringContent(Main.IdAcount.ToString()), "comId");
                            request.Content = content;
                            var response = await client.SendAsync(request);
                            response.EnsureSuccessStatusCode();
                            var responseContent = await response.Content.ReadAsStringAsync();
                            if (response.IsSuccessStatusCode)
                            {
                                UpdateThanhCong uc = new UpdateThanhCong(Main);
                                object Content = uc.Content;
                                uc.Content = null;
                                Main.pnlShowPopUp.Children.Add(Content as UIElement);
                                ChotDon.Visibility = Visibility.Collapsed;
                                ucCDT.getList();
                            }
                        }
                        else
                        {
                            Main.pnlShowPopUp.Children.Add(new Error(Main));
                            ChotDon.Visibility = Visibility.Collapsed;
                        }
                    }

                }
                catch (Exception ex)
                {
                    //Debug.Print("Lỗi: Vui lòng nhập một giá trị ngày hợp lệ.");
                    Main.pnlShowPopUp.Children.Add(new Error(Main));
                    ChotDon.Visibility = Visibility.Collapsed;

                }
            }
        }

        private void CheckBox_MouseEnter(object sender, MouseEventArgs e)
        {
            borTuDong.Visibility = Visibility.Visible;
        }

        private void cbTuDong_Checked(object sender, RoutedEventArgs e)
        {
            cbThuCong.IsChecked = false;
        }

        private void cbThuCong_Checked(object sender, RoutedEventArgs e)
        {
            cbTuDong.IsChecked = false;
        }

        private void cbTuDong_MouseLeave(object sender, MouseEventArgs e)
        {
            borTuDong.Visibility = Visibility.Collapsed;
        }

        private void cbThuCong_MouseEnter(object sender, MouseEventArgs e)
        {
            borThuCong.Visibility = Visibility.Visible;
        }

        private void cbThuCong_MouseLeave(object sender, MouseEventArgs e)
        {
            borThuCong.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            ChonNgay.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ChonNgay.Visibility = Visibility.Collapsed;
            borChotDon.Visibility = Visibility.Visible;

        }

        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            ChotDon.Visibility = Visibility.Collapsed;
        }

        private void huy1(object sender, MouseButtonEventArgs e)
        {
            ChotDon.Visibility = Visibility.Collapsed;
        }
        private string RemoveSpacesAndLetters(string text)
        {
            // Sử dụng LINQ để lọc ra các ký tự là số
            string result = new string(text.Where(char.IsDigit).ToArray());

            return result;
        }


        private void AllowOnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            // Lấy văn bản hiện tại trong vùng nhập liệu
            var textBox = (TextBox)sender;
            string currentText = textBox.Text;

            // Lưu lại vị trí của con trỏ (caret) trước khi xóa
            int caretIndex = textBox.CaretIndex;

            // Kiểm tra xem vị trí con trỏ có nằm trong biên của chuỗi không
            if (caretIndex >= 0 && caretIndex < currentText.Length)
            {
                // Xóa ký tự tại vị trí con trỏ
                currentText = currentText.Remove(caretIndex, 1);
            }

            // Kiểm tra xem ký tự mới nhập có phải là số hay không
            if (!IsNumber(e.Text) || e.Text == " ")
            {
                // Nếu không phải là số, không thêm vào vị trí con trỏ và kết thúc xử lý
                e.Handled = true;
            }
            string filteredText = RemoveSpacesAndLetters(currentText);

            // Gán văn bản đã lọc vào vùng nhập liệu
            textBox.Text = filteredText;

            // Đặt lại vị trí con trỏ sau khi đã thêm vào và lọc
            textBox.CaretIndex = caretIndex + (filteredText.Length - currentText.Length);
        }
        private bool IsNumber(string text)
        {
            return int.TryParse(text, out _);
        }
        string dateUpdate;
        private async void hoanThanh1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (ucCDT.listChot.Count == 0)
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/chotDonTu/create");
                    request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                    var content = new MultipartFormDataContent();
               //     content.Add(new StringContent(ucCDT.idLich.ToString()), "id");
                    content.Add(new StringContent("true"), "is_auto");
                    dateUpdate = ucCDT.year + "-" + ucCDT.month + "-" + ucCDT.ngayUpdate;
                    content.Add(new StringContent(Convert(dateUpdate).ToString()), "date_chot");
                    content.Add(new StringContent(ucCDT.month.ToString()), "thang_ap_dung");
                    content.Add(new StringContent(ucCDT.year.ToString()), "nam_ap_dung");
                    int ngayAdd = Int32.Parse(txtSo.Text);
                    string date_autoChot = ucCDT.year + "-" + ucCDT.month + "-" + (ucCDT.ngayUpdate + ngayAdd);
                    content.Add(new StringContent(Convert(date_autoChot).ToString()), "date_auto_chot");
                    content.Add(new StringContent(Main.IdAcount.ToString()), "comId");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        UpdateThanhCong uc = new UpdateThanhCong(Main);
                        object Content = uc.Content;
                        uc.Content = null;
                        Main.pnlShowPopUp.Children.Add(Content as UIElement);
                        ChotDon.Visibility = Visibility.Collapsed;
                        ucCDT.getList();
                    }
                }
                else
                {

                    if (ucCDT.statusLich == 3)
                    {
                        var client = new HttpClient();
                        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/chotDonTu/update");
                        request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                        var content = new MultipartFormDataContent();
                        content.Add(new StringContent(ucCDT.idLich.ToString()), "id");
                        content.Add(new StringContent("true"), "is_auto");
                        dateUpdate = ucCDT.year + "-" + ucCDT.month + "-" + ucCDT.ngayUpdate;
                        content.Add(new StringContent(Convert(dateUpdate).ToString()), "date_chot");
                        content.Add(new StringContent(ucCDT.month.ToString()), "thang_ap_dung");
                        content.Add(new StringContent(ucCDT.year.ToString()), "nam_ap_dung");
                        int ngayAdd = Int32.Parse(txtSo.Text);
                        string date_autoChot = ucCDT.year + "-" + ucCDT.month + "-" + (ucCDT.ngayUpdate + ngayAdd);
                        content.Add(new StringContent(Convert(date_autoChot).ToString()), "date_auto_chot");
                        content.Add(new StringContent(Main.IdAcount.ToString()), "comId");
                        request.Content = content;
                        var response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                        var responseContent = await response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            UpdateThanhCong uc = new UpdateThanhCong(Main);
                            object Content = uc.Content;
                            uc.Content = null;
                            Main.pnlShowPopUp.Children.Add(Content as UIElement);
                            ChotDon.Visibility = Visibility.Collapsed;
                            ucCDT.getList();
                        }
                    }
                    else
                    {
                        Main.pnlShowPopUp.Children.Add(new Error(Main));
                        ChotDon.Visibility = Visibility.Collapsed;
                    }
                }

            }
            catch (Exception ex)
            {
                //Debug.Print("Lỗi: Vui lòng nhập một giá trị ngày hợp lệ.");
                Main.pnlShowPopUp.Children.Add(new Error(Main));
                ChotDon.Visibility = Visibility.Collapsed;

            }
        }
        string dateChot1;
        private static string Convert(string dateChot1)
        {
            TimeZoneInfo originalTimeZone = TimeZoneInfo.Utc;
            if (DateTime.TryParse(dateChot1, out DateTime inputDate))
            {
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(inputDate, originalTimeZone);
                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                DateTime vnTime = TimeZoneInfo.ConvertTime(localTime, vnTimeZone);
                dateChot1 = vnTime.ToString("yyyy-MM-dd HH:mm:ss");

            }
            return dateChot1;

        }
        public string GetNgay1()
        {
            return dateChot1;
        }
        int y = 0;
        private void Border_aMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string x = txtSo.Text;
            int intValue = int.Parse(x);

            if (intValue > 1)
            {
                y = intValue - 1;
            }

            txtSo.Text = y.ToString();

        }

        private void Bordear_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            string x = txtSo.Text;
            int intValue = int.Parse(x);
            // int y = 0;
            //y = 
            y = intValue + 1;

            txtSo.Text = y.ToString();
        }
    }
}
