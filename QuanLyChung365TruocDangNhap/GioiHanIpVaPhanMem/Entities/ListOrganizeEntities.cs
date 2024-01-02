using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities
{
    public class ListOrganizeEntities
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Content
        {
            public string key { get; set; }
            public object value { get; set; }
            public int type { get; set; }
            public int image { get; set; }
            public int select { get; set; } = 0;
            public string imageName { get; set; } = "";
        }

        public class Data
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<OrganizeData> data { get; set; }

        }
        public class OrganizeData
        {
            public int? id { get; set; }
            public int? comId { get; set; }
            public int? parentId { get; set; }
            public string organizeDetailName { get; set; }
            public int? level { get; set; }
            public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
            public List<Content> content { get; set; }
            public string comName { get; set; }
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
