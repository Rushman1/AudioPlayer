using System.Windows;
using AudioPlayer_Net9.ViewModels;
using MahApps.Metro.Controls;

namespace AudioPlayer_Net9.Views {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : MetroWindow {
    public MainWindow(MainViewModel vm) {
      InitializeComponent();
      DataContext = vm;
    }
    private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e) {
      if (ActualWidth < 800) {
        SidebarColumn.Width = new GridLength(0);
        //LibraryText.Visibility = Visibility.Collapsed;
        /*
         * AlbumText.Visibility = Visibility.Collapsed;
         * ArtistText.Visibility = Visibility.Collapsed;
         * Playlists.Visibility = Visibility.Collapsed;
         * Favorites.Visibility = Visibility.Collapsed;
         * SettingsText.Visibility = Visibility.Collapsed;
         *
         */
      } else if (ActualWidth < 1200) {
        SidebarColumn.Width = new GridLength(60);
        //LibraryText.Visibility = Visibility.Collapsed;
        /*
         * AlbumText.Visibility = Visibility.Collapsed;
         * ArtistText.Visibility = Visibility.Collapsed;
         * Playlists.Visibility = Visibility.Collapsed;
         * Favorites.Visibility = Visibility.Collapsed;
         * SettingsText.Visibility = Visibility.Collapsed;
         *
         */
      } else {
        SidebarColumn.Width = new GridLength(200);
        //LibraryText.Visibility = Visibility.Visible;
        /*
         * AlbumText.Visibility = Visibility.Visible;
         * ArtistText.Visibility = Visibility.Visible;
         * Playlists.Visibility = Visibility.Visible;
         * Favorites.Visibility = Visibility.Visible;
         * SettingsText.Visibility = Visibility.Visible;
         *
         */

      }
    }
  }
}