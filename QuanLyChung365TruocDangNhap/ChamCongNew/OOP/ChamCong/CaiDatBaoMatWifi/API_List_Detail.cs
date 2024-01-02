using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi
{
    public class API_List_Detail
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Content
        {
            public string key { get; set; }
            public object value { get; set; }
        }

        public class Data_List_Detail
        {

            public bool? result { get; set; }
            public string message { get; set; }
            public List<ListAll> data { get; set; }

        }
        public class ListAll
        {
            public Detail detail { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public string displayDp { get; set; }
            public int stt { get; set; }
            public List<ListOrg> list_org { get; set; }
            public string displayDp1 { get; set; }
            public List<ListPo> list_pos { get; set; }
            public string displayDp2 { get; set; }
            public List<ListEmp> list_emps { get; set; }
            public string displayDp3 { get; set; }
            public List<ListLoc> list_loc { get; set; }

            public string displayDp5 { get; set; }
            public List<string> list_device { get; set; }
            public string displayDp6 { get; set; }
            public List<ListShift> list_shifts { get; set; }
            public string displayDp7 { get; set; }
            public List<ListWifi> list_wifi { get; set; }
            public List<RelaxTime> list_time { get; set; }


        }
        public class Detail
        {
            public int type_loc { get; set; }
            public int type_wifi { get; set; }
            public string _id { get; set; }
            public int? setting_id { get; set; }
            public int? com_id { get; set; }
            public List<int?> list_org { get; set; }
            public List<int?> list_pos { get; set; }
            public List<int?> list_emps { get; set; }
            public List<object> list_shifts { get; set; }
            public List<int?> list_wifi { get; set; }
            public List<int?> list_loc { get; set; }
            public List<int?> list_ip { get; set; }
            public List<string> list_device { get; set; }
            public DateTime? start_time { get; set; }
            public DateTime? end_time { get; set; }
            public DateTime? create_time { get; set; }
            public string setting_name { get; set; }
        }
        public class ListEmp
        {
            public int? _id { get; set; }
            public string userName { get; set; }
            public int? idQLC { get; set; }
        }
        public class ListLoc
        {
            public string _id { get; set; }
            public int? cor_id { get; set; }
            public string cor_location_name { get; set; }
            public double? cor_lat { get; set; }
            public double? cor_long { get; set; }
            public int? cor_radius { get; set; }
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
            public string start_time { get; set; }
            public string start_time_latest { get; set; }
            public string end_time { get; set; }
            public string end_time_earliest { get; set; }
            public int? over_night { get; set; }
            public int? shift_type { get; set; }
            public object num_to_calculate { get; set; }
            public int? num_to_money { get; set; }
            public int? money_per_hour { get; set; }
            public int? is_overtime { get; set; }
            public int? status { get; set; }
            public List<RelaxTime> relaxTime { get; set; }
            public int? flex { get; set; }
            public DateTime? create_time { get; set; }
            public int? type_shift { get; set; }
            private int? Type_shift { get; set; }
            public string type_name { get; set; }
        }
        public class RelaxTime
        {
            public object start_time_relax { get; set; }
            public object end_time_relax { get; set; }
            public string _id { get; set; }
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

        public class List_Detail
        {
            public Data_List_Detail data { get; set; }
            public object error { get; set; }
        }
    }
}
