using System;
using AVFoundation;
using Foundation;
using Plugin.RxMediaPlayer.Abstractions;

namespace Plugin.RxMediaPlayer.Views
{
    public class VideoView : AVPlayerLayer, IVideoView
    {
        public VideoView()
        {
        }

        public VideoView(NSCoder coder) : base(coder)
        {
        }

        protected VideoView(NSObjectFlag t) : base(t)
        {
        }

        protected internal VideoView(IntPtr handle) : base(handle)
        {
        }
    }
}