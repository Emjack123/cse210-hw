using System;
using System.Collections.Generic;

namespace FitnessTracker
{
        // BASE CLASS: Activity
    // ==========================================
    public abstract class Activity
    {
        // Encapsulation: Private member variables
        private DateTime _date;
        private int _minutes;

        // Protected properties to allow derived classes access to the data safely
        protected DateTime Date => _date;
        protected int Minutes => _minutes;

        public Activity(DateTime date, int minutes)
        {
            _date = date;
            _minutes = minutes;
        }

        // Abstract methods to be overridden by derived classes
        public abstract double GetDistance();
        public abstract double GetSpeed();
        public abstract double GetPace();

        // Virtual summary method defined in the base class to avoid duplication
        public virtual string GetSummary()
        {
            return $"{_date.ToString("dd MMM yyyy")} {this.GetType().Name} ({_minutes} min): " +
                   $"Distance: {GetDistance():F1} km, Speed: {GetSpeed():F1} kph, Pace: {GetPace():F2} min per km";
        }
    }

    
    // DERIVED CLASS: Running
    // ==========================================
    public class Running : Activity
    {
        private double _distanceKm;

        public Running(DateTime date, int minutes, double distanceKm) : base(date, minutes)
        {
            _distanceKm = distanceKm;
        }

        public override double GetDistance() => _distanceKm;

        public override double GetSpeed() => (_distanceKm / Minutes) * 60;

        public override double GetPace() => Minutes / _distanceKm;
    }

    // ==========================================
    // DERIVED CLASS: Cycling
    // ==========================================
    public class Cycling : Activity
    {
        private double _speedKph;

        public Cycling(DateTime date, int minutes, double speedKph) : base(date, minutes)
        {
            _speedKph = speedKph;
        }

        // Distance = (Speed * Time) -> Time in hours is Minutes / 60
        public override double GetDistance() => (_speedKph * Minutes) / 60;

        public override double GetSpeed() => _speedKph;

        public override double GetPace() => 60 / _speedKph;
    }

    // ==========================================
    // DERIVED CLASS: Swimming
    // ==========================================
    public class Swimming : Activity
    {
        private int _laps;
        private const double LapLengthMeters = 50.0;

        public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
        {
            _laps = laps;
        }

        // Distance = laps * 50 meters / 1000 to get kilometers
        public override double GetDistance() => (_laps * LapLengthMeters) / 1000.0;

        public override double GetSpeed() => (GetDistance() / Minutes) * 60;

        public override double GetPace() => Minutes / GetDistance();
    }

    // ==========================================
    // MAIN PROGRAM
    // ==========================================
    class Program
    {
        static void Main(string[] sender)
        {
            // Create a single list containing different types of activities
            List<Activity> activities = new List<Activity>();

            // Add at least one activity of each type
            activities.Add(new Running(new DateTime(2026, 6, 14), 30, 4.8));
            activities.Add(new Cycling(new DateTime(2026, 6, 14), 45, 20.5));
            activities.Add(new Swimming(new DateTime(2026, 6, 14), 20, 24)); // 24 laps = 1.2 km

            Console.WriteLine("--- Fitness Center Activity Summary --- \n");

            // Iterate through the list and polymorphically call GetSummary
            foreach (Activity activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
            }
        }
    }
}