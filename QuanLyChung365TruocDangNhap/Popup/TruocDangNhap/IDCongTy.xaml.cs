using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyChung365TruocDangNhap.Popup.TruocDangNhap
{
	/// <summary>
	/// Interaction logic for IDCongTy.xaml
	/// </summary>
	public partial class IDCongTy : Window
	{

		

		public IDCongTy()
		{
			InitializeComponent();
			
		}
		Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);

		private void txtIDCompany_LostFocus(object sender, RoutedEventArgs e)
		{
		    tblIDCompany.Text = "";
			if (string.IsNullOrEmpty(txtIDCompany.Text))
			{
				tblIDCompany.Text = "Không được để trống";
			}
		
		}

		private void btnAddDangKy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
		
		}

		private void btnAddDangNhap_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			
		}

		private void Back(object sender, MouseButtonEventArgs e)
		{
		
		}

		private void Continue(object sender, MouseButtonEventArgs e)
		{
		}
	}
}
