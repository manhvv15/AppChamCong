using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien
{
    public class API_DsTaiSan
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class TaiSan
        {
            public string _id { get; set; }
            public int? ts_id { get; set; }
            public string ts_ten { get; set; }
            public int? so_luong_con_lai { get; set; }
            public string Name { get { return ts_ten + "(" + so_luong_con_lai + ")"; } }
        }

        public class DS_TaiSan
        {
            public List<TaiSan> data { get; set; }
        }
    }



}

