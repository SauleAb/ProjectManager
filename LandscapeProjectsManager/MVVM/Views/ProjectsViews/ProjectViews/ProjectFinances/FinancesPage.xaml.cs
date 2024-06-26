using Amazon;
using Amazon.S3;
using augalinga.Data.Entities;
using LandscapeProjectsManager.MVVM.ViewModels;

namespace LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews.ProjectFinances;

public partial class FinancesPage : ContentPage
{
    string _projectName;
    ExpensesViewModel _expensesViewModel;
    public FinancesPage(string projectName)
    {
        InitializeComponent();
        _projectName = projectName;
        _expensesViewModel = new ExpensesViewModel(_projectName);
        BindingContext = _expensesViewModel;
        FinancesLabel.Text = $"{_projectName} Finances";
        UpdateLabels();
    }

    public void UpdateLabels()
    {
        IncomeLabel.Text = $"{_expensesViewModel.Income}�";
        OutcomeLabel.Text = $"{_expensesViewModel.Outcome}�";
        TotalLabel.Text = $"{_expensesViewModel.Total}�";
    }

    private async void AddExpense_Clicked(object sender, EventArgs e)
    {
        var modalPage = new AddExpense(_projectName, _expensesViewModel);
        await Navigation.PushModalAsync(modalPage);
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        var selectedExpense = financesDataGrid.SelectedRow as Expense;
        bool answer = await DisplayAlert("Alert", "Are you sure you want to delete this transaction?", "Yes", "No");
        if (answer)
        {
            _expensesViewModel.RemoveExpense(selectedExpense.Id); // remove from database and local
            UpdateDataGrid();
            UpdateLabels();
        }
    }

    public void UpdateDataGrid()
    {
        financesDataGrid.ItemsSource = null;
        financesDataGrid.ItemsSource = _expensesViewModel.Expenses; //.Take(5).ToList()
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