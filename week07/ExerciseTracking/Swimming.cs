using System;

namespace ExerciseTracking // <-- ADDED
{
    public class Swimming : Activity
    {
        private int _laps;
        private const double LAP_LENGTH_KM = 50.0 / 1000.0; // 50 meters / 1000 = 0.05 km

        public Swimming(string date, int minutes, int laps) 
            : base(date, minutes, "Swimming")
        {
            _laps = laps;
        }

        // Override GetDistance: calculates distance from laps
        public override double GetDistance()
        {
            // Distance (km) = laps * 0.05 km
            return (double)_laps * LAP_LENGTH_KM;
        }

        // Override GetSpeed: calculates speed from distance and time
        public override double GetSpeed()
        {
            // Speed (kph) = (distance / minutes) * 60
            double distance = GetDistance();
            if (GetMinutes() == 0) return 0;
            return (distance / GetMinutes()) * 60.0;
        }

        // Override GetPace: calculates pace from distance and time
        public override double GetPace()
        {
            // Pace (min per km) = minutes / distance
            double distance = GetDistance();
            if (distance == 0.0) return 0;
            return GetMinutes() / distance;
        }
    }
} // <-- ADDED