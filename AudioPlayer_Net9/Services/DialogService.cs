using AudioPlayer_Net9.Interfaces;
using AudioPlayer_Net9.Models;
using AudioPlayer_Net9.ViewModels;
using AudioPlayer_Net9.Views;
using Microsoft.Extensions.DependencyInjection;

namespace AudioPlayer_Net9.Services {
  public class DialogService : IDialogService {
    private readonly IServiceProvider _serviceProvider;
    public DialogService(IServiceProvider serviceProvider) {
      _serviceProvider = serviceProvider;
    }
    public bool? ShowMetadataDialog(Track track) {
      var vm = ActivatorUtilities.CreateInstance<EditMetadataViewModel>(_serviceProvider, track);
      var window = new EditMetadataWindow(vm);
      return window.ShowDialog();
    }
  }
}
