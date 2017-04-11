using System;
using AVFoundation;
using Foundation;
using Plugin.RxMediaPlayer.Abstractions;
using UIKit;

namespace Plugin.RxMediaPlayer.Views.iOS
{
    public class RxVideoView : UIView, IVideoView
    {


        public void SetPlayer(AVPlayer player)
        {
            PlayerLayer.Player = player;
        }

        public void Disconnect()
        {
            PlayerLayer.RemoveFromSuperLayer();
        }

        public AVPlayerLayer PlayerLayer
        {
            get
            {
                if (Layer.Sublayers != null)
                {
                    foreach (var sublayer in Layer.Sublayers)
                    {
                        var avLayer = sublayer as AVPlayerLayer;
                        if (avLayer != null)
                        {
                            return avLayer;
                        }
                    }
                }

                var avPlayerLayer = new AVPlayerLayer();
                avPlayerLayer.Frame = Bounds;
                Layer.AddSublayer(avPlayerLayer);

                return avPlayerLayer;
            }
        }
    }
}