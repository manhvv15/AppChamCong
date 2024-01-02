using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD;

namespace QuanLyChung365TruocDangNhap.ThietLapCongTy.Entities
{
    public class API_List_Employee_Untimed
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public int? totalCount { get; set; }
            public List<EmployeeData> data { get; set; }

        }
        public class EmployeeData
        {
            public int? _id { get; set; }
            public int? idQLC { get; set; }
            public string userName { get; set; }
            public string phone { get; set; }
            public string phoneTK { get; set; }
            public string email { get; set; }
            public string address { get; set; }
            public double? birthday { get; set; }
            public int? gender { get; set; }
            public int? married { get; set; }
            public int? experience { get; set; }
            public int? education { get; set; }
            public int? com_id { get; set; }
            public string comName { get; set; }
            public int? organizeDetailId { get; set; }
            public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
            public string organizeDetailName { get; set; }
            public int? position_id { get; set; }
            public string positionName { get; set; }
            public double? start_working_time { get; set; }
            public string ly_do_nghi { get; set; }
            public string ly_do_nghi_phep { get; set; }
            public int? type_nghi { get; set; }
        }
        public class ListOrganizeDetailId
        {
            public int? level { get; set; }
            public int? organizeDetailId { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }


    }
}
