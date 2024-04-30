using Amazon;
using Amazon.S3;
using augalinga.Data.Access;
using LandscapeProjectsManager.MVVM.Models;
using Windows.Media.Protection.PlayReady;

namespace LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews;

public partial class AddDocument : ContentPage
{
    string bucket = "augalinga-app";
    string filePath;
    string fileName;
    IAmazonS3 s3Client = new AmazonS3Client(RegionEndpoint.EUNorth1);
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
        filePath = document.FullPath.ToString();
        fileName = document.FileName.ToString();
        outputText.Text = filePath;
    }

    private async void AddDocumentButton_Clicked(object sender, EventArgs e)
    {
        await S3Bucket.UploadFileAsync(s3Client, bucket, fileName, filePath);
        await Shell.Current.Navigation.PopAsync();
    }
}