using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;

namespace AudioPlayer_Net9.ViewModels;

public class TrackListViewModel : ViewModelBase, INavigationAware {
  private readonly IMusicLibraryService _musicLibraryService;
  private ObservableCollection<Track> _tracks = new ObservableCollection<Track>();
  public TrackListViewModel(IMusicLibraryService musicLibraryService) {
    _musicLibraryService = musicLibraryService;
    Tracks = new ObservableCollection<Track>(_musicLibraryService.Library.Tracks);
    TracksView = CollectionViewSource.GetDefaultView(Tracks);
    TracksView.SortDescriptions.Add(new SortDescription(nameof(Track.Title), ListSortDirection.Ascending));
    TracksView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Track.GroupLetter)));
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

  public ICollectionView TracksView { get; }
}