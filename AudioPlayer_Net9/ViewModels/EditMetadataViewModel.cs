using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;
using AudioPlayer_Net9.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Media.Imaging;

namespace AudioPlayer_Net9.ViewModels {
  public partial class EditMetadataViewModel : ViewModelBase {
    [ObservableProperty] private Track selectedTrack;
    private readonly IMetadataService _metadataService;
    private readonly IFileDialogService _fileDialogService;
    [ObservableProperty] private string title = string.Empty;
    [ObservableProperty] private string artist = string.Empty;
    [ObservableProperty] private string album = string.Empty;
    [ObservableProperty] private string genre = string.Empty;
    [ObservableProperty] private uint year;
    [ObservableProperty] private uint trackNumber;
    //[ObservableProperty] private string genre = string.Empty;

    private BitmapImage _albumArt;
    public BitmapImage? AlbumArt {
      get => _albumArt;
      set {
        if (Equals(value, _albumArt)) return;
        _albumArt = value;
        OnPropertyChanged();
      }
    }
    public string? AlbumArtPath { get; set; } = String.Empty;
    
    public EditMetadataViewModel(IMetadataService metadataService, IFileDialogService fileDialogService) {
      _metadataService = metadataService;
      _fileDialogService = fileDialogService;
    }

    public void Load(Track track) {
      SelectedTrack = track;
      Title = track.Title;
      Artist = track.Artist;
      Album = track.Album;
      Genre = track.Genre;
      Year = track.Year;
      TrackNumber = track.TrackNumber;
      AlbumArt = track.AlbumArt;
    }

    [RelayCommand]
    private void ChangeCover() {
      var imagePath = _fileDialogService.SelectImageFile();
      if (String.IsNullOrWhiteSpace(imagePath))
        return;

      AlbumArtPath = imagePath;
      AlbumArt = new BitmapImage(new Uri(imagePath));
    }

    [RelayCommand]
    private void Save() {
      SelectedTrack.Title = Title;
      SelectedTrack.Artist = Artist;
      SelectedTrack.Album = Album;
      SelectedTrack.Genre = Genre;
      SelectedTrack.Year = Year;
      SelectedTrack.TrackNumber = TrackNumber;
      SelectedTrack.AlbumArt = AlbumArt;
      SelectedTrack.AlbumArtPath = AlbumArtPath??String.Empty;
      _metadataService.Save(SelectedTrack);
      RequestClose?.Invoke(true);
    }

    [RelayCommand]
    private void Cancel() {
      RequestClose?.Invoke(false);
    }

    public event Action<bool?>? RequestClose;
  }
}
