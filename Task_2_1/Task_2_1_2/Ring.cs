using Eric.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_1_2
{
    // Кольцо
    class Ring : Figure
    {
        Round innerRound;
        Round outerRound;

        public Ring(int x, int y, int radius, int thickness) : base(x, y)
        {
            this.name = "Кольцо";
            if (radius - thickness <= 0)
                throw new ArgumentOutOfRangeException("thickness", "Thickness must be less than Radius");
            innerRound = new Round(x, y, radius - thickness);
            outerRound = new Round(x, y, radius);
        }
        public double GetArea() => outerRound.GetArea() - innerRound.GetArea();
        public double GetOuterCircumference => outerRound.GetCircumference();
        public double GetInnerCircumference => innerRound.GetCircumference();
        public override MyString Draw()
        {
            return this.GetBaseInfo() + $"Внешний радиус {outerRound.Radius}, внутренний радиус {innerRound.Radius}, площадь {GetArea()}";
        }
    }
}
