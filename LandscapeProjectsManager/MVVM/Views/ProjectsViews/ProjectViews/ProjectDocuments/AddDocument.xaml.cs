using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using augalinga.Data.Access;
using augalinga.Data.Entities;
using LandscapeProjectsManager.MVVM.Models;
using LandscapeProjectsManager.MVVM.ViewModels;

namespace LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews.Documents;

    public partial class AddDocument : ContentPage
    {
        string bucket = "augalinga-app";
        string folder = "documents/";
        string filePath;
        string fileName;
        string _projectName;
        IAmazonS3 s3Client = new AmazonS3Client(RegionEndpoint.EUNorth1);
        DocumentsViewModel _documentsViewModel;
        public DataContext DataContext { get; set; } = new DataContext();
        public AddDocument(string projectName, DocumentsViewModel documentsViewModel)
        {
            InitializeComponent();
            _projectName = projectName;
            _documentsViewModel = documentsViewModel;
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }

        private async void DocumentSelect_Clicked(object sender, EventArgs e)
        {
            var document = await FilePicker.Default.PickAsync();
            filePath = document.FullPath.ToString();
            fileName = document.FileName.ToString();
            documentsOutputText.Text = filePath;
        }

        private async void AddDocumentButton_Clicked(object sender, EventArgs e)
        {
            string objectKey = _projectName + "/" + folder + fileName;

            if (!string.IsNullOrWhiteSpace(documentsOutputText.Text))
            {
                var newDocument = new Document
                {
                    Project = _projectName,
                    Link = $"https://{bucket}.s3.amazonaws.com/{objectKey}"
                };

                await DataContext.Documents.AddAsync(newDocument);
                _documentsViewModel.AddDocumentToCollection(newDocument);
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