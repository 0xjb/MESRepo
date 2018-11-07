using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Acquintance
{
    interface IBatch
    {
        float GetBatchId();

        float GetBeerId();

        int GetAcceptableProducts();

        int GetDefectProducts();

        float GetTemperature();

        float GetHumidity();

        float GetVibration();

        string GetTimestamp();
    }
}
