using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Entities;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Popups.PopupSoDoToChuc;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Popups.PopupSoDoViTri;
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
using WindowsInput;


namespace QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.CoCau_ViTri_ToChuc
{
    /// <summary>
    /// Interaction logic for ucSoDoViTRi.xaml
    /// </summary>
    public partial class ucSoDoViTRi : UserControl
    {

        MainWindow Main;
        BrushConverter br = new BrushConverter();
        List<Child_SoDoViTri> listChildky = new List<Child_SoDoViTri>();
        List<ListPositionEntities.PositionData> ListAllPosition = new List<ListPositionEntities.PositionData>();
        bool isHasChild = false;
        public bool isDragging = false;
        public Point lastMousePosition;
        public Child_SoDoViTri ItemToSwap = null;
        InputSimulator inputSimulator = new InputSimulator();
        public ucSoDoViTRi(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            LoadSoDoChucVu();
            LoadListChucVu();
        }
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                //isDragging = true;
                lastMousePosition = e.GetPosition(canvas);
                ((UIElement)sender).CaptureMouse();
                Border border = sender as Border;
                var selectedItem = border.DataContext as Child_SoDoViTri;
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
            var selectedItem = border.DataContext as Child_SoDoViTri;
            ItemToSwap = selectedItem;
            inputSimulator.Mouse.LeftButtonUp();


        }
        #region Hover Event
        private void bod_ThemMoiViTri_MouseEnter(object sender, MouseEventArgs e)
        {
            bod_ThemMoiViTri.BorderThickness = new Thickness(1);
            bod_ThemMoiViTri.Background = (Brush)br.ConvertFrom("#339DFA");
        }

        private void bod_ThemMoiViTri_MouseLeave(object sender, MouseEventArgs e)
        {
            bod_ThemMoiViTri.BorderThickness = new Thickness(0);
            bod_ThemMoiViTri.Background = (Brush)br.ConvertFrom("#4C5BD4");
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
        #endregion

        #region Click Event
        private void bod_ThemMoiViTri_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isHasChild == false)
            {
                Main.grShowPopup.Children.Add(new ucThemMoiViTri1(Main));
            }
            else Main.grShowPopup.Children.Add(new ucThemMoiViTri(Main, ListAllPosition));
        }


        #endregion

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

        #region CallAPI
        public async void LoadSoDoChucVu()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Comons.Api_ThietLapCongTy.Api_SoDoViTri);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resconten = await response.Content.ReadAsStringAsync();

                Root_SoDoViTri result = JsonConvert.DeserializeObject<Root_SoDoViTri>(resconten);


                if (result != null)
                {
                    listChildky.Add(result.data.data);
                    lsvSoDoToChuc.ItemsSource = null;
                    lsvSoDoToChuc.ItemsSource = listChildky;
                    lsvSoDoToChuc.Items.Refresh();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ API: " + ex.Message);
            }
        }

        private async void LoadListChucVu()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Comons.Api_ThietLapCongTy.Api_position_listAll);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);

                var resconten = await response.Content.ReadAsStringAsync();

                ListPositionEntities.Root result = JsonConvert.DeserializeObject<ListPositionEntities.Root>(resconten);
                ListAllPosition = result.data.data;
                if (ListAllPosition.Count > 0) isHasChild = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ API: " + ex.Message);
            }
        }
        #endregion

        private void StackPanel_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }



        private void grid_Loaded(object sender, RoutedEventArgs e)
        {

            StackPanel stackPanel = (StackPanel)sender;
            stackPanel.Children.Add(new ucNestedListViewViTri(Main, this) as UIElement);
        }

        private void grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
