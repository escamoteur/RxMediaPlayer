using Plugin.RxMediaPlayer.Abstractions;
using System;
using System.Reactive.Subjects;
using System.Threading;
using Android.Content;
using Android.OS;
using Android.Util;
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.Extractor;
using Com.Google.Android.Exoplayer2.Source;
using Com.Google.Android.Exoplayer2.Trackselection;
using Com.Google.Android.Exoplayer2.Upstream;
using Com.Google.Android.Exoplayer2.Util;
using Plugin.RxMediaPlayer.Views;
using IMediaSource = Plugin.RxMediaPlayer.Abstractions.IMediaSource;
using Object = Java.Lang.Object;


namespace Plugin.RxMediaPlayer
{
  /// <summary>
  /// Implementation for Feature
  /// </summary>
  public class RxMediaPlayerImplementation : Java.Lang.Object,  IRxMediaPlayer, IExoPlayerEventListener
    {
      private SimpleExoPlayer TheExoPlayer;
      private Context _context;
      private DefaultLoadControl _defaultLoadControl;
      private DefaultBandwidthMeter _defaultBandwidthMeter;
      private AdaptiveVideoTrackSelection.Factory _adaptiveVideoTrackSelectionFactory;
      private TrackSelector _trackSelector;
        private Com.Google.Android.Exoplayer2.Source.IMediaSource _videoSource;
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
          _context = CrossRxMediaPlayer.AppContext;

            // 1. Create a default TrackSelector
            Handler mainHandler = new Handler();
          _defaultBandwidthMeter = new DefaultBandwidthMeter();
          _adaptiveVideoTrackSelectionFactory = new AdaptiveVideoTrackSelection.Factory(_defaultBandwidthMeter);
          _trackSelector = new DefaultTrackSelector(_adaptiveVideoTrackSelectionFactory);

          // 2. Create a default LoadControl
          _defaultLoadControl = new DefaultLoadControl();

          // 3. Create the player
          TheExoPlayer = ExoPlayerFactory.NewSimpleInstance(_context, _trackSelector, _defaultLoadControl);

          TheExoPlayer.AddListener(this);
      }




        public IDisposable ConnectView(IVideoView view)
      {
          if (view is RxVideoView)
          {
              TheExoPlayer.SetVideoTextureView((RxVideoView)view);
          }
          else
          {
              (view as FullPlayerViewAndroid)?.SetPlayer(TheExoPlayer);
          }
          return new PlayerViewConnection() { Player = TheExoPlayer, View = view };
      }



      public void SetMediaUrlSource(string url)
      {
            // Produces DataSource instances through which media data is loaded.
            var dataSourceFactory = new DefaultDataSourceFactory(_context, Util.GetUserAgent(_context, "ExoPlayerTest"), _defaultBandwidthMeter);
            // Produces Extractor instances for parsing the media data.
            var extractorsFactory = new DefaultExtractorsFactory();

            // This is the MediaSource representing the media to be played.
            var uri = Android.Net.Uri.Parse(url);
            _videoSource = new ExtractorMediaSource(uri, dataSourceFactory, extractorsFactory, null, null);
            TheExoPlayer.Prepare(_videoSource);
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
          TheExoPlayer.PlayWhenReady = true;
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



// Event Listener

        public void OnLoadingChanged(bool p0)
        {
            Log.Debug("RxVideoPlayer", "OnLoadingChanged " + p0);
        }

        public void OnPlayerError(ExoPlaybackException p0)
        {
            Log.Debug("RxVideoPlayer", "PlayerException " + p0.Type);
        }

        public void OnPlayerStateChanged(bool p0, int p1)
        {
            Log.Debug("RxVideoPlayer", "StateChanged " + p1);
        }

        public void OnPositionDiscontinuity()
        {
            Log.Debug("RxVideoPlayer", "OnPositionDiscontinuity ");
        }

        public void OnTimelineChanged(Timeline p0, Object p1)
        {
            Log.Debug("RxVideoPlayer", "Timeline changed: " + p0.PeriodCount);
        }

        public void OnTracksChanged(TrackGroupArray p0, TrackSelectionArray p1)
        {
            Log.Debug("RxVideoPlayer", "Trackchanged");
        }
    }
}