using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi
{
    public class XemChiTietLichLichLamViecEntites
    {
        public class Data
        {
            public string date { get; set; }
            public int day { get; set; }
            public string shift_id { get; set; }
            public List<string> listShift_id { get; set; }
        }

        public class Root
        {
            public int type { get; set; }
            public List<Data> data { get; set; }
        }
    }
}
