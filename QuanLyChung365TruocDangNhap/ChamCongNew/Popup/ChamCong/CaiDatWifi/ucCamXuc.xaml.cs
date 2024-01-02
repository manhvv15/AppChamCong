using QuanLyChung365TruocDangNhap.ChamCongNew.CaiDatDeX.ThongBao;
using QuanLyChung365TruocDangNhap.ChamCongNew.Common;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CamXuc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popups.ChamCong.CaiDatWifi
{
    /// <summary>
    /// Interaction logic for ucCamXuc.xaml
    /// </summary>
    public partial class ucCamXuc : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        MainWindow Main;    
        BrushConverter bc = new BrushConverter();

        public ucCamXuc(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            LoadCamXuc();
            getToggleEmotion();
        }


        #region Call Api Cảm Xúc

        private List<List_CamXuc> _listCamXuc;
        public List<List_CamXuc> listCamXuc
        {
            get { return _listCamXuc; }
            set { _listCamXuc = value; OnPropertyChanged(); }
        }
        public async void LoadCamXuc()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.DanhSach_CamXuc_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resConten = await response.Content.ReadAsStringAsync();

                Root_CamXuc resultCamXuc = JsonConvert.DeserializeObject<Root_CamXuc>(resConten);

                if (resultCamXuc != null)
                {
                    listCamXuc = resultCamXuc.data.list;
                    foreach (var item in listCamXuc)
                    {
                        tbDiemChuan.Text = item.avg_pass_score.ToString();
                    }
                    lsvDanhSachCamSuc.ItemsSource = listCamXuc;
                }
            }
            catch (Exception)
            {
            }
        }

        public async void getToggleEmotion()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/emotions/getToggleEmotion");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resConten = await response.Content.ReadAsStringAsync();

                Root_Togg resultCamXuc = JsonConvert.DeserializeObject<Root_Togg>(resConten);

                if (resultCamXuc != null)
                {
                    if (resultCamXuc.data.data.emotion_active == true)
                    {
                        btnOnOff.HorizontalAlignment = HorizontalAlignment.Right;
                        bodBackOnOff.Background = (Brush)bc.ConvertFrom("#12DD00");
                    }
                    else
                    {
                        btnOnOff.HorizontalAlignment = HorizontalAlignment.Left;
                        bodBackOnOff.Background = (Brush)bc.ConvertFrom("#ECECEC");
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion


        Data_OnOff_CamXuc Emotion_Setting = new Data_OnOff_CamXuc();
        private async void BackOnOff(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.OnOff_CamXuc_Api);
                request.Headers.Add("Authorization", "Bearer "+ Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resOnOff = await response.Content.ReadAsStringAsync();

                Root_OnOff_CamXuc result = JsonConvert.DeserializeObject<Root_OnOff_CamXuc>(resOnOff);
                if (result.data != null)
                {

                    if (btnOnOff.HorizontalAlignment == HorizontalAlignment.Left)
                    {
                        double On  = 1;
                        result.data.emotion_setting = true;
                        btnOnOff.HorizontalAlignment = HorizontalAlignment.Right;
                        bodBackOnOff.Background = (Brush)bc.ConvertFrom("#12DD00");
                        Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, On));
                    }
                    else
                    {
                        double Off = 2;
                        result.data.emotion_setting = false;
                        btnOnOff.HorizontalAlignment = HorizontalAlignment.Left;
                        bodBackOnOff.Background = (Brush)bc.ConvertFrom("#ECECEC");
                        Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, Off));
                    }
                }
            }
            catch (Exception)
            {}
        }

        private void ThemThangDiem(object sender, MouseButtonEventArgs e)
        {
            List_CamXuc cx = new List_CamXuc();
            if (cx != null)
            {
                Main.grShowPopup.Children.Add(new ucThemMoiCamXuc(Main,cx,this));
            }
        }
        #region Cập nhật cảm xúc
        private int emotionid;
        private List_CamXuc ListCamXuc;
        string ErrorSytem;
        private async void CapNhatCamXuc(object sender, MouseButtonEventArgs e)
        {
            try
            {
                List_CamXuc listCamXuc = (sender as Border).DataContext as List_CamXuc;
                if (listCamXuc != null)
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.CapNhat_CamXuc_Api);
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(listCamXuc.emotion_detail), "emotion_detail");
                    content.Add(new StringContent(listCamXuc.min_score.ToString()), "min_score");
                    content.Add(new StringContent(listCamXuc.max_score.ToString()), "max_score");
                    content.Add(new StringContent(listCamXuc.emotion_id.ToString()), "emotion_id");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                    {
                        LoadCamXuc();
                        Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, ListCamXuc));
                    }
                }
            }
            catch (Exception)
            {
                ErrorSytem = "error";
                Main.grShowPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
            }
        }

        private void ThongTinTextInListView(object sender, RoutedEventArgs e)
        {
            ListView row = FindAncestor<ListView>((DependencyObject)e.OriginalSource);
            if (row != null)
            {
                TextBox tb_EmotionDetail = FindChild<TextBox>(row, "tb_EmotionDetail");
                TextBox tb_MinScore = FindChild<TextBox>(row, "tb_MinScore");
                TextBox tb_MaxScore = FindChild<TextBox>(row, "tb_MaxScore");
                if (tb_EmotionDetail != null && tb_MinScore != null && tb_MaxScore != null)
                {
                    
                }
            }
        }

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

        //private void CapNhatCamXuc(object sender, MouseButtonEventArgs e)
        //{
        //    List_CamXuc listCamXuc = (sender as Border).DataContext as List_CamXuc;
        //    if (listCamXuc != null)
        //    {
        //        Main.grShowPopup.Children.Add(new ucCapNhatCamXuc(Main, listCamXuc, this));
        //    }
        //}

        string tbStart = "";
        private void tb_Start(object sender, TextChangedEventArgs e)
        {
            ListView lv = sender as ListView;
      
        }

        private void XoaCamXuc(object sender, MouseButtonEventArgs e)
        {
            List_CamXuc listCamXuc = (sender as Border).DataContext as List_CamXuc;
            if (listCamXuc != null)
            {
                Main.grShowPopup.Children.Add(new ucXoaCamXuc(Main,listCamXuc, this));
            }
        }

        private async void LuuDiemChuanCamSuc(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.CapNhat_DiemChuan_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(tbDiemChuan.Text), "avg_score");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                   var resDiemChuan = await response.Content.ReadAsStringAsync();
                    LoadCamXuc();
                    double save = 3;
                    Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, save));
                }  
            }
            catch (Exception)
            {
                ErrorSytem = "error";
                Main.grShowPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
            }
        }

        private void btnHuyCamSuc_MouseEnter(object sender, MouseEventArgs e)
        {
            btnHuyCamSuc.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            txtHuy.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
        }

        private void btnHuyCamSuc_MouseLeave(object sender, MouseEventArgs e)
        {
            btnHuyCamSuc.Background = (Brush)bc.ConvertFrom("#6666");
            txtHuy.Foreground = (Brush)bc.ConvertFrom("#474747");
        }

        private void btnHuyCamSuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in listCamXuc)
            {
                tbDiemChuan.Text = item.avg_pass_score.ToString();
            }
        }

        private void scroll_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset);
            //e.Handled = true;
        }

        private void icListChill_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    scroll.ScrollToVerticalOffset(scroll.VerticalOffset);
                    scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - e.Delta);
                }
                else
                {
                    Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
                }
            }
            catch { }
        }

        private void tb_MinScore_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tb_MaxScore_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ListView row = FindAncestor<ListView>((DependencyObject)e.OriginalSource);
            if (row != null)
            {
                TextBox tb_MinScore = FindChild<TextBox>(row, "tb_MinScore");
                TextBox tb_MaxScore = FindChild<TextBox>(row, "tb_MaxScore");
                if (tb_MinScore != null && tb_MaxScore != null)
                {
                    tb_MinScore.PreviewTextInput += tb_MinScore_PreviewTextInput;
                    tb_MaxScore.PreviewTextInput += tb_MaxScore_PreviewTextInput;
                }
            }
        }

       
    }
}
