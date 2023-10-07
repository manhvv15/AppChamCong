using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.TestPhanTrang
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data_API_DS_Cty
    {
        public bool result { get; set; }
        public string message { get; set; }
        public int totalItems { get; set; }
        public List<Company> items { get; set; }
    }

    public class Company
    {
        public int com_id { get; set; }
        public int com_parent_id { get; set; }
        public string com_name { get; set; }
        public string com_email { get; set; }
        public object com_phone_tk { get; set; }
        public string id_way_timekeeping { get; set; }
        public string com_phone { get; set; }
        public string com_logo { get; set; }
        public string com_address { get; set; }
        public int com_create_time { get; set; }
        public List<object> manager { get; set; }
        public List<object> deputy { get; set; }
    }

    public class API_DS_Cty
    {
        public Data_API_DS_Cty data { get; set; }
        public object error { get; set; }
    }


}
