using System;
using Plugin.RxMediaPlayer.Abstractions;

namespace Plugin.RxMediaPlayer.Player
{
    public class VolumeController : IVolumeController
    {
        public IObservable<IVolume> VolumeChanges { get; }
        public IVolume CurrentVolume { get; set; }
        public IVolume MaxVolume { get; set; }
        public bool Mute { get; set; }
    }
}