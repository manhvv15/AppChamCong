using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities
{
    public class ListUserEntities
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public int? total { get; set; }
            public List<UserData> data { get; set; }

        }
        public class UserData
        {
            public string phone { get; set; }
            public string avatarUser { get; set; }
            public int? ep_id { get; set; }
            public string userName { get; set; }
            public string organizeDetailName { get; set; }
            public string positionName { get; set; }
            public int? position_id { get; set; }
            public int? gender { get; set; }
            public string address { get; set; }
            public double? start_working_time { get; set; }
            public string email { get; set; }
            public int? organizeDetailId { get; set; }
            public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
            public bool isCheck { get; set; }
        }

        public class ListOrganizeDetailId
        {
            public int? level { get; set; }
            public int? organizeDetailId { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }


    }
}
