using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAwards.BLL.Interfaces;
using UserAwards.Entities;

namespace UserAwards.BLL
{
    public class UserAwardsBLL : IUserAwardsBLL
    {
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

        public IEnumerable<Award> GetAllAwards()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

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
