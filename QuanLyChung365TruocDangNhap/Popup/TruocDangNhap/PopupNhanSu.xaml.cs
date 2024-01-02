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
	/// Interaction logic for PopupNhanSu.xaml
	/// </summary>
	public partial class PopupNhanSu : UserControl
	{
	
		public PopupNhanSu()
		{
			InitializeComponent();
		}

		private void btnExit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			bodDangKyTKMoi.Visibility = Visibility.Collapsed;
		}

		private void btnDangKy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			
		}

		private void btnDangNhap_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{

		}

		private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
		{
			
		}
	} 
}
