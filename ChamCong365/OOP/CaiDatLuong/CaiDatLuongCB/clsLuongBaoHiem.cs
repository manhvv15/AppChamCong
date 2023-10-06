using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.CaiDatLuong.CaiDatLuongCB
{
    public class clsLuongBaoHiem
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public List<DataSalary> data_salary { get; set; }
            public List<DataContract> data_contract { get; set; }
        }

        public class DataContract
        {
            public string _id { get; set; }
            public int con_id { get; set; }
            public int con_id_user { get; set; }
            public string con_name { get; set; }
            public DateTime con_time_up { get; set; }
            public DateTime con_time_end { get; set; }
            public string con_file { get; set; }
            public int con_salary_persent { get; set; }
            public DateTime con_time_created { get; set; }
        }

        public class DataSalary
        {
            public string _id { get; set; }
            public int sb_id { get; set; }
            public int sb_id_user { get; set; }
            public int sb_id_com { get; set; }
            public int sb_salary_basic { get; set; }
            public int sb_salary_bh { get; set; }
            public int sb_pc_bh { get; set; }
            public DateTime sb_time_up { get; set; }
            public int sb_location { get; set; }
            public string sb_lydo { get; set; }
            public string sb_quyetdinh { get; set; }
            public int sb_first { get; set; }
            public DateTime sb_time_created { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public string message { get; set; }
        }


    }
}
