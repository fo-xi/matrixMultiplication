using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace matrixMultiplication
{
	public static class MatrixCalculator
	{
		// Получение количества всех потоков
		//private static readonly int NumThreads = Environment.ProcessorCount;
		private static readonly int NumThreads = 8;

		[DllImport("OpenMPDll.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int Multiple(double[,] a, int aRowСount, int aColumnСount,
			double[,] b, int bRowСount, int bColumnСount, int threadCount, double[,] result);

		public static Matrix Multiply(Matrix a, Matrix b)
		{
			if (a.ColumnСount != b.RowСount)
			{
				throw new ArgumentException("Matrices are not compatible for multiplication");
			}

			var result = new double[a.RowСount, b.ColumnСount];

			for (int rowIndex = 0; rowIndex < a.RowСount; rowIndex++)
			{
				for (int columnIndex = 0; columnIndex < b.ColumnСount; columnIndex++)
				{
					double sum = 0;
					for (int k = 0; k < a.ColumnСount; k++)
					{
						sum += a.Data[rowIndex, k] * b.Data[k, columnIndex];
					}

					result[rowIndex, columnIndex] = sum;
				}
			}

			return new Matrix(result);
		}

		// Task
		public static Matrix ParallelMultiply(Matrix a, Matrix b)
		{
			if (a.ColumnСount != b.RowСount)
			{
				throw new ArgumentException("Matrices are not compatible for multiplication");
			}

			var result = new double[a.RowСount, b.ColumnСount];

			// Количество строк обрабатываемых каждым потоком
			int rowsPerThread = a.RowСount / NumThreads;

			Task[] tasks = new Task[NumThreads];
			for (int i = 0; i < NumThreads; i++)
			{
				int startIndex = i * rowsPerThread;
				int endIndex = (i == NumThreads - 1) ? a.RowСount : (i + 1) * rowsPerThread;

				tasks[i] = Task.Run(() =>
				{
					for (int rowIndex = startIndex; rowIndex < endIndex; rowIndex++)
					{
						for (int columnIndex = 0; columnIndex < b.ColumnСount; columnIndex++)
						{
							double sum = 0;
							for (int k = 0; k < a.ColumnСount; k++)
							{
								sum += a.Data[rowIndex, k] * b.Data[k, columnIndex];
							}

							result[rowIndex, columnIndex] = sum;
						}
					}
				});
			}

			Task.WaitAll(tasks);
			return new Matrix(result);
		}

		// OpenMP
		public static Matrix ParallelMultiply2(Matrix a, Matrix b)
		{
			if (a.ColumnСount != b.RowСount)
			{
				throw new ArgumentException("Matrices are not compatible for multiplication");
			}

			var result = new double[a.RowСount, b.ColumnСount];

            Multiple(
                a.Data,
                a.RowСount,
                a.ColumnСount,
                b.Data,
                b.RowСount,
                b.ColumnСount,
                NumThreads,
                result);

            return new Matrix(result);
		}
	}
}
