using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Services;
using AudioPlayer_Net9.ViewModels;
using AudioPlayer_Net9.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AudioPlayer_Net9
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
      public static IHost AppHost { get; private set; } = null;
      public App() {
        AppHost = Host.CreateDefaultBuilder().ConfigureServices((context, services) => {
          ConfigureServices(services);
        }).Build();

      }
      protected override async void OnStartup(StartupEventArgs e) {
        await AppHost.StartAsync();
      var settingsService = AppHost.Services.GetRequiredService<ISettingsService>();
      await settingsService.LoadAsync();
      settingsService.Settings.MusicFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

      if (!String.IsNullOrWhiteSpace(settingsService.Settings.MusicFolder) && Directory.Exists(settingsService.Settings.MusicFolder)) {
        var libraryService = AppHost.Services.GetRequiredService<IMusicLibraryService>();
        await libraryService.LoadLibraryAsync(settingsService.Settings.MusicFolder);
      }

      var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
        base.OnStartup(e);
      }
      protected override async void OnExit(ExitEventArgs e) {
        await AppHost.StopAsync();
        AppHost.Dispose();
        base.OnExit(e);
      }
      private static void ConfigureServices(IServiceCollection services) {
        services.AddSingleton<IAudioPlayerService, AudioPlayerServices>();
        services.AddSingleton<IDialogService, DialogService>();
        services.AddSingleton<IMetadataService, MetadataService>();
        services.AddSingleton<IMusicLibraryService, MusicLibraryService>();
        services.AddSingleton<IFolderPickerService, FolderPickerService>();
        services.AddSingleton<IFileDialogService, FileDialogService>();
        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddSingleton<INavigationService, NavigationService>();

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();
        services.AddSingleton<AlbumListViewModel>();
        services.AddSingleton<AlbumListView>();
        services.AddSingleton<HomeViewModel>();
        services.AddSingleton<HomeView>();
        services.AddSingleton<ArtistListViewModel>();
        services.AddSingleton<ArtistListView>();
        services.AddSingleton<TrackListViewModel>();
        services.AddSingleton<TrackListView>();
        services.AddSingleton<SettingsViewModel>();
        services.AddSingleton<SettingsView>();
        services.AddSingleton<ArtistDetailViewModel>();
        services.AddSingleton<ArtistDetailView>();



        services.AddTransient<EditMetadataWindow>();
        services.AddTransient<EditMetadataViewModel>();
      }
    }

}
