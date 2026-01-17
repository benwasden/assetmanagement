using System;

namespace AssetManager
{
    public class Add
    {
        public void Run()
        {
            string dataDir = Path.Combine(AppContext.BaseDirectory, "data");
            Directory.CreateDirectory(dataDir);

            string path = Path.Combine(dataDir, "data.csv");

            string tag = "";
            string serial = "";

            Inventory inventory = new Inventory();
            var assets = inventory.Run() ?? new Dictionary<string, Asset>();

            // Tag Number
            while (tag == null || tag == "" || int.TryParse(tag, out _) == false)
            {
                Console.Clear();
                Console.Write("Enter a valid numeric asset tag: ");
                tag = Console.ReadLine() ?? "";
                if (assets.ContainsKey(tag))
                {
                    tag = "";
                    Console.WriteLine("That asset tag is in use. Please pick another one.");
                    Thread.Sleep(3000);
                }
            }

            // Model name
            Console.Clear();
            Console.Write("Enter the model name: ");
            string model = Console.ReadLine() ?? "";

            // Serial
            while (serial == null || serial == "")
            {
                Console.Clear();
                Console.Write("Enter the device's serial: ");
                serial = Console.ReadLine() ?? "";
            }

            // Location
            Console.Clear();
            Console.Write("Enter the Room Number: ");
            string room = Console.ReadLine() ?? "";

            // Owner
            Console.Clear();
            Console.Write("Enter the owner's name: ");
            string name = Console.ReadLine() ?? "";

            // Status
            Console.Clear();
            Console.Write("Enter the asset's status: ");
            string status = Console.ReadLine() ?? "";

            Console.Clear();
            Console.WriteLine("--Asset to be added--");
            Console.WriteLine();
            Console.WriteLine($"Asset Tag: {tag}");
            Console.WriteLine($"Model: {model}");
            Console.WriteLine($"Serial: {serial}");
            Console.WriteLine($"Location: {room}");
            Console.WriteLine($"Owner: {name}");
            Console.WriteLine($"Status: {status}");

            Console.WriteLine();
            Console.Write("Add this device? Y/N: ");
            string choice = Console.ReadLine() ?? "";

            if (choice == "yes" || choice == "y" || choice == "Yes" || choice == "Y")
            {
                string asset = $"{tag},{model},{serial},{room},{name},{status}";
                File.AppendAllText(path, Environment.NewLine + asset);
                Console.Clear();
                Console.WriteLine("Asset added!");
                Thread.Sleep(2000);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Asset not added.");
                Thread.Sleep(2000);
            }
        }
    }
}