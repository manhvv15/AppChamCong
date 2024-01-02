using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.RecommendSetting.OOP
{
    public class API_Time_KH
    {
        public class Data_TimeDx
        {
            public List<TimeDx> time_dx { get; set; }
        }

        public class TimeDx
        {
            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            public int _id { get; set; }
            public int id_dx { get; set; }
            public string name_cate_dx { get; set; }
            public string _time;
            public string time 
            {
                get { return _time; }
                set
                {
                    _time = value; OnPropertyChanged();
                }
            }
            public int com_id { get; set; }
            public int created_time { get; set; }
            public int __v { get; set; }
        }
    }
}
