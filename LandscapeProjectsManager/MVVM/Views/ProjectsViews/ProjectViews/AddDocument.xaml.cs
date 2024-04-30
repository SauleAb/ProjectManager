using augalinga.Data.Access;

namespace LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews;

public partial class AddDocument : ContentPage
{
	public AddDocument()
	{
		InitializeComponent();
	}

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }

    private async void DocumentSelect_Clicked(object sender, EventArgs e)
    {
        var document = await FilePicker.Default.PickAsync(new PickOptions
        {
            PickerTitle = "Pick a document",
            FileTypes = FilePickerFileType.Pdf
        });
        var documentSource = document.FullPath.ToString();
        outputText.Text = documentSource;
    }

    private async void AddDocumentButton_Clicked(object sender, EventArgs e)
    {

    }
}