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
            Console.WriteLine("Restaurant: Please wait. Processing...");
            Thread.Sleep(3000);
            Console.WriteLine($"Restaurant: {order.BuyerName}, your {order.Type} pizza is ready");
            orders.Remove(order);
            order.OnCreate -= MakePizza;
            order.Ready();
        }
    }

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
            Console.WriteLine($"{Name}: I want {type}");
            _order = new Order(Name, type);
            _order.OnReady += TakePizza;
            restaurant.CreateOrder(_order);
            _order.Create();
        }

        public void TakePizza(Order order)
        {
            Console.WriteLine($"{Name}: I take it!");
            order.OnReady -= TakePizza;
        }
    }

    class Order
    {
        public static uint OrderNumbers = 1;
        public event Action<Order> OnCreate = delegate { };
        public event Action<Order> OnReady = delegate { };
        public string BuyerName { get; private set; }
        public uint Number { get; private set; }
        public PizzaType Type { get; private set; }
        public Order(string name, PizzaType type)
        {
            Number = OrderNumbers;
            BuyerName = name;
            Type = type;
            OrderNumbers++;
        }
        public void Create()
        {
            OnCreate(this);
        }
        public void Ready()
        {
            OnReady(this);
        }
    }

    public enum PizzaType
    {
        Margherita,
        Carbonara,
        Napoletana,
        Americana
    }
}
