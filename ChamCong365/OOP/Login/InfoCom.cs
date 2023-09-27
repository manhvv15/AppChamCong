using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.Login
{
    public class InfoCom
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool result { get; set; }
            public string message { get; set; }
            public Data data { get; set; }
            public int _id { get; set; }
            public string phoneTK { get; set; }
            public string userName { get; set; }
            public string phone { get; set; }
            public string emailContact { get; set; }
            public string avatarUser { get; set; }
            public object address { get; set; }
            public int authentic { get; set; }
            public int createdAt { get; set; }
            public int idQLC { get; set; }
            public int com_vip { get; set; }
            public int com_size { get; set; }
            public string description { get; set; }
            public int departmentsNum { get; set; }
            public int userNum { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }


    }
}
