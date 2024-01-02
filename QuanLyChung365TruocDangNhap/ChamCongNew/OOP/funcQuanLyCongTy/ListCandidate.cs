using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy
{
    public class ListCandidateEntity
    {
        public bool result { get; set; }
        public int code { get; set; }
        public Success success { get; set; }
        public object error { get; set; }
    }

    public class Success
    {
        public string message { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public List<Candidate> data { get; set; }
        public int total { get; set; }
    }

    public class Candidate
    {
        public string full_id { get; set; }
        private string Id;
        public string id
        {
            get
            {
                return Id;
            }
            set
            {
                Id = value;
                SetFullID(value);
            }
        }
        public string name { get; set; }
        public string school { get; set; }
        public string cvFrom { get; set; }
        public string can_birthday { get; set; }
        public string user_recommend { get; set; }
        // tên tin ứng tuyển
        public string recruitment_news_name { get; set; }
        // mã tin ứng tuyển
        public string recruitment_news_id { get; set; }

        public string time_send_cv { get; set; }
        public string interview_time { get; set; }
        public string interview_result { get; set; }
        public string interview_vote { get; set; }
        public string salary_agree { get; set; }
        public string status { get; set; }
        public string cv { get; set; }
        // đường dẫn đến cv ứng viên
        public string cv_path { get; set; }
        // có cv hay chưa
        public bool isCv { get; set; }
        public bool isTime { get; set; }
        public string updated_at { get; set; }
        public string com_id { get; set; }
        private string Can_gender;
        public string can_gender
        {
            get
            {
                return GetGender(Can_gender);
            }
            set
            {
                Can_gender = value;
            }
        }

        private string Can_education;
        public string can_education
        {
            get
            {
                return GetEducation(Can_education);
            }
            set
            {
                Can_education = value;
            }
        }
        private string Can_exp { get; set; }
        public string can_exp
        {
            get
            {
                return GetCanExp(Can_exp);
            }
            set
            {
                Can_exp = value;
            }
        }
        private string Can_is_married;
        public string can_is_married
        {
            get
            {
                return GetCanMarried(Can_is_married);
            }
            set
            {
                Can_is_married = value;
            }
        }

        public string can_address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        private string Star_vote;
        public string star_vote
        {
            get
            {
                return Star_vote;
            }
            set
            {
                Star_vote = value;
                SetStart();
            }
        }
        public bool star1 { get; set; }
        public bool star2 { get; set; }
        public bool star3 { get; set; }
        public bool star4 { get; set; }
        public bool star5 { get; set; }
        public string user_hiring { get; set; }
        public string hr_name { get; set; }
        public string created_at { get; set; }
        public string new_name { get; set; }
        public string new_title { get; set; }
        public string id_process_interview { get; set; }
        public string is_offer_job { get; set; }

        public string hometown { get; set; }
        public string is_switch { get; set; }
        public string position_apply { get; set; }
        public string name_position_apply
        {
            get
            {
                return GetNamePositionApply();
            }
        }
        private string Recruitmen_id;
        // mã quy trình tuyển dụng áp dụng
        public string recruitmen_id
        {
            get
            {
                return Recruitmen_id;
            }
            set
            {
                Recruitmen_id = value;
                recruitmen_id_full = "QTTD" + value;
            }
        }
        // full mã quy trình tuyển dụng áp dụng
        public string recruitmen_id_full { get; set; }
        public string title { get; set; }
        private void SetStart()
        {
            star1 = SetStart(1);
            star2 = SetStart(2);
            star3 = SetStart(3);
            star4 = SetStart(4);
            star5 = SetStart(5);
        }

        private bool SetStart(int indexStar)
        {
            int star_count = Convert.ToInt32(star_vote);
            if (indexStar <= star_count) return true;
            return false;
        }

        private void SetFullID(string id)
        {
            full_id = "UV" + id;
        }

        private string GetGender(string gender)
        {
            switch (gender)
            {
                case "1":
                    return "Nam";
                case "2":
                    return "Nữ";

                default:
                    return "Khác";
            }
        }

        private string GetCanExp(string can_exp)
        {
            switch (can_exp)
            {
                case "0":
                    return "Chưa có kinh nghiệm";
                case "1":
                    return "0 - 1 năm kinh nghiệm";
                case "2":
                    return "1 - 2 năm kinh nghiệm";
                case "3":
                    return "2 - 5 năm kinh nghiệm";
                case "4":
                    return "5 - 10 năm kinh nghiệm";
                case "5":
                    return "Hơn 10 năm kinh nghiệm";
                default:
                    return "Chưa cập nhật";
            }
        }

        private string GetCanMarried(string can_is_married)
        {
            switch (can_is_married)
            {
                case "1":
                    return "Độc thân";
                case "2":
                    return "Đã kết hôn";
                default:
                    return "";
            }
        }

        private string GetEducation(string can_education)
        {
            switch (can_education)
            {
                case "1":
                    return "THPT trở lên";
                case "2":
                    return "Trung học trở lên";
                case "3":
                    return "Chứng chỉ";
                case "4":
                    return "Trung cấp trở lên";
                case "5":
                    return "Cao đẳng trở lên";
                case "6":
                    return "Cử nhân trở lên";
                case "7":
                    return "Đại học trở lên";
                case "8":
                    return "Thạc sĩ trở lên";
                case "9":
                    return "Thạc sĩ Nghệ thuật";
                case "10":
                    return "Thạc sĩ Thương mại";
                case "11":
                    return "Thạc sĩ Khoa học";
                case "12":
                    return "Thạc sĩ Kiến trúc";
                case "13":
                    return "Thạc sĩ QTKD";
                case "14":
                    return "Thạc sĩ Kỹ thuật ứng dụng";
                case "15":
                    return "Thạc sĩ Luật";
                case "16":
                    return "Thạc sĩ Y học";
                case "17":
                    return "Thạc sĩ Dược phẩm";
                case "18":
                    return "Tiến sĩ";
                case "19":
                    return "Khác";
                default:
                    return "";

            }
        }

        private string GetNamePositionApply()
        {
            switch (position_apply)
            {
                case "1":
                    return "SINH VIÊN THỰC TẬP";
                case "2":
                    return "NHÂN VIÊN THỬ VIỆC";
                case "3":
                    return "NHÂN VIÊN CHÍNH THỨC";
                case "4":
                    return "TRƯỞNG NHÓM";
                case "5":
                    return "PHÓ TRƯỞNG PHÒNG";
                case "6":
                    return "TRƯỞNG PHÒNG";
                case "7":
                    return "PHÓ GIÁM ĐỐC";
                case "8":
                    return "GIÁM ĐỐC";
                case "9":
                    return "NHÂN VIÊN PART TIME";
                case "10":
                    return "PHÓ BAN DỰ ÁN";
                case "11":
                    return "TRƯỞNG BAN DỰ ÁN";
                case "12":
                    return "PHÓ TỔ TRƯỞNG";
                case "13":
                    return "TỔ TRƯỞNG";
                case "14":
                    return "PHÓ TỔNG GIÁM ĐỐC";
                case "15":
                    return "";
                case "16":
                    return "TỔNG GIÁM ĐỐC";
                case "17":
                    return "THÀNH VIÊN HỘI ĐỒNG QUẢN TRỊ";
                case "18":
                    return "PHÓ CHỦ TỊCH HỘI ĐỒNG QUẢN TRỊ";
                case "19":
                    return "CHỦ TỊCH HỘI ĐỒNG QUẢN TRỊ";
                case "20":
                    return "NHÓM PHÓ";
                case "21":
                    return "TỔNG GIÁM ĐỐC TẬP ĐOÀN";
                case "22":
                    return "PHÓ TỔNG GIÁM ĐỐC TẬP ĐOÀN";
                default:
                    return "";
            }
        }
        public int? is_delete { get; set; }
        public int? ep_id_crm { get; set; }
        public DateTime? timeSendCv { get; set; }

        public string NvTuyenDung { get; set; }
        public string NguoiGioiThieu { get; set; }
        public List<Skill> listSkill { get; set; }
    }

    public class Skill
    {
        public int stt_skill { get; set; }
        public string id { get; set; }
        public string skill_name { get; set; }
        public string skillName { get { return skill_name; } set { skill_name = value; } }

        public bool star1 { get; set; }
        public bool star2 { get; set; }
        public bool star3 { get; set; }
        public bool star4 { get; set; }
        public bool star5 { get; set; }
        private string Skill_vote;
        public string skillVote
        {
            get
            {
                return Skill_vote;
                //return GetSkillVote();
            }
            set
            {
                Skill_vote = value;
                SetStart();
            }
        }
        public string skill_vote
        {
            get
            {
                return Skill_vote;
                //return GetSkillVote();
            }
            set
            {
                Skill_vote = value;
                SetStart();
            }
        }

        private void SetStart()
        {
            star1 = SetStart(1);
            star2 = SetStart(2);
            star3 = SetStart(3);
            star4 = SetStart(4);
            star5 = SetStart(5);
        }

        private string GetSkillVote()
        {
            if (star5) return "5";
            if (star4) return "4";
            if (star3) return "3";
            if (star2) return "2";
            if (star1) return "1";
            return "0";
        }

        private bool SetStart(int indexStar)
        {
            int star_count = Convert.ToInt32(skill_vote);
            if (indexStar <= star_count) return true;
            return false;
        }
    }

    public class DetailInterview
    {
        public string id { get; set; }
        public string resired_salary { get; set; }
        public string salary { get; set; }
        public string interview_time { get; set; }
        public string ep_interview { get; set; }
        public string note { get; set; }
        public string uv_email { get; set; }
        public string can_id { get; set; }
        public string process_interview_id { get; set; }
        public string process_interview_name { get; set; }
        public string is_switch { get; set; }
        public string created_at { get; set; }
        public string contentsend { get; set; }
    }

    public class DetaiCancelJob
    {
        public string id { get; set; }
        public string can_id { get; set; }
        public string resired_salary { get; set; }
        public string salary { get; set; }
        public string note { get; set; }
        public string is_delete { get; set; }
        public string deleted_at { get; set; }
        public string created_at { get; set; }
        public string is_switch { get; set; }
        private string Status { get; set; }
        public string status
        {
            get
            {
                return GetTypeCancel(Status);
            }
            set
            {
                Status = value;
            }
        }
        private string GetTypeCancel(string status)
        {
            switch (status)
            {
                case "1":
                    return "Hủy phỏng vấn";
                case "2":
                    return "Hủy nhận việc";
                case "3":
                    return "Hủy học việc";
                default:
                    return "Chưa cập nhật";
            }
        }
    }

    public class DetailContractJob
    {
        public string id { get; set; }
        public string can_id { get; set; }
        public string resired_salary { get; set; }
        public string salary { get; set; }
        public string note { get; set; }
        public string is_delete { get; set; }
        public string deleted_at { get; set; }
        public string created_at { get; set; }
        public string is_switch { get; set; }
        public string status { get; set; }
        public string offer_time { get; set; }
        public string ep_offer { get; set; }
    }

    public class DetailFailJob
    {
        public string id { get; set; }
        public string can_id { get; set; }
        public string is_delete { get; set; }
        public string deleted_at { get; set; }
        public string note { get; set; }
        public string email { get; set; }
        public string contentsend { get; set; }
        public string created_at { get; set; }

        private string Type { get; set; }
        public string type
        {
            get
            {
                return GetTypeFail(Type);
            }
            set
            {
                Type = value;
            }
        }
        private string GetTypeFail(string type)
        {
            switch (type)
            {
                case "1":
                    return "Trượt phỏng vấn";
                case "2":
                    return "Trượt học việc";
                case "3":
                    return "Trượt vòng loại hồ sơ";
                default:
                    return "Chưa cập nhật";
            }
        }
    }

    public class DetailGetJob
    {
        public string id { get; set; }
        public string can_id { get; set; }
        public string resired_salary { get; set; }
        public string salary { get; set; }
        public string interview_time { get; set; }
        public string ep_interview { get; set; }
        public string note { get; set; }
        public string uv_email { get; set; }
        public string contentsend { get; set; }
        public string created_at { get; set; }



    }
    public class ListSchedule
    {
        public string id { get; set; }
        public string name_schedule { get; set; }
        public string resired_salary { get; set; }
        public string salary { get; set; }
        public string interview_time { get; set; }
        public string ep_interview { get; set; }
        public string note { get; set; }
        public string uv_email { get; set; }
        public string contentsend { get; set; }
        public string can_id { get; set; }
        public string process_interview_id { get; set; }
        public string is_switch { get; set; }
        public string created_at { get; set; }
        public string phongvan { get; set; }
    }
}
