using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAwards.Entities;

namespace UserAwards.DAL.Interfaces
{
    public interface IUserAwardsDAL
    {
        void InsertUser(User user);
        void InsertAward(Award award);
        void InsertLink(Link link);
        User GetUserById(Guid id);
        User GetUserByName(string name);
        Award GetAwardById(Guid id);
        Award GetAwardByTitle(string title);
        bool UpdateUser(User user);
        bool UpdateAward(Award award);
        bool DeleteUserById(Guid id);
        bool DeleteAwardById(Guid id);
        bool DeleteLinkById(Guid id);
        bool DeleteLinkById(IEnumerable<Guid> ids);
        IEnumerable<User> GetAllUsers();
        IEnumerable<Award> GetAllAwards();
        IEnumerable<Link> GetAllLinks();
        IEnumerable<Award> GetAwardsByUserId(Guid Id);
        IEnumerable<User> GetUsersByAwardId(Guid Id);
        bool IsAccountExist(string name, string password);
        string[] GetRolesForUser(string name);
        bool IsUserInRole(string name, string role);
        bool SetUserPassword(Guid id, string password);
    }
}
