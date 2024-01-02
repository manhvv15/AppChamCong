
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager.ListCandidatesPopups.CandidateDetailPopups;
using Newtonsoft.Json;
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
using ListCandidateEntity = QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy.ListCandidateEntity;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.funcQuanLyCongTy.ListOfCandidatesPages
{
    /// <summary>
    /// Interaction logic for CandidateDetailPage.xaml
    /// </summary>
    public partial class CandidateDetailPage : Page
    {
        MainWindow Main;
        string token;
        string id;
        Candidate candidateDetailEntity;
        Dictionary<string, string> listAllEmployee = new Dictionary<string, string>();
        Dictionary<string, string> listRecruitmentNew = new Dictionary<string, string>();
        string url_cv = "";


        public CandidateDetailPage(MainWindow Main, string token, string id, Dictionary<string, string> listAllEmployee, Dictionary<string, string> listRecruitmentNew, bool perEdit)
        {
            InitializeComponent();
            this.Main = Main;
            this.token = token;
            this.id = id;
            this.listAllEmployee = listAllEmployee;
            this.listRecruitmentNew = listRecruitmentNew;
            if (perEdit)
            {
                gtBtnEdit.Visibility = Visibility.Visible;
            }
            else
            {
                gtBtnEdit.Visibility = Visibility.Collapsed;
            }
            GetData();

        }

        private async void GetData()
        {
            try
            {
                var httpClient = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Post;
                string api = APIs.API.list_candidate_api;

                httpRequestMessage.RequestUri = new Uri(api);
                httpRequestMessage.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);

                var parameters = new List<KeyValuePair<string, string>>();


                parameters.Add(new KeyValuePair<string, string>("canId", id));

                // Thiết lập Content
                var content = new FormUrlEncodedContent(parameters);
                httpRequestMessage.Content = content;

                // Thực hiện Post
                var response = await httpClient.SendAsync(httpRequestMessage);

                var responseContent = await response.Content.ReadAsStringAsync();

                ListCandidateEntity result = JsonConvert.DeserializeObject<ListCandidateEntity>(responseContent);
                candidateDetailEntity = result.success.data.data[0];
                BindingData();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        //Lưu dữ liệu 
        private void BindingData()
        {
            if (candidateDetailEntity == null) return;
            try
            {
                tbNameTitle.Text = candidateDetailEntity.name; //tên chi tiết ứng viên giai đoạn nhận hồ sơ
                //Thông tin ứng viên
                tbName.Text = candidateDetailEntity.name;
                tbID.Text = candidateDetailEntity.id;
                tbEmail.Text = candidateDetailEntity.email;
                tbPhone.Text = candidateDetailEntity.phone;
                tbSex.Text = candidateDetailEntity.can_gender;
                tbHometown.Text = candidateDetailEntity.hometown;
                tbSchool.Text = candidateDetailEntity.school;
                if (ConvertDate(candidateDetailEntity.can_birthday, "dd/MM/yyyy") != "")
                {
                    tbDateOfBirth.Text = ConvertDate(candidateDetailEntity.can_birthday, "dd/MM/yyyy");
                }
                if (candidateDetailEntity.hometown == null)
                {
                    tbHometown.Text = "Chưa cập nhật";
                }
                else
                {
                    tbHometown.Text = candidateDetailEntity.hometown;
                }
                tbEducation.Text = candidateDetailEntity.can_education;
                if (candidateDetailEntity.school == null)
                {
                    tbSchool.Text = "Chưa cập nhật";
                }
                else
                {
                    tbSchool.Text = candidateDetailEntity.school;

                }
                tbExp.Text = candidateDetailEntity.can_exp;
                tbMarried.Text = candidateDetailEntity.can_is_married;
                tbAddress.Text = candidateDetailEntity.can_address;
                //end


                //Thông tin tuyển dụng
                tbCvFrom.Text = candidateDetailEntity.cvFrom;
                tbTitle.Text = candidateDetailEntity.title;
                tbHR.Text = listAllEmployee[candidateDetailEntity.user_hiring];
                //end

                //Quá trình tuyển dụng
                if (ConvertDate(candidateDetailEntity.created_at, "dd/MM/yyyy") != "")
                {
                    tbTimeSendCV.Text = ConvertDate(candidateDetailEntity.created_at, "dd/MM/yyyy");
                }
                gridStar.DataContext = candidateDetailEntity;
                //end

                icSkillVote.ItemsSource = candidateDetailEntity.listSkill;
                string cv = candidateDetailEntity.cv;
                if (cv != null && cv != "")
                {
                    url_cv = APIs.API.api_cv + cv;
                    tbNoFile.Visibility = Visibility.Collapsed;
                    tbViewFile.Visibility = Visibility.Visible;
                }
                else
                {
                    tbNoFile.Visibility = Visibility.Visible;
                    tbViewFile.Visibility = Visibility.Collapsed;
                }

            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra,vui lòng kiểm tra lại!");
            }

        }

        //Định dạng datetime
        private string ConvertDate(string date, string format)
        {
            DateTime myDate;
            try
            {
                myDate = DateTime.ParseExact(date, "yyyy-MM-ddTHH:mm",
                                              System.Globalization.CultureInfo.InvariantCulture);
                return myDate.ToString(format);
            }
            catch
            {
                try
                {
                    myDate = DateTime.ParseExact(date, "yyyy-MM-dd",
                                                  System.Globalization.CultureInfo.InvariantCulture);
                    return myDate.ToString(format);
                }
                catch
                {
                    try
                    {
                        myDate = DateTime.ParseExact(date, "yyyy/MM/dd",
                                                      System.Globalization.CultureInfo.InvariantCulture);
                        return myDate.ToString(format);
                    }
                    catch
                    {
                        try
                        {
                            myDate = DateTime.ParseExact(date, "dd/MM/yyyy",
                                                          System.Globalization.CultureInfo.InvariantCulture);
                            return myDate.ToString(format);
                        }
                        catch
                        {
                            try
                            {
                                myDate = DateTime.ParseExact(date, "dd/MM/yyyy",
                                                              System.Globalization.CultureInfo.InvariantCulture);
                                return myDate.ToString(format);
                            }
                            catch
                            {
                                try
                                {
                                    myDate = DateTime.ParseExact(date, "M/d/yyyy",
                                                                  System.Globalization.CultureInfo.InvariantCulture);
                                    return myDate.ToString(format);
                                }
                                catch
                                {
                                    return "";
                                }
                            }
                        }
                    }
                }
            }

        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Page.ActualWidth <= 900)
            {
                stackPanelContainer.Orientation = Orientation.Vertical;
                scrollviewerContainer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                //content.Height = 700;
                gridLeft.Margin = new Thickness(0, 0, 0, 15);
                gridRight.Height = 216;
            }
            else
            {
                stackPanelContainer.Orientation = Orientation.Horizontal;
                //content.Height = Double.NaN;
                scrollviewerContainer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                gridLeft.Margin = new Thickness(0, 0, 20, 0);
                gridRight.Height = Double.NaN;
            }
        }



        private void NavigateBack(object sender, MouseButtonEventArgs e)
        {
            //foreach (Window window in Application.Current.Windows)
            //{
            //    if (window.GetType() == typeof(HomeView))
            //    {
            //        (window as HomeView).MainContent.NavigationService.GoBack();
            //    }
            //}
        }

        private void ViewFileCandidate(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = url_cv,
                UseShellExecute = true
            });
        }

        private void ShowPopupEdit(object sender, MouseButtonEventArgs e)
        {
            EditCandidateProfilePopup2 editCandidateProfilePopup = new EditCandidateProfilePopup2(token, listAllEmployee, listRecruitmentNew, candidateDetailEntity);
            Main.grShowPopup.Children.Add(editCandidateProfilePopup);
        }
    }
}
