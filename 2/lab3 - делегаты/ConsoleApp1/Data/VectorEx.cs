using System;
using System.Linq;

namespace lab3
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message) { }
    }

    public class VectorEx
    {
        public delegate bool Condition(int n);

        public event Action<int[], Condition> OnCalculationRequested;

        public void RequestCalculation(int[] array, Condition condition)
        {
            OnCalculationRequested?.Invoke(array, condition);
        }

        public static int SumPositiveElements(int[] array)
        {
            return array.Where(x => x > 0).Sum();
        }

        public static int ProductOfEvenElements(int[] array)
        {
            return array.Where(x => x % 2 == 0).Aggregate(1, (acc, x) => acc * x);
        }
    }
}