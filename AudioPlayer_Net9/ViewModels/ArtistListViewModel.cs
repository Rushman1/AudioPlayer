using System.Collections.ObjectModel;
using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AudioPlayer_Net9.ViewModels;

public class ArtistListViewModel : ViewModelBase {
  private readonly IMusicLibraryService _musicLibraryService;
  public ArtistListViewModel(IMusicLibraryService musicLibraryService) {
    _musicLibraryService = musicLibraryService;
    Artists = new ObservableCollection<Artist>(_musicLibraryService.Library.Artists);
  }
  public ObservableCollection<Artist> Artists { get; set; }
}