using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace matrixMultiplication
{
	public static class MatrixCalculator
	{
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

			// Содержит задачи, выполняющие перемножение
			Task[] tasks = new Task[numThreads];
			for (int i = 0; i < numThreads; i++)
			{
				int startIndex = i * rowsPerThread;

				// Если последний поток, то берем все до последней строки
				// Например: 1000 строк
				// Начальный индекс для первого потока = 0
				// Конечный индекс для первого потока = 62 (то есть 1000 : 16)
				// !!! Не включительно, так как отсчет идет от нуля

				// Начальный индекс для второго потока = 62
				// Конечный индекс для второго потока = 124 (то есть 2 * 62)

				int endIndex = (i == numThreads - 1) ? a.RowСount : (i + 1) * rowsPerThread;

				// Запустили задачу для каждого потока, потоки бегут парарллельно
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

		[DllImport("OpenMPDll.dll", EntryPoint = "Multiple")]
		public static extern int Multiple(double[,] a, int aRowСount, int aColumnСount,
			double[,] b, int bRowСount, int bColumnСount, int threadCount, double[,] result);

	}
}
