using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Entities;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.funcQuanLyCongTy.AddNewStaffTabList
{
    /// <summary>
    /// Interaction logic for ucDetailNewStaff.xaml
    /// </summary>
    public partial class ucDetailNewStaff : UserControl
    {
        MainWindow Main;
        List_NhanVien employee;
        public ucDetailNewStaff(MainWindow main, List_NhanVien employee)
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

                if (employee.avatarUser != null)
                {
                    imgAvata.ImageSource = new BitmapImage(new Uri(employee.avatarUser));
                }

                if (employee.ep_id != null)
                {
                    txbID.Text = (employee.ep_id)?.ToString();
                }
                if (employee.userName != null)
                {
                    txbEpName.Text = employee.userName;
                }
                txbComName.Text = (Main.IdAcount).ToString();
                if (employee.organizeDetailName != null)
                {
                    txbDepartmentName.Text = employee.organizeDetailName;
                }
                if (employee.experience != null)
                {
                    txbExperience.Text = ListItemComboboxUser.ListCbxExpEmployee.FirstOrDefault(x => x.ID == employee.experience.ToString())?.value;
                }
                if (employee.start_working_time != null)
                {
                    txbStartWorkingTime.Text = (DateTimeOffset.FromUnixTimeSeconds((long)(employee?.start_working_time)).ToLocalTime()).DateTime.ToString("dd/MM/yyyy");
                }
                if (employee.email != null)
                {
                    txbEmail.Text = (employee.email)?.ToString();
                }
                if (employee.phone != null)
                {
                    txbPhone.Text = (employee.phone)?.ToString();
                }
                if (employee.birthday != null)
                {
                    txbBirthDay.Text = (DateTimeOffset.FromUnixTimeSeconds((long)(employee?.birthday)).ToLocalTime()).DateTime.ToString("dd/MM/yyyy");
                }
                if (employee.gender != null)
                {
                    txbGender.Text = ListItemComboboxUser.ListCbxGenderEmployee.FirstOrDefault(x => x.ID == employee.gender.ToString())?.value;
                }
                if (employee.education != null)
                {
                    txbEducation.Text = ListItemComboboxUser.ListCbxEducationEmployee.FirstOrDefault(x => x.ID == employee.education.ToString())?.value;
                }
                if (employee.married != null)
                {
                    txbMarried.Text = ListItemComboboxUser.ListCbxMarriedEmployee.FirstOrDefault(x => x.ID == employee.married.ToString())?.value;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra");
            }

        }
        private ImageSource GetImageSourceFromPath(string path)
        {
            BitmapImage bitmap = new BitmapImage();

            try
            {

                bitmap.UriSource = new Uri(path);

            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., file not found, unsupported format, etc.)
                Console.WriteLine("Error loading the image: " + ex.Message);
            }

            return bitmap;
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
