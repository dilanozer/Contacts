using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.UseCases.Interfaces;

public interface IAddContactUseCase
{
    Task ExecuteAsync(Contact contact);
}