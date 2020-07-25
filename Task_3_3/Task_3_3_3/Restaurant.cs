using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_3_3_3
{
    class Restaurant
    {
        public LinkedList<Order> orders = new LinkedList<Order>();

        public void CreateOrder(Order order)
        {
            orders.AddLast(order);
            order.OnCreate += MakePizza;
        }

        public void MakePizza(Order order)
        {
            Notify.ConsoleNotify("Restaurant: Please wait. Processing...");
            Thread.Sleep(3000);
            Notify.ConsoleNotify($"Restaurant: {order.BuyerName}, your {order.Type} pizza is ready");
            orders.Remove(order);
            order.OnCreate -= MakePizza;
            order.Ready();
        }
    }
}
