using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew
{
    public class ListItemComboboxUser
    {
        public class Item
        {
            public string ID { get; set; }
            public string value
            {
                get; set;
            }
        }
        public static List<Item> ListCbxEducationEmployee = new List<Item>()
        {

            new Item(){ID = "1", value = "Trên Đại học"},
            new Item(){ID = "2", value = "Đại học"},
            new Item(){ID = "3", value = "Cao đẳng"},
            new Item(){ID = "4", value = "Trung cấp"},
            new Item(){ID = "5", value = "Đào tạo nghề"},
            new Item(){ID = "6", value = "Trung học phổ thông"},
            new Item(){ID = "7", value = "Trung học cơ sở"},
            new Item(){ID = "8", value = "Tiểu học"}
        };
        public static List<Item> ListCbxExpEmployee = new List<Item>()
        {
            new Item(){ID = "1", value = "Chưa có kinh nghiệm"},
            new Item(){ID = "2", value = "Dưới 1 năm "},
            new Item(){ID = "3", value = "1 năm "},
            new Item(){ID = "4", value = "2 năm "},
            new Item(){ID = "5", value = "3 năm "},
            new Item(){ID = "6", value = "4 năm "},
            new Item(){ID = "7", value = "5 năm "},
            new Item(){ID = "8", value = "Trên 5 năm "}
        };
        public static List<Item> ListCbxMarriedEmployee = new List<Item>() {
                    new Item(){ID = "1", value = "Độc thân"},
            new Item(){ID = "2", value = "Đã lập gia đình"}};

        public static List<Item> ListCbxGenderEmployee = new List<Item>() {
                    new Item(){ID = "1", value = "Nam"},
            new Item(){ID = "2", value = "Nữ"},
            new Item(){ID = "3", value = "Khác"}
        };
        public static List<Item> ListCbxTypeEmployee = new List<Item>() {
                    new Item(){ID = "0", value = "Tất cả"},
            new Item(){ID = "1", value = "Nhân viên chưa nhập lương cơ bản"},
            new Item(){ID = "2", value = "Nhân viên chưa cập nhật khuôn mặt"},
            new Item(){ID = "3", value = "Tài khoản nhân viên trùng"},
            new Item(){ID = "4", value = "Nhân viên chưa có phòng ban"}
        };
        public static List<Item> ListCbxTypeEmployeeOTP = new List<Item>() {
                    new Item(){ID = "0", value = "Tất cả"},
            new Item(){ID = "1", value = "Nhân viên đã xác thực OTP"},
            new Item(){ID = "2", value = "Nhân viên chưa xác thực OTP"},
        };
    }
}
