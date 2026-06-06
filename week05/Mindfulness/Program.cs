using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessApp
{
    // ==========================================
    // BASE CLASS: Activity
    // ==========================================
    public class Activity
    {
        private string _name;
        private string _description;
        private int _duration; // in seconds

        public Activity(string name, string description)
        {
            _name = name;
            _description = description;
        }

        protected int GetDuration()
        {
            return _duration;
        }

        public void DisplayStartingMessage()
        {
            Console.Clear();
            Console.WriteLine($"Welcome to the {_name}.\n");
            Console.WriteLine(_description);
            Console.WriteLine();
            Console.Write("How long, in seconds, would you like for your session? ");
            
            while (!int.TryParse(Console.ReadLine(), out _duration) || _duration <= 0)
            {
                Console.Write("Please enter a valid positive number of seconds: ");
            }

            Console.Clear();
            Console.WriteLine("Get ready...");
            ShowSpinner(3);
            Console.WriteLine();
        }

        public void DisplayEndingMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Well done!!");
            ShowSpinner(3);
            Console.WriteLine($"You have completed another {_duration} seconds of the {_name}.");
            ShowSpinner(4);
        }

        protected void ShowSpinner(int seconds)
        {
            List<string> spinnerAnimation = new List<string> { "|", "/", "-", "\\" };
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(seconds);

            int i = 0;
            while (DateTime.Now < endTime)
            {
                string frame = spinnerAnimation[i];
                Console.Write(frame);
                Thread.Sleep(250);
                Console.Write("\b \b"); // Erase the character

                i++;
                if (i >= spinnerAnimation.Count)
                {
                    i = 0;
                }
            }
        }

        protected void ShowCountDown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
        }
    }

    // ==========================================
    // DERIVED CLASS: BreathingActivity
    // ==========================================
    public class BreathingActivity : Activity
    {
        public BreathingActivity() : base("Breathing Activity", 
            "This activity will help you relax by talking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        {
        }

        public void Run()
        {
            DisplayStartingMessage();
            
            int duration = GetDuration();
            DateTime endTime = DateTime.Now.AddSeconds(duration);

            while (DateTime.Now < endTime)
            {
                Console.Write("Breathe in...");
                ShowCountDown(4);
                Console.WriteLine();

                Console.Write("Now breathe out...");
                ShowCountDown(6);
                Console.WriteLine("\n");
            }

            DisplayEndingMessage();
        }
    }

    // ==========================================
    // DERIVED CLASS: ReflectionActivity
    // ==========================================
    public class ReflectionActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        public ReflectionActivity() : base("Reflection Activity",
            "This activity will help you reflect on times in your life when you have shown great strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
        {
        }

        public void Run()
        {
            DisplayStartingMessage();

            Random rand = new Random();
            string prompt = _prompts[rand.Next(_prompts.Count)];

            Console.WriteLine("Consider the following prompt:\n");
            Console.WriteLine($"--- {prompt} --- \n");
            Console.WriteLine("When you have something in mind, press enter to continue.");
            Console.ReadLine();

            Console.WriteLine("Now ponder on each of the following questions as they relate to this experience.");
            Console.Write("You may begin in: ");
            ShowCountDown(5);
            Console.Clear();

            int duration = GetDuration();
            DateTime endTime = DateTime.Now.AddSeconds(duration);

            while (DateTime.Now < endTime)
            {
                string question = _questions[rand.Next(_questions.Count)];
                Console.Write($"> {question} ");
                ShowSpinner(8); // Give them 8 seconds to reflect per question
                Console.WriteLine();
            }

            DisplayEndingMessage();
        }
    }

    // ==========================================
    // DERIVED CLASS: ListingActivity
    // ==========================================
    public class ListingActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Who are those people that you appreciate?",
            "What are  your personal strengths ?",
            "Who are people that you have helped this week?",
            "When have you felt peace or inspiration this week?",
            "Who are  your personal heroes?"
        };

        public ListingActivity() : base("Listing Activity",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        {
        }

        public void Run()
        {
            DisplayStartingMessage();

            Random rand = new Random();
            string prompt = _prompts[rand.Next(_prompts.Count)];

            Console.WriteLine("List as many items as you can according to the following prompt:");
            Console.WriteLine($"--- {prompt} ---");
            Console.Write("You may begin in: ");
            ShowCountDown(5);
            Console.WriteLine();

            int itemCount = 0;
            int duration = GetDuration();
            DateTime endTime = DateTime.Now.AddSeconds(duration);

            // Keep reading lines until the time expires
            while (DateTime.Now < endTime)
            {
                // To keep the console responsive while waiting for user input near the time limit,
                // we accept input. If they are typing when the limit hits, this entry still counts.
                Console.Write("> ");
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    itemCount++;
                }
            }

            Console.WriteLine($"You listed {itemCount} items!");
            DisplayEndingMessage();
        }
    }

    // ==========================================
    // MAIN PROGRAM/MENU SYSTEM
    // ==========================================
    class Program
    {
        static void Main(string[] args)
        {
            string choice = "";
            while (choice != "4")
            {
                Console.Clear();
                Console.WriteLine("Menu Options:");
                Console.WriteLine("  1. Start breathing activity");
                Console.WriteLine("  2. Start reflection activity");
                Console.WriteLine("  3. Start listing activity");
                Console.WriteLine("  4. Quit");
                Console.Write("Select a choice from the menu: ");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BreathingActivity breathing = new BreathingActivity();
                        breathing.Run();
                        break;
                    case "2":
                        ReflectionActivity reflection = new ReflectionActivity();
                        reflection.Run();
                        break;
                    case "3":
                        ListingActivity listing = new ListingActivity();
                        listing.Run();
                        break;
                    case "4":
                        Console.WriteLine("\nGoodbye!");
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
