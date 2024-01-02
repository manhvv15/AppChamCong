using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.funcQuanLyCongTy.AddNewStaffTabList;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Them_Xoa_NhanVien;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager.AddNewStaffPopups
{
    /// <summary>
    /// Interaction logic for ucDeleteNewStaff.xaml
    /// </summary>
    public partial class ucDeleteNewStaff : UserControl
    {
        ucTatCaNhanVien ucTatCaNhanVien;
        int ep_id;
        public ucDeleteNewStaff(ucTatCaNhanVien ucTatCaNhanVien, int ep_id)
        {
            InitializeComponent();
            this.ucTatCaNhanVien = ucTatCaNhanVien;
            this.ep_id = ep_id;
        }
        private async void DeleteEmployee()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.managerUser_del_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(ep_id.ToString()), "idQLC");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                MessageEntities.Root result = JsonConvert.DeserializeObject<MessageEntities.Root>(responseContent);
                if (response.IsSuccessStatusCode)
                {

                    ucTatCaNhanVien.LoadDanhSachNhanVien();
                    ucTatCaNhanVien.ucInstallAddNewStaff.LoadDanhSachTatCaNhanVien();
                    ucTatCaNhanVien.Main.grShowPopup.Children.Add(new ucPopupSuccess(result.data.message));
                }
                else
                {
                    ucTatCaNhanVien.Main.grShowPopup.Children.Add(new ucPopupError(result.error.message));
                }


            }
            catch
            {
                ucTatCaNhanVien.Main.grShowPopup.Children.Add(new ucPopupError("Có lỗi xảy ra khi xóa nhân viên"));
            }

        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void ExitPopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;

        }

        private void OK_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DeleteEmployee();
            this.Visibility = Visibility.Collapsed;
        }
    }
}
