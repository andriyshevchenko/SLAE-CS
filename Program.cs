using System;
using System.Diagnostics;

namespace SystemOfEquations
{
    internal class Program
    {
        public static double[][] matrix1;
        public static double[][] row1;

        private static void Main(string[] args)
        {
            var random = new Random();

            var n = random.Next(10);

            matrix1 = GetRandom(random, n, n, 50);
            row1 = GetRandom(random, 1, n, 50);
            Console.WriteLine("Epsilon is");
            var eps = Convert.ToDouble(Console.ReadLine());
            var method = new GaussSeidel(matrix1, row1[0], eps, false);
            var solution = method.Answer;

            Print(matrix1);
            Print(row1);
            Print(solution);
            Console.WriteLine(method.Iterations + "iterations");

            for (var i = 0; i < matrix1.GetLength(0); i++)
            {
                double sum = 0;
                for (var j = 0; j < matrix1[0].GetLength(0); j++)
                {
                    sum += matrix1[i][j] * solution[j];
                }
                if (sum > 0 || sum < 0)
                {
                    Console.WriteLine($"{i} - {sum}");
                }
            }

            Console.ReadLine();
        }

        private static double[][] GetRandom(Random random, int n, int m, int maxValue)
        {
            var matr = new double[n][];
            for (var i = 0; i < n; i++)
            {
                matr[i] = new double[m];
            }

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    matr[i][j] = random.Next(1, maxValue);
                }
            }
            return matr;
        }

        private static long MeasureExecutionTime(Action method)
        {
            var watch =
                Stopwatch.StartNew();

            method();

            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        private static void Print(double[][] matrix)
        {
            var n = matrix.GetLength(0);
            var m = n;
            Console.WriteLine(n + "x" + m);
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    Console.Write(matrix[i][j] + " ");
                }
                Console.WriteLine();
            }
        }

        private static void Print(double[] matrix)
        {
            var n = matrix.GetLength(0);

            Console.WriteLine(n);
            for (var i = 0; i < n; i++)
            {
                Console.Write(matrix[i] + " ");

                Console.WriteLine();
            }
        }
    }
}