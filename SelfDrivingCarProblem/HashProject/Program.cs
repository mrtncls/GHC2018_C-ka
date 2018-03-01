using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace HashProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFiles = Directory.GetFiles($"{Environment.CurrentDirectory}\\Input");

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

        static int Rows;
        static int Columns;
        static int Vehicles;
        static int TotalRides;
        static int Bonus;
        static int TotalSteps;
        static List<Ride> Rides;

        private static void HandleInput(string input)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var fileName = Path.GetFileNameWithoutExtension(input);
            Console.WriteLine("-----------------------");
            Console.WriteLine($"Handeling: \t {fileName}");           

            var lines = File.ReadAllLines(input);

            var firstLine = lines.First().Split(" ");
            var i = 0;
            Rows = int.Parse(firstLine[i++]);
            Columns = int.Parse(firstLine[i++]);
            Vehicles = int.Parse(firstLine[i++]);
            TotalRides = int.Parse(firstLine[i++]);
            Bonus = int.Parse(firstLine[i++]);
            TotalSteps = int.Parse(firstLine[i++]);

            Rides = new List<Ride>();
            lines = lines.Skip(1).ToArray();
            foreach (var line in lines)
            {
                var splitted = line.Split(" ");
                i = 0;
                Rides.Add(new Ride
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
