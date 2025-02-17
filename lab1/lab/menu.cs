using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace lab
{
    public class menu
    {
        public void Func() // Make this method static to allow calling from Main
        {
            try
            {
                // Input parameters
                Console.WriteLine("Введите xn и xk (через пробел):");
                string[] xParams = Console.ReadLine().Split();
                double xn = double.Parse(xParams[0]);
                double xk = double.Parse(xParams[1]);

                Console.WriteLine("Введите yn и yk (через пробел):");
                string[] yParams = Console.ReadLine().Split();
                double yn = double.Parse(yParams[0]);
                double yk = double.Parse(yParams[1]);

                Console.WriteLine("Введите количество частей (n):");
                int n = int.Parse(Console.ReadLine());

                // Step calculation
                double h = (xk - xn) / n;
                double t = (yk - yn) / n;

                // Create an instance of the Calc class
                Calc calculator = new Calc();

                // Table header
                Console.WriteLine($"{"x",-10} {"y",-10} {"f",-10} {"g",-10}");

                // Calculate and output values
                for (double x = xn; x <= xk; x += h)
                {
                    for (double y = yn; y <= yk; y += t)
                    {
                        double fx = 0;
                        double gy = 0;

                        try
                        {
                            fx = calculator.Calculate_f(x, y);
                        }
                        catch (DivideByZeroException ex)
                        {
                            Console.WriteLine($"Ошибка: {ex.Message} для f при x = {x:F2}, y = {y:F2}");
                            fx = double.NaN; // Assign NaN if there's an error
                        }

                        try
                        {
                            gy = calculator.Calculate_g(x, y);
                        }
                        catch (DivideByZeroException ex)
                        {
                            Console.WriteLine($"Ошибка: {ex.Message} для g при x = {x:F2}, y = {y:F2}");
                            gy = double.NaN; // Assign NaN if there's an error
                        }

                        Console.WriteLine($"{x,-10:F2} {y,-10:F2} {fx,-10:F2} {gy,-10:F2}");
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Пожалуйста, введите корректные числовые значения.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public int enter_n()
        {
            Console.WriteLine("Введите кол-во элементов вектора:");
            int n = int.Parse(Console.ReadLine());
            return n;
        }

        public int[] enter_vector(int n)
        {
            Console.WriteLine("Введите элементы вектора(в строку через пробел):");
            string[] b = Console.ReadLine().Split();
            int[] a = new int[n];
            for (int i = 0; i < n; i++)
            {
                a[i] = int.Parse(b[i]);
            }
            return a;
        }

        public class NoDiv2ElementEx : Exception
        {
            public NoDiv2ElementEx() : base("Число, кратное двум, не найдено в массиве.") { }
        }

        public class Div2ElementIsLastEx : Exception
        {
            public Div2ElementIsLastEx() : base("Число, кратное двум, является последним в векторе => после него ничего нет.") { }
        }

        public class NoPositiveElementAfterDiv2Ex : Exception
        {
            public NoPositiveElementAfterDiv2Ex() : base("Нет положительных элементов после первого элемента, кратного двум.") { }
        }

        public int FindMinPositiveAfterDiv2(int n, int[] a)
        {
            int firstDiv2Index = -1;

            // Find the first element that is divisible by 2
            for (int i = 0; i < n; i++)
            {
                if (a[i] % 2 == 0)
                {
                    firstDiv2Index = i;
                    break;
                }
            }

            // If no divisible by 2 element found
            if (firstDiv2Index == -1)
            {
                throw new NoDiv2ElementEx();
            }

            // Check if the divisible by 2 element is the last one
            if (firstDiv2Index == n - 1)
            {
                throw new Div2ElementIsLastEx();
            }

            // Find the minimum positive element after the found divisible by 2 element
            int minPositive = int.MaxValue;
            bool hasPositive = false;

            for (int i = firstDiv2Index + 1; i < n; i++)
            {
                if (a[i] > 0)
                {
                    hasPositive = true;
                    if (a[i] < minPositive)
                    {
                        minPositive = a[i];
                    }
                }
            }

            // If no positive elements found after the first divisible by 2 element
            if (!hasPositive)
            {
                throw new NoPositiveElementAfterDiv2Ex();
            }

            return minPositive;
        }

        public int[][] enter_matr1(int n)
        {
            int[][] a = new int[n][];
            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();
                string[] elements = input.Split(' ');
                a[i] = new int[n]; 
                for (int j = 0; j < n; j++)
                {
                    a[i][j] = int.Parse(elements[j]);
                }
            }
            return a;
        }

        public int[] Create_Vector_from_matrix(int n, int[][] a)
        {
            int[] b = new int[n];
            int min = 0;
            for (int j = 0; j < n; j++)
            {
                min = int.MaxValue;
                for (int i = 0; i < n; i++)
                {
                    if (a[i][j] < min)
                    {
                        min = a[i][j];
                    }
                    b[j] = min;
                }
            }
            return b;
        }
    }
}
