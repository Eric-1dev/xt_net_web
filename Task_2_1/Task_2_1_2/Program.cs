using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Eric.String;

namespace Task_2_1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            byte key;
            List<Figure> figures = new List<Figure>();

            do
            {
                if (user == null)
                    GetName();
                Console.WriteLine($"Выберите действие, {user}:" +
                                  $"\n\t{(byte)Actions.ADD}. Добавить фигуру" +
                                  $"\n\t{(byte)Actions.DRAW}. Вывести фигуры" +
                                  $"\n\t{(byte)Actions.CLEAR}. Очистить холст" +
                                  $"\n\t{(byte)Actions.CHANGEUSR}. Сменить пользователя" +
                                  $"\n\t{(byte)Actions.EXIT}. Выход");
                byte.TryParse(Console.ReadKey().KeyChar.ToString(), out key);
                Console.WriteLine();

                switch (key)
                {
                    case (byte)Actions.ADD:
                        byte figCode;
                        Console.WriteLine("Выберите тип фигуры:" +
                                            $"\n\t\t{(byte)Figures.LINE}. Линия" +
                                            $"\n\t\t{(byte)Figures.CIRCLE}. Окружность" +
                                            $"\n\t\t{(byte)Figures.ROUND}. Круг" +
                                            $"\n\t\t{(byte)Figures.RING}. Кольцо" +
                                            $"\n\t\t{(byte)Figures.RECTANGLE}. Прямоугольник" +
                                            $"\n\t\t{(byte)Figures.SQUARE}. Квадрат" +
                                            $"\n\t\t{(byte)Figures.TRIANGLE}. Треугольник");
                        byte.TryParse(Console.ReadKey().KeyChar.ToString(), out figCode);
                        Console.WriteLine();
                        switch (figCode)
                        {
                            case (byte)Figures.LINE:
                                addLine(figures);
                                break;
                            default:
                                Console.WriteLine("Фигуры с таким номером не существует");
                                break;
                        }

                        break;
                    case (byte)Actions.DRAW:
                        Console.WriteLine("Список нарисованных фигур:");
                        figures.ForEach(x => Console.WriteLine(x.Draw()));
                        break;
                    case (byte)Actions.CLEAR:
                        figures.Clear();
                        Console.WriteLine("Холст очищен");
                        break;
                    case (byte)Actions.CHANGEUSR:
                        user = null;
                        Console.WriteLine("Смена пользователя");
                        break;
                    case (byte)Actions.EXIT:
                        break;
                    default:
                        Console.WriteLine("Неправильный ввод");
                        break;
                }
            }
            while (key != 5);

        }
        static void GetName()
        {
            Console.WriteLine("Представьтесь, пожалуйста");
            user = Console.ReadLine();
        }

        static MyString user;

        static void addLine(List<Figure> _figures)
        {
            _figures.Add(new Square(1, 1, 12));
        }
    }

    /*
     * Базовый класс Figure.
     * Содержит координаты начала отсчета, имя фигуры,
     * метод, возвращающий строку с базовой информацией
     * и абстрактный метод Draw(), отвечающий за отрисовку фигуры.
     */
    abstract class Figure
    {
        protected MyString name;
        protected int x, y;
        public Figure(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        abstract public MyString Draw(); // Сделаем абстрактным, чтобы понимать, что метод должен быть у каждой фигуры и не забыть его реализовать в потомках
        public int X { get => x; }
        public int Y { get => y; }
        public MyString Name { get => name; }

        protected MyString GetBaseInfo() => $"{Name}, базовые координаты: X={X} Y={Y}. Характеристики: ";
    }

    // Окружность
    class Circle : Figure
    {
        protected int radius;
        public Circle(int x, int y, int radius) : base(x, y)
        {
            this.radius = radius;
            this.name = "Окружность";
        }
        public int Radius { get => radius; }
        public double GetCircumference() => 2 * Math.PI * radius;
        public override MyString Draw()
        {
            return this.GetBaseInfo() + $"Радиус {this.Radius}";
        }
    }

    // Круг
    class Round : Circle
    {
        public Round(int x, int y, int radius) : base(x, y, radius)
        {
            this.name = "Круг";
        }
        public double GetArea() => Math.PI * radius * radius;
    }

    // Кольцо
    class Ring : Figure
    {
        Round innerRound;
        Round outerRound;

        public Ring (int x, int y, int radius, int thickness) : base(x, y)
        {
            this.name = "Кольцо";
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

    // Прямоугольник
    class Rectangle : Figure
    {
        protected int width, length;
        public Rectangle(int x, int y, int width, int length) : base(x, y)
        {
            this.name = "Прямоугольник";
            this.width = width;
            this.length = length;
        }

        public int Width { get => width; }

        public int Length { get => length; }

        public int GetArea() => width * length;
        public int GetPerimeter() => 2 * (width + length);
        public override MyString Draw()
        {
            return this.GetBaseInfo() + $"Длина {Length}, ширина {Width}";
        }
    }

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

    // Линия
    class Line : Figure
    {
        int x2, y2;
        public Line(int x1, int y1, int x2, int y2) : base(x1, y1)
        {
            this.x2 = x2;
            this.y2 = y2;
        }

        public int X2 { get => x2; }
        public int Y2 { get => Y2; }

        public double GetLength()
        {
            return Math.Sqrt((X2 - X) * (X2 - X) + (Y2 - Y) * (Y2 - Y));
        }
        public override MyString Draw()
        {
            return this.GetBaseInfo() + $"Координаты конца отрезка X2={X2}, Y2={Y2}, длина отрезка {GetLength()}";
        }
    }

    // Треугольник
    class Triangle : Figure
    {
        int a, b, c;
        public Triangle(int x, int y, int a, int b, int c) : base (x, y)
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

    enum Actions
    {
        ADD = 1,
        DRAW,
        CLEAR,
        CHANGEUSR,
        EXIT
    }

    enum Figures
    {
        LINE = 1,
        CIRCLE,
        ROUND,
        RING,
        RECTANGLE,
        SQUARE,
        TRIANGLE
    }
}
