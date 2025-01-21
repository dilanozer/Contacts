using Contacts.UseCases.PluginInterfaces;
using SQLite;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Plugins.DataStore.SQLite;

public class ContactSQLiteRepository : IContactRepository
{
    private SQLiteAsyncConnection _database;

    public ContactSQLiteRepository()
    {
        _database = new SQLiteAsyncConnection(Constants.DatabasePath);
        _database.CreateTableAsync<Contact>();
    }

    public async Task<List<Contact>> GetContactsAsync(string filterText)
    {
        if (string.IsNullOrWhiteSpace(filterText))
        {
            return await _database.Table<Contact>().ToListAsync();
        }

        return await _database.QueryAsync<Contact>(@"
            SELECT *
            FROM Contact
            WHERE
                NAME LIKE ? OR
                EMAIL LIKE ? OR
                PHONE LIKE ? OR
                ADDRESS LIKE ?",
            $"{filterText}%",
            $"{filterText}%",
            $"{filterText}%",
            $"{filterText}%");
    }

    public async Task<Contact> GetContactByIdAsync(int contactId)
    {
        return await _database.Table<Contact>().Where(x => x.ContactId == contactId).FirstOrDefaultAsync();
    }

    public async Task UpdateContact(int contactId, Contact contact)
    {
        if (contactId == contact.ContactId)
        {
            await _database.UpdateAsync(contact);
        }
    }

    public async Task AddContactAsync(Contact contact)
    {
        await _database.InsertAsync(contact);
    }

    public async Task DeleteContactAsync(int contactId)
    {
        var contact = await GetContactByIdAsync(contactId);
        if (contact != null && contact.ContactId == contactId)
        {
            await _database.DeleteAsync(contact);
        }
    }
}