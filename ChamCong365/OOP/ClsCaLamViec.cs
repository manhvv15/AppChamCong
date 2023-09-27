using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP
{
    public class ClsCaLamViec:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string TenCaLV { get; set; }
        public string ThoiGian { get; set; }
        public string MucPhat { get; set; }
        public string NgayBatDau { get; set; }
        //public string Check { get; set; }
        private string _Check = "0";
        public string Check { get => _Check; set { _Check = value; OnPropertyChanged(); } }
    }
}
