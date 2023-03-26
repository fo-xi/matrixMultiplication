using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            for (int i = 0; i < a.RowСount; i++)
            {
                for (int j = 0; j < b.ColumnСount; j++)
                {
                    for (int k = 0; k < a.ColumnСount; k++)
                    {
                        result[i, j] += a.Data[i, k] * b.Data[k, j];
                    }
                }
            }

            return new Matrix(result);
        }
    }
}
