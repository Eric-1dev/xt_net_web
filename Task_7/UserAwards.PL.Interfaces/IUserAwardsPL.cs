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
        void AddAward(Award award);
        void RemoveAwardById(Guid id);
        void AddAwardToUser(Guid userId, Guid awardId);
        void RemoveAwardFromUser(Guid userId, Guid awardId);
        IEnumerable<User> ShowAllUsers();
        IEnumerable<Award> ShowAllAwards();
    }
}
