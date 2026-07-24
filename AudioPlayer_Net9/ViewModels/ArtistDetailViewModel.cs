using System.Collections.ObjectModel;
using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AudioPlayer_Net9.ViewModels;

public partial class ArtistDetailViewModel : ViewModelBase, INavigationAware {
  private readonly IMusicLibraryService _musicLibraryService;
  private readonly INavigationService _navigationService;
  [ObservableProperty] private Artist? artist;
  [ObservableProperty] private string? imagePath;
  [ObservableProperty] private string? artistInfo;
  [ObservableProperty] private Album? selectedAlbum;

  public ObservableCollection<Album> Albums { get; } = new ObservableCollection<Album>();

  public ArtistDetailViewModel(IMusicLibraryService musicLibraryService, INavigationService navigationService) {
    _musicLibraryService = musicLibraryService;
    _navigationService = navigationService;
  }

  [RelayCommand]
  private void SelectAlbum(Album album) {
    _navigationService.NavigateTo<TrackListViewModel>(album);
  }

  public void OnNavigatedTo(object? parameter) {
    if (parameter is not Artist artist) {
      return;
    }

    Artist = artist;

    var trackCount = 0;
    TimeSpan trackDuration = new TimeSpan();
    foreach (var artistAlbum in Artist.Albums) {
      trackCount +=artistAlbum.Tracks.Count;
      trackDuration += artistAlbum.Tracks.Select(t => t.Duration).Aggregate(TimeSpan.Zero, (total, duration) => total + duration);
    }

    var runTime = $"{(trackDuration.Hours > 0 ? trackDuration.Hours + " hours" : "")} {trackDuration.Minutes:00} mins";
    
    ArtistInfo = $"{Artist.Albums.Count} albums • {trackCount} songs • {runTime}";
    Albums.Clear();
    foreach (var artistAlbum in artist.Albums) {
      Albums.Add(artistAlbum);
    }

    ImagePath = artist.ImagePath;
    SelectedAlbum = null;
  }
}