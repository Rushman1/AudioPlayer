using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AudioPlayer_Net9.ViewModels;

public partial class AlbumDetailViewModel : ViewModelBase, INavigationAware {
  private readonly IMusicLibraryService _musicLibraryService;
  private readonly INavigationService _navigationService;
  private readonly IEditMetadataWindowService _editMetadataWindowService;
  [ObservableProperty] private Album? album;
  [ObservableProperty] private string? albumInfo;
  private ObservableCollection<Track> _tracks = new ObservableCollection<Track>();


  public AlbumDetailViewModel(IMusicLibraryService musicLibraryService, INavigationService navigationService, IEditMetadataWindowService editMetadataWindowService) {
    _musicLibraryService = musicLibraryService;
    _navigationService = navigationService;
    _editMetadataWindowService = editMetadataWindowService;
  }

  public void OnNavigatedTo(object? parameter) {
    if (parameter is not Album album) return;
    Album = album;

    var trackCount = Album.Tracks.Count;
    TimeSpan tracksDuration = new TimeSpan();
    tracksDuration += Album.Tracks.Select(t => t.Duration).Aggregate(TimeSpan.Zero, (total, duration) => total + duration);

    var runTime = $"{(tracksDuration.Hours > 0 ? tracksDuration.Hours + " hours" : "")} {tracksDuration.Minutes}:{tracksDuration.Seconds} run time";
    AlbumInfo = $"{Album.Year} • {Album.Tracks[0].Genre} • {trackCount} songs • {runTime}";

    Tracks = new ObservableCollection<Track>(Album.Tracks);
    TracksView = CollectionViewSource.GetDefaultView(Tracks);
    TracksView.SortDescriptions.Add(new SortDescription(nameof(Track.TrackNumber), ListSortDirection.Ascending));

    int index = 0;
    Tracks.Clear();
    foreach (var albumTrack in Album.Tracks) {
      albumTrack.IsAlternateRow = true;
      albumTrack.IsAlternateRow = index % 2 != 1;
      Tracks.Add(albumTrack);
      index++;
    }
  }

  public ObservableCollection<Track> Tracks {
    get => _tracks;
    set => SetProperty(ref _tracks, value);
  }

  [RelayCommand]
  private void EditTrack(Track track) {
    if (track is null) return;
    _editMetadataWindowService.Show(track);
  }

  public ICollectionView TracksView { get; set; }

}