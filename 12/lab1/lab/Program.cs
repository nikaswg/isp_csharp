using System;
using static lab.menu;

namespace lab
{
    class Program
    {
        static void Main(string[] args)
        {
            menu _menu = new menu(); // Создаем экземпляр класса menu

            while (true) // Бесконечный цикл для меню
            {
                Console.WriteLine("МЕНЮ");
                Console.WriteLine("0. Вычисление функций");
                Console.WriteLine("1. Задача 1: Найти номер последнего минимального элемента.");
                Console.WriteLine("2. Задача 2: Построить вектор из суммы нечетных элементов строк матрицы.");
                Console.WriteLine("3. Задача 3: Перестановка строк матрицы.");
                Console.WriteLine("4. Выход.");

                string? choose = Console.ReadLine();

                switch (choose)
                {
                    case "0":
                        _menu.Func();
                        break;
                    case "1":
                        Console.WriteLine("Вы выбрали Задачу 1.");
                        try
                        {
                            int n = _menu.enter_n();
                            int[] a = _menu.enter_vector(n);

                            Console.WriteLine("Введите значение s:");
                            int s = int.Parse(Console.ReadLine());

                            Console.WriteLine("Введите значение t:");
                            int t = int.Parse(Console.ReadLine());

                            int result = _menu.FindLastMinAfterS(a, s, t);
                            Console.WriteLine($"Номер последнего минимального элемента: {result}");
                        }
                        catch (NoElementEqualsSEx ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (NoElementsLessThanTEx ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Ошибка: Введено нечисловое значение.");
                        }
                        break;

                    case "2":
                        Console.WriteLine("Вы выбрали Задачу 2.");
                        try
                        {
                            int n = _menu.enter_n();
                            int[][] matrix = _menu.enter_matr1(n);

                            int[] b = _menu.CreateVectorFromMatrix(matrix);
                            Console.WriteLine("Вектор b (сумма нечетных элементов строк):");
                            for (int i = 0; i < b.Length; i++)
                            {
                                Console.Write($"{b[i]} ");
                            }
                            Console.WriteLine();
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Ошибка: Введено нечисловое значение.");
                        }
                        break;

                    case "3":
                        Console.WriteLine("Вы выбрали Задачу 3.");
                        try
                        {
                            int n = _menu.enter_n();
                            int[][] matrix = _menu.enter_matr1(n);

                            int[][] sortedMatrix = _menu.SortMatrixByFirstElement(matrix);
                            Console.WriteLine("Отсортированная матрица:");
                            _menu.PrintMatrix(sortedMatrix);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Ошибка: Введено нечисловое значение.");
                        }
                        break;

                    case "4":
                        return; // Выход из программы

                    default:
                        Console.WriteLine("Некорректный выбор. Повторите попытку.");
                        break;
                }
            }
        }
    }
}