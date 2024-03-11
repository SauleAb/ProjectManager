using LandscapeProjectsManager.MVVM.Views.Projects.Project;

namespace LandscapeProjectsManager.MVVM.Views;

public partial class PhotosPage : ContentPage
{
	public PhotosPage()
	{
		InitializeComponent();
	}

    private async void Before_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BeforePhotosPage());
    }

    private async void After_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AfterPhotosPage());
    }
}