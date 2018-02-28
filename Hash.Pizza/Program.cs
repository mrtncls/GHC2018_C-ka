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
            Console.ReadLine();
        }
    }

    public class PizzaScanner
    {
        // Dataset
        private char[][] Pizza { get; set; }

        // Configuration
        private int RowsMax { get; set; }
        private int ColsMax { get; set; }
        private int LowestAmount { get; set; }
        private int HighestAmount { get; set; }

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

            const int counter = 0;
            foreach (var line in lines.Skip(1))
            {
                var ingredients = line.ToCharArray();
                Pizza[counter] = ingredients;
            }
        }

        public void Scan()
        {

        }
    }
}
