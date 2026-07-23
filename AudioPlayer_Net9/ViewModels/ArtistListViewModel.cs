using System.Collections.ObjectModel;
using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AudioPlayer_Net9.ViewModels;

public partial class ArtistListViewModel : ViewModelBase, INavigationAware {
  private readonly IMusicLibraryService _musicLibraryService;
  private readonly INavigationService _navigationService;
  public ArtistListViewModel(IMusicLibraryService musicLibraryService, INavigationService navigationService) {
    _musicLibraryService = musicLibraryService;
    _navigationService = navigationService;
    Artists = new ObservableCollection<Artist>(_musicLibraryService.Library.Artists);
  }
  public ObservableCollection<Artist> Artists { get; set; }
  [ObservableProperty] private Artist? selectedArtist;

  partial void OnSelectedArtistChanged(Artist? value) {
    if(value !=null)
      _navigationService.NavigateTo<ArtistDetailViewModel>(value);
  }
  public void OnNavigatedTo(object? parameter) {
    SelectedArtist = null;
  }
}