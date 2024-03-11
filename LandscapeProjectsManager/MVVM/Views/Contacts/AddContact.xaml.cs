using augalinga.Data.Access;

namespace LandscapeProjectsManager.MVVM.Views.Contacts;

public partial class AddContact : ContentPage
{
    public DataContext DataContext { get; set; } = new DataContext();


    public AddContact()
	{
		InitializeComponent();
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

        if (!string.IsNullOrWhiteSpace(contactName) && !string.IsNullOrWhiteSpace(contactNumber))
        {
            var newContact = new augalinga.Data.Entities.Contact
            {
                Category = category,
                Name = contactName,
                Number = contactNumber,
                Address = contactAddress,
            };

            await DataContext.Contacts.AddAsync(newContact);
            await DataContext.SaveChangesAsync();

            await Shell.Current.Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Alert", "Please enter fill in required fields", "OK");
        }
    }
}