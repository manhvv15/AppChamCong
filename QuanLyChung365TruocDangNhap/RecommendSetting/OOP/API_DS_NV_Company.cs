using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD;

namespace QuanLyChung365TruocDangNhap.RecommendSetting.OOP
{
    public class API_DS_NV_Company
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data_DS_NV_Compay
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public int? total { get; set; }
            public List<NV_Company_Infor> data { get; set; }

        }
        public class NV_Company_Infor
        {
            public int STT { get; set; }
            public int? _id { get; set; }
            public string phone { get; set; }
            public string avatarUser { get; set; }
            public int? ep_id { get; set; }
            public string userName { get; set; }
            public string organizeDetailName { get; set; }
            public string positionName { get; set; }
            public int? position_id { get; set; }
            public int? organizeDetailId { get; set; }
            public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
            public int? gender { get; set; }
            public int? married { get; set; }
            public string address { get; set; }
            public double? start_working_time { get; set; }
            public object email { get; set; }
        }
        public class ListOrganizeDetailId
        {
            public int? level { get; set; }
            public int? organizeDetailId { get; set; }
        }

        public class DS_NV_Compay
        {
            public Data_DS_NV_Compay data { get; set; }
            public object error { get; set; }
        }


    }
}
