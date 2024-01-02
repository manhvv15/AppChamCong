using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi
{
    public class API_QR
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<Item> data { get; set; }

        }
        public class Item
        {
            public int stt { get; set; }
            public string _id { get; set; }
            public int? id { get; set; }
            public int? com_id { get; set; }
            public List<int?> listUsers { get; set; }
            public List<int?> list_device { get; set; }
            public List<string> list_NameDevice { get; set; } = new List<string>();
            public int? QRCode_id { get; set; }
            public DateTime? start_time { get; set; }
            public string start_time_string
            {
                get
                {
                    if (start_time == null) return "Chưa cập nhật";
                    return start_time?.ToLocalTime().ToString("dd-MM-yyyy");
                }
            }
            public DateTime? end_time { get; set; }
            public string end_time_string
            {
                get
                {
                    if (end_time == null) return "Chưa cập nhật";
                    return end_time?.ToLocalTime().ToString("dd-MM-yyyy");
                }
            }
            public int? location_id { get; set; }
            public string cor_location_name { get; set; }
            public int? cor_radius { get; set; }
            public List<object> list_ip { get; set; }
            public List<object> list_name_wifi { get; set; }
            public List<string> QRCodeUrl { get; set; }
            public List<int?> QRstatus { get; set; }
            public List<string> QRCodeName { get; set; }
            public List<object> list_userName { get; set; }
            public List<object> list_positionName { get; set; }
            public List<object> list_organizeDetailName { get; set; }
            public List<object> list_shiftName { get; set; }
            public int? ep_num { get; set; }
            public int? org_num { get; set; }
            public int? pos_num { get; set; }
            public int? shift_num { get; set; }
            public List<int?> list_org { get; set; }
            public List<int?> list_pos { get; set; }
            public List<ListShift> list_shifts { get; set; }
            public string name { get; set; }
            public List<ListLoc> list_loc { get; set; }
            public int? type_ip { get; set; }
            public int? type_loc { get; set; }

        }
        public class ListLoc
        {
            public string _id { get; set; }
            public int? cor_id { get; set; }
            public string cor_location_name { get; set; }
            public double? cor_lat { get; set; }
            public double? cor_long { get; set; }
            public int? cor_radius { get; set; }
            public int? id_com { get; set; }
        }
        public class ListShift
        {
            public int? id { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }

    }
}
