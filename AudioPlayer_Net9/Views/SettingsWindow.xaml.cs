using System.Windows;
using AudioPlayer_Net9.ViewModels;

namespace AudioPlayer_Net9.Views {
  public partial class SettingsWindow : Window {
    public SettingsWindow(SettingsViewModel vm) {
      InitializeComponent();
      DataContext = vm;
    }
  }
}
