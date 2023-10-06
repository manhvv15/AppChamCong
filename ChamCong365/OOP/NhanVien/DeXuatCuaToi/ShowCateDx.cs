using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.NhanVien.DeXuatCuaToi
{
    public class ShowCateDxEntities
    {
        public class Data
        {
            public bool result { get; set; }
            public string message { get; set; }
            public List<Showcatedx> showcatedx { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }

        public class Showcatedx
        {
            public int _id { get; set; }
            public string name_cate_dx { get; set; }
        }
    }
}
