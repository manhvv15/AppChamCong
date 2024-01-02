using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat
{
    public class API_Project
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Project
        {
            public string _id { get; set; }
            public int? project_id { get; set; }
            public int? com_id { get; set; }
            public string project_name { get; set; }
            public string project_description { get; set; }
            public string time_in { get; set; }
            public string time_out { get; set; }
            public string date_start { get; set; }
            public string date_end { get; set; }
            public string project_card { get; set; }
            public string project_management { get; set; }
            public string project_member { get; set; }
            public string project_evaluate { get; set; }
            public string project_follow { get; set; }
            public int? type { get; set; }
            public int? project_type { get; set; }
            public object link_congviec { get; set; }
            public object description { get; set; }
            public int? is_delete { get; set; }
            public object deleted_at { get; set; }
            public int? created_at { get; set; }
            public int? created_by { get; set; }
            public object updated_at { get; set; }
            public int? created_id { get; set; }
            public int? open_or_close { get; set; }
            public object is_khancap { get; set; }
            public bool? deleted { get; set; }
            public int? __v { get; set; }
            public string endTime { get; set; }
        }

        public class DuAn
        {
            public List<Project> data { get; set; }
        }


    }
}
