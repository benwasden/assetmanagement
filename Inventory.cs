using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AssetManager {
    public class Inventory
    {
        // Variable to compare header in CSV file to
        private static readonly string[] ExpectedHeader =
        {
            "tag", "model", "serial", "room", "name", "status"
        };

        // If no CSV is found or it's not in a valid format, this is how to create a CSV
        private void CreateEmptyCsv(string path)
        {
            string headerLine = string.Join(",", ExpectedHeader);
            File.WriteAllText(path, headerLine + Environment.NewLine);
        }

        // Compare the header in the CSV file to the ExpectedHeader variable above.
        private bool HeaderIsValid(string headerLine)
        {
            var actualHeader = headerLine
                .Split(',')
                .Select(h => h.Trim().ToLower())
                .ToArray();

            return actualHeader.SequenceEqual(ExpectedHeader);
        }

        // Implemented when I needed it for the delete function which was written last. Could go back and use it for edit and add as well.
        // This saves the changes in a dictionary to a CSV file by overwriting the existing data with all the info in the dictionary.
        public void Save(string path, Dictionary<string, Asset> assets)
        {
            using var writer = new StreamWriter(path);

            writer.WriteLine(string.Join(",", ExpectedHeader));

            foreach (var asset in assets.Values)
            {
                writer.WriteLine(string.Join(",",
                    asset.Tag,
                    asset.Model,
                    asset.Serial,
                    asset.Room,
                    asset.Owner,
                    asset.Status
                ));
            }
        }

        // This pulls the info from the data.csv file and puts it into a dictionary that is returned for use throughout the application.
        // It also checks the validity of the CSV file formatting and creates a new one if it's not there or in the wrong format.
        public Dictionary<string, Asset>? Run()
        {
            // data.csv file path
            string dataDir = Path.Combine(AppContext.BaseDirectory, "data");
            Directory.CreateDirectory(dataDir);

            string path = Path.Combine(dataDir, "data.csv");

            // Checks to see if file exists and creates it if it's not there
            if (!File.Exists(path))
            {
                Console.WriteLine("CSV not found. Creating new file...");
                CreateEmptyCsv(path);
                Thread.Sleep(1500);
            }

            try
            {
                var lines = File.ReadAllLines(path);

                // Check for valid header in data.csv
                if (lines.Length == 0 || !HeaderIsValid(lines[0]))
                {
                    // If invalid, offer to make a new file with the correct formatting.
                    Console.Clear();
                    Console.Write("CSV format invalid. Would you like to create a new file with the correct format? Y/N: ");
                    string choice = Console.ReadLine() ?? "";
                    if (choice == "yes" || choice == "y" || choice == "Yes" || choice == "Y")
                    {
                        Console.WriteLine("WARNING: THIS WILL ERASE YOUR DATA.CSV. ARE YOU SURE YOU WANT TO DO THIS? Y/N: ");
                        string finalChoice = Console.ReadLine() ?? "";
                        if (finalChoice == "yes" || finalChoice == "y" || finalChoice == "Yes" || finalChoice == "Y")
                        {
                            CreateEmptyCsv(path);
                            Thread.Sleep(1500);
                            return new Dictionary<string, Asset>();
                        }
                    }

                    Console.WriteLine("Operation cancelled. CSV file was not modified.");
                    Thread.Sleep(2000);
                    return null;
                }

                // If inventory is empty, return an empty dictionary.
                if (lines.Length == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Inventory is empty.");
                    Thread.Sleep(1500);
                    return new Dictionary<string, Asset>();
                }

                // If valid, create dictionary from csv file
                var assets = lines
                    .Skip(1)
                    .Select(line => line.Split(','))
                    .Where(data => data.Length == ExpectedHeader.Length)
                    .ToDictionary(
                        data => data[0],
                        data => new Asset
                        {
                            Tag = data[0],
                            Model = data[1],
                            Serial = data[2],
                            Room = data[3],
                            Owner = data[4],
                            Status = data[5]
                        }
                    );

                return assets;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading inventory: {e.Message}");
                Thread.Sleep(2000);
                return new Dictionary<string, Asset>();
            }
        }
    }
}