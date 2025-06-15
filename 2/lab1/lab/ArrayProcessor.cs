using System;

namespace Lab
{
    public class ArrayProcessor
    {
        // Задание 2.1: Поменять местами максимальный и минимальный элементы вектора
        public void SwapMaxAndMinElements(ref int[] vector)
        {
            if (vector == null || vector.Length == 0)
                throw new ArgumentException("Вектор пуст");

            int minIndex = 0;
            int maxIndex = 0;

            for (int i = 1; i < vector.Length; i++)
            {
                if (vector[i] < vector[minIndex])
                    minIndex = i;
                if (vector[i] > vector[maxIndex])
                    maxIndex = i;
            }

            // Меняем местами
            int temp = vector[minIndex];
            vector[minIndex] = vector[maxIndex];
            vector[maxIndex] = temp;
        }

        // Задание 2.2: Удалить строку с максимальным элементом из матрицы
        public int[,] RemoveRowWithMaxElement(int[,] matrix, out int removedRowIndex)
        {
            if (matrix == null || matrix.GetLength(0) == 0)
                throw new ArgumentException("Матрица пуста");

            int n = matrix.GetLength(0);
            int maxValue = matrix[0, 0];
            removedRowIndex = 0;

            // Находим максимальный элемент и его строку
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i, j] > maxValue)
                    {
                        maxValue = matrix[i, j];
                        removedRowIndex = i;
                    }
                }
            }

            // Создаем новую матрицу без найденной строки
            int[,] newMatrix = new int[n - 1, n];
            for (int i = 0, newI = 0; i < n; i++)
            {
                if (i != removedRowIndex)
                {
                    for (int j = 0; j < n; j++)
                    {
                        newMatrix[newI, j] = matrix[i, j];
                    }
                    newI++;
                }
            }

            return newMatrix;
        }

        // Задание 2.3: Построить вектор из сумм положительных элементов столбцов
        public void CreateVectorOfPositiveColumnSums(int[,] matrix, out int[] vector)
        {
            if (matrix == null || matrix.GetLength(0) == 0)
                throw new ArgumentException("Матрица пуста");

            int n = matrix.GetLength(0);
            vector = new int[n];

            for (int j = 0; j < n; j++)
            {
                int sum = 0;
                for (int i = 0; i < n; i++)
                {
                    if (matrix[i, j] > 0)
                    {
                        sum += matrix[i, j];
                    }
                }
                vector[j] = sum;
            }
        }
    }
}
