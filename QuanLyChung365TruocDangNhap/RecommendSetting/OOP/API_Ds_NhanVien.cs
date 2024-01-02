using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.RecommendSetting.OOP
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class API_NV
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Ds_NhanVien
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public int? total { get; set; }
            public List<NV_Infor> data { get; set; }


        }
        public class NV_Infor
        {
            public string phone { get; set; }
            public object avatarUser { get; set; }
            public int? ep_id { get; set; }
            public string userName { get; set; }
            public string ep_status { get; set; }
            public string organizeDetailName { get; set; }
            public string positionName { get; set; }
            public string confirm_level { get; set; }
            public int? confirm_type { get; set; }
            public List<ListNVId> listNVId { get; set; }
        }
        public class ListNVId
        {
            public int? level { get; set; }
            public int? NVId { get; set; }
        }

        public class API_Ds_NhanVien
        {
            public Ds_NhanVien data { get; set; }
            public object error { get; set; }
        }



    }

}
