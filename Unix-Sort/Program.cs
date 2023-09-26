using System;

namespace Unix_Sort
{
    internal class Program
    {
        /// <summary>
        /// Этапы:
        /// 1. Строки всех файлов помещаются в один временный файл.
        /// 2. 
        /// </summary>
        public static void SortLines(List<string> files, Dictionary<string, string> flags)
        {
            List<string> lines = new List<string>();
            foreach (var fileName in files)
            {
                lines.AddRange(File.ReadAllLines(fileName));
            } 
                
            lines.Sort();
            // TODO: Не должны выводить на экран в этой функции
            foreach (var line in lines)
                Console.WriteLine(line);
        }

        static void Main(string[] args)
        {
            List<string> files = new List<string>();
            Dictionary<string, string> flags = new Dictionary<string, string>();
            if (args.Length > 0)
            {
                foreach (var arg in args)
                {
                    if (arg.StartsWith("-"))
                    {
                        string[] flagParam = arg.TrimStart('-').Split('=');
                        flags[flagParam[0]] = flagParam.Length == 2 ? flagParam[1] : "";
                    }
                    else
                    {
                        files.Add(arg);
                    }
                }
                SortLines(files, flags);
            }
        }
    }
}