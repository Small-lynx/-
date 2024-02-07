int width = 23, height = 23;
Console.SetWindowSize(width+5, height+5);
int[,] mass = new int[width, height];
for (int i = 0; i < width; i++)
{
    for (int j = 0; j < height; j++)
    {
        mass[i, j] = 0;
    }
}
Random rnd = new Random();
int number = 0;
// Построение лабиринта
for (int i = 1; i < width-1; i+=2)
{
    int y;
    for (int j = 1; j < height-1; j+=2)
    {
        if (mass[y = i-1, j] == 0)
        {
            number += 1;
            mass[i, j] = number;
        }
        else
        {
            mass[i, j] = mass[y = i-1, j];
        }
        
    }
    // вертикальные стены
    for (int j = 1; j < height - 1; j+=2)
    {
        int wall = rnd.Next(0, 2);
        if (wall==0 && j+2 < height)
        {
            y = 0;
            int a = mass[i, j];
            int b = mass[i, y = j + 2];
            if (a!=b)
            {
                mass[i, y = j + 1] = mass[i, j];
                mass[i, y = j + 2] = mass[i, j];
            }
        }
    }
    // Горизонтальные стенки
    for (int j = 1; j < height - 1; j += 2)
    {
        if (i+1 == width-1)
        {
            break;
        }
        int wall = rnd.Next(0, 2);
        if (wall==1)
        {
            y = 0;
            mass[y = i + 1, j] = mass[i, j];
        }
        if (wall == 0)
        {
            y = 0;
            int countA=0, countB=0;
            for (int a = 1; a < height - 1; a+=2)
            {
                if (mass[i,j] == mass[i, a])
                {
                    countA += 1;
                }
                if (mass[i, j] == mass[y = i+1, a])
                {
                    countB += 1;
                }
            }
            if (countB > countA -2)
            {
                mass[y = i + 1, j] = mass[i, j];
            }
        }
    }
}
//Заполннение последней строки
for (int j = 1; j < height - 1; j+=2)
{
    if (j + 2 < height)
    {
        int y = 0;
        int a = mass[width-2, j];
        int b = mass[width - 2, y = j + 2];
        if (a != b)
        {
            mass[width - 2, y = j + 1] = mass[width - 2, j];
        }
    }
}
//Заполнение массива символами
char[,] mass2 = new char[width, height];
for (int i = 0; i < width; i++)
{
    for (int j = 0; j < height; j++)
    {
        if (mass[i, j] == 0)
        {
            mass2[i, j] = '\u25a0';
        }
        else
        {
            mass2[i, j] = ' ';
        }
    }
}
mass2[1, 1] = '<';
mass2[width - 2, height - 2] = '>';

int curX = 1;
int curY = 1;
ConsoleKeyInfo k;

// Движение символа
do
{
    for (int i = 0; i < width; i++)
    {
        for (int j = 0; j < height; j++)
        {
            Console.Write(mass2[i, j]);
        }
        Console.Write("\n");
    }
    Console.CursorVisible = false;
    Console.SetCursorPosition(curX, curY);
    Console.Write('e');
    k = Console.ReadKey(true);
    switch (k.Key)
    {
        case ConsoleKey.UpArrow:
            curY--;
            break;
        case ConsoleKey.DownArrow:
            curY++;
            break;
        case ConsoleKey.LeftArrow:
            curX--;
            break;
        case ConsoleKey.RightArrow:
            curX++;
            break;
    }
    Console.Clear();
    } while (k.Key != ConsoleKey.Escape); // выходим из цикла по нажатию Esc
    Console.ReadLine();