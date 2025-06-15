using System;

namespace Lab
{
    public class Calculator
    {
        public (double? result, string error) CalculateF(double x, double y)
        {
            try
            {
                double xy = x * y;

                if (xy < 2)
                {
                    return (Math.Cos(xy) + (x - y * y) / 4, null);
                }
                else if (xy <= 5)
                {
                    double denominator = Math.Sqrt(5 * x * x - 24);
                    if (denominator == 0 || double.IsNaN(denominator))
                        return (null, "не определена");
                    return ((x * x * y * y * y) / denominator, null);
                }
                else
                {
                    double denominator = Math.Sin(xy + 1);
                    if (denominator == 0)
                        return (null, "не определена");
                    return ((x * x * x + y + x * y * y * y) / denominator, null);
                }
            }
            catch
            {
                return (null, "ошибка вычисления");
            }
        }

        public (double? result, string error) CalculateG(double x, double y)
        {
            try
            {
                double xy = x * y;

                if (xy < 2)
                {
                    double denominator = 2 + Math.Cos(x);
                    if (denominator == 0)
                        return (null, "не определена");
                    return (x / denominator, null);
                }
                else if (xy <= 5)
                {
                    return (x * x + Math.Atan(xy), null);
                }
                else
                {
                    double sqrtArg = Math.Cos(xy) - x;
                    if (sqrtArg < 0 || double.IsNaN(sqrtArg))
                        return (null, "не определена");
                    return (Math.Sqrt(sqrtArg), null);
                }
            }
            catch
            {
                return (null, "ошибка вычисления");
            }
        }
    }
}