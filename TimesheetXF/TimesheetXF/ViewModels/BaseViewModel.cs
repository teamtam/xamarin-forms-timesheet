using PropertyChanged;

namespace TimesheetXF.ViewModels
{
    [ImplementPropertyChanged]
    public class BaseViewModel
    {
        public bool IsLoading { get; set; }
    }
}
