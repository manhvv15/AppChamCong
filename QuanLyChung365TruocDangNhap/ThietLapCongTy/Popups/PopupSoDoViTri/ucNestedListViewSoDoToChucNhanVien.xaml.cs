using QuanLyChung365TruocDangNhap.ThietLapCongTy.Comons;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.Entities;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.Popups.PopupSoDoToChuc;
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

namespace QuanLyChung365TruocDangNhap.ThietLapCongTy.Popups.PopupSoDoViTri
{
    /// <summary>
    /// Interaction logic for ucNestedListViewSoDoToChuc.xaml
    /// </summary>
    public partial class ucNestedListViewSoDoToChucNhanVien : UserControl
    {

        frmMain Main;

        BrushConverter br = new BrushConverter();
        public ucNestedListViewSoDoToChucNhanVien(frmMain main)
        {
            InitializeComponent();
            this.Main = main;
        }
        public ucNestedListViewSoDoToChucNhanVien()
        {
            InitializeComponent();
        }

        #region Hover Event

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
           //Main.pnlShowPopUp.Children.Add(new ucThemToChuc(Main));
        }



        private void btn_XemChiTiet_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            var selectedItem = border.DataContext as API_Tree_SoDoToChuc.Child;
            if (selectedItem != null)
            {
                int org_id = (int)selectedItem.id;
                Main.pnlShowPopUp.Children.Add(new ucXemChiTiet(Main.IdAcount.ToString(), org_id, Properties.Settings.Default.Token));
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
        private void btn_XoaToChuc_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            var selectedItem = border.DataContext as API_Tree_SoDoToChuc.Child;
            if (selectedItem != null)
            {
                string name = selectedItem.name;
                int org_id = (int)selectedItem.id;
                Main.pnlShowPopUp.Children.Add(new ucXoaToChuc(Main, name, org_id));
            }
        }

        private void borEmpTimeKeep_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = ((Border)sender).DataContext as API_Tree_SoDoToChuc.Child;
            if (selectedItem != null)
            {
                List<API_Tree_SoDoToChuc.ListOrganizeDetailId> lisstOrganizeDetailId = selectedItem.listOrganizeDetailId;

                Main.pnlShowPopUp.Children.Add(new ucDanhSachNhanVienDiLam(Properties.Settings.Default.Token, lisstOrganizeDetailId));

            }
        }

        private void borEmpNoTimeKeep_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = ((Border)sender).DataContext as API_Tree_SoDoToChuc.Child;
            if (selectedItem != null)
            {
                List<API_Tree_SoDoToChuc.ListOrganizeDetailId> listOrganizeDetailId = selectedItem.listOrganizeDetailId;

                Main.pnlShowPopUp.Children.Add(new ucDanhSachNhanVienKhongDiLam(Properties.Settings.Default.Token, listOrganizeDetailId));

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
        #region CallAPI
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

        private void grid_Loaded(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            stackPanel.Children.Add(new ucNestedListViewSoDoToChucNhanVien(Main) as UIElement);
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void icon_SuaToChuc_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btn_SuaToChuc_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void btn_XoaToChuc_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

        }

        private void btn_LuuTenToChuc_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void borEmpTimeKeep_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

        }

        private void borEmpNoTimeKeep_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

        }

        private void btn_XemChiTiet_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

        }

        private void expanseTree_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void tb_InputTenToChuc_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // e.Handled = true;

        }
    }
}
