using ChamCong365.APIs;
using ChamCong365.funcQuanLyCongTy.DepartmentManagerTabList;
using ChamCong365.OOP.funcQuanLyCongTy;
using ChamCong365.Popup.funcCompanyManager;
using ChamCong365.Popup.funcCompanyManager.DepartmentManagerPopups;
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

namespace ChamCong365
{
    /// <summary>
    /// Interaction logic for ucListGroup.xaml
    /// </summary>

    public partial class ucListGroup : UserControl
    {
        public MainWindow Main;
        BrushConverter brus = new BrushConverter();
        public List<Group> GroupList = new List<Group>();
        public List<Company> ChildCompanyList = new List<Company>();
        public List<Company> dataCompanySelectBox = new List<Company>();
        public List<Department> DepartmentList = new List<Department>();
        public List<Team> TeamList = new List<Team>();
        private int com_id = -1;
        private int dep_id = -1;
        private int team_id = -1;
        private int gr_id = -1;

        public List<Group> listGroup = new List<Group>();
        List<Group> listGroupPT = new List<Group>();
        private int TotalListItem = 0;
        private int TongSoTrang = 0;
        private int SoDu = 0;
        public ucListGroup(MainWindow main)
        {
            InitializeComponent();
            this.Main = main;
            this.com_id = Main.IdAcount;
            LoadListGroup();
            LoadListTeam();
            LoadListDepartment();
            //LoadListChildCompany();
            LoadCompanySelectBox();
        }

        public async void LoadListGroup()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.group_listAll_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                if (com_id != -1) content.Add(new StringContent(com_id.ToString()), "com_id");
                if (dep_id != -1) content.Add(new StringContent(dep_id.ToString()), "dep_id");
                if (team_id != -1) content.Add(new StringContent(team_id.ToString()), "team_id");
                if (gr_id != -1) content.Add(new StringContent(gr_id.ToString()), "gr_id");

                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    GroupRoot result = JsonConvert.DeserializeObject<GroupRoot>(responseContent);
                    if (result.data.data != null)
                    {
                        int STT = 1;
                        GroupList = (from item in result.data.data
                                     select new Group
                                     {
                                         STT = STT++,
                                         gr_id = item.gr_id,
                                         gr_name = (item.gr_name == "") ? "Chưa cập nhật" : item.gr_name,
                                         team_id = item.team_id,
                                         team_name = (item.team_name == "") ? "Chưa cập nhật" : item.team_name,
                                         dep_id = item.dep_id,
                                         dep_name = (item.dep_name == "") ? "Chưa cập nhật" : item.dep_name,
                                         total_emp = item.total_emp,
                                         manager = (item.manager == "") ? "Chưa cập nhật" : item.manager,
                                         deputy = (item.deputy == "") ? "Chưa cập nhật" : item.deputy
                                     }).ToList();
                    }

                }

                if (gr_id == -1) lsvGroup.ItemsSource = GroupList.Prepend(new Group() { gr_id = -1, gr_name = "Chọn nhóm" });
                LoadTableData(GroupList);
            }
            catch
            {

            }
        }
        public async void LoadListTeam()
        {
            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.team_list_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                if (com_id != -1) content.Add(new StringContent(com_id.ToString()), "com_id");
                if (dep_id != -1) content.Add(new StringContent(dep_id.ToString()), "dep_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    TeamRoot result = JsonConvert.DeserializeObject<TeamRoot>(responseContent);
                    TeamList = result.data.data;
                    lsvTo.ItemsSource = TeamList.Prepend(new Team() { team_id = -1, team_name = "Chọn tổ" });
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi sảy ra khi load danh sách tổ " + ex.Message);
            }


        }
        public void LoadTableData(List<Group> list)
        {


            listGroupPT = new List<Group>();
            // ẩn phân trang khi không có dũ liệu

            // reset hiển thị phần phân trang
            textPage1.Text = "1";
            textPage2.Text = "2";
            textPage3.Text = "3";
            borLui1.Visibility = Visibility.Collapsed;
            borPageDau.Visibility = Visibility.Collapsed;
            borLen1.Visibility = Visibility.Collapsed;
            borPageCuoi.Visibility = Visibility.Collapsed;
            borPage1.Visibility = Visibility.Visible;
            borPage2.Visibility = Visibility.Collapsed;
            borPage3.Visibility = Visibility.Collapsed;
            borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");


            if (list.Count > 0)
            {
                TotalListItem = list.Count;
                TongSoTrang = (int)Math.Ceiling(TotalListItem / 10.0);
                SoDu = 10 - (TotalListItem % 10);
                var numberOfItem = (TongSoTrang == 1) ? TotalListItem : 10;
                for (int i = 0; i < numberOfItem; i++)
                {
                    listGroupPT.Add(list[i]);
                }
                //listDepartment = list;


                if (TongSoTrang <= 3)
                {
                    borPage3.Visibility = Visibility.Visible;

                    borLen1.Visibility = Visibility.Collapsed;
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    if (TongSoTrang == 2)
                    {
                        borPage3.Visibility = Visibility.Collapsed;
                        borPage2.Visibility = Visibility.Visible;

                    }
                    if (TongSoTrang == 1)
                    {
                        borPage3.Visibility = Visibility.Collapsed;
                        borPage2.Visibility = Visibility.Collapsed;

                    }
                }
                else
                {
                    borLui1.Visibility = Visibility.Collapsed;
                    borPageDau.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Visible;
                    borPageCuoi.Visibility = Visibility.Visible;
                }
            }
            else
            {
                listGroupPT = new List<Group>();
                borPage1.Visibility = Visibility.Collapsed;
            }

            dgGroup.ItemsSource = null;
            dgGroup.ItemsSource = listGroupPT;

        }
        public void LoadCompanySelectBox()
        {
            Company ParentCompany = new Company() { com_id = Main.IdAcount, com_name = Main.txbNameAccount.Text };
            //GetListChildCompany();
            List<Company> listDataDropBox = new List<Company>();
            listDataDropBox.Add(ParentCompany);
            lsvCompany.ItemsSource = listDataDropBox;
        }
        private async void LoadListChildCompany()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.list_ChildCompany_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                CompanyRoot result = JsonConvert.DeserializeObject<CompanyRoot>(responseContent);
                ChildCompanyList = result.data.items;
                lsvCompany.ItemsSource = ChildCompanyList;


            }
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi  load danh sách công ty!" + e.Message);
            }
        }
        public async void LoadListDepartment()
        {
            try
            {

                var httpClient = new HttpClient();
                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Post;
                string api = API.list_department_api;

                request.RequestUri = new Uri(api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                if (com_id != -1) content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                DepartmentRoot result = JsonConvert.DeserializeObject<DepartmentRoot>(responseContent);

                //load dropdown box
                DepartmentList = result.data.items;
                lsvDepartment.ItemsSource = DepartmentList.Prepend(new Department() { dep_id = -1, dep_name = "Chọn phòng ban" });

            }
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi khi load danh sách phòng ban" + e.Message);
            }
        }

        //dropdown box select company
        //dropdown box select company
        private void bodSelectCompany_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListCompanyCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListCompanyCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListCompanyCollapsed.Visibility -= Visibility.Collapsed;

            }
        }


        private void lsvCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Company)lsvCompany.SelectedItem;
            if (selectedItem != null)
            {
                com_id = selectedItem.com_id;
                txbSelectCompany.Text = selectedItem.com_name;
                txbSelectCompany.Foreground = (Brush)brus.ConvertFromString("#474747");
                bodListCompanyCollapsed.Visibility = Visibility.Collapsed;
            }
        }

        private void bodSelectDepartment_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListDepartmentCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListDepartmentCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListDepartmentCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Department)lsvDepartment.SelectedItem;
            if (selectedItem != null)
            {
                dep_id = selectedItem.dep_id;
                txbSelectDepartment.Text = selectedItem.dep_name;
                txbSelectDepartment.Foreground = (Brush)brus.ConvertFromString("#474747");
                bodListDepartmentCollapsed.Visibility = Visibility.Collapsed;
            }

        }

        //dropdown box select to
        private void bodSelectTo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListToCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListToCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListToCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectedItem = (Team)lsvTo.SelectedItem;
            if (selectedItem != null)
            {
                team_id = selectedItem.team_id;
                txbSelectTo.Text = selectedItem.team_name;
                txbSelectTo.Foreground = (Brush)brus.ConvertFromString("#474747");
                bodListToCollapsed.Visibility = Visibility.Collapsed;
            }
        }
        //dropbox select group
        private void bodSelectGroup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListGroupCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListGroupCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListGroupCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Group)lsvGroup.SelectedItem;
            if (selectedItem != null)
            {
                gr_id = selectedItem.gr_id;
                txbSelectGroup.Text = selectedItem.gr_name;
                txbSelectGroup.Foreground = (Brush)brus.ConvertFromString("#474747");
                bodListGroupCollapsed.Visibility = Visibility.Collapsed;
            }

        }

        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bodListDepartmentCollapsed.Visibility = Visibility.Collapsed;
            bodListCompanyCollapsed.Visibility = Visibility.Collapsed;
            bodListToCollapsed.Visibility = Visibility.Collapsed;
            bodListGroupCollapsed.Visibility = Visibility.Collapsed;

        }

        private void Detail_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (Group)dgGroup.SelectedItem;

            ucDetailNhom uc = new ucDetailNhom(Main, selectedItem.gr_id);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            //Main.txbLoadNameFunction.Text += " / " + ((TextBlock)sender).Text;
        }

        private void bodAddNhom_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucCreateNhom(this));
        }

        private void Update_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var SelectedGroup = (Group)dgGroup.SelectedItem;
            Main.grShowPopup.Children.Add(new ucUpdateNhom(this, SelectedGroup));

        }

        private void Delete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedGroup = (Group)dgGroup.SelectedItem;
            Main.grShowPopup.Children.Add(new ucDeleteNhom(this, selectedGroup.gr_id));

        }

        private void dgGroup_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void bodFind_MouseUp(object sender, MouseButtonEventArgs e)
        {
            LoadListGroup();
        }
        #region PhanTrang

        private void borPageDau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            borPageDau.Visibility = Visibility.Collapsed;
            borLui1.Visibility = Visibility.Collapsed;
            borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
            textPage1.Text = "1";
            textPage2.Text = "2";
            textPage3.Text = "3";
            borLen1.Visibility = Visibility.Visible;
            borPageCuoi.Visibility = Visibility.Visible;
            listGroupPT = new List<Group>();
            for (int i = 0; i < 10; i++)
            {
                if (listGroup != null) listGroupPT.Add(listGroup[i]);
            }
            //listGroup = result.data.items;
            dgGroup.ItemsSource = listGroupPT;
        }

        private void borLui1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            if (int.Parse(textPage1.Text) >= 1)
            {
                if (textPage3.Text == TongSoTrang.ToString() && borPageCuoi.Visibility == Visibility.Collapsed)
                {
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPageCuoi.Visibility = Visibility.Visible;
                    borLen1.Visibility = Visibility.Visible;
                    listGroupPT = new List<Group>();
                    for (int i = TongSoTrang * 10 - 20; i < TongSoTrang * 10 - 10; i++)
                    {
                        if (listGroup != null) listGroupPT.Add(listGroup[i]);
                    }
                    //listGroup = result.data.items;
                    dgGroup.ItemsSource = listGroupPT;
                }
                else
                {
                    if (textPage1.Text == "1")
                    {
                        borPageDau.Visibility = Visibility.Collapsed;
                        borLui1.Visibility = Visibility.Collapsed;
                        borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borLen1.Visibility = Visibility.Visible;
                        borPageCuoi.Visibility = Visibility.Visible;
                        listGroupPT = new List<Group>();
                        for (int i = 0; i < 10; i++)
                        {
                            if (listGroup != null) listGroupPT.Add(listGroup[i]);
                        }
                        //listGroup = result.data.items;
                        dgGroup.ItemsSource = listGroupPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                        listGroupPT = new List<Group>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            if (listGroup != null) listGroupPT.Add(listGroup[i]);
                        }
                        //listGroup = result.data.items;
                        dgGroup.ItemsSource = listGroupPT;
                    }


                }
            }

        }

        private void borPage1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (int.Parse(textPage1.Text) >= 1)
            {
                if (textPage1.Text == (TongSoTrang - 2).ToString() && borPageCuoi.Visibility == Visibility.Collapsed && TongSoTrang > 3)
                {

                    textPage1.Text = (TongSoTrang - 3).ToString();
                    textPage2.Text = (TongSoTrang - 2).ToString();
                    textPage3.Text = (TongSoTrang - 1).ToString();


                    BrushConverter brus = new BrushConverter();

                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    if (TongSoTrang > 2)
                    {
                        borLen1.Visibility = Visibility.Visible;
                        borPageCuoi.Visibility = Visibility.Visible;
                    }

                    listGroupPT = new List<Group>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        if (listGroup != null) listGroupPT.Add(listGroup[i]);
                    }
                    //listGroup = result.data.items;
                    dgGroup.ItemsSource = listGroupPT;

                }
                else
                {

                    if (textPage1.Text == "1")
                    {
                        BrushConverter brus = new BrushConverter();
                        borPageDau.Visibility = Visibility.Collapsed;
                        borLui1.Visibility = Visibility.Collapsed;
                        borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        if (TongSoTrang > 3)
                        {
                            borLen1.Visibility = Visibility.Visible;
                            borPageCuoi.Visibility = Visibility.Visible;
                        }

                        listGroupPT = new List<Group>();
                        var numberOfItem = (TongSoTrang == 1) ? TotalListItem : 10;
                        for (int i = 0; i < numberOfItem; i++)
                        {
                            if (listGroup != null) listGroupPT.Add(listGroup[i]);
                        }
                        //listGroup = result.data.items;
                        dgGroup.ItemsSource = listGroupPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                        listGroupPT = new List<Group>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            if (listGroup != null) listGroupPT.Add(listGroup[i]);
                        }
                        //listGroup = result.data.items;
                        dgGroup.ItemsSource = listGroupPT;
                    }
                }
            }
        }

        private void borPage2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
            listGroupPT = new List<Group>();
            var numOfData = (TongSoTrang == 2) ? TotalListItem : int.Parse(textPage2.Text) * 10;
            for (int i = int.Parse(textPage2.Text) * 10 - 10; i < numOfData; i++)
            {
                if (listGroup != null) listGroupPT.Add(listGroup[i]);
            }
            if (TongSoTrang > 3)
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;

                if (textPage2.Text == (TongSoTrang - 1).ToString())
                {
                    borPageCuoi.Visibility = Visibility.Visible;
                    borLen1.Visibility = Visibility.Visible;
                }
            }


            //listGroup = result.data.items;
            dgGroup.ItemsSource = listGroupPT;

        }

        private void borPage3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {


            if (TongSoTrang == 3)
            {
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                listGroupPT = new List<Group>();
                for (int i = TongSoTrang * 10 - 10; i < TotalListItem; i++)
                {
                    if (listGroup != null) listGroupPT.Add(listGroup[i]);
                }
                //listGroup = result.data.items;
                dgGroup.ItemsSource = listGroupPT;
            }
            else if (TongSoTrang > 3)
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                if (textPage3.Text == TongSoTrang.ToString())
                {

                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Collapsed;
                    listGroupPT = new List<Group>();

                    for (int i = TongSoTrang * 10 - 10; i < TotalListItem; i++)
                    {
                        if (listGroup != null) listGroupPT.Add(listGroup[i]);
                    }
                    //listGroup = result.data.items;
                    dgGroup.ItemsSource = listGroupPT;
                }
                else if (textPage3.Text == "3")
                {
                    textPage1.Text = "2";
                    textPage2.Text = "3";
                    textPage3.Text = "4";
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    listGroupPT = new List<Group>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        if (listGroup != null) listGroupPT.Add(listGroup[i]);
                    }
                    //listGroup = result.data.items;
                    dgGroup.ItemsSource = listGroupPT;
                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                    listGroupPT = new List<Group>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        if (listGroup != null) listGroupPT.Add(listGroup[i]);
                    }
                    //listGroup = result.data.items;
                    dgGroup.ItemsSource = listGroupPT;
                }

            }
        }

        private void borLen1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (TongSoTrang == 3)
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                listGroupPT = new List<Group>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    if (listGroup != null) listGroupPT.Add(listGroup[i]);
                }
                //listGroup = result.data.items;
                dgGroup.ItemsSource = listGroupPT;
            }
            else if (TongSoTrang > 3)
            {
                if (textPage3.Text == TongSoTrang.ToString())
                {
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Collapsed;
                    listGroupPT = new List<Group>();
                    for (int i = TongSoTrang * 10 - 10; i < TotalListItem; i++)
                    {
                        if (listGroup != null) listGroupPT.Add(listGroup[i]);
                    }
                    //listGroup = result.data.items;
                    dgGroup.ItemsSource = listGroupPT;

                }
                else if (textPage3.Text == "3")
                {

                    if (borPageDau.Visibility == Visibility.Collapsed && borPageCuoi.Visibility == Visibility.Visible)
                    {
                        BrushConverter brus = new BrushConverter();
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPageDau.Visibility = Visibility.Visible;
                        borLui1.Visibility = Visibility.Visible;
                        listGroupPT = new List<Group>();
                        for (int i = 10; i < 20; i++)
                        {
                            if (listGroup != null) listGroupPT.Add(listGroup[i]);
                        }
                        //listGroup = result.data.items;
                        dgGroup.ItemsSource = listGroupPT;

                    }
                    else if (borPageDau.Visibility == Visibility.Visible && borPageCuoi.Visibility == Visibility.Visible)
                    {
                        textPage1.Text = "2";
                        textPage2.Text = "3";
                        textPage3.Text = "4";
                        BrushConverter brus = new BrushConverter();
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        listGroupPT = new List<Group>();
                        for (int i = 20; i < 30; i++)
                        {
                            if (listGroup != null) listGroupPT.Add(listGroup[i]);
                        }
                        //listGroup = result.data.items;
                        dgGroup.ItemsSource = listGroupPT;
                    }


                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                    listGroupPT = new List<Group>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        if (listGroup != null) listGroupPT.Add(listGroup[i]);
                    }
                    //listGroup = result.data.items;
                    dgGroup.ItemsSource = listGroupPT;
                }

            }
        }

        private void borPageCuoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textPage3.Text = TongSoTrang.ToString();
            textPage2.Text = (TongSoTrang - 1).ToString();
            textPage1.Text = (TongSoTrang - 2).ToString();
            borPageDau.Visibility = Visibility.Visible;
            borLui1.Visibility = Visibility.Visible;
            BrushConverter brus = new BrushConverter();
            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            borPageCuoi.Visibility = Visibility.Collapsed;
            borLen1.Visibility = Visibility.Collapsed;
            listGroupPT = new List<Group>();
            for (int i = TongSoTrang * 10 - 10; i < TotalListItem; i++)
            {
                if (listGroup != null) listGroupPT.Add(listGroup[i]);
            }
            //listGroup = result.data.items;
            dgGroup.ItemsSource = listGroupPT;
        }
        #endregion
    }
}
