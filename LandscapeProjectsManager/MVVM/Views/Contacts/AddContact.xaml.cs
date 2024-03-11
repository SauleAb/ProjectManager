namespace LandscapeProjectsManager.MVVM.Views.Contacts;

public partial class AddContact : ContentPage
{
	public AddContact()
	{
		InitializeComponent();
	}

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }

    private void AddContactButton_Clicked(object sender, EventArgs e)
    {

    }
}