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
        // Здесь храним внешний вид объекта
        private char[] _view;        
        public char[] View
        {
            get => _view;
            set
            {
                _view = value;
                Draw();
            }
        }
        // Random _rnd - статическая, т.к. используется в разных классах. Незачем плодить сущности
        static public Random _rnd = new Random();
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public GameObject(int width, int height)
        {
            Width = width;
            Height = height;
            SetRndCoords();
            FitToEdge();
            for (int i = 0; i < allGameObjects.Count; i++)
            {
                if (!(this is Forest))
                    while (this.Cross(allGameObjects[i]))
                        SetRndCoords();
            }
            allGameObjects.Add(this);
        }
        private void SetRndCoords()
        {
            X = _rnd.Next(1, Game.FieldX - Width);
            Y = _rnd.Next(1, Game.FieldY - Height);
        }
        private void Draw_Clear(DRAW_CLEAR flag)
        {
            Console.SetCursorPosition(X, Y);
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (flag == DRAW_CLEAR.DRAW)
                        Console.Write(View[i * Width + j].ToString());
                    else
                        Console.Write(' ');
                }
                Console.SetCursorPosition(X, Y + i + 1);
            }
        }
        public void Draw() { Draw_Clear(DRAW_CLEAR.DRAW); }
        public void Clear() { Draw_Clear(DRAW_CLEAR.CLEAR); }
        public void Remove()
        {
            Clear();
            allGameObjects.Remove(this);
        }

        // Не даёт пересеч границу поля
        protected void FitToEdge()
        {
            if (X < 1) X = 1;
            if (X > Game.FieldX - Width) X = Game.FieldX - Width;
            if (Y < 1) Y = 1;
            if (Y > Game.FieldY - Height) Y = Game.FieldY - Height;
        }

        // Все игровые объекты храним в статической коллекции
        static internal List<GameObject> allGameObjects = new List<GameObject>();
        
        /*
         *Определяет пересечение объекта с другими.
         * Ранее принадлежал классу Character, но
         * пригодился в конструкторе GameObject'а.
         */
        public bool Cross(GameObject gObj)
        {
            if (X + Width > gObj.X && X < gObj.X + gObj.Width && Y + Height > gObj.Y && Y < gObj.Y + gObj.Height)
                return true;
            return false;
        }
    }

    class Character : GameObject
    {
        // Добавляем 2 массива для хранения основного вида и альтернативного
        protected char[] _view1, _view2;
        public Character(int width, int height) : base(width, height) { }
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
                        //i++;
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
        
        // Меняем модель на альтернаивную и обратно, имитируя шаги
        public void Step()
        {
            if (View == _view1)
                View = _view2;
            else
                View = _view1;
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
        public Player(int max_hp) : base(3, 3)
        {
            string str = "b_d" + ")╨(" + "\u2510-\u2510";
            _view1 = str.ToCharArray();
            str = "b_d" + ")╨(" + "\u250C-\u250C";
            _view2 = str.ToCharArray();
            HP = _max_hp = max_hp;

            View = _view1;
        }
        private void UpdateHPBar()
        {
            string hpBar = " HP: " + new string('|', HP) + " ";
            Console.SetCursorPosition(2, 0);
            Console.Write(hpBar);
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
        public Enemy() : base(3, 3)
        {
            string str = "/-\\" + "|||" + "\\_/";
            _view1 = str.ToCharArray();
            str = "/-\\" + "|-|" + "\\_/";
            _view2 = str.ToCharArray();
            View = _view1;
        }
    }

    // Класс леса
    class Forest : GameObject
    {
        public Forest(int width, int height) : base(width, height)
        {
            string str = new string('^', width * height);
            View = str.ToCharArray();
        }
    }

    // Класс аптечки
    class Medkit : GameObject
    {
        public int HealCount { get; private set; }
        public MED_TYPE MEDTYPE { get; private set; }
        public Medkit(MED_TYPE medType) : base(medType == MED_TYPE.BIG ? 5 : 3, 3)
        {
            string str;
            this.MEDTYPE = medType;
            if (this.MEDTYPE == MED_TYPE.SMALL)
            {
                HealCount = 1;
                str = "┌─┐" + "│+│" + "└─┘";
            }
            else
            {
                HealCount = 3;
                str = "┌───┐" + "│+++│" + "└───┘";
            }
            View = str.ToCharArray();
        }
    }

    // Деньги - цель игры
    class Money : GameObject
    {
        public Money() : base(3, 3)
        {
            string str = "┌─┐" + "│$│" + "└─┘";
            View = str.ToCharArray();
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

        private Player player;
        private readonly Money money;
        private readonly Enemy[] enemy = new Enemy[4];
        private readonly Medkit[] food_big = new Medkit[1];
        private readonly Medkit[] food_small = new Medkit[3];
        private readonly Forest[] forest = new Forest[4];

        public Game(int x, int y)
        {
            FieldX = x;
            FieldY = y;
            Console.SetWindowSize(FieldX + 1, FieldY + 1);
            Console.SetBufferSize(FieldX + 1, FieldY + 1);
            Console.CursorVisible = false;

            DrawEdge();

            player = new Player(5);
            money = new Money();

            for (int i = 0; i < enemy.Length; i++)
                enemy[i] = new Enemy();

            for (int i = 0; i < food_small.Length; i++)
                food_small[i] = new Medkit(MED_TYPE.SMALL);

            for (int i = 0; i < food_big.Length; i++)
                food_big[i] = new Medkit(MED_TYPE.BIG);

            for (int i = 0; i < forest.Length; i++)
                forest[i] = new Forest(GameObject._rnd.Next(2, 10), GameObject._rnd.Next(2, 10));
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
        private void Start() { 
            run = true;
            timer = new Timer(TickTak, null, 0, 150);
        }
        static internal void Stop(bool win)
        {
            run = false;
            Console.SetCursorPosition(Game.FieldX / 2 - 6, Game.FieldY / 2);
            if (win)
                Console.WriteLine("You WIN!!!");
            else
                Console.WriteLine("Game over!!!");
            timer.Dispose();
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
            foreach (var gameObj in GameObject.allGameObjects)
            {
                if (gameObj is Character)
                    (gameObj as Character).Step();
                if (gameObj is Enemy)
                    (gameObj as Enemy).Move((MOVE)GameObject._rnd.Next((int)MOVE.LEFT - 1, (int)MOVE.DOWN) + 1);
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
