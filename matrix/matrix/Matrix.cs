using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace matrix
{
    

    class Matrix
    {
        private List<List<double>> _matrix=new List<List<double>>();
        private int height, width;

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get{ return height; }
        }

        public List<List<double>>Elements
        {
            get { return _matrix; }
        }

        public Matrix(int n, int m)
        {
            height = n; width = m;
            for (int i = 0; i < n; ++i)
                _matrix.Add(new List<double>());
            foreach (var raw in _matrix)
                for (int i = 0; i < m; ++i)
                    raw.Add(0);
        }

        public void Read(string strMatrix)
        {
        }


        public static Matrix operator *( Matrix A, Matrix B)
        {
            Matrix result = new Matrix(A.Height, B.Width);
            for(int i=0; i< result.Height; i++)
                for(int j=0; j< i; j++)
                {
                    var buf = B.Elements[i][j];
                    B._matrix[i][j] = B._matrix[j][i];
                    B._matrix[j][i] = buf;
                }

            for (int i = 0; i < result.Height; i++)
                for (int j = 0; j < result.Width; j++)
                    for (int k = 0; k < A.Width; k++)
                        result._matrix[i][j] += A._matrix[i][k] * B._matrix[j][k];
            return A;
        }

        public static Matrix operator *(Matrix A, double x)
        {
            Matrix result = A;
            foreach (var raw in result._matrix)
                for (int i = 0; i < result.Width; i++)
                    raw[i] *= x;
            
            return result;
        }

        public static Matrix operator + (Matrix A, Matrix B)
        {
            Matrix Summ = new Matrix(A.Height, A.Width);
            for (int i = 0; i < Summ.Height; i++)
                for (int j = 0; j < Summ.Width; j++)
                    Summ._matrix[i][j] = A.Elements[i][j]+B.Elements[i][j];

            return Summ;
        }

    }
}
