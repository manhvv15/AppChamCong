using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.ViewModel
{
    public class PickersViewModel : ViewModelBase
    {
        private DateTime _date;
        private DateTime _time;
        private string _validatingTime;
        private DateTime? _futureValidatingDate;

        public PickersViewModel()
        {
            Date = DateTime.Now;
            Time = DateTime.Now;
        }

        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public DateTime Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }

        public string ValidatingTime
        {
            get => _validatingTime;
            set => SetProperty(ref _validatingTime, value);
        }

        public DateTime? FutureValidatingDate
        {
            get => _futureValidatingDate;
            set => SetProperty(ref _futureValidatingDate, value);
        }
    }
}
