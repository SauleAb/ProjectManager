using LandscapeProjectsManager.MVVM.ViewModels;

namespace LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews.ProjectFinances;

public partial class AddExpense : ContentPage
{
	private string _projectName;
	private ExpensesViewModel _expensesViewModel;
	public AddExpense(string projectName, ExpensesViewModel expensesViewModel)
	{
		InitializeComponent();
		_projectName = projectName;
		_expensesViewModel = expensesViewModel;
	}
}