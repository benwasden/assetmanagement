using System;
using System.Threading;

namespace AssetManager {
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("--- Asset Manager ---");
        
            Thread.Sleep(1000);
            string answer = "";
            while (answer != "4") {
                Console.Clear();
                Console.WriteLine(" --- Home Screen --- ");
                Console.WriteLine();
                Console.WriteLine("Menu Options:");
                Console.WriteLine("1. Search by asset tag");
                Console.WriteLine("2. Search by owner");
                Console.WriteLine("3. Create new asset");
                Console.WriteLine("4. Close application");
                Console.Write("Select a number from the menu: ");
                answer = Console.ReadLine() ?? "";

                // Search by tag num
                if (answer == "1")
                {
                    Tag tag = new Tag();
                    tag.Run();
                }
                // Search by owner
                else if (answer == "2")
                {
                    Owner owner = new Owner();
                    owner.Run();
                }
                // Add asset
                else if (answer == "3")
                {
                    Add add = new Add();
                    add.Run();
                }
                // Exit the app
                else if (answer == "4")
                {
                    Console.Clear();
                    Console.WriteLine("Thanks for using the asset manager!");
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a valid selection.");
                    Thread.Sleep(2000);
                }
            }
        }
    }
}