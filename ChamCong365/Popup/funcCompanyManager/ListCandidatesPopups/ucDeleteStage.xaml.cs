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

namespace ChamCong365.Popup.funcCompanyManager.ListCandidatesPopups
{
    /// <summary>
    /// Interaction logic for ucDeleteStage.xaml
    /// </summary>
    public partial class ucDeleteStage : UserControl
    {
        ucCandidatesList ucCandidatesList; 
        int id_process = 0;
        public ucDeleteStage(ucCandidatesList ucCandidatesList,int id_process)
        {
            InitializeComponent();
            this.ucCandidatesList = ucCandidatesList;
            this.id_process = id_process;
        }
        public async void DeleteProcess()
        {
            try {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.delete_process);
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(id_process.ToString()), "processInterId");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());

            }
            catch { }   
        }
        private void bodExitPopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Delete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DeleteProcess();
            ucCandidatesList.GetListProcess();
            this.Visibility=Visibility.Collapsed;

        }
    }
}
