

using System.ComponentModel;
using Plugin.RxMediaPlayer.Forms;
using Plugin.RxMediaPlayer.Forms.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FullVideoView), typeof(FullVideoViewRenderer))]
namespace Plugin.RxMediaPlayer.Forms.Droid
{
    public class FullVideoViewRenderer : ViewRenderer<FullVideoView,FullPlayerViewAndroid>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<FullVideoView> e)
        {
            base.OnElementChanged(e);

            FullPlayerViewAndroid nativePlayerView = null;

            if (Control == null)
            {
                // Instantiate the native control and assign it to the Control property with
                // the SetNativeControl method
                nativePlayerView = new FullPlayerViewAndroid(Context);

                SetNativeControl(nativePlayerView);
                e.NewElement.VideoView = nativePlayerView;
            }

            if (e.OldElement != null)
            {
                // Unsubscribe from event handlers and cleanup any resources
            }

            if (e.NewElement != null)
            {
                // Configure the control and subscribe to event handlers
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
    }
}