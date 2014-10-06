using System;
using Xamarin.Forms;
using TimesheetXF.Models;
using TimesheetXF.Services;

namespace TimesheetXF.Pages
{
    public class OutstandingTimesheetListPage : ContentPage
    {
        public OutstandingTimesheetListPage()
        {
            var listView = new ListView
            {
                ItemsSource = TimesheetService.GetTimesheetEntries(),
                ItemTemplate = new DataTemplate(typeof(ListCell)),
                RowHeight = 100 // NOTE: would be nice if this was more dynamic e.g. stack layout fill/padding ...
            };

            CreateEventHandlers(listView);

            Padding = new Thickness(0, 10, 0, 0);
            Title = "Timesheets";
            Content = listView;
        }

        private class ListCell : ViewCell
        {
            public ListCell()
            {
                var dateLabel = new Label
                {
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start
                };
                dateLabel.SetBinding<TimesheetEntry>(Label.TextProperty, m => m.DisplayDate);
                dateLabel.Font = Font.BoldSystemFontOfSize(NamedSize.Medium);

                var customerLabel = new Label
                {
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start
                };
                customerLabel.SetBinding<TimesheetEntry>(Label.TextProperty, m => m.Customer);

                var projectLabel = new Label
                {
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start
                };
                projectLabel.SetBinding<TimesheetEntry>(Label.TextProperty, m => m.Project);

                var viewLayout = new StackLayout()
                {
                    Padding = new Thickness(15, 0, 0, 0),
                    Orientation = StackOrientation.Vertical,
                    Children = { dateLabel, customerLabel, projectLabel }
                };

                View = viewLayout;
            }
        }

        private void CreateEventHandlers(ListView listView)
        {
            listView.ItemTapped += (sender, e) => { ((ListView)sender).SelectedItem = null; };
            listView.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;
                Navigation.PushAsync(new TimesheetEntryPage((TimesheetEntry)e.SelectedItem));
            };
        }
    }
}
