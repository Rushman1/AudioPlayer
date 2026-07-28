using System.Collections.ObjectModel;
using System.Reflection.PortableExecutable;
using System.Windows.Media.Imaging;

namespace AudioPlayer_Net9.Models {
  public class Album {
    public string Name { get; set; }
    public ObservableCollection<Track> Tracks { get; set; }
    public BitmapImage? AlbumArt { get; set; }
    public uint? Year { get; set; }
    public string Artist { get; set; } = String.Empty;
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
