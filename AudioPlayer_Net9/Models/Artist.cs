using System.Collections.ObjectModel;
using System.Reflection.PortableExecutable;

namespace AudioPlayer_Net9.Models {
  public class Artist {
    public string Name { get; set; }
    public ObservableCollection<Album> Albums { get; set; }  = new ObservableCollection<Album>();
    public ObservableCollection<Track> Tracks { get; } = new ObservableCollection<Track>();
    public int TrackCount { get; set; }
    public string? ImagePath { get; set; }
    /*
     * Future: Current and past members
     * Future: Artist image(s)
     */
    public string GroupLetter {
      get {
        if (String.IsNullOrWhiteSpace(Name))
          return "#";

        char c = char.ToUpper(Name[0]);

        return char.IsLetter(c) ? c.ToString() : "#";
      }
    }

  }
}
