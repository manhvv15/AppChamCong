using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CapNhatKhuonMat
{
    public class API_Ten
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data_Name
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<List> list { get; set; }
            public int? count { get; set; }
        }

        public class List
        {
            public string userName { get; set; }
            public int? idQLC { get; set; }
            public string ep_status { get; set; }
            public string IDnName
            {
                get
                {
                    if (idQLC == 0) return userName;
                    return idQLC.ToString() + " - " + userName;
                }
            }
        }

        public class Name
        {
            public Data_Name data { get; set; }
            public object error { get; set; }
        }


    }
}
