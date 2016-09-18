using System;
using System.Diagnostics;

namespace SystemOfEquations
{
    internal class Program
    {
        public static double[][] _matrix;
        public static double[] _row;

        private static void Main()
        {
            Console.WriteLine("Epsilon is");
            var eps = Convert.ToDouble("0,00001");

            var random = new Random();
            const int n = 2;
            Jacobi method;
            do
            {
                _row = GetRandomRow(random, 1, n, 50);
                _matrix = GetRandom(random, n, n, 50);
                method = new Jacobi(_matrix, _row, eps, false);
                method.Solve();
            } while (!method.Converge);

            Print(_matrix, "matrix");
            Print(_row, "row");
            Print(method.Answer, "solution");
            Console.ReadLine();
        }

        private static double[][] GetRandom(Random _random, int _n, int m, int maxValue)
        {
            var matr = new double[_n][];
            for (var i = 0; i < _n; i++)
            {
                matr[i] = new double[m];
            }

            for (var i = 0; i < _n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    matr[i][j] = _random.Next(1, maxValue);
                }
            }
            return matr;
        }

        private static double[] GetRandomRow(Random _random, int _n, int m, int maxValue)
        {
            return GetRandom(_random, 1, m, maxValue)[0];
        }

        private static long MeasureExecutionTime(Action method)
        {
            var watch =
                Stopwatch.StartNew();

            method();

            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        private static void Print(double[][] matrix, string name)
        {
            var n = matrix.GetLength(0);
            var m = _matrix[0].GetLength(0);
            Console.WriteLine(name);
            Console.WriteLine("{0}x{1}\n", n, m);
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    Console.Write(matrix[i][j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static void Print(double[] matrix, string name)
        {
            var n = matrix.GetLength(0);
            Console.WriteLine(name);
            Console.WriteLine(n + " x 1\n");
            for (var i = 0; i < n; i++)
            {
                Console.Write(matrix[i] + " ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}