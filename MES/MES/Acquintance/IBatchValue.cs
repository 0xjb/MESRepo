namespace MES.Acquintance
{
    public interface IBatchValue
    {
        /// <summary>
        /// Returns the batch value
        /// </summary>
        /// <returns></returns>
        float GetValue();

        /// <summary>
        /// Returns the timestamp
        /// </summary>
        /// <returns></returns>
        string GetTimeStamp();
    }
}
