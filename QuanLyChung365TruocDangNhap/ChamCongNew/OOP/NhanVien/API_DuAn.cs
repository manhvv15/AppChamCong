using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien
{
    public class API_DuAn
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data_Du_An
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<User> user { get; set; }
        }

        public class ListOrganizeDetailId
        {
            public int? level { get; set; }
            public int? organizeDetailId { get; set; }
        }

        public class Du_An
        {
            public Data_Du_An data { get; set; }
            public object error { get; set; }
        }

        public class User
        {
            public int? _id { get; set; }
            public int? ep_id { get; set; }
            public object ep_email { get; set; }
            public string ep_phone { get; set; }
            public string ep_name { get; set; }
            public object ep_image { get; set; }
            public int? role_id { get; set; }
            public int? position_id { get; set; }
            public string positionName { get; set; }
            public int? com_id { get; set; }
            public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
            public int? organizeDetailId { get; set; }
            public string organizeDetailName { get; set; }
        }


    }
}
