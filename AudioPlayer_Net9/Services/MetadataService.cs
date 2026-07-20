using System.IO;
using System.Windows.Media.Imaging;
using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;
using TagLib;
using File = TagLib.File;

namespace AudioPlayer_Net9.Services {
  public class MetadataService : IMetadataService {
    public Track Load(string filePath) {
      var tagFile = File.Create(filePath);
      var t = new Track() {
        FilePath = filePath,
        Title = tagFile.Tag.Title ?? Path.GetFileNameWithoutExtension(filePath),
        Artist = tagFile.Tag.FirstPerformer ?? string.Empty,
        Album = tagFile.Tag.Album ?? string.Empty,
        Genre = tagFile.Tag.FirstGenre ?? string.Empty,
        Year = tagFile.Tag.Year,
        Duration = tagFile.Properties.Duration,
        TrackNumber = tagFile.Tag.Track
      };
      if (tagFile.Tag.Pictures.Length > 0) {
        t.AlbumArt = ConvertToBitmapImage(tagFile.Tag.Pictures[0].Data.Data);
      }

      return t;
    }
    public void Save(Track track) {
      try {
        var tagFile = File.Create(track.FilePath);
        tagFile.Tag.Title = track.Title;
        tagFile.Tag.Track = track.TrackNumber;
        tagFile.Tag.Performers = new[] { track.Artist };
        tagFile.Tag.Album = track.Album;
        tagFile.Tag.Genres = new[] { track.Genre };
        tagFile.Tag.Year = track.Year;
        if (!String.IsNullOrEmpty(track.AlbumArtPath)) {
          var picture = new Picture(track.AlbumArtPath);
          tagFile.Tag.Pictures = [picture];
        }
        tagFile.Save();
      } catch (Exception ex) {
        Console.WriteLine(ex);
        throw;
      }
    }

    private BitmapImage ConvertToBitmapImage(byte[] imageData) {
      using var stream = new MemoryStream(imageData);
      var image = new BitmapImage();
      image.BeginInit();
      image.StreamSource = stream;
      image.CacheOption = BitmapCacheOption.OnLoad;
      image.EndInit();
      image.Freeze();
      return image;
    }
  }
}
