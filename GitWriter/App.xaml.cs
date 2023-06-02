using System.Windows;
using BookModel;
using GitWriter.Services;
using GitWriter.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GitWriter;

public partial class App : Application
{
    private static IHost? AppHost { get; set; }

    public App()
    {
        AppHost = Host
            .CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) => {
                services
                    .AddSingleton<MainWindow>(s =>
                        new MainWindow(s.GetRequiredService<BinderViewModel>()) {
                            DataContext = s.GetRequiredService<MainViewModel>(),
                        })
                    .AddSingleton<MainViewModel>()
                    .AddSingleton<BinderViewModel>()
                    .AddSingleton<IBinderService, DummyBinderService>()
                    .AddSingleton<IBookMetadataService, DummyMetadataService>()
                    .AddSingleton<IBookService, DummyBookService>()
                    .AddSingleton<IObservableBinderService, ObservableBinderService>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();
        var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
        startupForm.Show();
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}