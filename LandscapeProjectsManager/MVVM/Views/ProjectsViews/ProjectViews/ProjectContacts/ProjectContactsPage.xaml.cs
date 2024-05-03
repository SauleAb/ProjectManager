using LandscapeProjectsManager.MVVM.ViewModels;

namespace LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews.ProjectContacts;

public partial class ProjectContactsPage : ContentPage
{
    ContactsViewModel viewModel;
    string _projectName;
    public ProjectContactsPage(string projectName)
    {
        InitializeComponent();
        viewModel = new ContactsViewModel(projectName);
        _projectName = projectName;
        ContactsLabel.Text = $"{_projectName} Contacts";
        BindingContext = viewModel;
    }

    private async void AddContact_Clicked(object sender, EventArgs e)
    {
        var modalPage = new AddProjectContact(_projectName, viewModel);
        await Navigation.PushModalAsync(modalPage);
    }

    private void DeleteButton_Clicked(object sender, EventArgs e)
    {
        var selectedContact = dataGrid.SelectedRow as augalinga.Data.Entities.Contact;

        viewModel.Contacts.Remove(selectedContact); // remove from local collection

        viewModel.RemoveContact(selectedContact); // remove from database
    }
}