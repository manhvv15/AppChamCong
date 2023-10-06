using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.CaiDatLuong.BaoHiem
{
    public class clsDSBaoHiem
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class GroupOne
        {
            public string _id { get; set; }
            public int lgr_id { get; set; }
            public int lgr_id_com { get; set; }
            public string lgr_name { get; set; }
            public string lgr_note { get; set; }
            public DateTime lgr_time_created { get; set; }
        }

        public class Root
        {
            public bool data { get; set; }
            public string message { get; set; }
            public List<TaxList> tax_list { get; set; }
            public List<GroupOne> group_one { get; set; }
        }

        public class TaxList
        {
            public string _id { get; set; }
            public int cl_id { get; set; }
            public string cl_name { get; set; }
            public int cl_salary { get; set; }
            public DateTime cl_day { get; set; }
            public DateTime cl_day_end { get; set; }
            public string cl_active { get; set; }
            public string cl_note { get; set; }
            public int cl_type { get; set; }
            public int cl_type_tax { get; set; }
            public int cl_id_form { get; set; }
            public int cl_com { get; set; }
            public DateTime cl_time_created { get; set; }
            public string calculation_formula { get; set; }
            public List<TinhluongFormSalary> TinhluongFormSalary { get; set; }
        }

        public class TinhluongFormSalary
        {
            public string _id { get; set; }
            public int fs_id { get; set; }
            public int fs_id_com { get; set; }
            public int fs_type { get; set; }
            public string fs_name { get; set; }
            public int fs_data { get; set; }
            public string fs_repica { get; set; }
            public string fs_note { get; set; }
            public DateTime fs_time_created { get; set; }
        }


    }
}
