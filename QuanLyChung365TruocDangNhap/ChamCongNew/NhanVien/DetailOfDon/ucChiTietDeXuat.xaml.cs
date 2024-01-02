using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.Propose;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.NhanVien;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.DetailOfDon
{
    /// <summary>
    /// Interaction logic for ucChiTietDeXuat.xaml
    /// </summary>
    public partial class ucChiTietDeXuat : UserControl
    {
        class RootMessage
        {
            public string message { get; set; }
        }
        MainChamCong Main;
        /// <summary>
        /// Main.Back = 7;
        /// </summary>
        ChiTietDeXuat.DetailDeXuat detailDeXuat = new ChiTietDeXuat.DetailDeXuat();
        int dx_id = 0;
        public bool isFromListDx;
        public ucChiTietDeXuat(MainChamCong Main, int dx_id)

        {
            InitializeComponent();
            this.Main = Main;
            this.dx_id = dx_id;
            CollapsedAllStatus();
            GetChiTietDeXuat();
            //Main.Back = 7;
        }
        public ucChiTietDeXuat(MainChamCong Main, int dx_id, bool isFromListDx)
        {
            InitializeComponent();
            this.Main = Main;
            this.dx_id = dx_id;
            CollapsedAllStatus();
            GetChiTietDeXuat();
            this.isFromListDx = isFromListDx;

        }

        public async void GetChiTietDeXuat()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/catedx/showCTDX");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(dx_id.ToString()), "_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ChiTietDeXuat.Root result = JsonConvert.DeserializeObject<ChiTietDeXuat.Root>(responseContent);
                    detailDeXuat = result.data.detailDeXuat[0];
                    ShowSameUseData(detailDeXuat);
                    ShowEachOtherDetail(detailDeXuat);

                }

            }
            catch (Exception ex)
            {
                //  MessageBox.Show("Có lỗi xảy ra" + ex.Message);
            }
        }
        public void ShowEachOtherDetail(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            int nhomDx = detailDeXuat.nhom_de_xuat;
            switch (nhomDx)
            {
                case 1:
                    ShowDetailXinNghiPhep(detailDeXuat);
                    break;
                case 2:
                    ShowDetailXinDoiCa(detailDeXuat);
                    break;
                case 3:
                    ShowDetailDonTamUng(detailDeXuat);
                    break;
                case 4:
                    ShowDetailXinCapPhatTaiSan(detailDeXuat);
                    break;
                case 5:
                    ShowDetailDonXinThoiViec(detailDeXuat);
                    break;
                case 6:
                    ShowDetailDeXuatTangLuong(detailDeXuat);
                    break;
                case 7:
                    ShowDetailBoNhiem(detailDeXuat);
                    break;
                case 8:
                    ShowDetaiLuanChuyen(detailDeXuat);
                    break;
                case 9:
                    ShowDetailDonThamGiaDuAn(detailDeXuat);
                    break;
                case 10:
                    ShowDetailDonTangCa(detailDeXuat);
                    break;
                case 11:
                    ShowDetailDonNghiThaiSan(detailDeXuat);
                    break;
                case 12:
                    ShowDetailSuDungPhongHop(detailDeXuat);
                    break;
                case 13:
                    ShowDetailSuDungXeCong(detailDeXuat);
                    break;
                case 14:
                    ShowDetailSuaChuaCoSoVatChat(detailDeXuat);
                    break;
                case 15:
                    ShowDetailDeXuatThanhToan(detailDeXuat);
                    break;
                case 16:
                    ShowDetailDeXuatKhieuNai(detailDeXuat);
                    break;
                case 17:
                    ShowDetailCongCong(detailDeXuat);
                    break;
                case 18:
                    ShowDetailLichLamViec(detailDeXuat);
                    break;
                case 19:
                    ShowDetailThuongPhat(detailDeXuat);
                    break;
                case 20:
                    ShowDetailHoaHong(detailDeXuat);
                    break;
                case 21:
                    ShowDetailDiMuonVeSom(detailDeXuat);
                    break;
                case 22:
                    ShowDetailNghiRaNgoai(detailDeXuat);
                    break;
                case 23:
                    ShowDetailNhapNgayNhanLuong(detailDeXuat);
                    break;
                case 24:
                    ShowDetailTaiLieu(detailDeXuat);
                    break;
            }

        }
        public async Task<int> GetEmployeeId()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/employee/info");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    EmployeeInfo.Root result = JsonConvert.DeserializeObject<EmployeeInfo.Root>(responseContent);
                    return result.data.data.idQLC.Value;
                }

            }
            catch { }
            return 0;
        }
        public async Task<List<ListCateDxEntites.Showcatedx>> GetListCateDx()
        {
            List<ListCateDxEntites.Showcatedx> list = new List<ListCateDxEntites.Showcatedx>();
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.showlistcate_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ListCateDxEntites.Root result = JsonConvert.DeserializeObject<ListCateDxEntites.Root>(responseContent);
                    list = result.data.showcatedx;
                    return list;
                }

            }
            catch { MessageBox.Show("Co loi"); }
            return list;
        }
        public void CollapsedAllStatus()
        {
            DangChoDuyet.Visibility = Visibility.Collapsed;
            DaDuocTiepNhan.Visibility = Visibility.Collapsed;
            ChoLanhDaoDuyet.Visibility = Visibility.Collapsed;
            ChoCongTyDuyet.Visibility = Visibility.Collapsed;
            DaChapThuan.Visibility = Visibility.Collapsed;
            BiTuChoi.Visibility = Visibility.Collapsed;
            BuocDiLam.Visibility = Visibility.Collapsed;
            btn_TiepNhan.Visibility = Visibility.Collapsed;
            btn_ChuyenTiep.Visibility = Visibility.Collapsed;
            btn_Duyet.Visibility = Visibility.Collapsed;
            Btn_TuChoi.Visibility = Visibility.Collapsed;
            Btn_XoaDeXuat.Visibility = Visibility.Collapsed;
            btn_BuocDiLam.Visibility = Visibility.Collapsed;
            DeXuatBiXoa.Visibility = Visibility.Collapsed;
            QuaHanDuyet.Visibility = Visibility.Collapsed;
            Btn_HuyDuyet.Visibility = Visibility.Collapsed;
        }
        private async void ShowSameUseData(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {

            int ep_id = await GetEmployeeId();
            var listCateDX = await GetListCateDx();
            var isLanhDao = detailDeXuat.lanh_dao_duyet.Where(x => x.idQLC == ep_id).FirstOrDefault();
            if (isLanhDao != null)
            {
                switch (detailDeXuat.type_duyet)
                {
                    case 0:
                        CollapsedAllStatus();
                        DangChoDuyet.Visibility = Visibility.Visible;
                        btn_TiepNhan.Visibility = Visibility.Visible;

                        if (detailDeXuat.nhom_de_xuat == 1) { btn_BuocDiLam.Visibility = Visibility.Visible; }
                        Btn_TuChoi.Visibility = Visibility.Visible;
                        Btn_XoaDeXuat.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        CollapsedAllStatus();
                        BiTuChoi.Visibility = Visibility.Visible;
                        Btn_XoaDeXuat.Visibility = Visibility.Visible;
                        break;

                    case 5:
                        CollapsedAllStatus();
                        DaChapThuan.Visibility = Visibility.Visible;
                        if (detailDeXuat.nhom_de_xuat == 1 ||
                            detailDeXuat.nhom_de_xuat == 2 ||
                            detailDeXuat.nhom_de_xuat == 13 ||
                            detailDeXuat.nhom_de_xuat == 16 ||
                            detailDeXuat.nhom_de_xuat == 21 ||
                            detailDeXuat.nhom_de_xuat == 22)
                        {
                            Btn_HuyDuyet.Visibility = Visibility.Visible;
                        }
                        else Btn_HuyDuyet.Visibility = Visibility.Collapsed;
                        Btn_XoaDeXuat.Visibility = Visibility.Visible;
                        break;
                    case 6:
                        CollapsedAllStatus();
                        BuocDiLam.Visibility = Visibility.Visible;
                        Btn_XoaDeXuat.Visibility = Visibility.Visible;
                        break;
                    case 7:
                        CollapsedAllStatus();
                        DaDuocTiepNhan.Visibility = Visibility.Visible;
                        btn_Duyet.Visibility = Visibility.Visible;
                        btn_ChuyenTiep.Visibility = Visibility.Visible;
                        if (detailDeXuat.nhom_de_xuat == 1) { btn_BuocDiLam.Visibility = Visibility.Visible; }
                        Btn_TuChoi.Visibility = Visibility.Visible;
                        Btn_XoaDeXuat.Visibility = Visibility.Visible;
                        break;
                    case 10:
                        if (detailDeXuat.confirm_status.Value)
                        {
                            CollapsedAllStatus();
                            ChoLanhDaoDuyet.Visibility = Visibility.Visible;
                            Btn_HuyDuyet.Visibility = Visibility.Visible;
                            Btn_XoaDeXuat.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            CollapsedAllStatus();
                            DaDuocTiepNhan.Visibility = Visibility.Visible;
                            btn_Duyet.Visibility = Visibility.Visible;

                            btn_ChuyenTiep.Visibility = Visibility.Visible;
                            Btn_TuChoi.Visibility = Visibility.Visible;
                            Btn_XoaDeXuat.Visibility = Visibility.Visible;
                        }
                        break;
                    case 11:
                        CollapsedAllStatus();
                        ChoCongTyDuyet.Visibility = Visibility.Visible;

                        if (detailDeXuat.nhom_de_xuat == 1 ||
                            detailDeXuat.nhom_de_xuat == 2 ||
                            detailDeXuat.nhom_de_xuat == 13 ||
                            detailDeXuat.nhom_de_xuat == 16 ||
                            detailDeXuat.nhom_de_xuat == 21 ||
                            detailDeXuat.nhom_de_xuat == 22)
                        {
                            Btn_HuyDuyet.Visibility = Visibility.Visible;
                        }
                        else Btn_HuyDuyet.Visibility = Visibility.Collapsed;
                        Btn_XoaDeXuat.Visibility = Visibility.Visible;
                        break;
                    default:
                        CollapsedAllStatus();
                        break;
                }
                if (detailDeXuat?.type_duyet == 0 || detailDeXuat?.type_duyet == 7)
                {
                    if ((bool)detailDeXuat?.qua_han_duyet)
                    {
                        btn_TiepNhan.Visibility = Visibility.Collapsed;
                        btn_ChuyenTiep.Visibility = Visibility.Collapsed;
                        btn_Duyet.Visibility = Visibility.Collapsed;
                        Btn_TuChoi.Visibility = Visibility.Collapsed;
                        QuaHanDuyet.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {

                switch (detailDeXuat.type_duyet)
                {
                    case 0:
                        CollapsedAllStatus();
                        DangChoDuyet.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        CollapsedAllStatus();
                        BiTuChoi.Visibility = Visibility.Visible;
                        break;

                    case 5:
                        CollapsedAllStatus();
                        DaChapThuan.Visibility = Visibility.Visible;
                        break;
                    case 6:
                        CollapsedAllStatus();
                        BuocDiLam.Visibility = Visibility.Visible;
                        break;
                    case 7:
                        CollapsedAllStatus();
                        DaDuocTiepNhan.Visibility = Visibility.Visible;

                        break;
                    case 10:
                        CollapsedAllStatus();
                        ChoLanhDaoDuyet.Visibility = Visibility.Visible;

                        break;
                    case 11:
                        CollapsedAllStatus();
                        ChoCongTyDuyet.Visibility = Visibility.Visible;

                        break;
                    default:
                        CollapsedAllStatus();
                        break;
                }
            }

            if (detailDeXuat?.del_type != 1)
            {
                CollapsedAllStatus();
                DeXuatBiXoa.Visibility = Visibility.Visible;
            }
            //same data left
            txbTenDeXuat.Text = detailDeXuat?.ten_de_xuat;
            txbNguoiTao.Text = detailDeXuat?.nguoi_tao;
            txbNhomDx.Text = listCateDX.Where(x => x._id == detailDeXuat?.nhom_de_xuat).FirstOrDefault()?.name_cate_dx;
            txbCreatedDate.Text = detailDeXuat.thoi_gian_tao;
            txbCapNhat.Text = detailDeXuat?.cap_nhat.ToString();

            //same data right
            txbKieuDuyet.Text = detailDeXuat?.kieu_phe_duyet.ToString();
            lsvLanhDaoDuyet.ItemsSource = detailDeXuat?.lanh_dao_duyet;
            lsvNguoiTheoDoi.ItemsSource = detailDeXuat?.nguoi_theo_doi;
            txbStatusNguoiTao.Text = detailDeXuat?.nguoi_tao;
            txbStatusCreateTime.Text = detailDeXuat.thoi_gian_tao;
            foreach (var item in detailDeXuat?.lich_su_duyet)
            {
                item.time = item.time.ToLocalTime();
            }
            lsvLichSuDuyet.ItemsSource = detailDeXuat?.lich_su_duyet;


        }
        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {

            try
            {

            if (isFromListDx)
            {
                this.Visibility = Visibility.Collapsed;
                Main.dopBody.Children.RemoveAt(1);
                    return;
                //toiGuiDi1.Visibility = Visibility.Visible;
                //ToiGuiDi ucDS = new ToiGuiDi(Main);
                //                    Main.dopBody.Children.RemoveAt(1);
                //object ContentDS = ucDS.Content;
                //ucDS.Content = null;
                //Main.dopBody.Children.Add(ContentDS as UIElement);
                //return;
            }
            this.Visibility = Visibility.Collapsed;
            Main.dopBody.Children.RemoveAt(1);
            //DeXuatCuaToi uc = new DeXuatCuaToi(Main);
            //                    Main.dopBody.Children.RemoveAt(1);
            //object Content = uc.Content;
            //uc.Content = null;
            //Main.dopBody.Children.Add(Content as UIElement);
            }
            catch { }   
        }
        public async void TiepNhanDX()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/editdx/edit_active");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(dx_id.ToString()), "_id");
                content.Add(new StringContent("6"), "type");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    ucChiTietDeXuat uc = new ucChiTietDeXuat(Main, dx_id);
                                        Main.dopBody.Children.RemoveAt(1);
                    object Content = uc.Content;
                    uc.Content = null;
                    Main.dopBody.Children.Add(Content as UIElement);
                }

            }
            catch { }
        }
        private void btnTiepNhan_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TiepNhanDX();
        }

        public async void ChapThuanDX()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/editdx/edit_active");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(dx_id.ToString()), "_id");
                content.Add(new StringContent("1"), "type");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responsiveContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    RootMessage rootMessage = JsonConvert.DeserializeObject<RootMessage>(responsiveContent);

                    Main.grShowPopup.Children.Add(new ucNotificationPopup(rootMessage.message));
                    ucChiTietDeXuat uc = new ucChiTietDeXuat(Main, dx_id);
                                        Main.dopBody.Children.RemoveAt(1);
                    object Content = uc.Content;
                    uc.Content = null;
                    Main.dopBody.Children.Add(Content as UIElement);
                }

            }
            catch { }
        }
        private void btnChapThuan_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChapThuanDX();
        }
        public async void TuChoiDX()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/editdx/edit_active");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(dx_id.ToString()), "_id");
                content.Add(new StringContent("2"), "type");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    ucChiTietDeXuat uc = new ucChiTietDeXuat(Main, dx_id);
                                        Main.dopBody.Children.RemoveAt(1);
                    object Content = uc.Content;
                    uc.Content = null;
                    Main.dopBody.Children.Add(Content as UIElement);
                }

            }
            catch { }
        }

        private void btnTuChoi_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TuChoiDX();
        }

        private void btnChuyenTiep_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucChuyenTiepDx(Main, dx_id));

        }

        private void btnXoaDX_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucDeleteDx(this, dx_id));
        }
        public void ShowDetailSuDungPhongHop(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailSuDungPhongHop uc = new ucDetailSuDungPhongHop(detailDeXuat);

            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);

        }
        public void ShowDetailXinNghiPhep(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailXinNghiPhep uc = new ucDetailXinNghiPhep(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);

        }

        public void ShowDetailXinDoiCa(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailXinDoiCa uc = new ucDetailXinDoiCa(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);

        }
        public void ShowDetailDonTamUng(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailDonTamUng uc = new ucDetailDonTamUng(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }

        public void ShowDetailXinCapPhatTaiSan(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailDonXinCapPhatTaiSan uc = new ucDetailDonXinCapPhatTaiSan(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }

        public void ShowDetailDonXinThoiViec(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailDonXinThoiViec uc = new ucDetailDonXinThoiViec(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }
        public void ShowDetailDeXuatTangLuong(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailDeXuatTangLuong uc = new ucDetailDeXuatTangLuong(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }
        public void ShowDetailBoNhiem(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailBoNhiem uc = new ucDetailBoNhiem(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }

        public void ShowDetaiLuanChuyen(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailLuanChuyen uc = new ucDetailLuanChuyen(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }
        public void ShowDetailDonThamGiaDuAn(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailThamGiaDuAn uc = new ucDetailThamGiaDuAn(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }
        public void ShowDetailDonTangCa(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailXinTangCa uc = new ucDetailXinTangCa(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }
        public void ShowDetailDonNghiThaiSan(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailXinNghiThaiSan uc = new ucDetailXinNghiThaiSan(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }
        public void ShowDetailSuDungXeCong(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailSuDungXeCong uc = new ucDetailSuDungXeCong(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }
        public void ShowDetailSuaChuaCoSoVatChat(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailSuaChuaCoSoVatChat uc = new ucDetailSuaChuaCoSoVatChat(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }
        public void ShowDetailDeXuatThanhToan(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailDeXuatThanhToan uc = new ucDetailDeXuatThanhToan(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }
        public void ShowDetailDeXuatKhieuNai(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailDeXuatKhieuNai uc = new ucDetailDeXuatKhieuNai(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }
        public void ShowDetailCongCong(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailCongCong uc = new ucDetailCongCong(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }
        public void ShowDetailThuongPhat(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailThuongPhat uc = new ucDetailThuongPhat(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }
        public void ShowDetailHoaHong(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailHoaHongDoanhThu uc = new ucDetailHoaHongDoanhThu(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }
        public void ShowDetailTaiLieu(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailDeXuatXinTaiLieu uc = new ucDetailDeXuatXinTaiLieu(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }
        public void ShowDetailDiMuonVeSom(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailDiMuonVeSom uc = new ucDetailDiMuonVeSom(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }
        public void ShowDetailNhapNgayNhanLuong(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailNhapNgayNhanLuong uc = new ucDetailNhapNgayNhanLuong(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }

        public void ShowDetailLichLamViec(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailLichLamViec uc = new ucDetailLichLamViec(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }
        public void ShowDetailNghiRaNgoai(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            ucDetailNghiRaNgoai uc = new ucDetailNghiRaNgoai(detailDeXuat);
            object Content = uc.Content;
            uc.Content = null;
            StackDetailContaint.Children.Clear();
            StackDetailContaint.Children.Add(Content as UIElement);
        }

        private void btnBuocDiLam_MouseUp(object sender, MouseButtonEventArgs e)
        {
            BatBuocDiLam();
        }
        public async void BatBuocDiLam()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/editdx/edit_active");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(dx_id.ToString()), "_id");
                content.Add(new StringContent("3"), "type");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responsiveContent = await response.Content.ReadAsStringAsync();
                    RootMessage rootMessage = JsonConvert.DeserializeObject<RootMessage>(responsiveContent);

                    Main.grShowPopup.Children.Add(new ucNotificationPopup(rootMessage.message));
                    ucChiTietDeXuat uc = new ucChiTietDeXuat(Main, dx_id);
                                        Main.dopBody.Children.RemoveAt(1);
                    object Content = uc.Content;
                    uc.Content = null;
                    Main.dopBody.Children.Add(Content as UIElement);
                }

            }
            catch { }
        }

        private void btnHuyDuyet_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucHuyDuyetDX(this, dx_id));
        }

    }
}
