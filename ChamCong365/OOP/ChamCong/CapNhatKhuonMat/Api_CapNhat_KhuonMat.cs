using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong.CapNhatKhuonMat
{
    public class DataFace
    {
        public bool result { get; set; }
        public string message { get; set; }
        public int pageNumber { get; set; }
        public List<ListFace> data { get; set; }
    }

    public class ListFace
    {
        public int _id { get; set; }
        public string userName { get; set; }
        public int dep_id { get; set; }
        public int com_id { get; set; }
        public int allow_update_face { get; set; }
        public int position_id { get; set; }
        public string phoneTK { get; set; }
        public string avatarUser { get; set; }
        public string email { get; set; }
        public int idQLC { get; set; }
        public string dep_name { get; set; }
    }

    public class Root_Face
    {
        public DataFace data { get; set; }
        public object error { get; set; }
    }
}
