using System.Collections.ObjectModel;
using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;

namespace AudioPlayer_Net9.ViewModels;

public class TrackListViewModel : ViewModelBase, INavigationAware {
  private readonly IMusicLibraryService _musicLibraryService;
  private ObservableCollection<Track> _tracks = new ObservableCollection<Track>();
  public TrackListViewModel(IMusicLibraryService musicLibraryService) {
    _musicLibraryService = musicLibraryService;
  }
  public void OnNavigatedTo(object? parameter) {
    Tracks.Clear();
    if (parameter is Album album) {
      foreach (var track in album.Tracks) {
        Tracks.Add(track);
      }
    } else {
      foreach (var track in _musicLibraryService.Library.Tracks) {
        Tracks.Add(track);
      }
    }
  }

  public ObservableCollection<Track> Tracks {
    get => _tracks;
    set => SetProperty(ref _tracks, value);
  }
}