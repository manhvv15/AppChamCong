using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CapNhatKhuonMat
{
    public class API_PhongBanMoi
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Content
        {
            public string key { get; set; }
            public string value { get; set; }
        }

        public class Data_PhongBanMoi
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<PhongBan> data { get; set; }


        }
        public class PhongBan
        {
            public int? id { get; set; }
            public int? comId { get; set; }
            public int? parentId { get; set; }
            public string organizeDetailName { get; set; }
            public int? level { get; set; }
            public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }

            public string comName { get; set; }
        }
        public class ListOrganizeDetailId
        {
            public int? level { get; set; }
            public int? organizeDetailId { get; set; }
        }

        public class PhongBanMoi
        {
            public Data_PhongBanMoi data { get; set; }
            public object error { get; set; }
        }


    }
}
