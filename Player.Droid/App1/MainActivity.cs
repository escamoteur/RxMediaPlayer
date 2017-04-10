using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.RxMediaPlayer;


namespace App1
{
    [Activity(Label = "App1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };




            RxMediaPlayerImplementation Player = new RxMediaPlayerImplementation();
            Player.Init(this);

            Player.InitPlayer();


            FullPlayerView videoView  = FindViewById<FullPlayerView>(Resource.Id.MyVideo);

            Player.ConnectView(videoView);

            Player.SetMediaUrlSource("https://d2fx94pz3d1i3p.cloudfront.net/NUEbfYA4Rk47xX6B.mp4");

 


        }
    }
}

