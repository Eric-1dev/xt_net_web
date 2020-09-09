using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAwards.BLL.DR;
using UserAwards.Entities;
using UserAwards.PL.Interfaces;

namespace UserAwards.PL.Console
{
    class ConsoleUserAwardsPL : IUserAwardsPL
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

        public IEnumerable<Award> ShowAllAwards()
        {
            throw new NotImplementedException();
        }

        public void ShowAllUsers()
        {
            var users = UserAwardsBLLDR.UserAwardsBLL.GetAllUsers();

            foreach (var user in users)
            {
                System.Console.WriteLine($"Name: {user.Name} \t\t Age: {user.Age} \t Date of birth: {user.DateOfBirth}");
            }
        }
    }
}
