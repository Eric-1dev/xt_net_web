using Eric.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_1_2
{
    // Прямоугольник
    class Rectangle : Figure
    {
        protected int width, length;
        public Rectangle(int x, int y, int width, int length) : base(x, y)
        {
            this.name = "Прямоугольник";
            this.width = NotPositiveExeption(ref width);
            this.length = NotPositiveExeption(ref length);
        }

        public int Width { get => width; }

        public int Length { get => length; }

        public int GetArea() => width * length;
        public int GetPerimeter() => 2 * (width + length);
        public override MyString Draw()
        {
            return this.GetBaseInfo() + $"Длина {Length}, ширина {Width}";
        }
    }
}
