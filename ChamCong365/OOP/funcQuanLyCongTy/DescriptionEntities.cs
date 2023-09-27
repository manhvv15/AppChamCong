using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.funcQuanLyCongTy
{
    public class DescriptionEntities
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool result { get; set; }
            public string message { get; set; }
            public Info info { get; set; }
        }

        public class Info
        {

            public string description { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }
    }
}
