namespace LandscapeProjectsManager.MVVM.Views.Projects;

using Amazon.Util;
using augalinga.Data.Entities;
using AWSSDK;
using Microsoft.Maui.Controls;
using LandscapeProjectsManager.MVVM.ViewModels;
using LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews;
using Syncfusion.Maui.Core.Carousel;
using Amazon.S3.Model;
using Windows.Media.Protection.PlayReady;
using Amazon.S3;
using Amazon;

public partial class DocumentsPage : ContentPage
{
    string _projectName;
    string bucket = "augalinga-app";
    string folder = "documents/";
    string filePath;
    string fileName;
    IAmazonS3 s3Client = new AmazonS3Client(RegionEndpoint.EUNorth1);
    DocumentsViewModel _documentsViewModel = new DocumentsViewModel();
    public DocumentsPage(string projectName)
	{
		InitializeComponent();
        _projectName = projectName;
        BindingContext = _documentsViewModel;
        DocumentsLabel.Text = _projectName + " Documents";
	}

    private async void AddDocument_Clicked(object sender, EventArgs e)
    {
        var modalPage = new AddDocument(_projectName);
        await Navigation.PushModalAsync(modalPage);
    }


    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        var selectedDocument = documentsDataGrid.SelectedRow as Document;

        _documentsViewModel.RemoveDocument(selectedDocument.Link); // remove from database and local

        await Models.S3Bucket.DeleteObject(s3Client, bucket, selectedDocument.Link); // remove from bucket
    }

    private async void documentsDataGrid_CellDoubleTapped(object sender, Syncfusion.Maui.DataGrid.DataGridCellDoubleTappedEventArgs e)
    {
        var obj = e.RowData as Document;
        string link = obj.Link;
        await Launcher.OpenAsync(new Uri(link));
    }
}