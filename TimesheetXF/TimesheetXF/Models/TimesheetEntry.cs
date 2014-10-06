using System;

namespace TimesheetXF.Models
{
    public class TimesheetEntry
    {
        public Guid TimesheetId { get; set; }
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public string Project { get; set; }
        public decimal Hours { get; set; }
        public string Comment { get; set; }
        public bool SickLeave { get; set; }

        public string DisplayDate
        {
            get
            {
                if (Date != null)
                {
                    if (Date.Equals(DateTime.Today))
                        return "Today";
                    if (Date.Equals(DateTime.Today.AddDays(-1)))
                        return "Yesterday";
                    return Date.DayOfWeek + " " + Date.ToString("dd/MM/yyyy");
                }
                return string.Empty;
            }
        }

    }
}
