using AudioPlayer_Net9.Interfaces;
using Microsoft.Win32;

namespace AudioPlayer_Net9.Services {
  public class FolderPickerService : IFolderPickerService {
    public string? PickFolder() {
      var dialog = new OpenFolderDialog() { Title = "Select Music Folder", Multiselect = false };
      return dialog.ShowDialog() == true ? dialog.FolderName : null;
    }
  }
}
