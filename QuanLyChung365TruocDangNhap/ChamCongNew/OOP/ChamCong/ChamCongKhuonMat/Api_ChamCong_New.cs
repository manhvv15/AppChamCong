using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.ChamCongKhuonMat
{
    
    public class Data_ChamCong
    {
        public double score { get; set; }
        public string user_id { get; set; }
    }

    public class Root_ChamCong
    {
        public List<Data_ChamCong> data { get; set; }
    }

}
