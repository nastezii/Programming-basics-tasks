
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

