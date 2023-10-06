using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.NhanVien.DonDeXuat
{
    public class API_DS_ChucVu
    {
        public class Position
        {
            public string _id { get; set; }
            public int id { get; set; }
            public int comId { get; set; }
            public string positionName { get; set; }
            public int level { get; set; }
            public int isManager { get; set; }
            public int created_time { get; set; }
            public int update_time { get; set; }
        }

        public class API_DS_Chuc
        {
            public List<Position> positions { get; set; }
        }
    }
}
