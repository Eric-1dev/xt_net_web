using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAwards.Entities;

namespace UserAwards.PL.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var userAwards = new ConsoleUserAwardsPL();

            User user1 = new User("Alex", new DateTime(1988, 06, 03), 32);
            User user2 = new User("Eric", new DateTime(1997, 09, 23), 23);
            User user3 = new User("Ivan", new DateTime(1933, 02, 08), 76);

            Award award1 = new Award("Rage");

            var users = userAwards.GetAllUsers();
            var awards = userAwards.GetAllAwards();

            //userAwards.AddAward(award1);
            /*userAwards.AddUser(user1);
            userAwards.AddUser(user2);

            userAwards.ShowAllUsers();
            System.Console.WriteLine();

            var users = userAwards.GetAllUsers();
            //userAwards.RemoveUserById(users.FirstOrDefault().Id);
            userAwards.ChangeUserById(users.FirstOrDefault().Id, user3);*/

            //userAwards.AddAwardToUser(users.FirstOrDefault().Id, awards.LastOrDefault().Id);

            //userAwards.RemoveUserById(users.FirstOrDefault().Id);
            userAwards.RemoveAwardFromUser(users.FirstOrDefault().Id, awards.FirstOrDefault().Id);

            userAwards.ShowAllUsers();

            userAwards.ShowAllAwards();
        }
    }
}
