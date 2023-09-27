using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.funcQuanLyCongTy
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class TranferJobData
    {
        public bool result { get; set; }
        public string message { get; set; }
        public int totalCount { get; set; }
        public List<TranferJob> data { get; set; }
    }

    public class TranferJob
    {
        public int _id { get; set; }
        public int ep_id { get; set; }
        public DateTime created_at { get; set; }
        public string note { get; set; }
        public string mission { get; set; }
        public string userName { get; set; }
        public string old_dep_name { get; set; }
        public int old_dep_id { get; set; }
        public string old_position { get; set; }
        public int id_old_position { get; set; }
        public string old_com_name { get; set; }
        public string new_com_name { get; set; }
        public string new_dep_name { get; set; }
        public int new_dep_id { get; set; }
        public string new_position { get; set; }
        public int id_new_position { get; set; }
        public string team_name { get; set; }
        public string gr_name { get; set; }
        public int position_id { get; set; }
        public int new_com_id { get; set; }
        public int old_com_id { get; set; }
        public int? decision_id { get; set; }
    }
    public class TranferJobRoot
    {
        public TranferJobData data { get; set; }
        public object error { get; set; }
    }


}
