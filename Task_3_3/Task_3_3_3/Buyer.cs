using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3_3_3
{
    class Buyer
    {
        private Order _order;
        public string Name { get; private set; }
        public Buyer(string name)
        {
            Name = name;
        }
        public void MakeOrder(Restaurant restaurant, PizzaType type)
        {
            Notify.ConsoleNotify($"{Name}: I want {type}");
            _order = new Order(Name, type);
            _order.OnReady += TakePizza;
            restaurant.CreateOrder(_order);
            _order.Create();
        }

        public void TakePizza(Order order)
        {
            Notify.ConsoleNotify($"{Name}: I take it!");
            order.OnReady -= TakePizza;
        }
    }
}
