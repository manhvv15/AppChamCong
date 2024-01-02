using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Entities.AdministrationEntity.EmployeeManager
{
    public class APINestEntity
    {
        public listNest data { get; set; }
        public object error { get; set; }
    }
    public class listNest
    {
        public int itemsPerPage { get; set; }
        public string totalItems { get; set; }
        public List<NestEntity> items { get; set; }
    }
    public class NestEntity
    {
        public string gr_id { get; set; }
        public string dep_id { get; set; }
        public string gr_name { get; set; }
        public string parent_gr { get; set; }
        public string type { get; set; }
        public string typeGroup { get; set; }
        public string typeListNest { get; set; }

        public string total_empNes { get; set; }
        public string total_empNes_noTimeKeep { get; set; }
        public int total_empNes_TimeKeep { get; set; }
        public List<NestEntity> ListGroupEntity { get; set; }

    }

}
