using AudioPlayer_Net9.ViewModels;

namespace AudioPlayer_Net9.Interfaces;

public interface INavigationService {
  ViewModelBase? CurrentView { get; }
  event Action<ViewModelBase>? ViewChanged;
  void GoBack();
  bool CanGoBack { get; }
  void NavigateTo<TViewModel>(object? parameter = null) where TViewModel : ViewModelBase;
}