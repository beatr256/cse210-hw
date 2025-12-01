using System;
using EternalQuest;

class Program
{
    // Exceeding Requirements:
    // Added a gamification leveling system with titles:
    // Seeker, Apprentice, Disciple, Master, Eternal Champion.
    // Encourages the user with progression mechanics.

    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        int choice = 0;

        while (choice != 6)
        {
            manager.DisplayPlayerInfo();
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Choose: ");

            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1: manager.CreateGoal(); break;
                    case 2: manager.ListGoalDetails(); break;
                    case 3: manager.SaveGoals(); break;
                    case 4: manager.LoadGoals(); break;
                    case 5: manager.RecordEvent(); break;
                    case 6:
                        Console.WriteLine("Goodbye, adventurer!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice.\n");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please enter a number.\n");
            }
        }
    }
}
