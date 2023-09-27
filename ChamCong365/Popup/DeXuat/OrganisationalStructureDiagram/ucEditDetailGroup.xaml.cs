using ChamCong365.APIs;
using ChamCong365.OOP.funcQuanLyCongTy;
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

namespace ChamCong365.Popup.DeXuat.OrganisationalStructureDiagram
{
    /// <summary>
    /// Interaction logic for ucEditDetailNest.xaml
    /// </summary>
    public partial class ucEditDetailGroup : UserControl
    {
        ucOrganizationalchart ucOrganizationalchart;
        int com_id;
        int gr_id;
        public ucEditDetailGroup(ucOrganizationalchart ucOrganizationalchart, int com_id, organizationalStructure.InfoGroup group)
        {
            InitializeComponent();

            this.ucOrganizationalchart = ucOrganizationalchart;
            this.com_id = com_id;
            gr_id = group.gr_id;
            txbName.Text = group.gr_name;
            txbManager.Text = group.truong_nhom;
            txbDeputy.Text = group.pho_truong_nhom;
            txbTotalEp.Text = group.group_tong_nv.ToString();
            GetInfoGroup();
        }
        public async void GetInfoGroup()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.search_group_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id.ToString()), "com_id");
                content.Add(new StringContent(gr_id.ToString()), "gr_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    GroupRoot result = JsonConvert.DeserializeObject<GroupRoot>(responseContent);
                    txtNameDep.Text = result.data.data[0].dep_name;
                    txtNameNest.Text = result.data.data[0].team_name;
                }

            }
            catch
            {

            }
        }

        public async void EditDescription()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.edit_description_api);
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id.ToString()), "comId");
                content.Add(new StringContent(gr_id.ToString()), "groupId");
                string descriptionContent = new TextRange(
                rtbDescription.Document.ContentStart,
                rtbDescription.Document.ContentEnd
            ).Text;
                content.Add(new StringContent(descriptionContent), "description");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    this.Visibility = Visibility.Collapsed;
                    ucOrganizationalchart.LoadOrganizationalStructure();
                }

            }
            catch
            {

            }
        }

        private void CancelPopup(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Update(object sender, MouseButtonEventArgs e)
        {
            EditDescription();
        }
    }
}
