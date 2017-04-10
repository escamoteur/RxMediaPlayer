using Plugin.RxMediaPlayer.Abstractions;
using System;

#if __ANDROID__
using Android.Content;
#endif

namespace Plugin.RxMediaPlayer
{
  /// <summary>
  /// Cross platform RxMediaPlayer implemenations
  /// </summary>
  public class CrossRxMediaPlayer
  {
    static Lazy<IRxMediaPlayer> Implementation = new Lazy<IRxMediaPlayer>(() => CreateRxMediaPlayer(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

    /// <summary>
    /// Current settings to use
    /// </summary>
    public static IRxMediaPlayer Current
    {
      get
      {
        var ret = Implementation.Value;
        if (ret == null)
        {
          throw NotImplementedInReferenceAssembly();
        }
        return ret;
      }
    }

    static IRxMediaPlayer CreateRxMediaPlayer()
    {
#if PORTABLE
        return null;
#else
        return new RxMediaPlayerImplementation();
#endif
    }


#if __ANDROID__
      public static void Init(Context context)
      {
          AppContext = context;
      }

      public static Context   AppContext { get; set; }

#endif


        internal static Exception NotImplementedInReferenceAssembly()
    {
      return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
  }
}
