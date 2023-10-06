using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.NhanVien.DonDeXuat
{
    public class XetDuyetVaTheoDoi
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class DataXetDuyetTheoDoi

        {
            public bool result { get; set; }
            public string message { get; set; }
            public List<ListUsersDuyet> listUsersDuyet { get; set; }
            public List<ListUsersTheoDoi> listUsersTheoDoi { get; set; }
        }

        public class Employee
        {
            public int dep_id { get; set; }
            public int position_id { get; set; }
        }

        public class InForPerson
        {
            public Employee employee { get; set; }
        }

        public class ListUsersDuyet
        {
            public int _id { get; set; }
            public string userName { get; set; }
            public string avatarUser { get; set; }
            public int idQLC { get; set; }
            public InForPerson inForPerson { get; set; }
            public int level { get; set; }
        }

        public class ListUsersTheoDoi
        {
            public int _id { get; set; }
            public string userName { get; set; }
            public string avatarUser { get; set; }
            public int idQLC { get; set; }
        }

        public class XetDuyetTheoDoi

        {
            public DataXetDuyetTheoDoi data { get; set; }
            public object error { get; set; }
        }


    }
}
