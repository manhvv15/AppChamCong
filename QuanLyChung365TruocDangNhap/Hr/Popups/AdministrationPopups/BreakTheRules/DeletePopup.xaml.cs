﻿using QuanLyChung365TruocDangNhap.Hr.Entities.AdministrationEntity.PersonnelChangeEntity;
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

namespace QuanLyChung365TruocDangNhap.Hr.Popups.AdministrationPopups.BreakTheRules
{
    /// <summary>
    /// Interaction logic for DeletePopup.xaml
    /// </summary>
    public partial class DeletePopup : UserControl
    {

        string ep_id;
        string id_com;

        string token;
        // deligate event hide popups
        public delegate void HidePopup(int mode); //0: 0 load lại, 1:load lại
        public static event HidePopup hidePopup;

        public DeletePopup(string token, string ep_id, string id_com)
        {
            InitializeComponent();
            this.token = token;
            this.ep_id = ep_id;
            this.id_com = id_com;
        }
        private void CancelPopup(object sender, MouseButtonEventArgs e)
        {
            hidePopup(0);
        }

        private void ClickDelete(object sender, MouseButtonEventArgs e)
        {
            DeleteData();
        }
        private async void DeleteData()
        {
            try
            {
                var httpClient = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Post;
                string api = "http://210.245.108.202:3006/api/hr/personalChange/deleteQuitJobNew";

                httpRequestMessage.RequestUri = new Uri(api);

                httpRequestMessage.Headers.Add("Authorization", "Bearer " + token);

                var parameters = new List<KeyValuePair<string, string>>();

                parameters.Add(new KeyValuePair<string, string>("ep_id", ep_id));
                parameters.Add(new KeyValuePair<string, string>("id_com", id_com));

                // Thiết lập Content
                var content = new FormUrlEncodedContent(parameters);
                httpRequestMessage.Content = content;

                // Thực hiện Post
                var response = await httpClient.SendAsync(httpRequestMessage);

                var responseContent = await response.Content.ReadAsStringAsync();

                SuccessDelete result = JsonConvert.DeserializeObject<SuccessDelete>(responseContent);
                if (result.data.result)
                {
                    hidePopup(1);
                    CustomMessageBox.Show(result.data.message);

                }
                else
                {
                    CustomMessageBox.Show("Xóa thất bại, vui lòng thử lại!");
                }

            }
            catch
            {
                CustomMessageBox.Show("Có lỗi xảy ra, vui lòng thử lại!");
            }
        }
    }
}
