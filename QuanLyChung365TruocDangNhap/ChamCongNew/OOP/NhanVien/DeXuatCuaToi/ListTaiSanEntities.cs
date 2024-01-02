using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.DetailOfDon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi
{
    public class ListTaiSanEntities
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class TaiSan
        {
            public string _id { get; set; }
            public int? ts_id { get; set; }
            public string ts_ten { get; set; }
            public int? so_luong_con_lai { get; set; }
        }

        public class Root
        {
            public List<TaiSan> data { get; set; }
        }


    }
}
