using System.Windows.Input;
using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace AudioPlayer_Net9.ViewModels {
  public partial class MainViewModel : ObservableObject {
    private readonly INavigationService _navigationService;
    [ObservableProperty] private ViewModelBase? _currentView;
    public MainViewModel(INavigationService navigationService) {
      _navigationService = navigationService;
      _navigationService.ViewChanged += view => {
        CurrentView = view;
        GoBackCommand.NotifyCanExecuteChanged();
      };
      _navigationService.NavigateTo<HomeViewModel>();
    }

    [RelayCommand]
    private void ShowHome() {
      _navigationService.NavigateTo<HomeViewModel>();
    }

    [RelayCommand]
    private void ShowArtists() {
      _navigationService.NavigateTo<ArtistListViewModel>();
    }

    [RelayCommand]
    private void ShowAlbums() {
      _navigationService.NavigateTo<AlbumListViewModel>();
    }

    [RelayCommand]
    private void ShowTracks() {
      _navigationService.NavigateTo<TrackListViewModel>();
    }

    [RelayCommand]
    private void ShowSettings() {
      _navigationService.NavigateTo<SettingsViewModel>();
    }

    [RelayCommand(CanExecute = nameof(CanGoBack))]
    private void GoBack() {
      _navigationService.GoBack();
    }

    private bool CanGoBack() {
      return _navigationService.CanGoBack;
    }
  }
}
