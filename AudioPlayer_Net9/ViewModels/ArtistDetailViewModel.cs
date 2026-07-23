using System.Collections.ObjectModel;
using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AudioPlayer_Net9.ViewModels;

public partial class ArtistDetailViewModel : ViewModelBase, INavigationAware {
  [ObservableProperty] private Artist? artist;
  [ObservableProperty] private string? imagePath;
  [ObservableProperty] private string? artistInfo;

  public ObservableCollection<Album> Albums { get; } = new ObservableCollection<Album>();
  public void OnNavigatedTo(object? parameter) {
    if (parameter is not Artist artist) {
      return;
    }

    Artist = artist;

    var b = Artist.Tracks.Select(x => x.Duration);
    TimeSpan c = TimeSpan.Zero;
    foreach (var ts in b) {
      c += ts;
    }

    ArtistInfo = $"{Artist.Albums.Count} albums • {Artist.Albums?.Select(x => x.Tracks).Count()} songs • {c}1 hour 22 mins";
    Albums.Clear();
    foreach (var artistAlbum in artist.Albums) {
      Albums.Add(artistAlbum);
    }

    ImagePath = artist.ImagePath;
  }
}