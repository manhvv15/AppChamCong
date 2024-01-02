using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec
{
    public class Data
    {
        public bool result { get; set; }
        public string message { get; set; }
    }

    public class Root
    {
        public Data data { get; set; }
        public object error { get; set; }
    }

}
