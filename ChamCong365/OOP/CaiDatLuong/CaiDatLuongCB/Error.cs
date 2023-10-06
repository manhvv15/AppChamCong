using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.CaiDatLuong.CaiDatLuongCB
{
    public class Error
    {
        [JsonProperty("code")]
        public int code { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
    }
}
