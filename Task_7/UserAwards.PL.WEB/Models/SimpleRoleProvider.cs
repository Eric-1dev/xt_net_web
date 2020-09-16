using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using UserAwards.BLL.DR;
using UserAwards.BLL.Interfaces;

namespace UserAwards.PL.WEB.Models
{
    public class SimpleRoleProvider : RoleProvider
    {
        private readonly IUserAwardsBLL BLL = UserAwardsBLLDR.UserAwardsBLL;
        public override string[] GetRolesForUser(string username) => BLL.GetRolesForUser(username);

        public override bool IsUserInRole(string username, string roleName) => BLL.IsUserInRole(username, roleName);

        #region ANUNAFIK
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}