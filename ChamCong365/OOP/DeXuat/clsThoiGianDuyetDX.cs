using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.DeXuat
{
    public class clsThoiGianDuyetDX
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Root
        {
            public List<TimeDx> time_dx { get; set; }
        }

        public class TimeDx:INotifyPropertyChanged
        {

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            //public string _id { get; set; }
            public string id_dx { get; set; }
            public string name_cate_dx { get; set; }
            public string name_cate_dx_lo { get; set; }
            public string time { get; set; }
            private string _on_off;
            public string on_off 
            {
                get { return _on_off; }
                set { _on_off = value; OnPropertyChanged(); }
            }
            public string Text1 { get; set; }
            public string Text2 { get; set; }
            public string Text3 { get; set; }
            public string Text4 { get; set; }
            //public string created_time { get; set; }
            //public string __v { get; set; }
        }


    }
}
