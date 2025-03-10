using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class Vector
    {
        public delegate bool Condition(int n);

        public static int M1(int[] a, Condition condition)
        {
            return a.Where(x => condition(x)).Aggregate(1, (acc, x) => acc * x);
        }

        public static int M2(int[] a, Condition condition)
        {
            return a.Where(x => condition(x)).Sum();
        }
    }
}
