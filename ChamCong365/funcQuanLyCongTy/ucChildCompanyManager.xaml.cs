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

namespace ChamCong365
{
    /// <summary>
    /// Interaction logic for ucChildCompanyManagerment.xaml
    /// </summary>
    public partial class ucChildCompanyManagerment : UserControl
    {
        public class ChildCompany
        {
            public int Id { get;set; }
            public string Logo { get; set; }
            public string Name { get;set; } 
            public string Type { get; set; }
            public string SDT { get;set; }
            public string Address { get;set; } 
        }

        public ucChildCompanyManagerment()
        {
            InitializeComponent();

            #region FakeChildCompany
            List<ChildCompany> childCompanyList = new List<ChildCompany>();

            childCompanyList.Add(new ChildCompany { Id = 1, Logo = "img", Name = "HHP1", Type = "Công ty con", SDT = "039643209", Address="Định Công, Hoàng Mai fsfd sffds fsfd" });
            childCompanyList.Add(new ChildCompany { Id = 2, Logo = "img", Name = "HHP2", Type = "Công ty con", SDT = "039643209" });
            childCompanyList.Add(new ChildCompany { Id = 3, Logo = "img", Name = "HHP3", Type = "Công ty con", SDT = "039643209" });
            childCompanyList.Add(new ChildCompany { Id = 4, Logo = "img", Name = "HHP4", Type = "Công ty con", SDT = "039643209" });
            childCompanyList.Add(new ChildCompany { Id = 5, Logo = "img", Name = "HHP5", Type = "Công ty con", SDT = "039643209" });
            childCompanyList.Add(new ChildCompany { Id = 6, Logo = "img", Name = "HHP6", Type = "Công ty con", SDT = "039643209" });
            childCompanyList.Add(new ChildCompany { Id = 7, Logo = "img", Name = "HHP7", Type = "Công ty con", SDT = "039643209" });
            childCompanyList.Add(new ChildCompany { Id = 8, Logo = "img", Name = "HHP8", Type = "Công ty con", SDT = "039643209" });
            childCompanyList.Add(new ChildCompany { Id = 9, Logo = "img", Name = "HHP9", Type = "Công ty con", SDT = "039643209" });

            lsvChildCompany.ItemsSource= childCompanyList;
            #endregion
            List<string> listParentCompany = new List<string>() { "Fpt","HHP","TimViec"};
            lsvCongTyMe.ItemsSource = listParentCompany;


        }

        private void btnAddChildCompany_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodAddChilCompanyCollapsed.Visibility == Visibility.Collapsed)
            {
                bodAddChilCompanyCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodAddChilCompanyCollapsed.Visibility -= Visibility.Collapsed;

            }
        }
        private void btnExit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            bodAddChilCompanyCollapsed.Visibility -= Visibility.Collapsed;

        }
        private void txbChonCongTyMe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            bodCongTyMe_Collapsed.Visibility = Visibility.Visible;

        }

        private void bodDropDownBtn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodCongTyMe_Collapsed.Visibility == Visibility.Collapsed)
            {
                bodCongTyMe_Collapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodCongTyMe_Collapsed.Visibility -= Visibility.Collapsed;

            }
        }
        //Xử lý khi ấn chọn công ty
        private void lsvCongTyMe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbChonCongTyMe.Text = lsvCongTyMe.SelectedItem.ToString();
            bodCongTyMe_Collapsed.Visibility = Visibility.Collapsed;
        }

    }
}
