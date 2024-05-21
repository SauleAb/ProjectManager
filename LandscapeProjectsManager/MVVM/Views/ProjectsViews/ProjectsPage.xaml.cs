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
        Button clickedButton = (Button)sender;
        string projectName = clickedButton.Text;
        await Navigation.PushAsync(new ProjectPage(projectName, viewModel));
    }

    private async void AddProject_Clicked(object sender, EventArgs e)
    {
        var modalPage = new AddProjectPage(viewModel);
        await Navigation.PushModalAsync(modalPage);
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