using LandscapeProjectsManager.MVVM.ViewModels;

namespace LandscapeProjectsManager.MVVM.Views.Projects.Project;

public partial class AfterPhotosPage : ContentPage
{
    string _projectName;
    PhotosViewModel _photosViewModel;
    string _category;
    public AfterPhotosPage(string projectName, string category)
	{
		InitializeComponent();
        _projectName = projectName;
        _category = category;
        _photosViewModel = new PhotosViewModel(projectName, _category);
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