using ChamCong365.APIs;
using ChamCong365.OOP.funcQuanLyCongTy;
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

namespace ChamCong365.Popup.funcCompanyManager.AddNewStaffPopups
{
    /// <summary>
    /// Interaction logic for ucSetRole.xaml
    /// </summary>
    public partial class ucSetRole : UserControl
    {
        int ep_id;
        int role_id;
        public ucSetRole(int ep_id, int role_id)
        {
            InitializeComponent();
            this.ep_id = ep_id;
            this.role_id = role_id;
            cbRole.ItemsSource = ListItemCombobox.listCbxPermission;
            
            cbRole.SelectedIndex = role_id-1;


        }
        public async void EditUserRole()
        {
            try
            {
                if (cbRole.SelectedValue.Equals("1"))
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, API.setting_permision);
                    request.Headers.Add("authorization", "Bearer "+Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(ep_id.ToString()), "userId");
                    content.Add(new StringContent("1,2,3,4"), "role_td");
                    content.Add(new StringContent("1,2,3,4"), "role_ttns");
                    content.Add(new StringContent("1,2,3,4"), "role_ttvp");
                    content.Add(new StringContent("1,2,3,4"), "role_dldx");
                    content.Add(new StringContent("1,2,3,4"), "role_hnnv");
                    content.Add(new StringContent("1,2,3,4"), "role_bcns");
                    content.Add(new StringContent("1,2,3,4"), "role_tgl");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                }
                if (cbRole.SelectedValue.Equals("2"))
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, API.setting_permision);
                    request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(ep_id.ToString()), "userId");
                    content.Add(new StringContent(""), "role_td");
                    content.Add(new StringContent("1,2,3,4"), "role_ttns");
                    content.Add(new StringContent(""), "role_ttvp");
                    content.Add(new StringContent(""), "role_dldx");
                    content.Add(new StringContent(""), "role_hnnv");
                    content.Add(new StringContent(""), "role_bcns");
                    content.Add(new StringContent(""), "role_tgl");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                }
                if (cbRole.SelectedValue.Equals("3"))
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, API.setting_permision);
                    request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(ep_id.ToString()), "userId");
                    content.Add(new StringContent(""), "role_td");
                    content.Add(new StringContent(""), "role_ttns");
                    content.Add(new StringContent(""), "role_ttvp");
                    content.Add(new StringContent(""), "role_dldx");
                    content.Add(new StringContent(""), "role_hnnv");
                    content.Add(new StringContent(""), "role_bcns");
                    content.Add(new StringContent(""), "role_tgl");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                }
            }
            catch { MessageBox.Show("Có lỗi khi cấp quyền nhân viên"); }
        }
        public async void EditUserRole_IdField()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.edit_employee_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(cbRole.SelectedValue.ToString()), "role");
                content.Add(new StringContent(ep_id.ToString()), "ep_id");
                request.Content = content;
                var response = await client.SendAsync(request);
            }
            catch
            {
                MessageBox.Show("Có lỗi khi chỉnh sửa quyền");
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
            EditUserRole();
            EditUserRole_IdField();
            this.Visibility = Visibility.Collapsed;
        }
    }
}
