using Syncfusion.Maui.Scheduler;
using System;
using System.Globalization;

namespace LandscapeProjectsManager.MVVM.Views
{
    public partial class AddEventPage : ContentPage
    {
        private DateTime selectedDateTime;
        private CalendarViewModel calendarViewModel;

        public DateTime SelectedDateTime { get; set; }
        public string EventTitle { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public AddEventPage(DateTime selectedDateTime, CalendarViewModel calendarViewModel)
        {
            InitializeComponent();
            this.calendarViewModel = calendarViewModel;
            SelectedDateTime = selectedDateTime;
            EventTitle = "";
            StartTime = selectedDateTime.TimeOfDay;
            EndTime = selectedDateTime.AddHours(1).TimeOfDay;
            BindingContext = this;
        }

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            string eventName = Entry.Text;
            DateTime from = selectedDateTime.Date.Add(TimeFrom.Time);
            DateTime to = selectedDateTime.Date.Add(TimeTo.Time);
            bool isBaronaiteChecked = Baronaite.IsChecked;
            bool isGudaityteChecked = Gudaityte.IsChecked;
            bool isBothChecked = Both.IsChecked;

            if (!string.IsNullOrWhiteSpace(eventName) && (isBaronaiteChecked || isGudaityteChecked || isBothChecked))
            {
                var newEvent = new Meeting
                {
                    From = from,
                    To = to,
                    EventName = eventName,
                    IsAllDay = false,
                    Background = GetSelectedColor(isBaronaiteChecked, isGudaityteChecked, isBothChecked),
                    Notes = "Added from AddEventPage",
                    StartTimeZone = TimeZoneInfo.Local,
                    EndTimeZone = TimeZoneInfo.Local
                };

                Console.WriteLine("Adding new event: " + newEvent.EventName);
                calendarViewModel.Events.Add(newEvent);

                foreach (var ev in calendarViewModel.Events)
                {
                    Console.WriteLine("Event in collection: " + ev.EventName);
                }

                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Alert", "Fill in all required fields!", "OK");
            }
        }

        private Brush GetSelectedColor(bool isBaronaiteChecked, bool isGudaityteChecked, bool isBothChecked)
        {
            if (isBaronaiteChecked)
            {
                return new SolidColorBrush(Color.FromArgb("#FFFFD700")); // Yellow color for Laura Baronaite
            }
            else if (isGudaityteChecked)
            {
                return new SolidColorBrush(Color.FromArgb("#FF0000FF")); // Blue color for Laura Gudaityte
            }
            else if (isBothChecked)
            {
                return new SolidColorBrush(Color.FromArgb("#FF008000")); // Green color for Both
            }

            return new SolidColorBrush(Color.FromArgb("#FFFFFFFF")); // Default color if none selected
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
