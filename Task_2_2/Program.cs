using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
                    //snake.Rotation(key.Key);
                }
            }
        }
    }

    class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char[] View { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public GameObject(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            allGameObjects.Add(this);
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
                Console.SetCursorPosition(X, Y + 1 + i);

            }
        }
        public void Draw() { Draw_Clear(DRAW_CLEAR.DRAW); }
        public void Clear() { Draw_Clear(DRAW_CLEAR.CLEAR); }
        
        static public List<GameObject> allGameObjects = new List<GameObject>();
    }

    class Character : GameObject
    {
        public Character(int x, int y, int width, int height) : base(x, y, width, height)
        {

        }
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
            if (X < 1) X = 1;
            if (X > Game.X - 3) X = Game.X - 3;
            if (Y < 1) Y = 1;
            if (Y > Game.Y - 3) Y = Game.Y - 3;
            // --------------------------

            // Пересекаемся с другими объектами на карте
            for (int i = 0; i < allGameObjects.Count; i++)
            {
                if (this.Cross(allGameObjects[i]) && this != allGameObjects[i])
                {
                    if (this is Player && allGameObjects[i] is Enemy) // Пересечение с врагом
                    {
                        X = old_x; Y = old_y;
                        (this as Player).HP--;
                        if ((this as Player).HP < 1)
                        {
                            (this as Player).HP = 0;
                            Console.SetCursorPosition(Game.X / 2 - 6, Game.Y / 2);
                            Console.WriteLine("Game over!!!");
                        }
                        (this as Player).UpdateHPBar();
                    }
                    else if (this is Player && allGameObjects[i] is Forest) // Пересечение со съедобным объектом
                    {
                        allGameObjects[i].Clear();
                        allGameObjects.Remove(allGameObjects[i]);
                        i++;
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
    }

    class Player : Character
    {
        public int HP { get; internal set; }
        public Player(int x, int y, int hp) : base(x, y, 3, 3)
        {
            //string str = "bod" + ")╨(" + "\u250C-\u2510";
            HP = hp;
            string str = "b_d" + ")╨(" + "\u2510-\u2510".ToCharArray();
            View = str.ToCharArray();
        }
        public void UpdateHPBar()
        {
            string hpBar = "HP: " + new string('|', HP) + " ";
            Console.SetCursorPosition(2, 0);
            Console.Write(hpBar);
        }
    }
    class Enemy : Character
    {
        public Enemy(int x, int y) : base(x, y, 3, 3)
        {
            string str = "/-\\" + "|||" + "\\_/";
            View = str.ToCharArray();
        }
    }
    class Forest : GameObject
    {
        public Forest(int x, int y, int width, int height) : base(x, y, width, height)
        {
            string str = new string('^', width * height);
            View = str.ToCharArray();
        }
    }
    class Game
    {
        static public int X { get; private set; }
        static public int Y { get; private set; }
        public char Edge { get; set; } = '+';

        private Player player;
        public Game(int x, int y)
        {
            X = x;
            Y = y;
            Console.SetWindowSize(X + 1, Y + 1);
            Console.SetBufferSize(X + 1, Y + 1);
            Console.CursorVisible = false;

            DrawEdge();

            player = new Player(20, 30, 10);
            player.Draw();
            player.UpdateHPBar();

            Enemy enemy = new Enemy(40, 30);
            enemy.Draw();

            Forest forest = new Forest(25, 15, 3, 3);
            forest.Draw();
        }
        private void DrawEdge()
        {
            string str = new string(Edge, X);
            Console.SetCursorPosition(2, 0);
            Console.Write(str);
            Console.SetCursorPosition(0, Y);
            Console.Write(str);
            for (int i = 0; i < Y; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.WriteLine(Edge);
            }
        }

        public void MovePlayer(MOVE direction)
        {
            player.Move(direction);
        }
    }
    enum MOVE
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
        STAY
    }

    enum DRAW_CLEAR
    {
        DRAW,
        CLEAR
    }
}
