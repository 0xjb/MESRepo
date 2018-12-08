using System.Collections.Generic;

namespace MES.Acquintance
{
    public interface IBatch
    {

        double ProfitPerMin { get; set; }

        double Speed { get; set; }

        /// <summary>
        /// Returns the batch id
        /// </summary>
        /// <returns></returns>
        float GetBatchId();

        /// <summary>
        /// Returns the beer id
        /// </summary>
        /// <returns></returns>
        float GetBeerId();

        /// <summary>
        /// Returns the amount of acceptable products
        /// </summary>
        /// <returns></returns>
        int GetAcceptableProducts();

        /// <summary>
        /// Returns the amount of defect products
        /// </summary>
        /// <returns></returns>
        int GetDefectProducts();

        /// <summary>
        /// Returns the time stamp for the start of the production
        /// </summary>
        /// <returns></returns>
        string GetTimestampStart();

        /// <summary>
        /// Returns the time stamp for the end of the production
        /// </summary>
        /// <returns></returns>
        string GetTimestampEnd();

        /// <summary>
        /// Returns the oee
        /// </summary>
        /// <returns></returns>
        double GetOEE();

        /// <summary>
        /// Returns a list of temperatures
        /// </summary>
        /// <returns></returns>
        IList<IBatchValue> GetBatchTemperatures();

        /// <summary>
        /// Returns a list of humidities
        /// </summary>
        /// <returns></returns>
        IList<IBatchValue> GetBatchHumidities();

        /// <summary>
        /// Returns a list of vibrations
        /// </summary>
        /// <returns></returns>
        IList<IBatchValue> GetBatchVibrations();

        /// <summary>
        /// Adds to the ammount of acceptable or defect products
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="acceptable"></param>
        void AddProducts(int amount, bool acceptable);

        /// <summary>
        /// Sets the time stamp of the end of the production
        /// </summary>
        /// <param name="timestamp"></param>
        void SetTimestampEnd(string timestamp);

        /// <summary>
        /// Add an IBatchValue object
        /// </summary>
        /// <param name="values"></param>
        void AddBatchValue(IBatchValue value);

        /// <summary>
        /// Add a new IBatchValue object.
        /// Set type to a negative integer to create a temperature object.
        /// Set type to 0 to create a humidity object.
        /// Set type to a positive integer to create a vibration object.
        /// </summary>
        /// <param name="values"></param>
        void AddBatchValue(float value, string timestamp, int type);

        /// <summary>
        /// Add a list of IBatchValue objects
        /// </summary>
        /// <param name="values"></param>
        void AddBatchValues(IList<IBatchValue> values);
    }
}
