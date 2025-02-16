using System;
using System.Linq.Expressions;

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
                                    try
                                    {
                                        Console.WriteLine("Вы выбрали Вектор 1.");
                                        int n = _menu.enter_n();
                                        int[] a = new int[n];
                                        a = _menu.enter_vector(n);
                                        int c = _menu.vector_1(n, a);
                                        Console.WriteLine($"Минимальное положительное значение: {c}");
                                    }
                                    catch (menu.NoPositiveElementEx ex)
                                    {
                                        Console.WriteLine("Нету положительных элементов в векторе.");
                                    }
                                    break;
                                case "2":
                                    Console.WriteLine("Вы выбрали Вектор 2.");
                                    // Add additional functionality for Вектор 2 here
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