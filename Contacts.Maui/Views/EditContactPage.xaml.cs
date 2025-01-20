using Contacts.UseCases.Interfaces;

namespace Contacts.Maui.Views;

[QueryProperty(nameof(ContactId), "Id")]
public partial class EditContactPage : ContentPage
{
    private readonly IViewContactUseCase _viewContactUseCase;
    private readonly IEditContactUseCase _editContactUseCase;
    private CoreBusiness.Contact contact;
    public EditContactPage(
        IViewContactUseCase viewContactUseCase,
        IEditContactUseCase editContactUseCase)
    {
        _viewContactUseCase = viewContactUseCase;
        _editContactUseCase = editContactUseCase;
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
            contact = _viewContactUseCase.ExecuteAsync(int.Parse(value)).GetAwaiter().GetResult();
            if (contact != null)
            {
                contactControl.Name = contact.Name;
                contactControl.Email = contact.Email;
                contactControl.Phone = contact.Phone;
                contactControl.Address = contact.Address;
            }
        }
    }

    private async void BtnUpdate_OnClicked(object? sender, EventArgs e)
    {
        contact.Name = contactControl.Name;
        contact.Email = contactControl.Email;
        contact.Phone = contactControl.Phone;
        contact.Address = contactControl.Address;
        
        await _editContactUseCase.ExecuteAsync(contact.ContactId, contact);
        await Shell.Current.GoToAsync("..");
    }

    private void contactControl_OnClicked(object? sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}