using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.Entities
{
    public class API_LichLamViec_List
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class CyDetail
        {
            public string date { get; set; }
            public string shift_id { get; set; }
        }

        public class Data
        {

            public bool? result { get; set; }
            public string message { get; set; }
            public List<Cycle> data { get; set; }

        }
        public class Cycle
        {

            public int? cy_id { get; set; }
            public int? com_id { get; set; }
            public string cy_name { get; set; }
            public DateTime? apply_month { get; set; }
            public string str_apply_month { get; set; }
            public string start_date { get; set; }
            public List<CyDetail> cy_detail { get; set; }
            public string status { get; set; }
            public bool Is_personal { get; set; }
            public string is_personal
            {
                get
                {
                    if (Is_personal) return "Lịch làm việc cá nhân";
                    else return "Lịch làm việc chung";
                }
                set
                {
                    if (value == "0") Is_personal = false;
                    else if (value == "1") Is_personal = true;
                }
            }
            public string ep_count { get; set; }
            public int STT { get; set; }
        }
        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }


    }
}
