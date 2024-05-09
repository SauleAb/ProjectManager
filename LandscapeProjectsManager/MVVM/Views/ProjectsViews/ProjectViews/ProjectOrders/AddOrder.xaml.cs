using Amazon;
using Amazon.S3;
using augalinga.Data.Access;
using augalinga.Data.Entities;
using LandscapeProjectsManager.MVVM.ViewModels;

namespace LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews.ProjectOrders;

public partial class AddOrder : ContentPage
{
    string bucket = "augalinga-app";
    string folder = "orders/";
    string filePath;
    string fileName;
    string _projectName;
    IAmazonS3 s3Client = new AmazonS3Client(RegionEndpoint.EUNorth1);
    public DataContext DataContext { get; set; } = new DataContext();
    private OrdersViewModel _ordersViewModel;
    public AddOrder(string projectName, OrdersViewModel ordersViewModel)
    {
        InitializeComponent();
        _projectName = projectName;
        _ordersViewModel = ordersViewModel;
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }

    private async void OrderSelect_Clicked(object sender, EventArgs e)
    {
        var order = await FilePicker.Default.PickAsync();
        filePath = order.FullPath.ToString();
        fileName = order.FileName.ToString();
        draftsOutputText.Text = filePath;
    }

    private async void AddOrderButton_Clicked(object sender, EventArgs e)
    {
        string objectKey = _projectName + "/" + folder + fileName;

        if (!string.IsNullOrWhiteSpace(draftsOutputText.Text))
        {
            var newOrder = new Order
            {
                Project = _projectName,
                Link = $"https://{bucket}.s3.amazonaws.com/{objectKey}"
            };

            await DataContext.Orders.AddAsync(newOrder);
            await Models.S3Bucket.UploadFileAsync(s3Client, bucket, objectKey, filePath);
            await DataContext.SaveChangesAsync();
            _ordersViewModel.AddDraftToCollection(newOrder);
            ((OrdersPage)Shell.Current.Navigation.NavigationStack.Last()).UpdateDataGrid();
            await DisplayAlert("Success!", "The order(s) has been added successfully!", "OK");
            await Shell.Current.Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Alert", "Please first upload the files you want to add", "OK");
        }
    }
}