using AudioPlayer_Net9.Interfaces;
using NAudio.Wave;

namespace AudioPlayer_Net9.Services;

public class AudioPlayerServices : IAudioPlayerService, IDisposable {
  private WaveOutEvent? _outputDevice;
  private AudioFileReader? _audioFile;
  public bool IsPlaying => _outputDevice?.PlaybackState == PlaybackState.Playing;
  public bool IsPaused => _outputDevice?.PlaybackState == PlaybackState.Paused;
  public TimeSpan CurrentTime => _audioFile?.CurrentTime ?? TimeSpan.Zero;
  public TimeSpan TotalTime => _audioFile?.TotalTime ?? TimeSpan.Zero;
  public void Play(string filePath) {
    Stop();
    _audioFile = new AudioFileReader(filePath);
    _outputDevice = new WaveOutEvent();
    _outputDevice.Init(_audioFile);
    _outputDevice.Play();
  }
  public void Pause() {
    _outputDevice?.Pause();
  }
  public void Resume() {
    _outputDevice?.Play();
  }
  public void Stop() {
    _outputDevice?.Stop();
    _audioFile?.Dispose();
    _outputDevice?.Dispose();
    _audioFile = null;
    _outputDevice = null;
  }
  public void Dispose() {
    Stop();
  }
}