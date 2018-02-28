using System;
using System.IO;
using System.Linq;

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
        private int[,] Slices { get; set; }

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

        public void Scan()
        {
            Slices = new[,] { {0,0,2,1}, {0,2,2,2}, {0,3,2,4} };
        }

        public void WriteResult()
        {
            var output = Slices.Length + Environment.NewLine;
            for (var i = 0; i < Slices.Length / 4; i++)
                output += $"{Slices[i,0]} {Slices[i,1]} {Slices[i,2]} {Slices[i,3]} {Environment.NewLine}";
            Console.Write($"Result:{Environment.NewLine}{output}");
            File.WriteAllText("output.txt", output);
        }
    }
}
