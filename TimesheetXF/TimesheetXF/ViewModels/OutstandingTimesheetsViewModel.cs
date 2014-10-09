using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;
using Xamarin.Forms;
using TimesheetXF.Services;

namespace TimesheetXF.ViewModels
{
    [ImplementPropertyChanged]
    public class OutstandingTimesheetsViewModel : BaseViewModel
    {
        public ObservableCollection<TimesheetEntryViewModel> Timesheets { get; set; }

        public TimesheetEntryViewModel SelectedTimesheet { get; set; }

        public OutstandingTimesheetsViewModel()
        {
            Timesheets = new ObservableCollection<TimesheetEntryViewModel>();
        }

        private ICommand LoadTimesheets;

        public ICommand LoadItemsCommand
        {
            get
            {
                return LoadTimesheets ?? (LoadTimesheets = new Command(async () => await ExecuteLoadItemsCommand()));
            }
        }

        protected async Task ExecuteLoadItemsCommand()
        {
            if (IsLoading)
                return;
            IsLoading = true;
            try
            {
                Timesheets.Clear();
                var timesheets = await TimesheetService.GetTimesheetEntries();
                foreach (var timesheet in timesheets)
                {
                    Timesheets.Add(new TimesheetEntryViewModel(timesheet));
                }
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
