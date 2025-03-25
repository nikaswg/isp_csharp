using System;

namespace lab
{
    public class menu
    {
        // Исключения
        public class NoEvenElementEx : Exception
        {
            public NoEvenElementEx() : base("Чётный элемент не найден в массиве.") { }
        }

        public class NoOddElementBeforeEvenEx : Exception
        {
            public NoOddElementBeforeEvenEx() : base("Нечётные элементы до первого чётного элемента отсутствуют.") { }
        }

        public class FirstEvenElementIfFirstEx : Exception
        {
            public FirstEvenElementIfFirstEx() : base("Первый четный элемент первый в векторе => перед ним ничего нет.") { }
        }

        public class NoNegativeElementsInRowEx : Exception
        {
            public NoNegativeElementsInRowEx(int row) : base($"В строке {row + 1} отсутствуют отрицательные элементы.") { }
        }

        // Метод для вычисления функций (оставлен без изменений)
        public void Func()
        {
            try
            {
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

                double h = (xk - xn) / n;
                double t = (yk - yn) / n;

                Console.WriteLine($"{"x",-10} {"y",-10} {"f",-10} {"g",-10}");

                for (double x = xn; x <= xk; x += h)
                {
                    for (double y = yn; y <= yk; y += t)
                    {
                        double fx = Math.Sin(x) + y; // Пример функции f(x, y)
                        double gy = Math.Cos(y) + x; // Пример функции g(x, y)
                        Console.WriteLine($"{x,-10:F2} {y,-10:F2} {fx,-10:F2} {gy,-10:F2}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        // Ввод размера
        public int enter_n()
        {
            Console.WriteLine("Введите размер:");
            return int.Parse(Console.ReadLine());
        }

        // Ввод вектора
        public int[] enter_vector(int n)
        {
            Console.WriteLine("Введите элементы вектора через пробел:");
            string[] elements = Console.ReadLine().Split();
            int[] vector = new int[n];
            for (int i = 0; i < n; i++)
            {
                vector[i] = int.Parse(elements[i]);
            }
            return vector;
        }

        // Задача 1: Номер последнего максимального нечётного элемента до первого чётного
        public int FindLastMaxOddBeforeFirstEven(int n, int[] a)
        {
            int firstEvenIndex = -1;
            int maxOddValue = int.MinValue;
            int lastMaxOddIndex = -1;

            for (int i = 0; i < n; i++)
            {
                if (a[i] % 2 == 0)
                {
                    firstEvenIndex = i;
                    break;
                }
            }

            if (firstEvenIndex == -1)
            {
                throw new NoEvenElementEx();
            }

            for (int i = 0; i < firstEvenIndex; i++)
            {
                if (a[i] % 2 != 0 && a[i] >= maxOddValue)
                {
                    maxOddValue = a[i];
                    lastMaxOddIndex = i;
                }
            }

            if (lastMaxOddIndex == -1)
            {
                throw new NoOddElementBeforeEvenEx();
            }

            return lastMaxOddIndex;
        }

        // Ввод матрицы
        public int[][] enter_matr1(int n)
        {
            Console.WriteLine("Введите матрицу построчно:");
            int[][] matrix = new int[n][];
            for (int i = 0; i < n; i++)
            {
                string[] elements = Console.ReadLine().Split();
                matrix[i] = new int[n];
                for (int j = 0; j < n; j++)
                {
                    matrix[i][j] = int.Parse(elements[j]);
                }
            }
            return matrix;
        }

        public double[] Create_Vector_From_Matrix_AvgNegatives(int n, int[][] a)
        {
            double[] b = new double[n];
            for (int i = 0; i < n; i++)
            {
                int sum = 0;
                int count = 0;

                for (int j = 0; j < n; j++)
                {
                    if (a[i][j] < 0)
                    {
                        sum += a[i][j];
                        count++;
                    }
                }

                if (count == 0)
                {
                    throw new NoNegativeElementsInRowEx(i);
                }

                b[i] = (double)sum / count;
            }
            return b;
        }

        // Задача 3: Переставить строки и столбцы так, чтобы максимальный элемент оказался в правом нижнем углу
        public int[][] MoveMaxElementToBottomRight(int n, int[][] a)
        {
            int maxValue = int.MinValue;
            int maxRow = -1;
            int maxCol = -1;

            // Поиск максимального элемента и его индексов
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (a[i][j] > maxValue)
                    {
                        maxValue = a[i][j];
                        maxRow = i;
                        maxCol = j;
                    }
                }
            }

            if (maxRow == -1 || maxCol == -1)
            {
                throw new Exception("Максимальный элемент не найден.");
            }

            // Перестановка строки с максимальным элементом на последнюю строку
            if (maxRow != n - 1)
            {
                for (int j = 0; j < n; j++)
                {
                    int temp = a[maxRow][j];
                    a[maxRow][j] = a[n - 1][j];
                    a[n - 1][j] = temp;
                }
            }

            // Перестановка столбца с максимальным элементом на последний столбец
            if (maxCol != n - 1)
            {
                for (int i = 0; i < n; i++)
                {
                    int temp = a[i][maxCol];
                    a[i][maxCol] = a[i][n - 1];
                    a[i][n - 1] = temp;
                }
            }

            return a;
        }
    }
}