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

namespace ChamCong365.Popup.PopupTimeKeeping
{
    /// <summary>
    /// Interaction logic for ucLoadFirstDaySmall.xaml
    /// </summary>
    public partial class ucLoadFirstDaySmall : UserControl
    {
        public ucLoadFirstDaySmall()
        {
            InitializeComponent();
        }
        public void days(int numday)
        {
            txbFirstDaySmall.Text = numday + "";
        }
    }
}
