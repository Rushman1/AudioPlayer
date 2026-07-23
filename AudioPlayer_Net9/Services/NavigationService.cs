using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;
using AudioPlayer_Net9.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace AudioPlayer_Net9.Services;

public class NavigationService : INavigationService {
  private readonly IServiceProvider _serviceProvider;
  private readonly Stack<NavigationEntry> _history = new();
  private NavigationEntry? _currentEntry;
  public ViewModelBase? CurrentView { get; private set; }
  public event Action<ViewModelBase>? ViewChanged;
  public bool CanGoBack => _history.Count > 0;
  public NavigationService(IServiceProvider serviceProvider) {
    _serviceProvider = serviceProvider;
  }
  public void NavigateTo<TViewModel>(object? parameter = null) where TViewModel : ViewModelBase {

    if (_currentEntry != null) {
      _history.Push(_currentEntry);
    }

    _currentEntry = new NavigationEntry() { ViewModelType = typeof(TViewModel), Parameter = parameter };
    ShowView(_currentEntry);
  }

  public void GoBack() {
    if(!CanGoBack)return;
    _currentEntry = _history.Pop();
    ShowView(_currentEntry);
  }

  private void ShowView(NavigationEntry entry) {
    var vm = (ViewModelBase)
      _serviceProvider.GetRequiredService(entry.ViewModelType);

    if (vm is INavigationAware nav) {
      nav.OnNavigatedTo(entry.Parameter);
    }

    CurrentView = vm;

    ViewChanged?.Invoke(vm);
  }
}