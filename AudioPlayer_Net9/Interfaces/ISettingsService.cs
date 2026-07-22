using AudioPlayer_Net9.Models;

namespace AudioPlayer_Net9.Interfaces;

public interface ISettingsService {
  AppSettings Settings { get; }
  Task LoadAsync();
  Task SaveAsync();
}