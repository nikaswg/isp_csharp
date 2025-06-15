using System;

namespace Lab
{
    public class Menu
    {
        private readonly Calculator _calculator = new Calculator();
        private readonly ArrayProcessor _arrayProcessor = new ArrayProcessor();

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("МЕНЮ");
                Console.WriteLine("1. Вычисление функций f и g");
                Console.WriteLine("2. Работа с массивами");
                Console.WriteLine("3. Выход");
                Console.Write("Выберите пункт: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CalculateFunctions();
                        break;

                    case "2":
                        ShowArrayMenu();
                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        private void CalculateFunctions()
        {
            try
            {
                Console.WriteLine("Введите начальное значение xn:");
                double xn = double.Parse(Console.ReadLine());

                Console.WriteLine("Введите конечное значение xk:");
                double xk = double.Parse(Console.ReadLine());

                Console.WriteLine("Введите шаг h для x:");
                double h = double.Parse(Console.ReadLine());

                Console.WriteLine("Введите начальное значение yn:");
                double yn = double.Parse(Console.ReadLine());

                Console.WriteLine("Введите конечное значение yk:");
                double yk = double.Parse(Console.ReadLine());

                Console.WriteLine("Введите шаг t для y:");
                double t = double.Parse(Console.ReadLine());

                // Проверка корректности ввода
                if (h <= 0 || t <= 0)
                    throw new ArgumentException("Шаг должен быть положительным");
                if ((xn > xk && h > 0) || (xn < xk && h < 0))
                    throw new ArgumentException("Некорректный диапазон для x");
                if ((yn > yk && t > 0) || (yn < yk && t < 0))
                    throw new ArgumentException("Некорректный диапазон для y");

                // Заголовок таблицы
                Console.WriteLine("\n{0,10}{1,10}{2,20}{3,20}", "x", "y", "f(x,y)", "g(x,y)");
                Console.WriteLine(new string('-', 60));

                // Вычисление и вывод значений
                for (double x = xn; (h > 0 && x <= xk) || (h < 0 && x >= xk); x += h)
                {
                    for (double y = yn; (t > 0 && y <= yk) || (t < 0 && y >= yk); y += t)
                    {
                        var (fResult, fError) = _calculator.CalculateF(x, y);
                        var (gResult, gError) = _calculator.CalculateG(x, y);

                        string fStr = fResult.HasValue ? fResult.Value.ToString("F4") : fError;
                        string gStr = gResult.HasValue ? gResult.Value.ToString("F4") : gError;

                        Console.WriteLine("{0,10:F2}{1,10:F2}{2,20}{3,20}", x, y, fStr, gStr);
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: неверный формат числа");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Неизвестная ошибка: {ex.Message}");
            }
        }

        private void ShowArrayMenu()
        {
            while (true)
            {
                Console.WriteLine("\nРАБОТА С МАССИВАМИ");
                Console.WriteLine("1. 1.\tДан целочисленный вектор A(n). Найти номер последнего максимального элемента среди положительных элементов, начиная с первого элемента, большего заданного числа t. (Ответ индекс элемента (индексы идут от 0)).");
                Console.WriteLine("2. Найти максимальный элемент выше главной диагонали в матрице");
                Console.WriteLine("3. Построить вектор из сумм положительных элементов строк матрицы");
                Console.WriteLine("4. Вернуться в главное меню");
                Console.Write("Выберите пункт: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ProcessVector();
                        break;

                    case "2":
                        ProcessMatrixMaxAboveDiagonal();
                        break;

                    case "3":
                        ProcessMatrixSumPositive();
                        break;

                    case "4":
                        return;

                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        private void ProcessVector()
        {
            try
            {
                Console.Write("Введите размер вектора: ");
                int n = int.Parse(Console.ReadLine());

                Console.Write("Введите элементы вектора через пробел: ");
                int[] vector = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

                Console.Write("Введите число t: ");
                int t = int.Parse(Console.ReadLine());

                int result = _arrayProcessor.FindLastMaxPositiveAfterFirstGreaterThanT(vector, t);
                Console.WriteLine($"Номер последнего максимального положительного элемента: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private void ProcessMatrixMaxAboveDiagonal()
        {
            try
            {
                Console.Write("Введите размер матрицы: ");
                int n = int.Parse(Console.ReadLine());

                Console.WriteLine("Введите матрицу построчно (элементы через пробел):");
                int[,] matrix = new int[n, n];

                for (int i = 0; i < n; i++)
                {
                    string[] row = Console.ReadLine().Split();
                    for (int j = 0; j < n; j++)
                    {
                        matrix[i, j] = int.Parse(row[j]);
                    }
                }

                int max = _arrayProcessor.FindMaxAboveDiagonal(matrix);
                Console.WriteLine($"Максимальный элемент выше главной диагонали: {max}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private void ProcessMatrixSumPositive()
        {
            try
            {
                Console.Write("Введите размер матрицы: ");
                int n = int.Parse(Console.ReadLine());

                Console.WriteLine("Введите матрицу построчно (элементы через пробел):");
                int[,] matrix = new int[n, n];

                for (int i = 0; i < n; i++)
                {
                    string[] row = Console.ReadLine().Split();
                    for (int j = 0; j < n; j++)
                    {
                        matrix[i, j] = int.Parse(row[j]);
                    }
                }

                int[] vector = _arrayProcessor.CreateVectorOfPositiveRowSums(matrix);
                Console.WriteLine("Полученный вектор:");
                Console.WriteLine(string.Join(" ", vector));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}