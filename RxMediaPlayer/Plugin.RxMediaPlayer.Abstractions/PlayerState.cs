namespace Plugin.RxMediaPlayer.Abstractions
{
    public enum PlayerState
    {
        Initialized,
        Stopped,
        Paused,
        Playing,
        Loading,
        Buffering,
        Failed
    }
}