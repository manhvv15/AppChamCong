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
using System.Windows.Media;
using ChamCong365.Popup.DatePicker;
using ChamCong365.Popup.funcCompanyManager.ListCandidatesPopups;
using Microsoft.Office.Interop.Excel;
using Border = System.Windows.Controls.Border;
using System.Net.Http;
using ChamCong365.OOP.funcQuanLyCongTy;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using ChamCong365.APIs;

namespace ChamCong365
{
    /// <summary>
    /// Interaction logic for ucCandidatesList.xaml
    /// </summary>
    public partial class ucCandidatesList : UserControl
    {

        MainWindow Main;
        public ListProcess.Root listProcessRoot = new ListProcess.Root();
        Dictionary<string, string> listAllEmployee = new Dictionary<string, string>();
        Dictionary<string, string> listRecruitmentNew = new Dictionary<string, string>();

        public class Items
        {
            public string ID { get; set; }
            public string Name { get; set; }
        }
        // items in cbx process
        public List<Items> listItem_cbx4_1 = new List<Items>() {
            new Items() { ID = "0", Name = "Nhận hồ sơ ứng viên" },
            new Items() { ID = "1", Name = "Nhận việc" } ,
            new Items() { ID = "2", Name = "Trượt" } ,
            new Items() { ID = "3", Name = "Hủy" } ,
            new Items() { ID = "4", Name = "Ký hợp đồng" }
        };

        public List<Items> listItem_cbx4_12 = new List<Items>() {
            new Items() { ID = "1", Name = "Nhận việc" } ,
            new Items() { ID = "2", Name = "Trượt" } ,
            new Items() { ID = "3", Name = "Hủy" } ,
            new Items() { ID = "4", Name = "Ký hợp đồng" }
        };
        public List<Items> listItem_cbx4_2 = new List<Items>() {
            new Items() { ID = "-1", Name = "Nhận việc" } ,
            new Items() { ID = "-2", Name = "Trượt" } ,
            new Items() { ID = "-3", Name = "Hủy" } ,
            new Items() { ID = "-4", Name = "Ký hợp đồng" }
        };
        public List<Items> listItem_cbx4_process = new List<Items>() {
            new Items() { ID = "0", Name = "Nhận hồ sơ ứng viên" },
            new Items() { ID = "1", Name = "Nhận việc" } ,
            new Items() { ID = "2", Name = "Trượt" } ,
            new Items() { ID = "3", Name = "Hủy" } ,
            new Items() { ID = "4", Name = "Ký hợp đồng" }
        };

        // cbx status
        public List<Items> listItemStatus = new List<Items>()
        {
            new Items(){ID = "0", Name="Chọn trạng thái"},
            new Items(){ID = "1", Name="Trượt vòng loại phỏng vấn"},
            new Items(){ID = "2", Name="Trượt học việc"},
            new Items(){ID = "3", Name="Trượt vòng loại hồ sơ"},
            new Items(){ID = "4", Name="Hủy phỏng vấn"},
            new Items(){ID = "5", Name="Hủy nhận việc"},
            new Items(){ID = "6", Name="Hủy học việc"},
        };

        //cbx Gender
        public List<Items> listItemsGender = new List<Items>()
        {
            new Items(){ID = "-1", Name="Chọn giới tính"},
            new Items(){ID = "0", Name="Nam"},
            new Items(){ID = "1", Name="Nữ"},
            new Items(){ID = "2", Name="Khác"},
        };

        public ucCandidatesList(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            cbxStatus.ItemsSource = listItemStatus;
            cbxGender.ItemsSource = listItemsGender;
            GetAllEmployee();
            GetAllRecruitmentNew();
            GetListProcess();
        }

        public async void GetListProcess()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.list_process);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(tbName.Text), "name");
                if (dp1.SelectedDate != null)
                {
                    string fromDate = dp1.SelectedDate?.ToString("dd/MM/yyyy");
                    content.Add(new StringContent(fromDate), "fromDate");

                }
                if (dp2.SelectedDate != null)
                {
                    string toDate = dp2.SelectedDate?.ToString("dd/MM/yyyy");
                    content.Add(new StringContent(toDate), "toDate");
                }
                if (cbxGender.SelectedIndex != -1 && cbxGender.SelectedIndex != 0)  content.Add(new StringContent(cbxGender.SelectedValue.ToString()), "gender");
                if (cbxRecruitment.SelectedIndex != -1) content.Add(new StringContent(cbxRecruitment.SelectedValue.ToString()), "recruitmentNewsId");
                if(cbxUserHiring.SelectedIndex!=-1 && cbxUserHiring.SelectedIndex != 0) content.Add(new StringContent(cbxUserHiring.SelectedValue.ToString()), "userHiring");
                if (cbxStatus.SelectedIndex != -1 && cbxStatus.SelectedIndex!=0)  content.Add(new StringContent(cbxStatus.SelectedValue.ToString()), "status");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ListProcess.Root result = JsonConvert.DeserializeObject<ListProcess.Root>(responseContent);
                    listProcessRoot = result;
                }

                icListCandidate.ItemsSource = listProcessRoot.data.listCandidate;
                icListProcess.ItemsSource = listProcessRoot.data.data.Where(x => x.beforeProcess == 0);
                icListCandidateGetJob.ItemsSource = listProcessRoot.data.listCandidateGetJob;
                icListProcessNhanViec.ItemsSource = listProcessRoot.data.data.Where(x => x.beforeProcess == 1);
                icListCandidateFailJob.ItemsSource = listProcessRoot.data.listCandidateFailJob;
                icListProcessTruot.ItemsSource = listProcessRoot.data.data.Where(x => x.beforeProcess == 2);
                icListCandidateCancelJob.ItemsSource = listProcessRoot.data.listCandidateCancelJob;
                icListProcessHuy.ItemsSource = listProcessRoot.data.data.Where(x => x.beforeProcess == 3);
                icListCandidateContactJob.ItemsSource = listProcessRoot.data.listCandidateContactJob;
                icListProcessKyHopDong.ItemsSource = listProcessRoot.data.data.Where(x => x.beforeProcess == 2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra");
            }




        }

        private void tbName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetListProcess();
            }
        }

        private void cbxUserHiring_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cbxUserHiring.SelectedIndex = -1;
                string textSearch = cbxUserHiring.Text;
                cbxUserHiring.Items.Refresh();
                cbxUserHiring.ItemsSource = listAllEmployee.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
            }
        }

        private void cbxUserHiring_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxUserHiring.SelectedIndex = -1;
            string textSearch = cbxUserHiring.Text + e.Text;
            cbxUserHiring.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cbxUserHiring.Text = "";
                cbxUserHiring.Items.Refresh();
                cbxUserHiring.ItemsSource = listAllEmployee;
                cbxUserHiring.SelectedIndex = -1;
            }
            else
            {
                cbxUserHiring.ItemsSource = "";
                cbxUserHiring.Items.Refresh();
                cbxUserHiring.ItemsSource = listAllEmployee.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
            }
        }

        private void cbxRecruitment_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxRecruitment.SelectedIndex = -1;
            string textSearch = cbxRecruitment.Text + e.Text;
            cbxRecruitment.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cbxRecruitment.Text = "";
                cbxRecruitment.Items.Refresh();
                cbxRecruitment.ItemsSource = listRecruitmentNew;
                cbxRecruitment.SelectedIndex = -1;
            }
            else
            {
                cbxRecruitment.ItemsSource = "";
                cbxRecruitment.Items.Refresh();
                cbxRecruitment.ItemsSource = listRecruitmentNew.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
            }
        }

        private void cbxRecruitment_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cbxRecruitment.SelectedIndex = -1;
                string textSearch = cbxRecruitment.Text;
                cbxRecruitment.Items.Refresh();
                cbxRecruitment.ItemsSource = listRecruitmentNew.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
            }
        }

        // lấy toàn bộ nhân viên trong công ty
        private async void GetAllEmployee()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.managerUser_all);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent("10000"), "pageSize");
                request.Content = content;

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    EmployeeRoot result = JsonConvert.DeserializeObject<EmployeeRoot>(responseContent);
                    listAllEmployee.Add("", "Tất cả");
                    foreach (var item in result.data.items)
                    {
                        listAllEmployee.Add(item.ep_id.ToString(), item.ep_name + "(" + item.ep_id + ")");
                        cbxUserHiring.ItemsSource= listAllEmployee; 
                    }
                }
            }
            catch
            {
                MessageBox.Show("lỗi lấy tất cả nhân viên");
            }
        }
        private async void GetAllRecruitmentNew()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.RecruitmentNew_list_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ListRecruitmentNew.Root result = JsonConvert.DeserializeObject<ListRecruitmentNew.Root>(responseContent);

                    foreach (var item in result.success.data.data)
                    {
                        listRecruitmentNew.Add(item.id.ToString(), item.title);
                        cbxRecruitment.ItemsSource= listRecruitmentNew;
                    }
                }
            }
            catch
            {
                MessageBox.Show("lỗi lấy tất cả tin tuyển dụng");
            }
        }

        private void ImgDetailDeletePopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var image = (System.Windows.Controls.Image)sender;
            var grid1 = image.Parent as Grid;
            var grid2 = grid1.Parent as Grid;
            var border = grid2.Parent as System.Windows.Controls.Border;
            var GridContainer = border.Parent as Grid;
            GridContainer.Children[2].Visibility = Visibility.Visible;
        }

        private void dockDetail_MouseEnter(object sender, MouseEventArgs e)
        {
            var dockPanel = (DockPanel)sender;
            var detailTextBlock = (TextBlock)dockPanel.Children[1];
            detailTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
        }

        private void dockDetail_MouseLeave(object sender, MouseEventArgs e)
        {
            var dockPanel = (DockPanel)sender;
            var detailTextBlock = (TextBlock)dockPanel.Children[1];
            detailTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#474747"));
            //txtDetail.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#474747"));

        }

        private void dockDelete_MouseEnter(object sender, MouseEventArgs e)
        {
            var dockPanel = (DockPanel)sender;
            var detailTextBlock = (TextBlock)dockPanel.Children[1];
            detailTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5B4D"));
            //txtDelete.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5B4D"));
        }

        private void dockDelete_MouseLeave(object sender, MouseEventArgs e)
        {
            var dockPanel = (DockPanel)sender;
            var detailTextBlock = (TextBlock)dockPanel.Children[1];
            detailTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#474747"));
            //txtDelete.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#474747"));
        }

        private void bodCreateStage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucCreateStage(this, listProcessRoot.data.data));
        }

        private void dockDelete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucDeleteCandidate());

        }

        private void bodAddCandidate_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucAddCandidate(this,Properties.Settings.Default.Token, listAllEmployee, listRecruitmentNew));

        }

        private void lsvDSUngVien_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                scroll.ScrollToVerticalOffset(scroll.VerticalOffset);
                scroll1.ScrollToHorizontalOffset(scroll1.HorizontalOffset - e.Delta);
            }
            else
                scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }

        private void NavigateToCandidateDetail(object sender, MouseButtonEventArgs e)
        {

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (popupOption.Visibility == Visibility.Visible)
            {
                popupOption.Visibility = Visibility.Collapsed;
            }
            else
            {
                Border border = sender as Border;
                System.Windows.Point p = e.GetPosition(Main.dopBody);
                double x = p.X - popupOption.Width;
                double y = p.Y;

                Thickness thickness = new Thickness(x, y, 0, 0);
                popupOption.Margin = thickness;
                popupOption.Visibility = Visibility.Visible;
                popupOption.DataContext = border.DataContext;
            }
        }

        private void ShowPopupEditProcess(object sender, MouseButtonEventArgs e)
        {
            var select = ((TextBlock)sender).DataContext as ListProcess.Process;
            var id = select.id;
            Main.grShowPopup.Children.Add(new ucUpdateStage(this, listProcessRoot.data.data,select));


        }

        private void ShowPopupDeleteProcess(object sender, MouseButtonEventArgs e)
        {
            var select = ((TextBlock)sender).DataContext as ListProcess.Process;
            var id = select.id;
            Main.grShowPopup.Children.Add(new ucDeleteStage(this,select.id));

        }

        private void NavigateToCandidateDetailGetJob(object sender, MouseButtonEventArgs e)
        {

        }

        private void NavigateToCandidateProcessDetail(object sender, MouseButtonEventArgs e)
        {

        }

        private void NavigateToCandidateDetailFailJob(object sender, MouseButtonEventArgs e)
        {

        }

        private void NavigateToCandidateDetailCancelJob(object sender, MouseButtonEventArgs e)
        {

        }

        private void NavigateToCandidateDetailContractJob(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            Grid grid = sender as Grid;
            grid.Children[0].Visibility = Visibility.Visible;
            TextBlock textBlock = (TextBlock)grid.Children[1];
            var converter = new BrushConverter();
            textBlock.Foreground = (Brush)converter.ConvertFromString("#4C5BD4");
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid grid = sender as Grid;
            grid.Children[0].Visibility = Visibility.Collapsed;
            TextBlock textBlock = (TextBlock)grid.Children[1];
            var converter = new BrushConverter();
            textBlock.Foreground = (Brush)converter.ConvertFromString("#474747");
        }

        private void ClickViewDetail(object sender, MouseButtonEventArgs e)
        {

        }

        private void ClickDelete(object sender, MouseButtonEventArgs e)
        {

        }

        private void TransportFile(object sender, MouseButtonEventArgs e)
        {

        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            popupOption.Visibility = Visibility.Collapsed;
        }

        private void btnSearch_MouseUp(object sender, MouseButtonEventArgs e)
        {
            GetListProcess();
        }

        private void txbDetail_MouseEnter(object sender, MouseEventArgs e)
        {
            var textblock = sender as TextBlock;
            textblock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5B4D"));
        }

        private void txbDetail_MouseLeave(object sender, MouseEventArgs e)
        {
            var textblock= sender as TextBlock;
            textblock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#474747"));

        }

        private void txbDelete_MouseEnter(object sender, MouseEventArgs e)
        {
            var textblock = sender as TextBlock;
            textblock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5B4D"));
        }

        private void txbDelete_MouseLeave(object sender, MouseEventArgs e)
        {
            var textblock = sender as TextBlock;
            textblock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#474747"));
        }
    }
}
