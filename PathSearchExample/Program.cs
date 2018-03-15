using PathSearch;
using PathSearch.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathSearchExample
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[,] m = new byte[,]
            {
                {1,1,0,1,0,0 },
                {0,0,0,0,1,0 },
                {0,0,1,0,0,0 },
                {0,0,0,1,0,0 },
                {0,1,0,1,1,1 },
                {0,1,0,0,0,0 }
            };

            PrintMatrix(m, new List<Point>());

            IPathSearcher pathSearcher = new BfsSearcher();
            var path = pathSearcher.Search(new ByteMatrixGraph(m), new Point(5, 0), new Point(0, 5));

            Console.WriteLine();
            PrintMatrix(m, path.ToList());

            Console.ReadLine();
        }

        private static void PrintMatrix(byte[,] m, List<Point> path)
        {
            var rowsCount = m.GetLength(0);
            var columnsCount = m.GetLength(1);

            for(int row = 0; row < rowsCount; row++)
            {
                for(int column = 0; column < columnsCount; column++)
                {
                    if (path.Any(t => t.Row == row && t.Column == column))
                        Console.Write("x");
                    else
                    {
                        Console.Write(m[row, column]);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
