using Contacts.Maui.Models;
using Contact = Contacts.Maui.Models.Contact;

namespace Contacts.Maui.Views;

public partial class AddContactPage : ContentPage
{
    public AddContactPage()
    {
        InitializeComponent();
    }

    private void BtnCancel_OnClicked(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private void ContactControl_OnSave(object? sender, EventArgs e)
    {
        ContactRepository.AddContact(new Contact
        {
            Name = contactControl.Name,
            Email = contactControl.Email,
            Phone = contactControl.Phone,
            Address = contactControl.Address
        });
        
        Shell.Current.GoToAsync("..");
    }

    private void ContactControl_OnCancel(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private void ContactControl_OnError(object? sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}