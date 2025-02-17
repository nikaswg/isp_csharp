using System;
using System.Linq.Expressions;
using static lab.menu;

namespace lab
{
    class Program
    {
        static void Main(string[] args)
        {
            menu _menu = new menu(); // Instantiate the menu class once

            while (true) // Infinite loop to keep prompting until the user decides to exit
            {
                // Display the main menu
                Console.WriteLine("МЕНЮ");
                Console.WriteLine("Пункт 1: Вычисление функций.");
                Console.WriteLine("Пункт 2: Работа с массивами.");

                string? choose = Console.ReadLine();

                switch (choose)
                {
                    case "1":
                        _menu.Func(); // Call the Func method when the user chooses option 1
                        break;
                    case "2":
                        while (true) // Inner loop for array operations
                        {
                            Console.WriteLine("Выберите пункт:");
                            Console.WriteLine("1. Вектор 1:");
                            Console.WriteLine("2. Вектор 2:");
                            Console.WriteLine("3. Матрица:");

                            string? choose_mass = Console.ReadLine();
                            switch (choose_mass)
                            {
                                case "1":
                                        Console.WriteLine("Вы выбрали Вектор 1.");
                                        int n = _menu.enter_n();
                                        int[] a = new int[n];
                                        a = _menu.enter_vector(n);
                                        try
                                        {
                                            int c = _menu.FindMinPositiveAfterDiv2(n, a);
                                            Console.WriteLine($"Минимальное положительное значение: {c}");
                                        }
                                        catch (NoDiv2ElementEx ex)
                                        {
                                            Console.WriteLine(ex.Message); // "Число, кратное двум, не найдено в массиве."
                                        }
                                        catch (Div2ElementIsLastEx ex)
                                        {
                                            Console.WriteLine(ex.Message); // "Число, кратное двум, является последним в векторе => после него ничего нет."
                                        }
                                        catch (NoPositiveElementAfterDiv2Ex ex)
                                        {
                                            Console.WriteLine(ex.Message); // "Нет положительных элементов после первого элемента, кратного двум."
                                        }
                                        break;
                                case "2":
                                    Console.WriteLine("Вы выбрали Вектор 2.");
                                    n = _menu.enter_n();
                                    int[][] m1 = new int[n][];
                                    Console.WriteLine("Введите элементы матрицы:");
                                    m1 = _menu.enter_matr1(n);
                                    int[] b = new int[n];
                                    b = _menu.Create_Vector_from_matrix(n, m1);
                                    for (int i = 0; i < n; i++)
                                    {
                                        Console.Write($"{b[i]} ");
                                    }
                                    Console.WriteLine();
                                    break;
                                case "3":
                                    Console.WriteLine("Вы выбрали Матрицу.");
                                    // Add additional functionality for Матрица here
                                    break;
                                default:
                                    Console.WriteLine("Некорректный выбор. Повторите попытку.");
                                    continue; // Continue prompting for the correct choice
                            }
                            // After a valid selection, break out of the inner loop
                            break;
                        }
                        break; // Exit the outer switch case for option 2
                    default:
                        Console.WriteLine("Некорректный выбор. Повторите попытку.");
                        break; // Continue the outer loop to prompt again
                }
            }
        }
    }
}