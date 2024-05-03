using augalinga.Data.Entities;
using CommunityToolkit.Maui.Markup;
using LandscapeProjectsManager.MVVM.ViewModels;

namespace LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews.Photos;

public partial class BeforePhotosPage : ContentPage
{
    string _projectName;
    PhotosViewModel _photosViewModel;
    public BeforePhotosPage(string projectName)
    {
        InitializeComponent();
        _projectName = projectName;
        _photosViewModel = new PhotosViewModel(projectName);
        BindingContext = _photosViewModel;
        foreach (var photo in _photosViewModel.Photos)
        {
            var image = new Image
            {
                Source = ImageSource.FromStream(() => new MemoryStream(photo.Bytes)),
                MaximumHeightRequest = 200,
                MaximumWidthRequest = 300,
            };
            PhotoDisplay.Children.Add(image);
        }
    }
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {

    }
}