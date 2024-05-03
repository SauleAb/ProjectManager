using augalinga.Data.Access;
using LandscapeProjectsManager.MVVM.ViewModels;

namespace LandscapeProjectsManager.MVVM.Views.Contacts;

public partial class ContactView : ContentPage
{
    ContactsViewModel viewModel;
    string _category;
    public ContactView(string category)
	{
		InitializeComponent();
		viewModel = new ContactsViewModel(category);
        _category = category;
		ContactsLabel.Text = category;
		BindingContext = viewModel;
	}

    private async void AddContact_Clicked(object sender, EventArgs e)
    {
        var modalPage = new AddContact(_category, viewModel);
        await Navigation.PushModalAsync(modalPage);
    }

    private void DeleteButton_Clicked(object sender, EventArgs e)
    {
        var selectedContact = dataGrid.SelectedRow as augalinga.Data.Entities.Contact;

        viewModel.Contacts.Remove(selectedContact); // remove from local collection

        viewModel.RemoveContact(selectedContact); // remove from database
    }
}