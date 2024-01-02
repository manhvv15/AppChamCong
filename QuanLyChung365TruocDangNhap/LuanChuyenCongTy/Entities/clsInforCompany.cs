using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.LuanChuyenCongTy.Entities
{
    public class Data_InforCompany
    {
        public bool result { get; set; }
        public string message { get; set; }
        public Data_InforCompany data { get; set; }
        public int _id { get; set; }
        public string userName { get; set; }
        public int idQLC { get; set; }
    }

    public class Root_InforCompany
    {
        public Data_InforCompany data { get; set; }
        public object error { get; set; }
    }
}
