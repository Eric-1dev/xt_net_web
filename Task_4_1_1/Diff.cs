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

        public void Compare(string sourceFilePath, string historyFilePath)
        {
            Thread.Sleep(10); // Немного поспим, т.к. изменения могуть быть частыми и файл не успеет разблокироваться
            List<string> content = ReadAllLines(sourceFilePath, FileMode.Open);
            List<string> history = ReadAllLines(historyFilePath, FileMode.OpenOrCreate);

            List<string> previous = RestoreContentFromHistory(history);

            DateTime now = DateTime.Now;

            var diffsAdd = new List<DiffNode>();
            var diffsDel = new List<DiffNode>();

            // Тут творится такая магия, что студенты Хогвардса нервно курят свои палочки
            bool flag = true;
            for (int i = 0; i < content.Count; i++)
            {
                //if (!content[i].Equals(previous.ElementAtOrDefault(i)))
                {
                    //flag = false;
                    DiffNode node = new DiffNode();
                    node.Action = DiffTypes.Add;
                    node.Date = now;
                    node.StringNum = i;
                    node.Text = content[i];
                    diffsAdd.Add(node);
                    previous.Insert(i, content[i]);
                }
            }

            for (int i = content.Count; i < previous.Count; i++)
            {
                DiffNode node = new DiffNode();
                node.Action = DiffTypes.Delete;
                node.Date = now;
                node.StringNum = i;
                node.Text = previous[i];
                flag = true;
                for (int j = 0; j < diffsAdd.Count; j++)
                {
                    if (node.Text.Equals(diffsAdd.ElementAt(j).Text))
                    {
                        Console.WriteLine($"CurNum = {node.StringNum} - At_j = {diffsAdd.ElementAt(j).StringNum} == add.Count = {diffsAdd.Count} - j = {j}  Text = {node.Text}");
                        if (node.StringNum - diffsAdd.ElementAt(j).StringNum == diffsAdd.Count - j )
                        {
                            Console.WriteLine("Found!!!");
                            diffsAdd.RemoveAt(j);
                            flag = false;
                            foreach (var item in diffsDel)
                                item.StringNum--;
                            break;
                        }
                    }
                }
                Console.WriteLine();
                if (flag)
                {
                    diffsDel.Add(node);
                }
                previous.RemoveAt(i);
                i--;
            }

            foreach (var item in diffsAdd)
            {
                _diffs.AddLast(item);
            }
            foreach (var item in diffsDel)
            {
                _diffs.AddLast(item);
            }
            
            // Открываем файл изменений
            FileStream diffFileStream;
            StreamWriter writer;
            try
            {
                diffFileStream = new FileStream(historyFilePath, FileMode.Open);
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
            using (var fileStream = new FileStream(path, mode, FileAccess.Read, FileShare.ReadWrite))
            using (var streamReader = new StreamReader(fileStream))
            {
                LinkedList<string> file = new LinkedList<string>();

                while (!streamReader.EndOfStream)
                    file.AddLast(streamReader.ReadLine());
                return file.ToList<string>();
            }
        }

        private List<string> Serialize()
        {
            LinkedList<string> lines = new LinkedList<string>();
            foreach (var diff in _diffs)
            {
                lines.AddLast($"{diff.Action}|{diff.StringNum}|{diff.Date}|{diff.Text}");
            }
            return lines.ToList<string>();
        }
        private bool Deserialize(List<string> history)
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

        private List<string> RestoreContentFromHistory(List<string> history)
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
