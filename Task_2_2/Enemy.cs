using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_2
{
    class Enemy : AbstractCharacter
    {
        public Enemy()
        {
            View1 = new char[,] { { '/', '-', '\\' },
                                  { '|', '|',  '|' },
                                  { '\\', '_', '/' } };
            View2 = new char[,] { { '/', '-', '\\' },
                                  { '|', '-',  '|' },
                                  { '\\', '_', '/' } };
            CurView = View1;
        }

        internal override bool CrossingAction(AbstractGameObject secondObj, ref int old_x, ref int old_y)
        {
            if (secondObj is Player) // Пересечение врага с игроком
            {
                (secondObj as Player).Clear();
                (secondObj as Player).StayBack(false, ref old_x, ref old_y);
                (secondObj as Player).FitToEdge();
                (secondObj as Player).Draw();
                (secondObj as Player).Damage();
                return true;
            }
            return false;
        }
    }
}
