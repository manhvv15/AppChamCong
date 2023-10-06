using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong.CamXuc
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data_OnOff_CamXuc
    {
        public bool result { get; set; }
        public string message { get; set; }
        public bool emotion_setting { get; set; }
    }

    public class Root_OnOff_CamXuc
    {
        public Data_OnOff_CamXuc data { get; set; }
        public object error { get; set; }
    }
}
