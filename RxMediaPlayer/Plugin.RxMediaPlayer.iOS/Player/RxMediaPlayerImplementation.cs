using Plugin.RxMediaPlayer.Abstractions;
using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using AVFoundation;
using CoreMedia;
using Foundation;
using Plugin.RxMediaPlayer.Views;
using Plugin.RxMediaPlayer.Views.iOS;


namespace Plugin.RxMediaPlayer
{
  /// <summary>
  /// Implementation for RxMediaPlayer
  /// </summary>
  public class RxMediaPlayerImplementation : NSObject, IRxMediaPlayer
  {
      private IDisposable StateKVOSubscription;


      public PlayerState State { get; }
      public TimeSpan Position { get; }
      public TimeSpan Duration { get; }
      public TimeSpan Buffered { get; }

      public IObservable<PlayerError> Errors => ErrorsSubject;
      public IObservable<PlayerState> PlayerStates { get; private set; }
      public IObservable<PlayPosition> Positions => PositionsSubject;
      public IObservable<PlayPosition> BufferStates => BufferStatesSubject;

      private Subject<PlayerError> ErrorsSubject { get; } = new Subject<PlayerError>();
      private Subject<PlayerState> PlayerStatesSubject { get; } = new Subject<PlayerState>();
      private Subject<PlayPosition> PositionsSubject { get; } = new Subject<PlayPosition>();
      private Subject<PlayPosition> BufferStatesSubject { get; } = new Subject<PlayPosition>();


      private AVPlayer ThePlayer;

      private BehaviorSubject<AVPlayerTimeControlStatus> TimeControlStatus { get; set; }
      private BehaviorSubject<string> ReasonForWaitingToPlay { get; set; }

      public void InitPlayer()
      {
          TimeControlStatus = new BehaviorSubject<AVPlayerTimeControlStatus>(AVPlayerTimeControlStatus.WaitingToPlayAtSpecifiedRate);
          ReasonForWaitingToPlay = new BehaviorSubject<string>("AVPlayerWaitingWithNoItemToPlayReason");

          ThePlayer?.Dispose();
          ThePlayer = new AVPlayer();

          ThePlayer.AddObserver(this, "status", NSKeyValueObservingOptions.New, IntPtr.Zero);
          ThePlayer.AddObserver(this, "error", NSKeyValueObservingOptions.New, IntPtr.Zero);
          ThePlayer.AddObserver(this, "timeControlStatus", NSKeyValueObservingOptions.New, IntPtr.Zero);
          ThePlayer.AddObserver(this, "reasonForWaitingToPlay", NSKeyValueObservingOptions.New, IntPtr.Zero);


            ReasonForWaitingToPlay.Subscribe(status =>
            {
                Debug.WriteLine("Reason: " + status);
            });

            TimeControlStatus.Subscribe(status =>
          {
              Debug.WriteLine("TimeControlStatus: " + status);
          });



            PlayerStates = TimeControlStatus.Zip(ReasonForWaitingToPlay, (status, reason) =>
            {
              switch (status)
              {
                  case AVPlayerTimeControlStatus.Paused:
                      if (reason == "AVPlayerWaitingWithNoItemToPlayReason")
                      {
                          return PlayerState.Idle;
                      }
                      if ((ThePlayer.CurrentTime.Value != 0) && (ThePlayer.CurrentTime.Value == ThePlayer.CurrentItem.Duration.Value))
                      {
                          return PlayerState.Ended;
                      }
                      return PlayerState.Paused;
                  case AVPlayerTimeControlStatus.WaitingToPlayAtSpecifiedRate:
                      if (reason == "AVPlayerWaitingWithNoItemToPlayReason")
                      {
                          return PlayerState.Idle;
                      }
                      return PlayerState.Buffering;
                  case AVPlayerTimeControlStatus.Playing:
                      return PlayerState.Playing;
                  default:
                      throw new ArgumentOutOfRangeException(nameof(status), status, null);
              }
          });

          PlayerStates
          .Subscribe(change =>
            {
                Debug.WriteLine("------------------------Status: " + change.ToString());
            });

        }


      public override void ObserveValue(NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
      {
          if (keyPath == "timeControlStatus")
          {
            foreach (var entry in change)
            {
                if (entry.Key.ToString() == "new")
                {
                    var value = (NSNumber) entry.Value;
                    TimeControlStatus.OnNext((AVPlayerTimeControlStatus) value.Int32Value);
                }
            }
            }
          else if (keyPath == "reasonForWaitingToPlay")
          {
              foreach (var entry in change)
              {
                  if (entry.Key.ToString() == "new")
                  {
                      if ( entry.Value != NSNull.Null)
                      {
                          var value = (NSString)entry.Value;
                          ReasonForWaitingToPlay.OnNext(value);
                      }
                      else
                      {
                          ReasonForWaitingToPlay.OnNext("none");
                      }
                    }
              }


            }
            else if (keyPath == "status")
          {
              
          }
          else if (keyPath == "error")
          {

          }
          else
          {
              base.ObserveValue(keyPath, ofObject, change, context);
          }
      }


   


        public IDisposable ConnectView(IVideoView view)
      {
          var videoView = view as RxVideoView;
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
          if ((ThePlayer.CurrentItem != null) && (position.TotalSeconds <= ThePlayer.CurrentItem.Duration.Seconds))
          {
              ThePlayer.Seek(new CMTime(position.Seconds,10));
          }
        
      }

      public VideoAspectRatio AspectRatio { get; set; }
      public IVolumeController Volume { get; }

    }


    static class KVOHelper
    {
        public static IObservable<NSObservedChange> ObserveKey(this NSObject obj, string key) =>
            Observable.Create<NSObservedChange>(obs => obj.AddObserver(key, NSKeyValueObservingOptions.New, obs.OnNext));
    }
}