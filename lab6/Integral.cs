using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public class Integral
    {
        private double a, b;
        int n;

        public Integral()
        {
        }

        public Integral(double a, double b, int n)
        {
            A = a;
            B = b;
            N = n;
        }

        public double A { get => a; set => a = value; }
        public double B { get => b; set => b = value; }
        public int N { get => n; set => n = value; }

        public override string ToString()
        {
            return $"a = {A} b = {B} n = {N}";
        }
    }
}
