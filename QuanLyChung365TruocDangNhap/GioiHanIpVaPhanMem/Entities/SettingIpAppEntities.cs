using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities
{
    public class SettingIpAppEntities
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool result { get; set; }
            public string message { get; set; }
            public int total { get; set; }
            public List<User> data { get; set; }

        }
        public class User
        {
            public int STT { get; set; }
            public string phone { get; set; }
            public string avatarUser { get; set; }
            public int ep_id { get; set; }
            public string userName { get; set; }
            public string organizeDetailName { get; set; }
            public string positionName { get; set; }
            public List<string> listIPs { get; set; }
            public List<string> listApps { get; set; }
            public string start_date { get; set; }
            public string end_date { get; set; }
            public string app_num { get; set; }
            public string ip_num { get { return listIPs.Count.ToString(); } }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }
    }
}
