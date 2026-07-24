using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace AudioPlayer_Net9.Models;

public class Track : ObservableObject {
  private string _filePath = String.Empty;
  private string _title = String.Empty;
  private uint _trackNumber = 0;
  private string _artist = String.Empty;
  private string _album = String.Empty;
  private uint _year = 0;
  private string _genre = String.Empty;
  private TimeSpan _duration;
  private BitmapImage? _albumArt;
  private string _albumArtPath = String.Empty;
  public string FilePath {
    get => _filePath;
    set => SetProperty(ref _filePath, value);
  }
  public string Title {
    get => _title;
    set => SetProperty(ref _title, value);
  }
  public uint TrackNumber {
    get => _trackNumber;
    set => SetProperty(ref _trackNumber, value);
  }
  public string Artist {
    get => _artist;
    set => SetProperty(ref _artist, value);
  }
  public string Album {
    get => _album;
    set => SetProperty(ref _album, value);
  }
  public uint Year {
    get => _year;
    set => SetProperty(ref _year, value);
  }
  public string Genre {
    get => _genre;
    set => SetProperty(ref _genre, value);
  }
  public TimeSpan Duration {
    get => _duration;
    set {
      if (SetProperty(ref _duration, value)) OnPropertyChanged(nameof(DurationDisplay));
    }
  }
  public string DurationDisplay => Duration.Hours > 0 ? Duration.ToString(@"hh\:mm\:ss") : Duration.ToString(@"mm\:ss");
  public string AlbumArtPath {
    get => _albumArtPath;
    set => SetProperty(ref _albumArtPath, value);
  }
  public BitmapImage? AlbumArt {
    get => _albumArt;
    set => SetProperty(ref _albumArt, value);
  }

  public string GroupLetter {
    get {
      if (String.IsNullOrWhiteSpace(Title))
        return "#";

      char c = char.ToUpper(Title[0]);

      return char.IsLetter(c) ? c.ToString() : "#";
    }
  }
}