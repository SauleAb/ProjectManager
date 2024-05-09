using augalinga.Data.Access;
using augalinga.Data.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Syncfusion.Maui.Scheduler;
using System;
using System.Globalization;

namespace LandscapeProjectsManager.MVVM.Views
{
    public partial class AddEventPage : ContentPage
    {
        private CalendarViewModel calendarViewModel;

        public DateTime SelectedDateTime { get; set; }
        public string EventTitle { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DataContext DataContext { get; set; } = new DataContext();


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
            DateTime from = SelectedDateTime.Date.Add(TimeFrom.Time);
            DateTime to = SelectedDateTime.Date.Add(TimeTo.Time);
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
                    Employee = GetSelectedEmployee(isBaronaiteChecked, isGudaityteChecked, isBothChecked),
                    Background = GetSelectedColor(isBaronaiteChecked, isGudaityteChecked, isBothChecked),
                    Notes = "Added from AddEventPage",
                };

                DataContext.Meetings.Add(newEvent);

                await DataContext.SaveChangesAsync();

                calendarViewModel.AddEventToCollection(newEvent);

                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Alert", "Fill in all required fields!", "OK");
            }
        }

        private string GetSelectedEmployee(bool isBaronaiteChecked, bool isGudaityteChecked, bool isBothChecked)
        {
            if (isBaronaiteChecked)
            {
                return "Baronaite";
            }
            else if (isGudaityteChecked)
            {
                return "Gudaityte";
            }
            else if (isBothChecked)
            {
                return "Both";
            }
            return "";
        }

        private string GetSelectedColor(bool isBaronaiteChecked, bool isGudaityteChecked, bool isBothChecked)
        {
            if (isBaronaiteChecked)
            {
                return "#FFFFD700"; // Yellow
            }
            else if (isGudaityteChecked)
            {
                return "#1A9BAB"; // Electric
            }
            else if (isBothChecked)
            {
                return "#E7344C"; // Red
            }

            return "#FFFFFFFF"; // Default color
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }

        private void PointerGestureRecognizer_PointerEntered(object sender, PointerEventArgs e)
        {
            ((Button)sender).BackgroundColor = Color.FromRgb(240, 240, 240);
        }

        private void PointerGestureRecognizer_PointerExited(object sender, PointerEventArgs e)
        {
            ((Button)sender).BackgroundColor = Color.FromRgb(255, 255, 255);
        }
    }
}
