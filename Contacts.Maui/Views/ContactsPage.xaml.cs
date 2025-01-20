using System.Collections.ObjectModel;
using Contacts.UseCases.Interfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.Views;

public partial class ContactsPage : ContentPage
{
    private readonly IViewContactsUseCase _viewContactsUseCase;
    private readonly IDeleteContactUseCase _deleteContactUseCase;

    public ContactsPage(
        IViewContactsUseCase viewContactsUseCase,
        IDeleteContactUseCase deleteContactUseCase)
    {
        _viewContactsUseCase = viewContactsUseCase;
        _deleteContactUseCase = deleteContactUseCase;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        SearchBar.Text = string.Empty;
        
        LoadContacts();
    }

    private async void ListContacts_OnItemSelected(object? sender, SelectedItemChangedEventArgs e)
    {
        if (listContacts.SelectedItem != null)
        {
            await Shell.Current
                .GoToAsync($"{nameof(EditContactPage)}?Id={((Contact)listContacts.SelectedItem).ContactId}");
        }
    }

    private void ListContacts_OnItemTapped(object? sender, ItemTappedEventArgs e)
    {
        listContacts.SelectedItem = null;
    }

    private void BtnAdd_OnClicked(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"{nameof(AddContactPage)}");
    }

    private async void Delete_OnClicked(object? sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var contact = menuItem.CommandParameter as Contact;
        await _deleteContactUseCase.ExecuteAsync(contact.ContactId);
        
        LoadContacts(); 
    }

    private async void LoadContacts()
    {
        var contacts = new ObservableCollection<Contact>(await _viewContactsUseCase.ExecuteAsync(string.Empty));
        listContacts.ItemsSource = contacts;
    }

    private async void SearchBar_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var contacts = new ObservableCollection<Contact>(await _viewContactsUseCase.ExecuteAsync(((SearchBar)sender).Text));
        listContacts.ItemsSource = contacts;
    }
}