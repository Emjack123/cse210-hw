using System;

namespace ScriptureMemorizer
{
public class Reference
{
    private string _book;
    private int _chapter;
    private int _verse;
    private int _endVerse;

    // Single verse constructor (e.g., "John 3:16")
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
        _endVerse = verse; 
    }

    // Verse range constructor (e.g., "Proverbs 3:5-6")
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _verse = startVerse;
        _endVerse = endVerse;
    }

    public string GetDisplayText()
    {
        return _verse == _endVerse ? $"{_book} {_chapter}:{_verse}" : $"{_book} {_chapter}:{_verse}-{_endVerse}";
    }
}
}