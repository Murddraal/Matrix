using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace matrix
{    

    class Matrix
    {
        //Операции, с которыми работает класс Matrix
        public static string[]  operations = new string[] { "Sum", "Mult", "MultMatrix" };

        private List<List<double>> _matrix=new List<List<double>>();

        private int _height, _width;

        public Matrix() { }

        public Matrix(int height, int width)
        {
            _height = height; _width = width;
            for (int i = 0; i < height; ++i)
                _matrix.Add(new List<double>());
            foreach (var raw in _matrix)
                for (int i = 0; i < width; ++i)
                    raw.Add(0);
        }

        public Matrix(List<List<double>> matrix)
        {
            _matrix = matrix;
            _height = matrix.Count;
            _width = matrix[0].Count;
        }

        public static List<List<double>> Read(int height, int width)
        {
            //создание массива, куда будет поступать информация
            List<List<double>> matrix = new List<List<double>>();
            for (int i = 0; i < height; i++)
                matrix.Add(new List<double>());

            bool flag = false;
            //цикл осуществляется до тех пор, пока матрица не будет введена корректно
            while (!flag)
            {
                for (int i = 0; i < height; i++)
                {
                    string[] strLine = Console.ReadLine().Split('\t');
                    //если длина строки оказалась меньше строки матрицы
                    if (strLine.Length != width)
                    {
                        flag = false;
                        Console.WriteLine("Введённые значения матрицы неверны. Введите матрицу заново:");
                        break;
                    }
                    //проверяются считанные значения на корректность
                    double[] line = new double[width];
                    for (int j = 0; j < width; j++)
                        if (!(flag = double.TryParse(strLine[j], out line[j])))
                            break;
                    if (!flag)
                    {
                        Console.WriteLine("Введённые значения матрицы неверны. Введите матрицу заново:");
                        break;
                    }
                    //строка из массива double преобразуется в список double и идёт в матрицу
                    matrix[i] = line.ToList<double>();
                }
            }
            return matrix;
        }

        public int Height
        {
            get { return _height; }
        }

        public int Width
        {
            get { return _width; }
        }

        public List<List<double>> Elements
        {
            get { return _matrix; }
        }

        public static Matrix operator *( Matrix A, Matrix B)
        {
            Matrix result = new Matrix(A.Height, B.Width);
            //создаётся матрица, являющаяся транспонированной матрицей B
            //умножение матрицы на транспонированную матрицу происходит быстрее, нежели на обычную, т.к. идёт перебор только по стобцам 
            #region Транспонирование
            var matrBuf = new List<List<double>>();
            for (int i = 0; i < B.Width; ++i)
            {
                matrBuf.Add(new List<double>());
                for (int j = 0; j < B.Height; ++j)
                    matrBuf[i].Add(B.Elements[j][i]);
            }                

            for(int i=0; i< B.Height; i++)
                for(int j=0; j<= i; j++)
                {
                    matrBuf[j][i] = B.Elements[i][j];
                    matrBuf[i][j] = B.Elements[j][i];
                }
            var buf = new Matrix(matrBuf);
            #endregion

            for (int i = 0; i < result.Height; i++)
                for (int j = 0; j < result.Width; j++)
                    for (int k = 0; k < A.Width; k++)
                        result._matrix[i][j] += A.Elements[i][k] * buf.Elements[j][k];
            return result;
        }

        public static Matrix operator *(Matrix A, double x)
        {
               
            return x*A;
        }

        public static Matrix operator *(double x, Matrix A)
        {
            Matrix result = A;
            for (int i = 0; i < result.Height; i++)
                for (int j = 0; j < result.Width; j++)
                    result._matrix[i][j] *= x;
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
