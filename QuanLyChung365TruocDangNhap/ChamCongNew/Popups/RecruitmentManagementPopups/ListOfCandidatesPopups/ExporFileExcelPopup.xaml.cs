using QuanLyChung365TruocDangNhap.ChamCongNew.Pages.ManageRecruitmentPages.ListOfCandidatesPages;
using QuanLyChung365TruocDangNhap.ChamCongNew.Entities.ManageRecuitmentEntities.ListCandidateEntities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager;
using System.Reflection;
using Newtonsoft.Json;
using System.Net.Http;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popups.RecruitmentManagementPopups.ListOfCandidatesPopups
{
    /// <summary>
    /// Interaction logic for ExporFileExcelPopup.xaml
    /// </summary>
    public partial class ExporFileExcelPopup : UserControl
    {
        string token;
        string process_id;

        List<Items> listItem = new List<Items>();
        //Danh sách nhận hồ sơ
        List<DataEntity> listProfile = new List<DataEntity>();
        //Danh sách nhận việc
        List<DataEntity> listGetJob = new List<DataEntity>();
        //Danh sách trượt
        List<DataEntity> listFailJob = new List<DataEntity>();
        //Danh sách hủy
        List<DataEntity> listCancelJob = new List<DataEntity>();
        //Danh sách kí hợp đồng
        List<DataEntity> listContactJob = new List<DataEntity>();

        // deligate event hide popups
        public delegate void HidePopup(int mode); //0: 0 load lại, 1:load lại
        public static event HidePopup hidePopup;
        public ExporFileExcelPopup(string token, List<Items> listItem, List<DataEntity> listProfile, List<DataEntity> listGetJob, List<DataEntity> listFailJob, List<DataEntity> listCancelJob, List<DataEntity> listContactJob)
        {
            InitializeComponent();
            GetMainWindow();
            this.token = token;
            this.listItem = listItem;
            cbxstage.ItemsSource = this.listItem;
            this.listProfile = listProfile;
            this.listGetJob = listGetJob;
            this.listFailJob = listFailJob;
            this.listCancelJob = listCancelJob;
            this.listContactJob = listContactJob;
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
        private void CancelPopup(object sender, MouseButtonEventArgs e)
        {
            hidePopup(0);
        }
        private bool ValidateExcel()
        {
            if (cbxstage.SelectedIndex < 0)
            {
                Main.grShowPopup.Children.Add(new ucNotificationPopup("Vui lòng chọn giai đoạn!"));
                return false;
            }

            return true;
        }

        private async void clickExcel(object sender, MouseButtonEventArgs e)
        {
            if (ValidateExcel())
            {
                Microsoft.Win32.SaveFileDialog sv = new Microsoft.Win32.SaveFileDialog();
                sv.Filter = "Microsoft Excel Worksheet | *.xlsx";
                sv.FileName = "DataCandidate";
                if (sv.ShowDialog() == true)
                {
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    var file = new FileInfo(sv.FileName);
                    if (cbxstage.SelectedIndex.ToString() == "1")
                    {
                        int STT = 1;
                        List<DataEntity3> listDetail = new List<DataEntity3>();
                        foreach (var item in listGetJob)
                        {
                            listDetail.Add(await GetDataExcel(item.id));
                        }
                        var list = (from candidate in listDetail select new { STT = STT++, date = candidate.created_at, name = candidate.name, position_apply = candidate.new_title, school = candidate.school, gender = candidate.can_gender, apply_salary = candidate.salary_agree, agree_salary = candidate.salary_agree, cvFrom = candidate.cv_from, nameHr = candidate.user_hiring, link = "https://hungha365.com/phan-mem-nhan-su/quan-ly-tuyen-dung/danh-sach-ung-vien/chi-tiet-ung-vien/u" + candidate.id + "" }).ToList();

                        //var list = (from candidate in listGetJob select new { STT = STT++, date = candidate.created_at, name = candidate.name, position_apply = candidate.new_name, school = candidate.school, gender = candidate.Can_gender, apply_salary = candidate.salary_agree, agree_salary = candidate.salary_agree, nameHr = candidate.nameHr, link = "https://hungha365.com/phan-mem-nhan-su/quan-ly-tuyen-dung/danh-sach-ung-vien/chi-tiet-ung-vien/u" + candidate.id + "" }).ToList();
                        List<string> listHeader = new List<string>() { "STT", "Ngày/tháng/năm", "Họ và tên", "Vị trí", "Trường học", "Giới tính", "Mức lương mong muốn", "Mức lương thực", "Nguồn ứng viên", "Nhân viên tuyển dụng", "Link chi tiết ứng viên" };

                        ExportExcel(list, file, listHeader).ContinueWith(zz => this.Dispatcher.Invoke(() =>
                        {
                            //var x = new Popups.ConfirmBox();
                            //x.ConfirmTitle = "Xuất file excel";
                            //x.Message = "Xuất file excel thành công, bạn có muốn mở file không?";
                            //x.Accept = () =>
                            //{
                            //    System.Diagnostics.Process.Start(file.FullName);
                            //};
                            Main.grShowPopup.Children.Add(new ucNotificationPopup("Xuất file excel thành công!"));
                            hidePopup(1);
                        }));
                    }
                    else if (cbxstage.SelectedIndex.ToString() == "2")
                    {
                        int STT = 1;
                        List<DataEntity3> listDetail = new List<DataEntity3>();
                        foreach (var item in listFailJob)
                        {
                            listDetail.Add(await GetDataExcel(item.id));
                        }
                        var list = (from candidate in listDetail select new { STT = STT++, date = candidate.created_at, name = candidate.name, position_apply = candidate.new_title, school = candidate.school, gender = candidate.can_gender, apply_salary = candidate.salary_agree, agree_salary = candidate.salary_agree, cvFrom = candidate.cv_from, nameHr = candidate.user_hiring, link = "https://hungha365.com/phan-mem-nhan-su/quan-ly-tuyen-dung/danh-sach-ung-vien/chi-tiet-ung-vien/u" + candidate.id + "" }).ToList();

                        //var list = (from candidate in listGetJob select new { STT = STT++, date = candidate.created_at, name = candidate.name, position_apply = candidate.new_name, school = candidate.school, gender = candidate.Can_gender, apply_salary = candidate.salary_agree, agree_salary = candidate.salary_agree, nameHr = candidate.nameHr, link = "https://hungha365.com/phan-mem-nhan-su/quan-ly-tuyen-dung/danh-sach-ung-vien/chi-tiet-ung-vien/u" + candidate.id + "" }).ToList();
                        List<string> listHeader = new List<string>() { "STT", "Ngày/tháng/năm", "Họ và tên", "Vị trí", "Trường học", "Giới tính", "Mức lương mong muốn", "Mức lương thực", "Nguồn ứng viên", "Nhân viên tuyển dụng", "Link chi tiết ứng viên" };

                        ExportExcel(list, file, listHeader).ContinueWith(zz => this.Dispatcher.Invoke(() =>
                        {
                            //var x = new Popups.ConfirmBox();
                            //x.ConfirmTitle = "Xuất file excel";
                            //x.Message = "Xuất file excel thành công, bạn có muốn mở file không?";
                            //x.Accept = () =>
                            //{
                            //    System.Diagnostics.Process.Start(file.FullName);
                            //};
                            Main.grShowPopup.Children.Add(new ucNotificationPopup("Xuất file excel thành công!"));
                            hidePopup(1);
                        }));
                    }
                    else if (cbxstage.SelectedIndex.ToString() == "3")
                    {
                        int STT = 1;
                        List<DataEntity3> listDetail = new List<DataEntity3>();
                        foreach (var item in listCancelJob)
                        {
                            listDetail.Add(await GetDataExcel(item.id));
                        }
                        var list = (from candidate in listDetail select new { STT = STT++, date = candidate.created_at, name = candidate.name, position_apply = candidate.new_title, school = candidate.school, gender = candidate.can_gender, apply_salary = candidate.salary_agree, agree_salary = candidate.salary_agree, cvFrom = candidate.cv_from, nameHr = candidate.user_hiring, link = "https://hungha365.com/phan-mem-nhan-su/quan-ly-tuyen-dung/danh-sach-ung-vien/chi-tiet-ung-vien/u" + candidate.id + "" }).ToList();

                        //var list = (from candidate in listGetJob select new { STT = STT++, date = candidate.created_at, name = candidate.name, position_apply = candidate.new_name, school = candidate.school, gender = candidate.Can_gender, apply_salary = candidate.salary_agree, agree_salary = candidate.salary_agree, nameHr = candidate.nameHr, link = "https://hungha365.com/phan-mem-nhan-su/quan-ly-tuyen-dung/danh-sach-ung-vien/chi-tiet-ung-vien/u" + candidate.id + "" }).ToList();
                        List<string> listHeader = new List<string>() { "STT", "Ngày/tháng/năm", "Họ và tên", "Vị trí", "Trường học", "Giới tính", "Mức lương mong muốn", "Mức lương thực", "Nguồn ứng viên", "Nhân viên tuyển dụng", "Link chi tiết ứng viên" };

                        ExportExcel(list, file, listHeader).ContinueWith(zz => this.Dispatcher.Invoke(() =>
                        {
                            //var x = new Popups.ConfirmBox();
                            //x.ConfirmTitle = "Xuất file excel";
                            //x.Message = "Xuất file excel thành công, bạn có muốn mở file không?";
                            //x.Accept = () =>
                            //{
                            //    System.Diagnostics.Process.Start(file.FullName);
                            //};
                            Main.grShowPopup.Children.Add(new ucNotificationPopup("Xuất file excel thành công!"));
                            hidePopup(1);
                        }));
                    }
                    else if (cbxstage.SelectedIndex.ToString() == "4")
                    {
                        int STT = 1;
                        List<DataEntity3> listDetail = new List<DataEntity3>();
                        foreach (var item in listContactJob)
                        {
                            listDetail.Add(await GetDataExcel(item.id));
                        }
                        var list = (from candidate in listDetail select new { STT = STT++, date = candidate.created_at, name = candidate.name, position_apply = candidate.new_title, school = candidate.school, gender = candidate.can_gender, apply_salary = candidate.salary_agree, agree_salary = candidate.salary_agree, cvFrom = candidate.cv_from, nameHr = candidate.user_hiring, link = "https://hungha365.com/phan-mem-nhan-su/quan-ly-tuyen-dung/danh-sach-ung-vien/chi-tiet-ung-vien/u" + candidate.id + "" }).ToList();

                        //var list = (from candidate in listGetJob select new { STT = STT++, date = candidate.created_at, name = candidate.name, position_apply = candidate.new_name, school = candidate.school, gender = candidate.Can_gender, apply_salary = candidate.salary_agree, agree_salary = candidate.salary_agree, nameHr = candidate.nameHr, link = "https://hungha365.com/phan-mem-nhan-su/quan-ly-tuyen-dung/danh-sach-ung-vien/chi-tiet-ung-vien/u" + candidate.id + "" }).ToList();
                        List<string> listHeader = new List<string>() { "STT", "Ngày/tháng/năm", "Họ và tên", "Vị trí", "Trường học", "Giới tính", "Mức lương mong muốn", "Mức lương thực", "Nguồn ứng viên", "Nhân viên tuyển dụng", "Link chi tiết ứng viên" };

                        ExportExcel(list, file, listHeader).ContinueWith(zz => this.Dispatcher.Invoke(() =>
                        {
                            //var x = new Popups.ConfirmBox();
                            //x.ConfirmTitle = "Xuất file excel";
                            //x.Message = "Xuất file excel thành công, bạn có muốn mở file không?";
                            //x.Accept = () =>
                            //{
                            //    System.Diagnostics.Process.Start(file.FullName);
                            //};

                            Main.grShowPopup.Children.Add(new ucNotificationPopup("Xuất file excel thành công!"));
                            hidePopup(1);
                        }));
                    }
                    else
                    {
                        int STT = 1;
                        List<DataEntity3> listDetail = new List<DataEntity3>();
                        foreach (var item in listProfile)
                        {
                            listDetail.Add(await GetDataExcel(item.id));
                        }
                        var list = (from candidate in listDetail select new { STT = STT++, date = candidate.created_at, name = candidate.name, position_apply = candidate.new_title, school = candidate.school, gender = candidate.can_gender, apply_salary = candidate.salary_agree, agree_salary = candidate.salary_agree, cvFrom = candidate.cv_from, nameHr = candidate.user_hiring, link = "https://hungha365.com/phan-mem-nhan-su/quan-ly-tuyen-dung/danh-sach-ung-vien/chi-tiet-ung-vien/u" + candidate.id + "" }).ToList();

                        //var list = (from candidate in listGetJob select new { STT = STT++, date = candidate.created_at, name = candidate.name, position_apply = candidate.new_name, school = candidate.school, gender = candidate.Can_gender, apply_salary = candidate.salary_agree, agree_salary = candidate.salary_agree, nameHr = candidate.nameHr, link = "https://hungha365.com/phan-mem-nhan-su/quan-ly-tuyen-dung/danh-sach-ung-vien/chi-tiet-ung-vien/u" + candidate.id + "" }).ToList();
                        List<string> listHeader = new List<string>() { "STT", "Ngày/tháng/năm", "Họ và tên", "Vị trí", "Trường học", "Giới tính", "Mức lương mong muốn", "Mức lương thực", "Nguồn ứng viên", "Nhân viên tuyển dụng", "Link chi tiết ứng viên" };

                        ExportExcel(list, file, listHeader).ContinueWith(zz => this.Dispatcher.Invoke(() =>
                        {
                            //var x = new Popups.ConfirmBox();
                            //x.ConfirmTitle = "Xuất file excel";
                            //x.Message = "Xuất file excel thành công, bạn có muốn mở file không?";
                            //x.Accept = () =>
                            //{
                            //    System.Diagnostics.Process.Start(file.FullName);
                            //};
                            Main.grShowPopup.Children.Add(new ucNotificationPopup("Xuất file excel thành công!"));
                            hidePopup(1);
                        }));
                    }

                }
            }
        }

        private async Task ExportExcel<T>(List<T> data, FileInfo file, List<string> listHeader)
        {
            if (file.Exists)
            {
                file.Delete();
            }
            using (var package = new ExcelPackage(file))
            {
                var ws = package.Workbook.Worksheets.Add("Danh sách ứng viên theo giai đoạn");

                //ws.Cells["A1"].Value = "LỊCH SỬ ĐIỂM DANH";
                ws.Cells["A1:E1"].Merge = true;
                ws.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Row(1).Style.Font.Bold = true;
                ws.Row(1).Style.Font.Size = 13;

                ws.Cells["A2:E2"].Merge = true;
                ws.Row(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Row(2).Style.Font.Bold = true;
                ws.Row(2).Style.Font.Size = 13;
                for (int i = 0; i < listHeader.Count; i++)
                {
                    ws.Cells["A3"].Offset(0, i).Value = listHeader[i];
                }
                //ws.Cells["A3"].Offset(0, 0).Value = "Mã Ứng Viên";
                //ws.Cells["A3"].Offset(0, 1).Value = "Id";
                //ws.Cells["A3"].Offset(0, 2).Value = "Họ và tên";
                //ws.Cells["A3"].Offset(0, 3).Value = "Trường học";
                //ws.Cells["A3"].Offset(0, 4).Value = "Nguồn ứng viên";
                //ws.Cells["A3"].Offset(0, 5).Value = "Ngày/Tháng/Năm";
                ////ws.Cells["A3"].Offset(0, 6).Value = "";
                ////ws.Cells["A3"].Offset(0, 7).Value = "";
                ////ws.Cells["A3"].Offset(0, 8).Value = "";
                ////ws.Cells["A3"].Offset(0, 9).Value = "";
                ////ws.Cells["A3"].Offset(0, 10).Value = "";
                ////ws.Cells["A3"].Offset(0, 11).Value = "";
                ////ws.Cells["A3"].Offset(0, 12).Value = "";
                ////ws.Cells["A3"].Offset(0, 13).Value = "";
                ////ws.Cells["A3"].Offset(0, 14).Value = "";
                ////ws.Cells["A3"].Offset(0, 15).Value = "";
                ////ws.Cells["A3"].Offset(0, 16).Value = "";
                ////ws.Cells["A3"].Offset(0, 17).Value = "";
                ////ws.Cells["A3"].Offset(0, 18).Value = "";
                ////ws.Cells["A3"].Offset(0, 19).Value = "";
                ////ws.Cells["A3"].Offset(0, 20).Value = "";
                //ws.Cells["A3"].Offset(0, 21).Value = "Giới tính";
                ////ws.Cells["A3"].Offset(0, 22).Value = "";
                ////ws.Cells["A3"].Offset(0, 23).Value = "";
                ////ws.Cells["A3"].Offset(0, 24).Value = "";
                ////ws.Cells["A3"].Offset(0, 25).Value = "";
                ////ws.Cells["A3"].Offset(0, 26).Value = "";
                ////ws.Cells["A3"].Offset(0, 27).Value = "";
                ////ws.Cells["A3"].Offset(0, 28).Value = "";
                ////ws.Cells["A3"].Offset(0, 29).Value = "";
                ////ws.Cells["A3"].Offset(0, 30).Value = "";
                ////ws.Cells["A3"].Offset(0, 31).Value = "";
                ////ws.Cells["A3"].Offset(0, 32).Value = "";
                ////ws.Cells["A3"].Offset(0, 33).Value = "";
                //ws.Cells["A3"].Offset(0, 34).Value = "Nhân viên tuyển dụng";
                ////ws.Cells["A3"].Offset(0, 35).Value = "";
                ////ws.Cells["A3"].Offset(0, 36).Value = "";
                ////ws.Cells["A3"].Offset(0, 37).Value = "";
                ////ws.Cells["A3"].Offset(0, 38).Value = "";
                //ws.Cells["A3"].Offset(0, 39).Value = "Vị trí";


                ws.Row(3).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Row(3).Style.Font.Bold = true;
                ws.Row(3).Style.Font.Size = 13;

                for (int i = 0; i < data.Count; i++)
                {
                    ws.Row(i + 4).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    ws.Row(i + 4).Style.Font.Size = 13;
                }

                if (data == null)
                {
                    ws.Cells["A3"].Value = "Không có dữ liệu";
                }
                else
                {
                    var range = ws.Cells["A4"].LoadFromCollection(data);
                    ws.Cells["A3:" + range.End.Address].AutoFitColumns();
                }
                package.Save();
            }
        }
        static object GetPropertyValue(object obj, int index)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();

            if (index >= 0 && index < properties.Length)
            {
                return properties[index].GetValue(obj);
            }

            return null;
        }
        private async Task<DataEntity3> GetDataExcel(string id)
        {
            try
            {
                var httpClient = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Post;
                string api = APIs.API.detail_candidate_process;

                httpRequestMessage.RequestUri = new Uri(api);

                var parameters = new List<KeyValuePair<string, string>>();

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
                parameters.Add(new KeyValuePair<string, string>("id", id));
                //parameters.Add(new KeyValuePair<string, string>("process_id", process_id));

                // Thiết lập Content
                var content = new FormUrlEncodedContent(parameters);
                httpRequestMessage.Content = content;

                // Thực hiện Post
                var response = await httpClient.SendAsync(httpRequestMessage);

                var responseContent = await response.Content.ReadAsStringAsync();

                CandidateDetailEntity candidateDetailGetJob = JsonConvert.DeserializeObject<CandidateDetailEntity>(responseContent);
                DataEntity3 detail_candidate = candidateDetailGetJob.success.data.data.detail_candidate;
                return detail_candidate;
            }
            catch
            {
                //Main.grShowPopup.Children.Add(new ucNotificationPopup("Lỗi đăng nhập, vui lòng thử lại!");
            }
            return null;
        }
    }
}
