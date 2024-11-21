void PrintArray(int[] array, string message)
{
    Console.WriteLine(message + string.Join(',', array));
}

int[] n = new int[10] { 5, -3, -2, -4, -2, -6, 2, 0, -2, 0 };
PrintArray(n, "Initial array:");

int[] ShiftedHeights(int[] array)
{
    int minValue = array.Min();
    int[] shiftedArray = new int[array.Length];
    for (int i = 0; i < array.Length; i++)
    {
        shiftedArray[i] = Math.Abs(array[i] - minValue);
    }
    return shiftedArray;
}

var shiftedArray = ShiftedHeights(n);
PrintArray(shiftedArray, "Shifted height array:");

Console.WriteLine("Enter the water level height:");
int waterLevel = int.Parse(Console.ReadLine());
var waterColumns = WaterColumns(shiftedArray, waterLevel);
PrintArray(waterColumns, "Water column heights:");

int[] WaterColumns(int[] array, int waterLevel)
{
    int[] waterColumns = new int[array.Length];
    for (int i = 0; i < array.Length; i++)
    {
        if (array[i] <= waterLevel)
        {
            waterColumns[i] = 0;
        }
        else
        {
            waterColumns[i] = array[i] - waterLevel;
        }
    }
    return waterColumns;
}

int GetMaxWaterArea(int[] array1, int[] array2)
{
    int[] waterAreas = new int[array1.Length];
    for (int i = 0; i < array1.Length - 1; i++ )
    {
        if (array1[i] > 0 )
        {
            int area = 0;
            int j = 0;
            while (array1[i] == array1[i+j+1] && array1[i] > array2[i] )
            {
                area++;
                j++;
            }
            waterAreas[i] = area;
        }

        else
        {
            waterAreas[i] = 0;
        }
    }
    int max = waterAreas.Max();
    return max;
}
Console.WriteLine($"Surface area of ​​the largest water reservoir:{GetMaxWaterArea(waterColumns, shiftedArray)}");
PrintBackView(shiftedArray, waterLevel);

void PrintBackView(int[] array, int waterLevel)
{
    int max = array.Max();
    int length = array.Length;
    for (int i = 1; i <= length + 2; i++) { Console.Write("-"); }
    Console.Write(max + 1);
    Console.WriteLine();
    for (int i = max; i > waterLevel; i--)
    {
        for (int j = 0; j < length; j++)
        {
            if (array[j] >= i)
            {
                if (j == 0) { Console.Write("|N"); }
                else if (j == length - 1) { Console.Write("N|"); }
                else { Console.Write("N"); }
            }
            else
            {
                if (j == 0) { Console.Write("| "); }
                else if (j == length - 1) { Console.Write(" |"); }
                else { Console.Write(" "); }
            }
        }
        Console.Write(i);
        Console.WriteLine();
    }
    for (int i = waterLevel; i >= 1; i--)
    {
        for (int j = 0; j < length; j++)
        {
            if (array[j] >= i)
            {
                if (j == 0) { Console.Write("|N"); }
                else if (j == length - 1) { Console.Write("N|"); }
                else { Console.Write("N"); }
            }
            else
            {
                if (j == 0) { Console.Write("|~"); }
                else if (j == length - 1) { Console.Write("~|"); }
                else { Console.Write("~"); }
            }
        }
        if (i == waterLevel)
        {
            Console.Write($"{i}    (water level)");
            Console.WriteLine();
        }
        else
        {
            Console.Write(i);
            Console.WriteLine();
        }
    }
    for (int i = 0; i <= length + 2; i++) { Console.Write("-"); }
    Console.Write("0");
    Console.WriteLine();
}
