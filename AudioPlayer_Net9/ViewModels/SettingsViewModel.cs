using AudioPlayer_Net9.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AudioPlayer_Net9.ViewModels;

public partial class SettingsViewModel : ViewModelBase {
  private readonly IFolderPickerService _folderPickerService;
  private readonly ISettingsService _settingsService;
  private string _settingsPath;
  public SettingsViewModel(IFolderPickerService folderPickerService, ISettingsService settingsService) {
    _folderPickerService = folderPickerService;
    _settingsService = settingsService;
  }
  public string SettingsPath {
    get => _settingsPath;
    set {
      _settingsPath = value;
      OnPropertyChanged();
    }
  }
  [RelayCommand]
  private async Task OpenFolderPicker() {
    string? folder = _folderPickerService.PickFolder();
    if (String.IsNullOrEmpty(folder)) return;
    _settingsService.Settings.MusicFolder = folder;
  }
}