using System.ComponentModel;

using Plugin.RxMediaPlayer.Forms;
using Plugin.RxMediaPlayer.Forms.iOS;
using Plugin.RxMediaPlayer.Views.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(CustomVideoView), typeof(CustomVideoViewRenderer))]
namespace Plugin.RxMediaPlayer.Forms.iOS    
{


    public class CustomVideoViewRenderer : ViewRenderer<CustomVideoView, RxVideoView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CustomVideoView> e)
        {
            base.OnElementChanged(e);

            RxVideoView nativePlayerView = null;

            if (Control == null)
            {
                // Instantiate the native control and assign it to the Control property with
                // the SetNativeControl method
                nativePlayerView = new RxVideoView();

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