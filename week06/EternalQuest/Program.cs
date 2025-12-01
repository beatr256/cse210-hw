using System;
using System.Collections.Generic;
using System.IO;

// Base Goal class
abstract class Goal
{
    protected string Name;
    protected string Description;
    protected int Points;
    
    // FIX APPLIED HERE: Changed 'protected set' to 'set' (which is public) 
    // to allow deserialization in LoadGoals() from the Program class.
    public bool Completed { get; set; } 

    public Goal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
        Completed = false;
    }

    public abstract int RecordEvent();
    public abstract string GetStringRepresentation();
    public abstract void DisplayGoal();
}

// Simple Goal: completed once
class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) : base(name, description, points) { }

    public override int RecordEvent()
    {
        if (!Completed)
        {
            Completed = true;
            Console.WriteLine($"Congrats! You earned {Points} points for completing {Name}!");
            return Points;
        }
        else
        {
            Console.WriteLine($"{Name} is already completed.");
            return 0;
        }
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal:{Name},{Description},{Points},{Completed}";
    }

    public override void DisplayGoal()
    {
        Console.WriteLine($"[{(Completed ? "X" : " ")}] {Name} ({Description})");
    }
}

// Eternal Goal: can be repeated indefinitely
class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points) { }

    public override int RecordEvent()
    {
        Console.WriteLine($"You earned {Points} points for doing {Name}!");
        return Points;
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{Name},{Description},{Points}";
    }

    public override void DisplayGoal()
    {
        Console.WriteLine($"[∞] {Name} ({Description})");
    }
}

// Checklist Goal: requires multiple completions for bonus
class ChecklistGoal : Goal
{
    private int TargetCount;
    private int CurrentCount;
    private int Bonus;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonus) : base(name, description, points)
    {
        TargetCount = targetCount;
        Bonus = bonus;
        CurrentCount = 0;
    }

    public override int RecordEvent()
    {
        if (!Completed)
        {
            CurrentCount++;
            Console.WriteLine($"Progress: {CurrentCount}/{TargetCount} for {Name}. You earned {Points} points!");

            if (CurrentCount >= TargetCount)
            {
                Completed = true;
                Console.WriteLine($"Goal {Name} completed! Bonus {Bonus} points awarded!");
                return Points + Bonus;
            }
            return Points;
        }
        else
        {
            Console.WriteLine($"{Name} is already completed.");
            return 0;
        }
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{Name},{Description},{Points},{CurrentCount},{TargetCount},{Bonus},{Completed}";
    }

    public override void DisplayGoal()
    {
        Console.WriteLine($"[{(Completed ? "X" : " ")}] {Name} ({Description}) Completed {CurrentCount}/{TargetCount} times");
    }
}

// Main Program
class Program
{
    static List<Goal> goals = new List<Goal>();
    static int totalPoints = 0;
    const string fileName = "goals.txt";

    static void Main(string[] args)
    {
        LoadGoals();

        while (true)
        {
            Console.WriteLine("\nEternal Quest Menu:");
            Console.WriteLine($"You have {totalPoints} points."); // Display score in menu for convenience
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Display Goals");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    RecordEvent();
                    break;
                case "3":
                    DisplayGoals();
                    break;
                case "4":
                    SaveGoals();
                    break;
                case "5":
                    SaveGoals();
                    return;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }

    static void CreateGoal()
    {
        Console.WriteLine("Select Goal Type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Choice: ");
        string type = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter description: ");
        string description = Console.ReadLine();
        Console.Write("Enter points: ");
        int points = int.Parse(Console.ReadLine());

        switch (type)
        {
            case "1":
                goals.Add(new SimpleGoal(name, description, points));
                break;
            case "2":
                goals.Add(new EternalGoal(name, description, points));
                break;
            case "3":
                Console.Write("Enter target count: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus points: ");
                int bonus = int.Parse(Console.ReadLine());
                goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                break;
            default:
                Console.WriteLine("Invalid type");
                break;
        }
    }

    static void RecordEvent()
    {
        DisplayGoals();
        if (goals.Count == 0) return; // Exit if no goals
        
        Console.Write("Enter the number of the goal you accomplished: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= goals.Count)
        {
            totalPoints += goals[index - 1].RecordEvent();
        }
        else
        {
            Console.WriteLine("Invalid goal number.");
        }
    }

    static void DisplayGoals()
    {
        Console.WriteLine("\nGoals:");
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals currently saved.");
            return;
        }
        for (int i = 0; i < goals.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            goals[i].DisplayGoal();
        }
    }

    static void SaveGoals()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(totalPoints);
                foreach (var goal in goals)
                {
                    writer.WriteLine(goal.GetStringRepresentation());
                }
            }
            Console.WriteLine("Goals saved successfully!");
        }
        catch (Exception ex)
        {
             Console.WriteLine($"Error saving goals: {ex.Message}");
        }
    }

    static void LoadGoals()
    {
        if (File.Exists(fileName))
        {
            try
            {
                string[] lines = File.ReadAllLines(fileName);
                if (lines.Length == 0) return;

                totalPoints = int.Parse(lines[0]);
                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] parts = line.Split(':', 2);
                    string type = parts[0];
                    string data = parts[1];

                    string[] values = data.Split(',');

                    switch (type)
                    {
                        case "SimpleGoal":
                            // This line now works because Completed has a public setter.
                            goals.Add(new SimpleGoal(values[0], values[1], int.Parse(values[2])) { Completed = bool.Parse(values[3]) });
                            break;
                        case "EternalGoal":
                            goals.Add(new EternalGoal(values[0], values[1], int.Parse(values[2])));
                            break;
                        case "ChecklistGoal":
                            ChecklistGoal cg = new ChecklistGoal(values[0], values[1], int.Parse(values[2]), int.Parse(values[4]), int.Parse(values[5]));
                            
                            // This section is kept as you wrote it to handle the private CurrentCount field
                            var currentCountField = typeof(ChecklistGoal).GetField("CurrentCount", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                            currentCountField.SetValue(cg, int.Parse(values[3]));
                            
                            // This line now works because Completed has a public setter.
                            cg.Completed = bool.Parse(values[6]);
                            goals.Add(cg);
                            break;
                    }
                }
                Console.WriteLine("Goals loaded successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading goals: {ex.Message}. Starting with fresh score/goals.");
                goals.Clear();
                totalPoints = 0;
            }
        }
    }
}