using Amazon;
using Amazon.S3;
using augalinga.Data.Entities;
using LandscapeProjectsManager.MVVM.ViewModels;
using LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews.ProjectOrders;
using Syncfusion.Maui.Core.Carousel;

namespace LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews.ProjectOrders;

public partial class OrdersPage : ContentPage
{
    string _projectName;
    string bucket = "augalinga-app";
    IAmazonS3 s3Client = new AmazonS3Client(RegionEndpoint.EUNorth1);
    OrdersViewModel _ordersViewModel;

    public OrdersPage(string projectName)
    {
        InitializeComponent();
        _projectName = projectName;
        _ordersViewModel = new OrdersViewModel(_projectName);
        BindingContext = _ordersViewModel;
        OrdersLabel.Text = $"{_projectName} Orders";
        UpdateDataGrid();
    }

    private async void AddOrder_Clicked(object sender, EventArgs e)
    {
        var modalPage = new AddOrder(_projectName, _ordersViewModel);
        await Navigation.PushModalAsync(modalPage);
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        var selectedOrder = ordersDataGrid.SelectedRow as Order;
        var link = selectedOrder.Link;
        bool answer = await DisplayAlert("Alert", $"Are you sure you want to delete {link}?", "Yes", "No");
        if (answer)
        {
            _ordersViewModel.RemoveOrder(link); // remove from database and local
            await Models.S3Bucket.DeleteObject(s3Client, bucket, link); // remove from bucket
            UpdateDataGrid();
        }
    }

    private async void ordersDataGrid_CellDoubleTapped(object sender, Syncfusion.Maui.DataGrid.DataGridCellDoubleTappedEventArgs e)
    {
        var obj = e.RowData as Order;
        string key = $"{_projectName}/orders/{obj.Name}";
        try
        {
            string localFilePath = await Models.S3Bucket.DownloadObjectAsync(s3Client, bucket, key);
            await Launcher.OpenAsync(new Uri(localFilePath));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred while downloading the file: {ex.Message}", "OK");
        }
    }

    public void UpdateDataGrid()
    {
        ordersDataGrid.ItemsSource = null;
        ordersDataGrid.ItemsSource = _ordersViewModel.Orders;
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
