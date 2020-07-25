using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3_3_3
{
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
