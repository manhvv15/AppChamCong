using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CamXuc
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data_Togg
    {
        public bool result { get; set; }
        public string message { get; set; }
        public Data_Togg data { get; set; }
        public int _id { get; set; }
        public bool emotion_active { get; set; }
    }

    public class Root_Togg
    {
        public Data_Togg data { get; set; }
        public object error { get; set; }
    }

}
