using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAwards.Entities;

namespace UserAwards.PL.Interfaces
{
    public interface IUserAwardsPL
    {
        void AddUser(User user);
        void RemoveUserById(Guid id);
        void ChangeUserById(Guid id, User user);
        User GetUserById(Guid id);
        void SetUserPassword(Guid id, string password);
        void AddAward(Award award);
        void RemoveAwardById(Guid id);
        void ChangeAwardById(Guid id, Award award);
        Award GetAwardById(Guid id);
        void AddAwardToUser(Guid userId, Guid awardId);
        void RemoveAwardFromUser(Guid userId, Guid awardId);
        IEnumerable<User> GetAllUsers();
        IEnumerable<Award> GetAllAwards();
        IEnumerable<User> GetUsersByAwardId(Guid awardId);
        IEnumerable<Award> GetAwardsByUserId(Guid userId);
        UserCheckStatus UserCorrectionCheck(User user);
        AwardCheckStatus AwardCorrectionCheck(Award award);
        bool IsAccountExist(string name, string password);
    }
}
