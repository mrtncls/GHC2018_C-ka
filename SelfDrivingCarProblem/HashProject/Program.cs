using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        class Coordinate
        {
            public int R { get; set; }
            public int C { get; set; }
        }

        class Ride
        {
            public Coordinate From { get; set; }              
            public Coordinate To { get; set; }
            public int EarliestStart { get; set; }
            public int LatestFinish { get; set; }
        }

        private static void HandleInput(string input)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var fileName = Path.GetFileNameWithoutExtension(input);
            Console.WriteLine("-----------------------");
            Console.WriteLine($"Handeling: \t {fileName}");

            var lines = File.ReadAllLines(input);

            var firstLine = lines.First().Split(" ");
            var i = 0;
            var rows = int.Parse(firstLine[i++]);
            var columns = int.Parse(firstLine[i++]);
            var vehicles = int.Parse(firstLine[i++]);
            var totalRides = int.Parse(firstLine[i++]);
            var bonus = int.Parse(firstLine[i++]);
            var steps = int.Parse(firstLine[i++]);

            i = 0;
            var rides = new List<Ride>();
            lines = lines.Skip(1).ToArray();
            foreach (var line in lines)
            {
                var splitted = line.Split(" ");
                rides.Add(new Ride
                {
                    From = new Coordinate {R = int.Parse(splitted[i++]), C = int.Parse(splitted[i++])},
                    To = new Coordinate {R = int.Parse(splitted[i++]), C = int.Parse(splitted[i++])},
                    EarliestStart = int.Parse(splitted[i++]),
                    LatestFinish = int.Parse(splitted[i++])
                });
            }

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
