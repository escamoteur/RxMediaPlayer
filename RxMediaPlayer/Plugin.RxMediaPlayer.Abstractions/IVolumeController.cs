using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.RxMediaPlayer.Abstractions
{
    public interface IVolumeController
    {

        IObservable<IVolume> VolumeChanges { get; }
        /// <summary>
        /// The volume for the current MediaPlayer
        /// </summary>
        IVolume CurrentVolume { get; set; }

        /// <summary>
        /// The Maximum volume that can be used
        /// </summary>
        IVolume MaxVolume { get; set; }

        /// <summary>
        /// True if the sound is Muted
        /// </summary>
        bool Mute { get; set; }

    }
}
