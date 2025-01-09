namespace InventorsFileCreator
{
    internal class Inventor
    {
        string _name;
        int id ;
        Dictionary<string, int> _inventions;
        public string Name { get { return _name; } }
        public Dictionary<string, int> Inventions { get { return _inventions; } set { _inventions = value; } }
        public int Id { get; set; }
        public Inventor(string name) {_name = name;}
        public Inventor() { }

        public void AddInvention(string name, int year)
        {
            _inventions.Add(name, year);
        }

        public void DeleteInvention(string name)
        { 
            _inventions.Remove(name);
        }

        public Dictionary<string, int> CreateInventionsDictionary()
        {
            Dictionary<string, int> inventions = new Dictionary<string, int>();
            bool flag = true;

            while (flag)
            {
                Console.WriteLine("Do you want to add an invention? (y/n):");
                string option = Console.ReadLine();

                if (option.Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Enter the name of the invention:");
                    string name = Console.ReadLine();
                    if (inventions.ContainsKey(name))
                    {
                        Console.WriteLine("This invention already exists. Please enter another one.");
                        continue;
                    }

                    Console.WriteLine("Enter the year of invention:");
                    string input = Console.ReadLine();

                    while (!int.TryParse(input, out int year) || year < 0)
                    {
                        Console.WriteLine("Incorrect input, try again:");
                        input = Console.ReadLine();
                    }
                    inventions.Add(name, int.Parse(input));
                    Console.WriteLine("Invention added successfully!");
                }
                else if (option.Equals("n", StringComparison.OrdinalIgnoreCase))
                {
                    flag = false;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please enter 'y' to add a new invention or 'n' to exit.");
                }
            }
            return inventions;
        }
    }
}
