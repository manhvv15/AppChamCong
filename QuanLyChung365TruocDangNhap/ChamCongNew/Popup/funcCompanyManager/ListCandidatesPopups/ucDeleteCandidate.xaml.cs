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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager.ListCandidatesPopups
{
    /// <summary>
    /// Interaction logic for ucDeleteCandidate.xaml
    /// </summary>
    public partial class ucDeleteCandidate : UserControl
    {
        int canId = 0;
        ucCandidatesList ucCandidatesList;
        public ucDeleteCandidate(ucCandidatesList ucCandidatesList, int canid)
        {
            InitializeComponent();
            this.ucCandidatesList = ucCandidatesList;
            canId = canid;
        }
        private void bodExitPopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        public async void DeleteCandidate()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/hr/recruitment/softDeleteCandi");
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(canId.ToString()), "candidateId");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {

                    this.Visibility = Visibility.Collapsed;
                    this.ucCandidatesList.GetListProcess();

                }
            }
            catch (Exception ex) { }


        }

        private void DeleteCandidate(object sender, MouseButtonEventArgs e)
        {
            DeleteCandidate();
        }
    }
}
