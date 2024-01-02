using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ThietLapCongTy.Entities
{


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Child_SoDoViTri
    {
        public int? id { get; set; }
        public int? comId { get; set; }
        public int? parentId { get; set; }
        public int? level { get; set; }
        public string name { get; set; }
        public string comName { get; set; }
        public int? isManager { get; set; }
        public string type { get; set; }
        public string typeBottom { get; set; }
        public List<Child_SoDoViTri> Children { get; set; }
        public List<Child_SoDoViTri> children
        {
            get { return Children; }
            set
            {
                Children = value;
                setLineType();
            }
        }
        public void setLineType()
        {
            if (Children.Count > 0)
            {
                typeBottom = "LineBottom";
                Children[0].type = "Head";
                Children[children.Count - 1].type = "Tail";
                if (Children.Count == 1) Children[0].type = "Center";
            }


        }
    }

    public class Data_SoDoViTri
    {
        public bool? result { get; set; }
        public string message { get; set; }
        public Child_SoDoViTri data { get; set; }

    }

    public class Root_SoDoViTri
    {
        public Data_SoDoViTri data { get; set; }
        public object error { get; set; }
    }



}
