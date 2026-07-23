using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;

namespace AudioPlayer_Net9.Services {
  public class MusicLibraryService : IMusicLibraryService {
    private readonly IMetadataService _metadataService;
    public MusicLibrary? Library { get; set; }
    public MusicLibraryService(IMetadataService metadataService) {
      _metadataService = metadataService;
    }
    public async Task LoadLibraryAsync(string rootFolder) {
      await Task.Run(() => {
        var audioFiles = Directory.EnumerateFiles(rootFolder, "*.*", SearchOption.AllDirectories).Where(IsAudioFile).ToList();
        var tracks = audioFiles.Select(file => _metadataService.Load(file)).ToList();
        var artists = new ObservableCollection<Artist>(tracks
          .GroupBy(x => x.Artist)
          .Select(artistGroup => new Artist() {
            Name = artistGroup.Key,
            TrackCount = artistGroup.Count(),
            ImagePath = FindArtistImage(Directory.GetParent(artistGroup.Select(t=> Path.GetDirectoryName(t.FilePath)).First()??String.Empty)?.FullName??String.Empty),
            Albums = new ObservableCollection<Album>(artistGroup
              .GroupBy(t => t.Album)
              .Select(albumGroup => {
                var firstTrack = albumGroup.First();
                return new Album() { Name = albumGroup.Key, Artist = firstTrack.Artist, AlbumArt = firstTrack.AlbumArt, Year = firstTrack.Year, Tracks = new ObservableCollection<Track>(albumGroup.OrderBy(t => t.TrackNumber)) };
              })
              .OrderBy(z => z.Name))
          })
          .OrderBy(a => a.Name));

        var albums = new ObservableCollection<Album>(tracks.GroupBy(x => x.Album)
          .Select(albumGroup => {
            var firstTrack = albumGroup.First();
            return new Album() {
              Name = albumGroup.Key,
              Artist = firstTrack.Artist,
              AlbumArt = firstTrack.AlbumArt,
              Year = firstTrack.Year,
              Tracks = new ObservableCollection<Track>(albumGroup.OrderBy(t => t.TrackNumber))
            };
          }));

        //var albums = tracks.GroupBy(x => x.Album).OrderBy(g => g.Key).Select(g => new Album() { Name = g.Key }).ToList();
        //var albums = new ObservableCollection<Album>(
        //  tracks.GroupBy(x => x.Album).Select(albumTracks => new Album() {
        //    Name = albumTracks.Key,
        //    Tracks = new ObservableCollection<Track>(albumTracks
        //      .GroupBy(t => t.Title)
        //      .Select(albumTrack => new Track() { Title = albumTrack.Key, FilePath = albumTrack })
        //  }))

        this.Library = new MusicLibrary() {
          Albums = albums,
          Tracks = tracks,
          Artists = artists,
          RootFolder = rootFolder,
          LastScan = DateTime.Now
        };
      });
    }

    private static bool IsAudioFile(string file) { return AudioExtensions.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase);}
    private static readonly HashSet<string> AudioExtensions = [".mp3", ".flac", ".wav", ".m4a", ".aac", ".ogg", ".wma"];
    private string? FindArtistImage(string artistFolder) {
      string[] candidates =
      {
        "folder.jpg",
        "folder.png",
        "artist.jpg",
        "artist.png"
      };

      foreach (var file in candidates) {
        string path = Path.Combine(artistFolder, file);

        if (File.Exists(path))
          return path;
      }

      return null;
    }
  }
}