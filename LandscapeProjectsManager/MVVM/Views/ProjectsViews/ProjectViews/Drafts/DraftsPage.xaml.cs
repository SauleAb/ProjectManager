using Amazon;
using Amazon.S3;
using augalinga.Data.Entities;
using LandscapeProjectsManager.MVVM.ViewModels;
using Microsoft.Maui.Controls;
using Syncfusion.Maui.DataGrid;
using System;

namespace LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews.Drafts
{
    public partial class DraftsPage : ContentPage
    {
        string _projectName;
        string bucket = "augalinga-app";
        IAmazonS3 s3Client = new AmazonS3Client(RegionEndpoint.EUNorth1);
        DraftsViewModel _draftsViewModel;

        public DraftsPage(string projectName)
        {
            InitializeComponent();
            _projectName = projectName;
            _draftsViewModel = new DraftsViewModel(_projectName);
            BindingContext = _draftsViewModel;
            DraftsLabel.Text = $"{_projectName} Drafts";
        }

        private async void AddDraft_Clicked(object sender, EventArgs e)
        {
            var modalPage = new AddDraft(_projectName, _draftsViewModel);
            await Navigation.PushModalAsync(modalPage);
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var selectedDraft = draftsDataGrid.SelectedRow as Draft;
            _draftsViewModel.RemoveDraft(selectedDraft.Link); // remove from database and local
            await Models.S3Bucket.DeleteObject(s3Client, bucket, selectedDraft.Link); // remove from bucket
        }

        private async void draftsDataGrid_CellDoubleTapped(object sender, DataGridCellDoubleTappedEventArgs e)
        {
            var obj = e.RowData as Draft;
            string link = obj.Link;
            await Launcher.OpenAsync(new Uri(link));
        }
    }
}
