using LandscapeProjectsManager.MVVM.Views.Contacts;

namespace LandscapeProjectsManager;

public partial class ContactsPage : ContentPage
{
	public ContactsPage()
	{
		InitializeComponent();
	}

    private async void AddContact_Clicked(object sender, EventArgs e)
    {
        var modalPage = new AddContact();
        await Navigation.PushModalAsync(modalPage);
    }

    private async void ColleaguesButton_Clicked(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        string projectCategory = clickedButton.Text;
        await Navigation.PushAsync(new ContactView(projectCategory));
    }

    private async void ManufactureButton_Clicked(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        string projectCategory = clickedButton.Text;
        await Navigation.PushAsync(new ContactView(projectCategory));
    }

    private async void TransportButton_Clicked(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        string projectCategory = clickedButton.Text;
        await Navigation.PushAsync(new ContactView(projectCategory));
    }

    private async void NurseriesButton_Clicked(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        string projectCategory = clickedButton.Text;
        await Navigation.PushAsync(new ContactView(projectCategory));
    }
}