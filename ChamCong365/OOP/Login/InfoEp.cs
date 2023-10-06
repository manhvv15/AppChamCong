using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.Login
{
    public class InfoEp
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        //public class Account
        //{
        //    public int birthday { get; set; }
        //    public int gender { get; set; }
        //    public int married { get; set; }
        //    public int experience { get; set; }
        //    public int education { get; set; }
        //    public string _id { get; set; }
        //}

        //public class Candidate
        //{
        //    public int use_type { get; set; }
        //    public int user_reset_time { get; set; }
        //    public int use_view { get; set; }
        //    public int use_noti { get; set; }
        //    public int use_show { get; set; }
        //    public int use_show_cv { get; set; }
        //    public int use_active { get; set; }
        //    public int use_td { get; set; }
        //    public int use_check { get; set; }
        //    public int use_test { get; set; }
        //    public int point_time_active { get; set; }
        //    public string cv_title { get; set; }
        //    public object cv_muctieu { get; set; }
        //    public List<object> cv_city_id { get; set; }
        //    public List<object> cv_cate_id { get; set; }
        //    public int cv_capbac_id { get; set; }
        //    public int cv_money_id { get; set; }
        //    public int cv_loaihinh_id { get; set; }
        //    public int cv_time { get; set; }
        //    public int cv_time_dl { get; set; }
        //    public object cv_kynang { get; set; }
        //    public int um_type { get; set; }
        //    public object um_min_value { get; set; }
        //    public object um_max_value { get; set; }
        //    public int um_unit { get; set; }
        //    public object cv_tc_name { get; set; }
        //    public object cv_tc_cv { get; set; }
        //    public object cv_tc_dc { get; set; }
        //    public object cv_tc_phone { get; set; }
        //    public object cv_tc_email { get; set; }
        //    public object cv_tc_company { get; set; }
        //    public object cv_video { get; set; }
        //    public int cv_video_type { get; set; }
        //    public int cv_video_active { get; set; }
        //    public object use_ip { get; set; }
        //    public int time_send_vl { get; set; }
        //    public int percents { get; set; }
        //    public int vip { get; set; }
        //    public int check_create_usc { get; set; }
        //    public int emp_id { get; set; }
        //    public string _id { get; set; }
        //    public List<object> profileDegree { get; set; }
        //    public List<object> profileNgoaiNgu { get; set; }
        //    public List<object> profileExperience { get; set; }
        //}

        public class CompanyName
        {
            public int _id { get; set; }
            public string userName { get; set; }
        }

        public class Data
        {
            public bool result { get; set; }
            public string message { get; set; }
            public Data data { get; set; }
            public int _id { get; set; }
            public InForPerson inForPerson { get; set; }
            public string userName { get; set; }
            public int dep_id { get; set; }
            public int com_id { get; set; }
            public int position_id { get; set; }
            public int start_working_time { get; set; }
            public int idQLC { get; set; }
            public string phoneTK { get; set; }
            public string phone { get; set; }
            public string address { get; set; }
            public string avatarUser { get; set; }
            public int authentic { get; set; }
            public int birthday { get; set; }
            public int gender { get; set; }
            public int married { get; set; }
            public int experience { get; set; }
            public int education { get; set; }
            public string emailContact { get; set; }
            public string nameDeparment { get; set; }
            public CompanyName companyName { get; set; }
        }

        //public class Employee
        //{
        //    public int com_id { get; set; }
        //    public int dep_id { get; set; }
        //    public int start_working_time { get; set; }
        //    public int position_id { get; set; }
        //    public int team_id { get; set; }
        //    public int group_id { get; set; }
        //    public int time_quit_job { get; set; }
        //    public string ep_description { get; set; }
        //    public string ep_featured_recognition { get; set; }
        //    public string ep_status { get; set; }
        //    public int ep_signature { get; set; }
        //    public int allow_update_face { get; set; }
        //    public int version_in_use { get; set; }
        //    public string _id { get; set; }
        //}

        public class InForPerson
        {
            public int scan { get; set; }
            //public Account account { get; set; }
            //public Employee employee { get; set; }
            //public Candidate candidate { get; set; }
            public string _id { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }


    }
}
