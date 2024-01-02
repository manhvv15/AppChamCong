﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi.ChiTietDeXuat;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi
{
    public class DeXuatCuaToi_Entities
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

        public class Data
        {
            public bool result { get; set; }
            public string message { get; set; }
            public int totaldx { get; set; }
            public int dxChoDuyet { get; set; }
            public int dxCanduyet { get; set; }
            public int dxduyet { get; set; }
            public List<InforDx> data { get; set; }
            public int? totalPages { get; set; }

        }
        public class InforDx
        {

            public int _id { get; set; }
            public int id_user { get; set; }
            public string name_dx { get; set; }
            public string name_user { get; set; }
            public string id_user_duyet { get; set; }
            public string name_user_duyet { get; set; }
            public int type_dx { get; set; }
            public string type_dx_string { get; set; }
            public string Type_duyet { get; set; }
            public string type_duyet
            {
                get
                {
                    switch (Type_duyet)
                    {
                        case "5": return "Được chấp thuận";
                        case "11": return "Chờ công ty duyệt";
                        case "3": return "Bị từ chối";
                        case "6": return "Bắt buộc đi làm";
                        case "0": return "Đang chờ duyệt";
                        case "7": return "Đã được tiếp nhận";
                        case "10": return "Chờ lãnh đạo còn lại duyệt";
                        default: return "";

                    }

                }

                set { Type_duyet = value; }
            }
            public int Time_create { get; set; }
            public string time_create
            {
                set
                {
                    Time_create = int.Parse(value);
                }
                get
                {
                    return DateTimeOffset.FromUnixTimeSeconds(Time_create).ToLocalTime().ToString("HH:mm tt dd/MM/yyyy");
                }
            }
            public int? com_id { get; set; }
            public int? kieu_duyet { get; set; }
            public string id_user_theo_doi { get; set; }
            public List<FileKem> file_kem { get; set; }
            public int? type_time { get; set; }
            public object time_tiep_nhan { get; set; }
            public object time_duyet { get; set; }
            public int? active { get; set; }
            public int? del_type { get; set; }
            public int? tam_ung_status { get; set; }
            public int? thanh_toan_status { get; set; }
            public string organizeDetailName { get; set; }
            public string refuse_reason { get; set; }
            public int? __v { get; set; }

        }
        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }
    }
}
