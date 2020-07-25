using Eric.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_1_2
{
    // Квадрат
    class Square : Rectangle
    {
        public Square(int x, int y, int width) : base(x, y, width, width)
        {
            this.name = "Квадрат";
        }

        public override MyString Draw()
        {
            return this.GetBaseInfo() + $"Длина стороны {Width}";
        }
    }
}
