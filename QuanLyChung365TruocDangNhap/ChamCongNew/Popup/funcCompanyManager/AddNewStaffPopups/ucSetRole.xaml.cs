using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
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
    /// Interaction logic for ucSetRole.xaml
    /// </summary>
    public partial class ucSetRole : UserControl
    {
        int ep_id;
        int role_id;
        ucTatCaNhanVien ucTatCaNhanVien;


        public ucSetRole(ucTatCaNhanVien ucTatCaNhanVien, int ep_id, int role_id)
        {
            InitializeComponent();
            this.ucTatCaNhanVien = ucTatCaNhanVien;
            this.ep_id = ep_id;
            this.role_id = role_id;
            cbRole.ItemsSource = ListItemCombobox.listCbxPermission;

            cbRole.SelectedItem = ListItemCombobox.listCbxPermission.FirstOrDefault(x => x.ID == role_id.ToString());


        }
        public async void EditUserRole()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/managerUser/updateAdmin");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent("{\r\n    \"listUsers\" : [" + ep_id + "],\r\n    \"newIsAdmin\":" + cbRole.SelectedValue + "\r\n}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                MessageEntities.Root result = JsonConvert.DeserializeObject<MessageEntities.Root>(responseContent);
                if (response.IsSuccessStatusCode)
                {
                    ucTatCaNhanVien.LoadDanhSachNhanVien();
                    this.Visibility = Visibility.Collapsed;
                    ucTatCaNhanVien.Main.grShowPopup.Children.Add(new ucPopupSuccess(result.data.message));
                }
                else
                {
                    ucTatCaNhanVien.Main.grShowPopup.Children.Add(new ucPopupError(result.error.message));
                }

            }
            catch (Exception ex) { ucTatCaNhanVien.Main.grShowPopup.Children.Add(new ucPopupError("Có lỗi xảy ra")); }
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
            EditUserRole();
            this.Visibility = Visibility.Collapsed;
        }
    }
}
