using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_2_2
{
    // Класс игры. Задает основные параметры, добавляет персонажей
    class Game
    {
        static private bool Run { get; set; }
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
        static public bool IsRun() => Run;
        private void Start()
        {
            Run = true;
            timer = new Timer(TickTak, null, 0, 150);
        }
        static internal void Stop(bool win)
        {
            Run = false;
            timer.Dispose();
            Game.mutexObj.WaitOne();
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
            Game.mutexObj.ReleaseMutex();
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
            for (int i = 0; i < AbstractGameObject.allGameObjects.Count; i++)
            {
                if (AbstractGameObject.allGameObjects[i] is AbstractCharacter)
                    (AbstractGameObject.allGameObjects[i] as AbstractCharacter).Step();
                if (AbstractGameObject.allGameObjects[i] is Enemy)
                    (AbstractGameObject.allGameObjects[i] as Enemy).Move((MOVE)AbstractGameObject._rnd.Next((int)MOVE.LEFT - 1, (int)MOVE.DOWN) + 1);
            }
        }
    }
}
