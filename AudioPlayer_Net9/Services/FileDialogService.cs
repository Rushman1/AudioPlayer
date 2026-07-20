using AudioPlayer_Net9.Interfaces;
using Microsoft.Win32;

namespace AudioPlayer_Net9.Services;

public class FileDialogService : IFileDialogService {
  public string? SelectImageFile() {
    var dialog = new OpenFileDialog();
    dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
    dialog.Multiselect = false;
    return dialog.ShowDialog() == true ? dialog.FileName : null;
  }
}