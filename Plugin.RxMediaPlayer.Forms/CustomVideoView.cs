using Plugin.RxMediaPlayer.Abstractions;
using Xamarin.Forms;

namespace Plugin.RxMediaPlayer.Forms
{
    public class CustomVideoView  : View
    {
        public IVideoView VideoView { get; set; }
        
    }
}