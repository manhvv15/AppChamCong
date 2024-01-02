using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.RecommendSetting.OOP
{
    public class API_Update_TimeDuyet
    {
        public class Data_API_Update_TimeDuyet
        {
            public string message { get; set; }
            public Update update { get; set; }
        }

        public class Update
        {
            public int _id { get; set; }
            public int id_dx { get; set; }
            public string name_cate_dx { get; set; }
            public int time { get; set; }
            public int com_id { get; set; }
            public int created_time { get; set; }
            public int __v { get; set; }
        }
    }
}
