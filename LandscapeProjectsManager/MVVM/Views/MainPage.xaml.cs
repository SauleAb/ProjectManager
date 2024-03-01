namespace LandscapeProjectsManager;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnCalendarLabelTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalendarPage());
        }

	private async void OnContactsLabelTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactsPage());
        }

	private async void OnProjectsLabelTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProjectsPage());
        }
}

