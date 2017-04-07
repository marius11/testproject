using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Windows";
            ShowLargestFilesWithoutLinq(path);
            Console.WriteLine("***");
            ShowLargestFilesWithLinq(path);
            Console.WriteLine("***");
            ShowLargestFilesWithLinq2(path);
        }

        private static void ShowLargestFilesWithLinq2(string path)
        {
            var results = new DirectoryInfo(path).GetFiles()
                .OrderByDescending(f => f.Length)
                .Take(5);

            foreach (var file in results)
            {
                Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0} B");
            }
        }

        private static void ShowLargestFilesWithLinq(string path)
        {
            var results = from file in new DirectoryInfo(path).GetFiles()
                        orderby file.Length descending
                        select file;

            foreach (var file in results.Take(5))
            {
                Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0} B");
            }
        }

        private static void ShowLargestFilesWithoutLinq(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();
            Array.Sort(files, new FileInfoComparer());
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{files[i].Name, -20} : {files[i].Length, 10:N0} B");
            }
        }
    }
}
