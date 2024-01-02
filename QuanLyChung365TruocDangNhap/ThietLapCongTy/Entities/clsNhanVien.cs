using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD;

namespace QuanLyChung365TruocDangNhap.ThietLapCongTy.Entities
{
    public class List_NhanVien
    {
        public int STTNV { get; set; }
        public int? _id { get; set; }
        public string phone { get; set; }
        public string avatarUser { get; set; }
        public int? ep_id { get; set; }
        public string userName { get; set; }
        public string organizeDetailName { get; set; }
        public string positionName { get; set; }
        public int? position_id { get; set; }
        public int? gender { get; set; }
        public int? married { get; set; }
        public int? experience { get; set; }
        public double? birthday { get; set; }
        public int? education { get; set; }
        public string address { get; set; }
        public double? start_working_time { get; set; }
        public string email { get; set; }
        public int? organizeDetailId { get; set; }
        public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
        public bool isCheck { get; set; }

    }

    public class ListOrganizeDetailId_nv
    {
        public int? level { get; set; }
        public int? organizeDetailId { get; set; }

    }

    public class Data_NhanVien
    {

        public bool? result { get; set; }
        public string message { get; set; }
        public int? total { get; set; }
        public List<List_NhanVien> data { get; set; }


    }
    public class Root_NhanVien
    {
        public Data_NhanVien data { get; set; }
        public object error { get; set; }
    }
}
