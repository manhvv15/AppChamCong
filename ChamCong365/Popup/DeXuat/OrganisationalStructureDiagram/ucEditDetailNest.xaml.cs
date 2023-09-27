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
    public partial class ucEditDetailNest : UserControl
    {
        ucOrganizationalchart ucOrganizationalchart;
        int com_id;
        int team_id;
        public ucEditDetailNest(ucOrganizationalchart ucOrganizationalchart, int com_id, organizationalStructure.InfoTeam team)
        {
            InitializeComponent();

            this.ucOrganizationalchart = ucOrganizationalchart;
            this.com_id = com_id;
            team_id = team.gr_id;
            txbName.Text = team.gr_name;
            txbManager.Text = team.to_truong;
            txbDeputy.Text = team.pho_to_truong;
            txbTotalEp.Text = team.tong_nv.ToString();
            GetInfoTeam();

        }

        public async void GetInfoTeam()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.team_list_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id.ToString()), "com_id");
                content.Add(new StringContent(team_id.ToString()), "team_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    TeamRoot result = JsonConvert.DeserializeObject<TeamRoot>(responseContent);
                    txtNameDep.Text = result.data.data[0].dep_name;
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
                content.Add(new StringContent(team_id.ToString()), "teamId");
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
