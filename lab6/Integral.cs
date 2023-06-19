using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public class Integral: IDataErrorInfo
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

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch(columnName)
                {
                    case "A":
                        if(A < -1 || A > 1)
                        {
                            error = "The start of the range should be [-1;1]";
                        }
                        break;
                    case "B":
                        if (B < 0 || B > 10)
                        {
                            error = "The finish of the range should be [0;10]";
                        }
                        break;
                    case "N":
                        if (N < 100)
                        {
                            error = "The number of the split points should be >=100";
                        }
                        break;
                }
                return error;
            }
        }

        public double A { get => a; set => a = value; }
        public double B { get => b; set => b = value; }
        public int N { get => n; set => n = value; }


        public string Error => throw new NotImplementedException();

        public override string ToString()
        {
            return $"a = {A} b = {B} n = {N}";
        }
    }
}
