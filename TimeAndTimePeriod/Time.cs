using System;

namespace TimeAndTimePeriod
{
    public readonly struct Time : IEquatable<Time>, IComparable<Time>
    {
        private readonly byte _hours;
        private readonly byte _minutes;
        private readonly byte _seconds;

        public byte Hours => _hours;
        public byte Minutes => _minutes;
        public byte Seconds => _seconds;

        public long ToSeconds => _hours * 3600 + _minutes * 60 + _seconds;

        /// <summary>
        /// Generates time.
        /// </summary>
        /// <param name="hours">Hours (param between 0 and 24)</param>
        /// <param name="minutes">Minutes (param between 0 and 59)</param>
        /// <param name="seconds">Seconds (param between 0 and 59)</param>
        public Time(int hours, int minutes, int seconds)
        {
            var data = CkeckArgumants(hours, minutes, seconds);

            _hours = (byte)data.Item1;
            _minutes = (byte)data.Item2;
            _seconds = (byte)data.Item3;
        }

        /// <summary>
        /// Generates time (default seconds).
        /// </summary>
        /// <param name="hours">Hours (param between 0 and 24)</param>
        /// <param name="minutes">Minutes (param between 0 and 59)</param>
        public Time(int hours, int minutes) : this(hours, minutes, 0) { }

        /// <summary>
        /// Generates time (default minutes and secends).
        /// </summary>
        /// <param name="hours">Hours (param between 0 and 24)</param>
        public Time(int hours) : this(hours, 0, 0) { }

        /// <summary>
        /// Generates time from string.
        /// </summary>
        /// <param name="date">Time in format hh:mm:ss</param>
        public Time(string date)
        {
            var time = date.Split(":");
            if (time.Length != 3) throw new ArgumentException("Wrong data format.");
            if (!(int.TryParse(time[0], out var hours) && int.TryParse(time[1], out var minutes) && int.TryParse(time[2], out var seconds))) throw new ArgumentException("Wrong data format.");

            var data = CkeckArgumants(hours, minutes, seconds);

            _hours = (byte)data.Item1;
            _minutes = (byte)data.Item2;
            _seconds = (byte)data.Item3;
        }

        public static (int, int, int) CkeckArgumants(int h, int m, int s) =>
            !((h < 0 || h > 23) || (m < 0 || m > 59) || (s < 0 || s > 59)) ? (h, m, s) :
            throw new ArgumentOutOfRangeException();

        public override string ToString() => $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";

        public bool Equals(Time other) => Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;

        public override bool Equals(object obj) => obj is Time time && Equals(time);

        public override int GetHashCode() => (Hours, Minutes, Seconds).GetHashCode();

        public int CompareTo(Time other) =>
            Hours.CompareTo(other.Hours) == 0 ?
            (Minutes.CompareTo(other.Minutes) == 0 ? Seconds.CompareTo(other.Seconds) : Minutes.CompareTo(other.Minutes)) :
            Hours.CompareTo(other.Hours);

        public static bool operator ==(Time t1, Time t2) => t1.Equals(t2);
        public static bool operator !=(Time t1, Time t2) => !t1.Equals(t2);
        public static bool operator <(Time t1, Time t2) => t1.CompareTo(t2) < 0;
        public static bool operator >(Time t1, Time t2) => t1.CompareTo(t2) > 0;
        public static bool operator <=(Time t1, Time t2) => t1.CompareTo(t2) <= 0;
        public static bool operator >=(Time t1, Time t2) => t1.CompareTo(t2) >= 0;

        public static Time operator +(Time t1, TimePeriod t2)
        {
            long time = t1.ToSeconds + t2.Time;

            return new Time((byte)((time / 3600) % 24), (byte)((time % 3600) / 60), (byte)(time % 60));
        }

        public Time Plus(TimePeriod t1) => this + t1;
        public static Time Plus(Time t1, TimePeriod t2) => t1 + t2;

        public static Time operator -(Time t1, TimePeriod t2)
        {
            long time;

            if (t1.ToSeconds < t2.Time)
                time = (t1.ToSeconds + 24 * 60 * 60) - t2.Time;
            else
                time = t1.ToSeconds - t2.Time;

            return new Time((byte)((time / 3600) % 24), (byte)((time % 3600) / 60), (byte)(time % 60));
        }

        public Time Minus(TimePeriod t1) => this - t1;
        public static Time Minus(Time t1, TimePeriod t2) => t1 - t2;
    }
}
