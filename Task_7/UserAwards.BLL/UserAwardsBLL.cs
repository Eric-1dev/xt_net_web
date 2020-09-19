using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UserAwards.BLL.Interfaces;
using UserAwards.DAL.DR;
using UserAwards.DAL.Interfaces;
using UserAwards.Entities;

namespace UserAwards.BLL
{
    public class UserAwardsBLL : IUserAwardsBLL
    {
        private readonly IUserAwardsDAL DAL = UserAwardsDALDR.UserAwardsDAL;
        public AwardCheckStatus AddAward(Award award)
        {
            var checkResult = AwardCorrectionCheck(award);
            if (checkResult == AwardCheckStatus.CORRECT)
                DAL.InsertAward(award);
            return checkResult;
        }

        public bool AddAwardToUser(Guid userId, Guid awardId)
        {
            var users = GetAllUsers();
            var awards = GetAllAwards();
            var links = DAL.GetAllLinks();

            if (!users.Where(user => user.Id == userId).Any())
                return false;

            if (!awards.Where(award => award.Id == awardId).Any())
                return false;

            if (links.Where(link => link.UserId == userId && link.AwardId == awardId).Any())
                return false;

            var newlink = new Link(userId, awardId);
            DAL.InsertLink(newlink);

            return true;
        }

        public UserCheckStatus AddUser(User user)
        {
            var checkResult = UserCorrectionCheck(user);
            if (checkResult == UserCheckStatus.CORRECT)
                DAL.InsertUser(user);
            return checkResult;
        }

        public IEnumerable<Award> GetAllAwards() => DAL.GetAllAwards();

        public IEnumerable<User> GetAllUsers() => DAL.GetAllUsers();

        public Award GetAwardById(Guid id) => DAL.GetAwardById(id);

        public bool SetUserPassword(Guid id, string password) => DAL.SetUserPassword(id, password);

        public IEnumerable<Award> GetAwardsByUserId(Guid userId) => DAL.GetAwardsByUserId(userId);

        public User GetUserById(Guid id) => DAL.GetUserById(id);
        public User GetUserByName(string name) => DAL.GetUserByName(name);

        public IEnumerable<User> GetUsersByAwardId(Guid awardId) => DAL.GetUsersByAwardId(awardId);

        public void RemoveAwardById(Guid id)
        {
            var linksIdToDelete = DAL.GetAllLinks().Where(link => link.AwardId == id).Select(link => link.Id);
            
            DAL.DeleteLinkById(linksIdToDelete);
            DAL.DeleteAwardById(id);
        }

        public void RemoveAwardFromUser(Guid userId, Guid awardId)
        {
            var links = DAL.GetAllLinks();

            var linkToDelete = links.Where(link => link.UserId == userId && link.AwardId == awardId);

            if (linkToDelete != null)
                DAL.DeleteLinkById(linkToDelete.FirstOrDefault().Id);
        }

        public void RemoveUserById(Guid id)
        {
            var linksIdToDelete = DAL.GetAllLinks().Where(link => link.UserId == id).Select(link => link.Id);

            DAL.DeleteLinkById(linksIdToDelete);
            DAL.DeleteUserById(id);
        }

        public bool UpdateAwardById(Guid id, Award award)
        {
            var awards = GetAllAwards();

            if (!awards.Where(awrd => awrd.Id == id).Any())
                return false;

            award.Id = id;
            DAL.UpdateAward(award);

            return true;
        }

        public bool UpdateUserById(Guid id, User user)
        {
            var user_old = GetUserById(id);

            if (user_old == null)
                return false;

            user.Id = id;
            user.Password = user_old.Password;
            user.IsAdmin = user_old.IsAdmin;

            DAL.UpdateUser(user);

            return true;
        }

        public UserCheckStatus UserCorrectionCheck(User user)
        {
            string nameCheck = @"^[a-zA-Z0-9_\-]{3,20}$";
            if (!Regex.IsMatch(user.Name, nameCheck))
                return UserCheckStatus.INCORRECT_NAME;

            if (DAL.GetUserByName(user.Name) != null)
                return UserCheckStatus.ALLREADY_EXIST;

            if (user.Age > 150 || user.Age < 0)
                return UserCheckStatus.INCORRECT_AGE;

            return UserCheckStatus.CORRECT;
        }

        public AwardCheckStatus AwardCorrectionCheck(Award award)
        {
            string titleCheck = @"[a-zA-Zа-яА-ЯёЁ0-9_\-\s]{3,30}";
            if (!Regex.IsMatch(award.Title, titleCheck))
                return AwardCheckStatus.INCORRECT_TITLE;

            if (DAL.GetAwardByTitle(award.Title) != null)
                return AwardCheckStatus.ALLREADY_EXIST;

            return AwardCheckStatus.CORRECT;
        }

        public bool IsAccountExist(string name, string password) => DAL.IsAccountExist(name, password);

        public string[] GetRolesForUser(string name) => DAL.GetRolesForUser(name);

        public bool IsUserInRole(string name, string role) => DAL.IsUserInRole(name, role);
    }
}
