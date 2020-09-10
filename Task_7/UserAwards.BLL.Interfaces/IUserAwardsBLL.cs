using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAwards.Entities;

namespace UserAwards.BLL.Interfaces
{
    public interface IUserAwardsBLL
    {
        void AddUser(User user);
        void RemoveUserById(Guid id);
        bool UpdateUserById(Guid id, User user);
        void AddAward(Award award);
        void RemoveAwardById(Guid id);
        bool UpdateAwardById(Guid id, Award award);
        bool AddAwardToUser(Guid userId, Guid awardId);
        void RemoveAwardFromUser(Guid userId, Guid awardId);
        IEnumerable<User> GetAllUsers();
        IEnumerable<Award> GetAllAwards();
        IEnumerable<User> GetUsersByAward(Guid awardId);
        IEnumerable<Award> GetAwardsByUser(Guid userId);
    }
}
