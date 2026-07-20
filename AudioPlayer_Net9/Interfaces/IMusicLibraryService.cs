using AudioPlayer_Net9.Models;

namespace AudioPlayer_Net9.Interfaces;

public interface IMusicLibraryService {
  Task<MusicLibrary> LoadLibraryAsync(string rootFolder);
}