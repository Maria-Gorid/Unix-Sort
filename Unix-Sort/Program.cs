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
        public static List<string> SortLines(List<string> files, Dictionary<string, string> flags)
        {
            List<string> lines = new List<string>();
            foreach (var fileName in files)
            {
                lines.AddRange(File.ReadAllLines(fileName));
            }

            if (flags.ContainsKey("n"))
            {
                lines = lines.OrderBy(o => o, new StringNumericComparer()).ToList();
            }
            else if (flags.ContainsKey("f"))
            {
                lines.Sort((a, b) => string.Compare(a, b, StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                lines.Sort();
            }

            if (flags.ContainsKey("u"))
            {
                lines = lines.Distinct().ToList();
            }

            if (flags.ContainsKey("r"))
            {
                lines.Reverse();
            }

            return lines;
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
                        if (flagParam.Length == 2)
                        {
                            flags[flagParam[0]] = (flagParam[1] == "ignore-case" ? "f" : flagParam[1][0].ToString());
                        }
                        else
                        {
                            flags[flagParam[0]] = "";
                        }
                    }
                    else
                    {
                        files.Add(arg);
                    }
                }

                List<string> sortedList = SortLines(files, flags);

                if (flags.ContainsKey("o"))
                {
                    File.WriteAllLines(flags["o"], sortedList);
                }
                else
                {
                    sortedList.ForEach(s => Console.WriteLine(s));
                }
            }
        }
    }
}