using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ThietLapCongTy.Entities
{
    public class API_Tree_SoDoToChuc
    {
        public class Child
        {
            public int? id { get; set; }
            public int? comId { get; set; }
            public int? parentId { get; set; }
            public int? level { get; set; }
            public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
            public List<Content> content { get; set; }
            public string name { get; set; }
            public string comName { get; set; }
            public int? key { get; set; }
            public int? ep_num { get; set; }
            public int? nv_di_lam { get; set; }
            public int? nv_nghi { get; set; }
            public string manager { get; set; }
            public int? managerId { get; set; }
            public string type { get; set; }
            public string typeBottom { get; set; }
            public List<Child> Children { get; set; }
            public List<Child> children
            {
                get { return Children; }
                set
                {
                    Children = value;
                    setLineType();
                }
            }
            public void setLineType()
            {
                if (Children.Count > 0)
                {
                    typeBottom = "LineBottom";
                    Children[0].type = "Head";
                    Children[children.Count - 1].type = "Tail";
                    if (Children.Count == 1) Children[0].type = "Center";
                }


            }
        }

        public class Content
        {
            public string key { get; set; }
            public object value { get; set; }
        }
        public class Data
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public Child data { get; set; }

        }
        public class Detail
        {
            public string name { get; set; }
            public List<Child> children { get; set; }
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
