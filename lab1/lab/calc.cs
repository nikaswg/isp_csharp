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
                        if (1 + i == 0) // Проверка на деление на ноль
                        {
                            throw new DivideByZeroException("Деление на ноль в выражении для f при i * j == 2.");
                        }
                        return (i * i + 24) / Math.Pow((1 + i), 2);
                    }
                    else if (i * j > 2)
                    {
                        if (i * j == 0) // Проверка на деление на ноль
                        {
                            throw new DivideByZeroException("Деление на ноль в выражении для f при i * j > 2.");
                        }
                        return Math.Cos(j) + 1 / Math.Sqrt(i * j);
                    }
                }
            }

            return 0; // Возврат значения по умолчанию
        }

        public double Calculate_g(double xn, double xk, double yn, double yk, double n, double t)
        {
            // Проверка входных параметров
            if (n <= 0 || t <= 0)
            {
                throw new ArgumentOutOfRangeException("Шаги n и t должны быть положительными.");
            }

            for (double x = xn; x < xk; x += n)
            {
                for (double y = yn; y < yk; y += t)
                {
                    if (x * y < 2)
                    {
                        if (x - y - 1 == 0) // Проверка на деление на ноль
                        {
                            throw new DivideByZeroException("Деление на ноль в выражении для g при x * y < 2.");
                        }
                        return 1 / (x - y - 1);
                    }
                    if (x * y == 2)
                    {
                        if (1 + x == 0) // Проверка на деление на ноль
                        {
                            throw new DivideByZeroException("Деление на ноль в выражении для g при x * y == 2.");
                        }
                        return Math.Sin((x * y + 1 - x * x) / (1 + x));
                    }
                    if (y * y > 2)
                    {
                        return Math.Abs(2 * Math.Pow(x * y, 2) - 1 - x);
                    }
                }
            }
            return 0;
        }
    }
}