using augalinga.Data.Access;
using augalinga.Data.Entities;
using LandscapeProjectsManager.MVVM.ViewModels;

namespace LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews.ProjectFinances;

public partial class AddExpense : ContentPage
{
	private string _projectName;
	private ExpensesViewModel _expensesViewModel;
    public DataContext DataContext { get; set; } = new DataContext();

    public AddExpense(string projectName, ExpensesViewModel expensesViewModel)
	{
		InitializeComponent();
		_projectName = projectName;
		_expensesViewModel = expensesViewModel;
		ExpenseDate.Date = DateTime.Now;
	}

    private async void OnAddButtonClicked(object sender, EventArgs e)
    {
        string expenseDescription = ExpenseEntry.Text;
        decimal amount = Convert.ToDecimal(ExpenseAmount.Value);
        DateOnly date = new DateOnly(ExpenseDate.Date.Year, ExpenseDate.Date.Month, ExpenseDate.Date.Day);
        string type = "";
        if (TypePicker.SelectedItem.ToString() == "Outcome")
        {
            type = "-";
        }
        if (TypePicker.SelectedItem.ToString() == "Income")
        {
            type = "+";
        }
        if (!string.IsNullOrWhiteSpace(expenseDescription) && !string.IsNullOrWhiteSpace(amount.ToString()) && !string.IsNullOrWhiteSpace(type))
        {
            var newExpense = new Expense
            {
                Transaction = expenseDescription,
                Amount = amount,
                Date = date,
                Project = _projectName,
                Type = type
                
            };

            DataContext.Expenses.Add(newExpense);

            await DataContext.SaveChangesAsync();

            _expensesViewModel.AddExpenseToCollection(newExpense);

            ((FinancesPage)Shell.Current.Navigation.NavigationStack.Last()).UpdateLabels();
            ((FinancesPage)Shell.Current.Navigation.NavigationStack.Last()).UpdateDataGrid();
            await DisplayAlert("Success!", "The transaction has been added successfully!", "OK");
            await Shell.Current.Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Alert", "Fill in all required fields!", "OK");
        }
    }
    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }
}