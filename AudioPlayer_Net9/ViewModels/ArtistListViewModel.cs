using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AudioPlayer_Net9.ViewModels;

public partial class ArtistListViewModel : ViewModelBase, INavigationAware {
  private readonly IMusicLibraryService _musicLibraryService;
  private readonly INavigationService _navigationService;
  public ArtistListViewModel(IMusicLibraryService musicLibraryService, INavigationService navigationService) {
    _musicLibraryService = musicLibraryService;
    _navigationService = navigationService;
    Artists = new ObservableCollection<Artist>(_musicLibraryService.Library.Artists);
    ArtistsView = CollectionViewSource.GetDefaultView(Artists);
    ArtistsView.SortDescriptions.Add(new SortDescription(nameof(Artist.Name),ListSortDirection.Ascending));
    ArtistsView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Artist.GroupLetter)));
  }
  public ObservableCollection<Artist> Artists { get; set; }
  [ObservableProperty] private Artist? selectedArtist;

  partial void OnSelectedArtistChanged(Artist? value) {
    if(value !=null)
      _navigationService.NavigateTo<ArtistDetailViewModel>(value);
  }
  public void OnNavigatedTo(object? parameter) {
    SelectedArtist = null;
  }

  [RelayCommand]
  private void SelectArtist(Artist artist) {
    _navigationService.NavigateTo<ArtistDetailViewModel>(artist);
  }

  public ICollectionView ArtistsView { get; }
}