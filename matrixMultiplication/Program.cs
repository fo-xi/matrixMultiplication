using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrixMultiplication
{
    class Program
    {
        static void Main(string[] args)
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
