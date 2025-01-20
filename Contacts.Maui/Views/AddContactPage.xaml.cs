using Contacts.UseCases.Interfaces;

namespace Contacts.Maui.Views;

public partial class AddContactPage : ContentPage
{
    private readonly IAddContactUseCase _addContactUseCase;

    public AddContactPage(IAddContactUseCase addContactUseCase)
    {
        _addContactUseCase = addContactUseCase;
        InitializeComponent();
    }

    private void BtnCancel_OnClicked(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private async void ContactControl_OnSave(object? sender, EventArgs e)
    {
        await _addContactUseCase.ExecuteAsync(new CoreBusiness.Contact
        {
            Name = contactControl.Name,
            Email = contactControl.Email,
            Phone = contactControl.Phone,
            Address = contactControl.Address
        });
        
        await Shell.Current.GoToAsync("..");
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