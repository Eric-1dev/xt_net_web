using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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
                        int[] vals;
                        MyString str;

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
                                str = "Веведите координаты конца отрезка через пробел";
                                if (BaseCoordsTpl(out x, out y, str))
                                {
                                    vals = EnterValues();
                                    if (vals[0] == 2)
                                        figures.Add(new Line(x, y, vals[1], vals[2]));
                                    else
                                        IncorrectDataAlert();
                                }
                                break;
                            case (byte)Figures.CIRCLE:
                                str = "Веведите радиус окружности";
                                if (BaseCoordsTpl(out x, out y, str))
                                {
                                    vals = EnterValues();
                                    if (vals[0] == 1 && IsAllPositive(vals))
                                        figures.Add(new Circle(x, y, vals[1]));
                                    else
                                        IncorrectDataAlert();
                                }
                                break;
                            case (byte)Figures.ROUND:
                                str = "Веведите радиус круга";
                                if (BaseCoordsTpl(out x, out y, str))
                                {
                                    vals = EnterValues();
                                    if (vals[0] == 1 && IsAllPositive(vals))
                                        figures.Add(new Round(x, y, vals[1]));
                                    else
                                        IncorrectDataAlert();
                                }
                                break;
                            case (byte)Figures.RING:
                                str = "Веведите радиус кольца и его толщину через пробел";
                                if (BaseCoordsTpl(out x, out y, str))
                                {
                                    vals = EnterValues();
                                    if (vals[0] == 2 && IsAllPositive(vals) && vals[1] > vals[2])
                                        figures.Add(new Ring(x, y, vals[1], vals[2]));
                                    else
                                        IncorrectDataAlert();
                                }
                                break;
                            case (byte)Figures.RECTANGLE:
                                str = "Веведите длину и ширину прямоугольника через пробел";
                                if (BaseCoordsTpl(out x, out y, str))
                                {
                                    vals = EnterValues();
                                    if (vals[0] == 2 && IsAllPositive(vals))
                                        figures.Add(new Rectangle(x, y, vals[1], vals[2]));
                                    else
                                        IncorrectDataAlert();
                                }
                                break;
                            case (byte)Figures.SQUARE:
                                str = "Веведите длину стороны квадрата";
                                if (BaseCoordsTpl(out x, out y, str))
                                {
                                    vals = EnterValues();
                                    if (vals[0] == 1 && IsAllPositive(vals))
                                        figures.Add(new Square(x, y, vals[1]));
                                    else
                                        IncorrectDataAlert();
                                }
                                break;
                            case (byte)Figures.TRIANGLE:
                                str = "Введите размеры сторон треугольника через пробел";
                                if (BaseCoordsTpl(out x, out y, str))
                                {
                                    vals = EnterValues();
                                    if (vals[0] == 3 && IsAllPositive(vals))
                                        figures.Add(new Triangle(x, y, vals[1], vals[2], vals[3]));
                                    else
                                        IncorrectDataAlert();
                                }
                                break;
                            default:
                                IncorrectDataAlert();
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
                        IncorrectDataAlert();
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

        public static int[] EnterValues()
        {
            string[] str = Console.ReadLine().Trim().Split(' ');
            int[] vals = new int[str.Length + 1]; // Создаем массив полученных из консоли данных. Первый элемент хранить количество успешно введенных значений
            vals[0] = 0;
            for (int i = 0; i < str.Length; i++)
                if (int.TryParse(str[i], out vals[i + 1])) 
                    vals[0]++;
            return vals;
        }
        public static bool IsAllPositive(int[] vals)
        {
            for (int i = 0; i < vals.Length; i++)
                if (vals[i] <= 0)
                {
                    Console.WriteLine("Данные параметры не могут быть отрицательными");
                    return false;
                }
            return true;
        }

        // Ввод начальных координат и вывод сообщения о следующих необходимых данных
        public static bool BaseCoordsTpl(out int x, out int y, MyString str)
        {
            x = y = 0;
            int[] vals;
            Console.WriteLine("Введите координаты начала отсчета через пробел:");
            vals = EnterValues();
            if ( vals[0] == 2 )
            {
                x = vals[1];
                y = vals[2];
                Console.WriteLine(str);
                return true;
            }
            IncorrectDataAlert();
            return false;
        }
        public static void IncorrectDataAlert()
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
