using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB
{
    public class clsLuongBaoHiem
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public List<DataSalary> data_salary { get; set; }
            public List<DataContract> data_contract { get; set; }
        }

        public class DataContract
        {
            public string _id { get; set; }
            public int con_id { get; set; }
            public int con_id_user { get; set; }
            public string con_name { get; set; }
            public DateTime con_time_up { get; set; }
            public string con_time_up_date { get; set; }
            public DateTime con_time_end { get; set; }
            public string con_time_end_date { get; set; }
            public string con_file { get; set; }
            public int con_salary_persent { get; set; }
            public DateTime con_time_created { get; set; }
        }

        public class DataSalary
        {
            public string _id { get; set; }
            public int sb_id { get; set; }
            public int sb_id_user { get; set; }
            public int sb_id_com { get; set; }

            private long _sb_salary_basic;
            public long sb_salary_basic
            {
                get { return _sb_salary_basic; }
                set { _sb_salary_basic = value; }
            }
            public string sb_salary_basic_format
            {
                get { return sb_salary_basic.ToString("N0") + " VNĐ"; }
                set
                {
                    if (int.TryParse(value.Replace(",", ""), out int parsedLuongdc))
                    {
                        _sb_salary_basic = parsedLuongdc;
                    }
                }
            }

            private long _sb_salary_bh;
            public long sb_salary_bh 
            {
                get { return _sb_salary_bh; }
                set { _sb_salary_bh = value; }
            }
            public string sb_salary_bh_format
            {
                get { return sb_salary_bh.ToString("N0") + " VNĐ"; }
                set
                {
                    if (int.TryParse(value.Replace(",", ""), out int parsedLuongdc))
                    {
                        _sb_salary_bh = parsedLuongdc;
                    }
                }
            }

            private long _sb_pc_bh;
            public long sb_pc_bh 
            {
                get { return _sb_pc_bh; }
                set { _sb_pc_bh = value; }
            }
            public string sb_pc_bh_format
            {
                get { return sb_pc_bh.ToString("N0") + " VNĐ"; }
                set
                {
                    if (int.TryParse(value.Replace(",", ""), out int parsedLuongdc))
                    {
                        _sb_pc_bh = parsedLuongdc;
                    }
                }
            }
            public DateTime sb_time_up { get; set; }
            public string sb_time_up_date { get; set; }
            public int sb_location { get; set; }
            public string sb_lydo { get; set; }
            public string sb_quyetdinh { get; set; }
            public int sb_first { get; set; }
            public DateTime sb_time_created { get; set; }
            public string salary_tang_giam { get; set; }
            public int isTang { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public string message { get; set; }
        }


    }
}
