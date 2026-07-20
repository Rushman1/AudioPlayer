using System.Windows;
using AudioPlayer_Net9.ViewModels;

namespace AudioPlayer_Net9.Views {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    public MainWindow(MainViewModel vm) {
      InitializeComponent();
      DataContext = vm;
    }
  }
}