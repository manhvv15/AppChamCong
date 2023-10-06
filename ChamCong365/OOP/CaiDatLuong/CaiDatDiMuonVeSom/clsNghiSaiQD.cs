using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.CaiDatLuong.CaiDatDiMuonVeSom
{
    public class clsNghiSaiQD
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Account
        {
            public int? birthday { get; set; }
            public int gender { get; set; }
            public int married { get; set; }
            public int experience { get; set; }
            public int education { get; set; }
            public string _id { get; set; }
        }

        public class Candidate
        {
            public int use_type { get; set; }
            public int user_reset_time { get; set; }
            public int use_view { get; set; }
            public int use_noti { get; set; }
            public int use_show { get; set; }
            public int use_show_cv { get; set; }
            public int use_active { get; set; }
            public int use_td { get; set; }
            public int use_check { get; set; }
            public int use_test { get; set; }
            public int use_badge { get; set; }
            public int point_time_active { get; set; }
            public string cv_title { get; set; }
            public string cv_muctieu { get; set; }
            public string cv_kynang { get; set; }
            public object cv_giai_thuong { get; set; }
            public object cv_hoat_dong { get; set; }
            public object cv_so_thich { get; set; }
            public List<int> cv_city_id { get; set; }
            public List<int> cv_cate_id { get; set; }
            public int cv_capbac_id { get; set; }
            public int cv_money_id { get; set; }
            public int cv_loaihinh_id { get; set; }
            public int cv_time { get; set; }
            public int cv_time_dl { get; set; }
            public int? um_type { get; set; }
            public int? um_min_value { get; set; }
            public int? um_max_value { get; set; }
            public int? um_unit { get; set; }
            public string cv_tc_name { get; set; }
            public string cv_tc_cv { get; set; }
            public string cv_tc_dc { get; set; }
            public string cv_tc_phone { get; set; }
            public string cv_tc_email { get; set; }
            public string cv_tc_company { get; set; }
            public string cv_video { get; set; }
            public int cv_video_type { get; set; }
            public int cv_video_active { get; set; }
            public string use_ip { get; set; }
            public double percents { get; set; }
            public int vip { get; set; }
            public int check_create_usc { get; set; }
            public int emp_id { get; set; }
            public string _id { get; set; }
            public List<object> profileDegree { get; set; }
            public List<object> profileNgoaiNgu { get; set; }
            public List<object> profileExperience { get; set; }
            public int? time_send_vl { get; set; }
        }

        public class ConfigChat
        {
            public int notificationAcceptOffer { get; set; }
            public int notificationAllocationRecall { get; set; }
            public int notificationChangeSalary { get; set; }
            public int notificationCommentFromRaoNhanh { get; set; }
            public int notificationCommentFromTimViec { get; set; }
            public int notificationDecilineOffer { get; set; }
            public int notificationMissMessage { get; set; }
            public int notificationNTDExpiredPin { get; set; }
            public int notificationNTDExpiredRecruit { get; set; }
            public int notificationNTDPoint { get; set; }
            public int notificationSendCandidate { get; set; }
            public int notificationTag { get; set; }
            public List<object> removeSugges { get; set; }
            public string userNameNoVn { get; set; }
            public int doubleVerify { get; set; }
            public int active { get; set; }
            public string status { get; set; }
            public int acceptMessStranger { get; set; }
            public List<HistoryAccess> HistoryAccess { get; set; }
            public object pinHiddenConversation { get; set; }
            public List<object> pinInputTimes { get; set; }
            public string description { get; set; }
            public string nickName { get; set; }
        }

        public class DataKoCcFinal
        {
            public int ep_id { get; set; }
            public int shift_id { get; set; }
            public DateTime time { get; set; }
            public int count { get; set; }
            public Money money { get; set; }
            public List<DataPhatNghiKoPhep> data_phat_nghi_ko_phep { get; set; }
            public Detail detail { get; set; }
        }

        public class DataPhatNghiKoPhep
        {
            public string _id { get; set; }
            public int pc_money { get; set; }
            public int pc_shift { get; set; }
        }

        public class Datum
        {
            public int ep_id { get; set; }
            public string ep_name { get; set; }
            public string ep_avatar { get; set; }
            public string ep_dep_id { get; set; }
            public string ep_dep_name { get; set; }
            public string time_ad { get; set; }
            public string shift_id { get; set; }
            public string shift_name { get; set; }
            public string money { get; set; }
            public List<DataKoCcFinal> data_ko_cc_final { get; set; }
            public List<object> data_ko_cc_co_phep { get; set; }
        }

        public class Detail
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
            public int? city { get; set; }
            public int? district { get; set; }
            public string address { get; set; }
            public string otp { get; set; }
            public int authentic { get; set; }
            public int isOnline { get; set; }
            public string fromWeb { get; set; }
            public int fromDevice { get; set; }
            public int createdAt { get; set; }
            public int updatedAt { get; set; }
            public DateTime? lastActivedAt { get; set; }
            public int time_login { get; set; }
            public int role { get; set; }
            public string latitude { get; set; }
            public string longtitude { get; set; }
            public int idQLC { get; set; }
            public int idTimViec365 { get; set; }
            public int idRaoNhanh365 { get; set; }
            public string chat365_secret { get; set; }
            public int chat365_id { get; set; }
            public int scan_base365 { get; set; }
            public int check_chat { get; set; }
            public List<object> sharePermissionId { get; set; }
            public InForPerson inForPerson { get; set; }
            public object inForCompany { get; set; }
            public object inforRN365 { get; set; }
            public ConfigChat configChat { get; set; }
            public int scan { get; set; }
        }

        public class Employee
        {
            public int com_id { get; set; }
            public int dep_id { get; set; }
            //public int start_working_time { get; set; }
            //public int position_id { get; set; }
            //public int team_id { get; set; }
            //public int group_id { get; set; }
            //public int? time_quit_job { get; set; }
            //public string ep_description { get; set; }
            //public string ep_featured_recognition { get; set; }
            //public string ep_status { get; set; }
            //public int ep_signature { get; set; }
            //public int allow_update_face { get; set; }
            //public int version_in_use { get; set; }
            //public string _id { get; set; }
        }

        public class HistoryAccess
        {
            public string IdDevice { get; set; }
            public string IpAddress { get; set; }
            public string NameDevice { get; set; }
            public DateTime Time { get; set; }
            public bool AccessPermision { get; set; }
            public string _id { get; set; }
        }

        public class InForPerson
        {
            public int scan { get; set; }
            public Account account { get; set; }
            public Employee employee { get; set; }
            public Candidate candidate { get; set; }
            public string _id { get; set; }
        }

        public class Money
        {
            public string _id { get; set; }
            public int pc_money { get; set; }
            public int pc_shift { get; set; }
        }

        public class Root
        {
            public List<Datum> data { get; set; }
            public string message { get; set; }
        }


    }
}
