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
            bool exit = false;
            Game game = new Game(120, 40);

            while (!exit)
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
                            exit = true;
                            break;
                    }
                }
            }
        }
    }

    class GameObject
    {
        private char[] _view;
        static public Random _rnd = new Random();
        public int X { get; set; }
        public int Y { get; set; }
        public char[] View 
        {
            get => _view;
            set
            {
                _view = value;
                Draw();
            }
        }
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
                if (this is Character)
                    while ((this as Character).Cross(allGameObjects[i]))
                        SetRndCoords();
            }
            allGameObjects.Add(this);
        }
        private void SetRndCoords()
        {
            X = _rnd.Next(0, Game.X - Width);
            Y = _rnd.Next(0, Game.Y - Height);
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
        protected void FitToEdge()
        {
            if (X < 1) X = 1;
            if (X > Game.X - Width) X = Game.X - Width;
            if (Y < 1) Y = 1;
            if (Y > Game.Y - Height) Y = Game.Y - Height;
        }
        public void Draw() { Draw_Clear(DRAW_CLEAR.DRAW); }
        public void Clear() { Draw_Clear(DRAW_CLEAR.CLEAR); }
        public void Remove()
        {
            Clear();
            allGameObjects.Remove(this);
        }

        static internal List<GameObject> allGameObjects = new List<GameObject>();
    }

    class Character : GameObject
    {
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
                        (this as Player).Heal((allGameObjects[i] as Medkit).HEALCOUNT);
                        allGameObjects[i].Remove();
                        i++;
                    }
                    else if (this is Player && allGameObjects[i] is Key)
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
        public bool Cross(GameObject gObj)
        {
            if (X + Width > gObj.X && X < gObj.X + gObj.Width && Y + Height > gObj.Y && Y < gObj.Y + gObj.Height)
                return true;
            return false;
        }
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
        private int _max_hp;
        private int _hp;
        public int HP
        {
            get => _hp;
            internal set
            {
                _hp = value;
                if (_hp > _max_hp)
                    _hp = _max_hp;
                UpdateHPBar();
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
        public void Heal(int count)
        {
            HP += count;
        }
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
    class Forest : GameObject
    {
        public Forest(int width, int height) : base(width, height)
        {
            string str = new string('^', width * height);
            View = str.ToCharArray();
        }
    }
    class Medkit : GameObject
    {
        public int HEALCOUNT { get; private set; }
        public MED_TYPE MEDTYPE { get; private set; }
        public Medkit(MED_TYPE medType) : base(medType == MED_TYPE.BIG ? 5 : 3, 3)
        {
            string str;
            this.MEDTYPE = medType;
            if (this.MEDTYPE == MED_TYPE.SMALL)
            {
                HEALCOUNT = 1;
                str = "┌─┐" + "│+│" + "└─┘";
            }
            else
            {
                HEALCOUNT = 3;
                str = "┌───┐" + "│+++│" + "└───┘";
            }
            View = str.ToCharArray();
        }
    }
    class Key : GameObject
    {
        public Key() : base(3, 3)
        {
            string str = "┌─┐" + "│$│" + "└─┘";
            View = str.ToCharArray();
        }
    }
    class Game
    {
        static private bool _run { get; set; }
        static internal int X { get; private set; }
        static internal int Y { get; private set; }
        public char Edge { get; set; } = '+';
        static internal Timer timer;

        private Player player;
        private Key key;
        private Enemy[] enemy = new Enemy[4];
        private Medkit[] food_big = new Medkit[1];
        private Medkit[] food_small = new Medkit[3];
        private Forest[] forest = new Forest[4];

        public Game(int x, int y)
        {
            X = x;
            Y = y;
            Console.SetWindowSize(X + 1, Y + 1);
            Console.SetBufferSize(X + 1, Y + 1);
            Console.CursorVisible = false;

            DrawEdge();

            player = new Player(5);
            key = new Key();

            for (int i = 0; i < enemy.Length; i++)
                enemy[i] = new Enemy();

            for (int i = 0; i < food_big.Length; i++)
                food_big[i] = new Medkit(MED_TYPE.BIG);

            for (int i = 0; i < food_small.Length; i++)
                food_small[i] = new Medkit(MED_TYPE.SMALL);

            for (int i = 0; i < forest.Length; i++)
                forest[i] = new Forest(GameObject._rnd.Next(2, 10), GameObject._rnd.Next(2, 10));
            Start();            
        }
        private void DrawEdge()
        {
            string str = new string(Edge, X - 1);
            Console.SetCursorPosition(1, 0);
            Console.Write(str);
            Console.SetCursorPosition(1, Y);
            Console.Write(str);
            for (int i = 0; i < Y; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(Edge);
                Console.SetCursorPosition(X, i);
                Console.Write(Edge);
            }
        }
        static public bool isRun() => _run;
        private void Start() { 
            _run = true;
            timer = new Timer(TickTak, null, 0, 150);
        }
        static internal void Stop(bool win)
        {
            _run = false;
            Console.SetCursorPosition(Game.X / 2 - 6, Game.Y / 2);
            if (win)
                Console.WriteLine("You WIN!!!");
            else
                Console.WriteLine("Game over!!!");
            timer.Dispose();
        }

        public void MovePlayer(MOVE direction)
        {
            if (isRun())
                player.Move(direction);
        }
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
