using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyChung365TruocDangNhap.Popup.TruocDangNhap
{
    /// <summary>
    /// Interaction logic for PopUpDangXuat.xaml
    /// </summary>
    public partial class PopUpDangKy : UserControl
	{

	
		public PopUpDangKy()
		{
			InitializeComponent();

			
		}
		private frmMain frmMainW;
		public PopUpDangKy(frmMain main)
		{
			InitializeComponent();
			frmMainW = main;
			
		}
		private void btnExit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			this.Visibility = Visibility.Collapsed;
		}

		
		private void NhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			frmMainW.pnlDangKy.Visibility = Visibility.Visible;
			frmMainW.pnlMain.Visibility = Visibility.Collapsed;
			frmMainW.PopCT.Visibility = Visibility.Collapsed;
			frmMainW.PopNV.Visibility = Visibility.Collapsed;
			frmMainW.PopIdCT.Visibility = Visibility.Visible;
			frmMainW.btnDisplayFullSizebar.Visibility = Visibility.Collapsed;
			frmMainW.btnCollapseSizebar.Visibility = Visibility.Collapsed;
			frmMainW.pnlDangNhapCTy.Visibility = Visibility.Collapsed;
			this.Visibility = Visibility.Collapsed;
		
		}

		private void CongTy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			frmMainW.pnlDangKy.Visibility = Visibility.Visible;
			frmMainW.pnlMain.Visibility = Visibility.Collapsed;
			frmMainW.PopCT.Visibility = Visibility.Visible;
			frmMainW.PopNV.Visibility = Visibility.Collapsed;
			frmMainW.PopIdCT.Visibility = Visibility.Collapsed;
			frmMainW.btnDisplayFullSizebar.Visibility = Visibility.Collapsed;
			frmMainW.btnCollapseSizebar.Visibility = Visibility.Collapsed;
			frmMainW.pnlDangNhapCTy.Visibility = Visibility.Collapsed;
			this.Visibility = Visibility.Collapsed;
        }

		private void CaNhan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			
		}

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
			this.Visibility = Visibility.Collapsed;
        }
    }
}
