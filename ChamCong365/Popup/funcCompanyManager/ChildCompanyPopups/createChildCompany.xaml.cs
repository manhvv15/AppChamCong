using ChamCong365.APIs;
using ChamCong365.OOP.funcQuanLyCongTy;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace ChamCong365.Popup.funcCompanyManager.ChildCompanyPopups
{
    /// <summary>
    /// Interaction logic for createChildCompany.xaml
    /// </summary>
    public partial class createChildCompany : UserControl
    {
        string com_id;
        string file_logo_path;
        ucChildCompanyManagerment ucChildCompanyManagerment;
        public createChildCompany(ucChildCompanyManagerment ucChildCompanyManagerment, int com_id, List<Company> listChildCompany)
        {
            InitializeComponent();
            this.ucChildCompanyManagerment = ucChildCompanyManagerment;
            lsvSelectCompany.ItemsSource = ucChildCompanyManagerment.dataCompanySelectBox;
            this.com_id = com_id.ToString();
        }


        private async void CreateChildCompany()
        {
            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.create_child_Company_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(File.OpenRead(file_logo_path)), "avatarUser", file_logo_path);
                content.Add(new StringContent(txtName.Text), "com_name");
                content.Add(new StringContent(txtPhoneNumber.Text), "com_phone");
                content.Add(new StringContent(txtEmail.Text), "com_email");
                content.Add(new StringContent(txtAddress.Text), "com_address");
                content.Add(new StringContent(com_id), "com_parent_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    bodAddChildCompany.Visibility = Visibility.Collapsed;
                    bodThemThanhCong.Visibility = Visibility.Visible;
                    ucChildCompanyManagerment.GetListChildCompany();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm công ty con");
            }


        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {

            this.Visibility = Visibility.Collapsed;

        }

        private void btnExitPopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodSelectCompany_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //làm cho vùng đóng popup bao hết màn hình
            rectangleClosePopup.Margin = new Thickness(-2000);
            bodSelectCompanyCollapsed.Visibility = Visibility.Visible;
        }

        private void lsvSelectCompaty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedChildCompany = (Company)lsvSelectCompany.SelectedItem;
            com_id = selectedChildCompany.com_id.ToString();

            txbSelectCompany.Text = selectedChildCompany.com_name;
            txbSelectCompany.Foreground = new SolidColorBrush(Colors.Black);
            bodSelectCompanyCollapsed.Visibility = Visibility.Collapsed;

        }

        private void CloseSelectCompany_MouseDown(object sender, MouseButtonEventArgs e)
        {
            rectangleClosePopup.Margin = new Thickness(-0);
            bodSelectCompanyCollapsed.Visibility = Visibility.Collapsed;

        }

        private void bodBtnAdd_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CreateChildCompany();
        }

        private void bodThemThanhCong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Ok_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void ChooseLogo(object sender, MouseButtonEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            //dlg.DefaultExt = ".png";
            dlg.Filter = "Image files | *.bmp; *.jpg; *.gif; *.png; *.tif | All files | *.*";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.SafeFileName;
                file_logo_path = dlg.FileName;
                tbLogoName.Text = filename;
                imgLogo.Source = new BitmapImage(new Uri(file_logo_path));
            }
        }

    }
}
