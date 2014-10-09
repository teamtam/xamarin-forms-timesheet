using System;
using Xamarin.Forms;
using TimesheetXF.ViewModels;
using TimesheetXF.Services;

namespace TimesheetXF.Views
{
    public class TimesheetEntryPage : BaseContentPage
    {
        private const int LEFT_COLUMN_WIDTH = 100;

        protected TimesheetEntryViewModel ViewModel
        {
            get { return BindingContext as TimesheetEntryViewModel; }
            set { BindingContext = value; }
        }

        public TimesheetEntryPage(TimesheetEntryViewModel timesheet)
        {
            BindingContext = timesheet;

            var dateLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { CreateDateLabel(), CreateDate() }
            };
            dateLayout.Padding = new Thickness(0, 0, 0, 5);

            var customerLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { CreateCustomerLabel(), CreateCustomer() }
            };
            customerLayout.Padding = new Thickness(0, 0, 0, 5);

            var projectLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { CreateProjectLabel(), CreateProject() }
            };
            projectLayout.Padding = new Thickness(0, 0, 0, 5);

            var hoursInput = CreateHoursInput();
            var hoursLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { CreateHoursLabel(), hoursInput }
            };

            var commentInput = CreateCommentInput();
            var commentLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { CreateCommentLabel(), commentInput }
            };

            var sickLeaveInput = CreateSickLeaveInput();
            var sickLeaveLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { CreateSickLeaveLabel(), sickLeaveInput }
            };

            var submitButton = CreateSubmitButton(timesheet, hoursInput, commentInput, sickLeaveInput);

            var layout = new StackLayout
            {
                Children =
                {
                    dateLayout,
                    customerLayout,
                    projectLayout,
                    hoursLayout,
                    commentLayout,
                    sickLeaveLayout,
                    submitButton,
                    CreateLoadingIndicator()
                }
            };
            //var layout = CreateLoadingIndicatorRelativeLayout(content);
            //var layout = CreateLoadingIndicatorAbsoluteLayout(content);

            Padding = new Thickness(15, 10);
            Title = "Submit";
            Content = layout;
        }

        private Label CreateDateLabel()
        {
            return new Label
            {
                Text = "Date",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = LEFT_COLUMN_WIDTH,
                Font = Font.BoldSystemFontOfSize(NamedSize.Medium)
            };
        }

        private Label CreateDate()
        {
            var dateInput = new Label
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                Font = Font.SystemFontOfSize(NamedSize.Small)
            };
            dateInput.SetBinding<TimesheetEntryViewModel>(Label.TextProperty, m => m.DisplayDate);
            return dateInput;
        }

        private Label CreateCustomerLabel()
        {
            return new Label
            {
                Text = "Customer",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = LEFT_COLUMN_WIDTH,
                Font = Font.BoldSystemFontOfSize(NamedSize.Medium)
            };
        }

        private Label CreateCustomer()
        {
            var customerInput = new Label
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                Font = Font.SystemFontOfSize(NamedSize.Small),
            };
            customerInput.SetBinding<TimesheetEntryViewModel>(Label.TextProperty, m => m.Customer);
            return customerInput;
        }

        private Label CreateProjectLabel()
        {
            return new Label
            {
                Text = "Project",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = LEFT_COLUMN_WIDTH,
                Font = Font.BoldSystemFontOfSize(NamedSize.Medium)
            };
        }

        private Label CreateProject()
        {
            var projectInput = new Label
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                Font = Font.SystemFontOfSize(NamedSize.Small)
            };
            projectInput.SetBinding<TimesheetEntryViewModel>(Label.TextProperty, m => m.Project);
            return projectInput;
        }

        private static Label CreateHoursLabel()
        {
            return new Label
            {
                Text = "Hours",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = LEFT_COLUMN_WIDTH,
                Font = Font.BoldSystemFontOfSize(NamedSize.Medium)
            };
        }

        private Picker CreateHoursInput()
        {
            var hours = new Picker();
            for (double i = 0; i <= 24; i += 0.25)
            {
                hours.Items.Add(i.ToString());
            }
            hours.SelectedIndex = 32; // NOTE: Xamarin.Forms does not currently support data binding for the Picker
            hours.HorizontalOptions = LayoutOptions.FillAndExpand; // NOTE: why doesn't EndAndExpand work?
            hours.VerticalOptions = LayoutOptions.Start;
            hours.SelectedIndexChanged += (sender, e) => { ViewModel.Hours = Convert.ToDecimal(hours.Items[hours.SelectedIndex]); };
            return hours;
        }

        private static Label CreateCommentLabel()
        {
            return new Label
            {
                Text = "Comment",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = LEFT_COLUMN_WIDTH,
                Font = Font.BoldSystemFontOfSize(NamedSize.Medium)
            };
        }

        private Entry CreateCommentInput()
        {
            var commentInput = new Entry
            {
                HorizontalOptions = LayoutOptions.FillAndExpand, // NOTE: why doesn't EndAndExpand work?
                VerticalOptions = LayoutOptions.Start
            };
            commentInput.SetBinding<TimesheetEntryViewModel>(Entry.TextProperty, m => m.Comment, BindingMode.TwoWay);
            return commentInput;
        }

        private static Label CreateSickLeaveLabel()
        {
            return new Label
            {
                Text = "Sick Leave",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = LEFT_COLUMN_WIDTH,
                Font = Font.BoldSystemFontOfSize(NamedSize.Medium)
            };
        }

        private Switch CreateSickLeaveInput()
        {
            var sickLeaveInput = new Switch
            {
                HorizontalOptions = LayoutOptions.EndAndExpand, // NOTE: why doesn't End work?
                VerticalOptions = LayoutOptions.Start
            };
            sickLeaveInput.SetBinding<TimesheetEntryViewModel>(Switch.IsToggledProperty, m => m.SickLeave, BindingMode.TwoWay);
            return sickLeaveInput;
        }

        private Button CreateSubmitButton(TimesheetEntryViewModel timesheet, Picker hours, Entry comment, Switch sickLeave)
        {
            var submitButton = new Button
            {
                Text = "Submit",
                Font = Font.BoldSystemFontOfSize(NamedSize.Medium),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            submitButton.SetBinding<TimesheetEntryViewModel>(Button.CommandProperty, m => m.SaveTimesheetCommand);
            return submitButton;
        }
    }
}
