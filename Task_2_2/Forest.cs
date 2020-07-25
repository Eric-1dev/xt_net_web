using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_2
{
    // Класс леса
    class Forest : AbstractGameObject
    {
        public Forest(char c)
        {
            char[,] view = new char[_rnd.Next(2, 10), _rnd.Next(2, 10)];
            for (int i = 0; i < view.GetLength(1); i++)
                for (int j = 0; j < view.GetLength(0); j++)
                    view[j, i] = c;
            CurView = view;
        }
        public Forest() : this('^') { }
    }
}
