using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AudioPlayer_Net9.ViewModels;

public partial class AlbumListViewModel : ViewModelBase, INavigationAware {
  private readonly IMusicLibraryService _musicLibraryService;
  private readonly INavigationService _navigationService;
  private ObservableCollection<Album> _albums = new ObservableCollection<Album>();
  public AlbumListViewModel(IMusicLibraryService musicLibraryService, INavigationService navigationService) {
    _musicLibraryService = musicLibraryService;
    _navigationService = navigationService;
    Albums = new ObservableCollection<Album>(_musicLibraryService.Library.Albums);
    AlbumsView = CollectionViewSource.GetDefaultView(Albums);
    AlbumsView.SortDescriptions.Add(new SortDescription(nameof(Album.Name),ListSortDirection.Ascending));
    AlbumsView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Album.GroupLetter)));
  }
  [ObservableProperty] private Album? selectedAlbum;
  public ObservableCollection<Album> Albums { get; set; }

  public void OnNavigatedTo(object? parameter) {
    SelectedAlbum = null;
  }

  [RelayCommand]
  private void SelectAlbum(Album album) {
    _navigationService.NavigateTo<AlbumDetailViewModel>(album);
  }

  public ICollectionView AlbumsView { get; }

  partial void OnSelectedAlbumChanged(Album? value) {
    if (value != null)
      _navigationService.NavigateTo<TrackListViewModel>(value);
  }
}