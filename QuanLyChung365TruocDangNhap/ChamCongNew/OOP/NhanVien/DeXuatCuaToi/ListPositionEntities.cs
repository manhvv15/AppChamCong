using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi
{
    public class ListPositionEntities
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Position
        {
            public string _id { get; set; }
            public int? id { get; set; }
            public int? comId { get; set; }
            public string positionName { get; set; }
            public int? level { get; set; }
            public int? isManager { get; set; }
            public int? created_time { get; set; }
            public int? update_time { get; set; }
        }

        public class Root
        {
            public List<Position> positions { get; set; }
        }
    }
}
