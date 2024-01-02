﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien
{
    public class API_SettingConfirm
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class ListPrivateLevel
        {
            public int dexuat_id { get; set; }
            public int confirm_level { get; set; }
        }

        public class ListPrivateTime
        {
            public int dexuat_id { get; set; }
            public int confirm_time { get; set; }
        }

        public class ListPrivateType
        {
            public int dexuat_id { get; set; }
            public int confirm_type { get; set; }
        }

        public class Data_Confirm
        {
            public SettingConfirm settingConfirm { get; set; }
        }

        public class SettingConfirm
        {
            public string _id { get; set; }
            public int id { get; set; }
            public int comId { get; set; }
            public int ep_id { get; set; }
            public int confirm_level { get; set; }
            public int confirm_type { get; set; }
            public int created_time { get; set; }
            public int update_time { get; set; }
            public List<ListPrivateLevel> listPrivateLevel { get; set; }
            public List<ListPrivateType> listPrivateType { get; set; }
            public List<ListPrivateTime> listPrivateTime { get; set; }
        }


    }
}
