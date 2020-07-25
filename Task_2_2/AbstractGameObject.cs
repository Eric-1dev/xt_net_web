using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_2
{
    abstract class AbstractGameObject
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
        public ConsoleColor Color
        {
            get => _color;
            set
            {
                _color = value;
                Draw();
            }
        }
        public AbstractGameObject()
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
        internal void Draw() { Draw_Clear(DRAW_CLEAR.DRAW); }
        internal void Clear() { Draw_Clear(DRAW_CLEAR.CLEAR); }
        internal void Remove()
        {
            Clear();
            Game.mutexObj.WaitOne();
            allGameObjects.Remove(this);
            Game.mutexObj.ReleaseMutex();
        }

        // Не даёт пересечь границу поля
        internal void FitToEdge()
        {
            if (X < 1) X = 1;
            if (X > Game.FieldX - CurView.GetLength(1)) X = Game.FieldX - CurView.GetLength(1);
            if (Y < 1) Y = 1;
            if (Y > Game.FieldY - CurView.GetLength(0)) Y = Game.FieldY - CurView.GetLength(0);
        }

        // Все игровые объекты храним в статической коллекции
        static internal List<AbstractGameObject> allGameObjects = new List<AbstractGameObject>();
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
