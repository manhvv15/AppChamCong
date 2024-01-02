using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi
{
    public class EmployeeInfo
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Account
        {
            public int? birthday { get; set; }
            public int? gender { get; set; }
            public int? married { get; set; }
            public int? experience { get; set; }
            public int? education { get; set; }
            public string _id { get; set; }
        }

        public class CompanyName
        {
            public int? _id { get; set; }
            public string userName { get; set; }
        }

        public class Data
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public Data data { get; set; }
            public int? _id { get; set; }
            public InForPerson inForPerson { get; set; }
            public string userName { get; set; }
            public int? dep_id { get; set; }
            public int? com_id { get; set; }
            public int? position_id { get; set; }
            public double? start_working_time { get; set; }
            public int? idQLC { get; set; }
            public string phoneTK { get; set; }
            public string phone { get; set; }
            public string address { get; set; }
            public string avatarUser { get; set; }
            public int? authentic { get; set; }
            public int? birthday { get; set; }
            public int? gender { get; set; }
            public int? married { get; set; }
            public int? experience { get; set; }
            public int? education { get; set; }
            public object emailContact { get; set; }
            public string nameDeparment { get; set; }
            public string positionName { get; set; }
            public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
            public CompanyName companyName { get; set; }
        }

        public class Employee
        {
            public int? com_id { get; set; }
            public int? organizeDetailId { get; set; }
            public int? dep_id { get; set; }
            public double? start_working_time { get; set; }
            public int? position_id { get; set; }
            public int? team_id { get; set; }
            public int? group_id { get; set; }
            public int? time_quit_job { get; set; }
            public object ep_description { get; set; }
            public object ep_featured_recognition { get; set; }
            public string ep_status { get; set; }
            public int? ep_signature { get; set; }
            public int? allow_update_face { get; set; }
            public int? version_in_use { get; set; }
            public int? role_id { get; set; }
            public string _id { get; set; }
            public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
        }

        public class InForPerson
        {
            public int? scan { get; set; }
            public Account account { get; set; }
            public Employee employee { get; set; }
            public object candidate { get; set; }
            public string _id { get; set; }
        }

        public class ListOrganizeDetailId
        {
            public int? level { get; set; }
            public int? organizeDetailId { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }


    }
}
