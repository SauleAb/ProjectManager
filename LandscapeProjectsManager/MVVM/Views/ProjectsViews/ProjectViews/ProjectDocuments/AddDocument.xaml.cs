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
        IEnumerable<FileResult> selectedDocuments;
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
            selectedDocuments = await FilePicker.Default.PickMultipleAsync();
            foreach (var document in selectedDocuments)
            {
                filePath = document.FullPath.ToString();
                fileName = document.FileName.ToString();
                documentsOutputText.Text += $"{fileName}: {filePath}\n";
            }
    }

        private async void AddDocumentButton_Clicked(object sender, EventArgs e)
        {
        string objectKey;

        if (!string.IsNullOrWhiteSpace(documentsOutputText.Text))
        {
            foreach (var document in selectedDocuments)
            {
                objectKey = _projectName + "/" + folder + document.FileName;
                var newDocument = new Document
                {
                    Project = _projectName,
                    Link = $"https://{bucket}.s3.amazonaws.com/{objectKey}"
                };
                await DataContext.Documents.AddAsync(newDocument);
                await Models.S3Bucket.UploadFileAsync(s3Client, bucket, objectKey, document.FullPath);
                await DataContext.SaveChangesAsync();
                _documentsViewModel.AddDocumentToCollection(newDocument);
            };
            ((DocumentsPage)Shell.Current.Navigation.NavigationStack.Last()).UpdateDataGrid();
            await DisplayAlert("Success!", "The document(s) has been added successfully!", "OK");
            await Shell.Current.Navigation.PopAsync();
        }
        else
            {
                await DisplayAlert("Alert", "Please first upload the files you want to add", "OK");
            }
        }
    private void PointerGestureRecognizer_PointerEntered(object sender, PointerEventArgs e)
    {
        ((Button)sender).BackgroundColor = Color.FromRgb(240, 240, 240);
    }

    private void PointerGestureRecognizer_PointerExited(object sender, PointerEventArgs e)
    {
        ((Button)sender).BackgroundColor = Color.FromRgb(255, 255, 255);
    }

}