using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.UI;
using Plugin.RxMediaPlayer.Abstractions;

namespace Plugin.RxMediaPlayer
{
  
    public class FullPlayerViewAndroid : FrameLayout, IVideoView
    {
        public FullPlayerViewAndroid(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public FullPlayerViewAndroid(Context context) : base(context)
        {
            Init(context);
        }

        public FullPlayerViewAndroid(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context);
        }

        public FullPlayerViewAndroid(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context);
        }

        public FullPlayerViewAndroid(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Init(context);
        }


        private void Init(Context context)
        {
            PlayerView = new SimpleExoPlayerView(context);
            this.AddView(PlayerView);
        }

        public SimpleExoPlayerView PlayerView { get; set; }

        public void SetPlayer(SimpleExoPlayer player)
        {
            PlayerView.Player = player;
        }


    }
}