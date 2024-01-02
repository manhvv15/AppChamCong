using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.CoCau_ViTri_ToChuc;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Entities;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Popups.PopupSoDoViTri
{
    /// <summary>
    /// Interaction logic for ucNestedListViewViTri.xaml
    /// </summary>
    public partial class ucNestedListViewViTri : UserControl
    {
        MainWindow Main;
        ucSoDoViTRi ucSoDoViTRi;
        BrushConverter br = new BrushConverter();

        public ucNestedListViewViTri(MainWindow main, ucSoDoViTRi ucSoDoViTRi)
        {
            InitializeComponent();
            Main = main;
            this.ucSoDoViTRi = ucSoDoViTRi;
        }


        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (ucSoDoViTRi.canvas.Visibility == Visibility.Collapsed)
                {

                    if (Mouse.LeftButton == MouseButtonState.Pressed)
                    {

                        ucSoDoViTRi.canvas.Visibility = Visibility.Visible;
                        Border border = (sender as Border);
                        ucSoDoViTRi.canvas.DataContext = border.DataContext;
                        ucSoDoViTRi.isDragging = true;
                        ucSoDoViTRi.lastMousePosition = e.GetPosition(ucSoDoViTRi.canvas);
                        double deltaX = ucSoDoViTRi.lastMousePosition.X;
                        double deltaY = ucSoDoViTRi.lastMousePosition.Y;

                        // Update the position of the control

                        Canvas.SetLeft(ucSoDoViTRi.BorderMoveable, deltaX - 120);
                        Canvas.SetTop(ucSoDoViTRi.BorderMoveable, deltaY - 100);
                        ucSoDoViTRi.BorderMoveable.CaptureMouse();
                    }
                }

            }
            catch { }
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucSoDoViTRi.canvas.Visibility = Visibility.Collapsed;
            if (ucSoDoViTRi.ItemToSwap != null)
            {
                Border border = sender as Border;
                var selectedItem = border.DataContext as Child_SoDoViTri;
                Swap(ucSoDoViTRi.ItemToSwap.id, selectedItem.id);

            }
        }
        private async void Swap(int? startId, int? endId)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/positions/swap");
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent("{\"start_id\":" + startId + ",\"end_id\":" + endId + "}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    ucSoDoViTRi uc = new ucSoDoViTRi(this.Main);
                    Main.dopBody.Children.Clear();
                    object Content = uc.Content;
                    uc.Content = null;
                    Main.dopBody.Children.Add(Content as UIElement);
                    CustomMessageBox.Show("Thay đổi vị trí thành công");
                }
                else
                {
                    ucSoDoViTRi uc = new ucSoDoViTRi(this.Main);
                    Main.dopBody.Children.Clear();
                    object Content = uc.Content;
                    uc.Content = null;
                    CustomMessageBox.Show("Thay đổi vị trí thất bại");
                }

            }
            catch
            {
                ucSoDoViTRi uc = new ucSoDoViTRi(this.Main);
                Main.dopBody.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                CustomMessageBox.Show("Có lỗi xảy ra khi thay đổi vị trí");
            }
        }
        private void wp_ThemCap_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                // Lấy hàng (row) được nhấn chuột
                ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (row != null)
                {
                    // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                    Border btn_ThemCap = FindChild<Border>(row, "btn_ThemCap");
                    TextBlock path_ThemCap = FindChild<TextBlock>(row, "path_ThemCap");
                    TextBlock tb_ThemCap = FindChild<TextBlock>(row, "tb_ThemCap");

                    btn_ThemCap.Background = (Brush)br.ConvertFrom("#4C5BD4");
                    path_ThemCap.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
                    tb_ThemCap.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
                }
            }
            catch
            {

            }

        }

        private void wp_ThemCap_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                // Lấy hàng (row) được nhấn chuột
                ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (row != null)
                {
                    // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                    Border btn_ThemCap = FindChild<Border>(row, "btn_ThemCap");
                    TextBlock path_ThemCap = FindChild<TextBlock>(row, "path_ThemCap");
                    TextBlock tb_ThemCap = FindChild<TextBlock>(row, "tb_ThemCap");


                    btn_ThemCap.Background = (Brush)br.ConvertFrom("#666666");
                    path_ThemCap.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
                    tb_ThemCap.Foreground = (Brush)br.ConvertFrom("#474747");
                }
            }
            catch
            {

            }

        }

        private void wp_ThayThe_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                // Lấy hàng (row) được nhấn chuột
                ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (row != null)
                {
                    // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                    Border btn_ThayThe = FindChild<Border>(row, "btn_ThayThe");
                    Path path_ThayThe = FindChild<Path>(row, "path_ThayThe");
                    TextBlock tb_ThayThe = FindChild<TextBlock>(row, "tb_ThayThe");

                    btn_ThayThe.Background = (Brush)br.ConvertFrom("#4C5BD4");
                    path_ThayThe.Fill = (Brush)br.ConvertFrom("#FFFFFF");
                    tb_ThayThe.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
                }
            }
            catch
            {

            }

        }

        private void wp_ThayThe_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                // Lấy hàng (row) được nhấn chuột
                ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (row != null)
                {
                    // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                    Border btn_ThayThe = FindChild<Border>(row, "btn_ThayThe");
                    Path path_ThayThe = FindChild<Path>(row, "path_ThayThe");
                    TextBlock tb_ThayThe = FindChild<TextBlock>(row, "tb_ThayThe");

                    btn_ThayThe.Background = (Brush)br.ConvertFrom("#666666");
                    path_ThayThe.Fill = (Brush)br.ConvertFrom("#FFFFFF");
                    tb_ThayThe.Foreground = (Brush)br.ConvertFrom("#474747");
                }
            }
            catch
            {

            }

        }

        private void wp_Xoa_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                // Lấy hàng (row) được nhấn chuột
                ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (row != null)
                {
                    // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                    Border btn_Xoa = FindChild<Border>(row, "btn_Xoa");
                    Path path_Xoa = FindChild<Path>(row, "path_Xoa");
                    TextBlock tb_Xoa = FindChild<TextBlock>(row, "tb_Xoa");

                    btn_Xoa.Background = (Brush)br.ConvertFrom("#FFFFFF");
                    path_Xoa.Fill = (Brush)br.ConvertFrom("#F46A6A");
                    tb_Xoa.Foreground = (Brush)br.ConvertFrom("#F46A6A");
                }
            }
            catch
            {

            }

        }

        private void wp_Xoa_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                // Lấy hàng (row) được nhấn chuột
                ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (row != null)
                {
                    // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                    Border btn_Xoa = FindChild<Border>(row, "btn_Xoa");
                    Path path_Xoa = FindChild<Path>(row, "path_Xoa");
                    TextBlock tb_Xoa = FindChild<TextBlock>(row, "tb_Xoa");

                    btn_Xoa.Background = (Brush)br.ConvertFrom("#666666");
                    path_Xoa.Fill = (Brush)br.ConvertFrom("#FFFFFF");
                    tb_Xoa.Foreground = (Brush)br.ConvertFrom("#474747");
                }
            }
            catch
            {

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

        private void wp_Xoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DockPanel dockPanel = (DockPanel)sender;
            Child_SoDoViTri child = dockPanel.DataContext as Child_SoDoViTri;
            Main.grShowPopup.Children.Add(new ucXoaViTri(Main, ucSoDoViTRi, (int)child.id));
        }

        private void wp_ThemCap_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DockPanel dockPanel = (DockPanel)sender;
            Child_SoDoViTri child = dockPanel.DataContext as Child_SoDoViTri;
            Main.grShowPopup.Children.Add(new ucThemCap(Main, (int)child.level));
        }

        private void wp_ThayThe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DockPanel dockPanel = (DockPanel)sender;
            Child_SoDoViTri child = dockPanel.DataContext as Child_SoDoViTri;
            Main.grShowPopup.Children.Add(new ucThayThe(Main, (int)child.id, child.name));
        }

        private void grid_Loaded(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            stackPanel.Children.Add(new ucNestedListViewViTri(Main, ucSoDoViTRi) as UIElement);
        }
    }
}
