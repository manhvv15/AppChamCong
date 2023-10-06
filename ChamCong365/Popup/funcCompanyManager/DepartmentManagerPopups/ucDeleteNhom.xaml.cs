using ChamCong365.APIs;
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

namespace ChamCong365.Popup.funcCompanyManager.DepartmentManagerPopups
{
    /// <summary>
    /// Interaction logic for ucDeleteDepartment.xaml
    /// </summary>
    public partial class ucDeleteNhom : UserControl
    {
        ucListGroup ucliaListGroup;
        string gr_id;
        public ucDeleteNhom(ucListGroup ucliaListGroup, int gr_id)
        {
            InitializeComponent();
            this.ucliaListGroup = ucliaListGroup;
            this.gr_id = gr_id.ToString();
        }
        private async void DeleteGroup()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Delete, API.delete_group_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(gr_id), "gr_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    this.Visibility = Visibility.Collapsed;
                    this.ucliaListGroup.LoadListGroup();
                }

            }
            catch
            {
                MessageBox.Show("Có lỗi khi xóa nhóm");
            }
        }
        private void btnExitPopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Ok_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DeleteGroup();
        }
    }
}
