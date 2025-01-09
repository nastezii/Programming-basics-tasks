using InventorsFileCreator;
InventionsManager inventionsManager = new InventionsManager();

if (args.Length != 1)
{
    Console.WriteLine("Invalid number of arguments.");
    return;
}

string filePath = args[0];

Console.WriteLine("Commands: \nAdd inventor\nRemove inventor\nAdd invention\nRemove invention\nCreate a file");
Console.WriteLine("To finish enter \"End\".");
bool running = true;
string option;
while (running)
{
    Console.Write("\nEnter command: ");
    option = Console.ReadLine();

    if (option.Equals("Add inventor", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Enter the name:");
        string name = Console.ReadLine();
        if (inventionsManager.ContainsInventor(name))
        {
            Console.WriteLine("This inventor already exists.");
        }
        else
        {
            inventionsManager.AddInventor(name);
        }
    }
    else if (option.Equals("Remove inventor", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Enter the name:");
        string name = Console.ReadLine();
        if (!inventionsManager.ContainsInventor(name))
        {
            Console.WriteLine("This inventor is not in the database.");
        }
        else
        {
            inventionsManager.RemoveInventor(name);
        }
    }
    else if (option.Equals("Add invention", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Enter the name of inventor:");
        string name = Console.ReadLine();
        if (!inventionsManager.ContainsInventor(name))
        {
            Console.WriteLine("This inventor is not in the database.");
        }
        else
        {
            Console.WriteLine("Enter the name of invention:");
            string item = Console.ReadLine();
            Console.WriteLine("Enter the year of invention:");
            string input = Console.ReadLine();
            while (!int.TryParse(input, out int num) || num < 0)
            {
                Console.WriteLine("Incorrect input, try again:");
                input = Console.ReadLine();
            }
            int year = int.Parse(input);
            inventionsManager.AddInvention(name,item,year);
        }
    }
    else if (option.Equals("Remove invention", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Enter the name of investor:");
        string name = Console.ReadLine();
        if (!inventionsManager.ContainsInventor(name))
        {
            Console.WriteLine("This inventor is not in the database.");
        }
        else
        {
            Console.WriteLine("Enter the name of investion:");
            string item = Console.ReadLine();
            inventionsManager.RemoveInvention(name, item);
        }
    }
    else if (option.Equals("Create a file", StringComparison.OrdinalIgnoreCase))
    {
        inventionsManager.FileCreation(filePath);
    }
    else if (option.Equals("End", StringComparison.OrdinalIgnoreCase))
    {
        running = false;
        Console.WriteLine("Exiting the program.");
    }
    else
    {
        Console.WriteLine("Invalid option. Please, try again.");
    }
}


