using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_3_3_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Restaurant pizzahouse = new Restaurant();

            Buyer buyer1 = new Buyer("Eric");
            Buyer buyer2 = new Buyer("John");
            Buyer buyer3 = new Buyer("Joan");
            Buyer buyer4 = new Buyer("Liza");

            buyer1.MakeOrder(pizzahouse, PizzaType.Americana); Console.WriteLine();
            buyer2.MakeOrder(pizzahouse, PizzaType.Carbonara); Console.WriteLine();
            buyer3.MakeOrder(pizzahouse, PizzaType.Margherita); Console.WriteLine();
            buyer4.MakeOrder(pizzahouse, PizzaType.Napoletana); Console.WriteLine();
        }
    }
}
