namespace InventorsFileCreator
{
    internal class InventionsManager
    {
        private List<Inventor> _inventors = new List<Inventor>();
        static int lastOrderId = 1;
        int GenerateId() => lastOrderId++;
        public void AddInventor(string name)
        {
            Inventor inventor = new Inventor(name);
            inventor.Id = GenerateId();
            inventor.Inventions = inventor.CreateInventionsDictionary();
            _inventors.Add(inventor);
            Console.WriteLine("New inventor added successfully.");
        }
        public void RemoveInventor(string name)
        {
            _inventors.RemoveAll(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine("Inventor successfully removed.");
        }

        public void AddInvention(string name, string invention, int year)
        {
            var inventor = _inventors.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (inventor == null)
            {
                Console.WriteLine($"{name} not found.");
                return;
            }

            if (ContainsInvention(inventor, invention, ignoreCase: true))
            {
                Console.WriteLine($"{name} already has this invention.");
            }
            else
            {
                inventor.AddInvention(invention, year);
                Console.WriteLine($"New invention for {name} successfully added.");
            }
        }

        public void RemoveInvention(string name, string invention)
        {
            var inventor = _inventors.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (inventor == null)
            {
                Console.WriteLine($"{name} not found.");
                return;
            }

            if (!ContainsInvention(inventor, invention, ignoreCase: true))
            {
                Console.WriteLine($"{name} doesn't have this invention.");
            }
            else
            {
                inventor.DeleteInvention(invention);
                Console.WriteLine($"Invention for {name} successfully removed.");
            }
        }

        private bool ContainsInvention(Inventor inventor, string invention, bool ignoreCase = false)
        {
            if (inventor == null || inventor.Inventions == null)
                return false;

            if (ignoreCase)
            {
                return inventor.Inventions.Keys
                    .Any(x => x.Equals(invention, StringComparison.OrdinalIgnoreCase));
            }
            return inventor.Inventions.ContainsKey(invention);
        }

        public void FileCreation(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("id,name,year,invention");
                foreach (var inventor in _inventors)
                {
                    foreach (var invention in inventor.Inventions)
                    {
                        writer.WriteLine($"{inventor.Id},{inventor.Name},{invention.Value},{invention.Key}");
                    }
                }
            }
        }

        public bool ContainsInventor(string name)
        {
            return _inventors.Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
