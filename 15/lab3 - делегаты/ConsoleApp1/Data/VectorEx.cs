using System;
using System.Linq;

namespace lab3
{
    // Собственный тип исключения для обработки ошибок ввода
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message) { }
    }
    public class VectorEx
    {
        // Делегат для условия
        public delegate bool Condition(int n);

        // Событие для вызова методов
        public event Action<int[], Condition> OnCalculationRequested;

        // Метод для вызова события
        public void RequestCalculation(int[] array, Condition condition)
        {
            OnCalculationRequested?.Invoke(array, condition);
        }

        // Метод 1: Произведение элементов, удовлетворяющих условию
        public static int MultiplyElements(int[] array, Condition condition)
        {
            var filteredElements = array.Where(x => condition(x)).ToList();
            if (filteredElements.Count == 0)
            {
                throw new InvalidOperationException("Нет элементов, удовлетворяющих условию.");
            }
            return filteredElements.Aggregate(1, (acc, x) => acc * x);
        }

        // Метод 2: Сумма элементов, удовлетворяющих условию
        public static int SumElements(int[] array, Condition condition)
        {
            var filteredElements = array.Where(x => condition(x)).ToList();
            if (filteredElements.Count == 0)
            {
                throw new InvalidOperationException("Нет элементов, удовлетворяющих условию.");
            }
            return filteredElements.Sum();
        }
    }
}