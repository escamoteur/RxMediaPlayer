using System;
using System.Threading;

namespace Plugin.RxMediaPlayer.Abstractions
{
    /// <summary>
    /// Interface for RxMediaPlayer
    /// </summary>
    public interface IRxMediaPlayer
    {
        PlayerState  State { get; }
        TimeSpan Position { get; }
        TimeSpan Duration { get; }
        TimeSpan Buffered { get; }

        IObservable<PlayerError> Errors { get; }
        IObservable<PlayerState> PlayerStates { get; }

        IObservable<PlayPosition> Positions { get; }
        IObservable<PlayPosition> BufferStates { get; }

        IObservable<bool> InitPlayer();

        IObservable<bool> ConnectView(IVideoView view);
        IObservable<bool> DisconnectView();

        void SetMediaUrlSource(string url);
        void SetMediaSource(IMediaSource source);

        IObservable<IMediaSource> PrebufferUrlSource(string url, CancellationToken token);

        
        void Play();
        void Pause();     
        void Stop();

        void Seek(TimeSpan position);

        VideoAspectRatio AspectRatio { get; set; }
        IVolumeController Volume { get; }

    }
}
