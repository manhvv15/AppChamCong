using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi
{
    public class API_FilterComp
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Content
        {
            public string key { get; set; }
            public object value { get; set; }
        }

        public class Data_Filter_Comp
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<ListOrg> listOrg { get; set; }
            public List<ListPo> listPos { get; set; }
            public List<ListUser> listUsers { get; set; }
            public List<ListShift> listShifts { get; set; }
            public List<ListLoc> listLoc { get; set; }
            public List<ListWifi> listWifi { get; set; }
        }

        public class Dep
        {
            public string _id { get; set; }
            public int? id { get; set; }
            public int? comId { get; set; }
            public int? parentId { get; set; }
            public string organizeDetailName { get; set; }
            public int? level { get; set; }
            public int? range { get; set; }
            public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
            public List<Content> content { get; set; }
            public int? created_time { get; set; }
            public int? update_time { get; set; }
        }

        public class ListLoc
        {
            public string _id { get; set; }
            public int? cor_id { get; set; }
            public string cor_location_name { get; set; }
            public double? cor_lat { get; set; }
            public double? cor_long { get; set; }
            public double? cor_radius { get; set; }
            public int? id_com { get; set; }
        }

        public class ListOrg
        {
            public string _id { get; set; }
            public int? id { get; set; }
            public int? comId { get; set; }
            public int? parentId { get; set; }
            public string organizeDetailName { get; set; }
            public int? level { get; set; }
            public int? range { get; set; }
            public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
            public List<Content> content { get; set; }
            public int? created_time { get; set; }
            public int? update_time { get; set; }
        }

        public class ListOrganizeDetailId
        {
            public int? level { get; set; }
            public int? organizeDetailId { get; set; }
        }

        public class ListPo
        {
            public string _id { get; set; }
            public int? id { get; set; }
            public int? comId { get; set; }
            public string positionName { get; set; }
            public int? level { get; set; }
            public int? isManager { get; set; }
            public int? created_time { get; set; }
            public int? update_time { get; set; }
        }

        public class ListShift
        {
            public string _id { get; set; }
            public int? shift_id { get; set; }
            public int? com_id { get; set; }
            public string shift_name { get; set; }
            public string type_name { get; set; }

            public string start_time { get; set; }
            public string start_time_latest { get; set; }
            public string end_time { get; set; }
            public string end_time_earliest { get; set; }
            public int? over_night { get; set; }
            public int? shift_type { get; set; }
            public NumToCalculate num_to_calculate { get; set; }
            public int? num_to_money { get; set; }
            public int? is_overtime { get; set; }
            public int? status { get; set; }
            public List<RelaxTime> relaxTime { get; set; }
            public int? flex { get; set; }
            public DateTime? create_time { get; set; }
            public int? type { get; set; }
            public int? money_per_hour { get; set; }
        }

        public class ListUser
        {
            public int? _id { get; set; }
            public string userName { get; set; }
            public int? idQLC { get; set; }
            public List<Dep> dep { get; set; }
            public int? posId { get; set; }
        }

        public class ListWifi
        {
            public string _id { get; set; }
            public int? id { get; set; }
            public int? id_com { get; set; }
            public string ip_access { get; set; }
            public string name_wifi { get; set; }
            public string id_loc { get; set; }
            public int? created_time { get; set; }
            public int? update_time { get; set; }
        }

        public class NumToCalculate
        {
            [JsonProperty("$numberDecimal")]
            public string numberDecimal { get; set; }
        }

        public class RelaxTime
        {
            public object start_time_relax { get; set; }
            public object end_time_relax { get; set; }
            public string _id { get; set; }
        }

        public class Filter_Comp
        {
            public Data_Filter_Comp data { get; set; }
            public object error { get; set; }
        }


    }
}
