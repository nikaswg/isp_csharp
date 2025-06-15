using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    // Пользовательское исключение для обработки ввода
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message) { }
    }

    // Делегат для математических функций
    public delegate double MathFunction(double x, double y);

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("=== МЕНЮ ===");
                Console.WriteLine("1. Задание 1 (Вычисление функций)");
                Console.WriteLine("2. Задание 2 (Обработка вектора)");
                Console.WriteLine("3. Задание 3 (Обработка вектора с событиями)");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите задание: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Task1();
                        break;
                    case "2":
                        Task2();
                        break;
                    case "3":
                        Task3();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine($"Ошибка ввода: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nНажмите любую клавишу для выхода...");
                Console.ReadKey();
            }
        }

        // Задание 1: Работа с делегатами и функциями
        static void Task1()
        {
            Console.WriteLine("\n=== Задание 1 ===");
            Console.Write("Введите 1 для функции f или 2 для функции g: ");
            string funcChoice = Console.ReadLine();

            MathFunction func;
            if (funcChoice == "1")
                func = f;
            else if (funcChoice == "2")
                func = g;
            else
                throw new InvalidInputException("Должно быть 1 или 2");

            Console.Write("Введите x: ");
            double x = GetValidDouble();
            Console.Write("Введите y: ");
            double y = GetValidDouble();

            double result = z(func, x, y);
            Console.WriteLine($"Результат z = {result:F2}");
        }

        // Задание 2: Обработка вектора с делегатами
        static void Task2()
        {
            Console.WriteLine("\n=== Задание 2 ===");
            int[] vector = GetVector();

            Console.Write("Введите 1 для суммы отрицательных или 2 для произведения нечетных: ");
            string operationChoice = Console.ReadLine();

            if (operationChoice == "1")
            {
                int sum = VectorOperations.SumNegative(vector, x => x < 0);
                Console.WriteLine($"Сумма отрицательных элементов: {sum}");
            }
            else if (operationChoice == "2")
            {
                int product = VectorOperations.ProductOdd(vector, x => x % 2 != 0);
                Console.WriteLine($"Произведение нечетных элементов: {product}");
            }
            else
            {
                throw new InvalidInputException("Должно быть 1 или 2");
            }
        }

        // Задание 3: Обработка вектора с событиями
        static void Task3()
        {
            Console.WriteLine("\n=== Задание 3 ===");
            int[] vector = GetVector();

            var processor = new VectorEventProcessor();
            processor.OperationRequested += (arr, condition) =>
            {
                Console.Write("Введите 1 для суммы отрицательных или 2 для произведения нечетных: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    int sum = VectorOperations.SumNegative(arr, condition);
                    Console.WriteLine($"Сумма отрицательных элементов: {sum}");
                }
                else if (choice == "2")
                {
                    int product = VectorOperations.ProductOdd(arr, condition);
                    Console.WriteLine($"Произведение нечетных элементов: {product}");
                }
                else
                {
                    throw new InvalidInputException("Должно быть 1 или 2");
                }
            };

            processor.RequestOperation(vector, x => true);
        }

        // Функции для задания 1
        static double f(double x, double y) => 15 + 2 * x - y;
        static double g(double x, double y) => 6 * x - 3 * y;
        static double z(MathFunction func, double x, double y) => func(2 * x, y) + 2 * func(x, y);

        // Вспомогательные методы
        static double GetValidDouble()
        {
            if (!double.TryParse(Console.ReadLine(), out double result))
                throw new InvalidInputException("Некорректное число");
            return result;
        }

        static int[] GetVector()
        {
            Console.WriteLine("Введите элементы вектора через пробел:");
            string input = Console.ReadLine();
            try
            {
                return Array.ConvertAll(input.Split(' '), int.Parse);
            }
            catch
            {
                throw new InvalidInputException("Некорректный формат вектора");
            }
        }
    }

    // Класс с операциями над вектором
    public static class VectorOperations
    {
        public static int SumNegative(int[] array, Func<int, bool> condition)
        {
            return array.Where(condition).Where(x => x < 0).Sum();
        }

        public static int ProductOdd(int[] array, Func<int, bool> condition)
        {
            return array.Where(condition).Where(x => x % 2 != 0).Aggregate(1, (acc, x) => acc * x);
        }
    }

    // Класс для обработки событий (задание 3)
    public class VectorEventProcessor
    {
        public delegate void OperationHandler(int[] array, Func<int, bool> condition);
        public event OperationHandler OperationRequested;

        public void RequestOperation(int[] array, Func<int, bool> condition)
        {
            OperationRequested?.Invoke(array, condition);
        }
    }
}