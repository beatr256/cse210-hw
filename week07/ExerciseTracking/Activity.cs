using System;

namespace ExerciseTracking // <-- ADDED
{
    public class Activity
    {
        // Private attributes shared by all activities
        private DateTime _date;
        private int _minutes;
        private string _activityType;

        // Constructor to initialize shared attributes
        public Activity(string date, int minutes, string activityType)
        {
            _date = DateTime.Parse(date);
            _minutes = minutes;
            _activityType = activityType;
        }

        // Accessors for derived classes (use protected)
        protected int GetMinutes()
        {
            return _minutes;
        }

        protected string GetDateFormatted()
        {
            // Format example: "03 Nov 2022"
            return _date.ToString("dd MMM yyyy");
        }

        // Virtual methods to be overridden (Polymorphism)
        public virtual double GetDistance()
        {
            return 0.0;
        }

        public virtual double GetSpeed()
        {
            return 0.0;
        }

        public virtual double GetPace()
        {
            return 0.0;
        }

        // Method to produce the summary string
        public string GetSummary()
        {
            // Fetch calculated values (Polymorphism in action)
            double distance = GetDistance();
            double speed = GetSpeed();
            double pace = GetPace();

            // Handle cases where distance is zero (e.g., prevents division by zero in GetPace/GetSpeed)
            if (distance <= 0 && _activityType != "Running") // Running should always have distance stored
            {
                 // Handle swimming/cycling data if time or other input was zero
                 if (distance <= 0 && GetMinutes() <= 0) 
                 {
                     return $"{GetDateFormatted()} {_activityType} ({_minutes} min) - Data Incomplete/Invalid";
                 }
            }

            return $"{GetDateFormatted()} {_activityType} ({_minutes} min) - Distance {distance:F2} km, Speed: {speed:F2} kph, Pace: {pace:F2} min per km";
        }
    }
} // <-- ADDED