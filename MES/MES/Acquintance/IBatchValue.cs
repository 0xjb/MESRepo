namespace MES.Acquintance
{
    public interface IBatchValue
    {
        float Value { get; }

        string Timestamp { get; }

        /// <summary>
        /// Negative ints = Temp
        /// 0 = Humidity
        /// Positive ints = Vibration
        /// </summary>
        int Type { get; }
    }
}
