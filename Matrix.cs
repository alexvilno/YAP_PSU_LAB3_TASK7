using System;
namespace YAP_PSU_LAB3_TASK7
{
    public class Matrix
    {
        private int[,] MatrixArray; //двумерный массив-матрица

        private int GenerateRandomOddNumberInt(int n) //генерирует случайное число длины n состоящее из нечетных цифр
        {
            Random rnd = new Random();
            string value = "";
            int temp;

            for (int i = 0; i < n; ++i)
            {
                do
                {
                    temp = rnd.Next(0,9);
                } while (temp % 2 == 0 || temp == 0);
                value += (char)(temp + 48);
            }
            return Convert.ToInt32(value);
        }

        public Matrix(int r, int c) //выделяет память по матричный массив r x c
        {
            MatrixArray = new int[r, c];
        }

        public Matrix(int n, char none) //выделяет память под матричный массив n x n и заполняет массив случайными числами длины 4, состоящими из нечетных цифр
        {
            MatrixArray = new int[n, n];

            for (int i = 0; i < MatrixArray.GetUpperBound(0) + 1; ++i)
            {
                for (int j = MatrixArray.GetUpperBound(1); j >= 0; --j)
                {
                    MatrixArray[j, i] = GenerateRandomOddNumberInt(4);
                }
            }
        }
 
        public Matrix(int n, string none) //выделяет память под матричный массив n x n и заполняет его змейкой по возрастанию от левого верхнего угла (вправо)
        {
            MatrixArray = new int[n, n];
            int k = 1;
            for (int i = 0; i < n; ++i)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < n; ++j)
                    {
                        MatrixArray[i, j] = k;
                        ++k;
                    }
                }
                else
                {
                    for (int j = n - 1; j >= 0; --j)
                    {
                        MatrixArray[i, j] = k;
                        ++k;
                    }
                }
            }
        }

        private int substr_sum(int a, int b, int k) //считает сумму элементов [a,b] k-ой подстроки в матрице
        {
            int sum = 0;
            if (a < 0 || b > MatrixArray.GetUpperBound(0) + 1) throw new Exception("Out of range");
            for (int i = a; i <= b; ++i)
            {
                sum += MatrixArray[k, i];
            }
            return sum;
        }

        public int max_sum_triple_matrix() //находит наибольшую сумму элементов подматрицы 3 x 3
        {
            int max_sum = -9999999;

            for (int i = 0; i < MatrixArray.GetUpperBound(0) - 1; ++i)
            {
                for (int j = 0; j < MatrixArray.GetUpperBound(1) - 1; ++j)
                {
                    int sum = substr_sum(j,j + 2, i) + substr_sum(j, j + 2, i + 1) + substr_sum(j, j + 2, i + 2);
                    if (sum > max_sum)
                    {
                        max_sum = sum;
                    }
                }
            }

            return max_sum;
        }

        public void FillMatrix() //заполняет матрицу по столбцам (слева направо) и по строкам (сверху вниз)
        {
            for (int i = 0; i < MatrixArray.GetUpperBound(1) + 1; ++i)
            {
                for (int j = MatrixArray.GetUpperBound(0); j >= 0; --j)
                {

                    MatrixArray[j, i] = Convert.ToInt32(Console.ReadLine());
                }
            }
        }

        public override string ToString() //перегруженный ToString(), возвращает строку в виде матрицы
        {
            string matrix = "";
            for (int i = 0; i < MatrixArray.GetUpperBound(0) + 1; ++i)
            {
                for (int j = 0; j < MatrixArray.GetUpperBound(1) + 1; ++j)
                {
                    matrix += (String.Format("{0,5}", MatrixArray[i, j]));
                }
                matrix += "\n";
            }
            return matrix;
        }

        public static Matrix operator *(Matrix a, Matrix b) //умножение матриц
        {
            if (a.MatrixArray.GetUpperBound(0) != b.MatrixArray.GetUpperBound(1)) throw new Exception("Can not multiply");
            int[,] r = new int[a.MatrixArray.GetUpperBound(0) + 1, b.MatrixArray.GetUpperBound(1) + 1];
            for (int i = 0; i < a.MatrixArray.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < b.MatrixArray.GetUpperBound(1) + 1; j++)
                {
                    for (int k = 0; k < b.MatrixArray.GetUpperBound(0) + 1; k++)
                    {
                        r[i, j] += a.MatrixArray[i, k] * b.MatrixArray[k, j];
                    }
                }
            }
            Matrix res = new Matrix(a.MatrixArray.GetUpperBound(0), b.MatrixArray.GetUpperBound(1));
            res.MatrixArray = r;
            return res;
        }
    }
}
