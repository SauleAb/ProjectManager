using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using augalinga.Data.Entities;
using CommunityToolkit.Maui.Markup;
using LandscapeProjectsManager.MVVM.ViewModels;

namespace LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews.Photos;

public partial class BeforePhotosPage : ContentPage
{
    string _projectName;
    PhotosViewModel _photosViewModel;
    string _category;
    string bucket = "augalinga-app";
    IAmazonS3 s3Client = new AmazonS3Client(RegionEndpoint.EUNorth1);
    public BeforePhotosPage(string projectName, string category)
    {
        InitializeComponent();
        _projectName = projectName;
        _category = category;
        _photosViewModel = new PhotosViewModel(projectName, _category);
        BindingContext = _photosViewModel;
        LoadPhotos();
    }

    private async void LoadPhotos()
    {
        foreach (var photo in _photosViewModel.Photos)
        {
            string key = $"{_projectName}/photos/{photo.Name}";
            try
            {
                string url = GetPreSignedURL(key);
                var image = new Image
                {
                    Source = ImageSource.FromUri(new Uri(url)),
                    WidthRequest = 300,
                    HeightRequest = 200,
                    Margin = new Thickness(5)
                };
                PhotoDisplay.Children.Add(image);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred while loading the photo: {ex.Message}", "OK");
            }
        }
    }

    private string GetPreSignedURL(string key)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = bucket,
            Key = key,
            Expires = DateTime.Now.AddMinutes(60)
        };
        return s3Client.GetPreSignedURL(request);
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {

    }
}