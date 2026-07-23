using System.Collections.ObjectModel;
using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AudioPlayer_Net9.ViewModels;

public partial class AlbumListViewModel : ViewModelBase, INavigationAware {
  private readonly IMusicLibraryService _musicLibraryService;
  private readonly INavigationService _navigationService;
  private ObservableCollection<Album> _albums = new ObservableCollection<Album>();
  public AlbumListViewModel(IMusicLibraryService musicLibraryService, INavigationService navigationService) {
    _musicLibraryService = musicLibraryService;
    _navigationService = navigationService;
  }
  public void OnNavigatedTo(object? parameter) {
    Albums.Clear();
    if (parameter is Artist artist) {
      foreach (var album in artist.Albums) {
        Albums.Add(album);
      }
    } else {
      foreach (var album in _musicLibraryService.Library.Albums) {
        Albums.Add(album);
      }
    }
  }
  [ObservableProperty] private Album? selectedAlbum;
  public ObservableCollection<Album> Albums {
    get => _albums;
    set => SetProperty(ref _albums, value);
  }

  partial void OnSelectedAlbumChanged(Album? value) {
    if(value!=null)
      _navigationService.NavigateTo<TrackListViewModel>(value);
  }
}