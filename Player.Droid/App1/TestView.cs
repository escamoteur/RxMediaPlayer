using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Plugin.RxMediaPlayer.Abstractions;

namespace App1
{
    public class TestView : FrameLayout, IVideoView
    {
        public TestView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
       
        }

        public TestView(Context context) : base(context)
        {
            Init(context);
        }

        public TestView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context);
        }

        public TestView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context);
        }

        public TestView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Init(context);
        }

        private void Init(Context context)
        {
            Button b = new Button(context);
            b.Text = "TestButton";
            AddView(b);
        }
    }
}