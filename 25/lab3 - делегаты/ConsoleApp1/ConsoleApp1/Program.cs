using System;

namespace lab3
{
    class Program
    {
        static void PrintHeader(string title)
        {
            Console.WriteLine();
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($" {title.ToUpper()} ");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                PrintHeader("Лабораторная работа 3");
                Console.WriteLine("1. Работа с функциями F и G");
                Console.WriteLine("2. Работа с векторами (предопределённый массив)");
                Console.WriteLine("3. Работа с векторами (пользовательский массив)");
                Console.WriteLine("0. Выход из программы");
                Console.Write("Выберите раздел: ");

                if (!int.TryParse(Console.ReadLine(), out int mainChoice))
                {
                    Console.WriteLine("Ошибка: введите число от 0 до 3");
                    Console.ReadKey();
                    continue;
                }

                switch (mainChoice)
                {
                    case 1:
                        FunctionOperations();
                        break;
                    case 2:
                        PredefinedVectorOperations();
                        break;
                    case 3:
                        CustomVectorOperations();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void FunctionOperations()
        {
            PrintHeader("Работа с функциями");
            Console.WriteLine("Доступные функции:");
            Console.WriteLine("1. F(x,y) = 2x + y");
            Console.WriteLine("2. G(x,y) = 9x + y + 5");
            Console.Write("Выберите функцию (1-2): ");

            if (!int.TryParse(Console.ReadLine(), out int funcChoice) || funcChoice < 1 || funcChoice > 2)
            {
                Console.WriteLine("Ошибка: выберите 1 или 2");
                return;
            }

            FuncDelegate selectedFunc = funcChoice == 1 ? Functions.F : Functions.G;

            Console.Write("Введите значение x: ");
            if (!double.TryParse(Console.ReadLine(), out double x))
            {
                Console.WriteLine("Ошибка: введите корректное число");
                return;
            }

            Console.Write("Введите значение y: ");
            if (!double.TryParse(Console.ReadLine(), out double y))
            {
                Console.WriteLine("Ошибка: введите корректное число");
                return;
            }

            double result = Functions.Z(selectedFunc, x, y);
            Console.WriteLine();
            Console.WriteLine($"Результат вычисления Z(x,y) = {result:0.00}");
            Console.ReadKey();
        }

        static void PredefinedVectorOperations()
        {
            int[] predefinedArray = { -2, -3, 7, 14, 5, 6, 21 };

            PrintHeader("Работа с предопределённым вектором");
            Console.WriteLine("Текущий вектор: [" + string.Join(", ", predefinedArray) + "]");
            Console.WriteLine();
            Console.WriteLine("Доступные операции:");
            Console.WriteLine("1. Подсчёт элементов меньше 5");
            Console.WriteLine("2. Сумма отрицательных элементов");
            Console.Write("Выберите операцию (1-2): ");

            if (!int.TryParse(Console.ReadLine(), out int operation) || operation < 1 || operation > 2)
            {
                Console.WriteLine("Ошибка: выберите 1 или 2");
                return;
            }

            int result = operation == 1
                ? Vector.M1(predefinedArray, x => x < 5)
                : Vector.M2(predefinedArray, x => x < 0);

            Console.WriteLine();
            Console.WriteLine($"Результат: {result+3}");
            Console.ReadKey();
        }

        static void CustomVectorOperations()
        {
            PrintHeader("Работа с пользовательским вектором");
            Console.Write("Введите количество элементов в векторе: ");

            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.WriteLine("Ошибка: введите положительное число");
                return;
            }

            int[] customArray = new int[n];
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Введите элемент {i + 1}: ");
                if (!int.TryParse(Console.ReadLine(), out customArray[i]))
                {
                    Console.WriteLine("Ошибка: введите целое число");
                    return;
                }
            }

            VectorEx vectorEx = new VectorEx();
            vectorEx.OnCalculationRequested += (arr, condition) =>
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("Доступные операции:");
                    Console.WriteLine("1. Подсчёт элементов меньше 5");
                    Console.WriteLine("2. Сумма отрицательных элементов");
                    Console.Write("Выберите операцию (1-2): ");

                    if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 2)
                    {
                        throw new InvalidInputException("Неверный выбор. Допустимые значения: 1 или 2.");
                    }

                    int result = choice == 1
                        ? VectorEx.CountElementsLessThanFive(arr, condition)
                        : VectorEx.SumNegativeElements(arr, condition);

                    Console.WriteLine();
                    Console.WriteLine($"Результат: {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine("Операция завершена. Нажмите любую клавишу...");
                    Console.ReadKey();
                }
            };

            vectorEx.RequestCalculation(customArray, x => true);
        }
    }
}