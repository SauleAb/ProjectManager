using augalinga.Data.Access;
using LandscapeProjectsManager.MVVM.ViewModels;

namespace LandscapeProjectsManager.MVVM.Views.Contacts;

public partial class AddContact : ContentPage
{
    public DataContext DataContext { get; set; } = new DataContext();
    private ContactsViewModel _contactsViewModel;

    public AddContact()
	{
		InitializeComponent();
	}

    public AddContact(string category, ContactsViewModel contactsViewModel)
    {
        InitializeComponent();
        ContactCategoryPicker.SelectedItem = category;
        _contactsViewModel = contactsViewModel;
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }

    private async void AddContactButton_Clicked(object sender, EventArgs e)
    {
        string category = ContactCategoryPicker.SelectedItem.ToString();
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
            if (_contactsViewModel == null)
            {
                _contactsViewModel = new ContactsViewModel(category);
            }
            _contactsViewModel.AddContactToCollection(newContact);
            await Shell.Current.Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Alert", "Please enter fill in required fields", "OK");
        }
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