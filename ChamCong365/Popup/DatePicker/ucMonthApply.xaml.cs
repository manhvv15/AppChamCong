using System;
using System.Globalization;
using System.Windows.Controls;
using ChamCong365.Popup.PopupSalarySettings;

namespace ChamCong365.Popup.DatePicker
{
    /// <summary>
    /// Interaction logic for ucMonthApply.xaml
    /// </summary>
    public partial class ucMonthApply : UserControl
    {
        int month, year;
        public ucMonthApply()
        {
            InitializeComponent();
            DisplayMonth();
        }
        public void DisplayMonth()
        {
            int months = DateTime.DaysInMonth(year, month);
            DateTime now = DateTime.Now;
            for (int i = 0; i < 12; i++)
            {
                ucLoadMonths loadMonths = new ucLoadMonths();
               
            }
        }

    }
}
