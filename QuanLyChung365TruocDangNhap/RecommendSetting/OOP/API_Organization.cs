using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.RecommendSetting.OOP
{
    public class API_Organization
    {// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Content
        {
            public string key { get; set; }
            public object value { get; set; }
        }

        public class Data_Organization
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<Organization_Infor> data { get; set; }

        }
        public class Organization_Infor
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

        public class Organization
        {
            public Data_Organization data { get; set; }
            public object error { get; set; }
        }


    }
}
