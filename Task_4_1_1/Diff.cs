using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_4_1_1
{
    class Diff
    {
        private LinkedList<DiffNode> _diffs = new LinkedList<DiffNode>();
        private int _maxStringNum;

        public IEnumerable<DiffNode> Compare(string sourceFilePath, string diffFilePath)
        {
            List<string> content = ReadAllLines(sourceFilePath, FileMode.Open);
            List<string> history = ReadAllLines(diffFilePath, FileMode.OpenOrCreate);

            List<string> previous = RestoreContentFromHistory(history);

            DateTime now = DateTime.Now; // Запонимаем дату текущих изменений

            var diffsAdd = new List<DiffNode>(); // Список того, что нужно добавить в исходный файл, чтобы получить новый
            var diffsDel = new List<DiffNode>(); // Список того, что нужно удалить

            // Считываем строки нового файла во временный массив diffsAdd
            for (int i = 0; i < content.Count; i++)
            {
                DiffNode node = new DiffNode();
                node.Action = DiffTypes.Add;
                node.Date = now;
                node.StringNum = i;
                node.Text = content[i];
                diffsAdd.Add(node);
            }

            bool flag;
            int m = content.Count;

            // Удаляем все строки из старого файла, записывая изменения в diffsDel.
            // При этом выясняем, какие пары из diffsAdd и diffsDel друг друга компенсируют
            for (int i = 0; i < previous.Count; i++)
            {
                DiffNode node = new DiffNode();
                node.Action = DiffTypes.Delete;
                node.Date = now;
                node.StringNum = content.Count;
                node.Text = previous[i];
                flag = true;
                for (int j = 0; j < diffsAdd.Count; j++)
                {
                    if (node.Text.Equals(diffsAdd.ElementAt(j).Text))
                    {
                        if (node.StringNum - diffsAdd.ElementAt(j).StringNum == diffsAdd.Count - j)
                        {
                            diffsAdd.RemoveAt(j);
                            m--; // Корректируем номер удаляемой строки
                            flag = false; // Если нашли пару для удаляемой строчки - не записываем в список изменений
                            break;
                        }
                    }
                }
                if (flag)
                {
                    node.StringNum -= m;
                    diffsDel.Add(node);
                }
            }

            // Если нет изменений - не дергаем файл
            if (diffsAdd.Count != 0 || diffsDel.Count != 0)
            {

                foreach (var item in diffsDel)
                {
                    _diffs.AddLast(item);
                }
                foreach (var item in diffsAdd)
                {
                    _diffs.AddLast(item);
                }

                return _diffs;
                //WriteHistoryToFile(diffFilePath, history);
            }
            return null;

        }

        public void WriteHistoryToFile(string diffFilePath, IEnumerable<string> history)
        {
            // Открываем файл изменений
            FileStream diffFileStream;
            StreamWriter writer;
            try
            {
                diffFileStream = new FileStream(diffFilePath, FileMode.Open);
                writer = new StreamWriter(diffFileStream);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot access diff-file", ex);
            }

            history = Serialize();
            foreach (var line in history)
            {
                writer.WriteLine(line);
            }

            writer.Close();
            diffFileStream.Close();
        }

        // Свой ReadAllLines, который, в отличие от File.ReadAllLines считывает файл, даже если он открыт в блокноте
        private List<string> ReadAllLines(string path, FileMode mode)
        {
            Thread.Sleep(10); // Немного поспим, т.к. изменения могуть быть частыми и файл не успеет разблокироваться
            using (var fileStream = new FileStream(path, mode, FileAccess.Read, FileShare.ReadWrite))
            using (var streamReader = new StreamReader(fileStream))
            {
                LinkedList<string> file = new LinkedList<string>();

                while (!streamReader.EndOfStream)
                    file.AddLast(streamReader.ReadLine());
                return file.ToList<string>();
            }
        }

        private IEnumerable<string> Serialize()
        {
            LinkedList<string> lines = new LinkedList<string>();
            foreach (var diff in _diffs)
            {
                lines.AddLast($"{diff.Action}|{diff.StringNum}|{diff.Date}|{diff.Text}");
            }
            return lines.ToList<string>();
        }
        private bool Deserialize(IEnumerable<string> history)
        {
            _diffs.Clear();
            _maxStringNum = 0;
            string[] diffString;
            foreach (var line in history)
            {
                DiffNode node = new DiffNode();
                try
                {
                    diffString = line.Split('|');
                    node.Action = (DiffTypes)Enum.Parse(typeof(DiffTypes), diffString[0]);
                    node.StringNum = Int32.Parse(diffString[1]);
                    if (node.StringNum > _maxStringNum)
                        _maxStringNum = node.StringNum;
                    node.Date = DateTime.Parse(diffString[2]);
                    node.Text = line.Replace($"{diffString[0]}|{diffString[1]}|{diffString[2]}|", "");
                }
                catch
                {
                    return false;
                }
                _diffs.AddLast(node);
            }
            return true;
        }

        private List<string> RestoreContentFromHistory(IEnumerable<string> history)
        {
            Deserialize(history);

            List<string> content = new List<string>();

            foreach (var node in _diffs)
            {
                switch (node.Action)
                {
                    case DiffTypes.Add:
                        content.Insert(node.StringNum, node.Text);
                        break;
                    case DiffTypes.Delete:
                        content.RemoveAt(node.StringNum);
                        break;
                    default:
                        break;
                }
            }
            return content;
        }
    }
}
