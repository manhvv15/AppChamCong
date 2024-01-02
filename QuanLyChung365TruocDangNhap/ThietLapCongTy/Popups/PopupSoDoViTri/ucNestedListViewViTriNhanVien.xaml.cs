using QuanLyChung365TruocDangNhap.ThietLapCongTy.CoCau_ViTri_ToChuc;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.Entities;
using System;
using System.Collections.Generic;
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

namespace QuanLyChung365TruocDangNhap.ThietLapCongTy.Popups.PopupSoDoViTri
{
    /// <summary>
    /// Interaction logic for ucNestedListViewViTri.xaml
    /// </summary>
    public partial class ucNestedListViewViTriNhanVien : UserControl
    {
        frmMain Main;
        ucSoDoViTRi ucSoDoViTRi;
        BrushConverter br = new BrushConverter();
        public ucNestedListViewViTriNhanVien(frmMain main, ucSoDoViTRi ucSoDoViTRi)
        {
            InitializeComponent();
            Main = main;
            this.ucSoDoViTRi = ucSoDoViTRi;
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
            Main.pnlShowPopUp.Children.Add(new ucXoaViTri(Main, ucSoDoViTRi, (int)child.id));
        }

        private void wp_ThemCap_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DockPanel dockPanel = (DockPanel)sender;
            Child_SoDoViTri child = dockPanel.DataContext as Child_SoDoViTri;
            Main.pnlShowPopUp.Children.Add(new ucThemCap(Main, (int)child.level));
        }

        private void wp_ThayThe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DockPanel dockPanel = (DockPanel)sender;
            Child_SoDoViTri child = dockPanel.DataContext as Child_SoDoViTri;
            Main.pnlShowPopUp.Children.Add(new ucThayThe(Main, (int)child.id, child.name));
        }

        private void grid_Loaded(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            stackPanel.Children.Add(new ucNestedListViewViTriNhanVien(Main, ucSoDoViTRi) as UIElement);
        }
    }
}
