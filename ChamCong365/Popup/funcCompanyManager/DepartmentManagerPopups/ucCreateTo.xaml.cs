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

namespace ChamCong365.Popup.funcCompanyManager
{
    /// <summary>
    /// Interaction logic for ucCreateDepartment.xaml
    /// </summary>
    public partial class ucCreateTo : UserControl
    {
        ucListTo ucListTo;
        BrushConverter brus = new BrushConverter();

        string com_id = "";
        string dep_id = "";
        public ucCreateTo(ucListTo ucListTo, string com_id)
        {
            InitializeComponent();
            this.ucListTo = ucListTo;
            this.com_id = com_id;
            lsvCompany.ItemsSource = ucListTo.dataCompanySelectBox;
            lsvDepartment.ItemsSource = ucListTo.DepartmentList;

        }

        private async void CreateTeam()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.create_team_api);
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id), "com_id");
                content.Add(new StringContent(txtTeamName.Text), "team_name");
                content.Add(new StringContent(dep_id), "dep_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    this.ucListTo.Main.grShowPopup.Children.Add(new ucCreateSuccess());
                    this.Visibility = Visibility.Collapsed;
                    ucListTo.LoadListTeam();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi thêm tổ");
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

        private void bodSelectCompany_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListCompany.Visibility == Visibility.Collapsed)
            {
                bodListCompany.Visibility = Visibility.Visible;
            }
            else
            {
                bodListCompany.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCompany = (Company)lsvCompany.SelectedItem;
            if (selectedCompany != null)
            {
                com_id = selectedCompany.com_id.ToString();
                txbSelectCompany.Text = selectedCompany.com_name;
            }

            txbSelectCompany.Foreground = (Brush)brus.ConvertFromString("#474747");
            bodListCompany.Visibility = Visibility.Collapsed;
        }

        private void bodSelectDepartment_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListDepartment.Visibility == Visibility.Collapsed)
            {
                bodListDepartment.Visibility = Visibility.Visible;
            }
            else
            {
                bodListDepartment.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Department)lsvDepartment.SelectedItem;
            if (selectedItem != null)
            {
                dep_id = selectedItem.dep_id.ToString();
                txbSelectDepartment.Text = selectedItem.dep_name;
            }
            txbSelectDepartment.Foreground = (Brush)brus.ConvertFromString("#474747");
            bodListDepartment.Visibility = Visibility.Collapsed;
        }

        private void Create_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CreateTeam();
        }
    }
}
