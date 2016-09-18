using System;
using System.Collections.Generic;

namespace SystemOfEquations
{
    public class RelaxationMethod : IterationMethodBase
    {
        public double Omega { get; set; }

        public RelaxationMethod(double[][] leftPart, double[] rightPart, bool isParallel, double omega, double epsilon)
            : base(leftPart, rightPart, isParallel, epsilon)
        {
            Omega = omega;
        }

        protected override void TrySolve(double[][] leftPart, IReadOnlyList<double> rightPart)
        {
            double s1;
            double s2;
            var n = leftPart.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                s1 = 0;
                s2 = 0;
                for (int j = 0; j < i; j++)
                {
                    
                }
            }
        }

        protected override void TrySolveParallel(double[][] leftPart, double[] rightPart)
        {
            
        }
    }
}