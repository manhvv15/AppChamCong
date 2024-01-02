using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien
{
    public class API_DsTaiSanSuaChua
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class TaiSan
        {
            public string _id { get; set; }
            public int? com_id_sd { get; set; }
            public int? id_nv_sd { get; set; }
            public int? id_pb_sd { get; set; }
            public int? sl_dang_sd { get; set; }
            public int? day_bd_sd { get; set; }
            public int? tinhtrang_ts { get; set; }
            public string capital_name { get; set; }
            public int? sl_tai_san_con_lai { get; set; }
            public int? id_vi_tri_tai_san { get; set; }
            public int? Ma_tai_san { get; set; }
            public int? idbb { get; set; }
            public string Name
            {
                get
                {
                    return capital_name + "(" + sl_dang_sd + ")";
                }
            }
        }

        public class Ds_TaiSan
        {
            public List<TaiSan> data { get; set; }
        }


    }
}
