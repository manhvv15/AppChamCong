using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy
{

    public class ShiftData
    {
        public bool result { get; set; }
        public string message { get; set; }
        public int totalItems { get; set; }
        public List<Shift> items { get; set; }
    }

    public class Shift
    {
        public string _id { get; set; }
        public int type_end_date { get; set; }
        public int shift_id { get; set; }
        public int com_id { get; set; }
        public string shift_name { get; set; }
        public string start_time { get; set; }
        public string start_timex
        {
            get
            {
                string kq = "";
                if (!string.IsNullOrEmpty(start_time) && start_time.Contains(":"))
                {
                    var arr = start_time.Split(':');
                    int so;
                    if (int.TryParse(arr[0], out so))
                    {
                        if (so <= 12) kq = string.Format("{0}:{1}", arr[0], arr[1]);
                        if (so > 12) kq = string.Format("{0}:{1}", arr[0], arr[1]);
                    }
                }
                return kq;
            }
            set { }
        }
        public string start_time_latest { get; set; }
        public string end_timex
        {
            get
            {
                string kq = "";
                if (!string.IsNullOrEmpty(end_time) && end_time.Contains(":"))
                {
                    var arr = end_time.Split(':');
                    int so;
                    if (int.TryParse(arr[0], out so))
                    {
                        if (so <= 12) kq = string.Format("{0}:{1}", arr[0], arr[1]);
                        if (so > 12) kq = string.Format("{0}:{1}", arr[0], arr[1]);
                    }
                }
                return kq;
            }
            set { }
        }
        public string end_time { get; set; }
        public string end_time_earliest { get; set; }
        public int over_night { get; set; }
        public int shift_type { get; set; }
        public string nums_day { get; set; }
        public double num_to_calculate { get; set; }

        //private int _num_to_money;
        public int? num_to_money 
        {
            get; /*{ return _num_to_money; }*/
            set; /*{ _num_to_money = value; }*/
        }
        //public string num_to_money_fomated
        //{
        //    get { return num_to_money.ToString("N0"); }
        //    set
        //    {
        //        if (int.TryParse(value.Replace(".", ""), out int parsedLuong))
        //        {
        //            num_to_money = parsedLuong;
        //        }
        //    }
        //}

        private int? _money_per_hour;
        public int? money_per_hour 
        {
            get { return _money_per_hour; }
            set { _money_per_hour = value; }
        }
        public string money_per_hour_fomated
        {
            get { return money_per_hour?.ToString("N0"); }
            set
            {
                if (int.TryParse(value.Replace(".", ""), out int parsedLuong))
                {
                    money_per_hour = parsedLuong;
                }
            }
        }
        public int is_overtime { get; set; }
        public int status { get; set; }
        public List<RelaxTime> relaxTime { get; set; }
        public int flex { get; set; }
        public DateTime create_time { get; set; }
    }

    public class RelaxTime
    {
        public string start_time_relax { get; set; }
        public string end_time_relax { get; set; }
        public string _id { get; set; }
    }

    public class ShiftRoot
    {
        public ShiftData data { get; set; }
        public object error { get; set; }
    }


}

