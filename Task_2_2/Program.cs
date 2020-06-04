using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace Task_2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            bool exitGame = false;
            Game game = new Game(120, 40);

            while (!exitGame)
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(intercept: true);
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            game.MovePlayer(MOVE.UP);
                            break;
                        case ConsoleKey.DownArrow:
                            game.MovePlayer(MOVE.DOWN);
                            break;
                        case ConsoleKey.LeftArrow:
                            game.MovePlayer(MOVE.LEFT);
                            break;
                        case ConsoleKey.RightArrow:
                            game.MovePlayer(MOVE.RIGHT);
                            break;
                        case ConsoleKey.Escape:
                            exitGame = true;
                            break;
                    }
                }
            }
        }
    }

    class GameObject
    {
        // Random _rnd - статическая, т.к. используется в разных классах. Незачем плодить сущности
        static public Random _rnd = new Random();
        // Координаты объекта
        public int X { get; set; }
        public int Y { get; set; }
        
        // Здесь храним текущий внешний вид объекта
        private char[,] _curView;
        public char[,] CurView
        {
            get => _curView;
            protected set
            {
                _curView = value;
                FitToEdge();
                Draw();
            }
        }
        private ConsoleColor _color;
        public ConsoleColor Color {
            get => _color;
            set
            {
                _color = value;
                Draw();
            }
        }
        public GameObject()
        {
            CurView = new char[1, 1];
            Color = ConsoleColor.White;
            SetRndCoords();
            allGameObjects.Add(this);
        }
        private void SetRndCoords()
        {
            X = _rnd.Next(1, Game.FieldX - CurView.GetLength(1));
            Y = _rnd.Next(1, Game.FieldY - CurView.GetLength(0));
        }
        private void Draw_Clear(DRAW_CLEAR flag)
        {
            Game.mutexObj.WaitOne();
            Console.SetCursorPosition(X, Y);

            for (int i = 0; i < CurView.GetLength(0); i++)
            {
                for (int j = 0; j < CurView.GetLength(1); j++)
                {
                    if (flag == DRAW_CLEAR.DRAW)
                    {
                        Console.ForegroundColor = Color;
                        Console.Write(CurView[i, j].ToString());
                    }
                    else
                        Console.Write(' ');
                }
                Console.SetCursorPosition(X, Y + i + 1);
            }
            Game.mutexObj.ReleaseMutex();
        }
        protected void Draw() { Draw_Clear(DRAW_CLEAR.DRAW); }
        protected void Clear() { Draw_Clear(DRAW_CLEAR.CLEAR); }
        internal void Remove()
        {
            Clear();
            Game.mutexObj.WaitOne();
            allGameObjects.Remove(this);
            Game.mutexObj.ReleaseMutex();
        }

        // Не даёт пересечь границу поля
        protected void FitToEdge()
        {
            if (X < 1) X = 1;
            if (X > Game.FieldX - CurView.GetLength(1)) X = Game.FieldX - CurView.GetLength(1);
            if (Y < 1) Y = 1;
            if (Y > Game.FieldY - CurView.GetLength(0)) Y = Game.FieldY - CurView.GetLength(0);
        }

        // Все игровые объекты храним в статической коллекции
        static internal List<GameObject> allGameObjects = new List<GameObject>();
    }

    class Character : GameObject
    {
        // Добавляем 2 массива для хранения основного вида и альтернативного
        public char[,] View1 { get; set; }
        public char[,] View2 { get; set; }
        public Character() { }
        public void Move(MOVE direction)
        {
            int old_x = X;
            int old_y = Y;
            Clear();
            switch (direction)
            {
                case MOVE.RIGHT:
                    X++;
                    break;
                case MOVE.LEFT:
                    X--;
                    break;
                case MOVE.UP:
                    Y--;
                    break;
                case MOVE.DOWN:
                    Y++;
                    break;

            }
            // Не пересекаем границы поля
            FitToEdge();
            // --------------------------

            // Пересекаемся с другими объектами на карте
            for (int i = 0; i < allGameObjects.Count; i++)
            {
                if (this.Cross(allGameObjects[i]) && this != allGameObjects[i])
                {
                    if (this is Player && allGameObjects[i] is Enemy) // Пересечение игрока с врагом
                    {
                        X = old_x; Y = old_y;
                        (this as Player).Damage();
                    }
                    if (this is Enemy && allGameObjects[i] is Player) // Пересечение врага с игроком
                    {
                        X = old_x; Y = old_y;
                        (allGameObjects[i] as Player).Damage();
                    }
                    else if (this is Player && allGameObjects[i] is Medkit) // Пересечение игрока с аптечкой
                    {
                        (this as Player).Heal((allGameObjects[i] as Medkit).HealCount);
                        allGameObjects[i].Remove();
                        i--;
                    }
                    else if (this is Player && allGameObjects[i] is Money)
                    {
                        Game.Stop(true);
                    }
                    else
                        X = old_x; Y = old_y;
                }
            }
            // --------------------------------------------
            Draw();
        }

        // Определяет пересечение объекта с другими.
        private bool Cross(GameObject gObj)
        {
            if (X + CurView.GetLength(1) > gObj.X && X < gObj.X + gObj.CurView.GetLength(1) && Y + CurView.GetLength(0) > gObj.Y && Y < gObj.Y + gObj.CurView.GetLength(0))
                return true;
            return false;
        }

        // Меняем модель на альтернаивную и обратно, имитируя шаги
        public void Step()
        {
            if (CurView == View1)
                CurView = View2;
            else
                CurView = View1;
        }
    }

    class Player : Character
    {
        private readonly int _max_hp;
        private int _hp;
        public int HP
        {
            get => _hp;
            internal set
            {
                if (_hp != value) // Отрисовываем полоску HP только при изменении
                {
                    _hp = value;
                    if (_hp > _max_hp)
                        _hp = _max_hp;
                    UpdateHPBar();
                }
            }
        }
        public Player(int max_hp)
        {
            View1 = new char[,] { { 'b','_','d' },
                                   { ')','╨','(' },
                                   { '┌','-','┌' } };

            View2 = new char[,] { { 'b','_','d' },
                                   { ')','╨','(' },
                                   { '┐','-','┐' } };
            HP = _max_hp = max_hp;

            CurView = View1;
        }
        private void UpdateHPBar()
        {
            Game.mutexObj.WaitOne();
            string hpBar = " HP: " + new string('|', HP) + " ";
            Console.SetCursorPosition(2, 0);
            if (HP > (2 * _max_hp) / 3)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (HP > _max_hp / 3 && HP <= (2 * _max_hp) / 3)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(hpBar);
            Game.mutexObj.ReleaseMutex();
        }

        // Лечим
        public void Heal(int count)
        {
            HP += count;
        }

        // Дамажим
        public void Damage()
        {
            HP--;
            if (HP < 1)
            {
                HP = 0;
                Game.Stop(false);
            }
        }
    }
    class Enemy : Character
    {
        public Enemy()
        {
            View1 = new char[,] { { '/', '-', '\\' },
                                   { '|', '|',  '|' },
                                   { '\\', '_', '/' } };
            View2 = new char[,] { { '/', '-', '\\' },
                                   { '|', '-',  '|' }, 
                                   { '\\', '_', '/' } };
            CurView = View1;
        }
    }

    // Класс леса
    class Forest : GameObject
    {
        public Forest(char c)
        {
            char[,] view = new char[_rnd.Next(2, 10), _rnd.Next(2, 10)];
            for (int i = 0; i < view.GetLength(1); i++)
                for (int j = 0; j < view.GetLength(0); j++)
                    view[j, i] = c;
            CurView = view;
        }
        public Forest() : this('^') { }
    }

    // Класс аптечки
    class Medkit : GameObject
    {
        public int HealCount { get; private set; }
        public MED_TYPE MedType { get; private set; }
        public Medkit(MED_TYPE medType)
        {
            MedType = medType;
            if (MedType == MED_TYPE.SMALL)
            {
                HealCount = 1;
                CurView = new char[,] { { '┌','─','┐' },
                                        { '│','+','│' },
                                        { '└','─','┘' } };
            }
            else
            {
                HealCount = 3;
                CurView = new char[,] { { '┌','─','─','─','┐' },
                                        { '│','+','+','+','│' },
                                        { '└','─','─','─','┘' } };
            }
        }
    }

    // Деньги - цель игры
    class Money : GameObject
    {
        public Money()
        {
            CurView = new char[,] { { '┌', '─', '┐' },
                                    { '│', '$', '│' },
                                    { '└', '─', '┘' } };
        }
    }

    // Класс игры. Задает основные параметры, добавляет персонажей
    class Game
    {
        static private bool run { get; set; }
        static internal int FieldX { get; private set; }
        static internal int FieldY { get; private set; }
        public char Edge { get; set; } = '+';
        static internal Timer timer;
        static internal Mutex mutexObj = new Mutex();

        private Player player;
        private readonly Money money;
        private readonly Enemy[] enemy = new Enemy[6];
        private readonly Medkit[] food_big = new Medkit[1];
        private readonly Medkit[] food_small = new Medkit[3];
        private readonly Forest[] forest = new Forest[8];

        public Game(int x, int y)
        {
            FieldX = x;
            FieldY = y;
            Console.SetWindowSize(FieldX + 1, FieldY + 1);
            Console.SetBufferSize(FieldX + 1, FieldY + 1);
            Console.CursorVisible = false;

            DrawEdge();

            player = new Player(9);
            player.Color = ConsoleColor.Cyan;
            money = new Money();
            money.Color = ConsoleColor.Yellow;

            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i] = new Enemy();
                enemy[i].Color = ConsoleColor.Red;
            }

            for (int i = 0; i < food_small.Length; i++)
                food_small[i] = new Medkit(MED_TYPE.SMALL);

            for (int i = 0; i < food_big.Length; i++)
                food_big[i] = new Medkit(MED_TYPE.BIG);

            for (int i = 0; i < forest.Length; i++)
            {
                forest[i] = new Forest();
                forest[i].Color = ConsoleColor.Green;
            }
            Start();            
        }

        // Рисуем границы карты
        private void DrawEdge()
        {
            string str = new string(Edge, FieldX - 1);
            Console.SetCursorPosition(1, 0);
            Console.Write(str);
            Console.SetCursorPosition(1, FieldY);
            Console.Write(str);
            for (int i = 0; i < FieldY; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(Edge);
                Console.SetCursorPosition(FieldX, i);
                Console.Write(Edge);
            }
        }
        static public bool IsRun() => run;
        private void Start()
        { 
            run = true;
            timer = new Timer(TickTak, null, 0, 150);
        }
        static internal void Stop(bool win)
        {
            run = false;
            timer.Dispose();
            Console.SetCursorPosition(Game.FieldX / 2 - 6, Game.FieldY / 2);
            if (win)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You WIN!!!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Потрачено!!!");
            }
        }

        // Если игра запущена - игрок может ходить
        public void MovePlayer(MOVE direction)
        {
            if (IsRun())
                player.Move(direction);
        }

        // Функция таймера. Тут имитируем шаги и заставляем врагов ходить
        static void TickTak(object obj)
        {
            for (int i = 0; i < GameObject.allGameObjects.Count; i++)
            {
                if (GameObject.allGameObjects[i] is Character)
                    (GameObject.allGameObjects[i] as Character).Step();
                if (GameObject.allGameObjects[i] is Enemy)
                    (GameObject.allGameObjects[i] as Enemy).Move((MOVE)GameObject._rnd.Next((int)MOVE.LEFT - 1, (int)MOVE.DOWN) + 1);
            }
        }
    }
    enum MOVE
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    enum DRAW_CLEAR
    {
        DRAW,
        CLEAR
    }

    enum MED_TYPE
    {
        SMALL,
        BIG
    }
}
