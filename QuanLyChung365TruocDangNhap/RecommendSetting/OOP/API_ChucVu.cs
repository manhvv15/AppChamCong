using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.RecommendSetting.OOP
{
    public class API_ChucVu
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data_ChucVu
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<ChucVu_Infor> data { get; set; }

        }
        public class ChucVu_Infor
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
        public class ChucVu
        {
            public Data_ChucVu data { get; set; }
            public object error { get; set; }
        }


    }
}
