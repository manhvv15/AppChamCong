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

namespace QuanLyChung365TruocDangNhap.Popup.TruocDangNhap
{
	/// <summary>
	/// Interaction logic for PopupDangNhap.xaml
	/// </summary>
	public partial class PopupDangNhap : UserControl
	{
		
		private frmMain frmMainW;
		public PopupDangNhap()
		{
			InitializeComponent();
		
		}
		public PopupDangNhap(frmMain main)
		{
			InitializeComponent();
			frmMainW = main;
		}
		private void btnExit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			this.Visibility = Visibility.Collapsed;
		}

		private void btnAddNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			
			frmMainW.pnlDangKy.Visibility = Visibility.Visible;
			frmMainW.pnlMain.Visibility = Visibility.Collapsed;
			frmMainW.PopCT.Visibility = Visibility.Collapsed;
			frmMainW.PopNV.Visibility = Visibility.Collapsed;
			frmMainW.PopIdCT.Visibility = Visibility.Collapsed;
			frmMainW.btnDisplayFullSizebar.Visibility = Visibility.Collapsed;
			frmMainW.btnCollapseSizebar.Visibility = Visibility.Collapsed;
			frmMainW.pnlDangNhapCTy.Visibility = Visibility.Visible;

			frmMainW.textLoaiTK.Text = "TÀI KHOẢN NHÂN VIÊN";
			frmMainW.tb_TaiKhoanDangNhap.Text = Properties.Settings.Default.UserEp;
			frmMainW.tb_MatKhauGo.Password = Properties.Settings.Default.PassEp;
			this.Visibility = Visibility.Collapsed;



		}
		public void SetAllTableCollapsed()
		{

		}

		private void btnAddCongTy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			frmMainW.pnlDangKy.Visibility = Visibility.Visible;
			frmMainW.pnlMain.Visibility = Visibility.Collapsed;
			frmMainW.PopCT.Visibility = Visibility.Collapsed;
			frmMainW.PopNV.Visibility = Visibility.Collapsed;
			frmMainW.PopIdCT.Visibility = Visibility.Collapsed;
			frmMainW.btnDisplayFullSizebar.Visibility = Visibility.Collapsed;
			frmMainW.btnCollapseSizebar.Visibility = Visibility.Collapsed;
			frmMainW.pnlDangNhapCTy.Visibility = Visibility.Visible;
			frmMainW.textLoaiTK.Text = "TÀI KHOẢN CÔNG TY";
			frmMainW.tb_TaiKhoanDangNhap.Text = Properties.Settings.Default.User;
			frmMainW.tb_MatKhauGo.Password = Properties.Settings.Default.Pass;
			this.Visibility = Visibility.Collapsed;
		
		}

		private void btnAddCaNhan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			
		}

		private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			this.Visibility = Visibility.Collapsed;
		}
	}
}
