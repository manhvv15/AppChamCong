using ChamCong365.OOP.funcQuanLyCongTy;
//using ChamCong365.Popup.funcCompanyManager;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Pages.PageLichLamViecCaLamViec;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Tools;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.Comons;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
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
using static QRCoder.PayloadGenerator;
using static QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Tools.ucComboboxMuiltiSelect;

namespace QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Popup.LichLamViecPopups
{
    /// <summary>
    /// Interaction logic for ucThemNhanVienVaoLichLamViec.xaml
    /// </summary>
    public partial class ucThemNhanVienVaoLichLamViec : UserControl
    {
        frmMain Main;
        PageDanhSachLichLamViec pageDanhSachLichLamViec;
        int Cy_Id;
        string AppLy_Month;
        public ucThemNhanVienVaoLichLamViec(frmMain Main, PageDanhSachLichLamViec pageDanhSachLichLamViec, int id, string apply_Month)
        {
            InitializeComponent();
            this.Main = Main;
            this.Cy_Id = id;
            this.AppLy_Month = apply_Month;
            GetListEmployee();
        }

        #region CallApi
        public async void GetListEmployee()
        {
            try
            {

                var searchObject = new
                {
                    ep_status = "Active",
                    pageSize = 10000


                };
                string searchJson = JsonConvert.SerializeObject(searchObject, Formatting.Indented);

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.Api_DanhSachNhanVien);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var content = new StringContent(searchJson, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var resSaff = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Root_NhanVien resultSaff = JsonConvert.DeserializeObject<Root_NhanVien>(resSaff);

                    // Xử lý phản hồi ở đây
                    if (resultSaff.data.data != null)
                    {
                        List<ItemCbx> list = new List<ItemCbx>();
                        foreach (var item in resultSaff.data.data)
                        {
                            list.Add(new ItemCbx() { Key = item.ep_id.ToString(), Value = item.userName });
                        }

                        muiltiSelectStaff.ItemsSource = list;
                        //listUsersTheoDoi = lsvNguoiTheoDoi;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Đã xảy ra lỗi lấy danh sách ng duyệt " + ex.Message);
            }
        }


        public async void AddStaffToCycle()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/cycle/add_employee");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                string listStaffId = string.Join(",", muiltiSelectStaff.SelectList.Select(x => int.Parse(x.Key)));
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(listStaffId), "list_id");
                content.Add(new StringContent(Cy_Id.ToString()), "cy_id");

                content.Add(new StringContent(DateTime.Parse(AppLy_Month).ToString("yyyy-MM")), "curMonth");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                MessageEntities.Root resultMessage = JsonConvert.DeserializeObject<MessageEntities.Root>(responseContent);
                if (response.IsSuccessStatusCode)
                {

                    Main.pnlShowPopUp.Children.Add(new ucPopupSuccess(resultMessage.data.message));
                    this.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Main.pnlShowPopUp.Children.Add(new ucPopupError(resultMessage.error.message));
                }

            }
            catch (Exception ex) { Main.pnlShowPopUp.Children.Add(new ucPopupError("Có lỗi xảy ra")); }



        }

        #endregion
        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void ExitCreateSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodExitCreateSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void AddStaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AddStaffToCycle();
        }
    }
}
