using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAwards.Entities;

namespace UserAwards.Common
{
    public interface IUserAwardsDAL
    {
        bool AddUser(User user);
        bool AddAward(Award award);
        bool AddLink(Link link);
        User GetUserById(Guid id);
        Award GetAwardById(Guid id);
        Link GetLinkById(Guid id);
        bool UpdateUser(User user);
        bool UpdateAward(Award award);
        bool UpdateLink(Link link);
        bool DeleteUserById(Guid id);
        bool DeleteAwardById(Guid id);
        bool DeleteLinkById(Guid id);
        IEnumerable<User> GetAllUsers();
        IEnumerable<Award> GetAllAwards();
        IEnumerable<Link> GetAllLinks();
    }
}
