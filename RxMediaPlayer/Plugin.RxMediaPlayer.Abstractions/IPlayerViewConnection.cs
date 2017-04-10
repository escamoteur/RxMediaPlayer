using System;

namespace Plugin.RxMediaPlayer.Abstractions
{
    public interface IPlayerViewConnection : IDisposable
    {
         IVideoView View { get; set; }
    }
}