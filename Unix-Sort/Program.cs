﻿using System;

namespace Unix_Sort
{
    public class Program
    {
        public static List<string> SortFiles(List<string> files, Dictionary<string, string> flags)
        {
            List<string> lines = new List<string>();
            foreach (var fileName in files)
            {
                lines.AddRange(File.ReadAllLines(fileName));
            }

            if (flags.ContainsKey("n") && flags.ContainsKey("f"))
            {
                lines = lines.OrderBy(o => o, new StringNumericComparer()).ToList();
            }
            else
            {
                if (flags.ContainsKey("n"))
                {
                    // Сортировка "как чисел"
                    lines = lines.OrderBy(o => o, new StringNumericComparer()).ToList();
                }
                else if (flags.ContainsKey("f"))
                {
                    // Сортировка без учёта регистра
                    lines.Sort((a, b) => string.Compare(a, b, StringComparison.OrdinalIgnoreCase));
                }
                else
                {
                    // Просто сортировка
                    lines.Sort();
                }
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

        public static void Main(string[] args)
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
                            flags[flagParam[0]] = flagParam[1];
                        }
                        else
                        {
                            flags[flagParam[0]] = (flagParam[0] == "ignore-case" ? "f" : flagParam[0][0].ToString());
                        }
                    }
                    else
                    {
                        files.Add(arg);
                    }
                }

                List<string> sortedList = new List<string>();
                try
                {
                    sortedList = SortFiles(files, flags);
                    
                    if (flags.ContainsKey("o"))
                    {
                        File.WriteAllLines(flags["o"], sortedList);
                    }
                    else
                    {
                        sortedList.ForEach(s => Console.WriteLine(s));
                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine($"Файл {e.FileName} не найден!");
                }
            }
        }
    }
}