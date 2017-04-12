using Plugin.RxMediaPlayer.Abstractions;
using System;
using System.Reactive.Subjects;
using System.Runtime.Remoting.Messaging;
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
using Plugin.RxMediaPlayer.Droid;
using Plugin.RxMediaPlayer.Views;
using IMediaSource = Plugin.RxMediaPlayer.Abstractions.IMediaSource;
using Object = Java.Lang.Object;


namespace Plugin.RxMediaPlayer
{
    /// <summary>
    /// Implementation for Feature
    /// </summary>
    public class RxMediaPlayerImplementation : Java.Lang.Object, IRxMediaPlayer, IExoPlayerEventListener
    {
        private SimpleExoPlayer TheExoPlayer;
        private Context _context;
        private DefaultLoadControl _defaultLoadControl;
        private DefaultBandwidthMeter _defaultBandwidthMeter;
        private AdaptiveVideoTrackSelection.Factory _adaptiveVideoTrackSelectionFactory;
        private TrackSelector _trackSelector;
        private Com.Google.Android.Exoplayer2.Source.IMediaSource _videoSource;
        public PlayerState State { get;  set; }
        public TimeSpan Position { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan Buffered { get; set; }

        public IObservable<PlayerError> Errors => ErrorsSubject;
        public IObservable<PlayerState> PlayerStates => PlayerStatesSubject;
        public IObservable<PlayPosition> Positions => PositionsSubject;
        public IObservable<PlayPosition> BufferStates => BufferStatesSubject;

        private Subject<PlayerError> ErrorsSubject { get; } = new Subject<PlayerError>();
        private BehaviorSubject<PlayerState> PlayerStatesSubject { get;  set; }
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

            PlayerStatesSubject = new BehaviorSubject<PlayerState>(PlayerState.Idle);

        }


        public IDisposable ConnectView(IVideoView view)
        {
            if (view is RxVideoView)
            {
                TheExoPlayer.SetVideoTextureView((RxVideoView) view);
            }
            else
            {
                (view as RxFullPlayerView)?.SetPlayer(TheExoPlayer);
            }
            return new PlayerViewConnection() {Player = TheExoPlayer, View = view};
        }


        public void SetMediaUrlSource(string url)
        {
            // Produces DataSource instances through which media data is loaded.
            var dataSourceFactory = new DefaultDataSourceFactory(_context, Util.GetUserAgent(_context, "ExoPlayerTest"),
                _defaultBandwidthMeter);
            // Produces Extractor instances for parsing the media data.
            var extractorsFactory = new DefaultExtractorsFactory();

            // This is the MediaSource representing the media to be played.
            var uri = Android.Net.Uri.Parse(url);
            _videoSource = new ExtractorMediaSource(uri, dataSourceFactory, extractorsFactory, null, null);
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
            if (State == PlayerState.Idle)
            {
                TheExoPlayer.Prepare(_videoSource,false,false);
//                TheExoPlayer.Prepare(_videoSource);
            }
            TheExoPlayer.PlayWhenReady = true;
        }

        public void Pause()
        {
            TheExoPlayer.PlayWhenReady = false;
        }

        public void Stop()
        {
            TheExoPlayer.Stop();
        }

        public void Seek(TimeSpan position)
        {
            var duration =  TheExoPlayer.Duration;
            
            if ((duration != -9223372036854775807) && (position.TotalMilliseconds <= duration))
            {
                TheExoPlayer.SeekTo((long) position.TotalMilliseconds);
            }
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

        public void OnPlayerStateChanged(bool playReady, int exoState)
        {
            State = ConvertStates(exoState, playReady);
            PlayerStatesSubject.OnNext(State);

            Log.Debug("RxVideoPlayer", "StateChanged " + exoState);
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


        private PlayerState ConvertStates(int exoState, bool playReady)
        {
            switch (exoState)
            {
                case 1: return PlayerState.Idle;
                case 2: return PlayerState.Buffering;
                case 3:
                    if (playReady)
                    {
                        return PlayerState.Playing;
                    }
                    else
                    {
                        return PlayerState.Paused;
                    }
                case 4: return PlayerState.Ended;

                default:
                {
                    throw new System.IndexOutOfRangeException("Unknown Exoplayer State");
                }
            }
        }
    }
}