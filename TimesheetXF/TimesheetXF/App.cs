using System;
using Xamarin.Forms;
using TimesheetXF.Pages;

namespace TimesheetXF
{
    public class App
    {
        private static readonly Page MainPage;

        static App()
        {
            MainPage = new NavigationPage(new OutstandingTimesheetListPage());
        }

        public static Page GetMainPage()
        {
            return MainPage;
        }
    }
}
