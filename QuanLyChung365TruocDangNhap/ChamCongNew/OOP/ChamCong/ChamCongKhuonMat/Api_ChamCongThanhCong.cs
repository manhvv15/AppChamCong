using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.ChamCongKhuonMat
{
   
    public class Data_ThanhCong
    {
        public bool result { get; set; }
        public string message { get; set; }
    }

    public class Root_ThanhCong
    {
        public Data data { get; set; }
        public object error { get; set; }
    }

}
