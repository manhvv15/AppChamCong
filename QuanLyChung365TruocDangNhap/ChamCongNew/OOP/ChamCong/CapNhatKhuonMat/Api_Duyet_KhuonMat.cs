using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CapNhatKhuonMat
{
    public class Data_Duyet
    {
        public bool result { get; set; }
        public string message { get; set; }
    }

    public class Root_Duyet
    {
        public Data_Duyet data { get; set; }
        public object error { get; set; }
    }

}
