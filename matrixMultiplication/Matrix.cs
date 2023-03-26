using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrixMultiplication
{
    public class Matrix
    {
        public int RowСount 
        { 
            get 
            { 
                return Data.GetLength(0); 
            } 
        }

        public int ColumnСount
        {
            get
            {
                return Data.GetLength(1);
            }
        }

        public double[,] Data { get; }

        public Matrix(double[,] data)
        {
            Data = data;
        }

        public Matrix(int rowСount, int columnСount)
        {
            Random random = new Random();

            Data = new double[rowСount, columnСount];

            for (int i = 0; i < rowСount; i++)
            {
                for (int j = 0; j < columnСount; j++)
                {
                    Data[i, j] = random.Next(-50, 50);
                }
            }
        }
    }
}
