# Лабораторная работа №3 по ЯП ПГНИУ, задание 7
## Постановка задачи
Выполнить все три задания (ЛР 21, 22, 23) в виде методов одного класса. Задания ЛР 21 реализовать в виде конструкторов (кроме них, могут быть и другие конструкторы). Класс содержит единственное поле – двумерный массив. Перегрузить метод ToString() — сформировать строку из двумерного массива для отображения его на экране в виде таблицы.


*UML диаграмма класса:*


# Класс Matrix
## Поля
Класс содержит поля:
```c#
private int[,] MatrixArray;  //двумерный массив-матрица
```

## Конструкторы
Конструктор **от двух целочисленных параметров** выделяет память для массива (r x c) и инициализирует матричный массив нулями:

```c#
public Matrix(int r, int c) //выделяет память по матричный массив r x c
{
    MatrixArray = new int[r, c];
}
```

Конструкторы **от одного параметра n** выделяют память под матрицу n x n и инициализируют её особым образом (в соответсвтии с условиями задачи) в зависимости от фиктивного параметра:

```c#
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
```

В силу невозможности реализации двух разных конструкторов от одних параметров введет *второй фиктивный параметр*. Тут в зависимости от *второго фиктивного параметра* избирается принцип инициализации матрицы. Если параметр типа ```char``` &mdash; то матрица заполняется случайными числами длины 4 (без четных цифр), а если параметр типа ```string``` &mdash; то матрица заполняется змейкой. 

## Методы
```c#
private int GenerateRandomOddNumberInt(int n); //генерирует случайное число длины n состоящее из нечетных цифр;
private int substr_sum(int a, int b, int k); //считает сумму элементов [a,b] k-ой подстроки в матрице
public int max_sum_triple_matrix(); //находит наибольшую сумму элементов подматрицы 3 x 3
public void FillMatrix(); //заполняет матрицу по столбцам (слева направо) и по строкам (сверху вниз)
public void PrintMatrix(); //вывод матрицы
```

## Перегруженные операции
Для удобства я перегрузил операцию умножения для матриц, исходя из определения ниже:

![image](https://sun9-83.userapi.com/impg/m3W7s1KXC37jl02rtzKSucq5-5wgPE4qKDsVWQ/joTBgHwLSs4.jpg?size=1280x776&quality=96&sign=59180f86417592438d4769aa243d524f&type=album)

Выглядит это так:

```c#
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
```

## Пару слов о лабораторной работе 22, 23
22. В данном варианте исполнения работы использован самый тривиальный метод перебора, хотя конечно можно увеличить скорость поиска такой подматрицы в лучшем случае в 9 раз. 
23. Тут требовалось реализовать умножение матриц в соответсвтии с определением умножения для матриц. Дистрибутивности нет, при попытке умножить неумножаемые матрицы выбросится исключение. 

## Тесты
Для начала протестируем работу на ЛР 21. Выведем матрицу змейкой, а потом создадим матрицу, заполненую случайными 4 значными числами без четных цифр:

```c#
Matrix M1 = new Matrix(10, "");
Console.WriteLine("Матрица M1: ");
Console.WriteLine(M1.ToString());
```

*Вывод:*

```
Матрица M1: 
    1    2    3    4    5    6    7    8    9   10
   20   19   18   17   16   15   14   13   12   11
   21   22   23   24   25   26   27   28   29   30
   40   39   38   37   36   35   34   33   32   31
   41   42   43   44   45   46   47   48   49   50
   60   59   58   57   56   55   54   53   52   51
   61   62   63   64   65   66   67   68   69   70
   80   79   78   77   76   75   74   73   72   71
   81   82   83   84   85   86   87   88   89   90
  100   99   98   97   96   95   94   93   92   91
```
Вывод змейкой выполнен в соответствии с заданием.

```c#
Matrix M2 = new Matrix(10,' ');
Console.WriteLine("Матрица M2: ");
Console.WriteLine(M2.ToString());
```
*Вывод:*
```
Матрица M2: 
 3175 3713 5751 5551 5177 5115 1733 7517 5317 1713
 1357 1155 3377 5357 7515 5153 7137 1351 1557 5173
 1153 5771 1151 7137 7751 1533 1111 7517 1153 3355
 7735 5511 5517 7373 7755 1755 7577 3537 5515 5313
 1515 3513 3333 1735 7535 5375 5773 7177 5171 7577
 5373 7113 1733 3331 1713 7711 7551 3335 3337 7311
 5571 7155 7531 1751 7153 5711 5175 1155 1713 1337
 5711 1511 3711 7531 3313 1513 7573 5717 5171 1111
 7353 1571 3517 3111 1577 5515 3731 3131 3351 5517
 3537 1517 5353 1131 3371 3153 5757 3373 7377 5175
```

В матрице M1 с помощью метода ```public int max_sum_triple_matrix();``` найдем максимальную(наибольшая сумма элементов) подматрицу размера 3 х 3: 

```c#
Console.WriteLine("M1 наибольшая подматрица (сумма эл-тов) = " + M1.max_sum_triple_matrix());
```
*Вывод:*

```
M1 наибольшая подматрица (сумма эл-тов) = 780
```

Это будет решением ЛР 22.

Теперь создадим и заполним к примеру матрицы 2 x 2, 2 x 4, 4 x 2 и перемножим их в соответсвтии с заданием ЛР 23:
```c#
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
```
*Вывод:*
```
M3: 
    2    4
    1    3
    
M4:
    2    4    6    8
    1    3    5    7
    
M5: 
    4    8
    3    7
    2    6
    1    5
    
M3 * (M4 * M5): 
  200  616
  130  402
```

Все отлично работает.
