using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.funcQuanLyCongTy
{
    internal class ListRecruitmentNew
    {
        public class Data
        {
            public int total { get; set; }
            public List<RecruitmentNew> data { get; set; }

        }
        public class RecruitmentNew
        {
            public string _id { get; set; }
            public int id { get; set; }
            public string title { get; set; }
            public int number { get; set; }
            public int gender { get; set; }
            public int position_apply { get; set; }
            public int cit_id { get; set; }
            public string address { get; set; }
            public int cate_id { get; set; }
            public int salary_id { get; set; }
            public string recruitment_time { get; set; }
            public string recruitment_time_to { get; set; }
            public string job_detail { get; set; }
            public int woking_form { get; set; }
            public string probationary_time { get; set; }
            public int money_tip { get; set; }
            public string job_description { get; set; }
            public string interest { get; set; }
            public int recruitmen_id { get; set; }
            public int job_exp { get; set; }
            public int degree { get; set; }
            public string job_require { get; set; }
            public int member_follow { get; set; }
            public int hr_name { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public object deleted_at { get; set; }
            public int is_delete { get; set; }
            public int com_id { get; set; }
            public int is_com { get; set; }
            public string created_by { get; set; }
            public int is_sample { get; set; }
        }
        public class Root
        {
            public bool result { get; set; }
            public int code { get; set; }
            public Success success { get; set; }
            public object error { get; set; }
        }

        public class Success
        {
            public Data data { get; set; }
            public string message { get; set; }
        }
    }
}
