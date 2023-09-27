using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.funcQuanLyCongTy
{
    public class OrgStructureEmployee
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Company
        {
            public int _id { get; set; }
            public string userName { get; set; }
            public int idQLC { get; set; }
        }

        public class Data
        {
            public bool result { get; set; }
            public string message { get; set; }
            public int totalCount { get; set; }
            public Company company { get; set; }
            public List<Employee> listEmployee { get; set; }
        }

        public class Employee
        {
            public int STT { get; set; }
            public int _id { get; set; }
            public int idQLC { get; set; }
            public string userName { get; set; }
            public string phone { get; set; }
            public string phoneTK { get; set; }
            public string email { get; set; }
            public string address { get; set; }
            public double? birthday { get; set; }
            public string gender { get; set; }
            public string married { get; set; }
            public string experience { get; set; }
            public string education { get; set; }
            public int com_id { get; set; }
            public string Company { get; set; }
            public int dep_id { get; set; }
            public string Department { get; set; }
            public int? group_id { get; set; }
            public int? team_id { get; set; }
            public string Group { get; set; }
            public string Team { get; set; }
            public int position_id { get; set; }
            public double? start_working_time { get; set; }
            public string ly_do_nghi { get; set; }

            //custom properties
            public string ep_gender
            {
                get
                {
                    return GetGender(gender);
                }
            }

            public string ep_married
            {
                get
                {
                    return GetCanMarried(married);
                }

            }
            public string ep_education
            {
                get
                {
                    return GetEducation(education);
                }

            }

            public string ep_experience
            {
                get
                {
                    return GetCanExp(experience);
                }

            }

            public string positionName { get; set; }
        

            public string ep_birthday { get;set;
            }

            public string ep_start_working_time
            { get;set;
            }

            private static string GetCanMarried(string can_is_married)
            {
                switch (can_is_married)
                {
                    case "1":
                        return "Độc thân";
                    case "2":
                        return "Đã lập gia đình";
                    default:
                        return "Chưa cập nhật";
                }
            }

            private static string GetGender(string gender)
            {
                switch (gender)
                {
                    case "0":
                        return "Nam";
                    case "1":
                        return "Nữ";
                    case "2":
                        return "Khác";
                    default:
                        return "Chưa cập nhật";
                }
            }

            private static string GetCanExp(string exp)
            {
                switch (exp)
                {
        
                    case "1":
                        return "Chưa có kinh nghiệm";
                    case "2":
                        return "Dưới 1 năm kinh nghiệm";
                    case "3":
                        return "1 năm kinh nghiệm";
                    case "4":
                        return "2 năm kinh nghiệm";
                    case "5":
                        return "3 năm kinh nghiệm";
                    case "6":
                        return "4 năm kinh nghiệm";
                    case "7":
                        return "5 năm kinh nghiệm";
                    case "8":
                        return "Trên 5 năm kinh nghiệm";
                    default:
                        return "Chưa cập nhật";
                }
            }

            private static string GetEducation(string can_education)
            {
                switch (can_education)
                {
                    case "1":
                        return "Trên Đại học";
                    case "2":
                        return "Đại Học";
                    case "3":
                        return "Cao đẳng";
                    case "4":
                        return "Trung cấp";
                    case "5":
                        return "Đào tạo nghề";
                    case "6":
                        return "Trung học phổ thông";
                    case "7":
                        return "Trung học cơ sở";
                    case "8":
                        return "Tiểu học";
                    default:
                        return "Chưa cập nhật";

                }
            }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }


    }
}
