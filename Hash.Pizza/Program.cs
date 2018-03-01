using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hash.Pizza.SliceValidator;

namespace Hash.Pizza
{
    public class Program
    {
        static void Main(string[] args)
        {
            var scanner = new PizzaScanner(args[0]);
            scanner.Scan();
            scanner.WriteResult();
            Console.ReadLine();
        }
    }

    public class PizzaScanner
    {
        // Dataset
        private char[,] Pizza { get; set; }

        // Configuration
        private int RowsMax { get; set; }
        private int ColsMax { get; set; }
        private int LowestAmount { get; set; }
        private int HighestAmount { get; set; }

        // Result
        private List<int[]> Slices { get; set; }

        public PizzaScanner(string input)
        {
            ReadInput(input);
        }

        private void ReadInput(string input)
        {
            var lines = File.ReadAllLines(input);

            var confRow = lines.First().Split(' ');
            RowsMax = int.Parse(confRow[0]);
            ColsMax = int.Parse(confRow[1]);
            LowestAmount = int.Parse(confRow[2]);
            HighestAmount = int.Parse(confRow[3]);
            Console.WriteLine($"R: {RowsMax} C: {ColsMax} L: {LowestAmount} H: {HighestAmount}");

            lines = lines.Skip(1).ToArray();

            Pizza = new char[RowsMax, ColsMax];

            for (var l = 0; l < lines.Length; l++)
            for (var c = 0; c < lines[l].Length; c++)
                Pizza[l, c] = lines[l][c];
        }

        class Shape
        {
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Shapes
        {
            private Dictionary<int, IEnumerable<Shape>> ShapesBySize { get; set; }

            public Shapes(int r, int c, int l, int h)
            {
                ShapesBySize = Enumerable.Range(l * 2, h - 1).ToDictionary(i => i, hCounter =>
                {
                    var shapes = new List<Shape>();

                    var ss = Enumerable.Range(2, hCounter).Where(counter => hCounter % counter == 0 && counter <= h);

                    shapes.AddRange(ss.Select(counter => new Shape { height = counter, width = (int)Math.Floor(hCounter / (float)counter) }).ToList());

                    var revsersed = shapes.Where(s => s.height != s.width).Select(s => new Shape { height = s.width, width = s.height }).ToList();
                    shapes.AddRange(revsersed);

                    return shapes.AsEnumerable();
                });
            }
        }


        public void Scan()
        {
            ISliceValidator validator = new SliceValidatorVOne();
            Slices = new List<int[]>();

            int shapeIndex = 0;
            var shapes = new[]
            {
                new Shape {width = 3, height = 2},
                new Shape {width = 2, height = 3},
                new Shape {width = 3, height = 1},
                new Shape {width = 1, height = 3},
            };

            int rowStep = 1;
            for (int r = 0; r < RowsMax;)
            {
                for (int c = 0; c < ColsMax;)
                {
                    var xEnd = r + shapes[shapeIndex].height - 1;
                    var yEnd = c + shapes[shapeIndex].width - 1;

                    if (xEnd < RowsMax &&
                        c + shapes[shapeIndex].width - 1  < ColsMax &&
                        validator.SliceIsValid(Pizza, r, xEnd, c,
                            yEnd,
                            LowestAmount, HighestAmount, 2))
                    {
                        Slices.Add(new[] { r, c, xEnd, yEnd });
                        c += shapes[shapeIndex].width;
                        rowStep = shapes[shapeIndex].height;
                    }
                    else
                        c++;
                }

                r += rowStep;
            }
        }

        public void WriteResult()
        {
            var output = Slices.Count + Environment.NewLine;
            foreach (var slice in Slices)
                output += $"{slice[0]} {slice[1]} {slice[2]} {slice[3]} {Environment.NewLine}";
            Console.Write($"Result:{Environment.NewLine}{output}");
            File.WriteAllText("output.txt", output);
        }
    }
}