
using AVFoundation;
using Plugin.RxMediaPlayer.Abstractions;
using Plugin.RxMediaPlayer.Views.iOS;

namespace Plugin.RxMediaPlayer.Views
{
    public class PlayerViewConnection : IPlayerViewConnection
    {
        public void Dispose()
        {
            Player.Pause();
            (View as RxVideoView)?.Disconnect();
        }

        public AVPlayer Player { get; set; }
        public IVideoView View { get; set; }
    }
}