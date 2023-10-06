using ChamCong365.OOP.ChamCong.CaiDatLichLamViec.ChamCong365.Entities.funcQuanLyCongTy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong.CaiDatLichLamViec
{
    public class RootCalendar
    {
        public Data_Carlendar data { get; set; }
        public object error { get; set; }
    }
    public class Data_Carlendar
    {
        public List<DataCalendar> data { get; set; }
        
        public string memessage { get; set; }
    }
 
    public class DataCalendar
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool result { get; set; }
        public string message { get; set; }
        public string _id { get; set; }
        public int cy_id { get; set; }
        public string cy_name { get; set; }
        public string apply_month { get; set; }
        public string date { get; set; }
        public List<CyDetail> cy_detail { get; set; }
        private int _status;

        public int status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }
        public int is_personal { get; set; }
        public int ep_count { get; set; }
        private bool _isChecked;

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged();
            }
        }
    }
   
    public class CyDetail
    {
        [JsonProperty("date")]
        public string date { get; set; }
        [JsonProperty("shift_id")]
        public string shift_id { get; set; }
    }
   
}
