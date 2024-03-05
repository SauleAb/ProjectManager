using LandscapeProjectsManager.MVVM.ViewModels;
using LandscapeProjectsManager.MVVM.Views;

namespace LandscapeProjectsManager;

public partial class ProjectsPage : ContentPage
{
	public ProjectsPage()
	{
		InitializeComponent();
        BindingContext = new ProjectsViewModel();
    }
	public class Project
	{
		public string Name { get; set; }
	}

    private async void ProjectButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProjectPage());
    }
}