using ChamCong365.APIs;
using ChamCong365.OOP.CaiDatLuong.CaiDatLuongCB;
using ChamCong365.OOP.funcQuanLyCongTy;
using ChamCong365.Popup.funcCompanyManager.ChildCompanyPopups;
using ChamCong365.Popup.PopupSalarySettings;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace ChamCong365
{
    /// <summary>
    /// Interaction logic for ucChildCompanyManagerment.xaml
    /// </summary>
    public partial class ucChildCompanyManagerment : UserControl
    {

        MainWindow Main;
        List<Company> listChildCompany = new List<Company>();
        List<Company> listChildCompanyPT = new List<Company>();
        private int TotalItem = 0;
        private int TongSoTrang = 0;
        private int SoDu = 0;

        public List<Company> dataCompanySelectBox = new List<Company>();

        public ucChildCompanyManagerment(MainWindow main)
        {
            InitializeComponent();
            List<string> listParentCompany = new List<string>() { "Fpt", "HHP", "TimViec" };
            Main = main;
            Company ParentCompany = new Company() { com_id = Main.IdAcount, com_name = Main.txbNameAccount.Text };
            //GetListChildCompany();
            List<Company> listDataDropBox = new List<Company>();
            dataCompanySelectBox.Add(ParentCompany);
            GetListChildCompany();
        }



        public async void GetListChildCompany()
        {
            try
            {
                listChildCompanyPT = new List<Company>();
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.list_ChildCompany_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                CompanyRoot result = JsonConvert.DeserializeObject<CompanyRoot>(responseContent);

                listChildCompany = (from item in result.data.items
                                    select new Company
                                    {
                                        com_id = item.com_id,
                                        com_parent_id = item.com_parent_id,
                                        com_name = (item.com_name == null) ? "Chưa cập nhật" : item.com_name,
                                        com_email = (item.com_email == null) ? "Chưa cập nhật" : item.com_email,
                                        com_phone_tk = (item.com_phone_tk == null) ? "Chưa cập nhật" : item.com_phone_tk,
                                        id_way_timekeeping = item.id_way_timekeeping,
                                        com_phone = (item.com_phone == null) ? "Chưa cập nhật" : item.com_phone,
                                        com_logo = (item.com_logo == null) ? "/Resource/image/CompanyLogo.png" : item.com_logo,
                                        com_address = (item.com_address == null) ? "Chưa cập nhật" : item.com_address,
                                        com_create_time = (item.com_create_time == null) ? "Chưa cập nhật" : item.com_create_time
                                    }).ToList();

                TotalItem = result.data.items.Count;

                foreach (var item in result.data.items)
                {
                    dataCompanySelectBox.Add(item);
                }
                TongSoTrang = TotalItem / 10;
                SoDu = 10 - (TotalItem % 10);
                if (TotalItem % 10 > 0)
                {
                    TongSoTrang++;
                }
                if (TotalItem <= 10)
                {
                    for (int i = 0; i < TotalItem; i++)
                    {
                        listChildCompanyPT.Add(result.data.items[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        listChildCompanyPT.Add(result.data.items[i]);
                    }
                }

                //listChildCompany = result.data.items;
                dgChildCompany.ItemsSource = listChildCompanyPT;
                if (TongSoTrang < 3)
                {
                    if (TongSoTrang == 2)
                    {
                        borPage3.Visibility = Visibility.Collapsed;
                        borLen1.Visibility = Visibility.Collapsed;
                        borPageCuoi.Visibility = Visibility.Collapsed;
                    }
                    else if (TongSoTrang == 1)
                    {
                        borPage2.Visibility = Visibility.Collapsed;
                        borPage3.Visibility = Visibility.Collapsed;
                        borLen1.Visibility = Visibility.Collapsed;
                        borPageCuoi.Visibility = Visibility.Collapsed;
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
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại!" + e.Message);
            }
        }

        private void btnAddChildCompany_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new createChildCompany(this, Main.IdAcount, listChildCompany));
        }

        private void updateChildCompany_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = dgChildCompany.SelectedItem;
            var childCompany = (Company)selectedItem;
            Main.grShowPopup.Children.Add(new updateChildCompany(this, childCompany));
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
            listChildCompanyPT = new List<Company>();
            for (int i = 0; i < 10; i++)
            {
                listChildCompanyPT.Add(listChildCompany[i]);
            }
            //listChildCompany = result.data.items;
            dgChildCompany.ItemsSource = listChildCompanyPT;
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
                    listChildCompanyPT = new List<Company>();
                    for (int i = TongSoTrang * 10 - 20; i < TongSoTrang * 10 - 10; i++)
                    {
                        listChildCompanyPT.Add(listChildCompany[i]);
                    }
                    //listChildCompany = result.data.items;
                    dgChildCompany.ItemsSource = listChildCompanyPT;
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
                        listChildCompanyPT = new List<Company>();
                        for (int i = 0; i < 10; i++)
                        {
                            listChildCompanyPT.Add(listChildCompany[i]);
                        }
                        //listChildCompany = result.data.items;
                        dgChildCompany.ItemsSource = listChildCompanyPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                        listChildCompanyPT = new List<Company>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            listChildCompanyPT.Add(listChildCompany[i]);
                        }
                        //listChildCompany = result.data.items;
                        dgChildCompany.ItemsSource = listChildCompanyPT;
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

                    listChildCompanyPT = new List<Company>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        listChildCompanyPT.Add(listChildCompany[i]);
                    }
                    //listChildCompany = result.data.items;
                    dgChildCompany.ItemsSource = listChildCompanyPT;

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

                        listChildCompanyPT = new List<Company>();
                        var numberOfItem = (TongSoTrang == 1) ? TotalItem : 10;
                        for (int i = 0; i < numberOfItem; i++)
                        {
                            listChildCompanyPT.Add(listChildCompany[i]);
                        }
                        //listChildCompany = result.data.items;
                        dgChildCompany.ItemsSource = listChildCompanyPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                        listChildCompanyPT = new List<Company>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            listChildCompanyPT.Add(listChildCompany[i]);
                        }
                        //listChildCompany = result.data.items;
                        dgChildCompany.ItemsSource = listChildCompanyPT;
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
            listChildCompanyPT = new List<Company>();
            var numOfData = (TongSoTrang == 2) ? TotalItem : int.Parse(textPage2.Text) * 10;
            for (int i = int.Parse(textPage2.Text) * 10 - 10; i < numOfData; i++)
            {
                listChildCompanyPT.Add(listChildCompany[i]);
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


            //listChildCompany = result.data.items;
            dgChildCompany.ItemsSource = listChildCompanyPT;

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
                listChildCompanyPT = new List<Company>();
                for (int i = TongSoTrang * 10 - 10; i < TotalItem; i++)
                {
                    listChildCompanyPT.Add(listChildCompany[i]);
                }
                //listChildCompany = result.data.items;
                dgChildCompany.ItemsSource = listChildCompanyPT;
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
                    listChildCompanyPT = new List<Company>();

                    for (int i = TongSoTrang * 10 - 10; i < TotalItem; i++)
                    {
                        listChildCompanyPT.Add(listChildCompany[i]);
                    }
                    //listChildCompany = result.data.items;
                    dgChildCompany.ItemsSource = listChildCompanyPT;
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
                    listChildCompanyPT = new List<Company>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        listChildCompanyPT.Add(listChildCompany[i]);
                    }
                    //listChildCompany = result.data.items;
                    dgChildCompany.ItemsSource = listChildCompanyPT;
                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                    listChildCompanyPT = new List<Company>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        listChildCompanyPT.Add(listChildCompany[i]);
                    }
                    //listChildCompany = result.data.items;
                    dgChildCompany.ItemsSource = listChildCompanyPT;
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
                listChildCompanyPT = new List<Company>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    listChildCompanyPT.Add(listChildCompany[i]);
                }
                //listChildCompany = result.data.items;
                dgChildCompany.ItemsSource = listChildCompanyPT;
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
                    listChildCompanyPT = new List<Company>();
                    for (int i = TongSoTrang * 10 - 10; i < TotalItem; i++)
                    {
                        listChildCompanyPT.Add(listChildCompany[i]);
                    }
                    //listChildCompany = result.data.items;
                    dgChildCompany.ItemsSource = listChildCompanyPT;

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
                        listChildCompanyPT = new List<Company>();
                        for (int i = 10; i < 20; i++)
                        {
                            listChildCompanyPT.Add(listChildCompany[i]);
                        }
                        //listChildCompany = result.data.items;
                        dgChildCompany.ItemsSource = listChildCompanyPT;

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
                        listChildCompanyPT = new List<Company>();
                        for (int i = 20; i < 30; i++)
                        {
                            listChildCompanyPT.Add(listChildCompany[i]);
                        }
                        //listChildCompany = result.data.items;
                        dgChildCompany.ItemsSource = listChildCompanyPT;
                    }


                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                    listChildCompanyPT = new List<Company>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        listChildCompanyPT.Add(listChildCompany[i]);
                    }
                    //listChildCompany = result.data.items;
                    dgChildCompany.ItemsSource = listChildCompanyPT;
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
            listChildCompanyPT = new List<Company>();
            for (int i = TongSoTrang * 10 - 10; i < TotalItem; i++)
            {
                listChildCompanyPT.Add(listChildCompany[i]);
            }
            //listChildCompany = result.data.items;
            dgChildCompany.ItemsSource = listChildCompanyPT;
        }
        #endregion
        private void dgChildCompany_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }
    }
}
