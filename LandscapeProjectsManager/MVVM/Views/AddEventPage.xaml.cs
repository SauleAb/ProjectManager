using Syncfusion.Maui.Scheduler;

namespace LandscapeProjectsManager.MVVM.Views;

public partial class AddEventPage : ContentPage
{
	public AddEventPage()
	{
		InitializeComponent();
	}

    private async void OnAddButtonClicked(object sender, EventArgs e)
    {
        var viewModel =  new CalendarViewModel();

        var newEvent = new Meeting
        {
            From = DateTime.Now,
            To = DateTime.Now.AddHours(1),
            EventName = "New Event",
            IsAllDay = false,
            // Assign the color based on the selected radio button
            Background = GetSelectedColor(),
            Notes = "Added from AddEventPage", // Example: Set your notes
            StartTimeZone = TimeZoneInfo.Local,
            EndTimeZone = TimeZoneInfo.Local
        };

        viewModel.Events.Add(newEvent);

        await Shell.Current.GoToAsync("..");
    }

    private Brush GetSelectedColor()
    {
        if (this.FindByName<RadioButton>("Baronaite").IsChecked)
        {
            return new SolidColorBrush(Color.FromArgb("#FFFFD700")); // Yellow color for Laura Baronaite
        }
        else if (this.FindByName<RadioButton>("Gudaityte").IsChecked)
        {
            return new SolidColorBrush(Color.FromArgb("#FF0000FF")); // Example: Blue color for Laura Gudaityte
        }
        else if (this.FindByName<RadioButton>("Both").IsChecked)
        {
            return new SolidColorBrush(Color.FromArgb("#FF008000")); // Example: Green color for Both
        }

        return new SolidColorBrush(Color.FromArgb("#FFFFFFFF")); // Default color if none selected
    }
}