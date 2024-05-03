using augalinga.Data.Access;
using LandscapeProjectsManager.MVVM.ViewModels;

namespace LandscapeProjectsManager.MVVM.Views.ProjectsViews.ProjectViews.ProjectContacts;

public partial class AddProjectContact : ContentPage
{
    public DataContext DataContext { get; set; } = new DataContext();
    private ContactsViewModel _contactsViewModel;
    private string _projectName;
    public AddProjectContact(string projectName, ContactsViewModel contactsViewModel)
    {
        InitializeComponent();
        _contactsViewModel = contactsViewModel;
        _projectName = projectName;
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }

    private async void AddContactButton_Clicked(object sender, EventArgs e)
    {
        string category = _projectName;
        string contactName = EntryName.Text;
        string contactNumber = EntryPhone.Text;
        string contactAddress = EntryAddress.Text;
        string contactNotes = EntryNotes.Text;

        if (!string.IsNullOrWhiteSpace(contactName) && !string.IsNullOrWhiteSpace(contactNumber))
        {
            var newContact = new augalinga.Data.Entities.Contact
            {
                Category = category,
                Name = contactName,
                Number = contactNumber,
                Address = contactAddress,
                Notes = contactNotes
            };

            await DataContext.Contacts.AddAsync(newContact);
            await DataContext.SaveChangesAsync();
            _contactsViewModel.AddContactToCollection(newContact);
            await Shell.Current.Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Alert", "Please enter fill in required fields", "OK");
        }
    }
}