using ChamCong365.APIs;
using ChamCong365.OOP.funcQuanLyCongTy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ucCreateStage.xaml
    /// </summary>
    /// 

    public partial class ucUpdateStage : UserControl
    {
        ucCandidatesList ucCandidatesList;
        BrushConverter bc = new BrushConverter();
        int process_id = 0;
        public ucUpdateStage(ucCandidatesList ucCandidatesList, List<ListProcess.Process> list, ListProcess.Process process)
        {
            InitializeComponent();
            this.ucCandidatesList = ucCandidatesList;
            process_id = process.id;
            cbChonGiaiDoan.ItemsSource = list;
            txbName.Text = process.name;
            cbChonGiaiDoan.SelectedValue = process.id;
            cbChonGiaiDoan.Text = process.name;

        }

        private async void UpdateProcess()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.update_process);
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(process_id.ToString()), "processInterviewId");
                content.Add(new StringContent(txbName.Text), "name");
                content.Add(new StringContent(cbChonGiaiDoan.SelectedValue.ToString()), "processBefore");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    ucCandidatesList.GetListProcess();

                    this.Visibility = Visibility.Collapsed;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tạo giai đoạn mới");
            }
        }
        private void bodExitPopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        private void bodSelectStage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListStageCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListStageCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListStageCollapsed.Visibility -= Visibility.Collapsed;

            }
        }



        private void lsvStage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var selectedItem = (ListProcess.Process)lsvStage.SelectedItem;
            //if (selectedItem != null)
            //{
            //    this.process_id = selectedItem.id;
            //    txbSelectStage.Text = selectedItem.name;
            //    txbSelectStage.Foreground = (Brush)bc.ConvertFromString("#474747");
            //    bodListStageCollapsed.Visibility = Visibility.Collapsed;
            //}

        }

        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bodListStageCollapsed.Visibility = Visibility.Collapsed;
        }

        private void OK_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UpdateProcess();
        }
    }
}
