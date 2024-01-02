using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD;

namespace QuanLyChung365TruocDangNhap.ChotDonTu.OOP
{
    public class API_ChotDon
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data_Ds_ChotDon
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<ChotDon> data { get; set; }
           
        }
        public class ChotDon: INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            public int _status;

            public int status
            {
                get { return _status; }
                set
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }
            public string _id { get; set; }
            public int? id { get; set; }
            public int? comId { get; set; }
            public int? thang_ap_dung { get; set; }
            public int? nam_ap_dung { get; set; }
            public bool? is_auto { get; set; }
            public DateTime? date_chot { get; set; }
            public DateTime? date_auto_chot { get; set; }
            public int? created_time { get; set; }
            public int? update_time { get; set; }
            public DateTime? createdAt { get; set; }
            public DateTime? updatedAt { get; set; }
        }
        public class Ds_ChotDon
        {
            public Data_Ds_ChotDon data { get; set; }
            public object error { get; set; }
        }


    }
}
