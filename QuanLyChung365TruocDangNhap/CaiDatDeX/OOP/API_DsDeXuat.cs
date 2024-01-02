using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuanLyChung365TruocDangNhap.CaiDatDeX.OOP
{
    public class API_DsDeXuat
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data_DeXuat
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public int? total { get; set; }
            public List<DsDeXuat> data { get; set; }
            
        }
        public class DsDeXuat
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
        }
        public class DeXuat
        {
            public Data_DeXuat data { get; set; }
            public object error { get; set; }
        }


    }
}
