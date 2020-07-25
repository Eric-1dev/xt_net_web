using Eric.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_1_2
{
    // Окружность
    class Circle : Figure
    {
        protected int radius;
        public Circle(int x, int y, int radius) : base(x, y)
        {
            this.radius = NotPositiveExeption(ref radius);
            this.name = "Окружность";
        }
        public int Radius { get => radius; }
        public double GetCircumference() => 2 * Math.PI * radius;
        public override MyString Draw()
        {
            return this.GetBaseInfo() + $"Радиус {this.Radius}";
        }
    }
}
