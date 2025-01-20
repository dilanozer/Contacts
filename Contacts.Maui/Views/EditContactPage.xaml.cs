using Contacts.Maui.Models;

namespace Contacts.Maui.Views;
using Contact = Models.Contact;

[QueryProperty(nameof(ContactId), "Id")]
public partial class EditContactPage : ContentPage
{
    private Contact contact;
    
    public EditContactPage()
    {
        InitializeComponent();
    }
    
    private void BtnCancel_OnClicked(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }
    
    public string ContactId
    {
        set
        {
            contact = ContactRepository.GetContactById(int.Parse(value));
            if (contact != null)
            {
                contactControl.Name = contact.Name;
                contactControl.Email = contact.Email;
                contactControl.Phone = contact.Phone;
                contactControl.Address = contact.Address;
            }
        }
    }

    private void BtnUpdate_OnClicked(object? sender, EventArgs e)
    {
        contact.Name = contactControl.Name;
        contact.Email = contactControl.Email;
        contact.Phone = contactControl.Phone;
        contact.Address = contactControl.Address;
        
        ContactRepository.UpdateContact(contact.ContactId, contact);
        Shell.Current.GoToAsync("..");
    }

    private void contactControl_OnClicked(object? sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}