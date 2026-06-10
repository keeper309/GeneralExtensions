namespace GameCore.GeneralExtensions
{
    public interface ITimeWatcher
    {
        public float UnscaledTimeWithoutPause { get; }
        public float ScaledTimeWithoutPause { get; }
    }
}