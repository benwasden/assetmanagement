using System;

namespace AssetManager
{
    public class Edit
    {
        public void Run(string tag, string serial, string model, string room, string name, string status)
        {
            string dataDir = Path.Combine(AppContext.BaseDirectory, "data");
            Directory.CreateDirectory(dataDir);

            string path = Path.Combine(dataDir, "data.csv");

            // Location
            Console.Clear();
            Console.WriteLine($"Current Location: {room}");
            Console.Write("Enter the Room Number: ");
            string newRoom = Console.ReadLine() ?? room;

            // Owner
            Console.Clear();
            Console.WriteLine($"Current Owner: {name}");
            Console.Write("Enter the owner's name: ");
            string newName = Console.ReadLine() ?? name;

            // Status
            Console.Clear();
            Console.WriteLine($"Current status: {status}");
            Console.Write("Enter the asset's status: ");
            string newStatus = Console.ReadLine() ?? status;

            // Display asset to be edited
            Console.Clear();
            Console.WriteLine("--Asset to be edited--");
            Console.WriteLine();
            Console.WriteLine($"Asset Tag: {tag}");
            Console.WriteLine($"Model: {model}");
            Console.WriteLine($"Serial: {serial}");
            Console.WriteLine($"Location: {newRoom}");
            Console.WriteLine($"Owner: {newName}");
            Console.WriteLine($"Status: {newStatus}");

            Console.WriteLine();
            Console.Write("Edit this device? Y/N: ");
            string choice = Console.ReadLine() ?? "";

            // Confirm edit
            if (choice == "yes" || choice == "y" || choice == "Yes" || choice == "Y")
            {
                // Deletes the current entry for the device
                Delete delete = new Delete();
                delete.Run(tag);

                // Re-adds the device with the edited info
                string asset = $"{tag},{model},{serial},{newRoom},{newName},{newStatus}";
                File.AppendAllText(path, Environment.NewLine + asset);
                Console.Clear();
                Console.WriteLine($"{tag} edited!");
                Thread.Sleep(2000);
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Failed to edit {tag}.");
                Thread.Sleep(2000);
            }
        }
    }
}