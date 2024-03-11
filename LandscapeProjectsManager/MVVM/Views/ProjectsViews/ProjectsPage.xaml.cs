using LandscapeProjectsManager.MVVM.ViewModels;
using LandscapeProjectsManager.MVVM.Views;
using LandscapeProjectsManager.MVVM.Views.Projects;
using Syncfusion.Maui.Core.Carousel;

namespace LandscapeProjectsManager;

public partial class ProjectsPage : ContentPage
{
    ProjectsViewModel viewModel;
	public ProjectsPage()
	{
		InitializeComponent();
        viewModel = new ProjectsViewModel();
        BindingContext = viewModel;
    }

    private async void ProjectButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProjectPage());
    }

    private async void AddProject_Clicked(object sender, EventArgs e)
    {
        var modalPage = new AddProjectPage(viewModel);
        await Navigation.PushModalAsync(modalPage);
    }
}