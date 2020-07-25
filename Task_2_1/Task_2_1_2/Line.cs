using Eric.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_1_2
{
    // Линия
    class Line : Figure
    {
        int x2, y2;
        public Line(int x1, int y1, int x2, int y2) : base(x1, y1)
        {
            this.x2 = x2;
            this.y2 = y2;
            this.name = "Отрезок";
        }

        public int X2 { get => x2; }
        public int Y2 { get => y2; }

        public double GetLength()
        {
            return Math.Sqrt((X2 - X) * (X2 - X) + (Y2 - Y) * (Y2 - Y));
        }
        public override MyString Draw()
        {
            return this.GetBaseInfo() + $"Координаты конца отрезка X2={X2}, Y2={Y2}, длина отрезка {GetLength(),0:N3}";
        }
    }
}
