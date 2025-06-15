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
            return a.Count(x => x < 5);
        }

        public static int M2(int[] a, Condition condition)
        {
            return a.Where(x => x < 0).Sum();
        }
    }
}