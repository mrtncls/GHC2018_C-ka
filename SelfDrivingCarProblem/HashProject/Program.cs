using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

        public class Coordinate
        {
            public int R
            {
                get; set;
            }
            public int C
            {
                get; set;
            }
        }


        public class Car
        {
            private int arrivalStep = 0;

            public int currentX = 0;
            public int currentY = 0;

            private Ride currentRide = null;

            public bool NeedInstructions(int step, out Ride ride)
            {
                if (currentRide == null)
                {
                    ride = null;
                    return true;
                }

                if (arrivalStep == step)
                {
                    ride = currentRide;
                    currentX = currentRide.To.R;
                    currentY = currentRide.To.C;
                    return true;
                }

                ride = null;
                return false;
            }

            private void GiveAssignment(int currentTime, int toX, int toY, int toFinishX, int toFinishY)
            {
                var timeToDriveToStartPoint = CalcTime(currentX, currentY, toX, toY);
                var timeToFinishFromRideStart = CalcTime(toX, toY, toFinishX, toFinishY);

                arrivalStep = currentTime + timeToDriveToStartPoint + timeToFinishFromRideStart;
            }

            public void GiveAssignment(int currentTime, Ride ride)
            {
                currentRide = ride;
                GiveAssignment(currentTime, ride.From.R, ride.From.C, ride.To.R, ride.To.C);
            }

            public static int CalcTime(int a, int b, int x, int y)
            {
                return Math.Abs(b - y) + Math.Abs(a - x);
            }
        }

        public class Ride
        {
            public Coordinate From
            {
                get; set;
            }
            public Coordinate To
            {
                get; set;
            }
            public int EarliestStart
            {
                get; set;
            }
            public int LatestFinish
            {
                get; set;
            }

            public int Id
            {
                get; set;
            }

            public int Travel { get; set; }
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
            int id = 0;
            foreach (var line in lines)
            {
                var splitted = line.Split(" ");
                i = 0;

                var r = new Ride
                {
                    From = new Coordinate {R = int.Parse(splitted[i++]), C = int.Parse(splitted[i++])},
                    To = new Coordinate {R = int.Parse(splitted[i++]), C = int.Parse(splitted[i++])},
                    EarliestStart = int.Parse(splitted[i++]),
                    LatestFinish = int.Parse(splitted[i++]),
                    Id = id++
                };

                r.Travel = Car.CalcTime(r.From.R, r.From.C, r.To.R, r.To.C);

                Rides.Add(r);

            }

            var orderedRides = Rides.OrderBy(x => x.EarliestStart).ToList();

            List<Car> cars = new List<Car>();
            for (int j = 0; j < Vehicles; j++)
            {
                cars.Add(new Car());
            }
            
            Dictionary<Car, List<Ride>> log = cars.ToDictionary(car => car, car => new List<Ride>());

            for (int currentTime = 0; currentTime < TotalSteps; currentTime++)
            {
                var time = currentTime;
                orderedRides.RemoveAll(r => r.LatestFinish < time);
                
                foreach (var car in cars)
                {
                    if (car.NeedInstructions(currentTime, out Ride finishedRide))
                    {
                        if (finishedRide != null)
                        {
                            log[car].Add(finishedRide);
                        }

                        var temp = orderedRides.Select(x =>
                        {
                            var toStart = Car.CalcTime(car.currentX, car.currentY, x.From.R, x.From.C);
                            var rideData = new
                            {
                                ride = x,
                                travelToEnd = toStart + x.Travel,
                                travelToStart = toStart,
                                bonusPossible = time + toStart <= x.EarliestStart
                            };
                            return rideData;
                        });

                        var vartemp = temp
                            .Where(x => time + x.travelToEnd <= TotalSteps)
                            .OrderByDescending(x => x.bonusPossible)
                            .ThenBy(x=>x.travelToStart)
                            .ThenBy(x => x.travelToEnd)
                            .ThenBy(x => x.ride.LatestFinish);

                        var found = vartemp
                            .FirstOrDefault();

                        if (found != null)
                        {
                            orderedRides.Remove(found.ride);
                            car.GiveAssignment(currentTime, found.ride);
                        }
                    }
                }
            }

            var logBuilder = new StringBuilder();
            foreach (var carLog in log)
            {
                logBuilder.AppendLine($"{carLog.Value.Count} {string.Join(' ', carLog.Value.Select(r => r.Id))}");
            }

            WriteResult(logBuilder.ToString(), Path.GetFileName(fileName));

            watch.Stop();
            Console.WriteLine($"Time elapsed: \t {watch.ElapsedMilliseconds}");

            Console.WriteLine("-----------------------");
        }

        private int CalculateRide()
        {
            return 0;
        }

        private static void WriteResult(string output, string fileName)
        {
            File.WriteAllText($"Output\\{fileName}.out", output);
            Console.WriteLine($"Done with: \t {fileName}");
        }


    }
}
