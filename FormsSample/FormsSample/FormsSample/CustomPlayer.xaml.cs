using System;
using System.Reactive.Linq;
using Plugin.RxMediaPlayer;
using Plugin.RxMediaPlayer.Abstractions;
using Xamarin.Forms;

namespace FormsSample
{
    public partial class CustomPlayer : ContentPage
    {
        public CustomPlayer()
        {
            InitializeComponent();

        }

        public IDisposable ViewConnection { get; set; }

        protected override void OnAppearing()
        {
            CrossRxMediaPlayer.Current.SetMediaUrlSource("https://d2fx94pz3d1i3p.cloudfront.net/NUEbfYA4Rk47xX6B.mp4");

            CrossRxMediaPlayer.Current.PlayerStates.Subscribe(state =>
            {
                switch (state)
                {
                    case PlayerState.Idle:
                        PlayPause.Text = "Play";
                        break;
                    case PlayerState.Ended:
                        PlayPause.Text = "Play";
                        break;
                    case PlayerState.Paused:
                        PlayPause.Text = "Play";
                        break;
                    case PlayerState.Playing:
                        PlayPause.Text = "Pause";
                        break;
                    case PlayerState.Buffering:
                        break;
                    case PlayerState.Failed:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(state), state, null);
                }
             });

            base.OnAppearing();
        }

        private void PlayPause_OnClicked(object sender, EventArgs e)
        {
            var state = CrossRxMediaPlayer.Current.PlayerStates.Take(1).Wait();

            switch (state)
            {

                case PlayerState.Idle:
                    CrossRxMediaPlayer.Current.Play();
                    break;
                case PlayerState.Ended:
                    CrossRxMediaPlayer.Current.Seek(TimeSpan.FromSeconds(0));
                    CrossRxMediaPlayer.Current.Play();
                    break;
                case PlayerState.Paused:
                    CrossRxMediaPlayer.Current.Play();
                    break;
                case PlayerState.Playing:
                    CrossRxMediaPlayer.Current.Pause();
                    break;
                case PlayerState.Buffering:
                    break;
                case PlayerState.Failed:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnConnect1(object sender, EventArgs e)
        {
            ViewConnection?.Dispose();
            ViewConnection = CrossRxMediaPlayer.Current.ConnectView(MyVideoView.VideoView);
        }

        private void OnConnect2(object sender, EventArgs e)
        {
            CrossRxMediaPlayer.Current.Stop();

            ViewConnection?.Dispose();
            ViewConnection = CrossRxMediaPlayer.Current.ConnectView(MyVideoView2.VideoView);
        }
    }
}