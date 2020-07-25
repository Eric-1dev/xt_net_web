using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_2
{
    // Класс аптечки
    class Medkit : AbstractGameObject
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
}
