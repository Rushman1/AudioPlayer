namespace AudioPlayer_Net9.Models;

public class NavigationEntry {
  public Type ViewModelType { get; set; } = default;
  public object? Parameter { get; set; }
}