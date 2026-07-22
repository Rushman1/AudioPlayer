using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AudioPlayer_Net9.ViewModels {
  public partial class MainViewModel : ObservableObject {
    private readonly ArtistListViewModel _artistListViewModel;
    private readonly AlbumListViewModel _albumListViewModel;
    private readonly TrackListViewModel _trackListViewModel;
    private readonly SettingsViewModel _settingsViewModel;
    private readonly HomeViewModel _homeViewModel;
    [ObservableProperty] private ViewModelBase? _currentView;
    public MainViewModel(ArtistListViewModel artistListViewModel, AlbumListViewModel albumListViewModel, TrackListViewModel trackListViewModel, SettingsViewModel settingsViewModel, HomeViewModel homeViewModel) {
      _artistListViewModel = artistListViewModel;
      _albumListViewModel = albumListViewModel;
      _trackListViewModel = trackListViewModel;
      _settingsViewModel = settingsViewModel;
      _homeViewModel = homeViewModel;
      CurrentView = _homeViewModel;
    }

    public HomeViewModel HomeView => _homeViewModel;
    public ArtistListViewModel ArtistView => _artistListViewModel;
    public AlbumListViewModel AlbumView => _albumListViewModel;
    public TrackListViewModel TrackView => _trackListViewModel;
    public SettingsViewModel SettingView => _settingsViewModel;

    [RelayCommand]
    private void Navigate(ViewModelBase view) {
      CurrentView = view;
    }
  }
}
