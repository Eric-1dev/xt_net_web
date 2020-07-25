using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_2
{
    class Player : AbstractCharacter
    {
        private readonly int _max_hp;
        private int _hp;
        public int HP
        {
            get => _hp;
            internal set
            {
                if (_hp != value) // Отрисовываем полоску HP только при изменении
                {
                    _hp = value;
                    if (_hp > _max_hp)
                        _hp = _max_hp;
                    UpdateHPBar();
                }
            }
        }
        public Player(int max_hp)
        {
            View1 = new char[,] { { 'b','_','d' },
                                   { ')','╨','(' },
                                   { '┌','-','┌' } };

            View2 = new char[,] { { 'b','_','d' },
                                   { ')','╨','(' },
                                   { '┐','-','┐' } };
            HP = _max_hp = max_hp;

            CurView = View1;
        }
        private void UpdateHPBar()
        {
            Game.mutexObj.WaitOne();
            string hpBar = " HP: " + new string('|', HP) + " ";
            Console.SetCursorPosition(2, 0);
            if (HP > (2 * _max_hp) / 3)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (HP > _max_hp / 3 && HP <= (2 * _max_hp) / 3)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(hpBar);
            Game.mutexObj.ReleaseMutex();
        }

        // Лечим
        public void Heal(int count)
        {
            HP += count;
        }

        // Дамажим
        public void Damage()
        {
            HP--;
            if (HP < 1)
            {
                HP = 0;
                Game.Stop(false);
            }
        }

        internal override bool CrossingAction(AbstractGameObject secondObj, ref int old_x, ref int old_y)
        {
            if (secondObj is Enemy) // Пересечение игрока с врагом
            {
                StayBack(true, ref old_x, ref old_y);
                FitToEdge();
                Damage();
                return true;
            }
            if (secondObj is Medkit) // Пересечение игрока с аптечкой
            {
                Heal((secondObj as Medkit).HealCount);
                secondObj.Remove();
                //i--;
                return true;
            }
            if (secondObj is Money)
            {
                Game.Stop(true);
                return true;
            }
            return false;
        }
    }
}
