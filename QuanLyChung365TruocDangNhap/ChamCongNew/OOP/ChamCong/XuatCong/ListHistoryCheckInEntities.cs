using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.XuatCong
{
    public class ListHistoryCheckInEntities
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<HistoryCheckIn> data { get; set; }
            public int? total { get; set; }

        }
        public class HistoryCheckIn
        {

            public string _id { get; set; }
            public int? sheet_id { get; set; }
            public int? ep_id { get; set; }
            public DateTime? at_time { get; set; }
            public string device { get; set; }
            public string ts_location_name { get; set; }
            public int? shift_id { get; set; }
            public string userName { get; set; }
            public string shift_name { get; set; }
            public string image { get; set; }
        }
        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }

    }
}
