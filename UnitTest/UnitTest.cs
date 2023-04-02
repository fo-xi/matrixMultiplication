using matrixMultiplication;
using NUnit.Framework;
using System.Runtime.InteropServices;
using System;

namespace UnitTest
{
	public class UnitTest
	{
		private readonly double[,] _a = new double[,]
		{
				{ 2, -3, 1},
				{ 5, 4, -2}
		};

		private readonly double[,] _b = new double[,]
		{
				{ -7, 5},
				{ 2, -1},
				{ 4, 3}
		};

		private readonly double[,] _result = new double[,]
		{
				{ -16, 16},
				{ -35, 15}
		};

		[Test]
		public void TestMultiply()
		{
			var matrixA = new Matrix(_a);
			var matrixB = new Matrix(_b);
			var matrixResult = MatrixCalculator.Multiply(matrixA, matrixB);

			Assert.AreEqual(_result, matrixResult.Data);
		}

		[Test]
		public void TestParallelMultiply()
		{
			var matrixA = new Matrix(_a);
			var matrixB = new Matrix(_b);
			var matrixResult = MatrixCalculator.ParallelMultiply(matrixA, matrixB);

			Assert.AreEqual(_result, matrixResult.Data);
		}

		[Test]
		public void TestParallelMultiply2()
		{
			var matrixA = new Matrix(_a);
			var matrixB = new Matrix(_b);
			var matrixResult = MatrixCalculator.ParallelMultiply2(matrixA, matrixB);

			Assert.AreEqual(_result, matrixResult.Data);
		}

		[Test]
		public void TestParallelMultiply3()
		{
			var matrixA = new Matrix(_a);
			var matrixB = new Matrix(_b);
			var matrixResult = MatrixCalculator.ParallelMultiply3(matrixA, matrixB);

			Assert.AreEqual(_result, matrixResult.Data);
		}
	}
}
