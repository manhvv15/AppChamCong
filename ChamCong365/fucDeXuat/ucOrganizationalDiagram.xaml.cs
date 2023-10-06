

using ChamCong365.APIs;
using ChamCong365.fucDeXuat;
using ChamCong365.OOP.funcQuanLyCongTy;
using ChamCong365.Popup.DeXuat.OrganisationalStructureDiagram;
using ChamCong365.Popup.PopupTimeKeeping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for ucOrganisationalStructureDiagram.xaml
    /// </summary>
    public partial class ucOrganizationalchart : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {
        public organizationalStructure.InfoCompany infoCompany;
        private MainWindow Main;
        int com_id;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }



        public ucOrganizationalchart(MainWindow main)
        {
            InitializeComponent();
            main.scrollMain.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            com_id = main.IdAcount;
            this.Main = main;
            this.DataContext = this;
            WaitLoadData();
        }

        public async Task<bool> WaitLoadData()
        {
            Loadding.Visibility = Visibility.Visible;
            var list = new List<Task>() {
                    LoadOrganizationalStructure()
            };

            list.ForEach(t =>
          {
              t.ContinueWith(tt =>
              {
                  var ck = new List<bool>();
                  list.ForEach(b => ck.Add(b.IsCompleted));
                  if (!ck.Contains(false)) this.Dispatcher.Invoke(() =>
                  {
                      Loadding.Visibility = Visibility.Collapsed;
                  });
              });

          });
            return true;

        }
        public void CenterScrollView()
        {
            // Calculate the center point of the content within the ScrollViewer
            double centerY = (Main.scrollMain.ExtentHeight - Main.scrollMain.ViewportHeight) / 2;

            // Set the horizontal and vertical offsets to the center point
            Main.scrollMain.ScrollToVerticalOffset(centerY);
        }
        public async Task LoadOrganizationalStructure()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.organizationalStructure_detailInfoCompany);
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    organizationalStructure.Root result = JsonConvert.DeserializeObject<organizationalStructure.Root>(responseContent);
                    infoCompany = result.data.infoCompany;
                    //set parent company info
                    txbParentComName.Text = infoCompany.companyName;
                    txbParentComManager.Text = infoCompany.parent_manager.ToString();
                    txbParentDeputy.Text = infoCompany.parent_deputy.ToString();
                    txbTotalEmployee.Text = infoCompany.tong_nv.ToString();
                    txbTotalEmployeeTimeKeep.Text = infoCompany.tong_nv_da_diem_danh.ToString();
                    txbTotalEmployeeNoTimeKeep.Text = infoCompany.TotalEmployeeNoTimeKeep.ToString();


                    icItemListCompany.ItemsSource = infoCompany.infoChildCompany;
                    icItemListdep.ItemsSource = infoCompany.infoDep;
                    borParentComInfo.Visibility = Visibility.Visible;

                }


            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        private void NavigateToPage(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            string name = textBlock.Name;
            String uri = "OrganizationalChart/" + name + "Page";

        }


        //Chuyển sang page danh sách nhân viên tổng công ty
        private void NavigateToPageEmp(object sender, MouseButtonEventArgs e)
        {
            ucListStaff frm = new ucListStaff(com_id, -1, -1, -1);
            Main.dopBody.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            Main.dopBody.Children.Add(content as UIElement);

        }

        //Chuyển sang page danh sách nhân viên tổng công ty đã chấm công
        private void NavigateToPageEmpTime(object sender, MouseButtonEventArgs e)
        {
            ucListStaffTimeKeep frm = new ucListStaffTimeKeep(com_id, -1, -1, -1);
            Main.dopBody.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            Main.dopBody.Children.Add(content as UIElement);
        }





        //Chuyển sang page danh sách nhân viên tổng công ty chưa chấm công
        private void NavigateToPageEmpNoTime(object sender, MouseButtonEventArgs e)
        {
            ucListStaffNoTimeKeep frm = new ucListStaffNoTimeKeep(com_id,-1,-1,-1);
            Main.dopBody.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            Main.dopBody.Children.Add(content as UIElement);
        }

        //Chuyển sang page danh sách nhân viên phòng ban

        private void NavigateToDepAll(object sender, MouseButtonEventArgs e)
        {
            var grid = (Grid)sender;
            var selectedDep = grid.DataContext as organizationalStructure.InfoDep;
            if (selectedDep != null)
            {
                var dep_id = selectedDep.dep_id;
                ucListStaff frm = new ucListStaff(com_id,dep_id,-1,-1);
                Main.dopBody.Children.Clear();
                object content = frm.Content;
                frm.Content = null;
                Main.dopBody.Children.Add(content as UIElement);
            }

        }



        private void NavigateToDepNoTimeKeep(object sender, MouseButtonEventArgs e)
        {
            var grid = (Grid)sender;
            var selectedDep = grid.DataContext as organizationalStructure.InfoDep;
            if (selectedDep != null)
            {
                var dep_id = selectedDep.dep_id;
                ucListStaffNoTimeKeep frm = new ucListStaffNoTimeKeep(com_id, dep_id, -1, -1);
                Main.dopBody.Children.Clear();
                object content = frm.Content;
                frm.Content = null;
                Main.dopBody.Children.Add(content as UIElement);
            }
        }


        private void NavigateToDepTimeKeep(object sender, MouseButtonEventArgs e)
        {
            var grid = (Grid)sender;
            var selectedDep = grid.DataContext as organizationalStructure.InfoDep;
            if (selectedDep != null)
            {
                var dep_id = selectedDep.dep_id;
                ucListStaffNoTimeKeep frm = new ucListStaffNoTimeKeep(com_id, dep_id, -1, -1);
                Main.dopBody.Children.Clear();
                object content = frm.Content;
                frm.Content = null;
                Main.dopBody.Children.Add(content as UIElement);
            }
        }

        private void ShowUpdateDep(object sender, MouseButtonEventArgs e)
        {
            var SelectedChildCom = icItemListCompany.SelectedItem as organizationalStructure.InfoChildCompany;
            var selectedItem = ((Grid)sender).DataContext as organizationalStructure.InfoDep;
            if (selectedItem != null)
            {
                Main.grShowPopup.Children.Add(new ucEditDetailDepartment(this, com_id, selectedItem));
            }
        }


        private void ShowPopupOption(object sender, MouseButtonEventArgs e)
        {
            if (popupOption.Visibility == Visibility.Collapsed)
            {
                popupOption.Visibility = Visibility.Visible;
            }
            else
            {
                popupOption.Visibility = Visibility.Collapsed;
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            Grid grid = sender as Grid;
            grid.Children[0].Visibility = Visibility.Visible;
            TextBlock textBlock = (TextBlock)grid.Children[1];
            var converter = new BrushConverter();
            textBlock.Foreground = (Brush)converter.ConvertFromString("#4C5BD4");
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid grid = sender as Grid;
            grid.Children[0].Visibility = Visibility.Collapsed;
            TextBlock textBlock = (TextBlock)grid.Children[1];
            var converter = new BrushConverter();
            textBlock.Foreground = (Brush)converter.ConvertFromString("#474747");
        }

        //Chuyển page sang danh sách tổ chưa chấm công
        private void NavigateToPageNestNoTimeKeep(object sender, MouseButtonEventArgs e)
        {
            var grid = (Grid)sender;
            var selectedDep = grid.DataContext as organizationalStructure.InfoTeam;
            if (selectedDep != null)
            {
                var team_id = selectedDep.gr_id;
                ucListStaffNoTimeKeep frm = new ucListStaffNoTimeKeep(com_id, -1, team_id, -1);
                Main.dopBody.Children.Clear();
                object content = frm.Content;
                frm.Content = null;
                Main.dopBody.Children.Add(content as UIElement);
            }
        } 

        //Chuyển page sang danh sách tổ chấm công
        private void NavigateToPageNestTimeKeep(object sender, MouseButtonEventArgs e)
        {
            var grid = (Grid)sender;
            var selectedDep = grid.DataContext as organizationalStructure.InfoTeam;
            if (selectedDep != null)
            {
                var team_id = selectedDep.gr_id;
                ucListStaffTimeKeep frm = new ucListStaffTimeKeep(com_id, -1, team_id, -1);
                Main.dopBody.Children.Clear();
                object content = frm.Content;
                frm.Content = null;
                Main.dopBody.Children.Add(content as UIElement);
            }

        }

        //chuyển page sang danh sách tổng nhân viên tổ
        private void NavigateToPageNestSum(object sender, MouseButtonEventArgs e)
        {
            var grid = (Grid)sender;
            var selected = grid.DataContext as organizationalStructure.InfoTeam;
            if (selected != null)
            {
                var team_id = selected.gr_id;
                ucListStaff frm = new ucListStaff(com_id, -1, team_id, -1);
                Main.dopBody.Children.Clear();
                object content = frm.Content;
                frm.Content = null;
                Main.dopBody.Children.Add(content as UIElement);
            }
        }

        //Show chỉnh sửa tổ
        private void ShowUpdateNess(object sender, MouseButtonEventArgs e)
        {
            var SelectedTeam = ((Grid)sender).DataContext as organizationalStructure.InfoTeam;
            if (SelectedTeam != null)
            {
                Main.grShowPopup.Children.Add(new ucEditDetailNest(this, com_id, SelectedTeam));
            }

        }

        private void icListChill_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    scroll.ScrollToVerticalOffset(scroll.VerticalOffset);
                    Main.scrollMain.ScrollToHorizontalOffset(Main.scrollMain.HorizontalOffset - e.Delta);
                }
                else
                {
                    Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);

                }
            }
            catch { }
        }


        private void Border_MouseLeftButtonUp_Exit(object sender, MouseButtonEventArgs e)
        {


        }

        private void Update(object sender, MouseButtonEventArgs e)
        {

        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void CancelPopup(object sender, MouseButtonEventArgs e)
        {

            //bodDetailAndHuy.Visibility = Visibility.Collapsed;
        }

        private void ExitDetailTo_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //bodDetailAndHuy.Visibility = Visibility.Collapsed;
        }

        private void ShowUpdateGroup(object sender, MouseButtonEventArgs e)
        {
            var SelectedTeam = ((Grid)sender).DataContext as organizationalStructure.InfoGroup;
            if (SelectedTeam != null)
            {
                Main.grShowPopup.Children.Add(new ucEditDetailGroup(this, com_id, SelectedTeam));
            }

        }

        private void icItemListDepartment_MouseEnter(object sender, MouseEventArgs e)
        {
            com_id = Main.IdAcount;
        }

        private void stackChildCom_MouseEnter(object sender, MouseEventArgs e)
        {
            var stack = (StackPanel)sender;
            var selectedChildCom = stack.DataContext as organizationalStructure.InfoChildCompany;
            com_id = selectedChildCom.com_id;
        }

        private void ShowDetailDep(object sender, MouseButtonEventArgs e)
        {
            var textBlock = sender as TextBlock;
            var selectedDep = textBlock.DataContext as organizationalStructure.InfoDep;
            if (selectedDep != null)
            {
                Main.grShowPopup.Children.Add(new ucShowDetail(com_id, selectedDep.dep_id, "depId"));
            }
        }

        private void ShowDetailTeam(object sender, MouseButtonEventArgs e)
        {
            var textBlock = sender as TextBlock;
            var selected = textBlock.DataContext as organizationalStructure.InfoTeam;
            if (selected != null)
            {
                Main.grShowPopup.Children.Add(new ucShowDetail(com_id, selected.gr_id, "teamId"));
            }
        }

        private void ShowDetailGroup(object sender, MouseButtonEventArgs e)
        {
            var textBlock = sender as TextBlock;
            var selected = textBlock.DataContext as organizationalStructure.InfoGroup;
            if (selected != null)
            {
                Main.grShowPopup.Children.Add(new ucShowDetail(com_id, selected.gr_id, "groupId"));
            }
        }

        private void NavigateToPageGroupTimeKeep(object sender, MouseButtonEventArgs e)
        {
            var grid = (Grid)sender;
            var selectedGroup = grid.DataContext as organizationalStructure.InfoGroup;
            if (selectedGroup != null)
            {
                var gr_id = selectedGroup.gr_id;
                ucListStaffTimeKeep frm = new ucListStaffTimeKeep(com_id, -1, -1, gr_id);
                Main.dopBody.Children.Clear();
                object content = frm.Content;
                frm.Content = null;
                Main.dopBody.Children.Add(content as UIElement);
            }
        }

        private void NavigateToPageGroupNoTimeKeep(object sender, MouseButtonEventArgs e)
        {
            var grid = (Grid)sender;
            var selectedGroup = grid.DataContext as organizationalStructure.InfoGroup;
            if (selectedGroup != null)
            {
                var gr_id = selectedGroup.gr_id;
                ucListStaffNoTimeKeep frm = new ucListStaffNoTimeKeep(com_id, -1, -1, gr_id);
                Main.dopBody.Children.Clear();
                object content = frm.Content;
                frm.Content = null;
                Main.dopBody.Children.Add(content as UIElement);
            }
        }

        private void NavigateToPageGroupSum(object sender, MouseButtonEventArgs e)
        {
            var grid = (Grid)sender;
            var selectedGroup = grid.DataContext as organizationalStructure.InfoGroup;
            if (selectedGroup != null)
            {
                var gr_id = selectedGroup.gr_id;
                ucListStaff frm = new ucListStaff(com_id, -1, -1, gr_id);
                Main.dopBody.Children.Clear();
                object content = frm.Content;
                frm.Content = null;
                Main.dopBody.Children.Add(content as UIElement);
            }
        }

        private void NavigateToAddChildCom(object sender, MouseButtonEventArgs e)
        {
            ucChildCompanyManagerment frm = new ucChildCompanyManagerment(Main);
            Main.dopBody.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            Main.dopBody.Children.Add(content as UIElement);
        }

        private void NavigateToAddDepartment(object sender, MouseButtonEventArgs e)
        {
            ucListDpartment frm = new ucListDpartment(Main);
            Main.dopBody.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            Main.dopBody.Children.Add(content as UIElement);
        }

        private void NavigateToAddTeam(object sender, MouseButtonEventArgs e)
        {
            ucListTo frm = new ucListTo(Main);
            Main.dopBody.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            Main.dopBody.Children.Add(content as UIElement);
        }

        private void NavigateToAddGroup(object sender, MouseButtonEventArgs e)
        {
            ucListGroup frm = new ucListGroup(Main);
            Main.dopBody.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            Main.dopBody.Children.Add(content as UIElement);
        }

        private void CloseOptionPopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            popupOption.Visibility= Visibility.Collapsed;
        }
    }
}

