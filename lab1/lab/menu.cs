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

        public class NoPositiveElementEx : Exception 
        {
            public NoPositiveElementEx(): base("Положительный элемент не найдем в массиве.") { }
        }

        public class NoEvenAndPosElementEx : Exception
        {
            public NoEvenAndPosElementEx(): base("Четный и кратный двум элемент не найден в массиве") { }
        }

        public class Div2ElementIsLastEx : Exception
        {
            public Div2ElementIsLastEx() : base("Четный и кратный двум элемент является последним в векторе => после него ничего нет") { }
        }


        public int vector_1(int n, int[] a)
        {
            {
                int Pos_first_positive = -1;
                for (int i = 0; i < n; i++)
                {
                    if (a[i] > 0) 
                    {  
                        Pos_first_positive = i;
                        break;
                    }
                }
                if ( Pos_first_positive == -1 )
                {
                    throw new NoPositiveElementEx();
                }
            }
            return 0;
        }
    }
}
