using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.DuyetThietBiMoi
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Content
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class DataLichSuDiemDanh
    {
        public bool result { get; set; }
        public string message { get; set; }
        public List<ListLichSuDiemDanh> data { get; set; }
        public int total { get; set; }
    }
    public class ListLichSuDiemDanh: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string _id { get; set; }
        public int sheet_id { get; set; }
        public int ep_id { get; set; }
        public DateTime at_time { get; set; }
        public string display_at_time
        {
            get
            {
                DateTime x = at_time.AddHours(7);
                return x.ToString("HH:mm:ss") + "\n" + x.ToString("dd-MM-yyyy");
            }
        }
        public string device { get; set; }
        public string ts_location_name { get; set; }
        public int shift_id { get; set; }
        public string userName { get; set; }
        public int dep_id { get; set; }
        public string shift_name { get; set; }
        public string image { get; set; }
        public Dep dep { get; set; }
        public string dep_name { get; set; }
        private List<CaLamViec> _lstCa;
        public List<CaLamViec> lstCa
        {
            get { return _lstCa; }
            set { _lstCa = value; OnPropertyChanged(); }
        }
    }

    public class Dep
    {
        public string _id { get; set; }
        public int id { get; set; }
        public int comId { get; set; }
        public int parentId { get; set; }
        public string organizeDetailName { get; set; }
        public int level { get; set; }
        public int range { get; set; }
        public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
        public List<Content> content { get; set; }
        public int created_time { get; set; }
        public int update_time { get; set; }
    }

    public class ListOrganizeDetailId
    {
        public int level { get; set; }
        public int organizeDetailId { get; set; }
    }

    public class API_LichSuDiemDanh
    {
        public DataLichSuDiemDanh data { get; set; }
        public object error { get; set; }
    }
}
