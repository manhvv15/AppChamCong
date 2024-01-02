using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Popups.PopupSoDoToChuc;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Comons;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Entities;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Popups.PopupSoDoViTri;
using QuanLyChung365TruocDangNhap.ChamCongNew;
using WindowsInput;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.CoCau_ViTri_ToChuc
{
    /// <summary>
    /// Interaction logic for ucSoDoToChuc.xaml
    /// </summary>
    /// 
    public partial class ucSoDoToChuc : UserControl
    {
        #region Fake
        public class SoDo
        {

            public string name { get; set; }
            public string comName { get; set; }
            public int ep_num { get; set; }
            public string manager { get; set; }

        }
        List<SoDo> soDoList = new List<SoDo>();
        #endregion
        BrushConverter br = new BrushConverter();
        MainWindow Main;
        bool isEmptyOrganize = false;
        public bool isDragging = false;
        public Point lastMousePosition;
        public API_Tree_SoDoToChuc.Child ItemToSwap = null;
        InputSimulator inputSimulator = new InputSimulator();
        public ucSoDoToChuc(MainWindow main)
        {
            InitializeComponent();
            Main = main;

            LoadSoDoToChuc();
            #region Fake
            for (int i = 0; i < 1; i++)
            {
                soDoList.Add(new SoDo() { comName = $" Công ty {i}", ep_num = i, manager = $"Ky {i}", name = $"KyTo {i}" });
            }
            //lsvSoDoToChuc.ItemsSource = soDoList;
            #endregion

        }
        #region ThayDoiViTriCuaSoDoToChuc
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                //isDragging = true;
                lastMousePosition = e.GetPosition(canvas);
                ((UIElement)sender).CaptureMouse();
                Border border = sender as Border;
                var selectedItem = border.DataContext as API_Tree_SoDoToChuc.Child;
                ItemToSwap = selectedItem;
            }
            catch (Exception ex) { }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(canvas);
                double deltaX = currentPosition.X - lastMousePosition.X;
                double deltaY = currentPosition.Y - lastMousePosition.Y;

                // Update the position of the control
                Canvas.SetLeft((UIElement)BorderMoveable, Canvas.GetLeft((UIElement)sender) + deltaX);
                Canvas.SetTop((UIElement)BorderMoveable, Canvas.GetTop((UIElement)sender) + deltaY);

                lastMousePosition = currentPosition;
            }
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;

            canvas.Visibility = Visibility.Collapsed;
            ((UIElement)sender).ReleaseMouseCapture();
            Border border = sender as Border;
            var selectedItem = border.DataContext as API_Tree_SoDoToChuc.Child;
            ItemToSwap = selectedItem;
            inputSimulator.Mouse.LeftButtonUp();


        }
        #endregion
        #region Call Api
        List<API_Tree_SoDoToChuc.Child> listChildky = new List<API_Tree_SoDoToChuc.Child>();
        public async void LoadSoDoToChuc()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Comons.Api_ThietLapCongTy.Api_SoDoToChuc);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resconten = await response.Content.ReadAsStringAsync();

                API_Tree_SoDoToChuc.Root result = JsonConvert.DeserializeObject<API_Tree_SoDoToChuc.Root>(resconten);


                if (result != null)
                {
                    listChildky.Add(result.data.data);
                    lsvSoDoToChuc.ItemsSource = listChildky;
                    if (listChildky[0].children.Count == 0) isEmptyOrganize = true;
                    lsvSoDoToChuc.Items.Refresh();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ API: " + ex.Message);
            }
        }

        private List<Child_SoDoToChuc> LoadChildren(JToken parent)
        {
            List<Child_SoDoToChuc> children = new List<Child_SoDoToChuc>();
            if (parent["children"] != null)
            {
                foreach (var item in parent["children"])
                {
                    Child_SoDoToChuc org = item.ToObject<Child_SoDoToChuc>();
                    org.children = LoadChildren(item);
                    children.Add(org);
                }
            }
            return children;
        }

        private async void UpdateOrgName(string json = "")
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.Api_update_org);
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent(json, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {

                }

            }
            catch
            {

            }
        }
        #endregion

        #region Hover Event
        private void bod_ThemMoiToChuc_MouseEnter(object sender, MouseEventArgs e)
        {
            bod_ThemMoiToChuc.BorderThickness = new Thickness(1);
            bod_ThemMoiToChuc.Background = (Brush)br.ConvertFrom("#339DFA");

        }

        private void bod_ThemMoiToChuc_MouseLeave(object sender, MouseEventArgs e)
        {
            bod_ThemMoiToChuc.BorderThickness = new Thickness(0);
            bod_ThemMoiToChuc.Background = (Brush)br.ConvertFrom("#4C5BD4");
        }
        private void btn_SuaToChuc_MouseEnter(object sender, MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border btn_SuaToChuc = FindChild<Border>(row, "btn_SuaToChuc");
                Path icon_SuaToChuc = FindChild<Path>(row, "icon_SuaToChuc");
                if (btn_SuaToChuc != null && icon_SuaToChuc != null)
                {

                    btn_SuaToChuc.Background = (Brush)br.ConvertFrom("#6666");
                    icon_SuaToChuc.Fill = (Brush)br.ConvertFrom("#4C5BD4");
                }
            }
        }

        private void btn_SuaToChuc_MouseLeave(object sender, MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border btn_SuaToChuc = FindChild<Border>(row, "btn_SuaToChuc");
                Path icon_SuaToChuc = FindChild<Path>(row, "icon_SuaToChuc");
                if (btn_SuaToChuc != null && icon_SuaToChuc != null)
                {

                    btn_SuaToChuc.Background = (Brush)br.ConvertFrom("#FFFFFF");
                    icon_SuaToChuc.Fill = (Brush)br.ConvertFrom("#474747");
                }
            }


        }

        private void btn_XoaToChuc_MouseEnter(object sender, MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border btn_XoaToChuc = FindChild<Border>(row, "btn_XoaToChuc");
                Path icon_XoaToChuc = FindChild<Path>(row, "icon_XoaToChuc");
                if (btn_XoaToChuc != null && icon_XoaToChuc != null)
                {

                    btn_XoaToChuc.Background = (Brush)br.ConvertFrom("#6666");
                    icon_XoaToChuc.Stroke = (Brush)br.ConvertFrom("#FF3333");
                }
            }

        }

        private void btn_XoaToChuc_MouseLeave(object sender, MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border btn_XoaToChuc = FindChild<Border>(row, "btn_XoaToChuc");
                Path icon_XoaToChuc = FindChild<Path>(row, "icon_XoaToChuc");
                if (btn_XoaToChuc != null && icon_XoaToChuc != null)
                {

                    btn_XoaToChuc.Background = (Brush)br.ConvertFrom("#FFFFFF");
                    icon_XoaToChuc.Stroke = (Brush)br.ConvertFrom("#474747");
                }
            }

        }
        private void btn_LuuTenToChuc_MouseEnter(object sender, MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border btn_LuuTenToChuc = FindChild<Border>(row, "btn_LuuTenToChuc");
                Path icon_LuuTenToChuc = FindChild<Path>(row, "icon_LuuTenToChuc");
                if (btn_LuuTenToChuc != null && icon_LuuTenToChuc != null)
                {

                    btn_LuuTenToChuc.Background = (Brush)br.ConvertFrom("#6666");
                    icon_LuuTenToChuc.Stroke = (Brush)br.ConvertFrom("#4C5BD4");
                }
            }

        }

        private void btn_LuuTenToChuc_MouseLeave(object sender, MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border btn_LuuTenToChuc = FindChild<Border>(row, "btn_LuuTenToChuc");
                Path icon_LuuTenToChuc = FindChild<Path>(row, "icon_LuuTenToChuc");
                if (btn_LuuTenToChuc != null && icon_LuuTenToChuc != null)
                {

                    btn_LuuTenToChuc.Background = (Brush)br.ConvertFrom("#FFFFFF");
                    icon_LuuTenToChuc.Stroke = (Brush)br.ConvertFrom("#474747");
                }
            }

        }

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

        #region Click Event
        private void bod_ThemMoiToChuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemToChuc(Main, isEmptyOrganize));
        }

        private void icListChill_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    scroll.ScrollToVerticalOffset(scroll.VerticalOffset);
                    scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - e.Delta);
                }
                else
                {
                    Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
                }
            }
            catch { }
        }

        private void btn_XemChiTiet_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            var selectedItem = border.DataContext as API_Tree_SoDoToChuc.Child;
            if (selectedItem != null)
            {
                int org_id = (int)selectedItem.id;
                Main.grShowPopup.Children.Add(new ucXemChiTiet(Main.IdAcount.ToString(), org_id, Properties.Settings.Default.Token));
            }
        }

        private void btn_SuaToChuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);
            if (row != null)
            {
                WrapPanel wap_InputTenToChuc = FindChild<WrapPanel>(row, "wap_InputTenToChuc");
                TextBlock tb_TenToChuc = FindChild<TextBlock>(row, "tb_TenToChuc");
                Border btn_SuaToChuc = FindChild<Border>(row, "btn_SuaToChuc");

                if (wap_InputTenToChuc.Visibility == Visibility.Collapsed)
                {
                    wap_InputTenToChuc.Visibility = Visibility.Visible;
                    tb_TenToChuc.Visibility = Visibility.Collapsed;
                    btn_SuaToChuc.Visibility = Visibility.Collapsed;

                }
            }

        }

        private void btn_LuuTenToChuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string tb_InputTenToChuc_Text = "";
            //xử lý giao diện
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);
            if (row != null)
            {
                WrapPanel wap_InputTenToChuc = FindChild<WrapPanel>(row, "wap_InputTenToChuc");
                TextBlock tb_TenToChuc = FindChild<TextBlock>(row, "tb_TenToChuc");
                TextBox tb_InputTenToChuc = FindChild<TextBox>(row, "tb_InputTenToChuc");
                Border btn_SuaToChuc = FindChild<Border>(row, "btn_SuaToChuc");
                if (wap_InputTenToChuc.Visibility == Visibility.Visible)
                {
                    wap_InputTenToChuc.Visibility = Visibility.Collapsed;
                    tb_TenToChuc.Visibility = Visibility.Visible;
                    btn_SuaToChuc.Visibility = Visibility.Visible;
                    tb_TenToChuc.Text = tb_InputTenToChuc.Text;
                    tb_InputTenToChuc_Text = tb_InputTenToChuc.Text;
                }
            }
            // xử lý api
            Border border = sender as Border;
            int org_id = (int)border.DataContext;
            if (org_id != null)
            {
                string json = "{\"id\":" + org_id + ",\"organizeDetailName\":\"" + tb_InputTenToChuc_Text + "\"}";
                UpdateOrgName(json);
            }


        }
        #endregion
        #region ShowPopup


        private void borEmpTimeKeep_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = ((Border)sender).DataContext as API_Tree_SoDoToChuc.Child;
            if (selectedItem != null)
            {
                List<API_Tree_SoDoToChuc.ListOrganizeDetailId> lisstOrganizeDetailId = selectedItem.listOrganizeDetailId;

                Main.grShowPopup.Children.Add(new ucDanhSachNhanVienDiLam(Properties.Settings.Default.Token, lisstOrganizeDetailId));

            }
        }

        private void borEmpNoTimeKeep_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = ((Border)sender).DataContext as API_Tree_SoDoToChuc.Child;
            if (selectedItem != null)
            {
                List<API_Tree_SoDoToChuc.ListOrganizeDetailId> listOrganizeDetailId = selectedItem.listOrganizeDetailId;

                Main.grShowPopup.Children.Add(new ucDanhSachNhanVienKhongDiLam(Properties.Settings.Default.Token, listOrganizeDetailId));

            }

        }

        #endregion

        private void expanseTree_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = (Border)sender;
            Path path = border.Child as Path;
            DockPanel dockPanel = (DockPanel)border.Parent;
            Grid grid = (Grid)dockPanel.Parent;
            Border border1 = (Border)grid.Parent;
            StackPanel stackPanel = (StackPanel)border1.Parent;
            if (stackPanel.Children[3].Visibility == Visibility.Collapsed)
            {

                path.LayoutTransform = new RotateTransform(0);
                stackPanel.Children[3].Visibility = Visibility.Visible;
                stackPanel.Children[4].Visibility = Visibility.Visible;
            }
            else
            {
                path.LayoutTransform = new RotateTransform(-90);
                stackPanel.Children[3].Visibility = Visibility.Collapsed;
                stackPanel.Children[4].Visibility = Visibility.Collapsed;
            }
        }

        private void scroll_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset);
            //e.Handled = true;
        }
        private void grid_Loaded(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            stackPanel.Children.Add(new ucNestedListViewSoDoToChuc(Main, this) as UIElement);
        }

        private void grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {


        }

        private void lsvSoDoToChuc_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //e.Handled = true;

        }

        private void canvas_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            isDragging = true;
        }

        private void canvas_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //if(canvas.Visibility == Visibility.Collapsed) {
            //    Thread.Sleep(500);
            //    ucSoDoToChuc uc = new ucSoDoToChuc(this.Main);
            //    Main.dopBody.Children.Clear();
            //    object Content = uc.Content;
            //    uc.Content = null;
            //    Main.dopBody.Children.Add(Content as UIElement);
            //}
        }
    }
}
