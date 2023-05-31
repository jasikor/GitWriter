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
                    .AddSingleton<MainWindow>(s => new MainWindow() {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    })
                    .AddSingleton<MainViewModel>()
                    .AddSingleton<IBookService, DummyBookService>();
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