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
    IEnumerable<FileResult> selectedOrders;
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
        selectedOrders = await FilePicker.Default.PickMultipleAsync();
        foreach(var order in selectedOrders)
        {
            filePath = order.FullPath.ToString();
            fileName = order.FileName.ToString();
            ordersOutputText.Text += $"{fileName}: {filePath}\n";
        }
    }

    private async void AddOrderButton_Clicked(object sender, EventArgs e)
    {
        string objectKey;

        if (!string.IsNullOrWhiteSpace(ordersOutputText.Text))
        {
            foreach(var order in selectedOrders)
            {
                objectKey = _projectName + "/" + folder + order.FileName;
                var newOrder = new Order
                {
                    Project = _projectName,
                    Link = $"https://{bucket}.s3.amazonaws.com/{objectKey}",
                    Name = order.FileName
                };

                await DataContext.Orders.AddAsync(newOrder);
                await Models.S3Bucket.UploadFileAsync(s3Client, bucket, objectKey, order.FullPath);
                await DataContext.SaveChangesAsync();
                _ordersViewModel.AddOrderToCollection(newOrder);
            }
            ((OrdersPage)Shell.Current.Navigation.NavigationStack.Last()).UpdateDataGrid();
            await DisplayAlert("Success!", "The order(s) has been added successfully!", "OK");
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