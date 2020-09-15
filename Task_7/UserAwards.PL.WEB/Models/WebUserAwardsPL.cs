using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserAwards.BLL.DR;
using UserAwards.BLL.Interfaces;
using UserAwards.Entities;
using UserAwards.PL.Interfaces;

namespace UserAwards.PL.WEB.Modules
{
    public class WebUserAwardsPL : IUserAwardsPL
    {
        private readonly IUserAwardsBLL BLL = UserAwardsBLLDR.UserAwardsBLL;
        public void AddAward(Award award) => BLL.AddAward(award);

        public void AddAwardToUser(Guid userId, Guid awardId) => BLL.AddAwardToUser(userId, awardId);

        public void AddUser(User user) => BLL.AddUser(user);

        public void ChangeAwardById(Guid id, Award award) => BLL.UpdateAwardById(id, award);

        public void ChangeUserById(Guid id, User user) => BLL.UpdateUserById(id, user);

        public IEnumerable<Award> GetAllAwards() => BLL.GetAllAwards();

        public IEnumerable<User> GetAllUsers() => BLL.GetAllUsers();

        public Award GetAwardById(Guid id) => BLL.GetAwardById(id);

        public IEnumerable<Award> GetAwardsByUserId(Guid userId) => BLL.GetAwardsByUserId(userId);

        public User GetUserById(Guid id) => BLL.GetUserById(id);

        public IEnumerable<User> GetUsersByAwardId(Guid awardId) => BLL.GetUsersByAwardId(awardId);

        public void RemoveAwardById(Guid id) => BLL.RemoveAwardById(id);

        public void RemoveAwardFromUser(Guid userId, Guid awardId) => BLL.RemoveAwardFromUser(userId, awardId);

        public void RemoveUserById(Guid id) => BLL.RemoveUserById(id);
    }
}