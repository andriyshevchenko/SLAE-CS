using System;
using System.Collections.Generic;

namespace SystemOfEquations
{
    public class Jacobi : IterationMethodBase
    {
        public Jacobi(double[][] leftPart, double[] rightPart, double epsilon, bool isParallel) 
            : base(leftPart,rightPart,isParallel,epsilon)
        { 
            
        }

        protected override void TrySolve(double[][] leftPart, IReadOnlyList<double> rightPart)
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
                        curr[i] -= leftPart[i][j]*prev[j];
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

        protected override void TrySolveParallel(double[][] leftPart, double[] rightPart)
        {
        }
    }
}