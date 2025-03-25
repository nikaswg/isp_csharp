using System.Numerics;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Functions func = new Functions();

            Console.WriteLine("Задание 1:");
            Console.WriteLine("1. Использование функции f(x,y) = 2x-4y+6");
            Console.WriteLine("2. Использование функции g(x,y) = 2x+y");
            Console.WriteLine("Остальное. Выход");
            int choice = int.Parse(Console.ReadLine());

            FuncDelegate selectedFunc;

            switch (choice)
            {
                case 1:
                    selectedFunc = Functions.F;
                    break;
                case 2:
                    selectedFunc = Functions.G;
                    break;
                default:
                    return;
            }

            Console.WriteLine("Введите значение x:");
            double x = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите значение y:");
            double y = double.Parse(Console.ReadLine());

            double result = Functions.Z(selectedFunc, x, y);
            Console.WriteLine($"Результат z(x,y) = {result}");

            Console.WriteLine("Задание 2:");
            Console.WriteLine("1. Подсчет произведения четных элементов вектора");
            Console.WriteLine("2. Подсчет суммы элементов вектора, которые кратны 7");
            choice = int.Parse(Console.ReadLine());

            int[] a = { -2, -3, 7, 14, 5, 6, 21 };

            int k = 0;

            switch (choice)
            {
                case 1:
                    k = Vector.M1(a, x => x % 2 == 0);
                    break;
                case 2:
                    k = Vector.M2(a, x => x % 7 == 0);
                    break;
            }

            Console.WriteLine($"Результат: {k}");

            Console.WriteLine("Введите кол-во элементов вектора:");
            int n = int.Parse(Console.ReadLine());

            int[] b = new int[n];

            Console.WriteLine("Введите элементы вектора:");
            for (int i = 0; i < n; i++)
            {
                b[i] = int.Parse(Console.ReadLine());
            }

            // Создаем экземпляр класса VectorEx
            VectorEx vectorEx = new VectorEx();

            // Подписываемся на событие
            vectorEx.OnCalculationRequested += (arr, condition) =>
            {
                try
                {
                    Console.WriteLine("Выберите операцию:");
                    Console.WriteLine("1. Подсчет произведения элементов, удовлетворяющих условию");
                    Console.WriteLine("2. Подсчет суммы элементов, удовлетворяющих условию");
                    int choice = int.Parse(Console.ReadLine());

                    int result = 0;
                    switch (choice)
                    {
                        case 1:
                            result = VectorEx.MultiplyElements(arr, condition);
                            Console.WriteLine($"Произведение элементов: {result}");
                            break;
                        case 2:
                            result = VectorEx.SumElements(arr, condition);
                            Console.WriteLine($"Сумма элементов: {result}");
                            break;
                        default:
                            throw new InvalidInputException("Неверный выбор. Допустимые значения: 1 или 2.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: Введено нечисловое значение.");
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine("Завершение работы программы.");
                }
            };

            // Запрашиваем условие для вектора
            Console.WriteLine("Выберите условие для элементов вектора:");
            Console.WriteLine("1. Четные числа");
            Console.WriteLine("2. Числа, кратные 7");
            int conditionChoice = int.Parse(Console.ReadLine());

            VectorEx.Condition condition;
            switch (conditionChoice)
            {
                case 1:
                    condition = x => x % 2 == 0; // Условие для четных чисел
                    break;
                case 2:
                    condition = x => x % 7 == 0; // Условие для чисел, кратных 7
                    break;
                default:
                    Console.WriteLine("Неверный выбор условия. Используется условие по умолчанию (четные числа).");
                    condition = x => x % 2 == 0;
                    break;
            }

            // Вызываем событие
            vectorEx.RequestCalculation(b, condition);
        }
    }
}

