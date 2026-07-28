using System.Windows;
using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;
using AudioPlayer_Net9.ViewModels;
using AudioPlayer_Net9.Views;
using Microsoft.Extensions.DependencyInjection;

namespace AudioPlayer_Net9.Services {
  public class MetadataWindowService : IEditMetadataWindowService {
    private readonly IServiceProvider _serviceProvider;
    public MetadataWindowService(IServiceProvider serviceProvider) {
      _serviceProvider = serviceProvider;
    }
    public void Show(Track track) {
      var window = _serviceProvider.GetRequiredService<EditMetadataWindow>();
      window.Owner = Application.Current.MainWindow;
      if (window.DataContext is EditMetadataViewModel vm) {
        vm.Load(track);
      }

      window.ShowDialog();
    }
  }
}