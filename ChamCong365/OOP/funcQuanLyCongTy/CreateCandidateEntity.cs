using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.funcQuanLyCongTy
{
    public class CreateCandidateEntity
    {
        public class Data
        {
            public bool result { get; set; }
            public string message { get; set; }
            public Candidate candidate { get; set; }
            public List<ListSkillInsert> listSkillInsert { get; set; }
            public List<ListNotify> listNotify { get; set; }
        }

        public class ListNotify
        {
            public int id { get; set; }
            public int canId { get; set; }
            public int type { get; set; }
            public int comNotify { get; set; }
            public int comId { get; set; }
            public int userId { get; set; }
            public DateTime createdAt { get; set; }
            public string _id { get; set; }
        }

        public class ListSkillInsert
        {
            public int id { get; set; }
            public int canId { get; set; }
            public object skillName { get; set; }
            public int skillVote { get; set; }
            public DateTime createAt { get; set; }
            public string _id { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }
    }
}
