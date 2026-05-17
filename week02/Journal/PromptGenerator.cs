using System;
using System.Collections.Generic;

public class PromptGenerator
{
    // Enforce at least 5 distinct prompts
    private List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What is something new you discovered about yourself this week?",
        "What was the most peaceful moment of your day?"
    };

    private Random _random = new Random();

    // Abstraction: The journal just asks for a prompt, ignoring how it's picked
    public string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }
}