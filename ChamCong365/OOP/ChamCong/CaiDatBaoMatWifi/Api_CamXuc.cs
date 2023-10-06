using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong.CaiDatBaoMatWifi
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data_CamXuc
    {
        public bool result { get; set; }
        public string message { get; set; }
        public List<List_CamXuc> list { get; set; }
    }

    public class List_CamXuc
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string _id { get; set; }
        public int emotion_id { get; set; }
        public string emotion_detail { get; set; }
        public double min_score { get; set; }
        public double max_score { get; set; }
        public int com_id { get; set; }
        public int avg_pass_score { get; set; }
    }

    public class Root_CamXuc
    {
        public Data_CamXuc data { get; set; }
        public object error { get; set; }
    }


}
