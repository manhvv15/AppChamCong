using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.funcQuanLyCongTy
{
    public class TamUngTien
    {

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool result { get; set; }
            public string message { get; set; }
            public Data data { get; set; }
            public int totalPages { get; set; }
            public bool hasNextPage { get; set; }
            public List<DeXuat> listDeXuat { get; set; }
            public List<User> listUser { get; set; }
        }

        public class DeXuat
        {
            public NoiDung noi_dung { get; set; }
            public int _id { get; set; }
            public string name_user { get; set; }
            public int id_user { get; set; }
            public int type_duyet { get; set; }
        }

        public class User
        {
            public int _id { get; set; }
            public string userName { get; set; }
            public string avatarUser { get; set; }
            public int idQLC { get; set; }
        }

        public class NoiDung
        {
            public TamUng tam_ung { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }

        public class TamUng
        {
            public decimal? sotien_tam_ung { get; set; }
        }
        public class TamUngTienEntities
        {
            public int _id { get; set; }
            public int ep_id { get; set; }
            public string ep_name { get; set; }
            public string ep_avatar { get; set; }
            public DateTime ngay_tam_ung { get; set; }
            public decimal? sotien_tam_ung { get; set; }
            public string trang_thai { get; set; }
            public bool isCheck { get; set; }
        }
    }
}
