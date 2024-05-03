using augalinga.Data.Access;
using augalinga.Data.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandscapeProjectsManager.MVVM.ViewModels
{
    public class ExpensesViewModel
    {
        private string _projectName;
        public ExpensesViewModel(string projectName)
        {
            _projectName = projectName;
            LoadExpenses(_projectName);
        }

        public decimal GetIncome()
        {
            var incomeExpenses = Expenses.Where(expense => expense.Type == "+");

            if (!incomeExpenses.Any())
            {
                return 0;
            }

            return incomeExpenses.Sum(expense => expense.Amount);
        }

        public decimal GetOutcome()
        {
            var outcomeExpenses = Expenses.Where(expense => expense.Type == "-");

            if (!outcomeExpenses.Any())
            {
                return 0;
            }

            return outcomeExpenses.Sum(expense => expense.Amount);
        }

        public decimal GetTotal() //add to project entity
        {
            if (Expenses.Count == 0)
            {
                return 0;
            }
            return GetIncome() - GetOutcome();
        }

        private ObservableCollection<Expense> _expenses;
        public ObservableCollection<Expense> Expenses
        {
            get => _expenses;
            set
            {
                _expenses = value;
                OnPropertyChanged(nameof(Expenses));
            }
        }

        public void AddExpenseToCollection(Expense expense)
        {
            Expenses.Add(expense);
            LoadExpenses(_projectName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadExpenses(string projectName)
        {
            using (var context = new DataContext())
            {
                var expenses = context.Expenses.Where(expense => expense.Project == projectName).ToList();

                Expenses = new ObservableCollection<Expense>(expenses);
            }
        }

        public void RemoveExpense(int expenseId)
        {
            //local
            var expenseToRemove = Expenses.FirstOrDefault(p => p.Id == expenseId);
            Expenses.Remove(expenseToRemove);

            //database
            using (var dbContext = new DataContext())
            {
                dbContext.Expenses.Remove(expenseToRemove);
                dbContext.SaveChanges();
            }

            LoadExpenses(_projectName);
        }
    }
}
