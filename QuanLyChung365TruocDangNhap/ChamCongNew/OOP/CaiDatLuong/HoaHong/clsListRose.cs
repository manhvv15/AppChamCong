using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.HoaHong
{
    
    public class ListThietLap
    {
        public int stt { get; set; }
        public string _id { get; set; }
        public int tl_id { get; set; }
        public int tl_id_com { get; set; }
        public int tl_id_rose { get; set; }
        public string tl_name { get; set; }
        public long tl_money_min { get; set; }
        public object tl_money_max { get; set; }
        public TlPhanTram tl_phan_tram { get; set; }
        public int tl_chiphi { get; set; }
        public int tl_hoahong { get; set; }
        public int tl_kpi_yes { get; set; }
        public int tl_kpi_no { get; set; }
        public string tl_time_created { get; set; }
    }

    public class Root_HoaHong
    {
        public bool data { get; set; }
        public string message { get; set; }
        public List<ListThietLap> listThietLap { get; set; }
    }

    public class TlPhanTram
    {
        [JsonProperty("$numberDecimal")]
        public string numberDecimal { get; set; }
    }


}
