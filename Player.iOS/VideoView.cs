using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AVFoundation;
using Foundation;
using UIKit;

namespace Player.iOS
{
    public class VideoView : UIView
    {


        public void SetPlayer(AVPlayer player)
        {
            PlayerLayer.Player = player;
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