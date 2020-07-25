using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_1_2
{
    // Круг
    class Round : Circle
    {
        public Round(int x, int y, int radius) : base(x, y, radius)
        {
            this.name = "Круг";
        }
        public double GetArea() => Math.PI * radius * radius;
    }
}
