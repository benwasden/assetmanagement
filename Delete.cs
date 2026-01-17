using System;
using System.IO;

namespace AssetManager
{
    public class Delete
    {
        public void Run(string tag)
        {
            Inventory inventory = new Inventory();
            var assets = inventory.Run();

            // Checking if assets dictionary exists
            if (assets == null)
            {
                Console.WriteLine("The inventory couldn't be loaded.");
                return;
            }
            // Checking if requested tag exists in the dictionary
            if (!assets.ContainsKey(tag))
            {
                Console.WriteLine($"{tag} does not exist.");
            }

            // Removes the asset from the dictionary
            assets.Remove(tag);

            // Specifying path to data.csv
            string dataDir = Path.Combine(AppContext.BaseDirectory, "data");
            string path = Path.Combine(dataDir, "data.csv");

            // Attempt to delete asset from CSV
            try {
                inventory.Save(path, assets);
                Console.Clear();
                Console.WriteLine($"{tag} deleted successful!");
            } catch (Exception e)
            {
                Console.WriteLine($"Error altering inventory: {e.Message}");
            }
        }
    }
}