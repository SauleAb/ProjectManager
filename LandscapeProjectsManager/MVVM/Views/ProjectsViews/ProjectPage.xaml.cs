using LandscapeProjectsManager.MVVM.Views.Projects;

namespace LandscapeProjectsManager.MVVM.Views;

public partial class ProjectPage : ContentPage
{
	public ProjectPage(string projectName)
	{
		InitializeComponent();
        ProjectName.Text = projectName;
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
}