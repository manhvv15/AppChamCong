using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatDiMuonVeSom
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Addition_DMVS
    {
        public int sheet_id { get; set; }
        public int ep_id { get; set; }
        public string ts_image { get; set; }
        public DateTime ts_date { get; set; }
        public DateTime check_in { get; set; }
        public DateTime check_out { get; set; }
        public object ts_location_name { get; set; }
        public int shift_id { get; set; }
        public string note { get; set; }
        public string ts_error { get; set; }
        public string status { get; set; }
        public int is_success { get; set; }
        public string ep_name { get; set; }
        public int ep_gender { get; set; }
        public double late { get; set; }
        public int late_second { get; set; }
        public double early { get; set; }
        public int early_second { get; set; }
        public string late_second_string { get; set; }
        public string early_second_string { get; set; }
    }

    public class Data_DMVS
    {
        public int sheet_id { get; set; }
        public DateTime date { get; set; }
        public string date_format { get; set; }
        public int shift_id { get; set; }
        public int pm_type_phat { get; set; }
        public double cong { get; set; }
        public Addition_DMVS addition { get; set; }
        public int ep_id { get; set; }
        public User user { get; set; }
        public Shift shift { get; set; }
        public string shift_name { get; set; }
        public string organizeDetailName { get; set; }
        public string avatarUser { get; set; }
        public string pm_monney_formatted { get; set; }
        public string ep_idCom { get; set; }
        public PmInfo pm_info { get; set; }
    }

    public class PmInfo
    {
        public string _id { get; set; }
        public int pm_id { get; set; }
        public int pm_id_com { get; set; }
        public int pm_shift { get; set; }
        public int pm_type { get; set; }
        public int pm_minute { get; set; }
        public int pm_type_phat { get; set; }
        public DateTime pm_time_begin { get; set; }
        public DateTime pm_time_end { get; set; }
        public int pm_monney { get; set; }
        public string pm_monney_formatted
        {
            get { return pm_monney.ToString("N0"); }
            set
            {
                if (int.TryParse(value.Replace(",", ""), out int parsedLuong))
                {
                    pm_monney = parsedLuong;
                }
            }
        }
        public DateTime pm_time_created { get; set; }
    }

    public class Root_DMVS
    {
        public List<Data_DMVS> data { get; set; }
        public string message { get; set; }
    }

    public class Shift
    {
        public int shift_id { get; set; }
        public string shift_name { get; set; }
    }

    public class User
    {
        public int idQLC { get; set; }
        public string userName { get; set; }
        public string organizeDetailName { get; set; }
        public string positionName { get; set; }
        public string avatarUser { get; set; }
    }


}
