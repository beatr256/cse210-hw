using System;

namespace ExerciseTracking // Corrected: Adds the namespace
{
    public class Cycling : Activity
    {
        // Specific attribute for Cycling (stored in kph)
        private double _speedKph;

        // Constructor calls base constructor and sets specific attribute
        public Cycling(string date, int minutes, double speedKph) 
            : base(date, minutes, "Cycling")
        {
            _speedKph = speedKph;
        }

        // Override GetDistance: calculates distance from speed and time
        public override double GetDistance()
        {
            // Distance (km) = (speed / 60) * minutes
            return (_speedKph / 60.0) * GetMinutes();
        }

        // Override GetSpeed: returns the stored speed
        public override double GetSpeed()
        {
            return _speedKph;
        }

        // Override GetPace: calculates pace from speed
        public override double GetPace()
        {
            // Pace (min per km) = 60 / speed
            if (_speedKph == 0.0) return 0;
            return 60.0 / _speedKph;
        }
    }
} // Corrected: Closes the namespace block