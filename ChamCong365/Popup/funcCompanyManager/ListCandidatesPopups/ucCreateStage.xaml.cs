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

    public partial class ucCreateStage : UserControl
    {
        ucCandidatesList ucCandidatesList;
        BrushConverter bc = new BrushConverter();
        int process_id = 0;
        public ucCreateStage(ucCandidatesList ucCandidatesList, List<ListProcess.Process> list)
        {
            InitializeComponent();
            this.ucCandidatesList = ucCandidatesList;
            cbChonGiaiDoan.ItemsSource = list;

        }

        private async void CreateProcess()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.create_process_api);
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(txbName.Text), "name");
                content.Add(new StringContent(cbChonGiaiDoan.SelectedValue.ToString()), "processBefore");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    ucCandidatesList.GetListProcess();
                    MessageBox.Show("Thêm thành công");
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
            CreateProcess();
        }
    }
}
