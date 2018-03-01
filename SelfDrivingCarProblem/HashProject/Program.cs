using System;
using System.Collections.Generic;
using System.IO;

namespace HashProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFiles = new List<string>();
            inputFiles.Add($"{Environment.CurrentDirectory}\\Input\\example.in");
            inputFiles.Add($"{Environment.CurrentDirectory}\\Input\\small.in");
            inputFiles.Add($"{Environment.CurrentDirectory}\\Input\\medium.in");
            inputFiles.Add($"{Environment.CurrentDirectory}\\Input\\big.in");

            foreach (var file in inputFiles)
            {
                HandleInput(file);
            }

            Console.ReadLine();
        }


        private static void HandleInput(string input)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var fileName = Path.GetFileNameWithoutExtension(input);
            Console.WriteLine("-----------------------");
            Console.WriteLine($"Handeling: \t {fileName}");

            var lines = File.ReadAllLines(input);

            WriteResult("", Path.GetFileName(fileName));

            watch.Stop();
            Console.WriteLine($"Time elapsed: \t {watch.ElapsedMilliseconds}");

            Console.WriteLine("-----------------------");
        }

        private static void WriteResult(string output, string fileName)
        {
            File.WriteAllText($"Output\\{fileName}.out", output);
            Console.WriteLine($"Done with: \t {fileName}");
        }


    }
}
