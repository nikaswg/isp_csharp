using System;

namespace lab3
{
    public class Functions
    {
        public static double F(double x, double y)
        {
            return 2 * x - 4 * y + 6;
        }

        public static double G(double x, double y)
        {
            return 2 * x + y;
        }

        public static double Z(FuncDelegate func, double x, double y)
        {
            return func(x, y - 8) + 2 * func(x, y - 1);
        }
    }

    public delegate double FuncDelegate(double x, double y);
}
