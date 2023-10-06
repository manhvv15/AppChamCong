using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data_NhanVien
    {
        public bool result { get; set; }
        public string message { get; set; }
        public int totalItems { get; set; }
        public List<Item_NhanVien> items { get; set; }
    }

    public class Item_NhanVien : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public int _id { get; set; }
        public int ep_id { get; set; }
        public string ep_email { get; set; }
        public string ep_email_lh { get; set; }
        public string ep_phone_tk { get; set; }
        public string ep_name { get; set; }
        public int? ep_education { get; set; }
        public int? ep_exp { get; set; }
        public string ep_phone 
        { 
            get ; 
            set; 
        }
        public string ep_image { get; set; }
        public string ep_address { get; set; }
        public int ep_gender { get; set; }
        public int ep_married { get; set; }
        public int? ep_birth_day { get; set; }
        public string ep_description { get; set; }
        public int create_time { get; set; }
        public int role_id { get; set; }
        public int? group_id { get; set; }
        public int? start_working_time { get; set; }
        public int position_id { get; set; }
        public string ep_status { get; set; }
        private int _allow_update_face;
        public int allow_update_face
        {
            get { return _allow_update_face; }
            set{ _allow_update_face = value; OnPropertyChanged(); }
            
        }
        public int com_id { get; set; }
        public int? dep_id { get; set; }
        public string dep_name { get; set; }
        public string gr_name { get; set; }

        //custom field
        public string positionName { get; set; }

        public string ep_married_status { get; set; }

        private bool _isCheck;
        public bool isChecked {
            get { return _isCheck; }
            set { _isCheck = value; OnPropertyChanged(); }
        }
    }

    public class Root_NhanVien
    {
        public Data_NhanVien data { get; set; }
        public object error { get; set; }
    }


}
