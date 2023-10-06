using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.funcQuanLyCongTy
{
    public class EmployeeData
    {
        public bool result { get; set; }
        public string message { get; set; }
        public int totalItems { get; set; }
        public List<Employee> items { get; set; }
    }

    public class Employee
    {
        public int STT { get; set; }
        public int _id { get; set; }
        public int ep_id { get; set; }
        public string ep_email { get; set; }
        public string ep_email_lh { get; set; }
        public string ep_phone_tk { get; set; }
        public string ep_name { get; set; }
        public string dep_name { get; set; }
        public int? ep_education { get; set; }
        public int? ep_exp { get; set; }
        public string ep_phone { get; set; }
        public string ep_image { get; set; }
        public string ep_address { get; set; }
        public int ep_gender { get; set; }
        public int ep_married { get; set; }
        public double? ep_birth_day { get; set; }
        public string ep_description { get; set; }
        public double create_time { get; set; }
        public int role_id { get; set; }
        public int? group_id { get; set; }
        public double? start_working_time { get; set; }
        public int position_id { get; set; }
        public string ep_status { get; set; }
        public int allow_update_face { get; set; }
        public int com_id { get; set; }
        public int? dep_id { get; set; }

        public string gr_name { get; set; }

        //custom field
        public string nameDeparment { get; set; }
        public string positionName { get; set; }

        public string ep_married_status { get; set; }

        public bool isCheck { get; set; }



    }

    public class EmployeeRoot
    {
        public EmployeeData data { get; set; }
        public object error { get; set; }
    }

}
