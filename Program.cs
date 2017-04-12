using System;
using System.IO;
using System.Linq;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = args.Length == 0 ? @"C:\Windows" : args[0];

            ShowLargestFilesWithoutLinq(path, 75);
            Console.WriteLine("***");
            ShowLargestFilesWithLinq(path, 75);
            Console.WriteLine("***");
            ShowLargestFilesWithLinq2(path, 75);
        }

        private static void ShowLargestFilesWithLinq2(string path, int top)
        {
            var results = new DirectoryInfo(path).GetFiles()
                .OrderByDescending(f => f.Length)
                .Take(top);

            if (top > results.Count())
            {
                Console.WriteLine("Warning: There aren't enough files to create a top " + top + " list!");
            }

            int i = 0;
            foreach (var file in results)
            {
                Console.WriteLine($"#{++i,-6}{file.Name,-20} : {file.Length,10:N0} B");
            }
        }

        private static void ShowLargestFilesWithLinq(string path, int top)
        {
            var results = from file in new DirectoryInfo(path).GetFiles()
                        orderby file.Length descending
                        select file;

            if (top > results.Count())
            {
                Console.WriteLine("Warning: There aren't enough files to create a top " + top + " list!");
            }

            int i = 0;
            foreach (var file in results.Take(top))
            {
                Console.WriteLine($"#{++i,-5}{file.Name,-20} : {file.Length,10:N0} B");
            }
        }

        private static void ShowLargestFilesWithoutLinq(string path, int top)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();

            if (top > files.Length)
            {
                Console.WriteLine("Warning: There aren't enough files to create a top " + top + " list!");
                top = files.Length;
            }

            Array.Sort(files, new FileInfoComparer());
            for (int i = 0; i < top; i++)
            {
                Console.WriteLine($"#{i + 1,-6}{files[i].Name,-20} : {files[i].Length,10:N0} B");
            }
        }
    }
}
