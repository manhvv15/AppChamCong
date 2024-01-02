using QuanLyChung365TruocDangNhap.Hr.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace QuanLyChung365TruocDangNhap.CacChucNangTaiKhoan.Entities
{
    public class Data_Company
    {
        public bool result { get; set; }
        public string message { get; set; }
        public Data_Company data { get; set; }
        public int _id { get; set; }
        public int com_id { get; set; }
        public object com_parent_id { get; set; }
        public string com_name { get; set; }
        public string com_email { get; set; }
        public string com_phone_tk { get; set; }
        public string type_timekeeping { get; set; }
        public string id_way_timekeeping { get; set; }
        public string com_logo { get; set; }
        public int com_role_id { get; set; }
        public int com_size { get; set; }
        public string com_description { get; set; }
        public int com_create_time { get; set; }
        public double com_update_time { get; set; }
        public int com_authentic { get; set; }
        public string com_qr_logo { get; set; }
        public BitmapImage avatarUser_format
        {
            get
            {
                BitmapImage img = null;
                if (!string.IsNullOrEmpty(com_qr_logo))
                {
                    img = new Uri("https://chamcong.24hpay.vn/upload/employee/" + com_qr_logo).GetThumbnail(100);
                }
                if (img == null) img = new Uri("https://chamcong.timviec365.vn/images/ep_logo.png").GetThumbnail(100);
                return img;
            }
        }
        public int enable_scan_qr { get; set; }
        public string from_source { get; set; }
        public string com_email_lh { get; set; }
        public string com_phone { get; set; }
        public string com_address { get; set; }
        public int com_vip { get; set; }
        public int departmentsNum { get; set; }
        public int userNum { get; set; }
    }

    public class Root_Company
    {
        public Data_Company data { get; set; }
        public object error { get; set; }
    }
}
