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
                        int x, y;
                        int radius;
                        int thickness;
                        int width;
                        int length;
                        int a, b, c;

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
                                int x2, y2;
                                BaseCoordsTpl();
                                if (EnterTwoValues(out x, out y))
                                {
                                    Console.WriteLine("Веведите координаты конца отрезка через пробел");
                                    if (EnterTwoValues(out x2, out y2))
                                        figures.Add(new Line(x, y, x2, y2));
                                }
                                break;
                            case (byte)Figures.CIRCLE:
                                BaseCoordsTpl();
                                if (EnterTwoValues(out x, out y))
                                {
                                    Console.WriteLine("Веведите радиус окружности");
                                    if (EnterOneValuePos(out radius))
                                        figures.Add(new Circle(x, y, radius));
                                }
                                break;
                            case (byte)Figures.ROUND:
                                BaseCoordsTpl();
                                if (EnterTwoValues(out x, out y))
                                {
                                    Console.WriteLine("Веведите радиус круга");
                                    if (EnterOneValuePos(out radius))
                                        figures.Add(new Round(x, y, radius));
                                }
                                break;
                            case (byte)Figures.RING:
                                BaseCoordsTpl();
                                if (EnterTwoValues(out x, out y))
                                {
                                    Console.WriteLine("Веведите радиус кольца и его толщину через пробел");
                                    if (EnterTwoValuesPos(out radius, out thickness))
                                        figures.Add(new Ring(x, y, radius, thickness));
                                }
                                break;
                            case (byte)Figures.RECTANGLE:
                                BaseCoordsTpl();
                                if (EnterTwoValues(out x, out y))
                                {
                                    Console.WriteLine("Веведите длину и ширину прямоугольника через пробел");
                                    if (EnterTwoValuesPos(out length, out width))
                                        figures.Add(new Rectangle(x, y, width, length));
                                }
                                break;
                            case (byte)Figures.SQUARE:
                                BaseCoordsTpl();
                                if (EnterTwoValues(out x, out y))
                                {
                                    Console.WriteLine("Веведите длину стороны квадрата");
                                    if (EnterOneValuePos(out width))
                                        figures.Add(new Square(x, y, width));
                                }
                                break;
                            case (byte)Figures.TRIANGLE:
                                BaseCoordsTpl();
                                if (EnterTwoValues(out x, out y))
                                {
                                    Console.WriteLine("Веведите длину стороны квадрата");
                                    if (EnterOneValuePos(out width))
                                        figures.Add(new Triangle(x, y, a, b, c));
                                }
                                break;
                            default:
                                IncorrectAlert();
                                break;
                        }

                        break;
                    case (byte)Actions.DRAW:
                        Console.WriteLine("Список нарисованных фигур:");
                        figures.ForEach(action => Console.WriteLine(action.Draw()));
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
                        IncorrectAlert();
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

        public static bool EnterValues(ref int val1, ref int val2)
        {
            string[] str = Console.ReadLine().Trim().Split(' ');
            if ( str.Length == values.Length )
        }
        public static bool EnterTwoValues(out int val1, out int val2)
        {
            val1 = val2 = 0;
            string[] str = Console.ReadLine().Trim().Split(' ');
            if (str.Length == 2)
            {
                int.TryParse(str[0], out val1);
                int.TryParse(str[1], out val2);
                return true;
            }
            IncorrectAlert();
            return false;
        }
        public static bool EnterThreeValuesPos(out int val1, out int val2, out int val3)
        {
            if (EnterTwoValues(out val1, out val2))
                if (val1 > 0 && val2 > 0)
                    return true;
            IncorrectAlert();
            return false;
        }
        public static bool EnterTwoValuesPos(out int val1, out int val2)
        {
            if (EnterTwoValues(out val1, out val2))
                if (val1 > 0 && val2 > 0)
                    return true;
            IncorrectAlert();
            return false;
        }
        public static bool EnterOneValuePos(out int val)
        {
            int unused = 1; // Неиспользуемая переменная, позволяющаяя получить true, при правильном вводе val
            if (EnterTwoValuesPos(out val, out unused))
                return true;
            return false;
        }
        public static void BaseCoordsTpl()
        {
            Console.WriteLine("Введите координаты начала отсчета через пробел:");
        }
        public static void IncorrectAlert()
        {
            Console.WriteLine("Некорректный ввод");
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
