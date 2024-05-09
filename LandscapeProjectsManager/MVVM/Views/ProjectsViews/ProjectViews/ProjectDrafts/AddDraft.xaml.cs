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
            var draft = await FilePicker.Default.PickAsync();
            filePath = draft.FullPath.ToString();
            fileName = draft.FileName.ToString();
            draftsOutputText.Text = filePath;
        }

        private async void AddDraftButton_Clicked(object sender, EventArgs e)
        {
            string objectKey = _projectName + "/" + folder + fileName;

            if (!string.IsNullOrWhiteSpace(draftsOutputText.Text))
            {
                var newDraft = new Draft
                {
                    Project = _projectName,
                    Link = $"https://{bucket}.s3.amazonaws.com/{objectKey}"
                };

                await DataContext.Drafts.AddAsync(newDraft);
                await Models.S3Bucket.UploadFileAsync(s3Client, bucket, objectKey, filePath);
                await DataContext.SaveChangesAsync();
                _draftsViewModel.AddDraftToCollection(newDraft);
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Alert", "Please enter fill in required fields", "OK");
            }
    }
}