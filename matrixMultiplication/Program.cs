using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrixMultiplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Example2();
        }

        static void Example1()
        {
            var a = new double[,]
            {
                { 2, -3, 1},
                { 5, 4, -2}
            };

            var b = new double[,]
            {
                { -7, 5},
                { 2, -1},
                { 4, 3}
            };

            var matrixA = new Matrix(a);
            var matrixB = new Matrix(b);
            var result = MatrixCalculator.Multiply(matrixA, matrixB);

            PrintMatrix(matrixA);
            PrintMatrix(matrixB);
            PrintMatrix(result);

            Console.ReadKey();
        }

        static void Example2()
        {
            const int rowСount = 1000;
            const int columnСount = 1000;

            var stopwatchTest = new Stopwatch();
            stopwatchTest.Start();
            var matrixA = new Matrix(rowСount, columnСount);
            var matrixB = new Matrix(rowСount, columnСount);
            stopwatchTest.Stop();
            Console.WriteLine($"Создание матриц: {stopwatchTest.Elapsed}");

            // Тест №1
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            MatrixCalculator.Multiply(matrixA, matrixB);
            stopwatch.Stop();
            Console.WriteLine($"Последовательный алгоритм: {stopwatch.Elapsed}");

            // Тест №2
            stopwatch = new Stopwatch();
            stopwatch.Start();
            MatrixCalculator.ParallelMultiply(matrixA, matrixB);
            stopwatch.Stop();
            Console.WriteLine($"Параллельный алгоритм №1: {stopwatch.Elapsed}");

            // Тест №3
            stopwatch = new Stopwatch();
            stopwatch.Start();
            MatrixCalculator.ParallelMultiply2(matrixA, matrixB);
            stopwatch.Stop();
            Console.WriteLine($"Параллельный алгоритм №2: {stopwatch.Elapsed}");

            Console.ReadKey();
        }

        static void PrintMatrix(Matrix matrix)
        {
            for (int i = 0; i < matrix.RowСount; i++)
            {
                for (int j = 0; j < matrix.ColumnСount; j++)
                {
                    Console.Write($"{matrix.Data[i, j]}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
