namespace Plugin.RxMediaPlayer.Abstractions
{
    public enum PlayerState
    {
        Idle,
        Ended,
        Paused,
        Playing,
        Loading,
        Buffering,
        Failed
    }
}