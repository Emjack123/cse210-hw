using System;
using System.Collections.Generic;
using System.IO;

namespace ScriptureMemorizer
{

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public void HideRandomWords(int numberToHide)
    {
        Random random = new Random();
        // Stretch Challenge: Filter to only words that are NOT hidden
        List<Word> visibleWords = _words.Where(w => !w.IsHidden()).ToList();
        int actualToHide = Math.Min(numberToHide, visibleWords.Count);

        for (int i = 0; i < actualToHide; i++)
        {
            int randomIndex = random.Next(visibleWords.Count);
            visibleWords[randomIndex].Hide();
            visibleWords.RemoveAt(randomIndex); // Don't pick the same word twice in one turn
        }
    }

    public string GetDisplayText()
    {
        string textBlock = string.Join(" ", _words.Select(w => w.GetDisplayText()));
        return $"{_reference.GetDisplayText()} - {textBlock}";
    }

    public bool IsCompletelyHidden() => _words.All(w => w.IsHidden());
}
}