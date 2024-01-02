using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi
{
    public class MucDoanhThuEntities
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Danhthu
        {
            public string _id { get; set; }
            public int? tl_id { get; set; }
            public int? tl_id_com { get; set; }
            public int? tl_id_rose { get; set; }
            public string tl_name { get; set; }
            public int? tl_money_min { get; set; }
            public object tl_money_max { get; set; }
            public TlPhanTram tl_phan_tram { get; set; }
            public int? tl_chiphi { get; set; }
            public int? tl_hoahong { get; set; }
            public int? tl_kpi_yes { get; set; }
            public int? tl_kpi_no { get; set; }
            public string tl_time_created { get; set; }
        }

        public class Data
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<Danhthu> danhthuList { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }

        public class TlPhanTram
        {
            [JsonProperty("$numberDecimal")]
            public string numberDecimal { get; set; }
        }
    }
}
