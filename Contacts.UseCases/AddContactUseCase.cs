using Contacts.UseCases.Interfaces;
using Contacts.UseCases.PluginInterfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.UseCases;

public class AddContactUseCase : IAddContactUseCase
{
    private readonly IContactRepository _contactRepository;

    public AddContactUseCase(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task ExecuteAsync(Contact contact)
    {
        await _contactRepository.AddContactAsync(contact);
    }
}