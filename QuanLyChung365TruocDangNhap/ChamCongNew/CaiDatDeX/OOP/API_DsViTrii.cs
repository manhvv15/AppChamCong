using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.CaiDatDeX.OOP
{
    public class API_DsViTrii
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data_DsViTri
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<ViTri> data { get; set; }

        }
        public class ViTri
        {
            public string _id { get; set; }
            public int? id { get; set; }
            public int? comId { get; set; }
            public string positionName { get; set; }
            public int? level { get; set; }
            public int? isManager { get; set; }
            public int? created_time { get; set; }
            public int? update_time { get; set; }
        }
        public class DsViTri
        {
            public Data_DsViTri data { get; set; }
            public object error { get; set; }
        }


    }
}
