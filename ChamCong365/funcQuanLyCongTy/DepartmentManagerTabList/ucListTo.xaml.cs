using Microsoft.Office.Interop.Excel;
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
using ChamCong365.Popup.funcCompanyManager;
using ChamCong365.Popup.funcCompanyManager.DepartmentManagerPopups;
using ChamCong365.funcQuanLyCongTy.DepartmentManagerTabList;
using System.Net.Http;
using ChamCong365.APIs;
using ChamCong365.OOP.funcQuanLyCongTy;
using Newtonsoft.Json;


namespace ChamCong365
{

    public partial class ucListTo : UserControl
    {
        public MainWindow Main;
        BrushConverter brus = new BrushConverter();
        public List<Company> ChildCompanyList = new List<Company>();
        public List<Department> DepartmentList = new List<Department>();
        public List<Team> listTeam = new List<Team>();
        private int com_id = -1;
        private int dep_id = -1;
        private int team_id = -1;
        List<Team> listTeamPT = new List<Team>();
        private int TotalListItem = 0;
        private int TongSoTrang = 0;
        private int SoDu = 0;

        public List<Company> dataCompanySelectBox = new List<Company>();

        public ucListTo(MainWindow main)
        {
            InitializeComponent();
            this.Main = main;
            com_id = Main.IdAcount;
            //load drop box
            LoadCompanySelectBox();
            //LoadListChildCompany();
            LoadListDepartment();

            // load data in datagrid
            LoadListTeam();

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
                if (team_id != -1) content.Add(new StringContent(team_id.ToString()), "team_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    TeamRoot result = JsonConvert.DeserializeObject<TeamRoot>(responseContent);
                    int STT = 1;
                    listTeam = (from item in result.data.data
                                select new Team
                                {
                                    STT = STT++,
                                    team_id = item.team_id,
                                    team_name = (item.team_name == "") ? "Chưa cập nhật" : item.team_name,
                                    dep_id = item.dep_id,
                                    dep_name = (item.dep_name == "") ? "Chưa cập nhật" : item.dep_name,
                                    total_emp = item.total_emp,
                                    manager = (item.manager == "") ? "Chưa cập nhật" : item.manager,
                                    deputy = (item.deputy == "") ? "Chưa cập nhật" : item.deputy
                                }).ToList();
                }
                //load select box
                if (team_id == -1) lsvTo.ItemsSource = listTeam.Prepend(new Team() { team_id = -1, team_name = "Chọn tổ" });
                LoadTableData(listTeam);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi sảy ra " + ex.Message);
            }


        }

        public void LoadTableData(List<Team> list)
        {


            listTeamPT = new List<Team>();
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
                    listTeamPT.Add(list[i]);
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
                listTeamPT = new List<Team>();
                borPage1.Visibility = Visibility.Collapsed;
            }

            dgTeam.ItemsSource = null;
            dgTeam.ItemsSource = listTeamPT;

        }



        public void LoadCompanySelectBox()
        {
            Company ParentCompany = new Company() { com_id = Main.IdAcount, com_name = Main.txbNameAccount.Text };
            //GetListChildCompany();
            List<Company> listDataDropBox = new List<Company>();
            listDataDropBox.Add(ParentCompany);
            dataCompanySelectBox.Add(ParentCompany);
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
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại!" + e.Message);
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
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
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
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại!" + e.Message);
            }
        }

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
                this.com_id = selectedItem.com_id;
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
                this.dep_id = selectedItem.dep_id;
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
                this.team_id = selectedItem.team_id;
                txbSelectTo.Text = selectedItem.team_name;
                txbSelectTo.Foreground = (Brush)brus.ConvertFromString("#474747");
                bodListToCollapsed.Visibility = Visibility.Collapsed;
            }

        }

        //xử lý ấn nút thêm phòng ban
        private void bodAddDepartment_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }




        private void bodAddTo_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucCreateTo(this, this.com_id.ToString()));
        }

        private void Update_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (Team)dgTeam.SelectedItem;
            Main.grShowPopup.Children.Add(new ucUpdateTo(this, this.com_id.ToString(), selectedItem));

        }

        private void Delete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (Team)dgTeam.SelectedItem;

            Main.grShowPopup.Children.Add(new ucDeleteTo(this, selectedItem.team_id));

        }

        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bodListDepartmentCollapsed.Visibility = Visibility.Collapsed;
            bodListCompanyCollapsed.Visibility = Visibility.Collapsed;
            bodListToCollapsed.Visibility = Visibility.Collapsed;

        }

        private void Detail_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (Team)dgTeam.SelectedItem;

            ucDetailTo uc = new ucDetailTo(Main, selectedItem.team_id);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            //Main.txbLoadNameFunction.Text += " / " + ((TextBlock)sender).Text;
        }

        private void dgTeam_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
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
            listTeamPT = new List<Team>();
            for (int i = 0; i < 10; i++)
            {
                if (listTeam != null) listTeamPT.Add(listTeam[i]);
            }
            //listTeam = result.data.items;
            dgTeam.ItemsSource = listTeamPT;
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
                    listTeamPT = new List<Team>();
                    for (int i = TongSoTrang * 10 - 20; i < TongSoTrang * 10 - 10; i++)
                    {
                        if (listTeam != null) listTeamPT.Add(listTeam[i]);
                    }
                    //listTeam = result.data.items;
                    dgTeam.ItemsSource = listTeamPT;
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
                        listTeamPT = new List<Team>();
                        for (int i = 0; i < 10; i++)
                        {
                            if (listTeam != null) listTeamPT.Add(listTeam[i]);
                        }
                        //listTeam = result.data.items;
                        dgTeam.ItemsSource = listTeamPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                        listTeamPT = new List<Team>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            if (listTeam != null) listTeamPT.Add(listTeam[i]);
                        }
                        //listTeam = result.data.items;
                        dgTeam.ItemsSource = listTeamPT;
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

                    listTeamPT = new List<Team>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        if (listTeam != null) listTeamPT.Add(listTeam[i]);
                    }
                    //listTeam = result.data.items;
                    dgTeam.ItemsSource = listTeamPT;

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

                        listTeamPT = new List<Team>();
                        var numberOfItem = (TongSoTrang == 1) ? TotalListItem : 10;
                        for (int i = 0; i < numberOfItem; i++)
                        {
                            if (listTeam != null) listTeamPT.Add(listTeam[i]);
                        }
                        //listTeam = result.data.items;
                        dgTeam.ItemsSource = listTeamPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                        listTeamPT = new List<Team>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            if (listTeam != null) listTeamPT.Add(listTeam[i]);
                        }
                        //listTeam = result.data.items;
                        dgTeam.ItemsSource = listTeamPT;
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
            listTeamPT = new List<Team>();
            var numOfData = (TongSoTrang == 2) ? TotalListItem : int.Parse(textPage2.Text) * 10;
            for (int i = int.Parse(textPage2.Text) * 10 - 10; i < numOfData; i++)
            {
                if (listTeam != null) listTeamPT.Add(listTeam[i]);
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


            //listTeam = result.data.items;
            dgTeam.ItemsSource = listTeamPT;

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
                listTeamPT = new List<Team>();
                for (int i = TongSoTrang * 10 - 10; i < TotalListItem; i++)
                {
                    if (listTeam != null) listTeamPT.Add(listTeam[i]);
                }
                //listTeam = result.data.items;
                dgTeam.ItemsSource = listTeamPT;
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
                    listTeamPT = new List<Team>();

                    for (int i = TongSoTrang * 10 - 10; i < TotalListItem; i++)
                    {
                        if (listTeam != null) listTeamPT.Add(listTeam[i]);
                    }
                    //listTeam = result.data.items;
                    dgTeam.ItemsSource = listTeamPT;
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
                    listTeamPT = new List<Team>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        if (listTeam != null) listTeamPT.Add(listTeam[i]);
                    }
                    //listTeam = result.data.items;
                    dgTeam.ItemsSource = listTeamPT;
                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                    listTeamPT = new List<Team>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        if (listTeam != null) listTeamPT.Add(listTeam[i]);
                    }
                    //listTeam = result.data.items;
                    dgTeam.ItemsSource = listTeamPT;
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
                listTeamPT = new List<Team>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    if (listTeam != null) listTeamPT.Add(listTeam[i]);
                }
                //listTeam = result.data.items;
                dgTeam.ItemsSource = listTeamPT;
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
                    listTeamPT = new List<Team>();
                    for (int i = TongSoTrang * 10 - 10; i < TotalListItem; i++)
                    {
                        if (listTeam != null) listTeamPT.Add(listTeam[i]);
                    }
                    //listTeam = result.data.items;
                    dgTeam.ItemsSource = listTeamPT;

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
                        listTeamPT = new List<Team>();
                        for (int i = 10; i < 20; i++)
                        {
                            if (listTeam != null) listTeamPT.Add(listTeam[i]);
                        }
                        //listTeam = result.data.items;
                        dgTeam.ItemsSource = listTeamPT;

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
                        listTeamPT = new List<Team>();
                        for (int i = 20; i < 30; i++)
                        {
                            if (listTeam != null) listTeamPT.Add(listTeam[i]);
                        }
                        //listTeam = result.data.items;
                        dgTeam.ItemsSource = listTeamPT;
                    }


                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                    listTeamPT = new List<Team>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        if (listTeam != null) listTeamPT.Add(listTeam[i]);
                    }
                    //listTeam = result.data.items;
                    dgTeam.ItemsSource = listTeamPT;
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
            listTeamPT = new List<Team>();
            for (int i = TongSoTrang * 10 - 10; i < TotalListItem; i++)
            {
                if (listTeam != null) listTeamPT.Add(listTeam[i]);
            }
            //listTeam = result.data.items;
            dgTeam.ItemsSource = listTeamPT;
        }
        #endregion

        private void borFind_MouseUp(object sender, MouseButtonEventArgs e)
        {

            LoadListTeam();

        }
    }

}
