using System;

namespace Lab
{
    public class ArrayProcessor
    {
        // Задание 2.1: Номер последнего максимального положительного элемента после первого элемента > t
        public int FindLastMaxPositiveAfterFirstGreaterThanT(int[] vector, int t)
        {
            int firstIndex = -1;
            for (int i = 0; i < vector.Length; i++)
            {
                if (vector[i] > t)
                {
                    firstIndex = i;
                    break;
                }
            }

            if (firstIndex == -1)
            {
                throw new ArgumentException("Нет элементов больше t");
            }

            int maxValue = int.MinValue;
            int lastMaxIndex = -1;

            for (int i = firstIndex; i < vector.Length; i++)
            {
                if (vector[i] > 0 && vector[i] >= maxValue)
                {
                    maxValue = vector[i];
                    lastMaxIndex = i;
                }
            }

            if (lastMaxIndex == -1)
            {
                throw new ArgumentException("Нет положительных элементов после первого элемента > t");
            }

            return lastMaxIndex;
        }

        // Задание 2.2: Максимальный элемент выше главной диагонали
        public int FindMaxAboveDiagonal(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int max = int.MinValue;

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (matrix[i, j] > max)
                    {
                        max = matrix[i, j];
                    }
                }
            }

            return max;
        }

        // Задание 2.3: Вектор из сумм положительных элементов строк
        public int[] CreateVectorOfPositiveRowSums(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int[] vector = new int[n];

            for (int i = 0; i < n; i++)
            {
                int sum = 0;
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        sum += matrix[i, j];
                    }
                }
                vector[i] = sum;
            }

            return vector;
        }
    }
}