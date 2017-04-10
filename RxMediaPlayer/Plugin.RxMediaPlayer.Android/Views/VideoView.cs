using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Plugin.RxMediaPlayer.Abstractions;

namespace Plugin.RxMediaPlayer
{
    public class RxVideoView : TextureView, IVideoView
    {
        VideoAspectRatio AspectRatio { get; set; }

        public RxVideoView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public RxVideoView(Context context) : base(context)
        {
        }

        public RxVideoView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public RxVideoView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public RxVideoView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }


    }
}