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

            var matrixA = new Matrix(rowСount, columnСount);
            var matrixB = new Matrix(rowСount, columnСount);

            // Тест №1
            var stopwatch = new Stopwatch();
            //stopwatch.Start();
            //MatrixCalculator.Multiply(matrixA, matrixB);
            //stopwatch.Stop();
            //Console.WriteLine($"Последовательный алгоритм: {stopwatch.Elapsed}");

            //Тест №2
            stopwatch = new Stopwatch();
            stopwatch.Start();
            MatrixCalculator.ParallelMultiply(matrixA, matrixB);
            stopwatch.Stop();
            Console.WriteLine($"Параллельный алгоритм №2 Task: {stopwatch.Elapsed}");

            // Тест №3
            stopwatch = new Stopwatch();
            stopwatch.Start();
            MatrixCalculator.ParallelMultiply2(matrixA, matrixB);
            stopwatch.Stop();
            Console.WriteLine($"Параллельный алгоритм №3 OpenMP: {stopwatch.Elapsed}");

            Console.ReadKey();
        }

        static void PrintMatrix(Matrix matrix)
        {
            for (int rowIndex = 0; rowIndex < matrix.RowСount; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < matrix.ColumnСount; columnIndex++)
                {
                    Console.Write($"{matrix.Data[rowIndex, columnIndex]}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
