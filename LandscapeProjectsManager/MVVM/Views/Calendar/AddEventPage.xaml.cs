using augalinga.Data.Entities;
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

        private string GetSelectedColor(bool isBaronaiteChecked, bool isGudaityteChecked, bool isBothChecked)
        {
            if (isBaronaiteChecked)
            {
                return "#FFFFD700"; // Yellow color new SolidColorBrush(Color.FromArgb(
            }
            else if (isGudaityteChecked)
            {
                return "#FF0000FF"; //blue
            }
            else if (isBothChecked)
            {
                return "#FF008000"; // Green color for Both
            }

            return "#FFFFFFFF"; // Default color if none selected
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
