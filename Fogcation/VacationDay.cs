using System;
using System.Collections.Generic;
using System.Text;

namespace Fogcation
{
    public enum DayType { FullDay, HalfDay, CompanyHoliday };

    [Serializable]
    public class VacationDay
    {
        public DateTime Dt { get; set; }
        public DayType DayType { get; set; }
        
        public TimeSpan Hours
        {
            get
            {
                return TimeSpanFromDayType(DayType);
            }
        }

        public static string PrettyPrintDayType(DayType type)
        {
            switch (type)
            {
                case DayType.HalfDay:
                    return "50%";
                case DayType.FullDay:
                    return "100%";
                case DayType.CompanyHoliday:
                    return "";
                default:
                    throw new ArgumentException("Invalid day type!", "type");
            }
        }

        private TimeSpan TimeSpanFromDayType(DayType type)
        {
            var ts = new TimeSpan(-frmMain.cHoursInAWorkDay, 0, 0);
            if (type == DayType.HalfDay)
            {
                ts = TimeSpan.FromTicks(ts.Ticks / 2);
            }
            if (type == DayType.CompanyHoliday)
            {
                ts = new TimeSpan(0);
            }
            return ts;
        }

        public override string ToString()
        {
            var sDate = Dt.ToString(frmMain.sLongDateFormat);
            
            if (DayType == DayType.CompanyHoliday)
            {
                return sDate;
            }

            return String.Format(
                "{0} at {1}",
                sDate,
                PrettyPrintDayType(DayType)
            );
        }
    }
}
