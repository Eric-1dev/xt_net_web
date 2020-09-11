using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Policy;
using System.Text;
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
        public void AddAward(Award award) => DAL.InsertAward(award);

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

        public void AddUser(User user) => DAL.InsertUser(user);

        public IEnumerable<Award> GetAllAwards() => DAL.GetAllAwards();

        public IEnumerable<User> GetAllUsers() => DAL.GetAllUsers();

        public Award GetAwardById(Guid id) => DAL.GetAwardById(id);

        public IEnumerable<Award> GetAwardsByUser(Guid userId)
        {
            var awardsId = DAL.GetAllLinks().Where(link => link.UserId == userId).Select(link => link.AwardId);
            var awards = GetAllAwards();

            return awards.Where(user => awardsId.Contains(user.Id));
        }

        public User GetUserById(Guid id) => DAL.GetUserById(id);

        public IEnumerable<User> GetUsersByAward(Guid awardId)
        {
            var usersId = DAL.GetAllLinks().Where(link => link.AwardId == awardId).Select(link => link.UserId);
            var users = GetAllUsers();

            return users.Where(user => usersId.Contains(user.Id));
        }

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
            var users = GetAllUsers();

            if (!users.Where(usr => usr.Id == id).Any())
                return false;

            user.Id = id;
            DAL.UpdateUser(user);

            return true;
        }
    }
}
