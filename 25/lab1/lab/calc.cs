using System;

namespace lab
{
    public class Calc
    {
        public double Calculate_f(double x, double y)
        {
            if (x * y < 0)
            {
                if ((Math.Pow(y, 2) - 1) == 0)
                {
                    throw new DivideByZeroException("Деление на ноль в выражении для f при x * y == 0.");
                }
                return (4 * x - y + 1) / (y * y - 1);
            }
            else if (x * y == 0)
            {
                return 3 + x * x + y * y * y;
            }
            else // x * y > 0
            {
                //не проверяем деление на 0, т.к. выражение 1+y^2+x^2 не может равняться 0
                return (x - y) / (1 + y * y + x * x);
            }
        }

        public double Calculate_g(double x, double y)
        {
            if (x * y < 0)
            {
                return Math.Abs(Math.Pow(x, 4) + y);
            }
            else if (x * y == 0)
            {
                if ((x - x * y) == 0)
                {
                    throw new DivideByZeroException("Деление на ноль в выражении для g при x * y == 0.");
                }
                return (1 - x) / (x - x * y);
            }
            else // x * y > 0
            {
                if ((1+ x * y + Math.Pow(y, 3)) == 0)
                {
                    throw new Exception("Отрицательно подкоренное выражение в выражение для g при x * y > 0");
                }
                return Math.Sqrt(1 + x * y + Math.Pow(y, 3));
            }
        }
    }
}

