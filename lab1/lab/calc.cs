using System;

namespace lab
{
    public class Calc
    {
        public double Calculate_f(double xn, double xk, double yn, double yk, double n, double t)
        {
            // Проверка входных параметров
            if (n <= 0 || t <= 0)
            {
                throw new ArgumentOutOfRangeException("Шаги n и t должны быть положительными.");
            }

            for (double i = xn; i < xk; i += n)
            {
                for (double j = yn; j < yk; j += t)
                {
                    if (i * j < 2)
                    {
                        return Math.Sin(i) + Math.Cos(j);
                    }
                    else if (i * j == 2)
                    {
                        return (i * i + 24) / Math.Pow((1 + i), 2);
                    }
                    else if (i * j > 2)
                    {
                        return Math.Cos(j) + 1 / Math.Sqrt(i * j);
                    }
                }
            }

            return 0; // Возврат значения по умолчанию
        }


    }
}