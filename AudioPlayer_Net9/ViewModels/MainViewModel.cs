using System.Collections.ObjectModel;
using System.Diagnostics;
using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AudioPlayer_Net9.ViewModels {
  public partial class MainViewModel : ObservableObject {
    private readonly IDialogService _dialogService;
    private readonly IFolderPickerService _folderPickerService;
    private readonly IMusicLibraryService _musicLibraryService;
    [ObservableProperty]private MusicLibrary? _library;
    [ObservableProperty] private bool _isLoading = false;
    private LibraryView _currentView = LibraryView.Artists;
    private Artist _selectedArtist;
    private Album _selectedAlbum;
    private IEnumerable<Track> _tracks;
    public MainViewModel(IDialogService dialogService, IFolderPickerService folderPickerService, IMusicLibraryService musicLibraryService) {
      _dialogService = dialogService;
      _folderPickerService = folderPickerService;
      _musicLibraryService = musicLibraryService;
    }
    [RelayCommand]
    private void EditMetadata(Track track) {
      _dialogService.ShowMetadataDialog(track);
    }

    public Track SelectedTrack { get; set; }
    public IEnumerable<Track> Tracks => SelectedAlbum?.Tracks??Enumerable.Empty<Track>();
    public IEnumerable<Album> Albums => SelectedArtist?.Albums ?? Enumerable.Empty<Album>();
    public bool IsBackButtonEnabled => CurrentView == LibraryView.Albums || CurrentView == LibraryView.Tracks;
    public bool IsArtistsView => CurrentView == LibraryView.Artists;
    public bool IsAlbumsView => CurrentView == LibraryView.Albums;
    public bool IsTracksView => CurrentView == LibraryView.Tracks;
    public Artist SelectedArtist {
      get => _selectedArtist;
      set {
        if (SetProperty(ref _selectedArtist, value)) {
          OnPropertyChanged(nameof(Albums));
          CurrentView = LibraryView.Albums;
        }
      }
    }
    public Album SelectedAlbum {
      get => _selectedAlbum;
      set {
        if (Equals(value, _selectedAlbum)) return;
        _selectedAlbum = value;
        OnPropertyChanged();
        CurrentView = LibraryView.Tracks;
      }
    }
    public LibraryView CurrentView {
      get => _currentView;
      set {
        _currentView = value;
        OnPropertyChanged();
        OnPropertyChanged(nameof(IsArtistsView));
        OnPropertyChanged(nameof(IsAlbumsView));
        OnPropertyChanged(nameof(IsTracksView));
      }
    }

    [RelayCommand]
    private async Task OpenLibrary() {
      try {
        var sw = Stopwatch.StartNew();
        IsLoading = true;
        string? folder = _folderPickerService.PickFolder();
        Debug.WriteLine($"Folder Picker: {sw.Elapsed}");
        if(String.IsNullOrEmpty(folder))return;

        Library = await _musicLibraryService.LoadLibraryAsync(folder);
        Debug.WriteLine($"Library Loaded: {sw.Elapsed}");
        Debug.WriteLine($"Artists: {Library.Artists.Count}");
        Debug.WriteLine($"Albums: {Library.Albums.Count}");
        Debug.WriteLine($"Tracks: {Library.Tracks.Count}");
      } catch (Exception e) {
        IsLoading = false;
        Console.WriteLine(e);
        throw;
      } finally {
        IsLoading = false;
      }
    }

    [RelayCommand]
    private void GoBack() {
      switch (CurrentView) {
        case LibraryView.Tracks:
          SelectedAlbum = null;
          CurrentView = LibraryView.Albums;
          break;
        case LibraryView.Albums:
          SelectedArtist = null;
          CurrentView = LibraryView.Artists;
          break;
      }
    }
  }
}
