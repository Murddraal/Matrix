using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrix
{
    class Program
    {
        static void Main(string[] args)
        {

            string operation;
            Console.WriteLine("Данная программа реализует следующие операции над матрицами: Sum, Mult, MultMatrix.");
            Console.WriteLine("Все вводимые числа дожны отедляться друг от друга табуляцией.");
            Console.WriteLine("Введите тип операции (Sum, Mult, MultMatrix):");

            //проверка операции на корректность. цикл работает до получения корректных данных
            while (true)
                if (!Matrix.operations.Contains(operation = Console.ReadLine()))
                    Console.WriteLine("Операция введена неверно! Попробуйте снова");
                else
                    break;

            var result=new Matrix();

            switch (operation)
            {
                case "Sum":
                    {
                        Console.WriteLine("Введите размерность матриц одной строкой:");
                        int height = 0, width = 0;
                        while (true)
                        {
                            string[] strSize = Console.ReadLine().Split('\t');
                            //считываем строку, содеражащую высоту и ширину матрицы. Строка разбивается на подстроки, разделитель - \t. 
                            //Если данные некорректны, то осуществляется повторный ввод данных
                            if (strSize.Length != 2)
                            {
                                Console.WriteLine("Неверное кол-во параметров! Попробуйте снова");
                                continue;
                            }
                            if (!(int.TryParse(strSize[0], out height) && int.TryParse(strSize[1], out width)) || !(height > 0 && width > 0))
                                Console.WriteLine("Размерность введена неверно! Попробуйте снова:");                            
                            else
                                break;
                        }

                        Console.WriteLine("Введите первую матрицу:");

                        var matrixA = new Matrix(Matrix.Read(height, width));

                        Console.WriteLine("Ввелите вторую матрицу:");

                        var matrixB = new Matrix(Matrix.Read(height, width));

                        result = matrixA + matrixB;
                        break;
                    }
                case "Mult":
                    {
                        Console.WriteLine("Введите размерность матрицы:");
                        int height = 0, width = 0;
                        while (true)
                        {
                            var strSize = Console.ReadLine().Split('\t');

                            if (strSize.Length != 2)
                            {
                                Console.WriteLine("Неверное кол-во параметров! Попробуйте снова");
                                continue;
                            }
                            if (!(int.TryParse(strSize[0], out height) && int.TryParse(strSize[1], out width))|| !(height > 0 && width > 0))
                                Console.WriteLine("Размерность введена неверно! Попробуйте снова");                            
                            else
                                break;
                        }

                        Console.WriteLine("Ввелите матрицу:");

                        var matrix = new Matrix(Matrix.Read(height, width));

                        Console.WriteLine("Ввелите число, на которое хотите умножить матрицу:");
                        bool flag = false;
                        double numb = 0;

                        while (!flag)
                        {
                            string strNumb;
                            if (!(flag = double.TryParse(strNumb = Console.ReadLine(), out numb)))
                                Console.WriteLine("Число введено неверно! Попробуйте снова");
                            else
                                break;
                        }

                        result = matrix * numb;
                        break;
                    }
                case "MultMatrix":
                    {
                        Console.WriteLine("Введите три числа, задающих размерность матриц(второе число является шириной первой матрицы и высотой второй):");
                        int heightA = 0, widthHeight = 0, widthB = 0;

                        while (true)
                        {
                            var strSize = Console.ReadLine().Split('\t');
                            if (strSize.Length != 3)
                            {
                                Console.WriteLine("Неверное кол-во параметров! Попробуйте снова");
                                continue;
                            }
                            //считываем строку, сожеражащую высоту и ширину матрицы. Строка разбивается на подстроки, разделитель - \t. 
                            //Если данные некорректны, то нужно ввест
                            if (!(int.TryParse(strSize[0], out heightA) && int.TryParse(strSize[1], out widthHeight) && int.TryParse(strSize[2], out widthB)) || !(heightA > 0 && widthB > 0 && widthHeight > 0))
                                Console.WriteLine("Размерность введена неверно! Попробуйте снова:");                            
                            else
                                break;
                        }                       

                        Console.WriteLine("Ввелите первую матрицу:");
                        var matrixA = new Matrix(Matrix.Read(heightA, widthHeight));

                        Console.WriteLine("Ввелите вторую матрицу:");
                        var matrixB = new Matrix(Matrix.Read(widthHeight, widthB));

                        result = matrixA * matrixB;
                        break;
                    }
            }

            Console.WriteLine("Результат:");

            for (int i = 0; i < result.Height; ++i)
            {
                for (int j = 0; j < result.Width; ++j)
                    Console.Write(result.Elements[i][j].ToString() + '\t');
                Console.WriteLine();
            }
        }
    }
}
