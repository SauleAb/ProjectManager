using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using augalinga.Data.Access;
using augalinga.Data.Entities;
using LandscapeProjectsManager.MVVM.Models;

namespace LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews;

public partial class AddDocument : ContentPage
{
    string bucket = "augalinga-app";
    string folder = "documents/";
    string filePath;
    string fileName;
    string _projectName;
    IAmazonS3 s3Client = new AmazonS3Client(RegionEndpoint.EUNorth1);
    public DataContext DataContext { get; set; } = new DataContext();
    public AddDocument(string projectName)
	{
		InitializeComponent();
        _projectName = projectName;
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
        string objectKey = folder + _projectName + "/" + fileName;

        if (!string.IsNullOrWhiteSpace(outputText.Text))
        {
            var newDocument = new Document
            {
                Project = _projectName,
                Link = $"https://{bucket}.s3.amazonaws.com/{objectKey}"
            };

            await DataContext.Documents.AddAsync(newDocument);
            await Models.S3Bucket.UploadFileAsync(s3Client, bucket, objectKey, filePath);
            await DataContext.SaveChangesAsync();

            await Shell.Current.Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Alert", "Please enter fill in required fields", "OK");
        }
    }
}