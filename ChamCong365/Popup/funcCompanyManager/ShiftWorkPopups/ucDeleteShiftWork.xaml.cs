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

namespace ChamCong365.Popup.funcCompanyManager.ShiftWorkPopups
{
    /// <summary>
    /// Interaction logic for ucDeleteShiftWork.xaml
    /// </summary>
    public partial class ucDeleteShiftWork : UserControl
    {
        ucShiftWorkManager ucShiftWorkManager;
        string shift_id;
        public ucDeleteShiftWork(ucShiftWorkManager ucShiftWorkManager, string shift_id)
        {
            InitializeComponent();
            this.ucShiftWorkManager = ucShiftWorkManager;
            this.shift_id = shift_id;
        }
        private async void DeleteShift()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, API.delete_shift_api);
            request.Headers.Add("Authorization", "Bearer "+Properties.Settings.Default.Token);
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(shift_id), "shift_id");
            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
            {
                ucShiftWorkManager.GetListShift();
            };

        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodHuyXoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodDongYXoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            DeleteShift();

        }
    }
}
