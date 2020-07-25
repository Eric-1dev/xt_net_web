using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3_1_1
{
    public class Human
    {
        private static int _numbers = 1;
        public int Number { get; private set; }
        public string Name { get; private set; }
        public Human(string name)
        {
            Name = name;
            Number = _numbers++;
        }
        public override bool Equals(object obj)
        {
            return Name == (obj as Human).Name && Number == (obj as Human).Number;
        }
    }
}
