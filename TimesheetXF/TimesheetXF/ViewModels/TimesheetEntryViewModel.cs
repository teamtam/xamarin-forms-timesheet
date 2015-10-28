using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;
using Xamarin.Forms;
using TimesheetXF.Models;
using TimesheetXF.Services;

namespace TimesheetXF.ViewModels
{
    [ImplementPropertyChanged]
    public class TimesheetEntryViewModel : BaseViewModel
    {
        private TimesheetEntry Timesheet { get; set; }

        [AlsoNotifyFor("DisplayDate")]
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public string Project { get; set; }
        public decimal Hours { get; set; }
        public string Comment { get; set; }
        public bool SickLeave { get; set; }

        public ObservableCollection<decimal> HoursCollection { get; set; }

        protected TimesheetEntryViewModel()
        {
            Timesheet = new TimesheetEntry();
        }

        public TimesheetEntryViewModel(TimesheetEntry timesheet)
        {
            Timesheet = timesheet;
            Date = Timesheet.Date;
            Customer = Timesheet.Customer;
            Project = Timesheet.Project;
            Hours = 8;
            Comment = Timesheet.Comment;
            SickLeave = Timesheet.SickLeave;
            HoursCollection = new ObservableCollection<decimal>();
            for (decimal i = 0; i <= 24; i += 0.25m)
            {
                HoursCollection.Add(i);
            }
        }

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

        private ICommand SaveTimesheet;

        public ICommand SaveTimesheetCommand
        {
            get
            {
                return SaveTimesheet ?? (SaveTimesheet = new Command(async () => await ExecuteSaveItemCommand()));
            }
        }

        protected async Task ExecuteSaveItemCommand()
        {
            if (IsLoading)
                return;
            IsLoading = true;
            try
            {
                Timesheet.Hours = Hours;
                Timesheet.Comment = Comment;
                Timesheet.SickLeave = SickLeave;
                await TimesheetService.SubmitTimesheetEntry(Timesheet);
                await App.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await App.MainPage.DisplayAlert("Error", ex.Message, "OK", null);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
