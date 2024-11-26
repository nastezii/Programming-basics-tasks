//Command arrays:
string[] charCommands = { "all", "lower", "alpha", "alnum" };
string[] stringCommands = { "print", "set", "substr", "upper", "contains" };
string[] csvCommands = { "load", "text", "table", "entities", "get", "set", "save" };

//Initial values:
string currentRow = "";
string textFromFile = "";
string[,] convertedArray = ConvertToTable();
Inventor[] inventors = CreateStructOfInventors();

//Entry point into the program:

while (true)
{
    Console.WriteLine("Enter command:");
    string input = Console.ReadLine();
    string[] subCommands = input.Split(':');
    if (subCommands[0] == "exit")
    {
        break;
    }
    if (subCommands[0] == "char")
    {
        CharProcess(subCommands);
    }
    else if (subCommands[0] == "string")
    {
        StringProcess(subCommands);
    }
    else if (subCommands[0] == "csv")
    {
        CsvProcess(subCommands);
    }
    else
    {
        Console.WriteLine($"Class command {subCommands[0]} do not exist.");
    }
}

// 1 part :

void CharProcess(string[] subCommands)
{
    if (subCommands.Length != 2)
    {
        Console.WriteLine($"Error: shold have 2 parts in char command, but have: {subCommands.Length}.");
    }
    else
    {
        if (charCommands.Contains(subCommands[1]))
        {
            if (subCommands[1] == charCommands[0]) { CharPrintAll(); }
            else if (subCommands[1] == charCommands[1]) { CharPrintLower(); }
            else if (subCommands[1] == charCommands[2]) { CharPrintAlpha(); }
            else if (subCommands[1] == charCommands[3]) { CharPrintAlnum(); }
        }
        else
        {
            Console.WriteLine($"Command \"{subCommands[1]}\" does not exist.");
        }
    }
}

void CharPrintAll()
{
    Console.WriteLine("All ASCII table elements:");
    for (int i = 0; i <= 127; i++)
    {
        Console.WriteLine($"{i}: {Convert.ToChar(i)}");
    }
}

void CharPrintLower()
{
    Console.WriteLine("All lower ASCII table elements:");
    for (int i = 97; i <= 122; i++)
    {
        Console.WriteLine($"{i}: {Convert.ToChar(i)}");
    }
}

void CharPrintAlpha()
{
    Console.WriteLine("All alpha ASCII table elements:");
    for (int i = 65; i <= 90; i++)
    {
        Console.WriteLine($"{i}: {Convert.ToChar(i)}");
    }
    for (int i = 97; i <= 122; i++)
    {
        Console.WriteLine($"{i}: {Convert.ToChar(i)}");
    }
}

void CharPrintAlnum()
{
    Console.WriteLine("All numbers in ASCII table:");
    for (int i = 48; i <= 57; i++)
    {
        Console.WriteLine($"{i}: {Convert.ToChar(i)}");
    }
}


// 2 part:

void StringProcess(string[] subCommands)
{
    if (subCommands.Length < 2)
    {
        Console.WriteLine($"Error: shold have minimum 2 parts in string command, but have: {subCommands.Length}.");
    }
    else
    {
        if (stringCommands.Contains(subCommands[1]))
        {
            if (subCommands[1] == stringCommands[0])
            {
                StringPrint();  
            }
            if (subCommands[1] == stringCommands[1])
            {
                StringSet(subCommands[2]);             
            }
            if (subCommands[1] == stringCommands[2])
            {
                int startIndex = int.Parse(subCommands[2]);
                int lenght = int.Parse(subCommands[3]);
                StringSubstr(startIndex, lenght);
            }
            if (subCommands[1] == stringCommands[3])
            {
                StringUpper();
            }
            if (subCommands[1] == stringCommands[4])
            {
                StringContains(subCommands[2]);
            }

        }
        else
        {
            Console.WriteLine($"Command \"{subCommands[1]}\" does not exist.");
        }
    }
}

void StringPrint()
{
    Console.WriteLine($"Current row: {currentRow}\nLenght: {currentRow.Length}");
}

void StringSet(string newRow)
{
    currentRow = newRow;
}

void StringSubstr(int startIndex, int lenght)
{
    if (startIndex >= currentRow.Length || startIndex < 0)
    {
        Console.WriteLine("No element found with such index.");
    }
    else if (startIndex + lenght - 1 > currentRow.Length)
    {
        Console.WriteLine("Length exceeds the line limit.");
    }
    else
    {
        Console.WriteLine(currentRow.Substring(startIndex, lenght));
    }
}

void StringUpper()
{
    Console.WriteLine($"String with uppercase letters: {currentRow.ToUpper()}.");
}

void StringContains(string otherString)
{
    Console.WriteLine($"Does the \"{currentRow}\" contain the \"{otherString}\" line?: {currentRow.Contains(otherString)}.");
}


// 3 part:

string[,] ConvertToTable()
{
    string[] lines = textFromFile.Split("\r\n");
    string[] worlds = lines[0].Split(',');
    string[,] table = new string[lines.Length, worlds.Length];
    for (int i = 0; i < lines.Length; i++)
    {
        string[] row = lines[i].Split(',');
        for (int j = 0; j < worlds.Length; j++)
        {
            table[i, j] = row[j];
        }
    }
    return table;
}

Inventor[] CreateStructOfInventors()
{
    Inventor[] inventors = new Inventor[convertedArray.GetLength(0) - 1];
    int lenght = 0;
    for (int i = 1; i < convertedArray.GetLength(0); i++)
    {
        inventors[lenght].id = int.Parse(convertedArray[i, 0]);
        inventors[lenght].fullname = convertedArray[i, 1];
        inventors[lenght].invention = convertedArray[i, 2];
        inventors[lenght].year = int.Parse(convertedArray[i, 3]);
        lenght++;
    }
    return inventors;
}

void ConvertFromStructToTable()
{
    for (int i = 0; i < convertedArray.GetLength(0) - 1; i++)
    {
        convertedArray[i + 1, 0] = inventors[i].id.ToString();
        convertedArray[i + 1, 1] = inventors[i].fullname.ToString();
        convertedArray[i + 1, 2] = inventors[i].invention.ToString();
        convertedArray[i + 1, 3] = inventors[i].year.ToString();
    }
}

void ConvertFromTableToText()
{
    textFromFile = "";
    for (int i = 0; i < convertedArray.GetLength(0); i++)
    {
        for (int j = 0; j < convertedArray.GetLength(1); j++)
        {
            if (j == convertedArray.GetLength(1) - 1)
            {
                textFromFile = textFromFile + convertedArray[i, j];
            }
            else
            {
                textFromFile = textFromFile + convertedArray[i, j] + ',';
            }
        }
        textFromFile = textFromFile + "\r\n";
    }
}

void CsvLoad()
{
    textFromFile = File.ReadAllText("./data.csv");
}

void CsvText()
{
    Console.WriteLine("Text from data.csv:");
    Console.WriteLine(textFromFile);
}

void CsvTable()
{
    for (int i = 0; i < convertedArray.GetLength(0); i++)
    {
        for (int j = 0; j < convertedArray.GetLength(1); j++)
        {
            Console.Write(convertedArray[i, j]);
            for (int k = 1; k < 30 - convertedArray[i, j].Length; k++)
            {
                Console.Write(" ");
            }    
        }
        Console.Write('\n');
    }
}
void CsvEntities()
{
    for (int i = 0; i < inventors.Length; i++)
    {
        Console.WriteLine("Inventor:");
        Console.WriteLine($"\tId: {inventors[i].id}.");
        Console.WriteLine($"\tFull name: {inventors[i].fullname}.");
        Console.WriteLine($"\tInvention: {inventors[i].invention}.");
        Console.WriteLine($"\tYear: {inventors[i].year}.");
    }
}

void CsvGet(string input)
{
    int index = int.Parse(input);
    if (index < 0 || index > inventors.Length)
    {
        Console.WriteLine("Index is out of bonds.");
    }
    else 
    {
        Console.WriteLine("Inventor:");
        Console.WriteLine($"\tId: {inventors[index].id}.");
        Console.WriteLine($"\tFull name: {inventors[index].fullname}.");
        Console.WriteLine($"\tInvention: {inventors[index].invention}.");
        Console.WriteLine($"\tYear: {inventors[index].year}.");
    }
}

void CsvSet(string index, string field, string newValue)
{
    int row = int.Parse(index);
    if (row < 0 || row > convertedArray.GetLength(1) - 1)
    {
        Console.WriteLine("Incorrect entered index of struct.");
    }
    if (field != "id" && field != "fullname" && field != "invention" && field != "year")
    {
        Console.WriteLine("Incorrect entered field.");
    }
    if (field == "id")
    {
        inventors[row].id = int.Parse(newValue);
    }
    if (field == "fullname")
    {
        inventors[row].fullname = newValue;
    }
    if (field == "invention")
    {
        inventors[row].invention = newValue;
    }
    if (field == "year")
    {
        inventors[row].year = int.Parse(newValue);
    }
}

void CsvSave()
{
    ConvertFromStructToTable();
    ConvertFromTableToText();
    File.WriteAllText("./data.csv", textFromFile);
}

void CsvProcess(string[] subCommands)
{
    if (csvCommands.Contains(subCommands[1]))
    {
        if (subCommands[1] == csvCommands[0])
        {
            CsvLoad();
            convertedArray = ConvertToTable();
            inventors = CreateStructOfInventors();
        }
        else if (subCommands[1] == csvCommands[1])
        {
            CsvText();
        }
        else if (subCommands[1] == csvCommands[2])
        {
            CsvTable();
        }
        else if (subCommands[1] == csvCommands[3])
        {
            CsvEntities();
        }
        else if (subCommands[1] == csvCommands[4])
        {
            if (subCommands.Length <= 2)
            {
                Console.WriteLine("Can't execute comand, because you haven't entered index of struct.");
            }
            else
            {
                CsvGet(subCommands[2]);
            }
        }
        else if (subCommands[1] == csvCommands[5])
        {
            if (subCommands.Length < 5)
            {
                Console.WriteLine("Can't execute command csv(set), beсause you have entered not all data.");
            }
            else
            {
                CsvSet(subCommands[2], subCommands[3], subCommands[4]);
                ConvertFromStructToTable();
                ConvertFromTableToText();
            }
        }
        else if (subCommands[1] == csvCommands[6])
        {
            CsvSave();
        }
    }
    else 
    {
        Console.WriteLine($"Command {subCommands[1]} does not exist.");
    }
        
}

struct Inventor
{
    public int id;
    public string fullname;
    public string invention;
    public int year;
}
