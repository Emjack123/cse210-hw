using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    public List<Entry> _entries = new List<Entry>();
    public string _fileSeparator = "~|~";

    // List of prompts built directly into the management system
    public List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What is something new you discovered about yourself this week?"
    };

    public void AddEntry()
    {
        // 1. Get a random prompt
        Random random = new Random();
        int index = random.Next(_prompts.Count);
        string chosenPrompt = _prompts[index];

        Console.WriteLine($"\nPrompt: {chosenPrompt}");
        Console.Write("> ");
        string userResponse = Console.ReadLine();

        // 2. Get the current date automatically
        string currentDate = DateTime.Now.ToShortDateString();

        // 3. Create and add the new entry object
        Entry newEntry = new Entry(currentDate, chosenPrompt, userResponse);
        _entries.Add(newEntry);
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("\nYour journal is currently empty.");
            return;
        }

        Console.WriteLine("\n=== Journal Entries ===");
        foreach (Entry entry in _entries)
        {
            entry.Display(); // Delegating the display responsibility to the Entry class
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                outputFile.WriteLine(entry.ExportAsFileLine(_fileSeparator));
            }
        }
        Console.WriteLine($"Journal successfully saved to {filename}");
    }

    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("Error: That file does not exist.");
            return;
        }

        // Clear existing entries to replace them completely
        _entries.Clear();

        string[] lines = File.ReadAllLines(filename);
        foreach (string line in lines)
        {
            // Splitting line using our custom unique separator
            string[] parts = line.Split(new string[] { _fileSeparator }, StringSplitOptions.None);
            
            if (parts.Length == 3)
            {
                string date = parts[0];
                string prompt = parts[1];
                string text = parts[2];

                Entry loadedEntry = new Entry(date, prompt, text);
                _entries.Add(loadedEntry);
            }
        }
        Console.WriteLine($"Journal successfully loaded from {filename}");
    }
}