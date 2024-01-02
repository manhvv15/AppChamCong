using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.XuatCong;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
//using DocumentFormat.OpenXml.Wordprocessing;
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
using Employee = QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy.Employee;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping
{
    /// <summary>
    /// Interaction logic for ucLichSuDiemDanh.xaml
    /// </summary>
    public partial class ucDanhSachBuocDiLam : UserControl
    {
        MainWindow Main;
        List<Employee> listAllEmployee1 = new List<Employee>();
        public ucDanhSachBuocDiLam(MainWindow main)
        {
            InitializeComponent();
            Main = main;

            GetHistoryCheckIn();
        }
        public async void GetHistoryCheckIn(int pageNumber = 1)
        {
            try
            {
                bool isGetEmployee = await GetAllEmployee();
                if (isGetEmployee)
                {

                    var bodyObject = new
                    {
                        curPage = pageNumber

                    };
                    string json = JsonConvert.SerializeObject(bodyObject);

                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/employee/listForceWork");
                    request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);

                    var content = new StringContent(json, null, "application/json");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        ListForceWorkEntites.Root result = JsonConvert.DeserializeObject<ListForceWorkEntites.Root>(responseContent);
                        if (pagin.SelectedPage == 0) pagin.TotalRecords = (int)result.data.total;
                        if (result.data.list != null)
                        {
                            foreach (var item in result.data.list)
                            {
                                if (item.time_create != null && item.time_create != "")
                                {
                                    item.time_create = DateTimeOffset.FromUnixTimeSeconds(long.Parse(item.time_create)).ToLocalTime().ToString("HH:mm:ss dd-MM-yyyy");
                                }
                                if (item.time_duyet != null && item.time_duyet != "")
                                {
                                    item.time_duyet = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(item.time_duyet)).ToLocalTime().ToString("HH:mm:ss dd-MM-yyyy");
                                }
                                string[] listIdUserDuyet = item.id_user_duyet.Split(',');
                                foreach (string idUser in listIdUserDuyet)
                                {

                                    item.name_user_duyet += listAllEmployee1.Where(x => x.ep_id == int.Parse(idUser)).FirstOrDefault()?.ep_name + "| ";

                                }
                                item.name_user_duyet = CatBoKiTuCuoiKhongPhaiChu(item.name_user_duyet);



                            }
                        }
                        dgHistoryCheckIn.ItemsSource = result.data.list;
                    }
                }
            }
            catch (Exception ex) { }
        }
        static string CatBoKiTuCuoiKhongPhaiChu(string str)
        {
            while (!string.IsNullOrEmpty(str) && !char.IsLetter(str[str.Length - 1]))
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }

        private async Task<bool> GetAllEmployee()
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
                    listAllEmployee1 = result.data.items;

                }
                return true;
            }
            catch
            {
                MessageBox.Show("lỗi lấy tất cả nhân viên");
            }
            return false;
        }
        private void pagin_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            GetHistoryCheckIn(pagin.SelectedPage);
        }

        private void btnTimKiem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetHistoryCheckIn();
        }

        private void btnXuatExcel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void dgHistoryCheckIn_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {

                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    var scrollViewer = FindVisualChild<ScrollViewer>(dgHistoryCheckIn);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - e.Delta);
                        e.Handled = true;
                    }

                }
                else
                {
                    Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
                }
            }
            catch { }
        }

        private T FindVisualChild<T>(DependencyObject visual) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(visual, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null)
                        return childItem;
                }
            }
            return null;
        }
    }
}
