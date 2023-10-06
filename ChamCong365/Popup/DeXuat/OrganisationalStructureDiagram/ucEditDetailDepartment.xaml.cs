using ChamCong365.APIs;
using ChamCong365.OOP.funcQuanLyCongTy;
using Microsoft.Office.Interop.Excel;
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
    /// 

    public partial class ucEditDetailDepartment : UserControl
    {
        ucOrganizationalchart ucOrganizationalchart;
        int com_id;
        int dep_id;
        public ucEditDetailDepartment(ucOrganizationalchart ucOrganizationalchart, int com_id, organizationalStructure.InfoDep department)
        {
            InitializeComponent();

            this.ucOrganizationalchart = ucOrganizationalchart;
            this.com_id = com_id;
            dep_id = department.dep_id;
            txbName.Text = department.dep_name;
            txbManager.Text = department.manager;
            txbDeputy.Text = department.deputy;
            txbTotalEp.Text = department.total_emp.ToString();

        }

        public async void EditDescription(int dep_id)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.edit_description_api);
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id.ToString()), "comId");
                content.Add(new StringContent(dep_id.ToString()), "depId");
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
            EditDescription(dep_id);
        }
    }
}
