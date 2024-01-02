using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Comons
{
    public class Api_ThietLapCongTy
    {
        public const string Api_SoDoToChuc = "http://210.245.108.202:3000/api/qlc/organizeDetail/list";
        public const string Api_DanhSachNhanVien = "http://210.245.108.202:3000/api/qlc/managerUser/listUser";
        public const string Api_DanhSachNhanVien_Filter = "http://210.245.108.202:3000/api/qlc/managerUser/listAllFilter";

        public const string Api_update_org = "https://api.timviec365.vn/api/qlc/organizeDetail/update";
        public const string Api_delete_org = "https://api.timviec365.vn/api/qlc/organizeDetail/delete";
        public const string Api_Employee_listEmUntimed = "https://api.timviec365.vn/api/qlc/managerUser/listEmUntimed";
        public const string Api_Create_Org = "https://api.timviec365.vn/api/qlc/organizeDetail/create";
        public const string Api_addEmployeeToOrganize = "https://api.timviec365.vn/api/qlc/organizeDetail/addListUser";
        public const string Api_SoDoViTri = "https://api.timviec365.vn/api/qlc/positions/list";
        public const string Api_position_listAll = "https://api.timviec365.vn/api/qlc/positions/listAll";
        public const string Api_create_position = "https://api.timviec365.vn/api/qlc/positions/create";
        public const string Api_delete_position = "https://api.timviec365.vn/api/qlc/positions/delete";
        public const string Api_update_position = "https://api.timviec365.vn/api/qlc/positions/update";
        public const string Api_get_district = "https://hungha365.com/data/district.json";
        public const string IPnAPP_listUser = "http://210.245.108.202:3000/api/qlc/settingIPApp/listUser";
        public const string listAll_organize = "http://210.245.108.202:3000/api/qlc/organizeDetail/listAll";
        public const string IPnApp_listApp = "http://210.245.108.202:3000/qlc/settingIPApp/listApp";
        public const string list_position = "http://210.245.108.202:3000/api/qlc/positions/listAll";
        public const string userManager_listUsers = "http://210.245.108.202:3000/api/qlc/managerUser/listUser";
        public const string list_all_app = "http://210.245.108.202:3000/api/qlc/settingIPApp/listAllApp";
        public const string IPnAPP_Setting = "https://api.timviec365.vn/api/qlc/settingIPApp/setting";

    }
}
