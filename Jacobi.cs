using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SystemOfEquations
{
    public class Jacobi
    {
        public Jacobi(double[][] leftPart, double[] rightPart, double epsilon, bool isParallel)
        {
            if (Epsilon > 0.1)
                throw new ArgumentException("epsilon > 0.1", nameof(epsilon));

            Epsilon = epsilon;
            Answer = new double[rightPart.GetLength(0)];
            if (isParallel)
            {
                TrySolveParallel(leftPart, rightPart);
                return;
            }

            TrySolve(leftPart, rightPart);
        }

        public double Epsilon { get; set; }
        public long Iterations { get; private set; }
        public double[] Answer { get; private set; }

        public bool Converge => !Answer[0].Equals(double.NaN)
                                && !double.IsInfinity(Answer[0]);


        private void TrySolve(double[][] leftPart, IReadOnlyList<double> rightPart)
        {
            var n = leftPart.GetLength(0);

            var prev = new double[rightPart.Count];
            for (var i = 0; i < n; i++)
            {
                prev[i] = 0;
            }

            long counter = 0;
            while (true)
            {
                if (counter > 10000)
                //    return;
                ++counter;

                var curr = new double[rightPart.Count];

                for (var i = 0; i < n; i++)
                {
                    curr[i] = rightPart[i];
                    for (var j = 0; j < n; j++)
                    {
                        if (i == j) continue;
                        curr[i] -= leftPart[i][j] * prev[j];
                    }
                    curr[i] /= leftPart[i][i];
                }

                double error = 0;
            
                for (var i = 0; i < n; i++)
                {
                    error += Math.Abs(curr[i] - prev[i]);
                }

                if (error < Epsilon)
                    break;

                prev = curr;
            }

            Answer = prev;
            Iterations = counter;
        }

        private void TrySolveParallel(double[][] leftPart, double[] rightPart)
        {
        }
    }
}