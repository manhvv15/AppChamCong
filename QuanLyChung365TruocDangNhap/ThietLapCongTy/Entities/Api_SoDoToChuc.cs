using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ThietLapCongTy.Entities
{
    public class Child_SoDoToChuc
    {
        public int id { get; set; }
        public int comId { get; set; }
        public int parentId { get; set; }
        public int level { get; set; }
        public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
        public List<Content> content { get; set; }
        public string name { get; set; }
        public string comName { get; set; }
        public int key { get; set; }
        public int ep_num { get; set; }
        public string manager { get; set; }
        public int managerId { get; set; }
        public List<Child_SoDoToChuc> children { get; set; }
    }

    public class Content
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class Data_SoDoToChuc
    {
        public bool result { get; set; }
        public string message { get; set; }
        public Data_SoDoToChuc data { get; set; }
        public string name { get; set; }
        public List<Child_SoDoToChuc> children { get; set; }
    }

    public class ListOrganizeDetailId
    {
        public int level { get; set; }
        public int organizeDetailId { get; set; }
    }

    public class Root_SoDoToChuc
    {
        public Data_SoDoToChuc data { get; set; }
        public object error { get; set; }
    }
}
