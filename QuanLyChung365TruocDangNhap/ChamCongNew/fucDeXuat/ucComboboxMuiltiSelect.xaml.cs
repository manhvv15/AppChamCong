//using NPOI.SS.Formula.Functions;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.fucDeXuat
{
    /// <summary>
    /// Interaction logic for ucComboboxMuiltiSelect.xaml
    /// </summary>
    /// 

    public partial class ucComboboxMuiltiSelect : UserControl, INotifyPropertyChanged
    {
        private List<T> _list;
        List<T> List
        {
            get { return _list; }
            set { _list = value; OnPropertyChanged("List"); }
        }

        private List<T> _selectedList;
        List<T> SelectedList
        {
            get { return _selectedList; }
            set { _selectedList = value; OnPropertyChanged("SelectedList"); }
        }
        public ucComboboxMuiltiSelect()
        {
            InitializeComponent();
            
            

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UnSelectedApp_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void ComboBoxOpen_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
