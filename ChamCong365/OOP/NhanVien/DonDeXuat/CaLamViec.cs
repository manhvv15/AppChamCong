using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.NhanVien.DonDeXuat
{
    public class CaLamViec
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data_CaLamViec
        {
            public bool result { get; set; }
            public string message { get; set; }
            public int totalItems { get; set; }
            public List<Item_CaLamViec> items { get; set; }
        }

        public class Item_CaLamViec
        {
            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            public string _id { get; set; }
            public int shift_id { get; set; }
            public int com_id { get; set; }
            public string shift_name { get; set; }
            public string start_time { get; set; }
            public string start_time_latest { get; set; }
            public string end_time { get; set; }
            public string end_time_earliest { get; set; }
            public int over_night { get; set; }
            public int shift_type { get; set; }
            public double num_to_calculate { get; set; }
            public string num_to_money { get; set; }
            public int is_overtime { get; set; }
            public int status { get; set; }
            public List<RelaxTime> relaxTime { get; set; }
            public int flex { get; set; }
            public DateTime create_time { get; set; }

            private bool _ischecked;
            public bool ischecked
            {
                get { return _ischecked; }
                set
                {
                    _ischecked = value; OnPropertyChanged();
                }
            }
        }

        public class RelaxTime
        {
            public string start_time_relax { get; set; }
            public string end_time_relax { get; set; }
            public string _id { get; set; }
        }

        public class Root_CaLamViec
        {
            public Data_CaLamViec data { get; set; }
            public object error { get; set; }
        }


    }
}
