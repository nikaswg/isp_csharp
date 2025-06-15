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

        // Метод 1: Количество элементов меньше 5
        public static int CountElementsLessThanFive(int[] array, Condition condition)
        {
            return array.Count(x => x < 5);
        }

        // Метод 2: Сумма отрицательных элементов
        public static int SumNegativeElements(int[] array, Condition condition)
        {
            return array.Where(x => x < 0).Sum();
        }
    }
}