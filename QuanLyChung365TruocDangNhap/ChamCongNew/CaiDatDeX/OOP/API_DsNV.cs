using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.API_NVV;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.CaiDatDeX.OOP
{
    public class API_DsNV
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data_Ds_NV
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public int? total { get; set; }
            public List<NV> data { get; set; }

        }
        public class NV
        {
            public bool isChecked { get; set; }
            public string phone { get; set; }
            public object avatarUser { get; set; }
            public int? ep_id { get; set; }
            public string userName { get; set; }
            public string organizeDetailName { get; set; }
            public string positionName { get; set; }
            public List<ListPrivateLevel> listPrivateLevel { get; set; }
            public List<ListPrivateType> listPrivateType { get; set; }
            public List<ListPrivateTime> listPrivateTime { get; set; }
        }
        public class ListPrivateLevel
        {
            public int? dexuat_id { get; set; }
            public int? confirm_level { get; set; }
        }

        public class ListPrivateTime
        {
            public int? dexuat_id { get; set; }
            public int? confirm_time { get; set; }
        }

        public class ListPrivateType
        {
            public int? dexuat_id { get; set; }
            public int? confirm_type { get; set; }
        }

        public class Ds_NV
        {
            public Data_Ds_NV data { get; set; }
            public object error { get; set; }
        }


    }
}
