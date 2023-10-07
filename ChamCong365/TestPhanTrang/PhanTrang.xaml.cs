using ChamCong365.APIs;
using ChamCong365.NhanVien.ChamCongBangTaiKhoanCongTy.Function;
using ChamCong365.OOP.ChamCong.CauHinhChamCong;
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
using static ChamCong365.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi;

namespace ChamCong365.TestPhanTrang
{
    /// <summary>
    /// Interaction logic for PhanTrang.xaml
    /// </summary>
    public partial class PhanTrang : UserControl
    {
        MainWindow Main;
        public PhanTrang(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            LoadListCompany();
            LoadListDepartment();
        }
        List<TestPhanTrang.API_DS_Cty> listCompany = new List<API_DS_Cty>();
        private async void LoadListCompany()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/company/child/list");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    //CompanyRoot result = JsonConvert.DeserializeObject<CompanyRoot>(responseContent);
                    //ChildCompanyList = result.data.items;
                    //lsvCompany.ItemsSource = ChildCompanyList;
                    API_DS_Cty result = JsonConvert.DeserializeObject<API_DS_Cty>(responseContent);
                    lsvCompany.ItemsSource = result.data.items;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load cty "+ex.Message);
            }
        }
        private async void LoadListDepartment()
        {
            try
            {

                var httpClient = new HttpClient();
                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Post;
                string api = API.list_department_api;

                request.RequestUri = new Uri(api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                DepartmentRoot result = JsonConvert.DeserializeObject<DepartmentRoot>(responseContent);

                //load dropdown box
                lsvPhongBan.ItemsSource = result.data.items;
               // lsvPhongBan.ItemsSource = DepartmentList;

            }
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi khi load danh sách phòng ban" + e.Message);
            }
        }
        private void borTenChonLoai_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            Company d = (sender as Border).DataContext as Company;
            if (d != null)
            {
                txbSelectCompany.Text = d.com_name;
                Company.Visibility = Visibility.Collapsed;
                //idTheoDoi = ((ListUsersTheoDoi)lsvNguoiTheoDoi.SelectedItem).idQLC;
            }
            
        }

        private void bodSelectCompany_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(Company.Visibility == Visibility.Visible)
            {
                Company.Visibility = Visibility.Collapsed;
            }
            else
            {
                Company.Visibility = Visibility.Visible;
            }
        }

        private void borTenChonCongTy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Department d = (sender as Border).DataContext as Department;    
            if(d != null)
            {
                txbSelectDepartment.Text = d.dep_name;
                PhongBan.Visibility = Visibility.Collapsed;
            }
        }

        private void bodSearchDepartment_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (PhongBan.Visibility == Visibility.Visible)
            {
                PhongBan.Visibility = Visibility.Collapsed;
            }
            else
            {
                PhongBan.Visibility = Visibility.Visible;
            }
        }
    }
}
