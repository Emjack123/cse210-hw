using System;

public class Entry
{
    public string _date;
    public string _promptText;
    public string _entryText;

    // Constructor to initialize an entry
    public Entry(string date, string promptText, string entryText)
    {
        _date = date;
        _promptText = promptText;
        _entryText = entryText;
    }

    // Abstraction: The Journal doesn't need to know how an entry prints, 
    // it just calls this method.
    public void Display()
    {
        Console.WriteLine($"Date: {_date} - Prompt: {_promptText}");
        Console.WriteLine($"{_entryText}");
        Console.WriteLine(new string('-', 40));
    }

    //  method to format data cleanly for file saving
    public string ExportAsFileLine(string separator)
    {
        return $"{_date}{separator}{_promptText}{separator}{_entryText}";
    }
}