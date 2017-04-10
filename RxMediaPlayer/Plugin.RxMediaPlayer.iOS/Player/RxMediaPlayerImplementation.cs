using Plugin.RxMediaPlayer.Abstractions;
using System;
using System.Reactive.Subjects;
using System.Threading;


namespace Plugin.RxMediaPlayer
{
  /// <summary>
  /// Implementation for RxMediaPlayer
  /// </summary>
  public class RxMediaPlayerImplementation : IRxMediaPlayer
  {
      public PlayerState State { get; }
      public TimeSpan Position { get; }
      public TimeSpan Duration { get; }
      public TimeSpan Buffered { get; }

      public IObservable<PlayerError> Errors => ErrorsSubject;
      public IObservable<PlayerState> PlayerStates => PlayerStatesSubject;
      public IObservable<PlayPosition> Positions => PositionsSubject;
      public IObservable<PlayPosition> BufferStates => BufferStatesSubject;

      private Subject<PlayerError> ErrorsSubject { get; } = new Subject<PlayerError>();
      private Subject<PlayerState> PlayerStatesSubject { get; } = new Subject<PlayerState>();
      private Subject<PlayPosition> PositionsSubject { get; } = new Subject<PlayPosition>();
      private Subject<PlayPosition> BufferStatesSubject { get; } = new Subject<PlayPosition>();



        public void InitPlayer()
      {
          throw new NotImplementedException();
      }

      public IDisposable ConnectView(IVideoView view)
      {
          throw new NotImplementedException();
      }

      public void SetMediaUrlSource(string url)
      {
          throw new NotImplementedException();
      }

      public void SetMediaSource(IMediaSource source)
      {
          throw new NotImplementedException();
      }

      public IObservable<IMediaSource> PrebufferUrlSource(string url, CancellationToken token)
      {
          throw new NotImplementedException();
      }

      public void Play()
      {
          throw new NotImplementedException();
      }

      public void Pause()
      {
          throw new NotImplementedException();
      }

      public void Stop()
      {
          throw new NotImplementedException();
      }

      public void Seek(TimeSpan position)
      {
          throw new NotImplementedException();
      }

      public VideoAspectRatio AspectRatio { get; set; }
      public IVolumeController Volume { get; }
  }
}