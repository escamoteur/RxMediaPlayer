using Plugin.RxMediaPlayer.Abstractions;
using System;
using System.Reactive.Subjects;
using System.Threading;
using AVFoundation;
using Foundation;
using Plugin.RxMediaPlayer.Views;


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


      private AVPlayer ThePlayer;


      public void InitPlayer()
      {
          ThePlayer?.Dispose();
          ThePlayer = new AVPlayer();
      }

      public IDisposable ConnectView(IVideoView view)
      {
          var videoView = view as RXVideoView;
          videoView.SetPlayer(ThePlayer);
          return new PlayerViewConnection(){Player = ThePlayer,View = view};
      }

      public void SetMediaUrlSource(string url)
      {
          ThePlayer.ReplaceCurrentItemWithPlayerItem(new AVPlayerItem(NSUrl.FromString(url)));
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
          ThePlayer.Play();
      }

      public void Pause()
      {
         ThePlayer.Pause();
      }

      public void Stop()
      {
          ThePlayer.Pause();
      }

      public void Seek(TimeSpan position)
      {
          throw new NotImplementedException();
      }

      public VideoAspectRatio AspectRatio { get; set; }
      public IVolumeController Volume { get; }
  }
}