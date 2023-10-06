using ChamCong365.TimeKeeping;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.CaiDatLuong.ChinhSachBaoHiem
{

    /// <summary>
    /// Interaction logic for ucAddSaffToGroundInsurance.xaml
    /// </summary>
    public partial class ucThemNhanVienVaoNhomBH : UserControl
    {
        //List<Saff> saffs = new List<Saff>();
        BrushConverter br = new BrushConverter();
        public ucThemNhanVienVaoNhomBH()
        {
            InitializeComponent();
            
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ExitCreateSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility=System.Windows.Visibility.Collapsed;    
        }
    }
}
