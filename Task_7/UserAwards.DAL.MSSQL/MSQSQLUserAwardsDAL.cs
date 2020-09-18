using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAwards.DAL.Interfaces;
using UserAwards.Entities;

namespace UserAwards.DAL.MSSQL
{
    public class MSQSQLUserAwardsDAL : IUserAwardsDAL
    {
        public bool DeleteAwardById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteLinkById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteLinkById(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Award> GetAllAwards()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Link> GetAllLinks()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Award GetAwardById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Award GetAwardByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Award> GetAwardsByUserId(Guid Id)
        {
            throw new NotImplementedException();
        }

        public string[] GetRolesForUser(string name)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsersByAwardId(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void InsertAward(Award award)
        {
            throw new NotImplementedException();
        }

        public void InsertLink(Link link)
        {
            throw new NotImplementedException();
        }

        public void InsertUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool IsAccountExist(string name, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsUserInRole(string name, string role)
        {
            throw new NotImplementedException();
        }

        public bool SetUserPassword(Guid id, string password)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAward(Award award)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
