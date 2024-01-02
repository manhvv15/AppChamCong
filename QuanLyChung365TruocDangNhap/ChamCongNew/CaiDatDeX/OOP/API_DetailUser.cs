using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.CaiDatDeX.OOP
{
    public class API_DetailUser
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data_DetailUser
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<Detail> data { get; set; }
            
        }
        public class Detail
        {
            public string _id { get; set; }
            public int? id { get; set; }
            public int? comId { get; set; }
            public int? dexuat_id { get; set; }
            public string dexuat_name { get; set; }
            public string ht_duyet { get; set; }
            public int? confirm_level { get; set; }
            public int? confirm_type { get; set; }
            public int? confirm_time { get; set; }
            public int? created_time { get; set; }
            public int? update_time { get; set; }
            public string confirmTypeName { get; set; }
        }
        public class DetailUser
        {
            public Data_DetailUser data { get; set; }
            public object error { get; set; }
        }


    }
}
