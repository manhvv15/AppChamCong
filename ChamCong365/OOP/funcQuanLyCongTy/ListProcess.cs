using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.funcQuanLyCongTy
{
    public class ListProcess
    {
        public class Data
        {
            public bool result { get; set; }
            public string message { get; set; }
            public List<Process> data { get; set; }
            public List<Candidate> listCandidate { get; set; }
            public List<CandidateGetJob> listCandidateGetJob { get; set; }
            public List<CandidateCancelJob> listCandidateCancelJob { get; set; }
            public List<CandidateFailJob> listCandidateFailJob { get; set; }
            public List<CandidateContactJob> listCandidateContactJob { get; set; }

        }

        public class Process
        {
            public int id { get; set; }
            public string name { get; set; }
            public string _id { get; set; }
            public int? processBefore { get; set; }
            public int? beforeProcess { get; set; }
            public int? comId { get; set; }
            public DateTime? createdAt { get; set; }
            public int? totalCandidate { get; set; }
            public List<Candidate> listCandidate { get; set; }
        }

        public class Candidate
        {
            public string _id { get; set; }
            public int processInterviewId { get; set; }
            public int id { get; set; }
            public int canId { get; set; }
            public string canName { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string StarVote { get; set; }
            public string starVote
            {
                get
                {
                    return StarVote;
                }
                set
                {
                    StarVote = value;
                    SetStart();
                }
            }
            private void SetStart()
            {
                star1 = SetStart(1);
                star2 = SetStart(2);
                star3 = SetStart(3);
                star4 = SetStart(4);
                star5 = SetStart(5);
            }
            private bool SetStart(int indexStar)
            {
                int star_count = Convert.ToInt32(starVote);
                if (indexStar <= star_count) return true;
                return false;
            }
            public bool star1 { get; set; }
            public bool star2 { get; set; }
            public bool star3 { get; set; }
            public bool star4 { get; set; }
            public bool star5 { get; set; }
            public int recruitmentNewsId { get; set; }
            public string title { get; set; }
            public int userHiring { get; set; }
            public string nameHr { get; set; }
            public string name { get; set; }
            public string hrName { get; set; }


        }
        public class CandidateGetJob
        {
            public string _id { get; set; }
            public int canId { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public int gender { get; set; }
            public string birthday { get; set; }
            public string hometown { get; set; }
            public int education { get; set; }
            public string school { get; set; }
            public int exp { get; set; }
            public int isMarried { get; set; }
            public string address { get; set; }
            public string cvFrom { get; set; }
            public int userRecommend { get; set; }
            public string StarVote { get; set; }
            public string star_vote
            {
                get
                {
                    return StarVote;
                }
                set
                {
                    StarVote = value;
                    SetStart();
                }
            }
            private void SetStart()
            {
                star1 = SetStart(1);
                star2 = SetStart(2);
                star3 = SetStart(3);
                star4 = SetStart(4);
                star5 = SetStart(5);
            }
            private bool SetStart(int indexStar)
            {
                int star_count = Convert.ToInt32(StarVote);
                if (indexStar <= star_count) return true;
                return false;
            }
            public bool star1 { get; set; }
            public bool star2 { get; set; }
            public bool star3 { get; set; }
            public bool star4 { get; set; }
            public bool star5 { get; set; }
            public int recruitmentNewsId { get; set; }
            public string new_title { get; set; }
            public int user_hiring { get; set; }
            public string hrName { get; set; }
        }
        public class CandidateCancelJob
        {
            public string _id { get; set; }
            public int canId { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public int gender { get; set; }
            public string birthday { get; set; }
            public string hometown { get; set; }
            public int education { get; set; }
            public string school { get; set; }
            public int exp { get; set; }
            public int isMarried { get; set; }
            public string address { get; set; }
            public string cvFrom { get; set; }
            public int userRecommend { get; set; }
            public int star_vote { get; set; }
            public int recruitmentNewsId { get; set; }
            public string new_title { get; set; }
            public int user_hiring { get; set; }
            public string hrName { get; set; }
        }

        public class CandidateContactJob
        {
            public string _id { get; set; }
            public int canId { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public int gender { get; set; }
            public string birthday { get; set; }
            public string hometown { get; set; }
            public int education { get; set; }
            public string school { get; set; }
            public int exp { get; set; }
            public int isMarried { get; set; }
            public string address { get; set; }
            public string cvFrom { get; set; }
            public int userRecommend { get; set; }
            public int star_vote { get; set; }
            public int recruitmentNewsId { get; set; }
            public string new_title { get; set; }
            public int user_hiring { get; set; }
            public string hrName { get; set; }
        }

        public class CandidateFailJob
        {
            public string _id { get; set; }
            public int canId { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public int gender { get; set; }
            public string birthday { get; set; }
            public string hometown { get; set; }
            public int education { get; set; }
            public string school { get; set; }
            public int exp { get; set; }
            public int isMarried { get; set; }
            public string address { get; set; }
            public string cvFrom { get; set; }
            public int userRecommend { get; set; }
            public int star_vote { get; set; }
            public int recruitmentNewsId { get; set; }
            public string new_title { get; set; }
            public int user_hiring { get; set; }
            public string hrName { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }
    }
}
