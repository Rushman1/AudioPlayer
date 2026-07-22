using System.IO;
using System.Text.Json;
using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;

namespace AudioPlayer_Net9.Services;

public class SettingsService : ISettingsService {
  private readonly string _settingsFile;
  public AppSettings Settings { get; private set; } = new AppSettings();

  public SettingsService() {
    _settingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AudioPlayer", "settings.json");
  }
  public async Task LoadAsync() {
    if(!File.Exists(_settingsFile))return;
    var json = await File.ReadAllTextAsync(_settingsFile);
    Settings = JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
  }
  public async Task SaveAsync() {
    Directory.CreateDirectory(Path.GetDirectoryName(_settingsFile)!);
    var json = JsonSerializer.Serialize(Settings, new JsonSerializerOptions() { WriteIndented = true });
    await File.WriteAllTextAsync(_settingsFile, json);
  }
}