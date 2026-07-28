using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;
using CommunityToolkit.Mvvm.Input;

namespace AudioPlayer_Net9.ViewModels;

public partial class TrackListViewModel : ViewModelBase, INavigationAware {
  private readonly IMusicLibraryService _musicLibraryService;
  private readonly IEditMetadataWindowService _editMetadataWindowService;
  private ObservableCollection<Track> _tracks = new ObservableCollection<Track>();
  public TrackListViewModel(IMusicLibraryService musicLibraryService, IEditMetadataWindowService editMetadataWindowService) {
    _musicLibraryService = musicLibraryService;
    _editMetadataWindowService = editMetadataWindowService;
    Tracks = new ObservableCollection<Track>(_musicLibraryService.Library.Tracks);
    TracksView = CollectionViewSource.GetDefaultView(Tracks);
    TracksView.SortDescriptions.Add(new SortDescription(nameof(Track.Title), ListSortDirection.Ascending));
    TracksView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Track.GroupLetter)));
  }
  public void OnNavigatedTo(object? parameter) {
    int index = 0;
    Tracks.Clear();
    if (parameter is Album album) {
      foreach (var track in album.Tracks) {
        track.IsAlternateRow = index % 2 == 1;
        Tracks.Add(track);
        index++;
      }
    } else {
      foreach (var track in _musicLibraryService.Library.Tracks) {
        track.IsAlternateRow = index % 2 == 1;
        Tracks.Add(track);
        index++;
      }
    }
  }

  public ObservableCollection<Track> Tracks {
    get => _tracks;
    set => SetProperty(ref _tracks, value);
  }

  [RelayCommand]
  private void EditTrack(Track track) {
    if(track is null) return;
    _editMetadataWindowService.Show(track);
  }

  public ICollectionView TracksView { get; }
}