using System;

namespace ScriptureMemorizer
{
class Program
{
    static void Main(string[] args)
    {
        // Setup initial scripture (Using the multi-verse constructor)
        Reference reference = new Reference("Proverbs", 3, 5, 6);
        string text = "Trust in the Lord with all thine heart and lean not unto thine own understanding";
        Scripture scripture = new Scripture(reference, text);

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();

            // Ending program automatically if everything is hidden
            if (scripture.IsCompletelyHidden())
            {
                Console.WriteLine("All words hidden! Good luck memorizing!");
                break;
            }

            Console.WriteLine("Press Enter to hide more words, or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.Trim().ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWords(3); // Hides 3 words at a time
        }
    }
}
}