using System;
using System.Collections.Generic;
using System.Text;

namespace Fogcation
{
    public enum Percentage { FullDay, HalfDay };

    [Serializable]
    public class VacationDay
    {
        public DateTime Dt { get; set; }
        public Percentage DayType { get; set; }
        
        public TimeSpan Hours
        {
            get
            {
                return TimeSpanFromPercentage(DayType);
            }
        }

        public static string PrettyPrintPercentage(Percentage percentage)
        {
            switch (percentage)
            {
                case Percentage.HalfDay:
                    return "50%";
                case Percentage.FullDay:
                    return "100%";
                default:
                    throw new ArgumentException("Invalid percentage!", "percentage");
            }
        }

        private TimeSpan TimeSpanFromPercentage(Percentage percentage)
        {
            var ts = new TimeSpan(-frmMain.cHoursInAWorkDay, 0, 0);
            if (percentage == Percentage.HalfDay)
            {
                ts = TimeSpan.FromTicks(ts.Ticks / 2);
            }
            return ts;
        }

        public override string ToString()
        {
            return String.Format(
                "{0} at {1}",
                Dt.ToString(frmMain.sLongDateFormat),
                PrettyPrintPercentage(DayType)
            );
        }
    }
}
