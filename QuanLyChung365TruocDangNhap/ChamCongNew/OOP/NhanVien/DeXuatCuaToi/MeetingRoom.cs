using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi
{
    public class MeetingRoom
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class MeetingRoomWitStatus
        {
            public string _id { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int diadiem { get; set; }
            public int succhua { get; set; }
            public int trangthai { get; set; }
            public int com_id { get; set; }
            public int created_at { get; set; }
            public int? updated_at { get; set; }
            public bool deleted { get; set; }
            public int __v { get; set; }
            public int availableStatus { get; set; }
        }

        public class Root
        {
            public List<MeetingRoomWitStatus> meetingRoomWitStatus { get; set; }
        }

    }
}
