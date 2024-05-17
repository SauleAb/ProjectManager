using Amazon;
using Amazon.S3;
using augalinga.Data.Access;
using augalinga.Data.Entities;
using LandscapeProjectsManager.MVVM.ViewModels;

namespace LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews.Drafts;
    public partial class AddDraft : ContentPage
    {
        string bucket = "augalinga-app";
        string folder = "drafts/";
        string filePath;
        string fileName;
        string _projectName;
        IEnumerable<FileResult> selectedDrafts;
        IAmazonS3 s3Client = new AmazonS3Client(RegionEndpoint.EUNorth1);
        public DataContext DataContext { get; set; } = new DataContext();
        private DraftsViewModel _draftsViewModel;
        public AddDraft(string projectName, DraftsViewModel draftsViewModel)
        {
            InitializeComponent();
            _projectName = projectName;
            _draftsViewModel = draftsViewModel;
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }

        private async void DraftSelect_Clicked(object sender, EventArgs e)
        {
            selectedDrafts = await FilePicker.Default.PickMultipleAsync();
            foreach (var draft in selectedDrafts)
            {
                filePath = draft.FullPath.ToString();
                fileName = draft.FileName.ToString();
                draftsOutputText.Text += $"{fileName}: {filePath}\n";
            }
        }

    private async void AddDraftButton_Clicked(object sender, EventArgs e)
    {
        string objectKey;

        if (!string.IsNullOrWhiteSpace(draftsOutputText.Text))
        {
            foreach (var draft in selectedDrafts)
            {
                objectKey = _projectName + "/" + folder + draft.FileName;
                var newDraft = new Draft
                {
                    Project = _projectName,
                    Link = $"https://{bucket}.s3.amazonaws.com/{objectKey}",
                    Name = draft.FileName
                };
                await DataContext.Drafts.AddAsync(newDraft);
                await Models.S3Bucket.UploadFileAsync(s3Client, bucket, objectKey, draft.FullPath);
                await DataContext.SaveChangesAsync();
                _draftsViewModel.AddDraftToCollection(newDraft);
            };
            ((DraftsPage)Shell.Current.Navigation.NavigationStack.Last()).UpdateDataGrid();
            await DisplayAlert("Success!", "The draft(s) has been added successfully!", "OK");
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