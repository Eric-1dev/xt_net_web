using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eric.String;

namespace Task_2_1_2
{
    /*
     * Базовый класс Figure.
     * Содержит координаты начала отсчета, имя фигуры,
     * метод, возвращающий строку с базовой информацией
     * и абстрактный метод Draw(), отвечающий за отрисовку фигуры.
     */
    abstract class Figure
    {
        protected MyString name;
        protected int x, y;
        public Figure(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        abstract public MyString Draw(); // Сделаем абстрактным, чтобы понимать, что метод должен быть у каждой фигуры и не забыть его реализовать в потомках
        public int X { get => x; }
        public int Y { get => y; }
        public MyString Name { get => name; }

        protected int NotPositiveExeption(ref int x) // Проверяем в backend'е, если проггер накосячит в UI. Наказане - эксепшн
        {
            if (x <= 0)
                throw new ArgumentOutOfRangeException("Value", "Value must be positive");
            return x;
        }

        protected MyString GetBaseInfo() => $"{Name}, базовые координаты: X={X} Y={Y}. Характеристики: ";
    }
}
