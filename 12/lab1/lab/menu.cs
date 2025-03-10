using System;

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

        // Исключение для задачи 1: элемент, равный s, не найден
        public class NoElementEqualsSEx : Exception
        {
            public NoElementEqualsSEx() : base("Элемент, равный s, не найден в массиве.") { }
        }

        // Исключение для задачи 1: нет элементов, меньших t, после первого элемента, равного s
        public class NoElementsLessThanTEx : Exception
        {
            public NoElementsLessThanTEx() : base("Нет элементов, меньших t, после первого элемента, равного s.") { }
        }

        public class S_EndEx : Exception
        {
            public S_EndEx() : base("Первый элемент равный s последний в векторе => после него ничего нет.") { }
        }

        // Метод для задачи 1: Найти номер последнего минимального элемента среди элементов, меньших t и лежащих правее первого элемента, равного s
        public int FindLastMinAfterS(int[] a, int s, int t)
        {
            int firstSElementIndex = -1;

            // Находим первый элемент, равный s
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == s)
                {
                    firstSElementIndex = i;
                    break;
                }
            }

            // Если элемент, равный s, не найден
            if (firstSElementIndex == -1)
            {
                throw new NoElementEqualsSEx();
            }

            if (firstSElementIndex == a.Length - 1)
            {
                throw new S_EndEx();
            }

            // Ищем минимальный элемент, меньший t, после первого элемента, равного s
            int minValue = int.MaxValue;
            int minIndex = -1;

            for (int i = firstSElementIndex + 1; i < a.Length; i++)
            {
                if (a[i] < t && a[i] < minValue)
                {
                    minValue = a[i];
                }
            }

            for (int i = firstSElementIndex + 1; i < a.Length; i++)
            {
                if (a[i] == minValue)
                {
                    minIndex = i;
                }
            }

            // Если нет элементов, меньших t
            if (minIndex == -1)
            {
                throw new NoElementsLessThanTEx();
            }

            return minIndex;
        }

        // Метод для задачи 2: Построить вектор b, где b[i] — сумма нечетных элементов i-й строки матрицы
        public int[] CreateVectorFromMatrix(int[][] matrix)
        {
            int n = matrix.Length;
            int[] b = new int[n];

            for (int i = 0; i < n; i++)
            {
                int sum = 0;
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i][j] % 2 != 0)
                    {
                        sum += matrix[i][j];
                    }
                }
                b[i] = sum;
            }

            return b;
        }

        // Метод для задачи 3: Перестановка строк матрицы так, чтобы первые элементы строк образовывали неубывающую последовательность
        public int[][] SortMatrixByFirstElement(int[][] matrix)
        {
            int n = matrix.Length;

            // Сортировка строк по первому элементу
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (matrix[i][0] > matrix[j][0])
                    {
                        // Меняем строки местами
                        int[] temp = matrix[i];
                        matrix[i] = matrix[j];
                        matrix[j] = temp;
                    }
                }
            }

            return matrix;
        }

        // Метод для ввода количества элементов вектора
        public int enter_n()
        {
            Console.WriteLine("Введите кол-во элементов вектора:");
            int n = int.Parse(Console.ReadLine());
            return n;
        }

        // Метод для ввода элементов вектора
        public int[] enter_vector(int n)
        {
            Console.WriteLine("Введите элементы вектора (в строку через пробел):");
            string[] b = Console.ReadLine().Split();
            int[] a = new int[n];
            for (int i = 0; i < n; i++)
            {
                a[i] = int.Parse(b[i]);
            }
            return a;
        }

        // Метод для ввода квадратной матрицы
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

        // Метод для вывода матрицы
        public void PrintMatrix(int[][] matrix)
        {
            int n = matrix.Length;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{matrix[i][j]} ");
                }
                Console.WriteLine();
            }
        }
    }
}