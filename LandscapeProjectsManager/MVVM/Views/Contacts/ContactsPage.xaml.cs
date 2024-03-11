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
}