using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Task_4_1_1
{
    // Единичный элемент истории изменений
    internal class DiffNode
    {
        public DateTime Date { get; internal set; }
        public int StringNum { get; internal set; }
        public DiffTypes Action { get; internal set; }
        public string Text { get; internal set; }
    }

    enum DiffTypes
    {
        Add,
        Delete
    }
}
