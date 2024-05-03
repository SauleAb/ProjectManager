using LandscapeProjectsManager.MVVM.Views.Projects.Project;
using LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews.Photos;

namespace LandscapeProjectsManager.MVVM.Views;

public partial class PhotosPage : ContentPage
{
    string _projectName;
	public PhotosPage(string projectName)
	{
		InitializeComponent();
        _projectName = projectName;
        ProjectName.Text = _projectName + " Photos";
	}

    private async void Before_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BeforePhotosPage(_projectName, "Before"));
    }

    private async void After_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AfterPhotosPage(_projectName, "After"));
    }

    private async void AddPhoto_Clicked(object sender, EventArgs e)
    {
        var modalPage = new AddPhoto(_projectName);
        await Navigation.PushModalAsync(modalPage);
    }
}