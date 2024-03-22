using augalinga.Data.Entities;
using LandscapeProjectsManager.MVVM.ViewModels;
using LandscapeProjectsManager.MVVM.Views.Projects;

namespace LandscapeProjectsManager.MVVM.Views;

public partial class ProjectPage : ContentPage
{
    ProjectsViewModel _viewModel;
    string _projectName;
	public ProjectPage(string projectName, ProjectsViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        _projectName = projectName;
        ProjectName.Text = _projectName;
    }

    private async void Contacts_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProjectContactsPage());
    }

    private async void Photos_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PhotosPage());
    }

    private async void Documents_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DocumentsPage());
    }

    private async void Orders_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new OrdersPage());
    }

    private async void Drafts_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DraftsPage());
    }

    private async void Finances_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FinancesPage());
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Alert", "Are you sure you want to delete this project?", "Yes", "No");
        if (answer)
        {
            _viewModel.RemoveProject(_projectName);
            await Navigation.PopAsync();
        }
    }
}