using Microsoft.Win32;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.CoCau_ViTri_ToChuc;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Comons;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Popups.PopupSoDoToChuc
{
    /// <summary>
    /// Interaction logic for ucThemToChuc.xaml
    /// </summary>
    public partial class ucThemToChuc : UserControl
    {
        string Token = "";
        string com_id = "";
        List<ListUserEntities.UserData> ListAllUser = new List<ListUserEntities.UserData>();
        BrushConverter br = new BrushConverter();
        MainWindow Main;
        API_OrganizeDetail.Organize organizeCreated = new API_OrganizeDetail.Organize();
        List<ListOrganizeEntities.OrganizeData> ListOrganizeData = new List<ListOrganizeEntities.OrganizeData>();
        List<ListOrganizeEntities.Content> themThongTin = new List<ListOrganizeEntities.Content>();
        Dictionary<string, string> ListOrganize = new Dictionary<string, string>();
        bool isEmptyOrganize = false;
        public class Saff1
        {
            public string name { get; set; }
            public string id { get; set; }
            public string tochuc { get; set; }
            public string vitri { get; set; }
        }
        List<Saff1> saffList1 = new List<Saff1>();
        public ucThemToChuc()
        {
            InitializeComponent();
            lsvAddInfo.ItemsSource = themThongTin;
            #region FACKE
            for (int i = 0; i < 10; i++)
            {
                saffList1.Add(new Saff1() { id = $"{i}", name = $"Ky{i}", tochuc = $"Tổ chức {i}", vitri = $"Nhân Viên Cấp {i}" });
            }
            lsvCheckNhanVien.ItemsSource = saffList1;
            #endregion
        }
        public ucThemToChuc(MainWindow Main,bool isEmptyOrganize)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Main = Main;
            this.isEmptyOrganize = isEmptyOrganize;
            this.Token = Properties.Settings.Default.Token;
            this.com_id = Main.IdAcount.ToString();
            GetListOrganize();
            GetAllUser();
        }

        #region Click Event
        private void btn_ThemThongTin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            themThongTin.Add(new ListOrganizeEntities.Content() { key = "", value = "" });
            lsvAddInfo.ItemsSource = themThongTin;
            lsvAddInfo.Items.Refresh();
        }

        private void XoaDoiTuong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = lsvAddInfo.SelectedItem as ListOrganizeEntities.Content;
            if (themThongTin.Count > 0)
                themThongTin.RemoveAt(themThongTin.IndexOf(selectedItem));
            lsvAddInfo.Items.Refresh();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;

        }

        private void bodExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodThemMoiToChuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (ValidateForm())
            {
                if (isEmptyOrganize)
                {
                    CreateFirstOrganize();  
                }
                else
                {

                CreateOrganize();
                }
            }
        }
        private void bodQuayLai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }


        private void bod_TimKiemNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            listSaff = new List<ListUserEntities.UserData>();
            foreach (var staff in ListAllUser)
            {
                if (staff.userName.ToLower().Contains(textTimKiemNhanVien.Text.ToLower()))
                {
                    listSaff.Add(staff);
                }
            }
            lsvCheckNhanVien.ItemsSource = listSaff;
            if (textTimKiemNhanVien.Text == "")
            {
                lsvCheckNhanVien.ItemsSource = ListAllUser;
            }
        }

        public bool shouldProcessEvent = true;
        private void btn_SelectListSafff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (shouldProcessEvent)
            {
                if (bod_DSNhanVien.Visibility == Visibility.Collapsed)
                {
                    bod_DSNhanVien.Visibility = Visibility.Visible;
                }
                else
                {
                    bod_DSNhanVien.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void lsvCheckNhanVien_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //scrollListNhanVien.ScrollToVerticalOffset(scrollListNhanVien.VerticalOffset - e.Delta);
        }
        private void popup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bod_DSNhanVien.Visibility == Visibility.Visible)
            {
                bod_DSNhanVien.Visibility = Visibility.Collapsed;
            }
        }
        private void xoaNvDaChon_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        List<Saff1> lstChonNhanVien = new List<Saff1>();
        private void lsvCheckNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (lsvCheckNhanVien.SelectedItem != null)
                {
                    var chonNV = lsvCheckNhanVien.SelectedItem.ToString();
                    if (!lstChonNhanVien.Any(item => item.name == chonNV))
                    {
                        Saff1 infor = new Saff1()
                        {
                            name = ((Saff1)lsvCheckNhanVien.SelectedItem).name,
                            id = ((Saff1)lsvCheckNhanVien.SelectedItem).id,
                        };

                        lstChonNhanVien.Add(infor);
                        lstChonNhanVien = lstChonNhanVien.ToList();
                        lsvNhanVienDuocChon.ItemsSource = lstChonNhanVien;
                        lsvNhanVienDuocChon.Visibility = Visibility.Visible;
                        bod_DSNhanVien.Visibility = Visibility.Collapsed;

                    }
                }
            }
            catch { }
        }
        private void xoaNhanVienDuocChon_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Saff1 index = (Saff1)lsvNhanVienDuocChon.SelectedItem;
            if (index != null)
            {
                lstChonNhanVien.Remove(index);
                lsvNhanVienDuocChon.ClearValue(ItemsControl.ItemsSourceProperty);
                lsvNhanVienDuocChon.ItemsSource = lstChonNhanVien;
                shouldProcessEvent = false;
            }
            shouldProcessEvent = true;
            if (lstChonNhanVien.Count == 0)
            {
                lsvNhanVienDuocChon.Visibility = Visibility.Collapsed;

            }
        }
        List<ListUserEntities.UserData> listSaff = new List<ListUserEntities.UserData>();
        private void textTimKiemNhanVien_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textTimKiemNhanVien.Text == "")
            {
                lsvCheckNhanVien.ItemsSource = ListAllUser;
            }
        }

        private void cboValueInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = (ComboBox)sender;

            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);
            if (row != null)
            {
                ComboBox cboValueInput = FindChild<ComboBox>(row, "cboValueInput");
                Border bod_TextInput = FindChild<Border>(row, "bod_TextInput");
                Border bod_AnhInput = FindChild<Border>(row, "bod_AnhInput");
                if (cboValueInput != null && cboValueInput.SelectedIndex == 0)
                {
                    bod_TextInput.Visibility = Visibility.Visible;
                    bod_AnhInput.Visibility = Visibility.Collapsed;
                }
                else
                {
                    bod_TextInput.Visibility = Visibility.Collapsed;
                    bod_AnhInput.Visibility = Visibility.Visible;

                    bod_TextInput = null;
                    bod_AnhInput = null;
                }
            }
        }

        #endregion

        #region Hover Event

        private void bodQuayLai_MouseEnter(object sender, MouseEventArgs e)
        {
            bodQuayLai.Background = (Brush)br.ConvertFrom("#4C5DB4");
            tb_QuayLai.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void bodQuayLai_MouseLeave(object sender, MouseEventArgs e)
        {
            bodQuayLai.Background = (Brush)br.ConvertFrom("#FFFFFF");
            tb_QuayLai.Foreground = (Brush)br.ConvertFrom("#4C5DB4");
        }

        private void bodLuuThongTinNhanVien_MouseEnter(object sender, MouseEventArgs e)
        {
            bodLuuThongTinNhanVien.BorderThickness = new Thickness(1);
            bodLuuThongTinNhanVien.Background = (Brush)br.ConvertFrom("#339DFA");
        }

        private void bodLuuThongTinNhanVien_MouseLeave(object sender, MouseEventArgs e)
        {
            bodLuuThongTinNhanVien.BorderThickness = new Thickness(0);
            bodLuuThongTinNhanVien.Background = (Brush)br.ConvertFrom("#4C5DB4");
        }

        private void btn_ThemThongTin_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_ThemThongTin.BorderThickness = new Thickness(1);
            btn_ThemThongTin.Background = (Brush)br.ConvertFrom("#FF6048");
        }

        private void btn_ThemThongTin_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_ThemThongTin.BorderThickness = new Thickness(0);
            btn_ThemThongTin.Background = (Brush)br.ConvertFrom("#FF7A00");
        }

        private void bod_ThemToChuc_MouseEnter(object sender, MouseEventArgs e)
        {
            bod_ThemToChuc.BorderThickness = new Thickness(1.5);
            bod_ThemToChuc.BorderBrush = (Brush)br.ConvertFrom("#4C5DB4");
        }

        private void bod_ThemToChuc_MouseLeave(object sender, MouseEventArgs e)
        {
            if (focus1 == false)
            {
                bod_ThemToChuc.BorderThickness = new Thickness(1);
                bod_ThemToChuc.BorderBrush = (Brush)br.ConvertFrom("#666666");
            }
        }
        bool focus1 = false;
        private void textNhapTenCapToChuc_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            bod_ThemToChuc.BorderThickness = new Thickness(1.5);
            bod_ThemToChuc.BorderBrush = (Brush)br.ConvertFrom("#4C5DB4");
            focus1 = true;
        }

        private void textNhapTenCapToChuc_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            bod_ThemToChuc.BorderThickness = new Thickness(1);
            bod_ThemToChuc.BorderBrush = (Brush)br.ConvertFrom("#666666");
            focus1 = false;
        }

        private void cboLeverCap_MouseEnter(object sender, MouseEventArgs e)
        {
            cboLeverCap.BorderThickness = new Thickness(1.5);
            cboLeverCap.BorderBrush = (Brush)br.ConvertFrom("#4C5DB4");
        }
        private void cboLeverCap_MouseLeave(object sender, MouseEventArgs e)
        {
            if (focus2 == false)
            {
                cboLeverCap.BorderThickness = new Thickness(1);
                cboLeverCap.BorderBrush = (Brush)br.ConvertFrom("#666666");
            }
        }

        bool focus2 = false;
        private void cboLeverCap_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboLeverCap.SelectedItem != null)
            {
                cboLeverCap.BorderThickness = new Thickness(1.5);
                cboLeverCap.BorderBrush = (Brush)br.ConvertFrom("#4C5DB4");
                focus2 = true;
            }
            else
            {
                cboLeverCap.BorderThickness = new Thickness(1);
                cboLeverCap.BorderBrush = (Brush)br.ConvertFrom("#666666");
                focus2 = false;
            }
        }

        private void cboLeverCap_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            cboLeverCap.BorderThickness = new Thickness(1.5);
            cboLeverCap.BorderBrush = (Brush)br.ConvertFrom("#4C5DB4");
            focus2 = true;
        }

        private void cboLeverCap_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            cboLeverCap.BorderThickness = new Thickness(1);
            cboLeverCap.BorderBrush = (Brush)br.ConvertFrom("#666666");
            focus2 = false;
        }

        private void cboCacCapToChuc_MouseEnter(object sender, MouseEventArgs e)
        {
            cboCacCapToChuc.BorderThickness = new Thickness(1.5);
            cboCacCapToChuc.BorderBrush = (Brush)br.ConvertFrom("#4C5DB4");
        }
        private void cboCacCapToChuc_MouseLeave(object sender, MouseEventArgs e)
        {

            cboCacCapToChuc.BorderThickness = new Thickness(1);
            cboCacCapToChuc.BorderBrush = (Brush)br.ConvertFrom("#666666");

        }

        bool focus3 = false;
        private void cboCacCapToChuc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboCacCapToChuc.SelectedIndex < 0)
            {
                cboCacCapToChuc.BorderThickness = new Thickness(1.5);
                cboCacCapToChuc.BorderBrush = (Brush)br.ConvertFrom("#4C5DB4");
                focus3 = true;
            }
            else
            {
                cboCacCapToChuc.BorderThickness = new Thickness(1);
                cboCacCapToChuc.BorderBrush = (Brush)br.ConvertFrom("#666666");
                focus3 = false;
            }
        }

        private void cboCacCapToChuc_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            cboCacCapToChuc.BorderThickness = new Thickness(1.5);
            cboCacCapToChuc.BorderBrush = (Brush)br.ConvertFrom("#4C5DB4");
            focus3 = true;
        }


        private void bod_MoTa_MouseEnter(object sender, MouseEventArgs e)
        {
            bod_MoTa.BorderThickness = new Thickness(1.5);
            bod_MoTa.BorderBrush = (Brush)br.ConvertFrom("#4C5DB4");
        }

        private void bod_MoTa_MouseLeave(object sender, MouseEventArgs e)
        {
            if (focus4 == false)
            {
                bod_MoTa.BorderThickness = new Thickness(1);
                bod_MoTa.BorderBrush = (Brush)br.ConvertFrom("#666666");
            }
        }
        bool focus4 = false;
        private void textMoTa_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            bod_MoTa.BorderThickness = new Thickness(1.5);
            bod_MoTa.BorderBrush = (Brush)br.ConvertFrom("#4C5DB4");
            focus4 = true;
        }

        private void textMoTa_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            bod_MoTa.BorderThickness = new Thickness(1);
            bod_MoTa.BorderBrush = (Brush)br.ConvertFrom("#666666");
            focus4 = false;
        }

        private void bod_TimKiemNhanVien_MouseEnter(object sender, MouseEventArgs e)
        {
            bod_TimKiemNhanVien.BorderThickness = new Thickness(1.5);
            bod_TimKiemNhanVien.BorderBrush = (Brush)br.ConvertFrom("#4C5DB4");
        }

        private void bod_TimKiemNhanVien_MouseLeave(object sender, MouseEventArgs e)
        {
            if (focus5 == false)
            {
                bod_TimKiemNhanVien.BorderThickness = new Thickness(1);
                bod_TimKiemNhanVien.BorderBrush = (Brush)br.ConvertFrom("#666666");
            }
        }
        bool focus5 = false;
        private void textTimKiemNhanVien_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            bod_TimKiemNhanVien.BorderThickness = new Thickness(1.5);
            bod_TimKiemNhanVien.BorderBrush = (Brush)br.ConvertFrom("#4C5DB4");
            focus5 = true;
        }

        private void textTimKiemNhanVien_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            bod_TimKiemNhanVien.BorderThickness = new Thickness(1.5);
            bod_TimKiemNhanVien.BorderBrush = (Brush)br.ConvertFrom("#4C5DB4");
            focus5 = false;
        }
        #endregion

        #region Methond Comons
        // Hàm giúp tìm kiếm đối tượng con trong VisualTree
        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T typedChild && (child as FrameworkElement)?.Name == childName)
                {
                    return typedChild;
                }
                else
                {
                    T childOfChild = FindChild<T>(child, childName);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        // Hàm giúp tìm cha của một đối tượng trong VisualTree
        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }







        #endregion
        #region CallApi
        public async void GetAllUser()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.userManager_listUsers);
                request.Headers.Add("authorization", "Bearer " + Token);
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    ListUserEntities.Root result = JsonConvert.DeserializeObject<ListUserEntities.Root>(responseContent);
                    ListAllUser = result.data.data;
                    lsvCheckNhanVien.ItemsSource = ListAllUser;
                }

            }
            catch
            {

            }
        }
        public async void CreateOrganize()
        {
            try
            {

                    string OrganizeName = textNhapTenCapToChuc.Text;
                    ListOrganizeEntities.OrganizeData Organize = cboCacCapToChuc.SelectedItem as ListOrganizeEntities.OrganizeData;
                    int OrganizeParentId = (int)Organize?.parentId;
                    int OrganizeLevel = (int)Organize?.level;
                    if (cboLeverCap.SelectedIndex == 0)
                    {
                        OrganizeParentId = (int)Organize?.parentId;
                        OrganizeLevel = (int)Organize?.level;
                    }
                    else if (cboLeverCap.SelectedIndex == 1)
                    {
                        OrganizeParentId = (int)Organize?.id;
                        OrganizeLevel = (int)Organize?.level + 1;
                    }
                    List<ListOrganizeEntities.ListOrganizeDetailId> listOrganizeDetailId = Organize.listOrganizeDetailId;
                    var listContent = themThongTin.Prepend(new ListOrganizeEntities.Content() { key = "Mô tả", value = textMoTa.Text });
                    dynamic DataObject = new { };

                    DataObject = new
                    {
                        organizeDetailName = OrganizeName,
                        content = listContent,
                        parentId = OrganizeParentId,
                        level = OrganizeLevel,
                        listOrganizeDetailId = listOrganizeDetailId,
                    };



                    string json = JsonConvert.SerializeObject(DataObject);


                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.Api_Create_Org);
                    request.Headers.Add("Authorization", "Bearer " + Token);
                    var content = new StringContent(json, null, "application/json");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        API_OrganizeDetail.Root result = JsonConvert.DeserializeObject<API_OrganizeDetail.Root>(responseContent);
                        organizeCreated = result.data.data;
                        AddEmployeeToOrganize();
                        this.Visibility = Visibility.Collapsed;
                        ucSoDoToChuc sodo = new ucSoDoToChuc(Main);
                        Main.dopBody.Children.Clear();
                        object Content = sodo.Content;
                        sodo.Content = null;
                        Main.dopBody.Children.Add(Content as UIElement);
                    }
                

            }
            catch { }
        }

        public async void CreateFirstOrganize()
        {
            try
            {

                string OrganizeName = textNhapTenCapToChuc.Text;

                List<ListOrganizeEntities.ListOrganizeDetailId> listOrganizeDetailId = new List<ListOrganizeEntities.ListOrganizeDetailId>();
                var listContent = themThongTin.Prepend(new ListOrganizeEntities.Content() { key = "Mô tả", value = textMoTa.Text });
                dynamic DataObject = new { };

                DataObject = new
                {
                    organizeDetailName = OrganizeName,
                    content = listContent,
                    parentId = 0,
                    level = 1,
                    listOrganizeDetailId = listOrganizeDetailId
                };



                string json = JsonConvert.SerializeObject(DataObject);


                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.Api_Create_Org);
                request.Headers.Add("Authorization", "Bearer " + Token);
                var content = new StringContent(json, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_OrganizeDetail.Root result = JsonConvert.DeserializeObject<API_OrganizeDetail.Root>(responseContent);
                    organizeCreated = result.data.data;
                    AddEmployeeToOrganize();
                    this.Visibility = Visibility.Collapsed;
                    ucSoDoToChuc sodo = new ucSoDoToChuc(Main);
                    Main.dopBody.Children.Clear();
                    object Content = sodo.Content;
                    sodo.Content = null;
                    Main.dopBody.Children.Add(Content as UIElement);
                }


            }
            catch { }
        }
        public async void AddEmployeeToOrganize()
        {
            try
            {
                var DataObject = new
                {
                    listUsers = ListAllUser.Where(x => x.isCheck).Select(x => (int)x.ep_id).ToList(),
                    organizeDetailId = organizeCreated.id,
                    listOrganizeDetailId = organizeCreated.listOrganizeDetailId
                };
                string json = JsonConvert.SerializeObject(DataObject);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.Api_addEmployeeToOrganize);
                request.Headers.Add("authorization", "Bearer " + Token);

                var content = new StringContent(json, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);


            }
            catch { }
        }
        private async void GetListOrganize()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.listAll_organize);
                request.Headers.Add("authorization", "Bearer " + Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    ListOrganizeEntities.Root result = JsonConvert.DeserializeObject<ListOrganizeEntities.Root>(responseContent);
                    ListOrganizeData = result.data.data;
                    foreach (var item in ListOrganizeData)
                    {
                        ListOrganize.Add(item.id.ToString(), item.organizeDetailName);
                    }
                    cboCacCapToChuc.ItemsSource = ListOrganizeData;
                }
            }
            catch
            {

            }
        }
        #endregion

        private void bod_AnhInput_MouseDown(object sender, MouseButtonEventArgs e)
        {

            //Upload images then convert to base64
            Border border = sender as Border;
            DockPanel dockpanel = border.Parent as DockPanel;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Tất cả các tệp|*.*"; // Lọc tất cả các tệp

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                (dockpanel.Children[1] as TextBlock).Text = openFileDialog.SafeFileName;
                (dockpanel.Children[2] as System.Windows.Shapes.Path).Visibility = Visibility.Visible;

                try
                {
                    // Read the image file into a byte array
                    byte[] imageBytes = File.ReadAllBytes(filePath);

                    // Convert the byte array to a Base64 string
                    string base64String = "data:img/png;base64," + Convert.ToBase64String(imageBytes);
                    (dockpanel.Children[3] as TextBlock).Text = base64String;
                    (dockpanel.Children[4] as TextBlock).Text = "1";

                }
                catch (Exception ex)
                {
                   // MessageBox.Show("Đã xảy ra lỗi khi đọc tệp: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }


        }

        private void DuyetTatCaNhanVien_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in ListAllUser)
            {
                item.isCheck = true;
            }

            lsvCheckNhanVien.Items.Refresh();
        }

        private void DuyetTatCaNhanVien_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var item in ListAllUser)
            {
                item.isCheck = false;
            }
            lsvCheckNhanVien.Items.Refresh();

        }

        private void DeleteImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Shapes.Path imageDeletePath = (System.Windows.Shapes.Path)sender;
            DockPanel dockpanel = imageDeletePath.Parent as DockPanel;
            (dockpanel.Children[1] as TextBlock).Text = "";
            (dockpanel.Children[3] as TextBlock).Text = "";
            imageDeletePath.Visibility = Visibility.Collapsed;
        }
        private bool ValidateForm()
        {
            if (textNhapTenCapToChuc.Text.Trim() == "")
            {
                tb_ValidateTenCapToChuc.Text = "Tên tổ chức không được để trống!" as string;
                tb_ValidateTenCapToChuc.Visibility = Visibility.Visible;
                return false;
            }



            return true;
        }

    }
}
