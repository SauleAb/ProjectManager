using LandscapeProjectsManager.MVVM.ViewModels;

namespace LandscapeProjectsManager.MVVM.Views.Contacts;

public partial class ContactView : ContentPage
{
    ContactsViewModel viewModel;
    public ContactView(string category)
	{
		InitializeComponent();
		viewModel = new ContactsViewModel(category);
		ContactsLabel.Text = category;
		BindingContext = viewModel;
	}
}