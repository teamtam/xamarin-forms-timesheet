using System;
using Xamarin.Forms;
using TimesheetXF.Views;

namespace TimesheetXF
{
    public class App
    {
        public static readonly Page MainPage = new NavigationPage(new OutstandingTimesheetListPage());

        public static INavigation Navigation
        {
            get { return MainPage.Navigation; }
        }
    }
}
