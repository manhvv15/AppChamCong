using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.funcQuanLyCongTy
{
    public class organizationalStructure
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool result { get; set; }
            public string message { get; set; }
            public InfoCompany infoCompany { get; set; }
        }

        public class InfoChildCompany
        {
            public int com_id { get; set; }
            public string com_name { get; set; }
            public int tong_nv { get; set; }
            public int tong_nv_da_diem_danh { get; set; }
            public string manager { get; set; }
            public string deputy { get; set; }
            public string type { get; set; }
            public string typeBottom { get; set; }
            private int totalEmployeeNoTimeKeep { get; set; }
            public int TotalEmployeeNoTimeKeep
            {
                get
                {
                    settotalEmployeeNoTimeKeep();
                    return totalEmployeeNoTimeKeep;
                }
            }
            public void settotalEmployeeNoTimeKeep()
            {
                totalEmployeeNoTimeKeep = tong_nv - tong_nv_da_diem_danh;
            }
            private List<InfoDep> InfoDep { get; set; }
            public List<InfoDep> infoDep
            {
                get { return InfoDep; }
                set
                {
                    InfoDep = value;
                    setLineType();
                }
            }
            public void setLineType()
            {
                if (InfoDep.Count > 0)
                {
                    typeBottom = "LineBottom";
                    InfoDep[0].type = "Head";
                    InfoDep[infoDep.Count - 1].type = "Tail";
                }

            }

        }

        public class InfoCompany
        {
            public int parent_com_id { get; set; }
            public string companyName { get; set; }
            public object parent_manager { get; set; }
            public object parent_deputy { get; set; }
            public int tong_nv { get; set; }
            public int tong_nv_da_diem_danh { get; set; }
            private int totalEmployeeNoTimeKeep { get; set; }
            public int TotalEmployeeNoTimeKeep
            {
                get
                {
                    settotalEmployeeNoTimeKeep();
                    return totalEmployeeNoTimeKeep;
                }
            }
            public void settotalEmployeeNoTimeKeep()
            {
                totalEmployeeNoTimeKeep = tong_nv - tong_nv_da_diem_danh;
            }
            private List<InfoDep> InfoDep { get; set; }
            public List<InfoDep> infoDep
            {
                get { return InfoDep; }
                set
                {
                    InfoDep = value;
                    setDepLineType();
                }
            }
            public void setDepLineType()
            {
                if (InfoDep.Count > 0)
                {
                    InfoDep[0].type = "Head";
                    //InfoDep[infoDep.Count - 1].type = "Tail";
                }
            }
            private List<InfoChildCompany> InfoChildCompany { get; set; }
            public List<InfoChildCompany> infoChildCompany
            {
                get { return InfoChildCompany; }
                set
                {
                    InfoChildCompany = value;
                    setChildComLineType();
                }
            }
            public void setChildComLineType()
            {
                if (InfoChildCompany.Count > 0)
                {
                    InfoChildCompany[InfoChildCompany.Count - 1].type = "Tail";
                    //InfoDep[infoDep.Count - 1].type = "Tail";
                }
            }




        }

        public class InfoDep
        {
            public string manager { get; set; }
            public int dep_id { get; set; }
            public string dep_name { get; set; }
            public string deputy { get; set; }
            public string description { get; set; }
            public int total_emp { get; set; }
            public int tong_nv_da_diem_danh { get; set; }
            private int totalEmployeeNoTimeKeep { get; set; }
            public int TotalEmployeeNoTimeKeep
            {
                get
                {
                    settotalEmployeeNoTimeKeep();
                    return totalEmployeeNoTimeKeep;
                }
            }
            public void settotalEmployeeNoTimeKeep()
            {
                totalEmployeeNoTimeKeep = total_emp - tong_nv_da_diem_danh;
            }
            public string type { get; set; }
            public string typeBottom { get; set; }
            private List<InfoTeam> InfoTeam { get; set; }
            public List<InfoTeam> infoTeam
            {
                get { return this.InfoTeam; }
                set
                {

                    this.InfoTeam = value;
                    SetTypeLineBottom();
                    SetTeamTypeLine();


                }
            }

            public void SetTeamTypeLine()
            {
                if (this.InfoTeam.Count > 0)
                {
                    InfoTeam[0].type = "Head";
                    InfoTeam[InfoTeam.Count - 1].type = "Tail";
                    if (InfoTeam.Count == 1) InfoTeam[0].type = "Center";

                }
            }
            public void SetTypeLineBottom()
            {
                if (this.InfoTeam.Count > 0) this.typeBottom = "LineBottom";
            }
        }



        public class InfoTeam
        {

            public int gr_id { get; set; }
            public string gr_name { get; set; }
            public string description { get; set; }
            public string to_truong { get; set; }
            public string pho_to_truong { get; set; }
            public int tong_nv { get; set; }
            public int tong_nv_da_diem_danh { get; set; }
            private int totalEmployeeNoTimeKeep { get; set; }
            public int TotalEmployeeNoTimeKeep
            {
                get
                {
                    settotalEmployeeNoTimeKeep();
                    return totalEmployeeNoTimeKeep;
                }
            }
            public void settotalEmployeeNoTimeKeep()
            {
                totalEmployeeNoTimeKeep = tong_nv - tong_nv_da_diem_danh;
            }
            public string type { get; set; }
            public string typeBottom { get; set; }
            private List<InfoGroup> InfoGroup { get; set; }
            public List<InfoGroup> infoGroup
            {
                get { return this.InfoGroup; }
                set
                {

                    {
                        this.InfoGroup = value;
                        setGroupLineType();
                        SetTypeLineBottom();
                    }

                }
            }
            public void setGroupLineType()
            {
                if (InfoGroup.Count > 0)
                {
                    InfoGroup[0].type = "Head";
                    InfoGroup[InfoGroup.Count - 1].type = "Tail";
                    if (InfoGroup.Count == 1) InfoGroup[0].type = "Center";
                }
            }
            public void SetTypeLineBottom()
            {
                if (this.InfoGroup.Count > 0) this.typeBottom = "LineBottom";
            }
        }
        public class InfoGroup
        {

            public int gr_id { get; set; }
            public string gr_name { get; set; }
            public string description { get; set; }
            public string truong_nhom { get; set; }
            public string pho_truong_nhom { get; set; }
            public int group_tong_nv { get; set; }
            public int tong_nv_da_diem_danh { get; set; }
            private int totalEmployeeNoTimeKeep { get; set; }
            public int TotalEmployeeNoTimeKeep
            {
                get
                {
                    settotalEmployeeNoTimeKeep();
                    return totalEmployeeNoTimeKeep;
                }
            }
            public void settotalEmployeeNoTimeKeep()
            {
                totalEmployeeNoTimeKeep = group_tong_nv - tong_nv_da_diem_danh;
            }
            public string type { get; set; }
        }
        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }


    }
}
