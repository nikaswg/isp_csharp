using System;

namespace lab
{
    class Program
    {
        static void Main(string[] args)
        {
            menu _menu = new menu(); // Создание объекта класса menu

            while (true) // Бесконечный цикл для обработки меню
            {
                Console.WriteLine("МЕНЮ");
                Console.WriteLine("Пункт 1: Вычисление функций.");
                Console.WriteLine("Пункт 2: Работа с массивами.");
                Console.WriteLine("Пункт 3: Выход.");

                string? choose = Console.ReadLine();

                switch (choose)
                {
                    case "1": // Вычисление функций
                        _menu.Func();
                        break;

                    case "2": // Работа с массивами
                        while (true)
                        {
                            Console.WriteLine("Выберите пункт:");
                            Console.WriteLine("1. Вектор:");
                            Console.WriteLine("2. Построить вектор из матрицы:");
                            Console.WriteLine("3. Переместить максимальный элемент матрицы:");
                            Console.WriteLine("4. Вернуться в главное меню.");

                            string? choose_mass = Console.ReadLine();
                            switch (choose_mass)
                            {
                                case "1": // Работа с вектором
                                    Console.WriteLine("Введите размер вектора:");
                                    int n = _menu.enter_n();
                                    int[] vector = _menu.enter_vector(n);
                                    try
                                    {
                                        int lastMaxOddIndex = _menu.FindLastMaxOddBeforeFirstEven(n, vector);
                                        Console.WriteLine($"Номер последнего максимального нечётного элемента до первого чётного: {lastMaxOddIndex}");
                                    }
                                    catch (menu.NoEvenElementEx ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    catch (menu.NoOddElementBeforeEvenEx ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;

                                case "2": // Построение вектора из матрицы
                                    Console.WriteLine("Введите размер матрицы:");
                                    n = _menu.enter_n();
                                    int[][] matrix1 = _menu.enter_matr1(n);
                                    try
                                    {
                                        double[] avgNegatives = _menu.Create_Vector_From_Matrix_AvgNegatives(n, matrix1);
                                        Console.WriteLine("Построенный вектор:");
                                        foreach (double value in avgNegatives)
                                        {
                                            Console.Write($"{value:F2} ");
                                        }
                                        Console.WriteLine();
                                    }
                                    catch (menu.NoNegativeElementsInRowEx ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;

                                case "3": // Перемещение максимального элемента матрицы
                                    Console.WriteLine("Введите размер матрицы:");
                                    n = _menu.enter_n();
                                    int[][] matrix2 = _menu.enter_matr1(n);
                                    try
                                    {
                                        int[][] updatedMatrix = _menu.MoveMaxElementToBottomRight(n, matrix2);
                                        Console.WriteLine("Изменённая матрица:");
                                        for (int i = 0; i < n; i++)
                                        {
                                            for (int j = 0; j < n; j++)
                                            {
                                                Console.Write($"{updatedMatrix[i][j]} ");
                                            }
                                            Console.WriteLine();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;

                                case "4": // Вернуться в главное меню
                                    break;

                                default:
                                    Console.WriteLine("Некорректный выбор. Повторите попытку.");
                                    continue;
                            }
                            break;
                        }
                        break;

                    case "3": // Выход
                        return;

                    default:
                        Console.WriteLine("Некорректный выбор. Повторите попытку.");
                        break;
                }
            }
        }
    }
}