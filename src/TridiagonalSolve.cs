using InputValidation;
using System;
using static System.Collections.Generic.Create;

namespace SystemOfEquations
{
    public class TridiagonalSolve : SolveMethod
    {
        public TridiagonalSolve(double[][] leftPart, double[] rightPart, bool isParallel) : base(leftPart, rightPart, isParallel)
        {
        }

        protected override void TrySolve(double[][] leftPart, double[] rightPart)
        {
            leftPart.ThrowIfLessOrMoreThan(3, "must contains 3 arrays");
            rightPart.ThrowIfHasNoItems("has no items");

            var a = Zip(Array(0.0), leftPart[0]);
            var b = leftPart[1];
            var c = Zip(leftPart[2], Array(0.0));

            int n = rightPart.Length;

            double[] c_ = new double[n];
            double[] d_ = new double[n];
            c_[0] = c[0] / b[0];
            d_[0] = rightPart[0] / b[0];

            for (int i = 1; i < n - 1; i++)
            {
                c_[i] = c[i] / (b[i] - c_[i - 1] * a[i]);
            }

            for (int i = 1; i < n; i++)
            {
                d_[i] = (rightPart[i] - d_[i - 1] * a[i]) / (b[i] - c_[i - 1] * a[i]);
            }

            Answer[n - 1] = d_[n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                Answer[i] = d_[i] - c_[i] * Answer[i + 1];
            }
        }

        protected override void TrySolveParallel(double[][] leftPart, double[] rightPart)
        {
            throw new NotImplementedException();
        }
    }
}
