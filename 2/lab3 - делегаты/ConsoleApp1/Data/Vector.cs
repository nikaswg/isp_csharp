using System;
using System.Linq;

namespace lab3
{
    public class Vector
    {
        public static int SumPositiveElements(int[] a)
        {
            return a.Where(x => x > 0).Sum();
        }

        public static int ProductOfEvenElements(int[] a)
        {
            return a.Where(x => x % 2 == 0).Aggregate(1, (acc, x) => acc * x);
        }
    }
}