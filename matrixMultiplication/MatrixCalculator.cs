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

		// Parallel
		public static Matrix ParallelMultiply(Matrix a, Matrix b)
		{
			if (a.ColumnСount != b.RowСount)
			{
				throw new ArgumentException("Matrices are not compatible for multiplication");
			}

			var result = new double[a.RowСount, b.ColumnСount];

			Parallel.For(0, a.RowСount, rowIndex =>
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
			});

			return new Matrix(result);
		}

		// Task
		public static Matrix ParallelMultiply2(Matrix a, Matrix b)
		{
			if (a.ColumnСount != b.RowСount)
			{
				throw new ArgumentException("Matrices are not compatible for multiplication");
			}

			var result = new double[a.RowСount, b.ColumnСount];

			// Получение количества всех потоков
			int numThreads = Environment.ProcessorCount;

			// Количество строк обрабатываемых каждым потоком
			int rowsPerThread = a.RowСount / numThreads;

			Task[] tasks = new Task[numThreads];
			for (int i = 0; i < numThreads; i++)
			{
				int startIndex = i * rowsPerThread;
				int endIndex = (i == numThreads - 1) ? a.RowСount : (i + 1) * rowsPerThread;

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
		public static Matrix ParallelMultiply3(Matrix a, Matrix b)
		{
			if (a.ColumnСount != b.RowСount)
			{
				throw new ArgumentException("Matrices are not compatible for multiplication");
			}

			var numThreads = Environment.ProcessorCount;

			var result = new double[a.RowСount, b.ColumnСount];

            Multiple(
                a.Data,
                a.RowСount,
                a.ColumnСount,
                b.Data,
                b.RowСount,
                b.ColumnСount,
                numThreads,
                result);

            return new Matrix(result);
		}
	}
}
