using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_2
{
    abstract class AbstractCharacter : AbstractGameObject
    {
        // Добавляем 2 массива для хранения основного вида и альтернативного
        public char[,] View1 { get; set; }
        public char[,] View2 { get; set; }
        public AbstractCharacter() { }
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
                if (CheckCrossing(allGameObjects[i]) && this != allGameObjects[i])
                {
                    if (!CrossingAction(allGameObjects[i], ref old_x, ref old_y))
                    {
                        X = old_x;
                        Y = old_y;
                    }

                    if (this is Enemy && allGameObjects[i] is Player) // Пересечение врага с игроком
                    {
                        (allGameObjects[i] as Player).Clear();
                        (allGameObjects[i] as Player).StayBack(false, ref old_x, ref old_y);
                        (allGameObjects[i] as Player).FitToEdge();
                        (allGameObjects[i] as Player).Draw();
                        (allGameObjects[i] as Player).Damage();
                    }
                }
            }
            // --------------------------------------------
            Draw();
        }

        // Определяет пересечение объекта с другими.
        private bool CheckCrossing(AbstractGameObject gObj)
        {
            if (X + CurView.GetLength(1) > gObj.X && X < gObj.X + gObj.CurView.GetLength(1) && Y + CurView.GetLength(0) > gObj.Y && Y < gObj.Y + gObj.CurView.GetLength(0))
                return true;
            return false;
        }

        // Определяет действие при пересечении объектов
        abstract internal bool CrossingAction(AbstractGameObject secondObj, ref int old_x, ref int old_y);

        // Отпрыгиваем
        internal void StayBack(bool iMove, ref int old_x, ref int old_y)
        {
            int mult = -1;
            if (iMove)
                mult = 1;
            if (X > old_x) X -= 3 * mult;
            else if (X < old_x) X += 3 * mult;
            else if (Y > old_y) Y -= 3 * mult;
            else if (Y < old_y) Y += 3 * mult;
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
}
