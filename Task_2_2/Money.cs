using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_2
{
    // Деньги - цель игры
    class Money : AbstractGameObject
    {
        public Money()
        {
            CurView = new char[,] { { '┌', '─', '┐' },
                                    { '│', '$', '│' },
                                    { '└', '─', '┘' } };
        }
    }
}
