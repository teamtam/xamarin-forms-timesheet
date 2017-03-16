using Xamarin.Forms;
using TimesheetXF.Views;

namespace TimesheetXF
{
    public class App : Application
    {
        public App()
        {
			//InitializeComponent();

			MainPage = new NavigationPage(new OutstandingTimesheetListPage());
        }
    }
}
