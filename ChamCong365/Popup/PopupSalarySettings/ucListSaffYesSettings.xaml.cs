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

namespace ChamCong365.Popup.ListTabInsurance
{
    /// <summary>
    /// Interaction logic for ucListSaffYesSettings.xaml
    /// </summary>
    public partial class ucListSaffYesSettings : UserControl
    {
        List<InsuranceSafff> itemsis = new List<InsuranceSafff>();
        public ucListSaffYesSettings()
        {
            InitializeComponent();
            itemsis.Add(new InsuranceSafff() { Name = "Nguyễn Văn A", PhongBan = "Phòng 13", ID = 100001, Anh = "./Resource/image/KyTo.jpg" });
            itemsis.Add(new InsuranceSafff() { Name = "Nguyễn Văn B", PhongBan = "Phòng 13", ID = 200002, Anh = "./Resource/image/KyTo.jpg" });
            itemsis.Add(new InsuranceSafff() { Name = "Nguyễn Văn C", PhongBan = "Phòng 13", ID = 300003, Anh = "./Resource/image/KyTo.jpg" });
            itemsis.Add(new InsuranceSafff() { Name = "Nguyễn Văn D", PhongBan = "Phòng 13", ID = 400004, Anh = "./Resource/image/KyTo.jpg" });
            itemsis.Add(new InsuranceSafff() { Name = "Nguyễn Văn E", PhongBan = "Phòng 13", ID = 500005, Anh = "./Resource/image/KyTo.jpg" });
            itemsis.Add(new InsuranceSafff() { Name = "Nguyễn Văn F", PhongBan = "Phòng 13", ID = 600006, Anh = "./Resource/image/KyTo.jpg" });
            itemsis.Add(new InsuranceSafff() { Name = "Nguyễn Văn I", PhongBan = "Phòng 13", ID = 700007, Anh = "./Resource/image/KyTo.jpg" });
            itemsis.Add(new InsuranceSafff() { Name = "Nguyễn Văn K", PhongBan = "Phòng 13", ID = 800008, Anh = "./Resource/image/KyTo.jpg" });
            lsvListSaffYes.ItemsSource = itemsis;
        }

        private void bodSelectMonthYes_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void bodSelectYearYes_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void bodSelectNameSaffInsuranceYes_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void bodSelectRoomYes_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
