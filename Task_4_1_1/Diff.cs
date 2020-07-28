using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_4_1_1
{
    internal class Diff
    {
        internal IEnumerable<DiffNode> Compare(string sourceFilePath, string diffFilePath)
        {
            var content = new List<string>(ReadAllLines(sourceFilePath, FileMode.Open));
            
            var history = new List<string>(ReadAllLines(diffFilePath, FileMode.OpenOrCreate));
            var diffs = new LinkedList<DiffNode>(Deserialize(history));

            var previous = new List<string>(RestoreContentFromHistory(history));

            DateTime currentDate = DateTime.Now; // Запонимаем дату текущих изменений

            var diffsAdd = new List<DiffNode>(); // Список того, что нужно добавить в исходный файл, чтобы получить новый
            var diffsDel = new List<DiffNode>(); // Список того, что нужно удалить

            // Считываем строки нового файла во временный массив diffsAdd
            for (int i = 0; i < content.Count; i++)
            {
                DiffNode node = new DiffNode();
                node.Action = DiffTypes.Add;
                node.Date = currentDate;
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
                node.Date = currentDate;
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
                            m--;
                            flag = false; // Если нашли пару для удаляемой строчки - не записываем в список изменений
                            break;
                        }
                    }
                }
                if (flag)
                {
                    node.StringNum -= m; // Корректируем номер удаляемой строки
                    diffsDel.Add(node);
                }
            }

            // Если нет изменений - возвращаем null
            if (diffsAdd.Count != 0 || diffsDel.Count != 0)
            {

                foreach (var item in diffsDel)
                {
                    diffs.AddLast(item);
                }
                foreach (var item in diffsAdd)
                {
                    diffs.AddLast(item);
                }

                return diffs;
            }
            return null;
        }

        internal void WriteContentToFile(string fileFullPath, IEnumerable<string> content)
        {
            FileStream fileStream;
            StreamWriter writer;
            try
            {
                fileStream = new FileStream(fileFullPath, FileMode.Open);
                writer = new StreamWriter(fileStream);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot access diff-file", ex);
            }
            foreach (var line in content)
                writer.WriteLine(line);

            writer.Close();
            fileStream.Close();
        }
        internal void WriteHistoryToFile(string diffFullPath, IEnumerable<DiffNode> diffs)
        {
            IEnumerable<string> history;
            // Открываем файл изменений
            FileStream diffFileStream;
            StreamWriter writer;
            try
            {
                diffFileStream = new FileStream(diffFullPath, FileMode.Open);
                writer = new StreamWriter(diffFileStream);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot access diff-file", ex);
            }

            history = Serialize(diffs);
            foreach (var line in history)
                writer.WriteLine(line);

            writer.Close();
            diffFileStream.Close();
        }

        // Свой ReadAllLines, который, в отличие от File.ReadAllLines считывает файл, даже если он открыт в блокноте
        private IEnumerable<string> ReadAllLines(string path, FileMode mode)
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

        private IEnumerable<string> Serialize(IEnumerable<DiffNode> diffs)
        {
            LinkedList<string> lines = new LinkedList<string>();
            foreach (var diff in diffs)
            {
                lines.AddLast($"{diff.Action}|{diff.StringNum}|{diff.Date}|{diff.Text}");
            }
            return lines.ToList<string>();
        }
        private IEnumerable<DiffNode> Deserialize(IEnumerable<string> history)
        {
            var diffs = new LinkedList<DiffNode>();
            int maxStringNum = 0;
            string[] diffString;
            foreach (var line in history)
            {
                DiffNode node = new DiffNode();
                try
                {
                    diffString = line.Split('|');
                    node.Action = (DiffTypes)Enum.Parse(typeof(DiffTypes), diffString[0]);
                    node.StringNum = Int32.Parse(diffString[1]);
                    if (node.StringNum > maxStringNum)
                        maxStringNum = node.StringNum;
                    node.Date = DateTime.Parse(diffString[2]);
                    node.Text = line.Replace($"{diffString[0]}|{diffString[1]}|{diffString[2]}|", "");
                }
                catch (Exception ex)
                {
                    throw new InvalidDataException("Invalid history format", ex);
                }
                diffs.AddLast(node);
            }
            return diffs;
        }

        private IEnumerable<string> RestoreContentFromHistory(IEnumerable<string> history) => RestoreContentFromHistory(history, DateTime.MaxValue);
        private IEnumerable<string> RestoreContentFromHistory(IEnumerable<string> history, DateTime date)
        {
            var diffs = Deserialize(history);
            List<string> content = new List<string>();

            foreach (var node in diffs)
            {
                if (node.Date > date)
                    break;
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

        internal IEnumerable<string> RestoreContentFormDiffFile(string diffFilePath, DateTime date)
        {
            var history = new List<string>(ReadAllLines(diffFilePath, FileMode.OpenOrCreate));
            var content = RestoreContentFromHistory(history, date);
            if (content.Count() == 0)
                return null;
            return content;
        }
    }
}
