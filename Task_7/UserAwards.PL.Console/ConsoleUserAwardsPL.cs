﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAwards.BLL.DR;
using UserAwards.BLL.Interfaces;
using UserAwards.Entities;
using UserAwards.PL.Interfaces;

namespace UserAwards.PL.Console
{
    class ConsoleUserAwardsPL : IUserAwardsPL
    {
        private readonly IUserAwardsBLL BLL = UserAwardsBLLDR.UserAwardsBLL;
        public void AddAward(Award award) => BLL.AddAward(award);

        public void AddAwardToUser(Guid userId, Guid awardId) => BLL.AddAwardToUser(userId, awardId);

        public void AddUser(User user) => BLL.AddUser(user);

        public void RemoveAwardById(Guid id) => BLL.RemoveAwardById(id);

        public void RemoveAwardFromUser(Guid userId, Guid awardId) => BLL.RemoveAwardFromUser(userId, awardId);

        public void RemoveUserById(Guid id) => BLL.RemoveUserById(id);

        public IEnumerable<Award> GetAllAwards() => BLL.GetAllAwards();

        public IEnumerable<User> GetAllUsers() => BLL.GetAllUsers();

        public IEnumerable<User> GetUsersByAward(Guid awardId) => BLL.GetUsersByAward(awardId);

        public IEnumerable<Award> GetAwardsByUser(Guid userId) => BLL.GetAwardsByUser(userId);

        public void ShowAllUsers()
        {
            System.Console.WriteLine("All users:");
            var users = GetAllUsers();
            foreach (var user in users)
            {
                var awards = GetAwardsByUser(user.Id);

                System.Console.WriteLine($"Name: {user.Name} \t\t Age: {user.Age} \t Date of birth: {user.DateOfBirth}");

                System.Console.Write($"\tAwards of {user.Name}: [");
                foreach (var award in awards)
                    System.Console.Write(award.Title + " ");
                System.Console.WriteLine("]");
            }
            System.Console.WriteLine();
        }
        public void ShowAllAwards()
        {
            System.Console.WriteLine("All awards:");
            var awards = GetAllAwards();
            foreach (var award in awards)
            {
                var users = GetUsersByAward(award.Id);

                System.Console.WriteLine($"Name: {award.Title}");

                System.Console.Write($"\tUsers who has award {award.Title}: [");
                foreach (var user in users)
                    System.Console.Write(user.Name + " ");
                System.Console.WriteLine("]");
            }
            System.Console.WriteLine();
        }

        public void ChangeUserById(Guid id, User user) => BLL.UpdateUserById(id, user);

        public void ChangeAwardById(Guid id, Award award) => BLL.UpdateAwardById(id, award);

        public User GetUserById(Guid id) => BLL.GetUserById(id);

        public Award GetAwardByID(Guid id) => BLL.GetAwardById(id);
    }
}
