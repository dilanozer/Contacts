using CommunityToolkit.Maui;
using Contacts.Maui.ViewModels;
using Contacts.Maui.Views_MVVM;
using Contacts.Maui.Views;
using Contacts.Plugins.DataStore.InMemory;
using Contacts.UseCases;
using Contacts.UseCases.Interfaces;
using Contacts.UseCases.PluginInterfaces;
using Microsoft.Extensions.Logging;

namespace Contacts.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<IContactRepository, ContactInMemoryRepository>();
        builder.Services.AddSingleton<IViewContactsUseCase, ViewContactsUseCase>();
        builder.Services.AddSingleton<IViewContactUseCase, ViewContactUseCase>();
        builder.Services.AddTransient<IEditContactUseCase, EditContactUseCase>();
        builder.Services.AddTransient<IAddContactUseCase, AddContactUseCase>();
        builder.Services.AddTransient<IDeleteContactUseCase, DeleteContactUseCase>();

        builder.Services.AddTransient<ContactsViewModel>();
        builder.Services.AddTransient<ContactViewModel>();

        builder.Services.AddTransient<ContactsPage>();
        builder.Services.AddTransient<EditContactPage>();
        builder.Services.AddTransient<AddContactPage>();

        builder.Services.AddTransient<Contacts_MVVM_Page>();
        builder.Services.AddTransient<EditContact_MVVM_Page>();
        builder.Services.AddTransient<AddContact_MVVM_Page>();

        return builder.Build();
    }
}