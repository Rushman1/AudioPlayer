using System.Windows;
using AudioPlayer_Net9.ViewModels;

namespace AudioPlayer_Net9.Views;

public partial class EditMetadataWindow : Window {
  public EditMetadataWindow(EditMetadataViewModel vm) {
    InitializeComponent();
    DataContext = vm;

    vm.RequestClose += result => {
      DialogResult = result;
      Close();
    };
  }
}