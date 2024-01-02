using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec
{
    public class EmployeeNotInCycleEntites
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool result { get; set; }
            public string message { get; set; }
            public int totalItems { get; set; }
            public List<Item> items { get; set; }
        }

        public class Item
        {
            public int _id { get; set; }
            public int ep_id { get; set; }
            public string ep_name { get; set; }
            public string dep_name { get; set; }
            public bool ischecked { get; set; }
            public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
        }

        public class ListOrganizeDetailId
        {
            public int level { get; set; }
            public int organizeDetailId { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }

    }
}
