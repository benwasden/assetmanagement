using System;

namespace AssetManager
{
    // These should probably have been consolidated under one "Search" class, with Tag and Owner being methods underneath them.
    // Search by tag
    public class Tag
    {
        Inventory inventory = new Inventory();
        
        public void Run()
        {
            // Check to see if data.csv exists and if it has any info
            var assets = inventory.Run();
            if (assets == null || assets.Count == 0)
            {
                return;
            }
            Console.Clear();
            Console.Write("Please enter an asset tag: ");
            string tag = Console.ReadLine() ?? "";
            
            // Find asset in list by tag number
            if (assets.TryGetValue(tag, out var asset))
            {
                Console.WriteLine($"Model: {asset.Model}");
                Console.WriteLine($"Serial: {asset.Serial}");
                Console.WriteLine($"Owner: {asset.Owner}");
                Console.WriteLine($"Room Number: {asset.Room}");

                Console.WriteLine();
                Console.WriteLine("Would you like to:");
                Console.WriteLine("1. Edit Asset");
                Console.WriteLine("2. Delete Asset");
                Console.Write("Enter the number or any other key to exit: ");
                string choice = Console.ReadLine() ?? "";

                // Edit or delete the asset
                if (choice == "1")
                {
                    Edit edit = new Edit();
                    edit.Run(tag, asset.Serial, asset.Model, asset.Room, asset.Owner, asset.Status);
                }
                else if (choice == "2")
                {
                    Console.Write("Are you sure you want to delete this asset? Y/N: ");
                    string choice2 = Console.ReadLine() ?? "";
                    if (choice2 == "yes" || choice2 == "y" || choice2 == "Yes" || choice2 == "Y") {
                        Delete delete = new Delete();
                        delete.Run(tag);
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("Asset not found.");
                Thread.Sleep(1500);
            }
        }
    }
    // Search by owner
    public class Owner
    {
        Inventory inventory = new Inventory();
        
        public void Run()
        {
            var assets = inventory.Run();
            // Check to see if data.csv exists and if it has any info
            if (assets == null || assets.Count == 0)
            {
                return;
            }
            Console.Clear();
            Console.Write("Enter the owner's name: ");
            string owner = Console.ReadLine() ?? "";
            
            // Go through assets dictionary and see if the requested owner's name is there, regardless of the user's capitalization.
            var matches = assets.Values.Where(a => a.Owner.Equals(owner, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matches.Count == 0)
            {
                Console.WriteLine($"No assets assigned to {owner}.");
                Thread.Sleep(2000);
            }
            else
            {
                // No owner listed = unassigned assets. If there is one, say it's their assets.
                if (owner == "")
                {
                    Console.WriteLine("Unassigned Assets:");
                }
                else {
                    Console.WriteLine($"{owner}'s Assets:");
                }


                foreach (var asset in matches)
                {
                    Console.WriteLine($"{asset.Tag} - {asset.Model} - {asset.Status}");
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to return home...");
                Console.ReadKey();
            }
        }
    }
}