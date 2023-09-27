using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.CaiDatLuong.CaiDatDiMuonVeSom
{
    public class clsDSCaiDatNghiSaiQD
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class ListPhatCa: INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            public string _id { get; set; }
            public int pc_id { get; set; }
            public int pc_money { get; set; }
            public string pc_money_str { get; set; }
            public DateTime pc_time { get; set; }
            public string pc_time_s { get; set; }
            public int pc_shift { get; set; }
            public string name_shift { get; set; }
            public string start_date { get; set; }
            public string end_date { get; set; }
            public int pc_com { get; set; }
            public int pc_type { get; set; }
            private int _OnOff { get; set;}
            public int OnOff
            {
                get { return _OnOff; }
                set { _OnOff = value; OnPropertyChanged(); }
            }
            public List<Shift> shifts { get; set; }
        }

        public class Root
        {
            public bool data { get; set; }
            public string message { get; set; }
            public List<ListPhatCa> listPhatCa { get; set; }
        }

        public class Shift
        {
            public string _id { get; set; }
            public int shift_id { get; set; }
            public int com_id { get; set; }
            public string shift_name { get; set; }
            public string start_time { get; set; }
            public string start_time_latest { get; set; }
            public string end_time { get; set; }
            public string end_time_earliest { get; set; }
            public DateTime create_time { get; set; }
            public int over_night { get; set; }
            public int shift_type { get; set; }
            public double num_to_calculate { get; set; }
            public int num_to_money { get; set; }
            public int is_overtime { get; set; }
            public int status { get; set; }
        }


    }
}
