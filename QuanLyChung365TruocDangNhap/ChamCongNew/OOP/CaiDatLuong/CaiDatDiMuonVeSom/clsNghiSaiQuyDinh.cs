using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatDiMuonVeSom
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class List_NSQD
    {
        public int ep_id { get; set; }
        public string avatarUser { get; set; }
        public DateTime date { get; set; }
        public string date_format { get; set; }
        public int shift_id { get; set; }
        public string shift_name { get; set; }
        public int phat { get; set; }
        public string userName { get; set; }
        public string organizeDetailName { get; set; }
        public string positionName { get; set; }
    }

    public class Root_NSQD
    {
        public List<List_NSQD> data { get; set; }
        public string message { get; set; }
    }


}
