using System;
using System.Collections.Generic; // Crucial for List<Activity>
using ExerciseTracking;           // Crucial to recognize Activity, Running, etc.

namespace ExerciseTracking
{
    
    public class Program // <--- ADDED 
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Exercise Tracking Program ---");
            Console.WriteLine("Demonstrating Inheritance and Polymorphism (using Kilometers)\n");

            // 1. Create a list of the base type (Activity)
            List<Activity> activities = new List<Activity>();

            // 2. Create at least one activity of each type
            Running run1 = new Running("2025-12-01", 30, 4.8); 
            activities.Add(run1);

            Cycling cycle1 = new Cycling("2025-12-02", 45, 20.0);
            activities.Add(cycle1);

            Swimming swim1 = new Swimming("2025-12-03", 60, 40); 
            activities.Add(swim1);

            // Add a second activity for more variety
            Running run2 = new Running("2025-12-04", 60, 10.0);
            activities.Add(run2);

            // 3. Iterate through the list and call GetSummary()
            Console.WriteLine("Summary of Activities:");
            Console.WriteLine("--------------------------------------------------------------------");

            foreach (Activity activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
            }

            Console.WriteLine("--------------------------------------------------------------------");
        } 

    }

    internal class Cycling : Activity
    {
        private string v1;
        private int v2;
        private double v3;

        public Cycling(string date, int minutes, double speed)
            : base(date, minutes, "Cycling")
        {
            this.v1 = date;
            this.v2 = minutes;
            this.v3 = speed;
        }
    }
} 