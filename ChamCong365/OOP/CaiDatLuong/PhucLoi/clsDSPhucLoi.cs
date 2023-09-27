using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.CaiDatLuong.PhucLoi
{
    public class clsDSPhucLoi
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public List<ListWelf> list_welf { get; set; }
            public List<ListWelfa> list_welfa { get; set; }
            public List<WfShift> wf_shift { get; set; }
            public List<ListGroup> list_group { get; set; }
        }

        public class ListGroup
        {
            public string _id { get; set; }
            public int lgr_id { get; set; }
            public int lgr_id_com { get; set; }
            public string lgr_name { get; set; }
            public string lgr_note { get; set; }
            public DateTime lgr_time_created { get; set; }
        }

        public class ListWelf
        {
            public string _id { get; set; }
            public int cl_id { get; set; }
            public string cl_name { get; set; }
            public long cl_salary { get; set; }
            public DateTime cl_day { get; set; }
            public DateTime cl_day_end { get; set; }
            public string cl_active { get; set; }
            public string cl_note { get; set; }
            public int cl_type { get; set; }
            public int cl_type_tax { get; set; }
            public string cl_type_tax_s { get; set; }
            public int cl_com { get; set; }
            public DateTime cl_time_created { get; set; }
        }

        public class ListWelfa
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
            public string cl_type_tax_s { get; set; }
            public int cl_com { get; set; }
            public DateTime cl_time_created { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public string message { get; set; }
        }

        public class Shift
        {
            public string _id { get; set; }
            public int shift_id { get; set; }
            public int com_id { get; set; }
            public string shift_name { get; set; }
            public string start_time { get; set; }
            public string start_time_latest { get; set; }
            public string end_time { get; set; }
            public string end_time_earliest { get; set; }
            public DateTime create_time { get; set; }
            public int over_night { get; set; }
            public int shift_type { get; set; }
            public double num_to_calculate { get; set; }
            public int num_to_money { get; set; }
            public int is_overtime { get; set; }
            public int status { get; set; }
        }

        public class WfShift
        {
            public string _id { get; set; }
            public int wf_id { get; set; }
            public int wf_money { get; set; }
            public DateTime wf_time { get; set; }
            public DateTime wf_time_end { get; set; }
            public int wf_shift { get; set; }
            public int wf_com { get; set; }
            public Shift shift { get; set; }
        }


    }
}
