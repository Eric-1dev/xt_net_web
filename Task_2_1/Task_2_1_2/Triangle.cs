using Eric.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_1_2
{
    // Треугольник
    class Triangle : Figure
    {
        int a, b, c;
        public Triangle(int x, int y, int a, int b, int c) : base(x, y)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
        public int A { get => a; }
        public int B { get => b; }
        public int C { get => c; }
        public override MyString Draw()
        {
            return this.GetBaseInfo() + $"Длины сторон {A} {B} {C}";
        }
    }
}
