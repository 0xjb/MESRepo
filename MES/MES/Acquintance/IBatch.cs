using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Acquintance
{
    public interface IBatch
    {
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
        /// Returns a list of IBatchValue objects
        /// </summary>
        /// <returns></returns>
        IList<IBatchValueSet> GetBatchValues();

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
        /// Adds a new IBatchValue object to the batch
        /// </summary>
        /// <param name="temperature"></param>
        /// <param name="humidity"></param>
        /// <param name="vibration"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        bool AddBatchValues(float temperature, float humidity,
            float vibration, string timestamp);

        /// <summary>
        /// Set the list of IBatchValue ofjects
        /// </summary>
        /// <param name="values"></param>
        void SetBatchValueSet(IList<IBatchValueSet> values);
    }
}
