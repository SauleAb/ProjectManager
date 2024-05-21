using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using augalinga.Data.Entities;
using CommunityToolkit.Maui.Markup;
using LandscapeProjectsManager.MVVM.ViewModels;

namespace LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews.Photos;

public partial class PhotosDisplayPage : ContentPage
{
    string _projectName;
    PhotosViewModel _photosViewModel;
    private Photo _currentPhoto;
    int _currentPhotoIndex;
    string _category;
    string bucket = "augalinga-app";
    IAmazonS3 s3Client = new AmazonS3Client(RegionEndpoint.EUNorth1);
    public PhotosDisplayPage(string projectName, string category)
    {
        InitializeComponent();
        _projectName = projectName;
        PhotoPageTitle.Text = category;
        _category = category.ToLower();
        _photosViewModel = new PhotosViewModel(projectName, _category);
        BindingContext = _photosViewModel;
        LoadPhotos();
        PhotoPopup.BackgroundColor = Color.FromRgba(0, 0, 0, 0.9);
    }

    private async void LoadPhotos()
    {
        foreach (var photo in _photosViewModel.Photos)
        {
            string key = $"{_projectName}/photos/{_category}/{photo.Name}";
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
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) => OnPhotoTapped(photo);
                image.GestureRecognizers.Add(tapGestureRecognizer);

                PhotoDisplay.Children.Add(image);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred while loading the photo: {ex.Message}", "OK");
            }
        }
    }

    private async Task OnPhotoTapped(Photo photo)
    {
        _currentPhoto = photo;
        _currentPhotoIndex = _photosViewModel.Photos.IndexOf(photo);
        await ShowPhoto(_currentPhotoIndex);
    }

    private async Task ShowPhoto(int index)
    {
        if (index >= 0 && index < _photosViewModel.Photos.Count)
        {
            _currentPhoto = _photosViewModel.Photos[index];
            string key = $"{_projectName}/photos/{_category}/{_currentPhoto.Name}";
            string imageUrl = GetPreSignedURL(key);
            PopupImage.Source = ImageSource.FromUri(new Uri(imageUrl));
            PhotoPopup.Opacity = 0;
            PhotoPopup.IsVisible = true;
            await PhotoPopup.FadeTo(1, 250);
        }
    }

    private async void CloseButton_Clicked(object sender, EventArgs e)
    {
        ClosePopup();
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
        string name = _currentPhoto.Name;
        string key = $"{_projectName}/photos/{_category}/{_currentPhoto.Name}";
        bool answer = await DisplayAlert("Alert", $"Are you sure you want to delete {name}?", "Yes", "No");
        if (answer)
        {
            try
            {
                _photosViewModel.RemovePhoto(_currentPhoto.Link);

                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = bucket,
                    Key = key
                };
                await s3Client.DeleteObjectAsync(deleteObjectRequest);

                var imageToRemove = PhotoDisplay.Children
                    .OfType<Image>()
                    .FirstOrDefault(img => img.Source.ToString().Contains(_currentPhoto.Name));

                if (imageToRemove != null)
                {
                    PhotoDisplay.Children.Remove(imageToRemove);
                }

                await ClosePopup();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred while deleting the photo: {ex.Message}", "OK");
            }
        }
    }

    private async Task ClosePopup()
    {
        await PhotoPopup.FadeTo(0, 250);
        PhotoPopup.IsVisible = false;
        PopupImage.Source = null;
        _currentPhoto = null;
    }

    private async void NextButton_Clicked(object sender, EventArgs e)
    {
        _currentPhotoIndex++;
        if (_currentPhotoIndex >= _photosViewModel.Photos.Count)
        {
            _currentPhotoIndex = 0;
        }
        await ShowPhoto(_currentPhotoIndex);
    }

    private async void PreviousButton_Clicked(object sender, EventArgs e)
    {
        _currentPhotoIndex--;
        if (_currentPhotoIndex < 0)
        {
            _currentPhotoIndex = _photosViewModel.Photos.Count - 1;
        }
        await ShowPhoto(_currentPhotoIndex);

    }
}