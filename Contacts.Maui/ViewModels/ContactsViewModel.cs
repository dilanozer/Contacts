using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Maui.Views_MVVM;
using Contacts.UseCases.Interfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.ViewModels;

public partial class ContactsViewModel : ObservableObject
{
    private readonly IViewContactsUseCase _viewContactsUseCase;
    private readonly IDeleteContactUseCase _deleteContactUseCase;
    public ObservableCollection<Contact> Contacts { get; set; }

    private string filterText { get; set; }

    public string FilterText
    {
        get { return filterText; }
        set
        {
            filterText = value;
            LoadContactsAsync(filterText);
        }
    }

    public ContactsViewModel(
        IViewContactsUseCase viewContactsUseCase,
        IDeleteContactUseCase deleteContactUseCase)
    {
        _viewContactsUseCase = viewContactsUseCase;
        _deleteContactUseCase = deleteContactUseCase;
        Contacts = new ObservableCollection<Contact>();
    }

    public async Task LoadContactsAsync(string filterText = null)
    {
        Contacts.Clear();

        var contacts = await _viewContactsUseCase.ExecuteAsync(filterText);
        if (contacts != null && contacts.Count > 0)
        {
            foreach (var contact in contacts)
            {
                Contacts.Add(contact);
            }
        }
    }

    [RelayCommand]
    public async Task DeleteContact(int contactId)
    {
        await _deleteContactUseCase.ExecuteAsync(contactId);
        await LoadContactsAsync();
    }

    [RelayCommand]
    public async Task GoToEditContact(int contactId)
    {
        await Shell.Current.GoToAsync($"{nameof(EditContact_MVVM_Page)}?Id={contactId}");
    }

    [RelayCommand]
    public async Task GoToAddContact()
    {
        await Shell.Current.GoToAsync($"{nameof(AddContact_MVVM_Page)}");
    }
}