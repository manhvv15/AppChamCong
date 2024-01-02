
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat
{
    public class API_CongTac
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Content
        {
            public string key { get; set; }
        }

        public class Data_CongTac
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<NoiCongTac> data { get; set; }
            
        }
        public class NoiCongTac
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

        public class CongTac
        {
            public Data_CongTac data { get; set; }
            public object error { get; set; }
        }


    }
}
