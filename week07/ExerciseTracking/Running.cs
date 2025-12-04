using System;

namespace ExerciseTracking // <-- ADDED
{
    public class Running : Activity
    {
        private double _distanceKm;

        public Running(string date, int minutes, double distanceKm) 
            : base(date, minutes, "Running")
        {
            _distanceKm = distanceKm;
        }

        // Override GetDistance: returns the stored distance
        public override double GetDistance()
        {
            return _distanceKm;
        }

        // Override GetSpeed: calculates speed from distance and time
        public override double GetSpeed()
        {
            // Speed (kph) = (distance / minutes) * 60
            if (GetMinutes() == 0) return 0;
            return (_distanceKm / GetMinutes()) * 60.0;
        }

        // Override GetPace: calculates pace from distance and time
        public override double GetPace()
        {
            // Pace (min per km) = minutes / distance
            if (_distanceKm == 0.0) return 0;
            return GetMinutes() / _distanceKm;
        }
    }
} // <-- ADDED