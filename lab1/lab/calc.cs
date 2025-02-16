using System;

namespace lab
{
    public class Calc
    {
        public double Calculate_f(double x, double y)
        {
            if (x * y < 2)
            {
                return Math.Sin(x) + Math.Cos(y);
            }
            else if (x * y == 2)
            {
                if (1 + x == 0)
                {
                    throw new DivideByZeroException("Деление на ноль в выражении для f при x * y == 2.");
                }
                return (x * x + 24) / Math.Pow((1 + x), 2);
            }
            else // x * y > 2
            {
                if (x * y == 0)
                {
                    throw new DivideByZeroException("Деление на ноль в выражении для f при x * y > 2.");
                }
                return Math.Cos(y) + 1 / Math.Sqrt(x * y);
            }
        }

        public double Calculate_g(double x, double y)
        {
            if (x * y < 2)
            {
                if (x - y - 1 == 0)
                {
                    throw new DivideByZeroException("Деление на ноль в выражении для g при x * y < 2.");
                }
                return 1 / (x - y - 1);
            }
            else if (x * y == 2)
            {
                if (1 + x == 0)
                {
                    throw new DivideByZeroException("Деление на ноль в выражении для g при x * y == 2.");
                }
                return Math.Sin((x * y + 1 - x * x) / (1 + x));
            }
            else // y * y > 2
            {
                return Math.Abs(2 * Math.Pow(x * y, 2) - 1 - x);
            }
        }
    }
}

