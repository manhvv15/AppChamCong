using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.PhanQuyenHeThong.OOP
{
    public class API_DsToChucc
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Content
        {
            public string key { get; set; }
            public object value { get; set; }
            public int? image { get; set; }
        }

        public class Data_DsToChuc
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<ToChuc> data { get; set; }

        }
        public class ToChuc
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

        public class DsToChuc
        {
            public Data_DsToChuc data { get; set; }
            public object error { get; set; }
        }


    }
}
