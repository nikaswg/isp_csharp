using System;

namespace lab
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ввод параметров
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

            // Вычисление шагов
            double h = (xk - xn) / n;
            double t = (yk - yn) / n;

            // Создание экземпляра класса Calc
            Calc calculator = new Calc();

            // Заголовок таблицы
            Console.WriteLine($"{"x",-10} {"y",-10} {"f",-10} {"g",-10}");

            // Вычисление и вывод значений
            for (double x = xn; x <= xk; x += h)
            {
                double fx = calculator.Calculate_f(x, xk, yn, yk, n, t); // Вызов метода
                for (double y = yn; y <= yk; y += t)
                {
                    double gy = calculator.Calculate_g(x, xk, yn, yk, n, t); // Нужно определить CalculateG
                    Console.WriteLine($"{x,-10:F2} {x,-10:F2} {fx,-10:F2} {gy,-10:F2}");
                }
            }
        }


    }
}