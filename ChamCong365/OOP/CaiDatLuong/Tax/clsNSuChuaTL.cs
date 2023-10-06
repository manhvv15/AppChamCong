using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.CaiDatLuong.Tax
{
    public class clsNSuChuaTL
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Account
        {
            //public int birthday { get; set; }
            public int gender { get; set; }
            public int married { get; set; }
            //public int experience { get; set; }
            //public int education { get; set; }
            public string _id { get; set; }
        }

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
        //    public string cv_muctieu { get; set; }
        //    public List<int> cv_city_id { get; set; }
        //    public List<int> cv_cate_id { get; set; }
        //    public int cv_capbac_id { get; set; }
        //    public int cv_money_id { get; set; }
        //    public int cv_loaihinh_id { get; set; }
        //    public int cv_time { get; set; }
        //    public int cv_time_dl { get; set; }
        //    public string cv_kynang { get; set; }
        //    public int? um_type { get; set; }
        //    public int? um_min_value { get; set; }
        //    public int? um_max_value { get; set; }
        //    public int? um_unit { get; set; }
        //    public string cv_tc_name { get; set; }
        //    public string cv_tc_cv { get; set; }
        //    public string cv_tc_dc { get; set; }
        //    public string cv_tc_phone { get; set; }
        //    public string cv_tc_email { get; set; }
        //    public string cv_tc_company { get; set; }
        //    public string cv_video { get; set; }
        //    public int cv_video_type { get; set; }
        //    public int cv_video_active { get; set; }
        //    public string use_ip { get; set; }
        //    public int time_send_vl { get; set; }
        //    public double percents { get; set; }
        //    public int vip { get; set; }
        //    public int check_create_usc { get; set; }
        //    public int emp_id { get; set; }
        //    public string _id { get; set; }
        //    public List<object> profileDegree { get; set; }
        //    public List<object> profileNgoaiNgu { get; set; }
        //    public List<object> profileExperience { get; set; }
        //    public int? use_badge { get; set; }
        //    public object cv_giai_thuong { get; set; }
        //    public object cv_hoat_dong { get; set; }
        //    public object cv_so_thich { get; set; }
        //}

        public class Department
        {
            public string _id { get; set; }
            public int dep_id { get; set; }
            public int com_id { get; set; }
            public string dep_name { get; set; }
            public DateTime dep_create_time { get; set; }
            public object manager_id { get; set; }
            public int dep_order { get; set; }
        }

        //public class Employee
        //{
        //    public int com_id { get; set; }
        //    public int dep_id { get; set; }
        //    public int start_working_time { get; set; }
        //    public int position_id { get; set; }
        //    public int team_id { get; set; }
        //    public int group_id { get; set; }
        //    public int? time_quit_job { get; set; }
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
            public Account account { get; set; }
            //public Employee employee { get; set; }
            //public Candidate candidate { get; set; }
            public string _id { get; set; }
        }

        public class ListUserFinal
        {
            public int _id { get; set; }
            public string email { get; set; }
            public string phoneTK { get; set; }
            public string userName { get; set; }
            public string alias { get; set; }
            public string phone { get; set; }
            public string emailContact { get; set; }
            public string avatarUser { get; set; }
            public int type { get; set; }
            //public int city { get; set; }
            //public int district { get; set; }
            public string address { get; set; }
            public string otp { get; set; }
            public int authentic { get; set; }
            public int isOnline { get; set; }
            public string fromWeb { get; set; }
            public int fromDevice { get; set; }
            //public int createdAt { get; set; }
            //public int updatedAt { get; set; }
            public DateTime? lastActivedAt { get; set; }
            public int time_login { get; set; }
            public int role { get; set; }
            public string latitude { get; set; }
            public string longtitude { get; set; }
            public int idQLC { get; set; }
            public int idTimViec365 { get; set; }
            public int idRaoNhanh365 { get; set; }
            public string chat365_secret { get; set; }
            public List<object> sharePermissionId { get; set; }
            public InForPerson inForPerson { get; set; }
            public object inForCompany { get; set; }
            public object inforRN365 { get; set; }
            public int scan { get; set; }
            public Department department { get; set; }
            public int? chat365_id { get; set; }
            public int? scan_base365 { get; set; }
            public int? check_chat { get; set; }
        }

        public class Root
        {
            public bool data { get; set; }
            public string message { get; set; }
            public List<ListUserFinal> listUserFinal { get; set; }
        }


    }
}
