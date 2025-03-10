using System;

namespace lab
{
    public class Calc
    {
        public double Calculate_f(double x, double y)
        {
            if (x * y < 3)
            {
                if (x - y == 0)
                {
                    throw new DivideByZeroException("Деление на нольь в выражении для f при x * y < 3 ");
                }
                return Math.Atan(1 / (x - y));
            }
            else if (x * y == 3)
            {
                if (1 + x - x * y == 0)
                {
                    throw new DivideByZeroException("Деление на ноль в выражении для f при x * y == 3.");
                }
                return 1 / (1 + x - x * y);
            }
            else // x * y > 3
            {
                return Math.Exp(x) + Math.Log(5 * Math.Abs(x - y));
            }
        }

        public double Calculate_g(double x, double y)
        {
            if (x * y < 3)
            {
                if (x - y - 1 == 0)
                {
                    throw new DivideByZeroException("Деление на ноль в выражении для g при x * y < 3.");
                }
                return 101 / (x - y - 1);
            }
            else if (x * y == 3)
            {
                return x * x * x + y * y;
            }
            else 
            {
                if (x + 1 == 0)
                {
                    throw new DivideByZeroException(" Деление на ноль в выражении для g при x * y > 3");
                }
                return Math.Log(Math.Abs(x)) + Math.Sin((x * y - 1) / (1 + x));
            }
        }
    }
}

