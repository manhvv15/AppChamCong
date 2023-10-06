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

namespace ChamCong365.Popup.DatePicker
{
    /// <summary>
    /// Interaction logic for ucLoadMonths.xaml
    /// </summary>
    public partial class ucLoadMonths : UserControl
    {
        public ucLoadMonths()
        {
            InitializeComponent();
        }
        public void months(int numday)
        {
            txbLoadMonth.Text = numday + "";
        }

        private void bodNumberMonth_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
