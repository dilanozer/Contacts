using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Maui.Views_MVVM;
using Contacts.UseCases.Interfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.ViewModels;

public partial class ContactViewModel : ObservableObject
{
    private readonly IViewContactUseCase _viewContactUseCase;
    private readonly IEditContactUseCase _editContactUseCase;
    private readonly IAddContactUseCase _addContactUseCase;
    private Contact contact;
    public Contact Contact
    {
        get => contact;
        set
        {
            SetProperty(ref contact, value);
        }
    }

    public bool IsNameProvided { get; set; }
    public bool IsEmailProvided { get; set; }
    public bool IsEmailFormatValid { get; set; }

    public ContactViewModel(
        IViewContactUseCase viewContactUseCase,
        IEditContactUseCase editContactUseCase,
        IAddContactUseCase addContactUseCase)
    {
        _viewContactUseCase = viewContactUseCase;
        _editContactUseCase = editContactUseCase;
        _addContactUseCase = addContactUseCase;
        Contact = new Contact();
    }

    public async Task LoadContact(int contactId)
    {
        Contact = await _viewContactUseCase.ExecuteAsync(contactId);
    }
    
    [RelayCommand]
    public async Task EditContact()
    {
        if (await ValidateContact())
        {
            await _editContactUseCase.ExecuteAsync(contact.ContactId, contact);
            await Shell.Current.GoToAsync($"{nameof(Contacts_MVVM_Page)}");
        }
    }
    
    [RelayCommand]
    public async Task AddContact()
    {
        if (await ValidateContact())
        {
            await _addContactUseCase.ExecuteAsync(contact);
            await Shell.Current.GoToAsync($"{nameof(Contacts_MVVM_Page)}");
        }
    }

    [RelayCommand]
    public async Task BackToContacts()
    {
        await Shell.Current.GoToAsync($"{nameof(Contacts_MVVM_Page)}");
    }

    private async Task<bool> ValidateContact()
    {
        if (!IsNameProvided)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Name is required", "OK");
            return false;
        }

        if (!IsEmailProvided)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Email is required", "OK");
            return false;
        }

        if (!IsEmailFormatValid)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Email format is incorrect", "OK");
            return false;
        }
        
        return true;
    }
}