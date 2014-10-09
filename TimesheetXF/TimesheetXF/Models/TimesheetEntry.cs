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
    }
}
