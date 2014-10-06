using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetXF.Models;

namespace TimesheetXF.Services
{
    public class TimesheetService
    {
        public static IEnumerable<TimesheetEntry> GetTimesheetEntries()
        {
            Task.Delay(2000).Wait(); // NOTE: just to simulate a HTTP request over the wire
            List<TimesheetEntry> dates = new List<TimesheetEntry>();
            DateTime current = DateTime.Today;
            for (int i = 0; i < 5; i++)
            {
                if (current.DayOfWeek == DayOfWeek.Saturday)
                    current = current.AddDays(-1);
                else if (current.DayOfWeek == DayOfWeek.Sunday)
                    current = current.AddDays(-2);
                dates.Insert(0, CreateTimesheetEntry(current));
                current = current.AddDays(-1);
            }
            return dates;
        }

        private static TimesheetEntry CreateTimesheetEntry(DateTime date)
        {
            return new TimesheetEntry()
            {
                TimesheetId = new Guid("7faef0a6-f977-493f-95a1-b14a9f90e8dd"),
                Date = date,
                Customer = "Some Customer",
                Project = "Some Project"
            };
        }

        public static void SubmitTimesheetEntry(TimesheetEntry timesheet)
        {
            Task.Delay(2000).Wait(); // NOTE: just to simulate a HTTP request over the wire
            return;
        }
    }
}
