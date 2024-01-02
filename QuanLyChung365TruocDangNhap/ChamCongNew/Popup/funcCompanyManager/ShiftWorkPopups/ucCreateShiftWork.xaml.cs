using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.Common;
//using DocumentFormat.OpenXml;
//using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager
{
    /// <summary>
    /// Interaction logic for ucCreateShiftWork.xaml
    /// </summary>
    public partial class ucCreateShiftWork : UserControl
    {
        public class CongTrenCa
        {
            public string Cong_ca { get; set; }
        }
        List<CongTrenCa> lstCongCa = new List<CongTrenCa>();
        ucShiftWorkManager ucShiftWorkManager;
        BrushConverter bc = new BrushConverter();
        List<double> dsCongTrenCa = new List<double>() { 0.25, 0.5, 1, 3 };
        MainWindow Main;
        private string com_id = "";
        private string shift_type = "1";
        public ucCreateShiftWork(MainWindow main ,ucShiftWorkManager ucShiftWorkManager, string com_id)
        {
            InitializeComponent();
            this.Main = main;   
            lstCongCa.Add(new CongTrenCa { Cong_ca = "0.25" });
            lstCongCa.Add(new CongTrenCa { Cong_ca = "0.5" });
            lstCongCa.Add(new CongTrenCa { Cong_ca = "1" });
            lstCongCa.Add(new CongTrenCa { Cong_ca = "3" });
            lsvCongTrenCa.ItemsSource = lstCongCa;
            this.ucShiftWorkManager = ucShiftWorkManager;
            this.com_id = com_id;
            OnOff = 1;
            btnOnOff.HorizontalAlignment = HorizontalAlignment.Left;
            bodBackOnOff.Background = (Brush)bc.ConvertFrom("#ECECEC");
            btnOnOffNoEndDate.HorizontalAlignment = HorizontalAlignment.Left;
            bodBackOnOffNoEndDate.Background = (Brush)bc.ConvertFrom("#ECECEC");
        }

        #region Call Api

        string ErrorSytem;
        private async void AddNewShift()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.create_shift_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(txtShiftName.Text), "shift_name");
                if (End_Date_Type == 1)
                {
                    content.Add(new StringContent(End_Date_Type.ToString()), "type_end_date");
                    content.Add(new StringContent(cbo_SelectTimeCheckIn.Time), "start_time");
                    if (cbo_SelectCheckInEarliest.Time != null)
                    {
                        content.Add(new StringContent(cbo_SelectCheckInEarliest.Time), "start_time_latest");
                    }
                    else
                    {
                        content.Add(new StringContent(""), "start_time_latest");
                    }
                }
                else
                {
                    content.Add(new StringContent(cbo_SelectTimeCheckIn.Time), "start_time");
                    content.Add(new StringContent(cbo_SelectTimeCheckOut.Time), "end_time");
                    if (cbo_SelectCheckInEarliest.Time != null)
                    {
                        content.Add(new StringContent(cbo_SelectCheckInEarliest.Time), "start_time_latest");
                    }
                    else
                    {
                        content.Add(new StringContent(""), "start_time_latest");
                    }
                    if (cbo_SelectCheckOutLatest.Time != null)
                    {
                        content.Add(new StringContent(cbo_SelectCheckOutLatest.Time), "end_time_earliest");
                    }
                    else
                    {
                        content.Add(new StringContent(""), "end_time_earliest");
                    }
                }

               
                content.Add(new StringContent(shift_type), "shift_type");
                if (shift_type == "1")
                {
                    string input = txbChonCa.Text;
                    string[] subString = input.Split(' ');
                    string firtValue = subString[0].Trim();
                    content.Add(new StringContent(firtValue.ToString()), "num_to_calculate");
                   
                }
                if (OnOff == 1)
                {
                    if (int.TryParse(txbCaQuaNgay.Text, out int value))
                    {
                        txbCaQuaNgay.Text = value.ToString();
                        if (value >= 2)
                        {
                            content.Add(new StringContent(txbCaQuaNgay.Text), "nums_day");
                            content.Add(new StringContent("1"), "over_night");

                        }
                        else
                        {
                            content.Add(new StringContent(txbCaQuaNgay.Text), "nums_day");
                            content.Add(new StringContent("0"), "over_night");
                        }
                    }
                }
                if (shift_type == "2") content.Add(new StringContent(txbSoTienTuongUng.Text), "num_to_money");
                content.Add(new StringContent(com_id), "com_id");

                if (shift_type == "3") content.Add(new StringContent(txbSoTienTheoGio.Text), "money_per_hour");

               
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    this.Visibility = Visibility.Collapsed;
                    //bodThemThanhCong.Visibility = Visibility.Visible;
                    Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, this));
                    ucShiftWorkManager.GetListShift();
                }
            }
            catch (Exception)
            {
                ErrorSytem = "Error";
                Main.grShowPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
            }
        }
        #endregion

        #region Click Event
        //Xử lý btn exit thêm ca
        private void btnExit_ThemCa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        //xử lý khi ấn "cài đặt giới hạn thời gian" trong màn hình thêm mới ca
        private void wraplimitTimeSetting_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (warpLimitTimeSettingZone.Visibility == Visibility.Collapsed)
            {
                warpLimitTimeSettingZone.Visibility = Visibility.Visible;
            }
            else
            {
                warpLimitTimeSettingZone.Visibility -= Visibility.Collapsed;
            }
        }
        //xử lý chọn công tính ca
        private void bodBtnTinhTheoSoCa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            shift_type = "1";
            bodBtnTinhTheoSoCa.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            txbTinhTheoSoCa.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
            bodBtnTinhTheoTien.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            txbTinhTheoTien.Foreground = (Brush)bc.ConvertFrom("#474747");
            bodBtnTinhTheoGio.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            txbTinhTheoGio.Foreground = (Brush)bc.ConvertFrom("#474747");
            warpSoCongTuongUng.Visibility = Visibility.Visible;
            wrapSoTienTuongUong.Visibility = Visibility.Collapsed;
            wrapSoTienTheoGio.Visibility = Visibility.Collapsed;
        }
        private void bodBtnTinhTheoTien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            shift_type = "2";
            bodBtnTinhTheoTien.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            txbTinhTheoTien.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
            bodBtnTinhTheoSoCa.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            txbTinhTheoSoCa.Foreground = (Brush)bc.ConvertFrom("#474747");
            bodBtnTinhTheoGio.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            txbTinhTheoGio.Foreground = (Brush)bc.ConvertFrom("#474747");
            warpSoCongTuongUng.Visibility = Visibility.Collapsed;
            wrapSoTienTuongUong.Visibility = Visibility.Visible;
            wrapSoTienTheoGio.Visibility = Visibility.Collapsed;
        }
        private void bodBtnTinhTheoGio_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            shift_type = "3";
            bodBtnTinhTheoGio.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            txbTinhTheoGio.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
            bodBtnTinhTheoSoCa.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            txbTinhTheoSoCa.Foreground = (Brush)bc.ConvertFrom("#474747");
            bodBtnTinhTheoTien.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            txbTinhTheoTien.Foreground = (Brush)bc.ConvertFrom("#474747");
            warpSoCongTuongUng.Visibility = Visibility.Collapsed;
            wrapSoTienTuongUong.Visibility = Visibility.Collapsed;
            wrapSoTienTheoGio.Visibility = Visibility.Visible;
        }
        //Xử lý chọn dropdownbox số công tương ứng
        private void Image_MouseLeftButtonUp_ChonCa(object sender, MouseButtonEventArgs e)
        {
            if (bodCongTrenCa.Visibility == Visibility.Collapsed)
            {
                bodCongTrenCa.Visibility = Visibility.Visible;
            }
            else
            {
                bodCongTrenCa.Visibility -= Visibility.Collapsed;
            }
        }
        //Xử lý khi ấn nút thêm
        
        private void bodBtnSua_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool allow = true;
                if (txtShiftName.Text == "" || string.IsNullOrEmpty(txtShiftName.Text))
                {
                    tb_ValidateTenCa.Visibility = Visibility.Visible;
                    tb_ValidateTenCa.Text = "Tên ca không được để trống";
                    allow = false;
                }
                else
                {
                    tb_ValidateTenCa.Visibility = Visibility.Collapsed;
                   
                }
                if (string.IsNullOrEmpty(cbo_SelectTimeCheckIn.Time) || cbo_SelectTimeCheckIn.Time == "--:-- --")
                {
                    tb_ValidateGioVaoCa.Visibility = Visibility.Visible;
                    tb_ValidateGioVaoCa.Text = "Bạn chưa nhập giờ vào ca hoặc nhập chưa đầy đủ!";
                    allow = false;
                }
                else
                {
                   tb_ValidateGioVaoCa.Visibility = Visibility.Collapsed;
                }
                if (stp_GioHetCa.Visibility == Visibility.Visible)
                {
                    if (string.IsNullOrEmpty(cbo_SelectTimeCheckOut.Time) || cbo_SelectTimeCheckOut.Time == "--:-- --")
                    {
                        tb_ValidateGioHetCa.Visibility = Visibility.Visible;
                        tb_ValidateGioHetCa.Text = "Bạn chưa nhập giờ hết ca hoặc nhập chưa đầy đủ!";
                        allow = false;
                    }
                    else
                    {
                        tb_ValidateGioHetCa.Visibility = Visibility.Collapsed;
                    }
                }

                if (shift_type == "1")
                {
                    if (string.IsNullOrEmpty(txbChonCa.Text) || txbChonCa.Text == "")
                    {
                        tb_ValidateSoCongTuongUng.Visibility = Visibility.Visible;
                        tb_ValidateSoCongTuongUng.Text = "Không được để trống số công tương ứng";
                        allow = false;
                    }
                    else
                    {
                        tb_ValidateSoCongTuongUng.Visibility = Visibility.Collapsed;
                       
                    }
                }
                if (shift_type == "2")
                {
                    if (string.IsNullOrEmpty(txbSoTienTuongUng.Text) || txbSoTienTuongUng.Text == "")
                    {
                        tb_ValidateSoTienTuongUng.Visibility = Visibility.Visible;
                        tb_ValidateSoTienTuongUng.Text = "Không được để trống số tiền tương ứng";
                        allow = false;
                    }
                    else
                    {
                        tb_ValidateSoTienTuongUng.Visibility = Visibility.Collapsed;
                        
                    }
                }
                if (shift_type == "3")
                {
                    if (string.IsNullOrEmpty(txbSoTienTheoGio.Text) || txbSoTienTheoGio.Text == "")
                    {
                        tb_ValidateSoTienTheoGio.Visibility = Visibility.Visible;
                        tb_ValidateSoTienTheoGio.Text = "Không được để trống số tiền theo giờ";
                        allow = false;
                    }
                    else
                    {
                        tb_ValidateSoTienTheoGio.Visibility = Visibility.Collapsed;
                       
                    }
                }
                if (allow)
                {
                    AddNewShift();
                }
            }
            catch (Exception)
            {}  
        }

        //Xử lý khi ấn ok sau khi ấn thêm
        private void bodThemThanhCong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        #region SelectTimeCheckIn    
        private void bodSelectTimeCheckIn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListTimeCheckInCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListTimeCheckInCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListTimeCheckInCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvTimeCheckIn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //txbSelectTimeCheckIn.Text = tpTimeCheckIn.Text;
            //txbSelectTimeCheckIn.Foreground = (Brush)bc.ConvertFromString("#474747");
            bodListTimeCheckInCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region SelectTimeCheckOut
        private void bodSelectTimeCheckOut_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListTimeCheckOutCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListTimeCheckOutCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListTimeCheckOutCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvTimeCheckOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //txbSelectTimeCheckOut.Text = tpTimeCheckOut.Text;
            //txbSelectTimeCheckOut.Foreground = (Brush)bc.ConvertFromString("#474747");

            bodListTimeCheckOutCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region SelectCheckInEarliest 
        private void bodSelectCheckInEarliest_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListCheckInEarliestCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListCheckInEarliestCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListCheckInEarliestCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvCheckInEarliest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //txbSelectCheckInEarliest.Text = tpCheckInEarliest.Text;
            //txbSelectCheckInEarliest.Foreground = (Brush)bc.ConvertFromString("#474747");

            bodListCheckInEarliestCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region SelectCheckOutLatest 
        private void bodSelectCheckOutLatest_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListCheckOutLatestCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListCheckOutLatestCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListCheckOutLatestCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvCheckOutLatest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //txbSelectCheckOutLatest.Text = tpCheckOutLatest.Text;
            //txbSelectCheckOutLatest.Foreground = (Brush)bc.ConvertFromString("#474747");

            bodListCheckOutLatestCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion
        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = ((Rectangle)sender).Parent as Grid;
            var bodPopUp = grid.Parent as Border;
            bodPopUp.Visibility = Visibility.Collapsed;
        }
        private void bod_DeleteCongCa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            txbChonCa.Text = "";
            bodCongTrenCa.Visibility -= Visibility.Collapsed;
        }
        private void bod_TangTien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (int.TryParse(txbSoTienTuongUng.Text, out int value))
            {
                txbSoTienTuongUng.Text = (value + 1).ToString();
            }
        }

        private void bod_GiamTien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (int.TryParse(txbSoTienTuongUng.Text, out int value))
            {
                txbSoTienTuongUng.Text = (value - 1).ToString();
            }
        }
        private void txbChonCa_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txbSoTienTuongUng_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #endregion

        #region Hover Event
        private void bod_CongCa_MouseEnter(object sender, MouseEventArgs e)
        {
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {

                Border bod_CongCa = FindChild<Border>(row, "bod_CongCa");
                TextBlock tb_CongCa = FindChild<TextBlock>(row, "tb_CongCa");
                if (bod_CongCa != null && tb_CongCa != null)
                {
                    bod_CongCa.Background = (Brush)bc.ConvertFrom("#4C5BD4");
                    tb_CongCa.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
                }
            }
        }

        private void bod_CongCa_MouseLeave(object sender, MouseEventArgs e)
        {
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {

                Border bod_CongCa = FindChild<Border>(row, "bod_CongCa");
                TextBlock tb_CongCa = FindChild<TextBlock>(row, "tb_CongCa");
                if (bod_CongCa != null && tb_CongCa != null)
                {
                    bod_CongCa.Background = (Brush)bc.ConvertFrom("#FFFFFF");
                    tb_CongCa.Foreground = (Brush)bc.ConvertFrom("#474747");
                }
            }
        }

        private void bod_DeleteCongCa_MouseEnter(object sender, MouseEventArgs e)
        {
            bod_DeleteCongCa.Background = (Brush)bc.ConvertFrom("#FF5B4D");

        }

        private void bod_DeleteCongCa_MouseLeave(object sender, MouseEventArgs e)
        {
            bod_DeleteCongCa.Background = (Brush)bc.ConvertFrom("#6666");
        }
        private void bodChonCa_MouseEnter(object sender, MouseEventArgs e)
        {
            if (txbChonCa.Text != "")
            {
                bod_DeleteCongCa.Visibility = Visibility.Visible;
            }
        }

        private void bodChonCa_MouseLeave(object sender, MouseEventArgs e)
        {
            bod_DeleteCongCa.Visibility = Visibility.Collapsed;
        }
        private void bod_TienTuongUng_MouseEnter(object sender, MouseEventArgs e)
        {
            gr_TangGiamTien.Visibility = Visibility.Visible;
        }

        private void bod_TienTuongUng_MouseLeave(object sender, MouseEventArgs e)
        {
            gr_TangGiamTien.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region Loaded Event
        //Xử lý khi ấn chọn công ty
        private void lsvCongTrenCa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lstcongca = lsvCongTrenCa.SelectedItem.ToString();
            if (!lstCongCa.Any(item => item.Cong_ca == lstcongca))
            {
                CongTrenCa congca = new CongTrenCa()
                {
                    Cong_ca = ((CongTrenCa)lsvCongTrenCa.SelectedItem).Cong_ca
                };
                txbChonCa.Text = congca.Cong_ca + " công / 1 ca";
                bodCongTrenCa.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Comons
        // Hàm giúp tìm kiếm đối tượng con trong VisualTree
        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T typedChild && (child as FrameworkElement)?.Name == childName)
                {
                    return typedChild;
                }
                else
                {
                    T childOfChild = FindChild<T>(child, childName);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        //// Hàm giúp tìm cha của một đối tượng trong VisualTree
        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }
        #endregion

        private void txbSoTienTuongUng_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
                tb_ValidateSoTienTuongUng.Visibility = Visibility.Visible;
                tb_ValidateSoTienTuongUng.Text = "Hãy nhập đúng định dạng số tiền!";
            }
            else
            {
                tb_ValidateSoTienTuongUng.Visibility = Visibility.Collapsed;
            }
        }
        private bool IsNumeric(string text)
        {
            return double.TryParse(text, out _);
        }

        private void txbSoTienTheoGio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
                tb_ValidateSoTienTheoGio.Visibility = Visibility.Visible;
                tb_ValidateSoTienTheoGio.Text = "Hãy nhập đúng định dạng số tiền!";
            }
            else
            {
                tb_ValidateSoTienTheoGio.Visibility = Visibility.Collapsed;
            }
        }
        int OnOff;
        private void BackOnOff(object sender, MouseButtonEventArgs e)
        {

            if (btnOnOff.HorizontalAlignment == HorizontalAlignment.Left)
            {
                 OnOff = 1;
                tb_CaQuaNgay.Foreground = (Brush)bc.ConvertFrom("#4C5BD4");
                btnOnOff.HorizontalAlignment = HorizontalAlignment.Right;
                bodBackOnOff.Background = (Brush)bc.ConvertFrom("#4C5BD4");
                wrap_CaQuaNgay.Visibility = Visibility.Visible;
            }
            else
            {
                OnOff = 2;
                txbCaQuaNgay.Text = "";
                tb_CaQuaNgay.Foreground = (Brush)bc.ConvertFrom("#474747");
                btnOnOff.HorizontalAlignment = HorizontalAlignment.Left;
                bodBackOnOff.Background = (Brush)bc.ConvertFrom("#ECECEC");
                wrap_CaQuaNgay.Visibility= Visibility.Collapsed;
            }
        }

        int End_Date_Type;
        private void BackOnOffNoEndDate(object sender, MouseButtonEventArgs e)
        {
            if (btnOnOffNoEndDate.HorizontalAlignment == HorizontalAlignment.Left)
            {
                End_Date_Type = 1;
                shift_type = "3";
                tb_NoEndDate.Foreground = (Brush)bc.ConvertFrom("#4C5BD4");
                btnOnOffNoEndDate.HorizontalAlignment = HorizontalAlignment.Right;
                bodBackOnOffNoEndDate.Background = (Brush)bc.ConvertFrom("#4C5BD4");
                stp_GioHetCa.Visibility = Visibility.Hidden;
                bodBtnTinhTheoSoCa.Visibility = Visibility.Collapsed;
                bodBtnTinhTheoTien.Visibility = Visibility.Collapsed;
                bodBtnTinhTheoGio.Background = (Brush)bc.ConvertFrom("#4C5BD4");
                warpSoCongTuongUng.Visibility = Visibility.Collapsed;
                wrapSoTienTuongUong.Visibility = Visibility.Collapsed;
                txbTinhTheoGio.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
                wrapSoTienTheoGio.Visibility = Visibility.Visible;
                wap_SelectCheckOutLatest.Visibility = Visibility.Collapsed;
            }
            else
            {
                End_Date_Type = 0;
                tb_NoEndDate.Foreground = (Brush)bc.ConvertFrom("#474747");
                btnOnOffNoEndDate.HorizontalAlignment = HorizontalAlignment.Left;
                bodBackOnOffNoEndDate.Background = (Brush)bc.ConvertFrom("#ECECEC");

                stp_GioHetCa.Visibility = Visibility.Visible;
                bodBtnTinhTheoSoCa.Visibility = Visibility.Visible;
                bodBtnTinhTheoTien.Visibility = Visibility.Visible;
                bodBtnTinhTheoGio.Background = (Brush)bc.ConvertFrom("#FFFFFF");
                warpSoCongTuongUng.Visibility = Visibility.Visible;
                wrapSoTienTuongUong.Visibility = Visibility.Visible;
                txbTinhTheoGio.Foreground = (Brush)bc.ConvertFrom("#474747");
                wrapSoTienTheoGio.Visibility = Visibility.Collapsed;
                wrapSoTienTuongUong.Visibility = Visibility.Collapsed;
                wap_SelectCheckOutLatest.Visibility = Visibility.Visible;
            }
        }
        private void bod_CaQuaNgay_MouseEnter(object sender, MouseEventArgs e)
        {
            gr_TangGiamNgay.Visibility = Visibility.Visible;
        }

        private void bod_CaQuaNgay_MouseLeave(object sender, MouseEventArgs e)
        {
            gr_TangGiamNgay.Visibility = Visibility.Collapsed;
        }

        private void bod_TangNgay_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbCaQuaNgay.Text))
            {
                txbCaQuaNgay.Text = "2";
            }
            else
            {
                if (int.TryParse(txbCaQuaNgay.Text, out int value))
                {
                    txbCaQuaNgay.Text = (value + 1).ToString();
                }
                else
                {
                    txbCaQuaNgay.Text = "2";
                }
            }
        }

        private void bod_GiamNgay_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (int.TryParse(txbCaQuaNgay.Text, out int value))
            {
                if (value > 2)
                {
                    txbCaQuaNgay.Text = (value - 1).ToString();
                }
                
               
            }
        }

        private void txbCaQuaNgay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
             if (!IsNumeric(e.Text))
             {
                    e.Handled = true;
                    tb_ValidateSoNgay.Visibility = Visibility.Visible;
                    tb_ValidateSoNgay.Text = "Hãy nhập đúng định dạng số tiền!";
             }
             else
             {
                 tb_ValidateSoNgay.Visibility = Visibility.Collapsed;
             }
        }
    }
}
