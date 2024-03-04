using Syncfusion.Maui.Scheduler;
using System;
using System.Globalization;

namespace LandscapeProjectsManager.MVVM.Views
{
    public partial class AddEventPage : ContentPage
    {
        private DateTime selectedDateTime;

        public AddEventPage(DateTime selectedDateTime)
        {
            InitializeComponent();
            this.selectedDateTime = selectedDateTime;
            EventDate.Date = selectedDateTime.Date;
            TimeFrom.Time = selectedDateTime.Date.TimeOfDay;
        }

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            var viewModel = new CalendarViewModel();

            string eventName = Entry.Text;
            DateTime from = selectedDateTime.Date.Add(TimeFrom.Time);
            DateTime to = selectedDateTime.Date.Add(TimeTo.Time);
            bool isBaronaiteChecked = Baronaite.IsChecked;
            bool isGudaityteChecked = Gudaityte.IsChecked;
            bool isBothChecked = Both.IsChecked;

            // Check if all required fields are filled
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

                viewModel.Events.Add(newEvent);
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
    }
}
