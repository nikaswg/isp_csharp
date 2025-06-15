using System;

namespace lab3
{
    public class Functions
    {
        public static double F(double x, double y)
        {
            return 5 + 3 * x - y;
        }

        public static double G(double x, double y)
        {
            return 7 * x - y;
        }

        public static double Z(FuncDelegate func, double x, double y)
        {
            return func(x, y) + 2 * func(x, y);
        }
    }

    public delegate double FuncDelegate(double x, double y);
}