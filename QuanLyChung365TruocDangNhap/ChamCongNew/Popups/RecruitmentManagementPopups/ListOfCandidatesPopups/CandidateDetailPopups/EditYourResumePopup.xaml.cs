﻿using QuanLyChung365TruocDangNhap.ChamCongNew.Entities.ListItemCombobox;
using QuanLyChung365TruocDangNhap.ChamCongNew.Entities.ManageRecuitmentEntities.ListCandidateEntities;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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
using Application = System.Windows.Application;
using Window = System.Windows.Window;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popups.RecruitmentManagementPopups.ListOfCandidatesPopups.CandidateDetailPopups
{
    /// <summary>
    /// Interaction logic for EditYourResumePopup.xaml
    /// </summary>
    public partial class EditYourResumePopup : UserControl
    {
        string token;
        CandidateDetailEntity candidateDetailEntity;
        public Dictionary<string, string> listAllEmployee = new Dictionary<string, string>();
        // danh sách vị trí tuyển dụng
        public Dictionary<string, string> listRecruitment = new Dictionary<string, string>();
        int first_star_vote = 0;
        //int skill_vote = 0;

        List<Skill> listSkill = new List<Skill>();
        // deligate event hide popups
        public delegate void HidePopup(int mode);
        public static event HidePopup hidePopup;

        public EditYourResumePopup(string token, Dictionary<string, string> listAllEmployee, Dictionary<string, string> listRecruitment, CandidateDetailEntity candidateDetailEntity)
        {
            InitializeComponent();
            GetMainWindow();
            this.token = token;
            this.listAllEmployee = listAllEmployee;
            this.listRecruitment = listRecruitment;
            this.candidateDetailEntity = candidateDetailEntity;
            SetUpCombobox();
            BindingData();
            DisplayFirstStarVote(first_star_vote);
        }
        private MainWindow Main;
        public void GetMainWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    Main = (window as MainWindow);
                }
            }
        }
        private void SetUpCombobox()
        {
            cbxGender.ItemsSource = ListItemCombobox.ListCbxGender;
            cbxEducation.ItemsSource = ListItemCombobox.ListCbxEducation;
            cbxExp.ItemsSource = ListItemCombobox.ListCbxExp;
            cbxMarried.ItemsSource = ListItemCombobox.ListMarried;
            cbxRecruitment.ItemsSource = listAllEmployee;
            cbxRecommend.ItemsSource = listAllEmployee;
            cbxPosition.ItemsSource = listRecruitment;
            cbxInterview.ItemsSource = listAllEmployee;
        }

        private void BindingData()
        {
            if (candidateDetailEntity.success == null || candidateDetailEntity.success.data == null || candidateDetailEntity.success.data.data.detail_candidate == null) return;
            try
            {
                tbName.Text = candidateDetailEntity.success.data.data.detail_candidate.name;
                tbEmail.Text = candidateDetailEntity.success.data.data.detail_candidate.email;
                if (ConvertDate(candidateDetailEntity.success.data.data.detail_candidate.can_birthday, "dd/MM/yyyy") != "")
                {
                    dpDateOfBirth.Text = ConvertDate(candidateDetailEntity.success.data.data.detail_candidate.can_birthday, "dd/MM/yyyy");
                    dpDateOfBirth.SelectedDate = DateTime.Parse(ConvertDate(candidateDetailEntity.success.data.data.detail_candidate.can_birthday, "dd/MM/yyyy"));
                }
                tbResiredSalary.Text = candidateDetailEntity.success.data.data.detail_interview.resired_salary;
                tbSalary.Text = candidateDetailEntity.success.data.data.detail_interview.salary;
                tbPhone.Text = candidateDetailEntity.success.data.data.detail_candidate.phone;
                tbHomeTown.Text = candidateDetailEntity.success.data.data.detail_candidate.hometown;
                tbSchool.Text = candidateDetailEntity.success.data.data.detail_candidate.school;
                tbAddress.Text = candidateDetailEntity.success.data.data.detail_candidate.can_address;
                tbCVFrom.Text = candidateDetailEntity.success.data.data.detail_candidate.cv_from;
                cbxExp.SelectedItem = ListItemCombobox.ListCbxExp.FirstOrDefault(t => t.value == candidateDetailEntity.success.data.data.detail_candidate.can_exp);
                cbxEducation.SelectedItem = ListItemCombobox.ListCbxEducation.FirstOrDefault(t => t.value == candidateDetailEntity.success.data.data.detail_candidate.can_education);
                cbxMarried.SelectedItem = ListItemCombobox.ListMarried.FirstOrDefault(t => t.value == candidateDetailEntity.success.data.data.detail_candidate.can_is_married);
                cbxGender.SelectedItem = ListItemCombobox.ListCbxGender.FirstOrDefault(t => t.value == candidateDetailEntity.success.data.data.detail_candidate.can_gender);
                if (ConvertDate(candidateDetailEntity.success.data.data.detail_candidate.time_send_cv, "dd/MM/yyyy") != "")
                {
                    dpTimeSendCV.Text = ConvertDate(candidateDetailEntity.success.data.data.detail_candidate.time_send_cv, "dd/MM/yyyy");
                    dpTimeSendCV.SelectedDate = DateTime.Parse(ConvertDate(candidateDetailEntity.success.data.data.detail_candidate.time_send_cv, "dd/MM/yyyy"));
                }
                if (ConvertDate(candidateDetailEntity.success.data.data.detail_interview.created_at, "dd/MM/yyyy") != "")
                {
                    dpTimeSwitch.Text = ConvertDate(candidateDetailEntity.success.data.data.detail_interview.created_at, "dd/MM/yyyy");
                    dpTimeSwitch.SelectedDate = DateTime.Parse(ConvertDate(candidateDetailEntity.success.data.data.detail_interview.created_at, "dd/MM/yyyy"));
                }
                if (ConvertDate(candidateDetailEntity.success.data.data.detail_interview.interview_time, "dd/MM/yyyy") != "")
                {
                    dpDate.Text = ConvertDate(candidateDetailEntity.success.data.data.detail_interview.interview_time, "dd/MM/yyyy");
                    dpDate.SelectedDate = DateTime.Parse(ConvertDate(candidateDetailEntity.success.data.data.detail_interview.interview_time, "dd/MM/yyyy"));
                }
                tbNote.Text = candidateDetailEntity.success.data.data.detail_interview.note;
                cbxPosition.SelectedItem = listRecruitment.FirstOrDefault(t => t.Key == candidateDetailEntity.success.data.data.detail_candidate.recruitment_news_id);
                cbxRecruitment.SelectedItem = listAllEmployee.FirstOrDefault(t => t.Key == candidateDetailEntity.success.data.data.detail_candidate.user_hiring);
                cbxRecommend.SelectedItem = listAllEmployee.FirstOrDefault(t => t.Key == candidateDetailEntity.success.data.data.detail_candidate.user_recommend);
                cbxInterview.SelectedItem = listAllEmployee.FirstOrDefault(t => t.Key == candidateDetailEntity.success.data.data.detail_interview.ep_interview);
                Int32.TryParse(candidateDetailEntity.success.data.data.detail_candidate.star_vote, out first_star_vote);
                listSkill = candidateDetailEntity.success.data.list_skill;
                icListSkill.ItemsSource = listSkill;
            }
            catch
            {

            }

        }

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
                                    try
                                    {
                                        myDate = DateTime.Parse(date);
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

        }

        private void DisplayFirstStarVote(int star)
        {
            var converter = new BrushConverter();
            Brush brush = (Brush)converter.ConvertFromString("#FDBE1E");
            switch (star)
            {
                case 0:
                    star1.Fill = null;
                    star2.Fill = null;
                    star3.Fill = null;
                    star4.Fill = null;
                    star5.Fill = null;
                    break;
                case 1:
                    star1.Fill = brush;
                    star2.Fill = null;
                    star3.Fill = null;
                    star4.Fill = null;
                    star5.Fill = null;
                    break;
                case 2:
                    star1.Fill = brush;
                    star2.Fill = brush;
                    star3.Fill = null;
                    star4.Fill = null;
                    star5.Fill = null;
                    break;
                case 3:
                    star1.Fill = brush;
                    star2.Fill = brush;
                    star3.Fill = brush;
                    star4.Fill = null;
                    star5.Fill = null;
                    break;
                case 4:
                    star1.Fill = brush;
                    star2.Fill = brush;
                    star3.Fill = brush;
                    star4.Fill = brush;
                    star5.Fill = null;
                    break;
                case 5:
                    star1.Fill = brush;
                    star2.Fill = brush;
                    star3.Fill = brush;
                    star4.Fill = brush;
                    star5.Fill = brush;
                    break;
            }
        }

        private void CancelPopup(object sender, MouseButtonEventArgs e)
        {
            hidePopup(0);
        }

        private void ClickUpdate(object sender, MouseButtonEventArgs e)
        {
            if (ValidateForm())
                UpdateCandidate();
        }

        private async void UpdateCandidate()
        {
            try
            {
                var client = new HttpClient();
                var multiForm = new MultipartFormDataContent();
                multiForm.Add(new StringContent(candidateDetailEntity.success.data.data.detail_candidate.id), "canId");
                multiForm.Add(new StringContent(tbResiredSalary.Text), "resiredSalary");
                multiForm.Add(new StringContent(tbSalary.Text), "salary");
                multiForm.Add(new StringContent(dpDate.SelectedDate.Value.ToString("yyyy-MM-dd")), "interviewTime");
                multiForm.Add(new StringContent(cbxInterview.SelectedValue.ToString()), "empInterview");
                multiForm.Add(new StringContent(tbNote.Text), "note");
                multiForm.Add(new StringContent(candidateDetailEntity.success.data.data.detail_interview.process_interview_id), "processInterviewId");
                multiForm.Add(new StringContent(candidateDetailEntity.success.data.data.detail_candidate.name), "name");
                multiForm.Add(new StringContent(candidateDetailEntity.success.data.data.detail_candidate.star_vote), "starVote");
                multiForm.Add(new StringContent(candidateDetailEntity.success.data.data.detail_candidate.user_hiring), "userHiring");
                multiForm.Add(new StringContent(candidateDetailEntity.success.data.data.detail_candidate.cv_from), "cvFrom");
                multiForm.Add(new StringContent(candidateDetailEntity.success.data.data.detail_candidate.time_send_cv), "timeSendCv");
                multiForm.Add(new StringContent(candidateDetailEntity.success.data.data.detail_candidate.email), "email");
                multiForm.Add(new StringContent(candidateDetailEntity.success.data.data.detail_candidate.recruitment_news_id), "recruitmentNewsId");
                multiForm.Add(new StringContent("0"), "checkEmail");
                if (!string.IsNullOrEmpty(candidateDetailEntity.success.data.data.detail_candidate.contentsend))
                    multiForm.Add(new StringContent(candidateDetailEntity.success.data.data.detail_candidate.contentsend), "contentsend");
                else
                    multiForm.Add(new StringContent("1"), "contentsend");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                /*                parameters.Add(new KeyValuePair<string, string>("name", tbName.Text));
                                parameters.Add(new KeyValuePair<string, string>("starVote", first_star_vote.ToString()));
                                parameters.Add(new KeyValuePair<string, string>("userHiring", cbxHiring.SelectedValue.ToString()));
                                parameters.Add(new KeyValuePair<string, string>("recruitmentNewsId", cbxPosition.SelectedValue.ToString()));
                                parameters.Add(new KeyValuePair<string, string>("cvFrom", tbCVFrom.Text));
                                parameters.Add(new KeyValuePair<string, string>("timeSendCv", dpTimeSendCV.SelectedDate.Value.ToString("yyyy-MM-dd")));
                                parameters.Add(new KeyValuePair<string, string>("email", tbEmail.Text));*/

                // send request to API
                var url = APIs.API.edit_candidate_interview;
                var response = await client.PostAsync(url, multiForm);
                var responseContent = await response.Content.ReadAsStringAsync();
                ProcessEntity result = JsonConvert.DeserializeObject<ProcessEntity>(responseContent);
                if (result.data.result)
                {
                    hidePopup(1);
                    Main.grShowPopup.Children.Add(new ucNotificationPopup("Cập nhật thành công!"));
                }
                else
                {
                    Main.grShowPopup.Children.Add(new ucNotificationPopup(result.error.message));
                }
            }
            catch
            {
                Main.grShowPopup.Children.Add(new ucNotificationPopup("Có lỗi xảy ra, vui lòng thử lại!"));
            }
        }

        private bool ValidateForm()
        {
            if (tbResiredSalary.Text.Trim() == "")
            {
                Main.grShowPopup.Children.Add(new ucNotificationPopup("Vui lòng điền mức lương mong muốn."));
                return false;
            }

            if (tbSalary.Text.Trim() == "")
            {
                Main.grShowPopup.Children.Add(new ucNotificationPopup("Vui lòng điền mức lương thực."));
                return false;
            }

            if (dpDate.SelectedDate == null)
            {
                Main.grShowPopup.Children.Add(new ucNotificationPopup("Vui lòng điền thời gian hẹn."));
                return false;
            }

            if (cbxInterview.SelectedIndex == -1)
            {
                Main.grShowPopup.Children.Add(new ucNotificationPopup("Vui lòng chọn nhân viên tham gia."));
                return false;
            }

            return true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public class SkillPost
        {
            public string name { get; set; }
            public string star { get; set; }
        }

        private void cbxInterview_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxInterview.SelectedIndex = -1;
            string textSearch = cbxInterview.Text + e.Text;
            cbxInterview.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cbxInterview.Text = "";
                cbxInterview.Items.Refresh();
                cbxInterview.ItemsSource = listAllEmployee;
                cbxInterview.SelectedIndex = -1;
            }
            else
            {
                cbxInterview.ItemsSource = "";
                cbxInterview.Items.Refresh();
                cbxInterview.ItemsSource = listAllEmployee.Where(t => t.Value.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }

        private void cbxInterview_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cbxInterview.SelectedIndex = -1;
                string textSearch = cbxInterview.Text;
                cbxInterview.Items.Refresh();
                cbxInterview.ItemsSource = listAllEmployee.Where(t => t.Value.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }
    }
}