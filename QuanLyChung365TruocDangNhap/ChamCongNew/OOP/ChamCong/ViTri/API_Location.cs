using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.ViTri
{
    public class API_Location
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<Location> list { get; set; }
            public int? total { get; set; }
        }

        public class Location
        {
            public int STT { get; set; }
            public int? cor_id { get; set; }
            public string cor_location_name { get; set; }
            public double? cor_lat { get; set; }
            public double? cor_long { get; set; }
            public double? cor_radius { get; set; }
            public int? id_com { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }

    }
}
