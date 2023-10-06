using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong.XuatCong
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
        public List<object> HistoryAccess { get; set; }
    }

    public class Data_Timkeeping
    {
        public bool result { get; set; }
        public string message { get; set; }
        public List<DataTimeSheet> dataTimeSheet { get; set; }
        public List<ListUser> listUser { get; set; }

        public List<ExcelDatas> excelDatas { get; set; }
    }

    public class ExcelDatas
    {
        public string avatarUserEx { get; set; }
        public int idQLCEx { get; set; }
        public string userNameEx { get; set; }

        public string lastActivedAtEx { get; set; }

        public DateTime LastActivedAt
        {
            get
            {
                DateTime aa;
                if (!string.IsNullOrEmpty(lastActivedAtEx) && DateTime.TryParse(lastActivedAtEx, out aa)) return aa;
                return new DateTime(9999, 12, 30);
            }
        }
        public string lastActivedTime
        {
            get
            {
                var x = "";
                DateTime tt;
                if (!string.IsNullOrEmpty(lastActivedAtEx) && DateTime.TryParse(lastActivedAtEx, out tt))
                {
                    x = tt.ToString("H:mm tt");
                }
                return x;
            }
            set { lastActivedAtEx = value; }
        }
        public string lastActivedDate
        {
            get
            {
                var x = "";
                DateTime tt;
                if (!string.IsNullOrEmpty(lastActivedAtEx) && DateTime.TryParse(lastActivedAtEx, out tt))
                {
                    x = tt.ToString("dd-MM-yyyy");
                }
                return x;
            }
            set { lastActivedAtEx = value; }
        }


    }
    public class DataTimeSheet
    {
        public int ep_id { get; set; }
        public DateTime time { get; set; }
        public int shift_id { get; set; }
        public string shift_name { get; set; }
    }

    public class Employee
    {
        public int com_id { get; set; }
        public int dep_id { get; set; }
        public int start_working_time { get; set; }
        public int position_id { get; set; }
        public int team_id { get; set; }
        public int group_id { get; set; }
        public int time_quit_job { get; set; }
        public string ep_description { get; set; }
        public string ep_featured_recognition { get; set; }
        public string ep_status { get; set; }
        public int ep_signature { get; set; }
        public int allow_update_face { get; set; }
        public int version_in_use { get; set; }
        public string _id { get; set; }
    }

    public class InForPerson
    {
        public int scan { get; set; }
        public Account account { get; set; }
        public Employee employee { get; set; }
        public object candidate { get; set; }
        public string _id { get; set; }
    }

    public class ListUser
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
        public string password { get; set; }
        public int? city { get; set; }
        public int? district { get; set; }
        public string address { get; set; }
        public object otp { get; set; }
        public int authentic { get; set; }
        public int isOnline { get; set; }
        public string fromWeb { get; set; }
        public int fromDevice { get; set; }
        public int createdAt { get; set; }
        public long updatedAt { get; set; }
        public string lastActivedAt { get; set; }

        public DateTime LastActivedAt
        {
            get
            {
                DateTime aa;
                if (!string.IsNullOrEmpty(lastActivedAt) && DateTime.TryParse(lastActivedAt, out aa)) return aa;
                return new DateTime(9999, 12, 30);
            }
        }
        public string lastActivedTime
        {
            get
            {
                var x = "";
                DateTime tt;
                if (!string.IsNullOrEmpty(lastActivedAt) && DateTime.TryParse(lastActivedAt, out tt))
                {
                    x = tt.ToString("H:mm tt");
                }
                return x;
            }
            set { lastActivedAt = value; }
        }
        public string lastActivedDate
        {
            get
            {
                var x = "";
                DateTime tt;
                if (!string.IsNullOrEmpty(lastActivedAt) && DateTime.TryParse(lastActivedAt, out tt))
                {
                    x = tt.ToString("dd-MM-yyyy");
                }
                return x;
            }
            set { lastActivedAt = value; }
        }
        //public string lastActivedDate
        //{
        //    get
        //    {
        //        if (lastActivedAt.HasValue)
        //        {
        //            return lastActivedAt.Value.ToString("yyyy-MM-dd");
        //        }
        //        else
        //        {
        //            return string.Join("", "Chưa cập nhật thời gian!"); // Trả về chuỗi rỗng nếu lastActivedAt là null
        //        }
        //    }
        //    set
        //    {
        //        if (DateTime.TryParse(value, out DateTime parsedDate))
        //        {
        //            if (lastActivedAt.HasValue)
        //            {
        //                lastActivedAt = parsedDate.Date.Add(lastActivedAt.Value.TimeOfDay);
        //            }
        //            else
        //            {
        //                lastActivedAt = parsedDate;
        //            }
        //        }
        //        else
        //        {
        //            // Xử lý lỗi nếu định dạng ngày không hợp lệ
        //            throw new ArgumentException("Ngày không hợp lệ!");
        //        }
        //    }
        //}

        //public string lastActivedTime
        //{
        //    get
        //    {
        //        if (lastActivedAt.HasValue)
        //        {
        //            return lastActivedAt.Value.ToString("HH:mm:ss");
        //        }
        //        else
        //        {
        //            return string.Join("","Chưa cập nhật thời gian!"); // Trả về chuỗi rỗng nếu lastActivedAt là null
        //        }
        //    }
        //    set
        //    {
        //        if (DateTime.TryParseExact(value, "HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime parsedTime))
        //        {
        //            if (lastActivedAt.HasValue)
        //            {
        //                lastActivedAt = lastActivedAt.Value.Date.Add(parsedTime.TimeOfDay);
        //            }
        //            else
        //            {
        //                lastActivedAt = parsedTime;
        //            }
        //        }
        //        else
        //        {
        //            // Xử lý lỗi nếu định dạng thời gian không hợp lệ
        //            throw new ArgumentException("Ngày không hợp lệ !");
        //        }
        //    }
        //}
        public int time_login { get; set; }
        public int role { get; set; }
        public string latitude { get; set; }
        public string longtitude { get; set; }
        public int idQLC { get; set; }
        public int idTimViec365 { get; set; }
        public int idRaoNhanh365 { get; set; }
        public string chat365_secret { get; set; }
        public List<string> sharePermissionId { get; set; }
        public InForPerson inForPerson { get; set; }
        public string inForCompany { get; set; }
        public string inforRN365 { get; set; }
        public ConfigChat configChat { get; set; }
        public int scan { get; set; }
        public int? chat365_id { get; set; }
        public int? scan_base365 { get; set; }
        public int? check_chat { get; set; }
    }

    public class Root_TimeKeeping
    {
        public Data_Timkeeping data { get; set; }
        public object error { get; set; }
    }


}
