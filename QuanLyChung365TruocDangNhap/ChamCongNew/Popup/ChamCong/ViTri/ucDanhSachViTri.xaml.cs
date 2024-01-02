using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.ViTri;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popups.ChamCong;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.ViTri
{
    /// <summary>
    /// Interaction logic for ucDanhSachViTri.xaml
    /// </summary>
    public partial class ucDanhSachViTri : UserControl
    {
        MainWindow Main;
        List<API_Location.Location> ListLocation = new List<API_Location.Location>();
        public ucDanhSachViTri(MainWindow Main)
        {
            InitializeComponent();
            this.Main = Main;
            GetListLocation();
        }
        #region CallApi
        public async void GetListLocation(int pageNumber = 1)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/location/list");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_Location.Root result = JsonConvert.DeserializeObject<API_Location.Root>(responseContent);
                    ListLocation = result.data.list;
                    if (pagin.SelectedPage == 0) pagin.TotalRecords = (int)result.data.total;
                    cbxLocation.ItemsSource = ListLocation.Prepend(new API_Location.Location() { cor_id = 0, cor_location_name = "Tất cả vị trí" });
                    int STT = ((pageNumber - 1) * 10) + 1;
                    foreach (API_Location.Location item in ListLocation.Skip((pageNumber - 1) * 10).Take(10))
                    {
                        item.STT = STT++;
                    }
                    dgv.ItemsSource = ListLocation.Skip((pageNumber - 1) * 10).Take(10);

                }
            }
            catch
            {
            }


        }
        #endregion


        private void cbxLocation_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.Key == Key.Back)
                {
                    cbxLocation.SelectedIndex = -1;
                    string textSearch = cbxLocation.Text;
                    cbxLocation.Items.Refresh();
                    cbxLocation.ItemsSource = ListLocation.Where(t => t.cor_location_name.ToLower().Contains(textSearch.RemoveUnicode().ToLower())).Prepend(new API_Location.Location() { cor_id = 0, cor_location_name = "Tất cả vị trí" });
                }
            }
            catch { }
        }
        private void cbxLocation_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                cbxLocation.SelectedIndex = -1;
                string textSearch = cbxLocation.Text + e.Text;
                cbxLocation.IsDropDownOpen = true;
                if (textSearch == "")
                {
                    cbxLocation.Text = "";
                    cbxLocation.Items.Refresh();
                    cbxLocation.ItemsSource = ListLocation.Prepend(new API_Location.Location() { cor_id = 0, cor_location_name = "Tất cả vị trí" });
                    cbxLocation.SelectedIndex = -1;
                }
                else
                {
                    cbxLocation.ItemsSource = "";
                    cbxLocation.Items.Refresh();
                    cbxLocation.ItemsSource = ListLocation.Where(t => t.cor_location_name.ToLower().Contains(textSearch.RemoveUnicode().ToLower())).Prepend(new API_Location.Location() { cor_id = 0, cor_location_name = "Tất cả vị trí" }); ;
                }
            }
            catch { }

        }
        private void dgv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {

                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - e.Delta);
                }
                else
                {
                    Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
                }
            }
            catch { }
        }

        private T FindVisualChild<T>(DependencyObject visual) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(visual, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null)
                        return childItem;
                }
            }
            return null;
        }

        private void paginNV_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            GetListLocation(pagin.SelectedPage);

        }

        private void cbxLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                List<API_Location.Location> ListLocationSearch = new List<API_Location.Location>();
                ListLocationSearch.Add(cbxLocation.SelectedItem as API_Location.Location);
                dgv.ItemsSource = ListLocationSearch;
                pagin.TotalRecords = 1;
            }
            catch (Exception ex) { }
        }


        private void Search_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (cbxLocation.SelectedIndex != -1)
                {
                    if (int.Parse(cbxLocation.SelectedValue.ToString()) == 0)
                    {
                        pagin.SelectedPage = 0;
                        GetListLocation();
                    }
                    else
                    {
                        List<API_Location.Location> ListLocationSearch = new List<API_Location.Location>();
                        ListLocationSearch.Add(cbxLocation.SelectedItem as API_Location.Location);
                        dgv.ItemsSource = ListLocationSearch;
                        pagin.TotalRecords = 1;
                    }
                }
            }
            catch
            {

            }
        }

        private void Delete_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var selectedId = ((Path)sender).DataContext.ToString();
            if (selectedId != "")
            {
                Main.grShowPopup.Children.Add(new ucXoaViTri(int.Parse(selectedId), this, Main));
            }
        }

        private void NavigateToAddLocationPopup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucAddLocation(Main, this));
        }

        private void Edit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var selectedLocation = ((Path)sender).DataContext as API_Location.Location;
            if (selectedLocation != null)
            {
                Main.grShowPopup.Children.Add(new ucUpdateLocation(Main, this, selectedLocation));
            }
        }
    }
}
