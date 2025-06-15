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

                if (xy < 1)
                {
                    return (Math.Atan((x + y) / (1 - xy)), null);
                }
                else if (xy <= 10)
                {
                    if (xy == 0)
                        return (null, "не определена (деление на 0)");
                    double value = Math.PI + Math.Sqrt((x * x + y * y) / xy);
                    return (value, null);
                }
                else
                {
                    if (1 - xy == 0)
                        return (null, "не определена (деление на 0)");
                    return (-Math.PI + (x + y) / (1 - xy), null);
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

                if (xy < 1)
                {
                    if (1 - xy == 0)
                        return (null, "не определена (деление на 0)");
                    return ((x * x + y * y) / (1 - xy), null);
                }
                else if (xy <= 10)
                {
                    if (xy <= 0)
                        return (null, "не определена (логарифм неположительного числа)");
                    return (Math.Log(xy), null);
                }
                else
                {
                    return ((x - 1) * (y + 1), null);
                }
            }
            catch
            {
                return (null, "ошибка вычисления");
            }
        }
    }
}