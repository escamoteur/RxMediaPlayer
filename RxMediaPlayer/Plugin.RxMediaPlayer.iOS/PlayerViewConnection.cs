
using AVFoundation;
using Plugin.RxMediaPlayer.Abstractions;

namespace Plugin.RxMediaPlayer.Views
{
    public class PlayerViewConnection : IPlayerViewConnection
    {
        public void Dispose()
        {
            Player.Pause();
            (View as RXVideoView)?.Disconnect();
        }

        public AVPlayer Player { get; set; }
        public IVideoView View { get; set; }
    }
}