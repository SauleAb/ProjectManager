namespace LandscapeProjectsManager.MVVM.Views.Projects;

using Amazon.Util;
using AWSSDK;
using LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews;

public partial class DocumentsPage : ContentPage
{
	public DocumentsPage()
	{
		InitializeComponent();
	}

    private async void AddDocument_Clicked(object sender, EventArgs e)
    {
        var modalPage = new AddDocument();
        await Navigation.PushModalAsync(modalPage);
    }

    private void DeleteButton_Clicked(object sender, EventArgs e)
    {

    }
}