using System;
using Xamarin.Forms;
using TimesheetXF.Services;
using TimesheetXF.ViewModels;

namespace TimesheetXF.Views
{
    public class OutstandingTimesheetListPage : BaseContentPage
    {
        protected OutstandingTimesheetsViewModel ViewModel
        {
            get { return BindingContext as OutstandingTimesheetsViewModel; }
            set { BindingContext = value; }
        }

        public OutstandingTimesheetListPage()
        {
            BindingContext = new OutstandingTimesheetsViewModel();

            var layout = new StackLayout
            {
                Children = { CreateLoadingIndicator(), CreateOutstandingTimesheets() }
            };
            //var layout = CreateLoadingIndicatorRelativeLayout(CreateOutstandingTimesheets());
            //var layout = CreateLoadingIndicatorAbsoluteLayout(CreateOutstandingTimesheets());

            Padding = new Thickness(0, 10, 0, 0);
            Title = "Timesheets";
            Content = layout;
        }

        private ListView CreateOutstandingTimesheets()
        {
            var outstandingTimesheets = new ListView
            {
                ItemsSource = ViewModel.Timesheets,
                ItemTemplate = new DataTemplate(typeof(ListCell)),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowHeight = 75, // NOTE: would be nice if this was more dynamic e.g. stack layout fill/padding ...
            };
            CreateEventHandlers(outstandingTimesheets);
            return outstandingTimesheets;
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
                dateLabel.SetBinding<TimesheetEntryViewModel>(Label.TextProperty, m => m.DisplayDate);
                dateLabel.Font = Font.BoldSystemFontOfSize(NamedSize.Medium);

                var customerLabel = new Label
                {
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start
                };
                customerLabel.SetBinding<TimesheetEntryViewModel>(Label.TextProperty, m => m.Customer);

                var projectLabel = new Label
                {
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start
                };
                projectLabel.SetBinding<TimesheetEntryViewModel>(Label.TextProperty, m => m.Project);

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
                Navigation.PushAsync(new TimesheetEntryPage((TimesheetEntryViewModel)e.SelectedItem));
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel == null || ViewModel.IsLoading)
                return;
            ViewModel.LoadItemsCommand.Execute(null);
        }
    }
}
