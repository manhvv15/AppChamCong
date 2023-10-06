using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.CaiDatLuong.CaiDatLuongCB
{
    public class UserFromServer
    {
        public UserFromServer()
        {
        }

        public UserFromServer(int id, int id365, int type365, string email, string password, string phone, string userName, string avatarUser, string status, int statusEmotion, DateTime lastActive, int active, int isOnline, int looker, int companyId, string companyName, string friendStatus = "none")
        {
            ID = id;
            Email = email;
            Password = password;
            Phone = phone;
            UserName = userName;
            AvatarUser = avatarUser;
            Status = status;
            Active = active;
            IsOnline = isOnline;
            Looker = looker;
            StatusEmotion = statusEmotion;
            LastActive = lastActive;
            CompanyId = companyId;
            CompanyName = companyName;
            ID365 = id365;
            Type365 = type365;
            FriendStatus = friendStatus;
        }

        public int ID { get; set; }
        public int ID365 { get; set; }
        public int Type365 { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string AvatarUser { get; set; }
        public string Status { get; set; }
        public int Active { get; set; }
        public int IsOnline { get; set; }
        public int Looker { get; set; }
        public int StatusEmotion { get; set; }
        public DateTime LastActive { get; set; }
        public int CompanyId { get; set; }
        public int NotificationPayoff { get; set; }
        public int NotificationCalendar { get; set; }
        public int NotificationReport { get; set; }
        public int NotificationOffer { get; set; }
        public int NotificationPersonnelChange { get; set; }
        public int NotificationRewardDiscipline { get; set; }
        public int NotificationNewPersonnel { get; set; }
        public int NotificationChangeProfile { get; set; }
        public int AcceptMessStranger { get; set; }
        public int NotificationTransferAsset { get; set; }
        public string CompanyName { get; set; }
        public int Type_Pass { get; set; }
        public string LinkAvatar { get; set; }
        public DateTime timeLastSeenerApp { get; set; }
        public string FriendStatus { get; set; }
        public int NotificationMissMessage { get; set; }
        public int NotificationCommentFromTimViec { get; set; }
        public int NotificationCommentFromRaoNhanh { get; set; }
        public int NotificationTag { get; set; }
        public int NotificationSendCandidate { get; set; }
        public int NotificationChangeSalary { get; set; }
        public int NotificationAllocationRecall { get; set; }
        public int NotificationAcceptOffer { get; set; }
        public int NotificationDecilineOffer { get; set; }
        public int NotificationNTDPoint { get; set; }
        public int NotificationNTDExpiredPin { get; set; }
        public int NotificationNTDExpiredRecruit { get; set; }
        public int IdTimViec { get; set; }
        public string FromWeb { get; set; }
        public int NotificationNTDApplying { get; set; }
        public string seenMessage { get; set; }
        public string description { get; set; }
    }
}
