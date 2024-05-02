using augalinga.Data.Access;
using Microsoft.Maui.Storage;
using augalinga.Data.Entities;
using Amazon.S3;
using Amazon;
using System.Collections.Generic;

namespace LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews.Photos;

public partial class AddPhoto : ContentPage
{
    string bucket = "augalinga-app";
    string folder = "photos/";
    string filePath;
    string fileName;
    string _projectName;
    IAmazonS3 s3Client = new AmazonS3Client(RegionEndpoint.EUNorth1);
    public DataContext DataContext { get; set; } = new DataContext();
    IEnumerable<FileResult> selectedPhotos;
    public AddPhoto(string projectName)
	{
		InitializeComponent();
        _projectName = projectName;
	}
    private async void PhotoSelect_Clicked(object sender, EventArgs e)
    {
        // var customFileType = new FilePickerFileType(
        //         new Dictionary<DevicePlatform, IEnumerable<string>>
        //         {
        //             { DevicePlatform.WinUI, new[] { ".Jpeg", ".Png", ".Videos" } },
        //             { DevicePlatform.macOS, new[] { ".Jpeg", ".Png", ".Videos" } },
        //         });
        var photos = await FilePicker.PickMultipleAsync(new PickOptions
        {
            PickerTitle = "Pick photo(s)",
            FileTypes = FilePickerFileType.Images
        });
        selectedPhotos = photos;

        foreach (var photo in selectedPhotos)
        {
            var filePath = photo.FullPath;
            var fileName = photo.FileName;

            // Use filePath and fileName as needed
            outputText.Text += $"{fileName}: {filePath}\n";
        }
    }

    private async Task<byte[]> ReadFileAsBytes(string filePath)
    {
        try
        {
            using (var stream = File.OpenRead(filePath))
            {
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file as bytes: {ex.Message}");
            return null;
        }
    }

    private async void AddPhotoButton_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(outputText.Text))
        {
            foreach (var photo in selectedPhotos)
            {
                var bytes = await ReadFileAsBytes(photo.FullPath);
                string objectKey = _projectName + "/" + folder + photo.FileName;
                var newPhoto = new Photo
                {
                    Project = _projectName,
                    Category = PhotoCategoryPicker.SelectedItem.ToString(),
                    Title = photo.FileName,
                    Bytes = bytes,
                    Link = $"https://{bucket}.s3.amazonaws.com/{objectKey}"
                };
                await DataContext.Photos.AddAsync(newPhoto);
                await DataContext.SaveChangesAsync();
                await Models.S3Bucket.UploadFileAsync(s3Client, bucket, objectKey, filePath);
            }
            await Shell.Current.Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Alert", "Please enter fill in required fields", "OK");
        }
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }
}