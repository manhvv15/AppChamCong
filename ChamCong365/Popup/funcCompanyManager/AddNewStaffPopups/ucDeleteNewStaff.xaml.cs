using ChamCong365.APIs;
using ChamCong365.funcQuanLyCongTy.AddNewStaffTabList;
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
    /// Interaction logic for ucDeleteNewStaff.xaml
    /// </summary>
    public partial class ucDeleteNewStaff : UserControl
    {
        ucListAllStaff ucListAllstaff;
        int ep_id;
        public ucDeleteNewStaff(ucListAllStaff ucListAllstaff, int ep_id)
        {
            InitializeComponent();
            this.ucListAllstaff = ucListAllstaff;
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
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    ucListAllstaff.LoadListEmployee();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi xóa nhân viên");
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
