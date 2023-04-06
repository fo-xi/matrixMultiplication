using matrixMultiplication;
using NUnit.Framework;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;

namespace UnitTest
{
	public class UnitTest
	{
		private readonly List<double[,]> _a = new List<double[,]>
		{
			new double [,] {{ 2, -3, 1},
							{ 5, 4, -2}},

			new double [,] {{ 11, -15, 1, 5, -9},
							{ 8, -8, -7, 12, -9},
							{ 9, -6, 1, 9, -9},
							{ -14, 15, 7, -5, -9},
							{ 11, 23, 7, 5, -9}},

			new double [,] {{ 1.1, -1.5, 1.5, 5.9, -9.6},
							{ 8.4, -8.56, -7.23, 12.7, -9.92},
							{ 9.1, -6.5, 1.7, 9.77, -9.5},
							{ -14.1, 15.02, 7.11, -5.4, -9.8},
							{ 11.06, 23.54, 7.88, 5.45, -9.3}},

            new double [,] {{ 1, 1.5, -1, 5.9, -9.6},
                            { -8.4, 8, 7.23, -12, 9.92},
                            { 9, 6.5, -1.7, -9.77, 9},
                            { 14.1, -15, -7, 5, 9},
                            { 11.06, -23.54, 7, -5, 9}},
		};

		private readonly List<double[,]> _b = new List<double[,]>
		{
				new double [,] {{ -7, 5},
								{ 2, -1},
								{ 4, 3}},

				new double [,] {{ 10, -17, 9, 5 },
								{ -7, 18, 4, -16 },
								{ 8, -6, 3, -13 },
								{ 11, 15, 2, 1 },
								{ -1, -23, 1, 5 }},

				new double [,] {{ 10.01, -17.24, 9.89, 5.99 },
								{ -7.22, 18.36, 4.4, -16.7 },
								{ 8.09, -6.36, 3.91, -13.5 },
								{ 11.41, 15.66, 2.2, 1.7 },
								{ -1.09, -23.54, 1.3, 5.7 }},

                new double [,] {{ -10.01, 17, -9, -5.99 },
                                { 7, -18.36, -4.4, 16 },
                                { 8.09, -6, 3.91, 13 },
                                { -11, -15, -2.2, -1.7 },
                                { 1, 23, 1.3, -5.7 }},
		};

		private readonly List<double[,]> _result = new List<double[,]>
		{
				new double [,] {{ -16, 16},
								{ -35, 15}},

				new double [,] {{ 287, -181, 43, 242 },
								{ 221, 149, 34, 226 },
								{ 248, 75, 69, 92 },
								{ -235, 598, -64, -451 },
								{ 69, 467, 213, -444 }},

				new double [,] {{ 111.759, 262.334, 10.644, -33.301 },
								{ 243.116 , 176.404, 32.187, 255.919 },
								{ 273.605, 89.592, 77.19, 102.568 },
								{ -242.998, 619.76, -70.181, -496.318 },
								{ 76.822, 495.672, 243.67, -476.994 }},

				new double [,] {{ -82.1, -313.84, -44.97, 49.7 },
								{ 340.495 , 75.1, 107.965, 236.162 },
								{ 58.127, 397.41, -83.053, -6.701 },
								{ -348.771, 689.1, -87.57, -475.259 },
								{ -154.861, 860.214, 54.106, -394.689 }},
		};

		private Matrix RoundNumber(Matrix matrix)
        {
			var result = new double[matrix.RowСount, matrix.ColumnСount];
			for (int j = 0; j < matrix.RowСount; j++)
			{
				for (int k = 0; k < matrix.ColumnСount; k++)
				{
					result[j, k] = Math.Round(matrix.Data[j, k], 3);
				}
			}
			
			return new Matrix(result);
		}

		[Test]
		public void TestMultiply()
		{
			for (int i = 0; i < _a.Count; i++)
            {
				var matrixA = new Matrix(_a[i]);
				var matrixB = new Matrix(_b[i]);
				var matrixResult = MatrixCalculator.Multiply(matrixA, matrixB);

				Assert.AreEqual(_result[i], RoundNumber(matrixResult).Data);
			};
		}

		[Test]
		public void TestParallelMultiply()
		{
			for (int i = 0; i < _a.Count; i++)
			{
				var matrixA = new Matrix(_a[i]);
				var matrixB = new Matrix(_b[i]);
				var matrixResult = MatrixCalculator.Multiply(matrixA, matrixB);

				Assert.AreEqual(_result[i], RoundNumber(matrixResult).Data);
			};
		}

		[Test]
		public void TestParallelMultiply2()
		{
			for (int i = 0; i < _a.Count; i++)
			{
				var matrixA = new Matrix(_a[i]);
				var matrixB = new Matrix(_b[i]);
				var matrixResult = MatrixCalculator.Multiply(matrixA, matrixB);

				Assert.AreEqual(_result[i], RoundNumber(matrixResult).Data);
			};
		}

        [Test]
        public void TestParallelMultiply3()
        {
            for (int i = 0; i < _a.Count; i++)
            {
                var matrixA = new Matrix(_a[i]);
                var matrixB = new Matrix(_b[i]);
                var matrixResult = MatrixCalculator.ParallelMultiply3(matrixA, matrixB);

                Assert.AreEqual(_result[i], RoundNumber(matrixResult).Data);
            };
        }
    }
}
