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
using ChamCong365.APIs;
using ChamCong365.funcQuanLyCongTy.DepartmentManagerTabList;
using ChamCong365.OOP.funcQuanLyCongTy;
using ChamCong365.Popup;
using ChamCong365.Popup.funcCompanyManager;
using ChamCong365.Popup.funcCompanyManager.DepartmentManagerPopups;
using Newtonsoft.Json;

namespace ChamCong365
{

    public partial class ucListDpartment : UserControl
    {
        MainWindow Main;
        int com_id = -1;
        int dep_id = -1;
        BrushConverter bc = new BrushConverter();
        List<Department> listDepartment = new List<Department>();
        List<Department> listDepartmentPT = new List<Department>();
        private int TotalListItem = 0;
        private int TongSoTrang = 0;
        private int SoDu = 0;

        public List<Company> dataCompanySelectBox = new List<Company>();
        public ucListDpartment(MainWindow main)
        {
            InitializeComponent();
            this.Main = main;
            com_id = main.IdAcount;
            LoadListDepartment();
            LoadCompanySelectBox();
            LoadDepartmentSelectBox();
            //GetlistChildCompany();



        }
        public void LoadCompanySelectBox()
        {
            Company ParentCompany = new Company() { com_id = Main.IdAcount, com_name = Main.txbNameAccount.Text };
            //GetListChildCompany();
            List<Company> listDataDropBox = new List<Company>();
            listDataDropBox.Add(ParentCompany);
            dataCompanySelectBox.Add(ParentCompany);
            lsvCompany.ItemsSource = dataCompanySelectBox;

        }
        public async void LoadDepartmentSelectBox()
        {
            try
            {
                List<Department> departmentsSelectBox = new List<Department>();
                departmentsSelectBox.Add(new Department() { dep_id = -1, dep_name = "Chọn phòng ban" });
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Post;
                string api = API.list_department_api;

                request.RequestUri = new Uri(api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                if (com_id != -1) content.Add(new StringContent(com_id.ToString()), "com_id");
                if (dep_id != -1) content.Add(new StringContent(dep_id.ToString()), "dep_id");

                request.Content = content;
                var response = await httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                DepartmentRoot result = JsonConvert.DeserializeObject<DepartmentRoot>(responseContent);
                if (result.data.items != null)
                {
                    lsvDepartment.ItemsSource = departmentsSelectBox.Concat(result.data.items);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load phòng ban");
            }
        }

        public async void LoadListDepartment()
        {
            try
            {

                listDepartment = new List<Department>();
                listDepartmentPT = new List<Department>();
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Post;
                string api = API.list_department_api;

                request.RequestUri = new Uri(api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                if (com_id != -1) content.Add(new StringContent(com_id.ToString()), "com_id");
                if (dep_id != -1) content.Add(new StringContent(dep_id.ToString()), "dep_id");

                request.Content = content;
                var response = await httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                DepartmentRoot result = JsonConvert.DeserializeObject<DepartmentRoot>(responseContent);
                if (result.data.items != null)
                {
                    int STT = 1;
                    listDepartment = (from item in result.data.items
                                      select new Department
                                      {
                                          STT = STT++,

                                          _id = item._id,
                                          dep_id = item.dep_id,
                                          com_id = item.com_id,
                                          dep_name = item.dep_name,
                                          dep_create_time = item.dep_create_time,
                                          manager_id = item.manager_id,
                                          dep_order = item.dep_order,
                                          total_emp = item.total_emp,
                                          manager = (item.manager == "") ? "Chưa cập nhật" : item.manager,
                                          deputy = (item.deputy == "") ? "Chưa cập nhật" : item.deputy
                                      }).ToList();
                }

                LoadTableData(listDepartment);



            }
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại!" + e.Message);
            }
        }

        public void LoadTableData(List<Department> departmentList)
        {
            listDepartmentPT = new List<Department>();

            borLui1.Visibility = Visibility.Collapsed;
            borPageDau.Visibility = Visibility.Collapsed;
            borLen1.Visibility = Visibility.Visible;
            borPageCuoi.Visibility = Visibility.Visible;
            borPage1.Visibility = Visibility.Visible;
            borPage2.Visibility = Visibility.Visible;
            borPage3.Visibility = Visibility.Visible;
            // ẩn phân trang khi không có dũ liệu
            if (departmentList.Count > 0)
            {
                TotalListItem = departmentList.Count;
                TongSoTrang = (int)Math.Ceiling(TotalListItem / 10.0);
                SoDu = 10 - (TotalListItem % 10);
                var numberOfItem = (TongSoTrang == 1) ? TotalListItem : 10;
                for (int i = 0; i < numberOfItem; i++)
                {
                    listDepartmentPT.Add(departmentList[i]);
                }
                //listDepartment = departmentList;
                dgDSPhongBan.ItemsSource = listDepartmentPT;


                textPage1.Text = "1";
                textPage2.Text = "2";
                textPage3.Text = "3";
                borLui1.Visibility = Visibility.Collapsed;
                borPageDau.Visibility = Visibility.Collapsed;
                borLen1.Visibility = Visibility.Collapsed;
                borPageCuoi.Visibility = Visibility.Collapsed;
                borPage1.Background = (Brush)bc.ConvertFrom("#4c5bd4");
                textPage1.Foreground = (Brush)bc.ConvertFrom("#ffffff");
                borPage2.Background = (Brush)bc.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)bc.ConvertFrom("#474747");
                borPage3.Background = (Brush)bc.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)bc.ConvertFrom("#474747");

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
                listDepartmentPT = new List<Department>();
                dgDSPhongBan.ItemsSource = listDepartmentPT;
                borLui1.Visibility = Visibility.Collapsed;
                borPageDau.Visibility = Visibility.Collapsed;
                borLen1.Visibility = Visibility.Collapsed;
                borPageCuoi.Visibility = Visibility.Collapsed;
                borPage1.Visibility = Visibility.Visible;
                borPage2.Visibility = Visibility.Collapsed;
                borPage3.Visibility = Visibility.Collapsed;
            }




        }

        //get danh sách tên công ty con đổ vào dropdown box chọn công ty
        private async void GetlistChildCompany()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.list_ChildCompany_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                CompanyRoot result = JsonConvert.DeserializeObject<CompanyRoot>(responseContent);

                foreach (var item in result.data.items)
                {
                    dataCompanySelectBox.Add(item);
                }
                lsvCompany.ItemsSource = dataCompanySelectBox;
            }
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại!" + e.Message);
            }
        }

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
                txbSelectCompany.Foreground = (Brush)bc.ConvertFromString("#474747");
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
                txbSelectDepartment.Foreground = (Brush)bc.ConvertFromString("#474747");
                bodListDepartmentCollapsed.Visibility = Visibility.Collapsed;
            }

        }

        //xử lý ấn nút thêm phòng ban
        private void bodAddDepartment_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucCreateDepartment(this, Main.IdAcount.ToString()));
        }


        private void UpdateDepartment_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedDepartment = (Department)dgDSPhongBan.SelectedItem;
            Main.grShowPopup.Children.Add(new ucUpdateDepartment(this, selectedDepartment));

        }

        private void DeleteDepartment_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedDepartment = (Department)dgDSPhongBan.SelectedItem;
            Main.grShowPopup.Children.Add(new ucDeleteDepartment(this, Main.IdAcount, selectedDepartment.dep_id));
        }

        private void Detail_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedDepartment = (Department)dgDSPhongBan.SelectedItem;

            ucDetailDepartment uc = new ucDetailDepartment(Main, selectedDepartment.dep_id);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            //Main.txbLoadNameFunction.Text +=" / " + ((TextBlock)sender).Text;
        }

        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bodListCompanyCollapsed.Visibility = Visibility.Collapsed;
            bodListDepartmentCollapsed.Visibility = Visibility.Collapsed;
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
            listDepartmentPT = new List<Department>();
            for (int i = 0; i < 10; i++)
            {
                if (listDepartment != null) listDepartmentPT.Add(listDepartment[i]);
            }
            //listDepartment = result.data.items;
            dgDSPhongBan.ItemsSource = listDepartmentPT;
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
                    listDepartmentPT = new List<Department>();
                    for (int i = TongSoTrang * 10 - 20; i < TongSoTrang * 10 - 10; i++)
                    {
                        if (listDepartment != null) listDepartmentPT.Add(listDepartment[i]);
                    }
                    //listDepartment = result.data.items;
                    dgDSPhongBan.ItemsSource = listDepartmentPT;
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
                        listDepartmentPT = new List<Department>();
                        for (int i = 0; i < 10; i++)
                        {
                            if (listDepartment != null) listDepartmentPT.Add(listDepartment[i]);
                        }
                        //listDepartment = result.data.items;
                        dgDSPhongBan.ItemsSource = listDepartmentPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                        listDepartmentPT = new List<Department>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            if (listDepartment != null) listDepartmentPT.Add(listDepartment[i]);
                        }
                        //listDepartment = result.data.items;
                        dgDSPhongBan.ItemsSource = listDepartmentPT;
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

                    listDepartmentPT = new List<Department>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        if (listDepartment != null) listDepartmentPT.Add(listDepartment[i]);
                    }
                    //listDepartment = result.data.items;
                    dgDSPhongBan.ItemsSource = listDepartmentPT;

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

                        listDepartmentPT = new List<Department>();
                        var numberOfItem = (TongSoTrang == 1) ? TotalListItem : 10;
                        for (int i = 0; i < numberOfItem; i++)
                        {
                            if (listDepartment != null) listDepartmentPT.Add(listDepartment[i]);
                        }
                        //listDepartment = result.data.items;
                        dgDSPhongBan.ItemsSource = listDepartmentPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                        listDepartmentPT = new List<Department>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            if (listDepartment != null) listDepartmentPT.Add(listDepartment[i]);
                        }
                        //listDepartment = result.data.items;
                        dgDSPhongBan.ItemsSource = listDepartmentPT;
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
            listDepartmentPT = new List<Department>();
            var numOfData = (TongSoTrang == 2) ? TotalListItem : int.Parse(textPage2.Text) * 10;
            for (int i = int.Parse(textPage2.Text) * 10 - 10; i < numOfData; i++)
            {
                if (listDepartment != null) listDepartmentPT.Add(listDepartment[i]);
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


            //listDepartment = result.data.items;
            dgDSPhongBan.ItemsSource = listDepartmentPT;

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
                listDepartmentPT = new List<Department>();
                for (int i = TongSoTrang * 10 - 10; i < TotalListItem; i++)
                {
                    if (listDepartment != null) listDepartmentPT.Add(listDepartment[i]);
                }
                //listDepartment = result.data.items;
                dgDSPhongBan.ItemsSource = listDepartmentPT;
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
                    listDepartmentPT = new List<Department>();

                    for (int i = TongSoTrang * 10 - 10; i < TotalListItem; i++)
                    {
                        if (listDepartment != null) listDepartmentPT.Add(listDepartment[i]);
                    }
                    //listDepartment = result.data.items;
                    dgDSPhongBan.ItemsSource = listDepartmentPT;
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
                    listDepartmentPT = new List<Department>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        if (listDepartment != null) listDepartmentPT.Add(listDepartment[i]);
                    }
                    //listDepartment = result.data.items;
                    dgDSPhongBan.ItemsSource = listDepartmentPT;
                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                    listDepartmentPT = new List<Department>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        if (listDepartment != null) listDepartmentPT.Add(listDepartment[i]);
                    }
                    //listDepartment = result.data.items;
                    dgDSPhongBan.ItemsSource = listDepartmentPT;
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
                listDepartmentPT = new List<Department>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    if (listDepartment != null) listDepartmentPT.Add(listDepartment[i]);
                }
                //listDepartment = result.data.items;
                dgDSPhongBan.ItemsSource = listDepartmentPT;
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
                    listDepartmentPT = new List<Department>();
                    for (int i = TongSoTrang * 10 - 10; i < TotalListItem; i++)
                    {
                        if (listDepartment != null) listDepartmentPT.Add(listDepartment[i]);
                    }
                    //listDepartment = result.data.items;
                    dgDSPhongBan.ItemsSource = listDepartmentPT;

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
                        listDepartmentPT = new List<Department>();
                        for (int i = 10; i < 20; i++)
                        {
                            if (listDepartment != null) listDepartmentPT.Add(listDepartment[i]);
                        }
                        //listDepartment = result.data.items;
                        dgDSPhongBan.ItemsSource = listDepartmentPT;

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
                        listDepartmentPT = new List<Department>();
                        for (int i = 20; i < 30; i++)
                        {
                            if (listDepartment != null) listDepartmentPT.Add(listDepartment[i]);
                        }
                        //listDepartment = result.data.items;
                        dgDSPhongBan.ItemsSource = listDepartmentPT;
                    }


                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                    listDepartmentPT = new List<Department>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        if (listDepartment != null) listDepartmentPT.Add(listDepartment[i]);
                    }
                    //listDepartment = result.data.items;
                    dgDSPhongBan.ItemsSource = listDepartmentPT;
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
            listDepartmentPT = new List<Department>();
            for (int i = TongSoTrang * 10 - 10; i < TotalListItem; i++)
            {
                if (listDepartment != null) listDepartmentPT.Add(listDepartment[i]);
            }
            //listDepartment = result.data.items;
            dgDSPhongBan.ItemsSource = listDepartmentPT;
        }
        #endregion
        private void dgDSPhongBan_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }
        //Lọc dữ liệu sau khi ấn tìm kiếm
        private void bodFind_MouseUp(object sender, MouseButtonEventArgs e)
        {

            LoadListDepartment();
        }
    }
}
