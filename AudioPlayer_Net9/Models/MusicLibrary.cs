using System.Collections.ObjectModel;

namespace AudioPlayer_Net9.Models;

public class MusicLibrary {
  public string? RootFolder { get; set; }
  public DateTime LastScan { get; set; }
  public IReadOnlyList<Artist> Artists { get; set; } = [];
  public IReadOnlyList<Album> Albums { get; set; } = [];
  public IReadOnlyList<Track> Tracks { get; set; } = [];

}