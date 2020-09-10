﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAwards.Entities
{
    public class User : IHasId
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public int Age { get; private set; }

        private User()
        {
            Id = Guid.NewGuid();
        }

        public User(string name, DateTime dateOfBirth, int age) : this()
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Age = age;
        }
    }
}
