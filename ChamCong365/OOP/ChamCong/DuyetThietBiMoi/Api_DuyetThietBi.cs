using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong.DuyetThietBiMoi
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data_TBDuyet
    {
        public bool result { get; set; }
        public string message { get; set; }
        public int totalItems { get; set; }
        public List<Item_TBDuyet> items { get; set; }
    }

    public class Item_TBDuyet : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string ed_id { get; set; }
        public string current_device { get; set; }
        public string current_device_name { get; set; }
        public object new_device { get; set; }
        public object new_device_name { get; set; }
        public int ep_id { get; set; }
        public string ep_email { get; set; }
        public string ep_name { get; set; }
        public int dep_id { get; set; }
        public int com_id { get; set; }
        public string ep_phone { get; set; }
        public string ep_image
        {
            get;
            set;
        }
        public string position_id { get; set; }
        public string ep_status { get; set; }
        private bool _isCheck;
        public bool isCheck
        {
            get { return _isCheck; }
            set { _isCheck = value; OnPropertyChanged(); }
        }

        public string positionName { get; set; }
        public string dep_name { get; set; }
    }

    public class Root_TBDuyet
    {
        public Data_TBDuyet data { get; set; }
        public object error { get; set; }
    }


}
