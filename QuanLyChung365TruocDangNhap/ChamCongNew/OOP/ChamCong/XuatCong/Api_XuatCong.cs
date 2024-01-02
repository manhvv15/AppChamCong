﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.XuatCong
{
    public class Data_BangCong
    {
        public bool result { get; set; }
        public string message { get; set; }
        public int totalItems { get; set; }
        public List<Item_BangCong> items { get; set; }
    }

    public class Item_BangCong
    {
        public int com_id { get; set; }
        public int com_parent_id { get; set; }
        public string com_name { get; set; }
        public string com_email { get; set; }
        public object com_phone_tk { get; set; }
        public string id_way_timekeeping { get; set; }
        public string com_phone { get; set; }
        public string com_logo { get; set; }
        public string com_address { get; set; }
        public int com_create_time { get; set; }
    }

    public class Root_BangCong
    {
        public Data_BangCong data { get; set; }
        public object error { get; set; }
    }

}
