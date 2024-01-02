﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CapNhatKhuonMat
{
    public class API_ListAll
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Content
        {
            public string key { get; set; }
            public string value { get; set; }
        }

        public class Data_ListUpdateNV
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<NV> data { get; set; }
            public int? count { get; set; }

        }
        public class NV
        {

            public int? _id { get; set; }
            public string userName { get; set; }
            public int? dep_id { get; set; }
            public int? com_id { get; set; }
            public int? allow_update_face { get; set; }
            public int? position_id { get; set; }
            public string positionName { get; set; }
            public string phoneTK { get; set; }
            public string avatarUser { get; set; }
            public string email { get; set; }
            public int? idQLC { get; set; }
            public Detail detail { get; set; }
            public bool isCheck { get; set; } = false;
        }
        public class Detail
        {
            public string _id { get; set; }
            public int? id { get; set; }
            public int? comId { get; set; }
            public int? parentId { get; set; }
            public string organizeDetailName { get; set; }
            public int? level { get; set; }
            public int? range { get; set; }
            public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
            public int? created_time { get; set; }
            public int? update_time { get; set; }
        }

        public class ListOrganizeDetailId
        {
            public int? level { get; set; }
            public int? organizeDetailId { get; set; }
        }

        public class ListUpdateNV
        {
            public Data_ListUpdateNV data { get; set; }
            public object error { get; set; }
        }


    }
}