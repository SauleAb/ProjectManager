using Amazon;
using Amazon.S3;
using augalinga.Data.Entities;
using LandscapeProjectsManager.MVVM.ViewModels;

namespace LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews.ProjectFinances;

public partial class FinancesPage : ContentPage
{
    string _projectName;
    string bucket = "augalinga-app";
    IAmazonS3 s3Client = new AmazonS3Client(RegionEndpoint.EUNorth1);
    ExpensesViewModel _expensesViewModel;
    private decimal _income;
    private decimal _outcome;
    private decimal _total;
    public FinancesPage(string projectName)
    {
        InitializeComponent();
        _projectName = projectName;
        _expensesViewModel = new ExpensesViewModel(_projectName);
        BindingContext = _expensesViewModel;
        FinancesLabel.Text = $"{_projectName} Finances";
        _income = _expensesViewModel.GetIncome();
        _outcome = _expensesViewModel.GetOutcome();
        _total = _expensesViewModel.GetTotal();
        IncomeLabel.Text = $"{_income.ToString()}€";
        OutcomeLabel.Text = $"{_outcome.ToString()}€";
        TotalLabel.Text = $"{_total.ToString()}€";

    }

    private async void AddExpense_Clicked(object sender, EventArgs e)
    {
        var modalPage = new AddExpense(_projectName, _expensesViewModel);
        await Navigation.PushModalAsync(modalPage);
    }

    private void DeleteButton_Clicked(object sender, EventArgs e)
    {
        var selectedExpense = financesDataGrid.SelectedRow as Expense;
        _expensesViewModel.RemoveExpense(selectedExpense.Id); // remove from database and local
    }
}