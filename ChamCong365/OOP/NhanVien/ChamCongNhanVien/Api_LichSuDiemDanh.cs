using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.NhanVien.ChamCongNhanVien
{
    
    public class Data_LichSuDiemDanh
    {
        public bool result { get; set; }
        public string message { get; set; }
        public int totalItems { get; set; }
        public List<Item_LichSuDiemDanh> items { get; set; }
    }

    public class Item_LichSuDiemDanh
    {
        public int sheet_id { get; set; }
        public int ep_id { get; set; }
        public string ts_image { get; set; }
        public string at_time { get; set; }
        public DateTime At_time
        {
            get
            {
                DateTime aa;
                if (!string.IsNullOrEmpty(at_time) && DateTime.TryParse(at_time, out aa)) return aa;
                return new DateTime(9999, 12, 30);
            }
        }
        public string time
        {
            get
            {
                var x = "";
                DateTime tt;
                if (!string.IsNullOrEmpty(at_time) && DateTime.TryParse(at_time, out tt))
                {
                    x = tt.ToString("H:mm tt");
                }
                return x;
            }
            set { at_time = value; }
        }
        public string date
        {
            get
            {
                var x = "";
                DateTime tt;
                if (!string.IsNullOrEmpty(at_time) && DateTime.TryParse(at_time, out tt))
                {
                    x = tt.ToString("dd-MM-yyyy");
                }
                return x;
            }
            set { at_time = value; }
        }

        //public DateTime? at_time { get; set; }

        //public string lastActivedDate
        //{
        //    get
        //    {
        //        if (at_time.HasValue)
        //        {
        //            return at_time.Value.ToString("dd-MM-yyyy");
        //        }
        //        else
        //        {
        //            return string.Join("", "Chưa cập nhật thời gian!");
        //        }
        //    }
        //    set
        //    {
        //        if (DateTime.TryParse(value, out DateTime parsedDate))
        //        {
        //            if (at_time.HasValue)
        //            {
        //                at_time = parsedDate.Date.Add(at_time.Value.TimeOfDay);
        //            }
        //            else
        //            {
        //                at_time = parsedDate;
        //            }
        //        }
        //        else
        //        {
        //            throw new ArgumentException("Ngày không hợp lệ!");
        //        }
        //    }
        //}

        //public string lastActivedTime
        //{
        //    get
        //    {
        //        if (at_time.HasValue)
        //        {
        //            return at_time.Value.ToString("HH:mm:ss");
        //        }
        //        else
        //        {
        //            return string.Join("", "Chưa cập nhật thời gian!");
        //        }
        //    }
        //    set
        //    {
        //        if (DateTime.TryParseExact(value, "HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime parsedTime))
        //        {
        //            if (at_time.HasValue)
        //            {
        //                at_time = at_time.Value.Date.Add(parsedTime.TimeOfDay);
        //            }
        //            else
        //            {
        //                at_time = parsedTime;
        //            }
        //        }
        //        else
        //        {
        //            throw new ArgumentException("Ngày không hợp lệ !");
        //        }
        //    }
        //}
        public string device { get; set; }
        public string ts_location_name { get; set; }
        public string wifi_name { get; set; }
        public string wifi_ip { get; set; }
        public string wifi_mac { get; set; }
        public string shift_id { get; set; }
        public int ts_com_id { get; set; }
        public string note { get; set; }
        public string bluetooth_address { get; set; }
        public int status { get; set; }
        public string ts_error { get; set; }
        public int is_success { get; set; }
        public string ep_name { get; set; }
        public int ep_gender { get; set; }
        public string shift_name { get; set; }
    }

    public class Root_LichSuDiemDanh
    {
        public Data_LichSuDiemDanh data { get; set; }
        public object error { get; set; }
    }


}
