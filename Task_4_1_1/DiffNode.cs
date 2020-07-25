using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Task_4_1_1
{
    // Единичный элемент истории изменений
    class DiffNode : ISerializable
    {
        public DateTime Date { get; private set; }
        public int StringNum { get; private set; }
        public DiffTypes Action { get; private set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }

    enum DiffTypes
    {
        Add,
        Delete
    }
}
