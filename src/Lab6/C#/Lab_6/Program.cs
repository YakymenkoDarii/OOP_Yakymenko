using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6_OOP
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Завдання 1.1.8 ===");
            double[] vectorA = { 3.5, -2.0, 4.1, -1.5, 0.0, -3.0 };
            double negativeProduct = 1.0;
            bool hasNegatives = false;

            foreach (var val in vectorA)
            {
                if (val < 0)
                {
                    negativeProduct *= val;
                    hasNegatives = true;
                }
            }
            Console.WriteLine($"Вектор: {string.Join(", ", vectorA)}");
            Console.WriteLine($"Добуток від'ємних елементів: {(hasNegatives ? negativeProduct.ToString() : "Відсутні")}");

            Console.WriteLine("\n=== Завдання 1.2.8 ===");
            double[] a = { 1, 2, 3 };
            double[] b = { 4, 5, 6 };
            double[] c = { 7, 8, 9 };

            double dotAB = DotProduct(a, b);
            double dotAC = DotProduct(a, c);
            double s = 2 * dotAB - 3 * dotAC;
            Console.WriteLine($"s = 2<a,b> - 3<a,c> = {s}");

            Console.WriteLine("\n=== Завдання 1.3.8 ===");
            double[] arr13 = { 0.5, 2.3, -0.8, -4.5, 1.0, 3.14 };

            var group1 = arr13.Where(x => Math.Abs(x) <= 1).ToArray();
            var group2 = arr13.Where(x => Math.Abs(x) > 1).ToArray();
            double[] result13 = group1.Concat(group2).ToArray();

            Console.WriteLine($"Початковий масив: {string.Join(", ", arr13)}");
            Console.WriteLine($"Перетворений масив: {string.Join(", ", result13)}");

            Console.WriteLine("\n=== Завдання 2.1.8 ===");
            int[,] matrix21 = {
                { 1, 5, 3, 9 },
                { 8, 2, 7, 4 },
                { 6, 1, 9, 3 }
            };
            Console.WriteLine("Матриця до:");
            PrintMatrix(matrix21);

            SortOddColumnsDescending(matrix21);

            Console.WriteLine("Матриця після сортування непарних стовпців за спаданням:");
            PrintMatrix(matrix21);

            Console.WriteLine("\n=== Завдання 2.2.8 ===");
            int[,] matrix22 = {
                { 1, -3, -5,  2 },
                { 2, -1,  4, -7 },
                { -3, 2, -7, -1 }
            };
            Console.WriteLine("Матриця до:");
            PrintMatrix(matrix22);

            matrix22 = SortColumnsByCharacteristic(matrix22);

            Console.WriteLine("Матриця після перестановки стовпців:");
            PrintMatrix(matrix22);

            Console.WriteLine("\n=== Завдання 2.3.8 ===");
            int[,] matrix23 = {
                { 1, -2, 3 },
                { -2, 5, 6 },
                { 3, 6, 9 }
            };
            Console.WriteLine("Матриця:");
            PrintMatrix(matrix23);
            ProcessTask238(matrix23);

            Console.ReadLine();
        }

        // Метод для 1.2.8: Скалярний добуток
        private static double DotProduct(double[] v1, double[] v2)
        {
            double sum = 0;
            for (int i = 0; i < v1.Length; i++) sum += v1[i] * v2[i];
            return sum;
        }

        // Метод для 2.1.8
        private static void SortOddColumnsDescending(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int c = 0; c < cols; c += 2)
            {
                int[] colArray = new int[rows];
                for (int r = 0; r < rows; r++) colArray[r] = matrix[r, c];

                Array.Sort(colArray);
                Array.Reverse(colArray);

                for (int r = 0; r < rows; r++) matrix[r, c] = colArray[r];
            }
        }

        // Метод для 2.2.8
        private static int[,] SortColumnsByCharacteristic(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            var charsWithIndex = new List<KeyValuePair<int, int>>();

            for (int c = 0; c < cols; c++)
            {
                int charSum = 0;
                for (int r = 0; r < rows; r++)
                {
                    int val = matrix[r, c];
                    if (val < 0 && Math.Abs(val) % 2 != 0)
                    {
                        charSum += Math.Abs(val);
                    }
                }
                charsWithIndex.Add(new KeyValuePair<int, int>(c, charSum));
            }

            charsWithIndex.Sort((x, y) => x.Value.CompareTo(y.Value));

            int[,] newMatrix = new int[rows, cols];
            for (int newCol = 0; newCol < cols; newCol++)
            {
                int oldCol = charsWithIndex[newCol].Key;
                for (int r = 0; r < rows; r++)
                {
                    newMatrix[r, newCol] = matrix[r, oldCol];
                }
            }
            return newMatrix;
        }

        // Метод для 2.3.8
        private static void ProcessTask238(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int sum = 0;

            for (int c = 0; c < cols; c++)
            {
                bool hasNegative = false;
                int colSum = 0;
                for (int r = 0; r < rows; r++)
                {
                    if (matrix[r, c] < 0) hasNegative = true;
                    colSum += matrix[r, c];
                }
                if (hasNegative) sum += colSum;
            }
            Console.WriteLine($"Сума стовпців з хоча б одним від'ємним елементом: {sum}");

            int minDim = Math.Min(rows, cols);
            bool foundMatch = false;
            for (int k = 0; k < minDim; k++)
            {
                bool match = true;
                for (int i = 0; i < minDim; i++)
                {
                    if (matrix[k, i] != matrix[i, k])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    Console.WriteLine($"Рядок і стовпець під індексом k={k} збігаються.");
                    foundMatch = true;
                }
            }
            if (!foundMatch) Console.WriteLine("Збігів між рядками та відповідними стовпцями не знайдено.");
        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    Console.Write($"{matrix[r, c],4} ");
                }
                Console.WriteLine();
            }
        }
    }
}