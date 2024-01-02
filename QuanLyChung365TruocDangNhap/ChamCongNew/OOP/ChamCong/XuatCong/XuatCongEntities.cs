using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.XuatCong
{
    public class XuatCongEntities
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class DataXuatCong
        {
            public int? ep_id { get; set; }
            public string avatarUser = "https://chamcong.timviec365.vn/images/ep_logo.png";
            public string ep_name { get; set; }
            public DateTime? ts_date { get; set; }
            public string ts_date_string { get { return ts_date.Value.ToLocalTime().ToString("dd/MM/yyyy"); } }
            public double? total_seconds { get; set; }
            public string day_of_week { get; set; }
            public double? total_time { get; set; }
            public int? shift_id { get; set; }
            public string shift_name { get; set; }
            public string start_time { get; set; }
            public string end_time { get; set; }
            public string hour_real { get; set; }
            public double? hour { get; set; }
            public double? minute { get; set; }
            public double? seconds { get; set; }
            public double? num_to_calculate { get; set; }
            public double? num_to_money { get; set; }
            public double? num_overtime { get; set; }
            public double? money_per_hour { get; set; }
            public List<DateTime?> lst_time { get; set; }
            public List<string> lst_time_string { get {
                List<string>  list = new List<string>();
                    foreach( var item in lst_time)
                    {
                        list.Add(item.Value.ToLocalTime().ToString("HH:mm:ss"));
                    }
                    return list;
                } }
            public string organizeDetailName { get; set; }
            public double? phat_cong_khac { get; set; }
            public double? late { get; set; }
            public double? early { get; set; }
            public double? phat_tien_muon { get; set; }
            public double? late_second { get; set; }
            public double? early_second { get; set; }
            public double? phat_cong_muon { get; set; }
            public double? cong_xn_them { get; set; }
            public double? tien_xn_them { get; set; }
            public double? tientheogio_xn_them { get; set; }


        }

        public class Root
        {
            public List<DataXuatCong> data { get; set; }
        }


    }
}
