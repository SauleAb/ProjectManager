using Amazon.Util;
using augalinga.Data.Entities;
using AWSSDK;
using Microsoft.Maui.Controls;
using Syncfusion.Maui.Core.Carousel;
using Amazon.S3.Model;
using Amazon.S3;
using Amazon;
using LandscapeProjectsManager.MVVM.ViewModels;

namespace LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews.Documents;

    public partial class DocumentsPage : ContentPage
    {
        string _projectName;
        string bucket = "augalinga-app";
        IAmazonS3 s3Client = new AmazonS3Client(RegionEndpoint.EUNorth1);
        DocumentsViewModel _documentsViewModel;
        public DocumentsPage(string projectName)
    	{
    		InitializeComponent();
            _projectName = projectName;
            _documentsViewModel = new DocumentsViewModel(_projectName);
            BindingContext = _documentsViewModel;
            DocumentsLabel.Text = _projectName + " Documents";
            UpdateDataGrid();
        }

        private async void AddDocument_Clicked(object sender, EventArgs e)
        {
            var modalPage = new AddDocument(_projectName, _documentsViewModel);
            await Navigation.PushModalAsync(modalPage);
        }


        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var selectedDocument = documentsDataGrid.SelectedRow as Document;
            string link = selectedDocument.Link;
            bool answer = await DisplayAlert("Alert", $"Are you sure you want to delete {link}?", "Yes", "No");
            if (answer)
            {
                _documentsViewModel.RemoveDocument(link); // remove from database and local
                await Models.S3Bucket.DeleteObject(s3Client, bucket, link); // remove from bucket
                UpdateDataGrid();
            }
    }

    private async void documentsDataGrid_CellDoubleTapped(object sender, Syncfusion.Maui.DataGrid.DataGridCellDoubleTappedEventArgs e)
        {
            var obj = e.RowData as Document;
            string link = obj.Link;
            await Launcher.OpenAsync(new Uri(link));
        }

        public void UpdateDataGrid()
        {
            documentsDataGrid.ItemsSource = null;
            documentsDataGrid.ItemsSource = _documentsViewModel.Documents;
        }
}
