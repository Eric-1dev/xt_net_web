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
        UserCheckStatus AddUser(User user);
        void RemoveUserById(Guid id);
        void ChangeUserById(Guid id, User user);
        User GetUserById(Guid id);
        User GetUserByName(string name);
        bool SetUserPassword(Guid id, string password);
        AwardCheckStatus AddAward(Award award);
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
        string[] GetRolesForUser(string name);
        bool IsUserInRole(string name, string role);
    }
}
