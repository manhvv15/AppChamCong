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
    public partial class updateChildCompany : UserControl
    {
        ucChildCompanyManagerment ucChildCompanyManagerment;
        string file_logo_path = "";
        Company childCompany;
        public updateChildCompany(ucChildCompanyManagerment ucChildCompanyManagerment, Company childCompany)
        {
            InitializeComponent();
            this.ucChildCompanyManagerment = ucChildCompanyManagerment;
            this.childCompany = childCompany;


            txtName.Text = childCompany.com_name;
            txxtEmail.Text = childCompany.com_email;
            txtPhoneNumber.Text = childCompany.com_phone;
            txtAddress.Text = childCompany.com_address;
        }

        private async void EditChildCompany()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.edit_child_company_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(childCompany.com_id.ToString()), "com_id");
                content.Add(new StreamContent(File.OpenRead(file_logo_path)), "avatarUser", file_logo_path);
                content.Add(new StringContent(txtName.Text), "userName");
                content.Add(new StringContent(txxtEmail.Text), "email");
                content.Add(new StringContent(txtPhoneNumber.Text), "phone");
                content.Add(new StringContent(txtAddress.Text), "address");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    bodUpdateChildCompany.Visibility = Visibility.Collapsed;
                    bodSuaThanhCong.Visibility = Visibility.Visible;
                    ucChildCompanyManagerment.GetListChildCompany();
                }

            }
            catch
            {
                MessageBox.Show("Có lối khi sửa");
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

        private void bodBtnUpdate_MouseUp(object sender, MouseButtonEventArgs e)
        {
            EditChildCompany();
        }

        private void OK_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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
