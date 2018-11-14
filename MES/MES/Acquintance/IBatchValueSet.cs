using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Acquintance
{
    public interface IBatchValueSet
    {
        /// <summary>
        /// Returns the temperature
        /// </summary>
        /// <returns></returns>
        float GetTemperature();

        /// <summary>
        /// Returns the humidity
        /// </summary>
        /// <returns></returns>
        float GetHumidity();

        /// <summary>
        /// Returns the vibration
        /// </summary>
        /// <returns></returns>
        float GetVibration();

        /// <summary>
        /// Returns the timestamp
        /// </summary>
        /// <returns></returns>
        string GetTimeStamp();
    }
}
