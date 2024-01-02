
using QuanLyChung365TruocDangNhap.ChamCongNew.Entities.SettingEntities;
using QuanLyChung365TruocDangNhap.ChamCongNew.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.Pages.ManageRecruitmentPages.ListOfCandidatesPages;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.CoCau_ViTri_ToChuc;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
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
using static NPOI.HSSF.Util.HSSFColor;

namespace QuanLyChung365TruocDangNhap.ChamCongNew
{
    /// <summary>
    /// Interaction logic for ucFunctionCompanyManager.xaml
    /// </summary>
    public partial class ucFunctionCompanyManager : UserControl
    {
        MainWindow Main;
        ucBodyHome UcBodyHome;
        public bool[,] arrPermission = new bool[8, 5];
  
        public ucFunctionCompanyManager(MainWindow main, ucBodyHome ucBodyHome)
        {
            InitializeComponent();
            main.Back = 2;
            this.Main = main;
            this.UcBodyHome = ucBodyHome;
            GetUserPermission();
            Main.LableFunction.Visibility = Visibility.Visible;
            //Main.txbLoadNameFunction.Text = UcBodyHome.txbQuanLyCongTy.Text;
        }

        private async void GetUserPermission()
        {
            try
            {
                var httpClient = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Post;
                string api = APIs.API.apiCheckRole;
                httpRequestMessage.RequestUri = new Uri(api);
                var parameters = new List<KeyValuePair<string, string>>();
                httpRequestMessage.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                parameters.Add(new KeyValuePair<string, string>("userId", Main.IdAcount.ToString()));
                parameters.Add(new KeyValuePair<string, string>("winform", "1"));

                // Thiết lập Content
                var content = new FormUrlEncodedContent(parameters);
                httpRequestMessage.Content = content;
                var response = await httpClient.SendAsync(httpRequestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();

                PermissionEntity result = JsonConvert.DeserializeObject<PermissionEntity>(responseContent);
                if (result.result)
                {
                    foreach (var item in result.success.data.list_permision)
                    {
                        int i = Convert.ToInt32(item.bar_id);
                        int j = Convert.ToInt32(item.per_id);
                        arrPermission[i, j] = true;
                    }


                }
                else
                {

                }
            }
            catch
            {

            }
        }


        private void wapbuttonWorkShiftManager_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucShiftWorkManager uc = new ucShiftWorkManager(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            //Main.txbLoadNameFunction.Text = UcBodyHome.txbQuanLyCongTy.Text + " / " + txbFunction1.Text;
        }

        private void wapChildCompanyManager_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucChildCompanyManagerment uc = new ucChildCompanyManagerment(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            //Main.txbLoadNameFunction.Text = UcBodyHome.txbQuanLyCongTy.Text + " / " + txbFunction2.Text;

        }

        private void wapDepartmentManager_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucCoCauToChuc_ViTri uc = new ucCoCauToChuc_ViTri(this.Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            //Main.txbLoadNameFunction.Text = UcBodyHome.txbQuanLyCongTy.Text + " / " + txbFunction3.Text;

        }


        private void wapCandidatesList_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //ucCandidatesList uc = new ucCandidatesList(this.Main);
            ListOfCandidatesPage uc = new ListOfCandidatesPage(Properties.Settings.Default.Token, Main.IdAcount.ToString(), null, true, true, true);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            //Main.txbLoadNameFunction.Text = UcBodyHome.txbQuanLyCongTy.Text + " / " + txbFunction5.Text;

        }

        private void wapInstallNewStaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucInstallAddNewStaff uc = new ucInstallAddNewStaff(this.Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            //Main.txbLoadNameFunction.Text = UcBodyHome.txbQuanLyCongTy.Text + " / " + txbFunction4.Text;
        }

        private void wapInstallOrganize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucSoDoToChuc uc = new ucSoDoToChuc(this.Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void wapInstallPosition_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucSoDoViTRi uc = new ucSoDoViTRi(this.Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }
    }
}
