using ChamCong365.OOP.funcQuanLyCongTy;
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
using System.Xml.Linq;

namespace ChamCong365.funcQuanLyCongTy.AddNewStaffTabList
{
    /// <summary>
    /// Interaction logic for ucDetailNewStaff.xaml
    /// </summary>
    public partial class ucDetailNewStaff : UserControl
    {
        MainWindow Main;
        Employee employee;
        public ucDetailNewStaff(MainWindow main, Employee employee)
        {
            InitializeComponent();
            Main = main;
            this.employee = employee;
            LoadEmployeeInfor();
        }
        public void LoadEmployeeInfor()
        {
            try
            {
                txbID.Text = (employee.ep_id == null) ? "Chưa cập nhật" : employee.ep_id.ToString();

                txbEpName.Text = (employee.ep_name == null) ? "Chưa cập nhật" : employee.ep_name.ToString();
                txbComName.Text = (employee.com_id == null) ? "Chưa cập nhật" : employee.com_id.ToString();
                txbDepartmentName.Text = (employee.nameDeparment == null) ? "Chưa cập nhật" : employee.nameDeparment.ToString();
                txbExperience.Text = (employee.ep_exp == null) ? "Chưa cập nhật" : employee.ep_exp.ToString();
                txbStartWorkingTime.Text = (employee.start_working_time == null) ? "Chưa cập nhật" : JavaTimeStampToDateTime((double)employee.start_working_time).ToString();
                txbEmail.Text = (employee.ep_email == null) ? "Chưa cập nhật" : employee.ep_email.ToString();
                txbPhone.Text = (employee.ep_phone == null) ? "Chưa cập nhật" : employee.ep_phone.ToString();
                txbBirthDay.Text = (employee.ep_birth_day == null) ? "Chưa cập nhật" : JavaTimeStampToDateTime((double)employee.ep_birth_day).ToString();
                txbGender.Text = (employee.ep_gender == null) ? "Chưa cập nhật" : employee.ep_gender.ToString();
                txbEducation.Text = (employee.ep_education == null) ? "Chưa cập nhật" : employee.ep_education.ToString();
                txbMarried.Text = (employee.ep_married == null) ? "Chưa cập nhật" : employee.ep_married.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra");
            }

        }
        public static DateTime JavaTimeStampToDateTime(double javaTimeStamp)
        {
            // Java timestamp is milliseconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(javaTimeStamp).ToLocalTime();
            return dateTime;
        }
        private void bodEditNewStaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucEditNewStaff uc = new ucEditNewStaff();
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }
    }
}
