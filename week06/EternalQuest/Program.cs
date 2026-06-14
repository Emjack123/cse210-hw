using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EternalQuest
{
    // ==========================================
    // BASE CLASS (Inheritance & Encapsulation)
    // ==========================================
    public abstract class Goal
    {
        private string _name;
        private string _description;
        private int _points;
        private bool _isComplete;

        public string Name => _name;
        public string Description => _description;
        public int Points => _points;
        public bool IsComplete 
        { 
            get => _isComplete; 
            protected set => _isComplete = value; 
        }

        // Used to track the type for clean JSON data saving
        public abstract string Type { get; }

        protected Goal(string name, string description, int points)
        {
            _name = name;
            _description = description;
            _points = points;
            _isComplete = false;
        }

        // Abstract Method: Forces ALL derived classes to write their own scoring logic
        public abstract int RecordEvent();

         
        public virtual string GetDetailsString()
        {
            string status = _isComplete ? "[X]" : "[ ]";
            return $"{status} {_name} ({_description})";
        }
    }

    // ==========================================
    // DERIVED CLASSES (Polymorphism)
    // ==========================================
    
    // 1. SIMPLE GOAL: Uses default string, overrides abstract recording
    public class SimpleGoal : Goal
    {
        public override string Type => "SimpleGoal";

        public SimpleGoal(string name, string description, int points) 
            : base(name, description, points) { }

        public override int RecordEvent()
        {
            if (IsComplete)
            {
                Console.WriteLine("✨ This goal has already been achieved!");
                return 0;
            }
            IsComplete = true;
            return Points;
        }
    }

    //  ETERNAL GOAL: No unique variables, but overrides both methods for unique behavior
    public class EternalGoal : Goal
    {
        public override string Type => "EternalGoal";

        public EternalGoal(string name, string description, int points) 
            : base(name, description, points) { }

        public override int RecordEvent()
        {
            // Never marks complete, just awards points endlessly
            return Points;
        }

        public override string GetDetailsString()
        {
            // Custom gamification paint job: displays [∞] instead of [ ]
            return $"[∞] {Name} ({Description})";
        }
    }

    //  CHECKLIST GOAL: Defines new variables, overrides both methods
    public class ChecklistGoal : Goal
    {
        private int _target;
        private int _bonus;
        private int _amountCompleted;

        public override string Type => "ChecklistGoal";
        public int Target => _target;
        public int Bonus => _bonus;
        public int AmountCompleted { get => _amountCompleted; set => _amountCompleted = value; }

        public ChecklistGoal(string name, string description, int points, int target, int bonus) 
            : base(name, description, points)
        {
            _target = target;
            _bonus = bonus;
            _amountCompleted = 0;
        }

        public override int RecordEvent()
        {
            if (IsComplete)
            {
                Console.WriteLine("✨ This checklist is already completely finished!");
                return 0;
            }

            _amountCompleted++;
            int totalPoints = Points;

            if (_amountCompleted >= _target)
            {
                IsComplete = true;
                totalPoints += _bonus;
                Console.WriteLine($"🎉 LEVEL UP! You completed the checklist and earned a bonus of {_bonus} points!");
            }

            return totalPoints;
        }

        public override string GetDetailsString()
        {
            string status = IsComplete ? "[X]" : "[ ]";
            return $"{status} {Name} ({Description}) -- Completed {_amountCompleted}/{_target} times";
        }
    }

    // ==========================================
    // STORAGE INTERFACES (Data Transfer Objects)
    // ==========================================
    public class SaveData
    {
        public int Score { get; set; }
        public List<GoalData> Goals { get; set; } = new();
    }

    public class GoalData
    {
        public string Type { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int Points { get; set; }
        public bool IsComplete { get; set; }
        public int Target { get; set; }
        public int Bonus { get; set; }
        public int AmountCompleted { get; set; }
    }

    // ==========================================
    //  Abstraction
    // ==========================================
    public class QuestManager
    {
        private List<Goal> _goals = new();
        private int _score = 0;

        public void Start()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== ETERNAL QUEST ===");
                Console.WriteLine($"🏆 Current Score: {_score} XP\n");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. List Goals");
                Console.WriteLine("3. Record Event");
                Console.WriteLine("4. Save Goals");
                Console.WriteLine("5. Load Goals");
                Console.WriteLine("6. Quit");
                Console.Write("Select a choice from the menu: ");
                
                string? choice = Console.ReadLine()?.Trim();
                Console.WriteLine("---");

                switch (choice)
                {
                    case "1": CreateGoal(); break;
                    case "2": ListGoals(); break;
                    case "3": RecordGoalEvent(); break;
                    case "4": SaveGoals(); break;
                    case "5": LoadGoals(); break;
                    case "6": 
                        Console.WriteLine("Keep striving on your eternal quest! Goodbye.");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select 1-6.");
                        break;
                }
            }
        }

        private void CreateGoal()
        {
            Console.WriteLine("The types of Goals are:");
            Console.WriteLine("  1. Simple Goal");
            Console.WriteLine("  2. Eternal Goal");
            Console.WriteLine("  3. Checklist Goal");
            Console.Write("Which type of goal would you like to create? ");
            string? typeChoice = Console.ReadLine()?.Trim();

            Console.Write("What is the name of your goal? ");
            string name = Console.ReadLine() ?? "Unnamed Goal";
            
            Console.Write("What is a short description of it? ");
            string desc = Console.ReadLine() ?? "";

            Console.Write("What is the amount of points associated with this goal? ");
            if (!int.TryParse(Console.ReadLine(), out int points)) return;

            switch (typeChoice)
            {
                case "1":
                    _goals.Add(new SimpleGoal(name, desc, points));
                    break;
                case "2":
                    _goals.Add(new EternalGoal(name, desc, points));
                    break;
                case "3":
                    Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                    if (!int.TryParse(Console.ReadLine(), out int target)) return;
                    
                    Console.Write("What is the bonus for accomplishing it that many times? ");
                    if (!int.TryParse(Console.ReadLine(), out int bonus)) return;

                    _goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
                    break;
                default:
                    Console.WriteLine("❌ Invalid type selection.");
                    return;
            }
            Console.WriteLine("✅ Goal created successfully!");
        }

        private void ListGoals()
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("Your quest log is empty.");
                return;
            }

            Console.WriteLine("The goals are:");
            for (int i = 0; i < _goals.Count; i++)
            {
                // Dynamic Dispatch (Polymorphism) outputs strings properly per type
                Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
            }
        }

        private void RecordGoalEvent()
        {
            if (_goals.Count == 0) return;

            ListGoals();
            Console.Write("Which goal did you accomplish? ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= _goals.Count)
            {
                Goal selectedGoal = _goals[choice - 1];
                int pointsGained = selectedGoal.RecordEvent(); // Dynamic invocation
                _score += pointsGained;

                if (pointsGained > 0)
                {
                    Console.WriteLine($"🌟 Congratulations! You earned {pointsGained} points!");
                }
            }
        }

        private void SaveGoals()
        {
            Console.Write("Enter file name (e.g., goals.json): ");
            string filename = Console.ReadLine()?.Trim() ?? "goals.json";

            var data = new SaveData { Score = _score };
            foreach (var goal in _goals)
            {
                var gData = new GoalData
                {
                    Type = goal.Type, Name = goal.Name, Description = goal.Description, Points = goal.Points, IsComplete = goal.IsComplete
                };

                if (goal is ChecklistGoal cg)
                {
                    gData.Target = cg.Target;
                    gData.Bonus = cg.Bonus;
                    gData.AmountCompleted = cg.AmountCompleted;
                }
                data.Goals.Add(gData);
            }

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filename, json);
            Console.WriteLine("💾 Saved successfully!");
        }

        private void LoadGoals()
        {
            Console.Write("Enter file name to load: ");
            string filename = Console.ReadLine()?.Trim() ?? "goals.json";

            if (!File.Exists(filename)) return;

            string json = File.ReadAllText(filename);
            var data = JsonSerializer.Deserialize<SaveData>(json);

            if (data != null)
            {
                _score = data.Score;
                _goals.Clear();

                foreach (var gData in data.Goals)
                {
                    if (gData.Type == "SimpleGoal")
                    {
                        var sg = new SimpleGoal(gData.Name, gData.Description, gData.Points);
                        if (gData.IsComplete) sg.RecordEvent();
                        _goals.Add(sg);
                    }
                    else if (gData.Type == "EternalGoal")
                    {
                        _goals.Add(new EternalGoal(gData.Name, gData.Description, gData.Points));
                    }
                    else if (gData.Type == "ChecklistGoal")
                    {
                        var cg = new ChecklistGoal(gData.Name, gData.Description, gData.Points, gData.Target, gData.Bonus)
                        {
                            AmountCompleted = gData.AmountCompleted
                        };
                        if (gData.IsComplete) cg.RecordEvent();
                        _goals.Add(cg);
                    }
                }
                Console.WriteLine("📂 Loaded successfully!");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new QuestManager().Start();
        }
    }
}