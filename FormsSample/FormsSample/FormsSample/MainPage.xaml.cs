using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.RxMediaPlayer;
using Xamarin.Forms;

namespace FormsSample
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

        }

        protected override void OnAppearing()
	    {
	        CrossRxMediaPlayer.Current.ConnectView(MyVideoView.VideoView);
	        CrossRxMediaPlayer.Current.SetMediaUrlSource("https://d2fx94pz3d1i3p.cloudfront.net/NUEbfYA4Rk47xX6B.mp4");

            base.OnAppearing();
	    }
	}
}
