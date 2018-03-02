using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

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

            public int Distance(Coordinate coord)
            {
                return Math.Abs(R - coord.R) + Math.Abs(C - coord.C);
            }
        }

        class Ride
        {
            public int Number { get; set; }
            public Coordinate From { get; set; }
            public Coordinate To { get; set; }
            public int EarliestStart { get; set; }
            public int LatestFinish { get; set; }
            public int TotalSteps { get; set; }

            public void CalcSteps()
            {
                TotalSteps = From.Distance(To);
            }

            public bool Possible(Coordinate coord, int step)
            {
                return
                    From.Distance(coord) + TotalSteps <= TotalSteps - step &&
                    LatestFinish > step &&
                    LatestFinish <= step;
            }
        }

        class Car
        {
            public List<Ride> Rides { get; set; }
        }

        static int Rows;
        static int Columns;
        static int Vehicles;
        static int TotalRides;
        static int Bonus;
        static int TotalSteps;
        static List<Ride> Rides;
        static List<Car> Cars;

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
            var number = 0;
            foreach (var line in lines)
            {
                var splitted = line.Split(" ");
                i = 0;
                var item = new Ride();
                item.Number = number++;
                item.From = new Coordinate { R = int.Parse(splitted[i++]), C = int.Parse(splitted[i++]) };
                item.To = new Coordinate { R = int.Parse(splitted[i++]), C = int.Parse(splitted[i++]) };
                item.EarliestStart = int.Parse(splitted[i++]);
                item.LatestFinish = int.Parse(splitted[i++]);
                item.CalcSteps();
                Rides.Add(item);
            }

            Cars = new List<Car>();
            for (int j = 0; j < Vehicles; j++)
            {
                var car = new Car();
                car.Rides = new List<Ride>();

                int stepsLeft = TotalSteps;
                var currCoordinate = new Coordinate { C = 0, R = 0 };
                while (stepsLeft > 0)
                {
                    int currentStep = TotalSteps - stepsLeft;

                    var canStartNow = Rides.Select(r =>
                    {
                        int dist = currCoordinate.Distance(r.From);
                        return new
                        {
                            Distance = dist,
                            Possible = dist + r.TotalSteps <= stepsLeft && r.EarliestStart <= currentStep && currentStep + dist + r.TotalSteps <= r.LatestFinish,
                            Ride = r
                        };
                    }).Where(r => r.Possible).OrderBy(r => r.Distance);

                    if (canStartNow.Any())
                    {
                        var ride = canStartNow.First();
                        car.Rides.Add(ride.Ride);
                        currCoordinate = ride.Ride.To;
                        stepsLeft -= ride.Distance + ride.Ride.TotalSteps;
                        Rides.Remove(ride.Ride);
                    }
                    else
                    {
                        var canStartAfterWaiting = Rides.Select(r =>
                        {
                            int dist = currCoordinate.Distance(r.From);
                            return new
                            {
                                Distance = dist,
                                Possible = currentStep + dist <= r.EarliestStart,
                                Ride = r
                            };
                        }).Where(r => r.Possible).OrderBy(r => r.Distance);

                        if (canStartAfterWaiting.Any())
                        {
                            var ride = canStartAfterWaiting.First();
                            car.Rides.Add(ride.Ride);
                            currCoordinate = ride.Ride.To;
                            stepsLeft = TotalSteps - (ride.Ride.EarliestStart + ride.Ride.TotalSteps);
                            Rides.Remove(ride.Ride);
                        }
                        else
                        {
                            stepsLeft = 0;
                        }
                    }
                }

                Cars.Add(car);
            }

            StringBuilder sb = new StringBuilder();
            foreach (var car in Cars)
            {
                sb.Append($"{car.Rides.Count}");
                foreach (var ride in car.Rides)
                    sb.Append($" {ride.Number}");
                sb.AppendLine();
            }

            WriteResult(sb.ToString(), Path.GetFileName(fileName));

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
