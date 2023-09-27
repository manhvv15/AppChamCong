using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.CaiDatLuong.CaiDatLuongCB
{
    public class APIUser
    {
        [JsonProperty("data")]
        public DataUser data { get; set; }
        [JsonProperty("error")]
        public Error error { get; set; }
    }
    public class DataUser
    {
        [JsonProperty("result")]
        public bool result { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
        [JsonProperty("userName")]
        public string userName { get; set; }
        [JsonProperty("total")]
        public int total { get; set; }
        [JsonProperty("currentTime")]
        public Int64 currentTime { get; set; }
        [JsonProperty("countConversation")]
        public int countConversation { get; set; }
        [JsonProperty("user_info")]
        public UserFromServer user_info { get; set; }
        [JsonProperty("user_list")]
        public List<UserFromServer> user_list { get; set; }
        [JsonProperty("warning")]
        public int warning { get; set; }
        [JsonProperty("token")]
        public string token { get; set; }
    }
}
