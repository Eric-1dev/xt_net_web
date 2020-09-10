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
        public void AddAward(Award award)
        {
            throw new NotImplementedException();
        }

        public void AddAwardToUser(Guid userId, Guid awardId)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void ChangeAwardById(Guid id, Award award)
        {
            throw new NotImplementedException();
        }

        public void ChangeUserById(Guid id, User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Award> GetAllAwards()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers() => BLL.GetAllUsers();

        public IEnumerable<Award> GetAwardsByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsersByAward(Guid awardId)
        {
            throw new NotImplementedException();
        }

        public void RemoveAwardById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void RemoveAwardFromUser(Guid userId, Guid awardId)
        {
            throw new NotImplementedException();
        }

        public void RemoveUserById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}