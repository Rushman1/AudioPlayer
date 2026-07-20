namespace AudioPlayer_Net9.Interfaces;

public interface IAudioPlayerService {
  void Play(string filePath);
  void Pause();
  void Resume();
  void Stop();
  bool IsPaused { get; }
  bool IsPlaying { get; }

  TimeSpan CurrentTime { get; }
  TimeSpan TotalTime { get; }
}