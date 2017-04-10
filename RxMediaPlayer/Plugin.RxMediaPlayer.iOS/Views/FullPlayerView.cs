using Plugin.RxMediaPlayer.Abstractions;
using AVKit;
using AVFoundation;
using Foundation;
using System;

namespace Plugin.RxMediaPlayer.Views
{
    public class FullPlayerView : AVPlayer​View​Controller, IVideoView
    {
        public FullPlayerView()
        {
        }

        public FullPlayerView(NSCoder coder) : base(coder)
        {
        }

        public FullPlayerView(string nibName, NSBundle bundle) : base(nibName, bundle)
        {
        }

        protected FullPlayerView(NSObjectFlag t) : base(t)
        {
        }

        protected internal FullPlayerView(IntPtr handle) : base(handle)
        {
        }
    }
}