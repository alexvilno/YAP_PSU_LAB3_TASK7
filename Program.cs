using System;

using YAP_PSU_LAB3_TASK7;

class LAB3_TASK7
{
    public static void Main(String[] args)
    {
        Matrix M1 = new Matrix(10, "");
        Console.WriteLine("Матрица M1: ");
        Console.WriteLine(M1.ToString());

        Matrix M2 = new Matrix(10,' ');
        Console.WriteLine("Матрица M2: ");
        Console.WriteLine(M2.ToString());

        Console.WriteLine("M1 наибольшая подматрица (сумма эл-тов) = " + M1.max_sum_triple_matrix());

        Matrix M3 = new Matrix(2, 2);
        M3.FillMatrix();
        Console.WriteLine("M3: " + "\n" + M3.ToString());

        Matrix M4 = new Matrix(2, 4);
        M4.FillMatrix();
        Console.WriteLine("M4: " + "\n" + M4.ToString());

        Matrix M5 = new Matrix(4, 2);
        M5.FillMatrix();
        Console.WriteLine("M5: " + "\n" + M5.ToString());

        Matrix M6 = M3 * (M4 * M5);

        Console.WriteLine("M3 * (M4 * M5): " + "\n" + M6.ToString());
    }
}
