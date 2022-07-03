using System;

namespace TimeAndTimePeriod
{
    public readonly struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        private readonly long _time;

        /// <summary>
        /// Time in seconds
        /// </summary>
        public long Time => _time;

        /// <summary>
        /// Generates time period.
        /// </summary>
        /// <param name="seconds">Seconds (param greater or equal 0)</param>
        public TimePeriod(long seconds) { _time = Ckeck(seconds); }

        /// <summary>
        /// Generates time period.
        /// </summary>
        /// <param name="hours">Hours (param greater than 0)</param>
        /// <param name="minutes">Minutes (param between 0 and 59)</param>
        /// <param name="seconds">Seconds (param between 0 and 59)</param>
        public TimePeriod(int hours, int minutes, int seconds) : this(CkeckArgument(hours) * 3600 + CkeckArgument(minutes) * 60 + CkeckArgument(seconds)) { }

        /// <summary>
        /// Generates time period (default seconds).
        /// </summary>
        /// <param name="hours">Hours (param greater than 0)</param>
        /// <param name="minutes">Minutes (param between 0 and 59)</param>
        public TimePeriod(int hours, int minutes) : this(hours, minutes, 0) { }

        /// <summary>
        /// Time period from string
        /// </summary>
        /// <param name="date">TimePeriod in format hhhh:mm:ss</param>
        public TimePeriod(string date)
        {
            var time = date.Split(":");
            if (time.Length != 3) throw new ArgumentException("Wrong data format.");
            if (!(int.TryParse(time[0], out var hours) && int.TryParse(time[1], out var minutes) && int.TryParse(time[2], out var seconds))) throw new ArgumentException("Wrong data format.");

            _time = Ckeck(CkeckArgument(hours) * 3600 + CkeckArgument(minutes) * 60 + CkeckArgument(seconds));
        }

        /// <summary>
        /// Generates time period.
        /// </summary>
        /// <param name="t1">Time struct param</param>
        /// <param name="t2">Time struct param</param>
        public TimePeriod(Time t1, Time t2)
        {
            var time_t1 = t1.Hours * 3600 + t1.Minutes * 60 + t1.Seconds;
            var time_t2 = t2.Hours * 3600 + t2.Minutes * 60 + t2.Seconds;

            _time = Ckeck(time_t2 - time_t1);
        }

        public static long Ckeck(long data) => data >= 0 ? data : throw new ArgumentOutOfRangeException();
        public static int CkeckArgument(int data) => data >= 0 ? data : throw new ArgumentOutOfRangeException();

        public override string ToString() => $"{((_time / 3600) % 24):D2}:{((_time % 3600) / 60):D2}:{(_time % 60):D2}";

        public bool Equals(TimePeriod other) => _time == other._time;

        public override bool Equals(object obj) => obj is TimePeriod && Equals(obj);

        public override int GetHashCode() => _time.GetHashCode();

        public int CompareTo(TimePeriod other) => _time.CompareTo(other._time);

        public static bool operator ==(TimePeriod t1, TimePeriod t2) => t1.Equals(t2);
        public static bool operator !=(TimePeriod t1, TimePeriod t2) => !t1.Equals(t2);
        public static bool operator <(TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) < 0;
        public static bool operator >(TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) > 0;
        public static bool operator <=(TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) <= 0;
        public static bool operator >=(TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) >= 0;

        public static TimePeriod operator +(TimePeriod t1, TimePeriod t2)
        {
            long time = t1.Time + t2.Time;

            return new TimePeriod((byte)((time / 3600) % 24), (byte)((time % 3600) / 60), (byte)(time % 60));
        }

        public TimePeriod Plus(TimePeriod t1) => this + t1;
        public static TimePeriod Plus(TimePeriod t1, TimePeriod t2) => t1 + t2;

        public static TimePeriod operator -(TimePeriod t1, TimePeriod t2)
        {
            long time;

            if (t1.Time < t2.Time)
                time = (t1.Time + 24 * 60 * 60) - t2.Time;
            else
                time = t1.Time - t2.Time;

            return new TimePeriod((byte)((time / 3600) % 24), (byte)((time % 3600) / 60), (byte)(time % 60));
        }

        public TimePeriod Minus(TimePeriod t1) => this - t1;
        public static TimePeriod Minus(TimePeriod t1, TimePeriod t2) => t1 - t2;
    }
}
