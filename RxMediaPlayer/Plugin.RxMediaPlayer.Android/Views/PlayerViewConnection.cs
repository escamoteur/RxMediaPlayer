using Com.Google.Android.Exoplayer2;
using Plugin.RxMediaPlayer.Abstractions;

namespace Plugin.RxMediaPlayer.Views
{
    public class PlayerViewConnection : IPlayerViewConnection
    {
        public void Dispose()
        {
           Player.ClearVideoSurface();
        }

        public SimpleExoPlayer Player { get; set; }
        public IVideoView View { get; set; }
    }
}